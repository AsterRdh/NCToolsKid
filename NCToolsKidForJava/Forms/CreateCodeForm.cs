using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NCToolsKidForJava.Utils;

namespace NCToolsKidForJava.Forms
{
    public partial class CreateCodeForm : Form
    {
        public CreateCodeForm()
        {
            InitializeComponent();
        }

        private void CreateCodeForm_Load(object sender, EventArgs e)
        {
            Dictionary<String, String> config = new ConfigUtil().readConfig();
            if (config.ContainsKey("GenerateCodeLib"))
            {
                GenerateCodeLibTextBox.Text = config["GenerateCodeLib"];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = folderBrowserDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                GenerateCodeLibTextBox.Text = folderBrowserDialog1.SelectedPath;
            }

        }

        private void CreateCodeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            new ConfigUtil().saveConfig("GenerateCodeLib", GenerateCodeLibTextBox.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = GenerateCodeLibTextBox.Text;
            string name = textBox2.Text;
            if (path != null && name !=null)
            {
                if (!path.EndsWith("\\"))
                {
                    path = path + "\\";
                }
                path = path + textBox2.Text;
                
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string path1 = path + "\\前端";
              
                if (!Directory.Exists(path1))
                {
                    Directory.CreateDirectory(path1);
                }

                string path2 = path + "\\后端";
                if (!Directory.Exists(path2))
                {
                    Directory.CreateDirectory(path2);
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GenerateCodeLibTextBox.Text + "\\" + textBox2.Text + "\\前端");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GenerateCodeLibTextBox.Text + "\\" + textBox2.Text + "\\后端");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string fontPath = GenerateCodeLibTextBox.Text + "\\" + textBox2.Text + "\\前端";
            string backPath = GenerateCodeLibTextBox.Text + "\\" + textBox2.Text + "\\后端";
            caseJavaToUTF8(backPath);
            removeJSError(fontPath);
            MessageBox.Show("调整完成", "调整完成", MessageBoxButtons.OKCancel);
            // 
        }
        private void removeJSError(String path)
        {
            Queue<FileSystemInfo> myQ = new Queue<FileSystemInfo>();
            DirectoryInfo directory = new DirectoryInfo(path);
            myQ.Enqueue(directory);
            while (myQ.Count > 0)
            {
                FileSystemInfo file = myQ.Dequeue();
                if (file is DirectoryInfo)
                {
                    FileSystemInfo[] fsinfos = ((DirectoryInfo)file).GetFileSystemInfos();
                    foreach (FileSystemInfo fsi in fsinfos)
                    {
                        myQ.Enqueue(fsi);
                    }
                }
                else
                {
                    FileInfo fileInfo = (FileInfo)file;
                    if (fileInfo.Name.EndsWith(".js"))
                    {
                        string text = File.ReadAllText(fileInfo.FullName, Encoding.UTF8);
                        text = text.Replace(" ", " ");
                        File.WriteAllText(fileInfo.FullName, text, Encoding.UTF8);
                    }
                }
            }
        }



        private void caseJavaToUTF8(String path )
        {
            Queue<FileSystemInfo> myQ = new Queue<FileSystemInfo>();
            DirectoryInfo directory = new DirectoryInfo(path);
            myQ.Enqueue(directory);
            while(myQ.Count > 0)
            {
                FileSystemInfo file = myQ.Dequeue();
                if(file is DirectoryInfo)
                {
                    FileSystemInfo[] fsinfos =((DirectoryInfo)file).GetFileSystemInfos();
                    foreach(FileSystemInfo fsi in fsinfos)
                    {
                        myQ.Enqueue(fsi);
                    }
                }
                else
                {
                    FileInfo fileInfo = (FileInfo)file;
                    if (fileInfo.Name.EndsWith(".java"))
                    {
                        string text = File.ReadAllText(fileInfo.FullName,Encoding.GetEncoding("GBK"));
                        text = text.Replace("nccloud.web.codeplatform.framework.action.base.BaseAction", "nccloud.web.ceri.pub.action.framework.CeriBaseAction");
                        text = text.Replace(" BaseAction", " CeriBaseAction");
                        text = text.Replace("nccloud.web.codeplatform.framework.action.base.RequestDTO", "nccloud.web.ceri.pub.action.framework.CERIRequestDTO");
                        text = text.Replace("RequestDTO ", "CERIRequestDTO ");
                        text = text.Replace("RequestDTO.class", "CERIRequestDTO.class");
                        text = text.Replace("paramWapper", "paramWrapper");
                        Encoding utf8WithoutBom = new System.Text.UTF8Encoding(false);
                        File.WriteAllText(fileInfo.FullName, text, utf8WithoutBom);
                    }
                }
            }
        }
    }
}
