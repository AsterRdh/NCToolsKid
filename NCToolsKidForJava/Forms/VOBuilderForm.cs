using System;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using NCToolsKidForJava.Utils;

namespace NCToolsKidForJava.Forms
{

    public partial class VOBuilderForm : Form
    {
        private DBUtils dbUtils = new DBUtils();
        private Dictionary<string, string> hashMap = new Dictionary<string, string>();

        public VOBuilderForm()
        {
            InitializeComponent();
        }

        private void VOBuilderForm_Load(object sender, EventArgs e)
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
                while (line != null)
                {
                    int splitIndex = line.Trim().IndexOf(" ");
                    if (splitIndex >= 0)
                    {
                        string key = line.Substring(0, splitIndex);
                        string value = line.Substring(splitIndex + 1);
                        switch (key)
                        {
                            case "DB_ADDR":

                                break;
                            case "DB_IP":

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


        private void loadMDDictionaryButton_Click(object sender, EventArgs e)
        {
            try
            {

                string sql =
                    "SELECT mCOLUMN.DISPLAYNAME,mCOLUMN.NAME "+
                    "FROM MD_COLUMN mCOLUMN " +
                      "left join md_class CLAZZ ON CLAZZ.DEFAULTTABLENAME=mCOLUMN.TABLEID " +
                      "left join MD_COMPONENT COMPONENT ON COMPONENT.ID=CLAZZ.COMPONENTID " +
                    "WHERE COMPONENT.NAME='"+ dbMdCode .Text+ "'"
                    ;
                DataSet set = dbUtils.executeQuery(sql);
                DataTable dt = set.Tables[0];
                hashMap = new Dictionary<string, string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strName1 = dt.Rows[i]["DISPLAYNAME"].ToString();
                    string strName2 = dt.Rows[i]["NAME"].ToString().ToLower();
                    if (!hashMap.ContainsKey(strName2))
                    {
                        hashMap.Add(strName2, strName1);
                    }
                 
                }
                set.Dispose();
                this.dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "加载元数据", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
            finally
            {
               
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void build_button_Click(object sender, EventArgs e)
        {
            if (hashMap == null)
            {
                hashMap= new Dictionary<string, string>();
            }
   
            DataGridViewRowCollection rows = this.dataGridView1.Rows;
          
            for(int i = 0; i < rows.Count; i++)
            {
                DataGridViewRow row = rows[i];
                DataGridViewCellCollection cells = row.Cells;
                if(cells[0].Value==null || cells[1].Value == null)
                {
                    continue;
                }
                string chName = cells[0].Value.ToString();
                string javaName = cells[1].Value.ToString();

     
                if (!hashMap.ContainsKey(javaName))
                {
                    hashMap.Add(javaName, chName);
                }
                else
                {
                    hashMap[javaName] = chName;
                }

            }



            string resVOText = resVOTextBox.Text;
            string[] dataRows = resVOText.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            StringBuilder paramsBuilder = new StringBuilder();
            StringBuilder staticParamsBuilder = new StringBuilder();
            StringBuilder gsBuilder = new StringBuilder();
            StringBuilder bgsBuilder = new StringBuilder();

            List<string> createdGetterSetter = new List<string>();

            foreach (string row in dataRows)
            {
                if (row.Trim().Length == 0) continue;
                string[] rowData= row.Split(new string[] { " " }, StringSplitOptions.None);
                string attrClass = rowData[0];
                string attrName = rowData[1];
                string trueAttrName = attrName;
                string trueAttrClass = attrClass;
                string camelArrtName = toCamel(trueAttrName);
                if (rowData.Length > 2)
                {
                    trueAttrName = rowData[2];
                }
                if (rowData.Length > 3)
                {
                    trueAttrClass = rowData[3];
                }
                string langName = attrName.ToLower();
                if (hashMap.ContainsKey(langName))
                {
                    langName = hashMap[langName];
                }
   

                //构建属性
                string param =
                    "/**" + Environment.NewLine +
                    " * " + langName + Environment.NewLine + 
                    " */" + Environment.NewLine +
                    "private " + attrClass + " " + attrName +";" + Environment.NewLine ;

                paramsBuilder.AppendLine(param);
                staticParamsBuilder.AppendLine("public static final String "+ trueAttrName.ToUpper() + "=\""+ attrName+ "\";" );
                gsBuilder.AppendLine(buildGetter(attrClass,attrName,trueAttrName,camelArrtName,trueAttrClass,langName, createdGetterSetter));
                gsBuilder.AppendLine(buildSetter(attrClass,attrName,trueAttrName,camelArrtName,trueAttrClass,langName, createdGetterSetter));
                buildOtherGetter(attrClass, attrName, bgsBuilder, createdGetterSetter);
                buildOtherSetter(attrClass, attrName, bgsBuilder, createdGetterSetter);


            }

            StringBuilder allBuilder = new StringBuilder();
            allBuilder.Append(paramsBuilder);
            allBuilder.AppendLine(" ");
            allBuilder.Append(staticParamsBuilder);
            allBuilder.AppendLine(" ");
            allBuilder.Append(gsBuilder);
            allBuilder.AppendLine(" ");
            allBuilder.Append(bgsBuilder);
            ResTextBox.Text= allBuilder.ToString();

        }


        private string buildGetter(string attrClass, string attrName, string trueAttrName, string camelArrtName, string trueAttrClass, string langName, List<string> createdGetterSetter)
        {
            string camelArrtNameF = toCamel2(trueAttrName);
            StringBuilder getter = new StringBuilder();
            if (attrClass.EndsWith("[]") || trueAttrClass.EndsWith("[]"))
            {
                //构造备注
                string trueAttrClassNotArray = trueAttrClass.Replace("[]","");
                string attrClassNotArray = attrClass.Replace("[]", "");
                if (trueAttrClassNotArray.Equals(attrClassNotArray))
                {
                    switch (trueAttrClassNotArray)
                    {
                        case "UFBoolean":
                            getter = getter.AppendLine("/**")
                                          .Append(" * 获取").Append(langName).AppendLine("数组")
                                          .Append(" * @return ").Append(langName).AppendLine("数组")
                                          .AppendLine(" */")
                                          .Append("public Boolean[] get").Append(camelArrtNameF).AppendLine("(){")
                                          .Append("\tif(this.").Append(attrName).AppendLine(" == null) return null;")
                                          .Append("\tBoolean[] booleans = new Boolean[").Append(attrName).AppendLine(".length];")
                                          .Append("\tfor (int i = 0; i < ").Append(attrName).AppendLine(".length; i++) {")
                                          .Append("\t\tUFBoolean ub = ").Append(attrName).AppendLine("[i];")
                                          .AppendLine("\t\tbooleans[i] = ub == null ? false : ub.booleanValue();")
                                          .AppendLine("\t}")
                                          .AppendLine("\treturn booleans;")
                                          .AppendLine("}");
                            createdGetterSetter.Add(("get"+ camelArrtNameF).ToLower());

                            getter = getter.AppendLine("/**")
                                           .Append(" * 获取").Append(langName).AppendLine("列表")
                                           .Append(" * @return ").Append(langName).AppendLine("列表")
                                           .AppendLine(" */")
                                           .Append("public List<Boolean> get").Append(camelArrtNameF).AppendLine("List(){")
                                           .Append("\tif(").Append(camelArrtNameF).AppendLine(" == null) return new ArrayList<>();")
                                           .Append("\treturn Arrays.stream(").Append(camelArrtNameF).AppendLine(").map(i->i==null?false:i.booleanValue()).collect(Collectors.toList());")
                                           //.Append("\treturn this.").Append(attrName).Append(" == null ? new ArrayList<>() : new ArrayList<>(Arrays.asList(this.").Append(attrName).AppendLine("):")
                                           .AppendLine("}");
                            createdGetterSetter.Add(("get" + camelArrtNameF + "List").ToLower());
                            break;
                        default:
                            getter = getter.AppendLine("/**")
                                           .Append(" * 获取").Append(langName).AppendLine("数组")
                                           .Append(" * @return ").Append(langName).AppendLine("数组")
                                           .AppendLine(" */")
                                           .Append("public ").Append(trueAttrClassNotArray).Append("[] get").Append(camelArrtNameF).AppendLine("(){")
                                           .Append("\treturn this.").Append(attrName).AppendLine(":")
                                           .AppendLine("}");
                            createdGetterSetter.Add(("get" + camelArrtNameF).ToLower());

                            getter = getter.AppendLine("/**")
                                           .Append(" * 获取").Append(langName).AppendLine("列表")
                                           .Append(" * @return ").Append(langName).AppendLine("列表")
                                           .AppendLine(" */")
                                           .Append("public List<").Append(trueAttrClassNotArray).Append("> get").Append(camelArrtNameF).AppendLine("List(){")
                                           .Append("\treturn this.").Append(attrName).Append(" == null ? new ArrayList<>() : new ArrayList<>(Arrays.asList(this.").Append(attrName).AppendLine(");")
                                           .AppendLine("}");
                            createdGetterSetter.Add(("get" + camelArrtNameF + "List").ToLower());
                            break;
                    }
                }
                else
                {
                    if (attrClass.EndsWith("[]")){
                        getter = getter.AppendLine("/**")
                                   .Append(" * 获取").Append(langName).AppendLine("列表")
                                   .Append(" * @return ").Append(langName).AppendLine("列表")
                                   .AppendLine(" */")
                                   .Append("public List<").Append(trueAttrClassNotArray).Append("> get").Append(camelArrtNameF).AppendLine("List(){")
                                   .Append("\tif (this.").Append(attrName).Append(" == null || ").Append("this.").Append(attrName).AppendLine(".length == 0) return  ArrayList<>();")
                                   .Append("\treturn Arrays.stream(this.").Append(attrName).Append(").map(i->").Append(trueAttrClassNotArray).AppendLine(".get(i)).collect(Collectors.toList());")
                                   .AppendLine("}");
                        createdGetterSetter.Add(("get" + camelArrtNameF + "List").ToLower());

                    }
                    else
                    {

                        getter = getter.AppendLine("/**")
                                   .Append(" * 获取").Append(langName).AppendLine("列表")
                                   .Append(" * @return ").Append(langName).AppendLine("列表")
                                   .AppendLine(" */")
                                   .Append("public List<").Append(trueAttrClassNotArray).Append("> get").Append(camelArrtNameF).AppendLine("List(){")
                                   .Append("\tif (this.").Append(attrName).AppendLine(" == null) return  ArrayList<>();")
                                   .Append("\tString[] split = ").Append(attrName).AppendLine(".trim().split(\",\");")
                                   .AppendLine("\treturn Arrays.stream(split)").Append("\t\t.map(i->");
                        if (trueAttrClassNotArray.Equals("Boolean"))
                        {
                            getter = getter.Append("i!=null && ( i.equals(\"Y\") || i.equals(\"y\") || i.equals(\"true\") || i.equals(\"TRUE\") || i.equals(\"1\")").AppendLine("）)");
                        }
                        else
                        {
                            getter = getter.Append(trueAttrClassNotArray).AppendLine(".get(i)");
                        }

                        getter = getter.AppendLine("\t\t.collect(Collectors.toList());")
                                       .AppendLine("}");
                        createdGetterSetter.Add(("get" + camelArrtNameF + "List").ToLower());

                    }
                    
                }
               
            }
            else
            {
                //构造备注
                getter = getter.AppendLine("/**")
                           .Append(" * 获取").AppendLine(langName)
                           .Append(" * @return ").AppendLine(langName)
                           .AppendLine(" */");
                if (trueAttrClass.Equals(attrClass))
                {
                    switch (trueAttrClass)
                    {
                        case "UFBoolean":
                            //构建真实变量名称
                            if (attrName.StartsWith("is_"))
                            {
                                getter = getter.Append("public boolean ").Append(camelArrtName).AppendLine("(){")
                                               .Append("\treturn this.").Append(attrName).Append("==null ? false : this.").Append(attrName).AppendLine(".booleanValue();")
                                               .AppendLine("}");
                                createdGetterSetter.Add((camelArrtName).ToLower());
                            }
                            else
                            {
                                getter = getter.Append("public boolean is").Append(camelArrtNameF).AppendLine("(){")
                                               .Append("\treturn this.").Append(attrName).Append("==null ? false : this.").Append(attrName).AppendLine(".booleanValue();")
                                               .AppendLine("}");
                                createdGetterSetter.Add(("is" + camelArrtNameF).ToLower());

                            }
                            break;
                        default:
                            getter = getter.Append("public ").Append(trueAttrClass).Append(" get").Append(camelArrtNameF).AppendLine("(){")
                                           .Append("\t").Append("return this.").Append(attrName).AppendLine(";")
                                           .AppendLine("}");
                            createdGetterSetter.Add(("get" + camelArrtNameF).ToLower());
                            break;
                    }
                }
                else
                {
                    getter = getter.Append("public ").Append(trueAttrClass).Append(" get").Append(camelArrtNameF).AppendLine("(){")
                                        .Append("\t").Append("return ").Append(trueAttrClass).Append(".get(this.").Append(attrName).AppendLine(");")
                                        .AppendLine("}");
                    createdGetterSetter.Add(("get" + camelArrtNameF).ToLower());
                }
                   
            }
            return getter.ToString();
        }

        private string buildSetter(string attrClass, string attrName, string trueAttrName, string camelArrtName, string trueAttrClass, string langName, List<string> createdGetterSetter)
        {
            string camelArrtNameF = toCamel2(trueAttrName);
            StringBuilder setter = new StringBuilder();
            //todo 
            if (trueAttrClass.EndsWith("[]") || attrClass.EndsWith("[]"))
            {
                string trueAttrClassNotArray = trueAttrClass.Replace("[]", "");
                string attrClassNotArray = attrClass.Replace("[]", "");
                if (attrClass.EndsWith("[]")) {
                    if (attrClassNotArray.Equals(trueAttrClassNotArray))
                    {
                        setter = setter.AppendLine("/**")
                                        .Append(" * 设置").Append(langName).AppendLine("数组")
                                        .Append(" * @param ").Append(langName).AppendLine("数组")
                                        .AppendLine(" */")
                                        .Append("public void set").Append(camelArrtNameF).Append("(").Append(trueAttrClass).Append(" ").Append(camelArrtName).AppendLine("){")
                                        .Append("\tthis.").Append(attrName).Append(" = ").Append(camelArrtName).AppendLine(";")
                                        .AppendLine("}");
                        setter = setter.AppendLine("/**")
                                        .Append(" * 获取").Append(langName).AppendLine("列表")
                                        .Append(" * @param ").Append(langName).AppendLine("列表")
                                        .AppendLine(" */")
                                        .Append("public void set").Append(camelArrtNameF).Append("List(").Append("List<").Append(trueAttrClassNotArray).Append("> ").Append(camelArrtName).AppendLine(" ){")
                                        .Append("\tif(").Append(camelArrtName).Append(" == null) this.").Append(attrName).AppendLine(" = null;")
                                        .Append("\tthis.").Append(attrName).Append(" = ").Append(camelArrtName).Append(".toArray(new ").Append(trueAttrClassNotArray).AppendLine("[0]);")
                                        .AppendLine("}");
                        setter = setter.AppendLine("/**")
                                        .Append(" * 添加").AppendLine(langName)
                                        .Append(" * @param ").AppendLine(langName)
                                        .AppendLine(" */")
                                        .Append("public void add").Append(camelArrtNameF).Append("(").Append(trueAttrClassNotArray).Append(" ... ").Append(camelArrtName).AppendLine(" ){")
                                        .Append("\tList<").Append(trueAttrClassNotArray).Append("> list = this.get").Append(camelArrtNameF).AppendLine("List();")
                                        .Append("\tif(").Append(camelArrtName).AppendLine(" == null){")
                                        .AppendLine("\t\tlist.add(null);")
                                        .AppendLine("\t}else{")
                                        .Append("\t\tfor(").Append(trueAttrClassNotArray).Append(" ").Append("var : ").Append(camelArrtName).AppendLine("){")
                                        .AppendLine("\t\t\tlist.add(var);")
                                        .AppendLine("\t\t}")
                                        .AppendLine("\t}")
                                        .Append("\tthis.").Append(attrName).Append(" = ").Append("list.toArray(new ").Append(trueAttrClassNotArray).AppendLine("[0]);")
                                        .AppendLine("}");
                        setter = setter.AppendLine("/**")
                                        .Append(" * 附加").AppendLine(langName)
                                        .Append(" * @param ").AppendLine(langName)
                                        .AppendLine(" */")
                                        .Append("public void append").Append(camelArrtNameF).Append("(Collection<").Append(trueAttrClassNotArray).Append("> ").Append(camelArrtName).AppendLine(" ){")
                                        .Append("\tif(").Append(camelArrtName).AppendLine(" == null){")
                                        .AppendLine("\t\treturn;")
                                        .AppendLine("\t}else{")
                                        .Append("\t\tList<").Append(trueAttrClassNotArray).Append("> list = this.get").Append(camelArrtNameF).AppendLine("List();")
                                        .Append("\t\tlist.addAll(").Append(camelArrtName).AppendLine(");")
                                        .Append("\t\tthis.").Append(attrName).Append(" = ").Append("list.toArray(new ").Append(trueAttrClassNotArray).AppendLine("[0]);")
                                        .AppendLine("\t}")
                                        .AppendLine("}");
                    }
                    else
                    {
                        setter = setter.AppendLine("/**")
                                       .Append(" * 设置").Append(langName).AppendLine("数组")
                                       .Append(" * @param ").Append(langName).AppendLine("数组")
                                       .AppendLine(" */")
                                       .Append("public void set").Append(camelArrtNameF).Append("(").Append(trueAttrClass).Append(" ").Append(camelArrtName).AppendLine("){")
                                       .Append("\t//todo 定制设置").Append(langName).AppendLine("数组方法")
                                       .AppendLine("}");
                        createdGetterSetter.Add(("set" + camelArrtNameF).ToLower());
                        setter = setter.AppendLine("/**")
                                        .Append(" * 获取").Append(langName).AppendLine("列表")
                                        .Append(" * @param ").Append(langName).AppendLine("列表")
                                        .AppendLine(" */")
                                        .Append("public void set").Append(camelArrtNameF).Append("List(").Append("List<").Append(trueAttrClassNotArray).Append("> ").Append(camelArrtName).AppendLine(" ){")
                                        .Append("\t//todo 定制设置").Append(langName).AppendLine("数组方法")
                                        .AppendLine("}");
                        createdGetterSetter.Add(("set" + camelArrtNameF+ "List").ToLower());
                        setter = setter.AppendLine("/**")
                                        .Append(" * 添加").AppendLine(langName)
                                        .Append(" * @param ").AppendLine(langName)
                                        .AppendLine(" */")
                                        .Append("public void add").Append(camelArrtNameF).Append("(").Append(trueAttrClassNotArray).Append(" ... ").Append(camelArrtName).AppendLine(" ){")
                                        .Append("\t//todo 定制设置").Append(langName).AppendLine("数组方法")
                                        .AppendLine("}");
                        setter = setter.AppendLine("/**")
                                        .Append(" * 附加").AppendLine(langName)
                                        .Append(" * @param ").AppendLine(langName)
                                        .AppendLine(" */")
                                        .Append("public void append").Append(camelArrtNameF).Append("(Collection<").Append(trueAttrClassNotArray).Append("> ").Append(camelArrtName).AppendLine(" ){")
                                        .Append("\t//todo 定制设置").Append(langName).AppendLine("数组方法")
                                        .AppendLine("}");
                    }
                }
                else
                {
                    if (trueAttrClassNotArray.Equals("Boolean"))
                    {
                        //setArray
                        setter = setter.AppendLine("/**")
                                        .Append(" * 设置").Append(langName).AppendLine("数组")
                                        .Append(" * @param ").Append(langName).AppendLine("数组")
                                        .AppendLine(" */")
                                        .Append("public void set").Append(camelArrtNameF).Append("(").Append(trueAttrClassNotArray).Append("[] ").Append(camelArrtName).AppendLine("){")
                                        .Append("\tif(").Append(camelArrtName).Append(" == null ) {this.").Append(attrName).AppendLine(" = null; return;}")
                                        .Append("\tthis.").Append(attrName).Append(" = CollectionUtils.caseToString(Arrays.stream(").Append(camelArrtName).AppendLine(")")
                                        .Append("\t\t.map(i->").Append("i!=null&&i ? \"Y\":\"N\"").AppendLine(")")
                                        .AppendLine("\t\t.collect(Collectors.toList()));")
                                        .AppendLine("}");
                        createdGetterSetter.Add(("set" + camelArrtNameF).ToLower());
                        //setList
                        setter = setter.AppendLine("/**")
                                         .Append(" * 获取").Append(langName).AppendLine("列表")
                                         .Append(" * @param ").Append(langName).AppendLine("列表")
                                         .AppendLine(" */")
                                         .Append("public void set").Append(camelArrtNameF).Append("List(").Append("List<").Append(trueAttrClassNotArray).Append("> ").Append(camelArrtName).AppendLine(" ){")
                                         .Append("\tif(").Append(camelArrtName).Append(" == null ) {this.").Append(attrName).AppendLine(" = null; return;}")
                                         .Append("\tthis.").Append(attrName).Append(" = CollectionUtils.caseToString(").Append(camelArrtName).AppendLine(".stream()")
                                         .Append("\t\t.map(i->").Append("i!=null&&i ? \"Y\":\"N\"").AppendLine(")")
                                         .Append("\t\t.collect(Collectors.toList()));")
                                         .AppendLine("}");
                        createdGetterSetter.Add(("set" + camelArrtNameF+"List").ToLower());
                        //addItem
                        setter = setter.AppendLine("/**")
                                           .Append(" * 添加").AppendLine(langName)
                                           .Append(" * @param ").AppendLine(langName)
                                           .AppendLine(" */")
                                           .Append("public void add").Append(camelArrtNameF).Append("(").Append(trueAttrClassNotArray).Append(" ... ").Append(camelArrtName).AppendLine(" ){")
                                            .Append("\tif(").Append(camelArrtName).Append(" == null) this.").Append(attrName).Append(" = this.").Append(attrName).AppendLine("==null?\"false\":\",false\";")
                                            .AppendLine("\telse {")
                                            .Append("\tthis.").Append(attrName).Append(" = this.").Append(attrName).Append("==null?\"\":(this.").Append(attrName).AppendLine(" + \",\");")
                                            .Append("\t\tthis.").Append(attrName).Append(" += CollectionUtils.caseToString(Arrays.stream(").Append(camelArrtName).AppendLine(")")
                                            .Append("\t\t\t\t.map(i->").Append("i!=null&&i ? \"Y\":\"N\"").AppendLine(")")
                                            .AppendLine("\t\t\t\t.collect(Collectors.toList())")
                                            .AppendLine("\t\t);")
                                            .AppendLine("\t}")
                                           .AppendLine("}");
                        //appendItems
                        setter = setter.AppendLine("/**")
                                         .Append(" * 附加").AppendLine(langName)
                                         .Append(" * @param ").AppendLine(langName)
                                         .AppendLine(" */")
                                         .Append("public void append").Append(camelArrtNameF).Append("(Collection<").Append(trueAttrClassNotArray).Append("> ").Append(camelArrtName).AppendLine(" ){")
                                         .Append("\tif(this.").Append(attrName).AppendLine(" == null) {")
                                        .Append("\t\tthis.").Append(attrName).Append(" = CollectionUtils.caseToString(").Append(camelArrtName).AppendLine(".stream()")
                                        .Append("\t\t\t\t.map(i->").Append("i!=null&&i ? \"Y\":\"N\"").AppendLine(")")
                                        .AppendLine("\t\t\t\t.collect(Collectors.toList())")
                                        .AppendLine("\t\t);")
                                        .AppendLine("\t\treturn;")
                                        .AppendLine("\t}else{")
                                        .Append("\t\t\tthis.").Append(attrName).Append(" += \",\" + CollectionUtils.caseToString(").Append(camelArrtName).AppendLine(".stream()")
                                        .Append("\t\t\t\t.map(i->").Append("i!=null&&i ? \"Y\":\"N\"").AppendLine(")")
                                        .AppendLine("\t\t\t\t.collect(Collectors.toList())")
                                        .AppendLine("\t\t);")
                                        .AppendLine("\t}")
                                         .AppendLine("}");
                    }
                    else
                    {
                        //setArray
                        setter = setter.AppendLine("/**")
                                        .Append(" * 设置").Append(langName).AppendLine("数组")
                                        .Append(" * @param ").Append(langName).AppendLine("数组")
                                        .AppendLine(" */")
                                        .Append("public void set").Append(camelArrtNameF).Append("(").Append(trueAttrClassNotArray).Append("[] ").Append(camelArrtName).AppendLine("){")
                                        .Append("\tif(").Append(camelArrtName).Append(" == null ) {this.").Append(attrName).AppendLine(" = null; return;}")
                                        .Append("\tthis.").Append(attrName).Append(" = CollectionUtils.caseToString(Arrays.stream(").Append(camelArrtName).AppendLine(")")
                                        .Append("\t\t.map(i->").Append("i==null?\"\":").Append(trueAttrClassNotArray).Append(".get(i).getNcValue()").AppendLine(")")
                                        .AppendLine("\t\t.collect(Collectors.toList()));")
                                        .AppendLine("}");

                        //setList
                        setter = setter.AppendLine("/**")
                                         .Append(" * 获取").Append(langName).AppendLine("列表")
                                         .Append(" * @param ").Append(langName).AppendLine("列表")
                                         .AppendLine(" */")
                                         .Append("public void set").Append(camelArrtNameF).Append("List(").Append("List<").Append(trueAttrClassNotArray).Append("> ").Append(camelArrtName).AppendLine(" ){")
                                         .Append("\tif(").Append(camelArrtName).Append(" == null ) {this.").Append(attrName).AppendLine(" = null; return;}")
                                         .Append("\tthis.").Append(attrName).Append(" = CollectionUtils.caseToString(").Append(camelArrtName).AppendLine(".stream()")
                                         .Append("\t\t.map(i->").Append("i==null?\"\":").Append(trueAttrClassNotArray).Append(".get(i).getNcValue()").AppendLine(")")
                                         .Append("\t\t.collect(Collectors.toList()));")
                                         .AppendLine("}");

                        //addItem
                        setter = setter.AppendLine("/**")
                                           .Append(" * 添加").AppendLine(langName)
                                           .Append(" * @param ").AppendLine(langName)
                                           .AppendLine(" */")
                                           .Append("public void add").Append(camelArrtNameF).Append("(").Append(trueAttrClassNotArray).Append(" ... ").Append(camelArrtName).AppendLine(" ){")
                                            .Append("\tif(").Append(camelArrtName).Append(" == null) this.").Append(attrName).Append(" = this.").Append(attrName).AppendLine("==null?\"false\":\",false\";")
                                            .AppendLine("\telse {")
                                            .Append("\tthis.").Append(attrName).Append(" = this.").Append(attrName).Append("==null?\"\":(this.").Append(attrName).AppendLine(" + \",\");")
                                            .Append("\t\tthis.").Append(attrName).Append(" += CollectionUtils.caseToString(Arrays.stream(").Append(camelArrtName).AppendLine(")")
                                            .Append("\t\t\t\t.map(i->").Append("i==null?\"\":").Append(trueAttrClassNotArray).Append(".get(i).getNcValue()").AppendLine(")")
                                            .AppendLine("\t\t\t\t.collect(Collectors.toList())")
                                            .AppendLine("\t\t);")
                                            .AppendLine("\t}")
                                           .AppendLine("}");
                        //appendItems
                        setter = setter.AppendLine("/**")
                                         .Append(" * 附加").AppendLine(langName)
                                         .Append(" * @param ").AppendLine(langName)
                                         .AppendLine(" */")
                                         .Append("public void append").Append(camelArrtNameF).Append("(Collection<").Append(trueAttrClassNotArray).Append("> ").Append(camelArrtName).AppendLine(" ){")
                                         .Append("\tif(this.").Append(attrName).AppendLine(" == null) {")
                                        .Append("\t\tthis.").Append(attrName).Append(" = CollectionUtils.caseToString(").Append(camelArrtName).AppendLine(".stream()")
                                        .Append("\t\t\t\t.map(i->").Append("i==null?\"\":").Append(trueAttrClassNotArray).Append(".get(i).getNcValue()").AppendLine(")")
                                        .AppendLine("\t\t\t\t.collect(Collectors.toList())")
                                        .AppendLine("\t\t);")
                                        .AppendLine("\t\treturn;")
                                        .AppendLine("\t}else{")
                                        .Append("\t\t\tthis.").Append(attrName).Append(" += \",\" + CollectionUtils.caseToString(").Append(camelArrtName).AppendLine(".stream()")
                                        .Append("\t\t\t\t.map(i->").Append("i==null?\"\":").Append(trueAttrClassNotArray).Append(".get(i).getNcValue()").AppendLine(")")
                                        .AppendLine("\t\t\t\t.collect(Collectors.toList())")
                                        .AppendLine("\t\t);")
                                        .AppendLine("\t}")
                                         .AppendLine("}");
                    }
                }

               
               
            }
            else
            {
                setter = setter.AppendLine("/**")
                               .Append(" * 设置").AppendLine(langName)
                               .Append(" * @param ").Append(camelArrtName).Append(" ").AppendLine(langName)
                               .AppendLine(" */");
                if (trueAttrClass.Equals(attrClass))
                {
                    switch (trueAttrClass)
                    {
                        case "UFBoolean":
                            if (attrName.StartsWith("is_"))
                            {
                                camelArrtNameF = camelArrtNameF.Substring(2);
                                setter = setter.Append("public void set").Append(camelArrtNameF).Append("(boolean ").Append(camelArrtName).AppendLine("){")
                                          .Append("\tthis.").Append(attrName).Append(" =  new UFBoolean(").Append(camelArrtName).AppendLine(");")
                                          .AppendLine("}");
                                createdGetterSetter.Add(("set" + camelArrtNameF).ToLower());
                            }
                            else
                            {
                                setter = setter.Append("public void set").Append(camelArrtNameF).Append("B(boolean ").Append(camelArrtName).AppendLine("){")
                                         .Append("\tthis.").Append(attrName).Append(" =  new UFBoolean(").Append(camelArrtName).AppendLine(");")
                                         .AppendLine("}");
                                createdGetterSetter.Add(("set" + camelArrtNameF+"B").ToLower());

                            }
                            break;
                        default:
                            setter = setter.Append("public void set").Append(camelArrtNameF).Append("(").Append(trueAttrClass).Append(" ").Append(camelArrtName).AppendLine("){")
                                           .Append("\tthis.").Append(attrName).Append(" = ").Append(camelArrtName).AppendLine(";")
                                           .AppendLine("}");
                            createdGetterSetter.Add(("set" + camelArrtNameF).ToLower());
                            break;
                    }
                }
                else
                {
                    setter = setter.Append("public void set").Append(camelArrtNameF).Append("(").Append(trueAttrClass).Append(" ").Append(camelArrtName).AppendLine("){")
                                          .Append("\tthis.").Append(attrName).Append(" = ").Append(camelArrtName).AppendLine(".getNcValue();")
                                          .AppendLine("}");

                    createdGetterSetter.Add(("set" + camelArrtNameF).ToLower());
                }
            }
            return setter.ToString();
        }

        private void buildOtherGetter(string attrClass, string attrName, StringBuilder bgsBuilder, List<string> createdGetterSetter)
        {
            string attrBigFirtst = attrName.Substring(0,1).ToUpper()+ attrName.Substring(1);
            if(createdGetterSetter.IndexOf(("get" + attrBigFirtst ).ToLower()) < 0)
            {
                string getter =
                    "public " + attrClass + " get" + attrBigFirtst + "(){" + Environment.NewLine
                    + "\t return this." + attrName + ";" + Environment.NewLine
                    + "}" + Environment.NewLine
                    ;
                bgsBuilder.AppendLine(getter);
            }
        
        }


        private void buildOtherSetter(string attrClass, string attrName, StringBuilder bgsBuilder, List<string> createdGetterSetter)
        {
            string attrBigFirtst = attrName.Substring(0, 1).ToUpper() + attrName.Substring(1);
            if (createdGetterSetter.IndexOf(("set" + attrBigFirtst).ToLower()) < 0)
            {
                string setter =
                    "public void set" + attrBigFirtst + "("+ attrClass+" "+attrName+"){" + Environment.NewLine
                    + "\tthis." + attrName + "="+ attrName + ";" + Environment.NewLine
                    + "}" + Environment.NewLine
                    ;
                bgsBuilder.AppendLine(setter);
            }

        }

        private string toCamel(string var)
        {
          
            string[] strItems = var.Trim().Split('_');
            string camare = strItems[0];
            for (int j = 1; j < strItems.Length; j++)
            {
                string temp = strItems[j].ToString();
                string temp1 = temp[0].ToString().ToUpper();
                string temp2 = "";
                temp2 = temp1 + temp.Remove(0, 1);
                camare += temp2;
            }

            return camare;
        }

        private string toCamel2(string var)
        {
            string camare = "";
            string[] strItems = var.Trim().Split('_');
            for (int j = 0; j < strItems.Length; j++)
            {
                string temp = strItems[j].ToString();
                string temp1 = temp[0].ToString().ToUpper();
                string temp2 = "";
                temp2 = temp1 + temp.Remove(0, 1);
                camare += temp2;
            }

            return camare;
        }
        private void formattingButton_Click(object sender, EventArgs e)
        {
            string resVOText = resVOTextBox.Text;
            string[] dataRows = resVOText.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            string pattern1 = ".*private .*\\.";
            string pattern2 = "  ;";
            string resVOText2 = "";
            foreach (string row in dataRows)
            {
                string rowN = Regex.Replace(row, pattern1, "");
                rowN = Regex.Replace(rowN, pattern2, "");
                resVOText2 += rowN + Environment.NewLine;

            }
            resVOTextBox.Text = resVOText2;
        }
    }
}
