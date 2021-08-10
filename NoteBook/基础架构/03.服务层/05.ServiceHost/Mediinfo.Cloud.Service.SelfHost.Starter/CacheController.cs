using Mediinfo.Enterprise.Config;
using Mediinfo.Enterprise.Log;
using Mediinfo.Infrastructure.Core.Cache;

using Oracle.ManagedDataAccess.Client;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web.Http;

namespace Mediinfo.Cloud.Service.SelfHost.Starter
{
    public class CacheController : ApiController
    {
        /// <summary>
        /// 缓存刷新接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("RefreshCanShuCache")]
        public string RefreshCanShuCache()
        {
            // 初始化连接
            OracleConnection conn = null;
            StringBuilder cacheStr = new StringBuilder();
            try
            {
                // 建立HIS6数据库连接
                conn = new OracleConnection(MediinfoConfig.GetValue("DbConfig.xml", "HIS6HC"));
                conn.Open();

                // 查询所有
                using (OracleCommand cmd = new OracleCommand("SELECT YINGYONGID,CANSHUID,CANSHUZHI FROM GY_CANSHU", conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    using (var reader = cmd.ExecuteReader())
                    {
                        // 读取所有记录
                        while (reader.Read())
                        {
                            string cacheKey = "GY_CANSHU:" + reader["YINGYONGID"] + ":" + reader["CANSHUID"].ToString();
                            object cacheVal = reader["CANSHUZHI"];
                            // 以应用ID和参数ID做联合主键
                            CacheManager.Cache.Update(cacheKey, cacheVal);
                            Mediinfo.Enterprise.Cache.CacheManager.Cache.Update("GY_CANSHULOG:" + reader["YINGYONGID"] + ":" + reader["CANSHUID"].ToString(),
                                cacheVal);
                            //刷新参数缓存时修改同时刷新日志参数
                            Mediinfo.Enterprise.Log.LogHelper.InitialCanshu();
                            cacheStr.AppendLine("缓存Key值: [" + cacheKey + "], 缓存Value值: [" + cacheVal + "]");
                        }
                    }

                    string ipAddress = "127.0.0.1";
                    // 获得当前IP地址
                    string name = Dns.GetHostName();
                    IPAddress[] ipadrlist = Dns.GetHostAddresses(name);
                    foreach (IPAddress ipa in ipadrlist)
                    {
                        if (ipa.AddressFamily == AddressFamily.InterNetwork)
                            ipAddress = ipa.ToString();
                    }
                    // 获取基线ID
                    string jixianid = MediinfoConfig.GetValue("ServiceManifest.xml", "JiXianID");

                    LogHelper.Intance.Info("初始化缓存", "服务器IP: " + ipAddress + ", 基线ID: " + jixianid, cacheStr.ToString());
                }
            }
            catch (Exception ex)
            {
                // 记录日志
                LogHelper.Intance.Error("初始化缓存", "初始化参数缓存发生错误！", ex.ToString());
            }
            finally
            {
                if (conn != null && conn.State != System.Data.ConnectionState.Closed)
                    conn.Close();
            }

            return "OK";
        }

        /// <summary>
        /// 获取日志参数
        /// </summary>
        /// <returns></returns>
        public static string GetRiZhiCanShu()
        {
            string canshuid = string.Empty;
            // 初始化连接
            OracleConnection conn = null;
            try
            {
                // 建立HIS6数据库连接
                conn = new OracleConnection(MediinfoConfig.GetValue("DbConfig.xml", "HIS6HC"));
                conn.Open();

                // 服务端系统日志和自定义日志统一控制
                using (OracleCommand cmd = new OracleCommand("SELECT t.CANSHUZHI FROM GY_CANSHU t WHERE CANSHUID='日志_IP段_服务端'", conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            canshuid += reader["CANSHUZHI"];
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                // 记录日志
                Enterprise.Log.LogHelper.Intance.Error("日志级别控制", "读取日志级别参数发生错误！", ex.ToString());
            }
            finally
            {
                if (conn != null && conn.State != System.Data.ConnectionState.Closed)
                    conn.Close();
            }
            return canshuid;
        }
    }
}
