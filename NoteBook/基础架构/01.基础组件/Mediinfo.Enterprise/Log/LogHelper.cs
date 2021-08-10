using Mediinfo.Enterprise.Cache;
using Mediinfo.Utility.Extensions;

using NLog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mediinfo.Enterprise.Log
{
    /// <summary>
    /// 日志信息分级别操作
    /// </summary>
    public class LogHelper
    {
        private Logger logger;

        private ESLog eSLog;

        private static LogHelper _LogHelper = new LogHelper();

        // 自定义日志级别 默认所有自定义日志都写
        private static int zdyRiZhi = 0;
        // 系统日志级别 默认系统日志都写
        private static int xtRiZhi = 0;

        //是否写入日志
        private static bool isWriteRiZhi = true;
        //
        private static string riZhiLeiXingKZ = "1";

        //日志级别参数
        //private static string riZhiJB = string.Empty;

        //日志ip段控制
        public static string riZHiIP = string.Empty;//ip为空则不限制

        //基线id
        public static string jiXianId = string.Empty;

        /// <summary>
        /// 客户端日志级别
        /// </summary>
        /// <param name="zdyRiZhiKz"></param>
        /// <param name="xtRiZhiKz"></param>
        /// <param name="localip"></param>
        public static void InitialRiZhiKZ(string zdyRiZhiKz, string xtRiZhiKz, string localip)
        {
            string[] zdys = zdyRiZhiKz.Split('|');
            string[] xts = xtRiZhiKz.Split('|');

            foreach (var zdyRzKz in zdys)
            {
                // 自定义日志
                int index1 = zdyRzKz.IndexOf("：");
                string[] zdyips = zdyRzKz.SubString(0, index1).Split('-');
                bool isZdyIp = CheckIpInRange(localip, zdyips[0], zdyips[1]);
                if (isZdyIp)
                {
                    zdyRiZhi = int.Parse(zdyRzKz.SubString(index1 + 1));
                }
            }

            foreach (var xtRzKz in xts)
            {
                // 系统日志
                int index2 = xtRzKz.IndexOf("：");
                string[] xtips = xtRzKz.SubString(0, index2).Split('-');
                bool isXtIp = CheckIpInRange(localip, xtips[0], xtips[1]);
                if (isXtIp)
                {
                    xtRiZhi = int.Parse(xtRzKz.SubString(index2 + 1));
                }
            }
        }


        /// <summary>
        /// 原有方法置空
        /// </summary>
        /// <param name="xtRiZhiKz"></param>
        public static void InitialRiZhiKZ(string xtRiZhiKz)
        {

        }

        /// <summary>
        /// 获取服务基线id并再次刷新参数
        /// </summary>
        /// <param name="jiXianID"></param>
        public static void InitialJiXian(string jiXianID)
        {
            jiXianId = jiXianID;
            InitialCanshu();
        }

        /// <summary>
        /// 服务端的自定义日志和系统日志控制(是否写入日志)
        /// </summary>
        /// <param name="xtRiZhiKz"></param>
        public static void InitialRiZhiKZ(string xtRiZhiKz, string jixianid)
        {
            try
            {
                //增加基线为空的判断
                if (string.IsNullOrWhiteSpace(jixianid))
                    isWriteRiZhi = true;
                else
                {
                    string newjixianid = jixianid.ToUpper();
                    if (!string.IsNullOrEmpty(xtRiZhiKz))
                    {
                        if (xtRiZhiKz.Contains("|"))//JCJG|BQYS|MZZJ
                        {
                            if (xtRiZhiKz.Contains(newjixianid))//包含基线则写日志
                            {
                                isWriteRiZhi = true;
                                //var spiltjiixan = xtRiZhiKz.Split('|');
                                //foreach (var jixian in spiltjiixan)
                                //{
                                //    if (jixian==newjixianid)
                                //    {
                                //isWriteRiZhi = true;
                                // }
                                //}
                            }
                            else
                            {
                                isWriteRiZhi = false;//参数中基线未配置则为不写日志
                            }
                        }
                        else//参数为1或者0，只有一个基线的情况
                        {
                            if (xtRiZhiKz == "1")
                                isWriteRiZhi = true;
                            else if (xtRiZhiKz == "0")
                                isWriteRiZhi = false;
                            else if (xtRiZhiKz == newjixianid)
                                isWriteRiZhi = true;
                            else
                                isWriteRiZhi = false;

                        }

                    }
                    else
                        isWriteRiZhi = true;//参数为空都写日志
                }
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// 服务端的自定义日志和系统日志级别(上下级)
        /// </summary>
        /// <param name="xtRiZhiJb"></param>
        public static void InitialRiZhiJB(string xtRiZhiJb)
        {
            //是否写入日志,用日志类型控制. 采用开关字符串 ,每一位代表一种日志类型. 1.菜单打开，2.客户端异常，3.服务调用，4服务端异常，5.SQL日志，6.性能日志 7.控件操作 8.自定义日志.范例 : 00000000
            //00000000


        }


        /// <summary>
        /// 服务端的自定义日志和系统日志ip限制
        /// </summary>
        /// <param name="xtRiZhiIP"></param>
        public static void InitialRiZhiIP(string xtRiZhiIP, string jixianid)
        {
            string newjixianid = jixianid.ToUpper();
            if (newjixianid == "QTYL")//其他医疗默认不限制ip
                return;
            if (!string.IsNullOrEmpty(xtRiZhiIP))
            {
                if (xtRiZhiIP.Contains("|"))
                {
                    if (xtRiZhiIP.Contains(newjixianid))//JCJG:10.10.1.1-10.10.1.126,192.168.0.1-192.168.0.128
                    {
                        var spiltjiixan = xtRiZhiIP.Split('|');
                        foreach (var jixian in spiltjiixan)
                        {
                            if (jixian.Contains(newjixianid))
                            {
                                var newjixian = jixian.Split(':');
                                riZHiIP = newjixian[1];
                                break;
                            }
                        }
                    }
                    else
                    {//没有配置基线级别则所有ip日志都写
                        riZHiIP = "";
                    }
                }
                else
                {
                    if (xtRiZhiIP.Contains(":"))//只有一个基线的情况
                    {
                        var spiltrizhi = xtRiZhiIP.Split(':');
                        if (spiltrizhi[0].Equals(newjixianid))
                        {
                            riZHiIP = spiltrizhi[1];
                        }
                    }
                    else
                    {
                        riZHiIP = "";
                    }
                }

            }
            else
            {
                //参数为空则所有日志都写
                riZHiIP = "";
            }
        }



        /// <summary>
        /// 判断IP是否在区间内
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="startIp"></param>
        /// <param name="endIp"></param>
        /// <returns></returns>
        public static bool CheckIpInRange(string ip, string startIp, string endIp)
        {
            long start = IP2Long(startIp);
            long end = IP2Long(endIp);
            long ipAddress = IP2Long(ip);
            bool inRange = (ipAddress >= start && ipAddress <= end);
            return inRange;
        }

        /// <summary>
        /// 判断ip是否在基线ip区间内
        /// </summary>
        /// <returns></returns>
        public static bool CheckJiXianIpInRange(string ip, string jixianip)
        {
            bool isInRange = false;
            //ip为本机ip，基线ip则是限制访问的ip段
            if (jixianip == "")
                isInRange = true;
            else
            {
                string[] xts = jixianip.Split(',');//192.168.0.1-192.168.0.128,192.168.1.1-192.168.1.128
                foreach (var xtRzKz in xts)
                {
                    // 系统日志
                    string[] xtips = xtRzKz.Split('-');
                    bool isXtIp = CheckIpInRange(ip, xtips[0], xtips[1]);
                    if (isXtIp)
                    {
                        isInRange = true;//ip在地址段中直接返回
                        break;
                    }
                }
            }
            return isInRange;
        }


        /// <summary>
        /// 把IP地址转成数字
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static long IP2Long(string ip)
        {
            try
            {
                if (ip == "::1")
                {
                    ip = "127.0.0.1";
                }

                string[] ipBytes;
                double num = 0;
                if (!string.IsNullOrEmpty(ip))
                {
                    ipBytes = ip.Split('.');
                    for (int i = ipBytes.Length - 1; i >= 0; i--)
                    {
                        num += ((int.Parse(ipBytes[i]) % 256) * Math.Pow(256, (3 - i)));
                    }
                }
                return (long)num;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("[" + ip + "]IP地址转换数字报错：" + ex.ToString());
            }
        }

        private LogHelper()
        {
            eSLog = new ESLog();
            logger = LogManager.GetCurrentClassLogger();

            InitialCanshu();
        }

        /// <summary>
        /// 刷新日志参数
        /// </summary>
        public static void InitialCanshu()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var iswrite = GetCanShu("公用_ES日志基线写入控制", "1");
                    InitialRiZhiKZ(iswrite, jiXianId);

                    var rizhijb = GetCanShu("公用_ES日志类型写入控制", "1");//默认值为1表示所有类型日志都写
                    riZhiLeiXingKZ = rizhijb;
                    //是否写入日志,用日志类型控制. 采用开关字符串 ,每一位代表一种日志类型. 1.菜单打开，2.客户端异常，3.服务调用，4服务端异常，5.SQL日志，6.性能日志 7.控件操作 8.自定义日志.范例 : 00000000
                    //00000000
                }
                catch (Exception ex)
                {
                }

            }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
        }

        public static string GetCanShu(string canShuId, string defaultValue)
        {
            // 默认应用为00为全局应用
            // 生成应用级参数缓存Key
            string cacheKey = string.Format("{0}:{1}:{2}", "GY_CANSHULOG", "00", canShuId);

            // 从缓存中查找值
            var cacheResult = CacheManager.Cache.Get(cacheKey);
            if (cacheResult != null)
            {
                return cacheResult.ToString();
            }

            // 没有找到参数时，返回空值
            return defaultValue;
        }

        /// <summary>
        /// 实现单例模式
        /// </summary>
        public static LogHelper Intance
        {
            get
            {
                return _LogHelper;
            }
        }

        /// <summary>
        /// 是否写入日志,用日志类型控制. 采用开关字符串 ,每一位代表一种日志类型. 1.菜单打开，2.客户端异常，3.服务调用，4服务端异常，5.SQL日志，6.性能日志 7.控件操作 8自定义日志.范例 : 00111100
        /// </summary>
        /// <param name="riZhiLX"></param>
        /// <returns></returns>
        public bool IsWriteRiZhiByLeiXing(int riZhiLX)
        {
            var canshu = riZhiLeiXingKZ;
            bool result = false;
            if (canshu == "1")
                return true;
            else if (canshu == "0")
                return false;
            else
            {
                switch (riZhiLX)
                {
                    case 1:
                        var zdyrz = canshu.Substring(0, 1);
                        if (zdyrz == "1")
                            result= true;
                        break;
                    case 2:
                        var zdyrz1 = canshu.Substring(1, 1);
                        if (zdyrz1 == "1")
                            result= true;
                        break;
                    case 3:
                        var zdyrz2 = canshu.Substring(2, 1);
                        if (zdyrz2 == "1")
                            result= true;
                        break;
                    case 4:
                        var zdyrz3 = canshu.Substring(3, 1);
                        if (zdyrz3 == "1")
                            result= true;
                        break;
                    case 5:
                        var zdyrz4 = canshu.Substring(4, 1);
                        if (zdyrz4 == "1")
                            result= true;
                        break;
                    case 6:
                        var zdyrz5 = canshu.Substring(5, 1);
                        if (zdyrz5 == "1")
                            result= true;
                        break;
                    case 7:
                        var zdyrz6 = canshu.Substring(6, 1);
                        if (zdyrz6 == "1")
                            result= true;
                        break;
                    case 8:
                        var zdyrz7 = canshu.Substring(7,1);
                        if (zdyrz7 == "1")
                            result =  true;
                        break;

                }
                return result;

            }
        }

        #region 自定义日志

        /// <summary>
        /// 提交日志信息
        /// </summary>
        /// <param name="suoYin">索引</param>
        /// <param name="riZhiBt">日志标题</param>
        /// <param name="riZhiNr">日志内容</param>
        /// <param name="id">ID</param>
        public void Info(string suoYin, string riZhiBt, string riZhiNr, string id = "")
        {
            if (!isWriteRiZhi) return;//是否写日志
            if (!IsWriteRiZhiByLeiXing(8))
                return;
            try
            {
                LogEntity logEntity = new LogEntity();
                logEntity.RiZhiID = Guid.NewGuid().ToString();
                logEntity.ChuangJianSj = DateTime.Now.ToInvariantString("yyyy/MM/dd HH:mm:ss");
                logEntity.SuoYin = suoYin;
                logEntity.RiZhiBt = riZhiBt;
                logEntity.RiZhiNr = riZhiNr.HtmlEntitiesEncode();
                logEntity.RiZhiJb = "INFO";
                eSLog.PutLog(logEntity, id);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }
        /// <summary>
        /// 提交警告日志信息
        /// </summary>
        /// <param name="suoYin">索引</param>
        /// <param name="riZhiBt">日志标题</param>
        /// <param name="riZhiNr">日志内容</param>
        public void Warn(string suoYin, string riZhiBt, string riZhiNr)
        {
            if (!isWriteRiZhi) return;//是否写日志

            if (!IsWriteRiZhiByLeiXing(8))
                return;
            try
            {
                LogEntity logEntity = new LogEntity();
                logEntity.RiZhiID = Guid.NewGuid().ToString();
                logEntity.ChuangJianSj = DateTime.Now.ToInvariantString("yyyy/MM/dd HH:mm:ss");
                logEntity.SuoYin = suoYin;
                logEntity.RiZhiBt = riZhiBt;
                logEntity.RiZhiNr = riZhiNr;
                logEntity.RiZhiJb = "WARN";
                eSLog.PutLog(logEntity);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        /// <summary>
        /// 提交错误日志信息
        /// </summary>
        /// <param name="suoYin">索引</param>
        /// <param name="riZhiBt">日志标题</param>
        /// <param name="riZhiNr">日志内容</param>
        public void Error(string suoYin, string riZhiBt, string riZhiNr)
        {
            if (!isWriteRiZhi) return;//是否写日志
            if (!IsWriteRiZhiByLeiXing(8))
                return;
            try
            {
                LogEntity logEntity = new LogEntity();
                logEntity.RiZhiID = Guid.NewGuid().ToString();
                logEntity.ChuangJianSj = DateTime.Now.ToInvariantString("yyyy/MM/dd HH:mm:ss");
                logEntity.SuoYin = suoYin;
                logEntity.RiZhiBt = riZhiBt;
                logEntity.RiZhiNr = riZhiNr;
                logEntity.RiZhiJb = "ERROR";
                eSLog.PutLog(logEntity);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        #endregion

        #region 系统日志
        /// <summary>
        /// 写入系统标准日志
        /// </summary>
        public void PutSysErrorLog(SysLogEntity logEntity)
        {
            if (!isWriteRiZhi) return;//公用_ES日志基线写入控制
            //根据日志类型判断
            if (!IsWriteRiZhiByLeiXing(logEntity.RiZhiLx))
                return;
            try
            {
                eSLog.PutLog(logEntity);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        public void PutSysInfoLog(SysLogEntity logEntity)
        {
            if (!isWriteRiZhi) return;//是否写日志
            //根据日志类型判断
            if (!IsWriteRiZhiByLeiXing(logEntity.RiZhiLx))
                return;

            try
            {
                eSLog.PutLog(logEntity);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        #endregion

    }
}