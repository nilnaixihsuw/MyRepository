using Mediinfo.Enterprise;
using Mediinfo.Enterprise.Config;
using Mediinfo.HIS.Core;
using Mediinfo.Utility;
using Mediinfo.Utility.Compress;
using Mediinfo.Utility.Extensions;
using Mediinfo.Utility.Util;
using Newtonsoft.Json;

using Polly;

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mediinfo.ServiceProxy.Core
{
    /// <summary>
    /// 服务客户端
    /// </summary>
    public class ServiceClient
    {
        /// <summary>
        /// 服务上下文信息
        /// </summary>
        private ServiceContext serviceContext { get; set; }

        /// <summary>
        /// 服务地址
        /// </summary>
        //private string serviceUrl = string.Empty;

        /// <summary>
        /// 服务名称
        /// </summary>
        private string serviceName = string.Empty;

        /// <summary>
        /// 服务版本
        /// </summary>
        private string clientVersion = string.Empty;

        /// <summary>
        /// API网关地址
        /// </summary>
        private string RunningMode { get { return MediinfoConfig.GetValue("WinFormMain.xml", "RunningMode"); } }

        /// <summary>
        /// 服务器地址
        /// </summary>
        private static List<string> ServerUrls = MediinfoConfig.GetValue("WinFormMain.xml", "ServerUrl").Split(',').ToList();

        /// <summary>
        /// 总请求次数
        /// </summary>
        private static ulong reqTimes = 0;

        /// <summary>
        /// 是否启动客户端网关缓存, 开启之后客户端请求将不请求网关直接请求服务
        /// </summary>
        private static string isAllowPermanentCache = string.Empty;

        /// <summary>
        /// 客户端网关缓存刷新时间，如果刷新时间为0则代表取永久缓存
        /// </summary>
        private static double apiCacheRefreshTime = -1;

        /// <summary>
        /// 待请求的服务地址
        /// </summary>
        public static string ServerUrl { get { return ServerUrls[(reqTimes++ % (ulong)ServerUrls.Count).ToInt()]; } }

        /// <summary>
        /// 请求客户端
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="clientVersion"></param>
        public ServiceClient(string serviceName, string clientVersion)
        {
            this.serviceName = serviceName;
            this.clientVersion = clientVersion;
        }

        /// <summary>
        /// 请求客户端
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        public ServiceClient(string address, int port)
        {
            //serviceUrl = "http://"+ address + ":" + port;
        }

        private string GetServiceByStandalone()
        {
            return "http://" + ServerUrl + "/" + serviceName;
        }

        static DateTime lastTimeout;    // 上次网关超时的时间

        /// <summary>
        /// //网关黑名单
        /// </summary>
        static Dictionary<string, DateTime> BlackListDic = new Dictionary<string, DateTime>();

        // 服务缓存上次刷新的时间
        static DateTime lastReflash = DateTime.Now;

        // 服务缓存列表
        private static Dictionary<string, string> serviceCacheList = null;

        /// <summary>
        /// 加载服务信息到本地缓存
        /// </summary>
        private async Task<Dictionary<string, string>> GetLoadGateway()
        {
            // 设置最后的缓存刷新时间
            lastReflash = DateTime.Now;

            try
            {
                // 从网关获取当前最新的服务注册信息
                string gatewayJson =
                                     await RestfulClient.GetAsync("http://" + ServiceClient.ServerUrl + "/GatewayServices", 3000);
                // 替换之前的缓存对象
                serviceCacheList =
                    JsonUtil.DeserializeToObject<Dictionary<string, string>>(gatewayJson);
            }
            catch (Exception ex)
            {
                // 设置服务缓存为空，直接查询网关
                serviceCacheList = null;

                // 设置3分钟之后重试
                lastReflash = DateTime.Now.AddMinutes(3);
                Enterprise.Log.LogHelper.Intance.Error("api网关", "客户端网关服务缓存刷新失败", ex.ToString());
            }

            // 返回缓存信息
            return serviceCacheList;
        }


        /// <summary>
        /// 根据网关获取服务地址
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="clientVersion"></param>
        /// <returns></returns>
        private string GetServiceByAPIGateway(string serviceName, string clientVersion)
        {
            if (String.IsNullOrWhiteSpace(isAllowPermanentCache))
                isAllowPermanentCache = GetIsAllowPermanentCache();

            // 检查本地配置文件中是否启动客户端网关缓存
            if ("Y".Equals(isAllowPermanentCache))
            {
                // 先从临时缓存中取服务
                string serviceUrl = CacheHelper.Cache.GetCache<string>(serviceName);
                if (!String.IsNullOrEmpty(serviceUrl))
                    return serviceUrl;
            }

            //客户端ip
            string clientAddress = string.Empty;
            if (!string.IsNullOrEmpty(HISClientHelper.IP))
            {
                clientAddress = $"客户端IP[{HISClientHelper.IP}],";
            }

            try
            {
                string url = "";//当前请求网关
                var currentTime = HISClientHelper.GetSysDate();
                if ((currentTime - lastTimeout).TotalHours < 1)
                {
                    throw new Exception("距离上次API网关请求超时不足一小时，直接启用本地缓存");
                }
                //黑名单检测
                foreach (var item in BlackListDic.ToList())
                {
                    //如果网关失效超过10分钟，则进行检测(可能问题已处理，网关恢复正常)
                    if ((currentTime - item.Value).TotalMinutes >= 10)
                    {
                        //检查网关能否访问
                        CheckUrlVisit(item.Key);
                    }
                }

                // 设置API网关重试机制
                var policy = Policy
                      .Handle<Exception>()
                      .WaitAndRetry(new[]
                      {
                        TimeSpan.FromSeconds(1),//update by xuyi 2021-7-21 修改网关重试机制，改为重试两次,一次一秒
                        TimeSpan.FromSeconds(1),
                        //TimeSpan.FromSeconds(1.5),
                        //TimeSpan.FromSeconds(3),
                        //TimeSpan.FromSeconds(3),
                        //TimeSpan.FromSeconds(3),
                      }, (ex, time) =>
                      {
                          //检查网关能否访问
                          CheckUrlVisit(url);
                          Enterprise.Log.LogHelper.Intance.Warn("api网关", $"{clientAddress}API网关【{url}】请求超时，正在重试...",
                             $"API网关【{url}】请求超时，正在重试...失败原因：" + ex.ToString() + ",请求持续了(" + time.Seconds + ")秒发生错误或无响应后，进行了重试");
                      });

                // 执行API网关请求
                return policy.Execute<string>(() =>
                {
                    url = ServerUrl;
                    string addr = RestfulClient.Get("http://" + url + "/LocalGateway/" + serviceName.ToString() + "/" + clientVersion);
                    if (string.IsNullOrEmpty(addr))
                    {
                        throw new ApplicationException("没有找到【" + serviceName + "】服务，版本号：" + clientVersion);
                    }

                    if ("Y".Equals(isAllowPermanentCache))
                    {
                        if (apiCacheRefreshTime == -1)
                        {
                            // 获取临时缓存时间
                            if (ConfigurationManager.AppSettings["APICacheRefreshTime"] != null)
                                apiCacheRefreshTime = Convert.ToDouble(ConfigurationManager.AppSettings["APICacheRefreshTime"]);
                        }

                        // 添加临时缓存
                        if (apiCacheRefreshTime != 0)
                            CacheHelper.Cache.SetCache(serviceName, "http://" + addr, apiCacheRefreshTime * 60);

                        // 添加永久缓存
                        PermanenceServiceCache.Instance.AddOrUpdateCache(serviceName, "http://" + addr);
                    }

                    return "http://" + addr;
                });

            }
            catch (Exception ex)
            {
                // 距离上次API网关请求超时超过一小时，则重置上次网关超时的时间
                if ((HISClientHelper.GetSysDate() - lastTimeout).TotalHours >= 1)
                    lastTimeout = HISClientHelper.GetSysDate();

                Enterprise.Log.LogHelper.Intance.Error("api网关", $"{clientAddress}已经达到最大重试次数，API网关无法响应请求，正在启用本地缓存！", ex.ToString());
                string addr = PermanenceServiceCache.Instance.GetFromCache<string>(serviceName);
                if (string.IsNullOrEmpty(addr))
                {
                    Enterprise.Log.LogHelper.Intance.Error("api网关", $"{clientAddress}API网关在最大重试次数内无法响应，服务缓存中也没有找到【" + serviceName + "】服务，版本号：" + clientVersion,
                        "API网关在最大重试次数内无法响应，服务缓存中也没有找到【" + serviceName + "】服务，版本号：" + clientVersion);
                    throw new ApplicationException("没有找到【" + serviceName + "】服务，版本号：" + clientVersion);
                }
                return "http://" + addr;
            }
        }

        /// <summary>
        /// 执行调用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller"></param>
        /// <param name="method"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public Result<T> Invoke<T>(string controller, string method, params ServiceParm[] parms)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    if (HISClientHelper.MainForm?.Cursor != null && HISClientHelper.MainForm?.Cursor != Cursors.WaitCursor)
                        HISClientHelper.MainForm.Cursor = Cursors.WaitCursor;
                }
                catch { }
            });

            try
            {
                string serviceUrl;
                switch (RunningMode)
                {
                    case "Standalone":
                        serviceUrl = GetServiceByStandalone();
                        break;
                    case "Cluster":
                        serviceUrl = GetServiceByAPIGateway(serviceName, clientVersion);
                        break;
                    default:
                        serviceUrl = GetServiceByStandalone();
                        break;
                }

                using (var client = new WebClientEx(3600000))
                {
                    string url = serviceUrl + "/" + controller + "/" + method;
                    var nameContent = new NameValueCollection();
                    foreach (var item in parms)
                    {
                        nameContent.Add(item.ParmName, item.ParmValue.ToCompress());
                    }

                    // 创建JWT
                    string token = TokenLocator.Instance.GetToken();
                    client.Headers.Add("token", token);

                    // 创建服务上下文
                    ServiceContext context = ContextLocator.Instance.GetServiceContext();
                    client.Headers.Add("context", context.ToCompress());

                    //client.Headers.Add("Content-Encoding", "gzip");
                    //client.Headers.Add("Accept-Encoding", "gzip");

                    byte[] response = null;
                    try
                    {
                        response = client.UploadValues(url, nameContent);
                    }
                    catch (Exception ex)
                    {
                        return new Result<T>(ReturnCode.ERROR, ex.Message, ex.ToString(), default(T));
                    }

                    if (client.IsError)
                    {
                        return new Result<T>(ReturnCode.ERROR, client.ErrorMessage.Message, client.ErrorMessage.ExceptionMessage, default(T));
                    }

                    string resStr = Encoding.UTF8.GetString(response);

                    ServiceResult serviceResult = JsonConvert.DeserializeObject<ServiceResult>(resStr);

                    var returnCode = serviceResult.ReturnCode.Decompress<ReturnCode>();

                    T contentResult = default(T);
                    if (returnCode == ReturnCode.SUCCESS)
                    {
                        contentResult = serviceResult.Content.Decompress<T>();
                    }

                    return new Result<T>(returnCode, serviceResult.ReturnMessage.Decompress<string>(), serviceResult.ExceptionContent.Decompress<string>(), contentResult);
                }
            }
            catch (Exception ex)
            {
                return new Result<T>(ReturnCode.ERROR, ex.Message, ex.ToString(), default(T));
            }
            finally
            {
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        //报表查询结束后设置为默认光标时卡住约30秒(报表异步查询等待时间为30秒)，vs启动程序无此现象
                        //故将光标设置改为异步操作
                        if (HISClientHelper.MainForm?.Cursor != null && HISClientHelper.MainForm?.Cursor != Cursors.Default)
                            HISClientHelper.MainForm.Cursor = Cursors.Default;
                    }
                    catch { }
                });
            }
        }

        /// <summary>
        /// 执行调用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller"></param>
        /// <param name="method"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public Result<T> Invoke<T>(string controller, string method, Dictionary<string, string> parms)
        {
            try
            {
                if (HISClientHelper.MainForm.Cursor != null)
                    HISClientHelper.MainForm.Cursor = Cursors.WaitCursor;
            }
            catch { }

            try
            {
                string serviceUrl;
                switch (RunningMode)
                {
                    case "Standalone":
                        serviceUrl = GetServiceByStandalone();
                        break;
                    case "Cluster":
                        serviceUrl = GetServiceByAPIGateway(serviceName, clientVersion);
                        break;
                    default:
                        serviceUrl = GetServiceByStandalone();
                        break;
                }

                using (var client = new WebClientEx(36000000))
                {
                    string url = serviceUrl + "/" + controller + "/" + method;
                    var nameContent = new NameValueCollection();
                    foreach (var item in parms)
                    {
                        nameContent.Add(item.Key, item.Value.CompressString());
                    }

                    // 创建JWT
                    string token = TokenLocator.Instance.GetToken();
                    client.Headers.Add("token", token);

                    // 创建服务上下文
                    ServiceContext context = ContextLocator.Instance.GetServiceContext();
                    client.Headers.Add("context", context.ToCompress());

                    //client.Headers.Add("Content-Encoding", "gzip");
                    //client.Headers.Add("Accept-Encoding", "gzip");

                    byte[] response = null;
                    try
                    {
                        response = client.UploadValues(url, nameContent);
                    }
                    catch (Exception ex)
                    {
                        return new Result<T>(ReturnCode.ERROR, ex.Message, ex.ToString(), default(T));
                    }

                    if (client.IsError)
                    {
                        return new Result<T>(ReturnCode.ERROR, client.ErrorMessage.Message, client.ErrorMessage.ExceptionMessage, default(T));
                    }

                    string resStr = Encoding.UTF8.GetString(response);

                    ServiceResult serviceResult = JsonConvert.DeserializeObject<ServiceResult>(resStr);

                    var returnCode = serviceResult.ReturnCode.Decompress<ReturnCode>();

                    T contentResult = default(T);
                    if (returnCode == ReturnCode.SUCCESS)
                    {
                        contentResult = serviceResult.Content.Decompress<T>();
                    }

                    return new Result<T>(returnCode, serviceResult.ReturnMessage.Decompress<string>(), serviceResult.ExceptionContent.Decompress<string>(), contentResult);
                }
            }
            catch (Exception ex)
            {
                return new Result<T>(ReturnCode.ERROR, ex.Message, ex.ToString(), default(T));
            }
            finally
            {
                try
                {
                    if (HISClientHelper.MainForm.Cursor != null)
                        HISClientHelper.MainForm.Cursor = Cursors.Default;
                }
                catch { }
            }
        }

        /// <summary>
        /// 执行异步调用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller"></param>
        /// <param name="method"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public async Task<Result<T>> InvokeAsync<T>(string controller, string method, params ServiceParm[] parms)
        {
            try
            {
                if (HISClientHelper.MainForm.Cursor != null)
                    HISClientHelper.MainForm.Cursor = Cursors.WaitCursor;
            }
            catch { }

            try
            {
                string serviceUrl;
                switch (RunningMode)
                {
                    case "Standalone":
                        serviceUrl = GetServiceByStandalone();
                        break;
                    case "Cluster":
                        serviceUrl = GetServiceByAPIGateway(serviceName, clientVersion);
                        break;
                    default:
                        serviceUrl = GetServiceByStandalone();
                        break;
                }

                using (var client = new WebClientEx(3600000))
                {
                    var tcs = new TaskCompletionSource<Result<T>>();

                    var nameContent = new NameValueCollection();
                    if (parms != null)
                    {
                        foreach (var item in parms)
                        {
                            nameContent.Add(item.ParmName, item.ParmValue.ToCompress());
                        }
                    }

                    string token = TokenLocator.Instance.GetToken();
                    client.Headers.Add("token", token);

                    // 创建服务上下文
                    ServiceContext context = ContextLocator.Instance.GetServiceContext();
                    client.Headers.Add("context", context.ToCompress());

                    Uri address = new Uri(serviceUrl + "/" + controller + "/" + method);

                    UploadValuesCompletedEventHandler handler = null;
                    handler = (_, e) =>
                    {
                        client.UploadValuesCompleted -= handler;
                        if (e.Cancelled)
                            tcs.TrySetCanceled();
                        else if (e.Error != null)
                            tcs.TrySetException(e.Error);
                        else
                        {

                            if (client.IsError)
                            {
                                tcs.TrySetResult(new Result<T>(ReturnCode.ERROR, client.ErrorMessage.Message, client.ErrorMessage.ExceptionMessage, default(T)));
                            }
                            else
                            {
                                string resStr = Encoding.UTF8.GetString(e.Result);
                                ServiceResult serviceResult = JsonConvert.DeserializeObject<ServiceResult>(resStr);
                                var resultT = new Result<T>(serviceResult.ReturnCode.Decompress<ReturnCode>(), serviceResult.ReturnMessage.Decompress<string>(), serviceResult.ExceptionContent.Decompress<string>(), serviceResult.Content.Decompress<T>());
                                tcs.TrySetResult(resultT);
                            }
                        }
                    };

                    client.UploadValuesCompleted += handler;
                    try
                    {
                        client.UploadValuesAsync(address, nameContent);
                    }
                    catch (Exception ex)
                    {

                        return new Result<T>(ReturnCode.ERROR, ex.Message, ex.ToString(), default(T));
                    }

                    return await tcs.Task;
                }
            }
            catch (Exception ex)
            {
                return new Result<T>(ReturnCode.ERROR, ex.Message, ex.ToString(), default(T));
            }
            finally
            {
                try
                {
                    if (HISClientHelper.MainForm.Cursor != null)
                        HISClientHelper.MainForm.Cursor = Cursors.Default;
                }
                catch { }
            }
        }

        /// <summary>
        /// 检查网关能否访问
        /// </summary>
        /// <param name="url">网关地址</param>
        private void CheckUrlVisit(string url)
        {
            //客户端ip
            string clientAddress = string.Empty;
            if (!string.IsNullOrEmpty(HISClientHelper.IP))
            {
                clientAddress = $"客户端IP[{HISClientHelper.IP}],";
            }
            bool success = false;//网关是否访问成功
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + url);
                request.Method = "GET";
                request.ServicePoint.Expect100Continue = false;
                request.ServicePoint.UseNagleAlgorithm = false;
                request.ServicePoint.ConnectionLimit = 65500;
                request.AllowWriteStreamBuffering = false;
                request.Proxy = null;
                request.Timeout = 3000;//超过3秒无响应默认网关无法访问
                using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
                {
                    if (resp.StatusCode == HttpStatusCode.OK)
                    {
                        success = true;
                    }
                    request.Abort();
                    request = null;
                }
            }
            catch (WebException ex)
            {
                success = false;
            }

            if (success)
            {
                //网关访问成功

                //移出黑名单
                BlackListDic.Remove(url);
                //加入服务器列表
                if (!ServerUrls.Contains(url))
                    ServerUrls.Add(url);
            }
            else
            {
                //网关访问失败

                //移出服务器列表
                ServerUrls.Remove(url);

                //加入黑名单
                var failedTime = HISClientHelper.GetSysDate();//失败时间
                if (!BlackListDic.ContainsKey(url))
                    BlackListDic.Add(url, failedTime);
                else
                    BlackListDic[url] = failedTime;
            }

            //如果所有网关都无法访问,则重置网关服务器地址，并清空黑名单
            if (ServerUrls.Count == 0)
            {
                string hmd = "";
                foreach (var item in BlackListDic)
                {
                    hmd += (item.Key + ";");
                }
                ServerUrls = MediinfoConfig.GetValue("WinFormMain.xml", "ServerUrl").Split(',').ToList();
                BlackListDic.Clear();
                Enterprise.Log.LogHelper.Intance.Error("api网关", $"{clientAddress}所有API网关都无法访问", $"所有API网关[{hmd}]请求超时或无响应，请立即检查集群网关！！！");
            }

            //记录网关黑名单具体地址
            if (BlackListDic.Count > 0)
            {
                string hmd = "";
                foreach (var item in BlackListDic)
                {
                    hmd += (item.Key + ";");
                }
                Enterprise.Log.LogHelper.Intance.Error("api网关", $"{clientAddress}网关黑名单", $"网关黑名单：{hmd},请立即处理！");
            }
        }

        /// <summary>
        /// 获取本地配置文件中是否启动网关缓存
        /// </summary>
        /// <returns>返回 Y: 启动; N: 不启动</returns>
        private string GetIsAllowPermanentCache()
        {
            if (ConfigurationManager.AppSettings["IsAllowPermanentCache"] != null)
            {
                if (ConfigurationManager.AppSettings["IsAllowPermanentCache"].Equals("Y"))
                {
                    return "Y";
                }
            }
            return "N";
        }
    }
}
