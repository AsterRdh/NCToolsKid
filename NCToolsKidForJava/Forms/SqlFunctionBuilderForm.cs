using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCToolsKidForJava.Forms
{
    struct SqlStruct
    {
        public String sql;
        public String note;
    }

    struct Param
    {
        public String name;
        public String clazz;
        public String note;
    }
    public partial class SqlFunctionBuilderForm : Form
    {
        public SqlFunctionBuilderForm()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String functionName = functionNameBox.Text;
            String functionDescribe = functionDescribeBox.Text;
            String[] sqlLines = sqlBox.Lines;
            List<SqlStruct> trueSqlLines = new List<SqlStruct>();
            List<Param> functionParams = new List<Param>();

            string[] separatingStrings = { "--" };
            int lineCounter = 0;
            foreach (String line in sqlLines)
            {
                if (line.Trim().Length == 0)
                {
                    continue;
                }
               
                if (line.Trim().StartsWith("--"))
                {
                    if(lineCounter == 1 && functionName.Length==0)
                    {
                        functionName = line.Trim().Substring(2);
                    }else if (lineCounter == 0 && functionDescribe.Length == 0)
                    {
                        functionDescribe = line.Trim().Substring(2);
                    }
                }
                else
                {
                    SqlStruct sqlStruct = new SqlStruct();
                    //去除注释内容
                    String[] linePart = line.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
                    if(linePart.Length == 0) { continue; }

                    String lineSql = linePart[0];
                    String note = linePart.Length > 1 ? linePart[1] : null;
                    if (lineSql.Contains("$"))
                    {
                        String[] sqlParts = lineSql.Split('$');
                        String trueSql = sqlParts[0];
                        for (int i = 1; i < sqlParts.Length; i++)
                        {
                            String sqlPart = sqlParts[i];
                            bool isParam = i%2 == 1;
                            if (isParam)
                            {
                                String[] strings = sqlPart.Split(' ');
                                Param param = new Param();
                                param.name = strings[0];
                                if (strings.Length >1)
                                {
                                    param.clazz = strings[1];
                                }
                                else
                                {
                                    param.clazz = "String";
                                }
                                if(strings.Length > 2)
                                {
                                    param.note = strings[2];
                                }
                                functionParams.Add(param);
                                trueSql += "\" + " + param.name + " + \"";
                            }
                            else
                            {
                                trueSql += sqlPart;
                            }

                            sqlStruct.sql = trueSql;
                        }
                    }
                    else
                    {
                        sqlStruct.sql = lineSql;
                    }
                    sqlStruct.note = note;
                    trueSqlLines.Add(sqlStruct);
                }
                lineCounter++;
            }
            functionNameBox.Text = functionName;
            functionDescribeBox.Text = functionDescribe;



            String res = "/**" + Environment.NewLine +
                         " * " + functionDescribe + Environment.NewLine;
            functionParams.ForEach(param =>
            {
                if (param.note != null)
                {
                    res += " * @param " + param.name + " " + param.note + Environment.NewLine;
                }
            });
            res += " * @return ";
            lineCounter = 0;
            trueSqlLines.ForEach(sqlLine =>
            {
                String sqlN = sqlLine.sql;
                sqlN = sqlN.Replace("  ", "&#8194;");
                String[] ss = sqlN.Split('+');
                sqlN = ss[0];
                if (ss.Length > 1)
                {
                    sqlN = sqlN.Substring(0, sqlN.Length - 2);
                    for (int i = 1; i < ss.Length; i++)
                    {
                        String sss = ss[i].Trim();
                        if (i % 2 == 1)
                        {
                            sqlN += "{@code "+ sss + "}";
                        }
                        else
                        {
                            if (sss.StartsWith("\""))
                            {
                                sss = sss.Substring(1);
                            }
                            if (sss.EndsWith("\""))
                            {
                                sss = sss.Substring(0, sss.Length - 1);
                            }
                            sqlN += sss;
                        }
                    }
                }
                


                if (lineCounter == 0)
                {
                    res += "<p>" + sqlN + "</p>" + Environment.NewLine;
                }
                else
                {
                    res += " *         <p>" + sqlN + "</p>" + Environment.NewLine;
                }
                lineCounter++;
            });
            res += 
                " **/" + Environment.NewLine +
                "public static String " + functionName + "(";
            int paramsCounter = 0;
            functionParams.ForEach(param =>
            {
                res += param.clazz + " " + param.name ;
                if (++paramsCounter < functionParams.Count) {
                    res += ", ";
                }
            });
            res += "){" + Environment.NewLine+
                "\treturn ";
            lineCounter = 0;
            trueSqlLines.ForEach(line =>
            {
                if (lineCounter != 0)
                {
                    res += "\t\t";

                }
                res += "\"" + line.sql + "\"";
                if( ++lineCounter < trueSqlLines.Count)
                {
                    res += " + ";
                }
                else
                {
                    res += ";";
                   
                }
                if (line.note != null)
                {
                    res += "//" + line.note;
                }
                res += Environment.NewLine;


            });
            res +=  "}";
            resFunctionBox.Text = res;

        }
    }
}
