using Mediinfo.Enterprise.Config;
using Mediinfo.Infrastructure.Core.Cache;

using Oracle.ManagedDataAccess.Client;
using System;
using System.Web.Http;

namespace Mediinfo.Cloud.Service.DevHost.Starter
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
                            // 以应用ID和参数ID做联合主键
                            CacheManager.Cache.Update("GY_CANSHU:" + reader["YINGYONGID"] + ":" + reader["CANSHUID"].ToString(),
                                reader["CANSHUZHI"]);
                            Mediinfo.Enterprise.Cache.CacheManager.Cache.Update("GY_CANSHULOG:" + reader["YINGYONGID"] + ":" + reader["CANSHUID"].ToString(),
                                reader["CANSHUZHI"]);
                            //刷新参数缓存时修改同时刷新日志参数
                            Mediinfo.Enterprise.Log.LogHelper.InitialCanshu();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 记录日志
                Enterprise.Log.LogHelper.Intance.Error("初始化缓存", "初始化参数缓存发生错误！", ex.ToString());
            }
            finally
            {
                if (conn != null && conn.State != System.Data.ConnectionState.Closed)
                    conn.Close();
            }

            return "OK";
        }
    }
}
