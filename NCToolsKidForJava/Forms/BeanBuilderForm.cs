using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCToolsKidForJava
{
    public partial class BeanBuilderForm : Form
    {
        public BeanBuilderForm()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dataTable = textBox1.Text;
            string[] datarows = dataTable.Split(new string[] {"\r\n"}, StringSplitOptions.None);
            string [,] data = new string[datarows.Length,3];
            StringBuilder stringBuilder = new StringBuilder();
            for ( int i=0;i< datarows.Length; i++)
            {
                string[] dataRow = datarows[i].Split(new string[] { "\t" }, StringSplitOptions.None);
                if (dataRow.Length >= 3)
                {
                    stringBuilder.Append("/**").Append(Environment.NewLine);
                    stringBuilder.Append(" * ").Append(dataRow[0]).Append(Environment.NewLine);
                    if (dataRow.Length >= 4)
                    {
                        stringBuilder.Append(" * ").Append(dataRow[3]).Append(Environment.NewLine);
                    }
                    stringBuilder.Append(" */").Append(Environment.NewLine);
                    stringBuilder.Append("private ").Append(dataRow[2]).Append(" ");
                    stringBuilder.Append(caseHump(dataRow[1]));
                    stringBuilder.Append(";").Append(Environment.NewLine); ;
                    stringBuilder.Append(Environment.NewLine);
                }
               
            }

            if (checkBox2.Checked)
            {
                string defName = textBox3.Text;
                for (int i=0; i< numericUpDown1.Value; i++)
                {
                    stringBuilder.Append("/**").Append(Environment.NewLine);
                    stringBuilder.Append(" * 备用字段").Append(i+1).Append(Environment.NewLine);
                    stringBuilder.Append(" */").Append(Environment.NewLine);
                    stringBuilder.Append("private String ").Append(defName).Append(i+1);
                    stringBuilder.Append(";").Append(Environment.NewLine); ;
                    stringBuilder.Append(Environment.NewLine);
                }
            }

            textBox2.Text = stringBuilder.ToString();
        }

        private string caseHump(string str)
        {
            if (!checkBox1.Checked)
            {
                return str;
            } 
            string[] ss = str.Split('_');
            string result = string.Empty;
            foreach (string s in ss)
            {
                result += s.Substring(0, 1).ToUpper() + s.Substring(1);
            }
            result = result.Substring(0, 1).ToLower() + result.Substring(1);
            return result;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Enabled = checkBox2.Checked;
            numericUpDown1.Enabled = checkBox2.Checked;
           
        }
    }
}
