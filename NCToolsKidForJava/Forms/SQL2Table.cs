using System;
using System.Collections;
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
    public partial class SQL2Table : Form
    {
        public SQL2Table()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                String sqlAll = textBox1.Text;
                String[] rows = sqlAll.Split(new string[] { "INSERT INTO" }, StringSplitOptions.None);
                Dictionary<String, tbv> dataMap = new Dictionary<String, tbv>();
                for (int i = 0; i < rows.Length; i++)
                {
                    if (rows[i].Length == 0) continue;
                    String row = rows[i];
                    int index = row.IndexOf("(");
                    String tableName = row.Substring(0, index);
                    String ln = row.Substring(index);
                    String[] lss = ln.Split(new string[] { "VALUES" }, StringSplitOptions.None);

                    if (dataMap.ContainsKey(tableName))
                    {
                        String n = lss[1];
                        n = n.Replace(" ", "").Replace("\n", "").Replace("\r", "");
                        n = n.Substring(n.IndexOf("(") + 1);
                        n = n.Substring(0, n.LastIndexOf(")"));
                        dataMap[tableName].list.Add(n.Split(','));
                    }
                    else
                    {
                        String l = lss[0];
                        l = l.Replace(" ", "").Replace("\n", "").Replace("\r", "");
                        l = l.Substring(l.IndexOf("(") + 1);
                        l = l.Substring(0, l.Length - 1);
                        tbv b = new tbv();
                        b.attar = l.Split(',');
                        b.list = new ArrayList();
                        String n = lss[1];
                        n = n.Replace(" ", "").Replace("\n", "").Replace("\r", "");
                        n = n.Substring(n.IndexOf("(") + 1);
                        n = n.Substring(0, n.LastIndexOf(")"));
                        b.list.Add(n.Split(','));
                        dataMap.Add(tableName, b);
                    }
                }
                String res = "";
                foreach (KeyValuePair<String, tbv> kvp in dataMap)
                {

                    String row = kvp.Key + "\r\n";
                    for (int attrIndex = 0; attrIndex < kvp.Value.attar.Length; attrIndex++)
                    {
                        String attr = kvp.Value.attar[attrIndex];
                        String datas = "";
                        foreach (String[] item in kvp.Value.list)
                        {
                            datas = datas + item[attrIndex] + "\t";
                        }
                        row = row + attr + "\t" + datas + "\r\n";
                    }
                    res = res + row + "\r\n";
                }
                textBox2.Text = res;
            }
            else
            {
                String table = textBox2.Text;
                String[] datas = table.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                Dictionary<String, tbv2> dataMap = new Dictionary<String, tbv2>();
                Dictionary<String, int> dataMap2 = new Dictionary<String, int>();
                String nowTable = "";
                foreach (String valRow in datas)
                {
                    String[] rr = valRow.Split(new string[] { "\t" }, StringSplitOptions.None);
                    if (rr.Length == 1)
                    {
                        if (rr.Length == 0|| rr[0].Length==0) continue;
                        nowTable = rr[0];
                        tbv2 dd = new tbv2();
                        dd.attar = new ArrayList();
                        dd.list = new ArrayList();
                        dd.rc = 0;
                        dataMap.Add(rr[0], dd);
                    }
                    else if (rr.Length > 1)
                    {
                        tbv2 dd = dataMap[nowTable];
                        dd.attar.Add(rr[0]);
                        String[] d= rr.Skip(1).ToArray();
                        dd.list.Add(d);
                        if (dataMap2.ContainsKey(nowTable))
                        {
                            dataMap2[nowTable] = d.Length;
                        }
                        else
                        {
                            dataMap2.Add(nowTable, d.Length);
                        }
                    }
                }
                String sqlAll = "";
                foreach (KeyValuePair<String, tbv2> kvp in dataMap)
                {
                    tbv2 data = kvp.Value;
                    for (int i = 0; i < dataMap2[kvp.Key]; i++)
                    {
                        String sql = "insert into " + kvp.Key + " (";
                        foreach(String attr in data.attar)
                        {
                            sql += attr + ", ";
                        }
                        sql = sql.Substring(0, sql.Length - 2)+") values (";
                        foreach (String[] dataRow in data.list)
                        {
                            sql += dataRow[i] + ", ";
                        }
                        sql = sql.Substring(0, sql.Length - 2) + ");";
                        sqlAll += sql + "\n\r"; 
                    }
                }
                textBox1.Text = sqlAll;
            }
        }
        private struct tbv
        {
            public String[] attar;
            public ArrayList list;
        }

        private struct tbv2
        {
            public ArrayList attar;
            public ArrayList list;
            public int rc;
        }
    }
    
}
