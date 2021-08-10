using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mediinfo.Infrastructure.HIS
{
    /// <summary>
    /// 特别注意：请不要随便更改TypeName,ElementName等属性，包括大小写！！！！
    /// 变种单例模式，因为xml序列化必须要求public的序列化方法，暂时不考虑线程安全
    /// </summary>

    [XmlType(TypeName = "HISGlobalSetting")]

    public class HISGlobalSetting : HISSetting
    {        
        private static HISGlobalSetting _instance = null;

        public static readonly string LoginDBUser = "数据库登录用户";
        public static readonly string MainDBUser = "数据库主用户";
        public static readonly string MainFTP = "主FTP";
        public static readonly string BakFTP = "备FTP";
        public static readonly string MainMQ = "MQ";
        public static readonly string Key = "MediHis5";

        /// <summary>
        /// 构造函数中需要进行初始化成员变量
        /// </summary>
        public HISGlobalSetting() : base()
        {
           // DBList = new List<DBConfig>();
          //  FTPList = new List<FTPServer>();
        }

        /// <summary>
        /// 加载默认的全局配置文件（HISGlobalSetting.xml）
        /// </summary>
        /// <returns></returns>
        public static HISGlobalSetting Load()
        {
            if (_instance == null)
            {
                _instance = Load<HISGlobalSetting>("HISGlobalSetting.xml");
            }
            return _instance;
        }

        ///// <summary>
        ///// 数据库配置列表
        ///// </summary>
        //[XmlArray("数据库列表")]
        //public List<DBConfig> DBList { get; set; }

        ///// <summary>
        ///// FTP服务器器地址
        ///// </summary>
        //[XmlArray("FTP服务器")]
        //public List<FTPServer> FTPList { get; set; }
    }

    ///// <summary>
    ///// FTP配置信息
    ///// </summary>
    //[XmlType(TypeName = "FTPServer")]
    //public class FTPServer
    //{
    //    public FTPServer()
    //    {

    //    }

    //    [XmlAttribute(AttributeName = "Name")]
    //    public string Name { get; set; }

    //    [XmlAttribute(AttributeName = "Server")]
    //    public string Server { get; set; }

    //    [XmlAttribute(AttributeName = "Port")]
    //    public int Port { get; set; }

    //    [XmlAttribute(AttributeName = "UserName")]
    //    public string UserName { get; set; }

    //    [XmlAttribute(AttributeName = "Password")]
    //    public string Password { get; set; }

    //}

    ///// <summary>
    ///// 数据库配置信息
    ///// </summary>
    //[XmlType(TypeName = "Database")]
    //public class DBConfig
    //{
    //    public DBConfig()
    //    {
    //    }

    //    public DBConfig(string name, string userName, string passWord, string dataSource)
    //    {
    //        Name = name;
    //        UserName = userName;
    //        Password = passWord;
    //        DataSource = dataSource;
    //    }

    //    [XmlAttribute(AttributeName = "Name")]
    //    public string Name { get; set; }

    //    [XmlAttribute(AttributeName = "UserName")]
    //    public string UserName { get; set; }

    //    [XmlAttribute(AttributeName = "Password")]
    //    public string Password { get; set; }

    //    [XmlAttribute(AttributeName = "DataSource")]
    //    public string DataSource { get; set; }
    //}
}
