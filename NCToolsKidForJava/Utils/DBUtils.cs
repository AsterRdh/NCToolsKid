using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCToolsKidForJava.Utils
{
    public partial class DBUtils
    {
        private OracleConnection conn;
        public DBTestRes TestBDConfig(string ip, string sid, string username, string password)
        {
            string dbConnectContext = buidlDBConnectContext(ip,sid,username,password);
            conn = new OracleConnection(dbConnectContext);
            try
            {
                conn.Open();
                return new DBTestRes(true, "测试成功");

            }
            catch (Exception ex)
            {
                //message = "错误：" + ex.Message.ToString();
                //re = false;
                //return null;

                return new DBTestRes(false, "错误：" + ex.Message.ToString());

            }
            finally
            {
                conn.Close();
            }
        }

        string buidlDBConnectContext(string ip,string sid,string username,string password)
        {
            string connectString = "User ID=" + username + ";Password=" + password + ";Data Source=" + ip + "/ " + sid;
            return connectString;

        }

        public DataSet executeQuery(String sql)
        {
            Dictionary<String, String> configs = new ConfigUtil().readConfig();
            string ip = MapUtil.get(configs, "dbIP");
            string sid = MapUtil.get(configs, "dbSID");
            string username = MapUtil.get(configs, "dbUsername");
            string password = MapUtil.get(configs, "dbPassword");
            if (ip == null || sid == null || username ==null || password == null)
            {
                throw new Exception("未配置数据库链接信息");
            }


            string dbConnectContext = buidlDBConnectContext(ip,sid, username, password);
            conn = new OracleConnection(dbConnectContext);
            OracleDataAdapter adapter = null;
            try
            {
                conn.Open();
                adapter = new OracleDataAdapter(sql, conn);
                DataSet set = new DataSet();
                adapter.Fill(set);
                return set;
            }
            catch(Exception ex)
            {
                throw new Exception("错误：" + ex.Message.ToString());
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                adapter.Dispose();
            }
        }
    }

    public struct DBTestRes
    {
        public bool isSucceed;
        public string msg;
        public DBTestRes(bool isSucceed, string msg)
        {
            this.isSucceed = isSucceed;
            this.msg = msg;
        }

        
    }


}
