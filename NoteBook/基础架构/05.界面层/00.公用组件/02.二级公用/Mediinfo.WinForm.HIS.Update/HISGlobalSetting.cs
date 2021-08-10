using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mediinfo.WinForm.HIS.Update
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
        /// 检测文件夹下得配置文件版本号
        /// </summary>
        public static List<string> UpdateDirectorys = new List<string>();
        /// <summary>
        /// 公用目录，必须下载项
        /// </summary>
        public static List<string> GongYongDirectorys = new List<string> { "PIC", "AssemblyClient" };
        /// <summary>
        /// 是否是HTTP更新
        /// </summary>
        public static bool IsHttp { get; set; } = false;//jcjg中置为false，专门为ftp使用。http已分离出去。
        /// <summary>
        ///需要启动的子系统
        /// </summary>
        public static string StartUp_ZXT { get; set; }
        /// <summary>
        /// 传参方式需要下载的子系统
        /// </summary>
        public static string zxt { get; set; }

        /// <summary>
        /// 构造函数中需要进行初始化成员变量
        /// </summary>
        public HISGlobalSetting() : base()
        {
            FTPINFO = new FTPUpdateConfig();
            HTTPINFO = new HTTPUpdateConfig();
        }
        public static List<HTTPUpdateConfig> LoadHttpInfos()
        {
            List<HTTPUpdateConfig> https = new List<HTTPUpdateConfig>();
            if (!string.IsNullOrEmpty(zxt))
            {
                _instance = null;
                HTTPUpdateConfig hTTPUpdateConfig = Load(zxt.ToLower()).HTTPINFO;
                https.Add(hTTPUpdateConfig);
            }
            else
            {
                if (UpdateDirectorys.Count != 0 && UpdateDirectorys != null)
                {
                    foreach (var directory in UpdateDirectorys)
                    {
                        _instance = null;
                        HTTPUpdateConfig hTTPUpdateConfig = Load(directory.ToLower()).HTTPINFO;
                        https.Add(hTTPUpdateConfig);
                    }
                }
                if (GongYongDirectorys.Count != 0 && GongYongDirectorys != null)
                {
                    foreach (var directory in GongYongDirectorys)
                    {
                        _instance = null;
                        HTTPUpdateConfig hTTPUpdateConfig = LoadGongYong(directory).HTTPINFO;
                        https.Add(hTTPUpdateConfig);
                    }
                }
            }
            return https;
        }
        /// <summary>
        /// 加载HISGlobalSettingHttp.xml文件，子系统文件夹
        /// </summary>
        /// <param name="directories"></param>
        /// <returns></returns>
        public static HISGlobalSetting Load(string directories)
        {
            DirectoryInfo rootPathInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);//根目录

            //==========非基础架构子系统放在AssemblyClient下级文件夹===========
            //目标文件夹位置
            string destinationSubdir = rootPathInfo.FullName + "AssemblyClient\\" + directories + "\\HISGlobalSettingHttp.xml";
            if (!Directory.Exists(rootPathInfo.FullName + "AssemblyClient\\" + directories))
                Directory.CreateDirectory(rootPathInfo.FullName + "AssemblyClient\\" + directories);//创建
            if (!File.Exists(rootPathInfo.FullName + "AssemblyClient\\" + directories + "\\HISGlobalSettingHttp.xml"))
            {
                //从父级文件夹下拷贝 +AssemblyClient
                File.Copy(rootPathInfo.FullName + "HISGlobalSettingHttp.xml", destinationSubdir);
                //写入配置信息
                GlobalXmlHelper.ModifyAttribute(destinationSubdir, "BanBenHao", "1");
                //拼接服务器上得基线名称
                string jixianmc = directories.ToUpper();
                //更新基线名称
                GlobalXmlHelper.ModifyAttribute(destinationSubdir, "JIXIANMC", jixianmc);
                //更新本地路径
                GlobalXmlHelper.ModifyAttribute(destinationSubdir, "localConfigPath", rootPathInfo.FullName + "AssemblyClient\\" + directories + "\\");
            }

            _instance = Load<HISGlobalSetting>(destinationSubdir);
            _instance.HTTPINFO.localConfigPath = rootPathInfo.FullName + "AssemblyClient\\" + directories + "\\";

            return _instance;
        }
        /// <summary>
        /// 加载HISGlobalSettingHttp.xml文件,公用文件夹
        /// </summary>
        /// <param name="directories"></param>
        /// <returns></returns>
        public static HISGlobalSetting LoadGongYong(string directories)
        {
            DirectoryInfo rootPathInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);//根目录
            if (directories == "AssemblyClient")
            {
                string destinationSubdir = rootPathInfo.FullName + "AssemblyClient\\" + "HISGlobalSettingHttp.xml";
                if (!File.Exists(destinationSubdir))
                {
                    File.Copy(rootPathInfo.FullName + "HISGlobalSettingHttp.xml", rootPathInfo.FullName + "AssemblyClient" + "\\HISGlobalSettingHttp.xml");
                    //写入配置信息
                    GlobalXmlHelper.ModifyAttribute(destinationSubdir, "BanBenHao", "1");
                    //更新基线名称
                    GlobalXmlHelper.ModifyAttribute(destinationSubdir, "JIXIANMC", "AssemblyClient_JCJG_Dev");
                    //更新本地路径
                    GlobalXmlHelper.ModifyAttribute(destinationSubdir, "localConfigPath", rootPathInfo.FullName + "AssemblyClient\\");
                }
                _instance = Load<HISGlobalSetting>(destinationSubdir);
                _instance.HTTPINFO.localConfigPath = rootPathInfo.FullName + "AssemblyClient\\";
            }
            else
            {
                string destinationSubdir = rootPathInfo.FullName + "AssemblyClient\\" + directories + "\\HISGlobalSettingHttp.xml";
                if (!File.Exists(destinationSubdir))
                {
                    FileHelper.CreateDir(rootPathInfo + "AssemblyClient\\" + directories);//创建
                    if (!File.Exists(rootPathInfo.FullName + "AssemblyClient\\" + directories + "\\HISGlobalSettingHttp.xml"))
                    {
                        File.Copy(rootPathInfo.FullName + "HISGlobalSettingHttp.xml", rootPathInfo.FullName + "AssemblyClient\\" + directories + "\\HISGlobalSettingHttp.xml");
                        //写入配置信息
                        GlobalXmlHelper.ModifyAttribute(destinationSubdir, "BanBenHao", "1");
                        //拼接服务器上得基线名称
                        string jixianmc = directories;
                        //更新基线名称
                        GlobalXmlHelper.ModifyAttribute(destinationSubdir, "JIXIANMC", jixianmc);
                        //更新本地路径
                        GlobalXmlHelper.ModifyAttribute(destinationSubdir, "localConfigPath", rootPathInfo.FullName + "AssemblyClient\\" + directories + "\\");
                    }
                }
                _instance = Load<HISGlobalSetting>(destinationSubdir);
                _instance.HTTPINFO.localConfigPath = rootPathInfo.FullName + "AssemblyClient\\" + directories + "\\";
            }
            return _instance;
        }
        /// <summary>
        /// 加载默认的全局配置文件（HISGlobalSetting.xml）
        /// </summary>
        /// <returns></returns>
        public static HISGlobalSetting Load()
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "AssemblyClient"))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "AssemblyClient");
            if (_instance == null)
            {
                _instance = Load<HISGlobalSetting>(AppDomain.CurrentDomain.BaseDirectory + "AssemblyClient\\HISGlobalSetting.xml");
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
        /// <summary>
        /// 客户端更新FTP配置信息
        /// </summary>
        [XmlElement(ElementName = "客户端更新FTP配置信息")]
        public FTPUpdateConfig FTPINFO { get; set; }
        [XmlElement(ElementName = "客户端更新HTTP配置信息")]
        public HTTPUpdateConfig HTTPINFO { get; set; }
    }
    /// <summary>
    /// HTTP配置
    /// </summary>
    public class HTTPUpdateConfig
    {
        public HTTPUpdateConfig(string banbenhao, string jixianmc, string localPath, long size)
        {
            JIXIANMC = jixianmc;
            BanBenHao = banbenhao;
            localConfigPath = localPath;
            FileSize = size;
        }
        /// <summary>
        /// 文件大小
        /// </summary>
        public long FileSize { get; set; }

        public HTTPUpdateConfig() { }
        /// <summary>
        /// 基线名称
        /// </summary>
        public string JIXIANMC { get; set; }
        /// <summary>
        /// 下载版本号 对应服务器包ID
        /// </summary>
        public string BanBenHao { get; set; }
        /// <summary>
        /// HISGlobalSetting.xml配置文件位置
        /// </summary>
        public string localConfigPath { get; set; }
    }

    public class FTPUpdateConfig
    {
        /// <summary>
        /// ftp用户名
        /// </summary>
        public string FtpUser { get; set; }
        /// <summary>
        /// ftp密码
        /// </summary>
        public string FtpPwd { get; set; }
        /// <summary>
        /// ftp地址
        /// </summary>
        public string FtpIp { get; set; }
        /// <summary>
        /// 备用ftp地址
        /// </summary>
        public string FtpSpareIp { get; set; }
        /// <summary>
        /// 更新程序名称
        /// </summary>
        public string UpdateExeName { get; set; }
        /// <summary>
        /// 登录用户窗口
        /// </summary>
        public string LoginFormName { get; set; }
        /// <summary>
        /// ftp根文件夹
        /// </summary>
       // public string FtpRootDirectoryName { get; set; }
        /// <summary>
        /// ftp子文件夹
        /// </summary>
        public string FtpFirstSubDirectoryName { get; set; }
    }
}
