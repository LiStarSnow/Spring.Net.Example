using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Spring.Net.Example.Dao.Config
{
    public class DBConfig
    {
        /// <summary>
        /// 数据库连接串
        /// </summary>
        public static string connString
        {
            get
            {
                string str = ConfigurationManager.ConnectionStrings["DataAppServices"].ConnectionString;
                //string str = string.Format(ConfigurationManager.ConnectionStrings["DataAppServices"].ConnectionString, Security.Secure.DecryptAES(ConfigurationManager.AppSettings["OracleDBPWD"].ToString(), "Cis20151018", "Cis21151018"));
                return str;
            }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public static readonly string TableNamePrefix = "tbCISKPI";
    }
}
