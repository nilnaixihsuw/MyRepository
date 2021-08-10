using Mediinfo.Enterprise;
using Mediinfo.Enterprise.Config;
using Mediinfo.Enterprise.Exceptions;
using Mediinfo.HIS.Core;
using Mediinfo.Infrastructure.Core.Config;
using Mediinfo.Infrastructure.Core.DBContext;
using Mediinfo.Utility;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediinfo.Infrastructure.JCJG
{
    /// <summary>
    /// HIS连接
    /// </summary>
    class HISConnectDTO
    {
        /// <summary>
        /// 连接名
        /// </summary>
        public string CONNECTNAME { get; set; }
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string CONNECTSTRING { get; set; }
    }


    /// <summary>
    /// HIS 连接上下文
    /// </summary>
    public class HISConnDBContext : DBContextBase
    {
        /// <summary>
        /// 单例
        /// </summary>
        private static HISConnDBContext Instance;
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string HISConnectionString { get; set; }
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static HISConnDBContext()
        {
            Database.SetInitializer<HISConnDBContext>(null);
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="contextOwnsConnection"></param>
        /// <param name="defaultSchema"></param>
        private HISConnDBContext(OracleConnection conn, bool contextOwnsConnection, string defaultSchema):base(conn,contextOwnsConnection,defaultSchema)
        {
            
        }

        /// <summary>
        /// 测试数据库是否正常连接
        /// </summary>
        /// <returns></returns>
        public bool TestDBConnection()
        {
            HISGlobalSetting config = HISGlobalSetting.Load();

            string connStr = config.GetConfigItemValue(HISGlobalSetting.LoginDBUser, "连接字符串", string.Empty);
            connStr = DESHelper.Decrypt(connStr, HISGlobalSetting.Key);

            if (string.IsNullOrWhiteSpace(connStr))
            {
                return false;
            }

            //判断连接用户是否能够登录成功
            try
            {
                OracleConnectionStringBuilder oraConn = new OracleConnectionStringBuilder(connStr);
                OracleConnection loginConn = new OracleConnection();

                loginConn.ConnectionString = oraConn.ConnectionString;
                loginConn.Open();
                loginConn.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Object locker = new Object();
        /// <summary>
        /// 获取单例
        /// </summary>
        /// <returns></returns>
        public static HISConnDBContext GetInstance()
        {
            lock (locker)
            {
                if (null == Instance)
                {
                    //HISGlobalSetting config = HISGlobalSetting.Load();

                    //string connStr = config.GetConfigItemValue(HISGlobalSetting.LoginDBUser, "连接字符串", string.Empty);
                    //connStr = DESHelper.Decrypt(connStr, HISGlobalSetting.Key);

                    //if (string.IsNullOrWhiteSpace(connStr))
                    //{
                    //    throw new DBException("数据库登录用户配置不正确", ReturnCode.LOGINDBCONNECTERROR);
                    //}
                    //string connStrTest = @"Data Source= (DESCRIPTION =     (ADDRESS_LIST =       (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.1.159)(PORT = 1521))     )     (CONNECT_DATA =       (SERVICE_NAME = orcl)     )   );User ID=HIS3;Password=HIS3;Min Pool Size=10;Connection Lifetime=120;Connection Timeout=600;Incr Pool Size=5;Decr Pool Size=2;";
                    //OracleConnectionStringBuilder oraConn = new OracleConnectionStringBuilder(connStr);
                    //OracleConnection loginConn = new OracleConnection();

                    ////判断连接用户是否能够登录成功
                    //try
                    //{
                    //    loginConn.ConnectionString = connStrTest;
                    //    loginConn.Open();
                    //}
                    //catch (Exception ex)
                    //{
                    //    throw new DBException(ex.Message, ReturnCode.LOGINDBCONNECTERROR);
                    //}


                    try
                    {

                        OracleConnectionStringBuilder oraConn = new OracleConnectionStringBuilder(MediinfoConfig.GetValue("DbConfig.xml", "HIS6"));
                        OracleConnection loginConn = new OracleConnection(MediinfoConfig.GetValue("DbConfig.xml", "HIS6"));

                        Instance = new HISConnDBContext(loginConn, true, oraConn.UserID.ToUpper());
                        Instance.HISConnectionString = MediinfoConfig.GetValue("DbConfig.xml", "HIS6");

                        //List<HISConnectDTO> connList;
                        //connList = Instance.Database.SqlQuery<HISConnectDTO>("Select connectname, connectstring from xt_connect", new OracleParameter()).ToList();

                        //HISConnectDTO hisDB = connList.Where(c => c.CONNECTNAME == "HIS").FirstOrDefault();
                        //Instance.HISConnectionString = DESHelper.Decrypt(hisDB.CONNECTSTRING, HISGlobalSetting.Key);

                        ////需要强制讲主数据库的DataSource 换成登录用户的DataSource,防止出现数据库中的配置和客户端不一致的情况
                        //OracleConnectionStringBuilder main = new OracleConnectionStringBuilder(Instance.HISConnectionString);
                        //main.DataSource = loginConn.DataSource;

                        //string connStrTest1 = @"Data Source= (DESCRIPTION =     (ADDRESS_LIST =       (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.1.159)(PORT = 1521))     )     (CONNECT_DATA =       (SERVICE_NAME = orcl)     )   );User ID=HIS6;Password=HIS6;Min Pool Size=10;Connection Lifetime=120;Connection Timeout=600;Incr Pool Size=5;Decr Pool Size=2;";

                        ////Instance.HISConnectionString = main.ConnectionString;
                        //Instance.HISConnectionString = connStrTest1;

                        ////写入配置文件，供外部读取
                        //config.SetConfigItemValue(HISGlobalSetting.MainDBUser, "连接字符串", DESHelper.Decrypt(Instance.HISConnectionString, HISGlobalSetting.Key));

                        //foreach (var item in connList.Where(c => c.CONNECTNAME != "HIS").ToList())
                        //{
                        //    config.SetConfigItemValue(item.CONNECTNAME, "连接字符串", item.CONNECTSTRING);
                        //}

                        //主FTP
                        //HISConnectDTO ftp = connList.Where(c => c.CONNECTNAME == "主FTP").FirstOrDefault();
                        //if (null == ftp)
                        //{
                        //    config.SetConfigItemValue(HISGlobalSetting.MainFTP, "连接字符串", "");
                        //}
                        //else
                        //{
                        //    config.SetConfigItemValue(HISGlobalSetting.MainFTP, "连接字符串", DESHelper.Decrypt(ftp.CONNECTSTRING,HISGlobalSetting.Key));
                        //}

                        ////备FTP
                        //ftp = connList.Where(c => c.CONNECTNAME == "备FTP").FirstOrDefault();
                        //if (null == ftp)
                        //{
                        //    config.SetConfigItemValue(HISGlobalSetting.BakFTP, "连接字符串", "");
                        //}
                        //else
                        //{
                        //    config.SetConfigItemValue(HISGlobalSetting.BakFTP, "连接字符串", DESHelper.Decrypt(ftp.CONNECTSTRING,HISGlobalSetting.Key));
                        //}

                        //MQ
                        //HISConnectDTO mq = connList.Where(c => c.CONNECTNAME == "MQ").FirstOrDefault();
                        //if (null == ftp)
                        //{
                        //    config.SetConfigItemValue(HISGlobalSetting.MainMQ, "MQ链接信息", "");
                        //}
                        //else
                        //{
                        //    config.SetConfigItemValue(HISGlobalSetting.MainMQ, "MQ链接信息", DESHelper.Decrypt(mq.CONNECTSTRING, HISGlobalSetting.Key));
                        //}
                    }
                    catch (Exception ex)
                    {
                        throw new DBException(ex.Message, ReturnCode.MAINDBCONNECTERROR);
                    }
                    finally
                    {
                        //if (loginConn.State == System.Data.ConnectionState.Open)
                        //    loginConn.Close();
                    }
                }

                return Instance;
            }
        }
    }
}
