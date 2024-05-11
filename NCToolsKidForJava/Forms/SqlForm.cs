using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace NCToolsKidForJava.Forms
{
    public partial class SqlForm : Form
    {
        public SqlForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                //空就默认从桌面出发,然后获取点击的路径
                string defaultfilePath = folderBrowserDialog1.SelectedPath;
                textBox1.Text = defaultfilePath;

                DirectoryInfo directory = new DirectoryInfo(defaultfilePath);
                TreeNode node = treeView1.Nodes.Add(directory.FullName, directory.Name);
                Dictionary<String, TreeNode> nodeMap = new Dictionary<String, TreeNode>
                {
                    { directory.FullName, node }
                };

                DirectoryInfo[] directories = directory.GetDirectories();


                Stack<DirectoryInfo> directoryInfos = new Stack<DirectoryInfo>(directories);
                while (directoryInfos.Count > 0)
                {
                    DirectoryInfo subDirectory = directoryInfos.Pop();

                    String upper = subDirectory.Parent.FullName;
                    TreeNode upperNode = null;
                    bool get = nodeMap.TryGetValue(upper,out upperNode);
                    if (get)
                    {
                        TreeNode subNode = upperNode.Nodes.Add(subDirectory.FullName, subDirectory.Name);
                        nodeMap.Add(subDirectory.FullName, subNode);
                        DirectoryInfo[] directories1 = subDirectory.GetDirectories();
                        for (int i = 0; i < directories1.Length; i++)
                        {
                            directoryInfos.Push(directories1[i]);
                        }
                    
                    }

                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //结构应该是
        }
    }
}
