using System;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;

namespace NCToolsKidForJava
{
    public partial class AutoPack : Form
    {
        public AutoPack()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                fontFilePath.Enabled = true;
                button3.Enabled = true;

            }
            else
            {
                fontFilePath.Enabled = false;
                fontFilePath.Text = "";
                button3.Enabled = false;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                backFilePath.Text = openFileDialog1.FileName;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = outFilePath.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                outFilePath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = fontFilePath.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                fontFilePath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FileInfo fi1 = new FileInfo(backFilePath.Text);


            if (fi1.Exists)
            {
                
                String newPath = outFilePath.Text + "\\" + fi1.Name;
                String newPathWithoutEnd = newPath.Substring(0, newPath.Length - 4);
                FileInfo fi2 = new FileInfo(newPath);
                int i = 1;
                while (fi2.Exists)
                {
                    newPath = newPathWithoutEnd + "(" + i++ + ").zip";
                    fi2 = new FileInfo(newPath);
                }
                fi1.CopyTo(newPath);

                String exPath = newPath.Substring(0, newPath.Length - 4);
                ZipFile.ExtractToDirectory(newPath, exPath);

                if (checkBox1.Checked)
                {
                    FileInfo fi3 = new FileInfo(fontFilePath.Text);
                    String fontPath = exPath + "\\replacement\\hotwebs\\nccloud\\resources";
                    CopyDirectory(fontFilePath.Text, fontPath);
                    //fi3.CopyTo(fontPath,true);
                }


            }
        }

        private void AutoPack_Load(object sender, EventArgs e)
        {
            String configFilePath = System.IO.Directory.GetCurrentDirectory() + "\\config.data";
            FileInfo conFile = new FileInfo(configFilePath);
            if (!conFile.Exists)
            {
                StreamWriter sw = new StreamWriter(configFilePath);
                sw.WriteLine("");
                sw.Close();

            }
            StreamReader sr = new StreamReader(configFilePath);
            String line;
            try
            {
                line = sr.ReadLine();
                while(line != null)
                {
                    int splitIndex = line.Trim().IndexOf(" ");
                    if (splitIndex >= 0)
                    {
                        string key = line.Substring(0, splitIndex);
                        string value = line.Substring(splitIndex + 1);

                        switch (key)
                        {
                            case "backFilePath":
                                backFilePath.Text = value;
                                break;
                            case "fontFilePath":
                                fontFilePath.Text = value;
                                break;
                            case "outFilePath":
                                outFilePath.Text = value;
                                break;
                           default:
                                break;

                        }
                    }
                    line = sr.ReadLine();
                }

                sr.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        private void AutoPack_FormClosing(object sender, FormClosingEventArgs e)
        {
            String configFilePath = System.IO.Directory.GetCurrentDirectory() + "\\config.data";
            FileInfo conFile = new FileInfo(configFilePath); 
            if (conFile.Exists)
            {
                conFile.Delete();   

            }
            StreamWriter sw = new StreamWriter(configFilePath);
            sw.WriteLine("backFilePath " + backFilePath.Text);
            sw.WriteLine("fontFilePath " + fontFilePath.Text);
            sw.WriteLine("outFilePath " + outFilePath.Text);
            sw.Close();
        }

        public static void CopyDirectory(string srcPath, string destPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath); FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)     //判断是否文件夹
                    {
                        if (!Directory.Exists(destPath + "\\" + i.Name))
                        {
                            Directory.CreateDirectory(destPath + "\\" + i.Name);   //目标目录下不存在此文件夹即创建子文件夹
                        }
                        CopyDirectory(i.FullName, destPath + "\\" + i.Name);    //递归调用复制子文件夹
                    }
                    else
                    {
                        File.Copy(i.FullName, destPath + "\\" + i.Name, true);      //不是文件夹即复制文件，true表示可以覆盖同名文件
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
