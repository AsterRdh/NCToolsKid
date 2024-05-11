using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NCToolsKidForJava.Utils;

namespace NCToolsKidForJava.Forms
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            //加载配置文件
            Dictionary<String, String> config = new ConfigUtil().readConfig();
            dbIPTextBox.Text = get(config, "dbIP");
            dbSIDTextBox.Text = get(config, "dbSID");
            dbUsernameTextBox.Text = get(config, "dbUsername");
            dbPasswordTextBox.Text = get(config, "dbPassword");

        }

        private string get(Dictionary<String, String> map,string key)
        {
            if (map.ContainsKey(key))
            {
                return map[key];
            }
            return "";
        }

        private void save()
        {
            Dictionary<String, String> config = new Utils.ConfigUtil().readConfig();
            config["dbIP"] = dbIPTextBox.Text;
            config["dbSID"] = dbSIDTextBox.Text;
            config["dbUsername"] = dbUsernameTextBox.Text;
            config["dbPassword"] = dbPasswordTextBox.Text;
            new ConfigUtil().saveConfig(config);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            save();
        }

        private void TestBDConfigButton_Click(object sender, EventArgs e)
        {
            DBTestRes testRes = new DBUtils().TestBDConfig(dbIPTextBox.Text, dbSIDTextBox.Text, dbUsernameTextBox.Text, dbPasswordTextBox.Text);
            if (testRes.isSucceed)
            {
                MessageBox.Show("测试成功", "测试链接", MessageBoxButtons.OKCancel);
            }
            else
            {
                MessageBox.Show(testRes.msg, "测试链接", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }


        }

        private void SaveCloseButton_Click(object sender, EventArgs e)
        {
            save();
            this.Close();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
