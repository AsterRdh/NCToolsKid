using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace NCToolsKidForJava
{
    public partial class LanCaller : Form
    {
        public LanCaller()
        {
            InitializeComponent();
        }

        Socket socketSend;
        Boolean isHost = false;
        Boolean isCon = false;
        Dictionary<string, Socket> diSocket = new Dictionary<string, Socket>();

        private void LanCaller_Load(object sender, EventArgs e)
        {
            SelfIPLabel.Text = GetIpAdress();
        }


        private string GetIpAdress()
        {
            try
            {
                string HostName = Dns.GetHostName(); //得到主机名
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址
                    //AddressFamily.InterNetwork表示此IP为IPv4,
                    //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return IpEntry.AddressList[i].ToString();
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void CreateRoomButton_Click(object sender, EventArgs e)
        {
            try
            {
                //创建一个socket，负责监听IP地址和端口号
                Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Parse(SelfIPLabel.Text.Trim());        //提供一个IP地址
                int port = int.Parse(SelfPortBox.Text);
                IPEndPoint point = new IPEndPoint(ip, port);      //端口号，其中包括了IP地址
                socketWatch.Bind(point);            //绑定IP地址和端口
                RoomStateLabel.Text = "监听成功";
                socketWatch.Listen(10);             //设置监听队列，最多允许同时10个连接，（0表示不限制）
                Thread th = new Thread(Listen);
                th.IsBackground = true;
                th.Start(socketWatch);              //socketWatch是Listen()的参数
                listBox1.Items.Add("Host: "+ SelfIPLabel.Text.Trim());
                isHost = true;
                groupBox2.Enabled = false;
                HostIpBox.Enabled = false;
                HostPortBox.Enabled = false;
                JoinRoomButton.Enabled = false;
                CreateRoomButton.Enabled = false;
                SelfPortBox.Enabled = false;
            }
            catch
            {

            }
        }

        private void Listen(Object o)
        {
            Socket socketWatch = o as Socket;
            while (true)
            {
                try
                {
                    //接下来等待客户端的连接
                    socketSend = socketWatch.Accept();   //为客户端创建一个负责通信的新的socket并接收来自客户端的请求
                                                         //将新的socket存放进集合中
                    diSocket.Add(socketSend.RemoteEndPoint.ToString(), socketSend);
                    //将远程连接的IP地址和端口号存入下拉框
                    listBox1.Items.Add(socketSend.RemoteEndPoint.ToString());
                    LogTextBox.Text = LogTextBox.Text + socketSend.RemoteEndPoint.ToString() + ":连接成功" + Environment.NewLine ;

                    Thread th = new Thread(ReceiveSend);
                    //开启一个新线程不停接收来自客户端的消息
                    th.IsBackground = true;
                    th.Start(socketSend);

                    //推送当前ip列表到客户端

                }
                catch
                {

                }


            }
        }

        /// <summary>
        /// 服务器端不停的接收客户端发送过来的消息
        /// </summary>
        /// <param name="o"></param>
        private void ReceiveSend(Object o)
        {
            Socket socketSend = o as Socket;
            while (true)
            {
                try
                {
                    //客户端连接成功后，服务器应该接受客户端发出的消息
                    byte[] buffer = new byte[1024 * 1024 * 1];
                    int r = socketSend.Receive(buffer);         // 实际接收到的有效字节数
                    if (r == 0)
                    {
                        break;
                    }
                    string str = Encoding.UTF8.GetString(buffer, 0, r);         //将字节流转换为string【str就是客户端向服务器发送的字符串】
                    //转发给所有客户端
                    btnSendMsgHost(str);
                    textBox1.Text = textBox1.Text + str + Environment.NewLine;
                }
                catch
                {

                }
            }
        }

        private void sendMessageButton_Click(object sender, EventArgs e)
        {
            string str = textBox3.Text.Trim();
            str = GetIpAdress() + ": " + str;
            if (isHost)
            {
                btnSendMsgHost(str);
                textBox1.AppendText( str + Environment.NewLine);
                textBox3.Text = "";       //清空文本框
            }
            else
            {
                btnSendMsgClient(str);
            }
        }

        private void btnSendMsgClient(string str)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
            socketSend.Send(buffer);
            textBox3.Text = "";
        }

        private void btnSendMsgHost(string str)
        {           //将服务器端的信息发送给客户端
           
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
            //socketSend.Send(buffer);
            //获得用户在下拉框中选中的IP地址，根据IP地址找到对应的socket,然后发送消息
            //string ip = cboUsers.SelectedIndex.ToString();
            foreach (var item in diSocket)
            {
                item.Value.Send(buffer);
            }
        }

        private void JoinRoomButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isCon)
                {
                    //创建一个socket用来连接服务器
                    socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPAddress ip = IPAddress.Parse(HostIpBox.Text.Trim());      //获取IP地址
                    IPEndPoint ipep = new IPEndPoint(ip, Convert.ToInt32(HostPortBox.Text.Trim()));

                    //获得要连接的远程服务器的IP地址和端口号
                    socketSend.Connect(ipep);
                    LogTextBox.AppendText("连接成功" + Environment.NewLine);
                    //创建一个线程，用来不断接收服务器端发送的信息
                    Thread th = new Thread(ReceiveMessage);
                    th.IsBackground = true;     //后台线程
                    th.Start(socketSend);       //socketSend是Start()的参数
                    isCon = true;
                    groupBox2.Enabled = false;
                    HostIpBox.Enabled = false;
                    HostPortBox.Enabled = false;
                    JoinRoomButton.Enabled = false;
                    CreateRoomButton.Enabled = false;
                    SelfPortBox.Enabled = false;
                }
               
            }
            catch
            {
                isCon = false;
            }
        }
        private void ReceiveMessage(Object o)
        {
            Socket socketSend = o as Socket;
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024 * 1024 * 1];
                    int r = socketSend.Receive(buffer);
                    if (r == 0)
                    {
                        break;
                    }
                    string str = Encoding.UTF8.GetString(buffer, 0, r);
                    textBox1.AppendText(str + Environment.NewLine);
                }
            }
            catch
            {
            }
        }

    }

    
}
