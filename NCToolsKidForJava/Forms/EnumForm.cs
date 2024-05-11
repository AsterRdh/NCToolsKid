using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCToolsKidForJava.Forms
{
    public partial class EnumForm : Form
    {
        public EnumForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;

            //textBox1.Text = text.Replace("  = MDEnum.valueOf\\(.* ", "");
            text = Regex.Replace(text, "  = MDEnum.valueOf\\(.* ", " "); ;
            text = text.Replace("    public static final ", "");
            text = text.Replace(".valueOf(", " ");
            text = text.Replace("));", "");
            text = text.Replace("\"", "");
            text = text.Replace("java.lang.", "");
            string[] lines = text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] firstLine = lines[0].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            string ncEnumClass = firstLine[0];
            string enumClass = ncEnumClass.Replace("Enum", "");
            string ncValueClass = firstLine[2];

            string resText = "public enum " + enumClass + "  implements IBaseEnum<" + ncEnumClass + ", " + ncValueClass + "> {";
            string items = "";

            string param =
                "\tprivate " + ncEnumClass+ " ndMDEnum;" + Environment.NewLine
                + "\tprivate " + ncValueClass + " ncValue;" + Environment.NewLine
                + "\tprivate String display;" + Environment.NewLine
                ;

            string constructor =
                "\t"+enumClass + "(" + ncEnumClass + " ndMDEnum, " + ncValueClass + " ncValue, String display) {" + Environment.NewLine
                + "\t\tthis.ndMDEnum = ndMDEnum;" + Environment.NewLine
                + "\t\tthis.ncValue = ncValue;" + Environment.NewLine
                + "\t\tthis.display = display;" + Environment.NewLine
                + "\t}"
                ;

            string getFunction =
                "\tpublic static "+ enumClass + " get("+ ncValueClass+" ncValue) {" + Environment.NewLine
                + "\t\tif (ncValue==null) return null;" + Environment.NewLine
                + "\t\tswitch (ncValue){" + Environment.NewLine
                ;
            string ncEnum = "";

            foreach (string line in lines)
            {
                string[] p = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                string ncEnumName = p[1].Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1];
                string ncValue = p[3];
                string vvv = ncValue;
                if (ncValueClass.Equals("String"))
                {
                    vvv = '"' + vvv + '"';
                }
                string fff = toDD(ncValue).ToUpper();
                items = items +"\t"+ fff  + "("+ ncEnumClass+"."+ fff + ","+ vvv+",\""+ ncEnumName+"\")," + Environment.NewLine;

                getFunction = getFunction +
                    "\t\t\tcase \"" + ncValue+ "\": return "+ enumClass + "."+ fff+";" + Environment.NewLine;
                
                ncEnum = ncEnum+
                    "\tpublic static final "+ ncEnumClass+" "+ fff+ " = MDEnum.valueOf("+ ncEnumClass+".class, "+ vvv+");" + Environment.NewLine;


            }

            getFunction = getFunction 
                + "\t\t\tdefault:" + Environment.NewLine
                + "\t\t\t\treturn null;" + Environment.NewLine
                + "\t\t}" + Environment.NewLine
                + "\t}" + Environment.NewLine
                ;

            items = items + "\t;";
            textBox1.Text = 
                resText 
                + Environment.NewLine 
                + items
                + Environment.NewLine
                + Environment.NewLine
                + param
                + Environment.NewLine
                + constructor
                + Environment.NewLine
                + Environment.NewLine
                + getFunction
                + Environment.NewLine
                + "\t@Override" + Environment.NewLine
                + "\tpublic "+ ncEnumClass + " getMDEnum() {" + Environment.NewLine
                + "\t\treturn ndMDEnum;" + Environment.NewLine
                + "\t}" + Environment.NewLine

                + "\t@Override" + Environment.NewLine
                + "\tpublic " + ncValueClass + " getNcValue() {" + Environment.NewLine
                + "\t\treturn ncValue;" + Environment.NewLine
                + "\t}" + Environment.NewLine

                + "\t@Override" + Environment.NewLine
                + "\tpublic String getDisplayName() {" + Environment.NewLine
                + "\t\treturn display;" + Environment.NewLine
                + "\t}" + Environment.NewLine
                + "}"
                + Environment.NewLine + ncEnum
                ;


        }

        private string toDD(string strItem)
        {
            string strItemTarget = ""+strItem.First();  //目标字符串
            for (int j = 1; j < strItem.Length; j++)  //strItem是原始字符串
            {
                string temp = strItem[j].ToString();
                if (Regex.IsMatch(temp, "[A-Z]"))
                {
                    temp = "_" + temp.ToLower();
                }
                strItemTarget += temp;
            }
            return strItemTarget;
        }
    }
}
