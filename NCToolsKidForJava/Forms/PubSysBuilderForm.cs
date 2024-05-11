using NCToolsKidForJava.Utils;
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
    public partial class PubSysBuilderForm : Form
    {
        private const string V = "null";
        private DBUtils dbUtils = new DBUtils();
        public PubSysBuilderForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //加载模块
            try
            {
                String sqlModuls =
                    "select d.MODULEID MODULE_ID,decode( dp.SYSTYPENAME,null,'',dp.SYSTYPENAME || '-') ||d.SYSTYPENAME SYSTYPE_NAME"+
                    " from dap_dapsystem d"+
                    " left join dap_dapsystem dp on d.PARENTCODE=dp.MODULEID"+
                    " order by d.MODULEID";
                DataSet set = dbUtils.executeQuery(sqlModuls);
                DataTable dt = set.Tables[0];
                dataSet1.Tables[0].Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strName1 = dt.Rows[i]["MODULE_ID"].ToString();
                    string strName2 = dt.Rows[i]["SYSTYPE_NAME"].ToString();
                    dataSet1.Tables[0].Rows.Add(strName1, strName2);
                }

                   
            }
            catch(Exception ex)
            {

            }
            comboBox1.SelectedIndex = 0;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pk = getPK();
            string org = textBox2.Text;
            string code = textBox3.Text;
            string name = textBox1.Text;
            
            string type = comboBox1.SelectedItem.ToString();
            string stateFlag = "0";
            switch (type)
            {
                case "整数":
                    type = "3";
                    break;
                case "字符串":
                    type = "2";
                    break;
                case "布尔型":
                    type = "1";
                    stateFlag = "2";
                    break;
                default:

                    break;
            }
           

            string value = textBox8.Text;
            string defValue = textBox5.Text;
            string vlaueRan = textBox6.Text;
            string memo = textBox7.Text;
            string now = DateTime.Now.ToString();
            now = now.Replace("/", "-");
            if (String.IsNullOrWhiteSpace(defValue))
            {
                defValue = V;
            }
            else
            {
                defValue = "'" + defValue + "'";
            }
            if (String.IsNullOrWhiteSpace(vlaueRan))
            {
                vlaueRan = V;
            }
            else
            {
                vlaueRan = "'" + vlaueRan + "'";

            }

            DataRowView moduleRow =(DataRowView) comboBox2.SelectedItem;
            string moduleID= moduleRow.Row["MODULE_ID"].ToString();
            string sqla =
                "INSERT INTO PUB_SYSINIT (" +
                    "CONTROLFLAG, DATAORIGINFLAG, DR, EDITFLAG, INITCODE, INITNAME, MODIFIEDTIME, MODIFIER, PK_ORG, " +
                    "PK_SYSINIT, SYSINIT, TS, VALUE)" +
                "VALUES(" +
                    "'N', 0, 0, 'Y', '"+code+"', '" + name + "', '', '', '"+ org + "', " +
                    "'"+ pk + "', '"+ pk + "', '"+ now + "', '"+ value + "'" +
                "); ";
            string sqlb=
                "INSERT INTO PUB_SYSINITTEMP("+
                    "AFTERCLASS, APPTAG, CHECKCLASS, CHECKREGEX, DATACLASS, DATAORIGINFLAG, DATASVAECLASS, DEFAULTVALUE, DOMAINFLAG, "+
                    "DR, EDITCOMPONENTCTRLCLASS, EDITVALUEPATH, GROUPCODE, GROUPNAME, INITCODE, INITNAME, MAINFLAG, META, MUTEXFLAG, "+
                    "ORGTYPECONVERTMODE, PARATYPE, PK_ORGTYPE, PK_REFINFO, PK_SYSINITTEMP, REF_CONDCLASS, REF_NAMEMAPPING, REFPATH, "+
                    "REMARK, SHOWFLAG, STATEFLAG, SYSFLAG, SYSINDEX, TS, VALUELIST, VALUETYPE)"+
                "VALUES( "+
                    "null, null, null, null, null, null, null, "+defValue+", '"+ moduleID +"'," +
                    "null, null, null, '~', null, '"+ code + "', '"+name+"', 'N', null, 0,"+
                    "'" + org + "', 'business', '" + org + "', '~', '"+ pk + "', null, null, null," +
                    "'"+ memo + "', 'Y', "+ stateFlag+", 'N', 0, '"+ now + "', "+ vlaueRan + ", "+ type +
                ");";
            textBox9.Text = "--"+name+Environment.NewLine + sqla +Environment.NewLine + sqlb;




        }
        public string GetRandomString(int length)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }
        public string getPK()
        {
            return "1001" + GetRandomString(16);
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "整数":
                    textBox6.Text = "0-8";
                    textBox5.Text = "0";
                    break;
                case "布尔型":
                    textBox6.Text = "Y,N";
                    textBox5.Text = "N";
                    break;
                case "字符串":
                    textBox6.Text = "";
                    break;
                default:
                    break;
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
