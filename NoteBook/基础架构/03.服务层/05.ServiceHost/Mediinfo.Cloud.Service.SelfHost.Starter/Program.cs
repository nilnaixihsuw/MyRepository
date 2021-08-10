using Mediinfo.APIGateway.Core.Services;
using Mediinfo.Cloud.Core.Consul;
using Mediinfo.Cloud.Core.Models;
using Mediinfo.Cloud.Core.Repository;
using Mediinfo.Cloud.Service.SelfHost.Starter.Configuration;
using Mediinfo.Enterprise.Config;
using Mediinfo.Enterprise.Log;
using Mediinfo.Infrastructure.JCJG;
using Mediinfo.ServiceProxy.Core;
using Mediinfo.Utility.Util;

using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Mediinfo.Cloud.Service.SelfHost.Starter
{
    class Program
    {
        /// <summary>
        /// 拦截窗口关闭事件
        /// </summary>
        /// <param name="CtrlType"></param>
        /// <returns></returns>
        public delegate bool ControlCtrlDelegate(int CtrlType);
        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(ControlCtrlDelegate HandlerRoutine, bool Add);
        private static ControlCtrlDelegate cancelHandler = new ControlCtrlDelegate(HandlerRoutine);

        /// <summary>
        /// 处理窗口关闭事件
        /// </summary>
        /// <param name="CtrlType"></param>
        /// <returns></returns>
        public static bool HandlerRoutine(int CtrlType)
        {
            switch (CtrlType)
            {
                case 0:
                case 2:
                    Console.WriteLine("");
                    Console.WriteLine("服务被强制关闭，正在注销服务...");
                    // 取消注册服务
                    using (ClusterClient clusterClient = new ClusterClient())
                    {
                        var task = clusterClient.Deregister(port);
                        Task.WaitAll(task);
                    }
                    Console.WriteLine("服务注销成功！");
                    Console.WriteLine("正在关闭中...");
                    break;
            }
            Console.ReadLine();
            return false;
        }

        /// <summary>
        /// 当前处理的请求数
        /// </summary>
        public static long CurrentRequestTimes { get; set; } = 0;
        private static IDisposable server = null;
        //获取命令行参数
        private static Options options = new Options();
        private static int port = 0;

        //注意！在UI事件上要加上async关键字
        static async Task MainAsync(string[] args)
        {
            try
            {

                // 设置窗口关闭事件
                SetConsoleCtrlHandler(cancelHandler, true);

                // 处理命令行参数
                CommandLine.Parser.Default.ParseArguments(args, options);

                // 获取端口号
                port = ApiPort.GetServicePort();
                if (args.Length > 0)
                {
                    port = options.DuanKouHao;
                }

                // 加载服务dll
                var assemblyPath = AppDomain.CurrentDomain.BaseDirectory;

                // 扫描服务dll
                DirectoryInfo TheFolder = new DirectoryInfo(assemblyPath);
                var mediDlls = TheFolder.GetFiles("Mediinfo.Service.*.dll", SearchOption.AllDirectories).ToList();

                // 初始化仓储
                CloudRepository cloudRepository = new CloudRepository();

                List<string> serviceNameList = new List<string>();

                // 遍历文件
                for (int i = 0; i < mediDlls.Count; i++)
                {
                    // 获取dll路径
                    string dllPath = mediDlls[i].FullName;
                    var assembly = Assembly.LoadFrom(dllPath);

                    // 反射DLL
                    ServiceInfo serviceInfo = new ServiceInfo(assembly);

                    string serviceName = serviceInfo.GetServiceName();
                    // 输出信息
                    Console.WriteLine("服务名称：" + serviceName);
                    Console.WriteLine("版本号：" + serviceInfo.GetServiceVersion());
                    serviceNameList.Add(serviceName);
                    Console.WriteLine("-------------------------------------------------------------");
                }

                // 配置服务允许访问的ip地址和端口号
                StartOptions startOptions = new StartOptions();
                startOptions.Port = port;
                startOptions.Urls.Add("http://127.0.0.1:" + port);
                startOptions.Urls.Add("http://localhost:" + port);

                // 获取本机所有IP并注册
                string name = System.Net.Dns.GetHostName();
                System.Net.IPAddress[] ipadrlist = System.Net.Dns.GetHostAddresses(name);
                foreach (System.Net.IPAddress ipa in ipadrlist)
                {
                    if (ipa.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        startOptions.Urls.Add("http://" + ipa.ToString() + ":" + port);
                }

                try
                {
                    // 启动服务
                    server = WebApp.Start<Startup>(startOptions);

                    Console.WriteLine("服务启动成功！端口号：" + port);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("服务启动发生错误：" + JsonUtil.SerializeObject(ex));
                    Console.WriteLine("-------------------------------------------------------------");
                    Enterprise.Log.LogHelper.Intance.Error("服务部署", "服务启动失败", "服务启动失败：" + ex.ToString() + JsonUtil.SerializeObject(ex));
                    throw ex;
                }

                // 执行一次健康检查接口，以便加载缓存
                HealthController healthController = new HealthController();
                healthController.Index();

                Console.WriteLine("正在初始化缓存...");

                // 初始化缓存控制器
                CacheController cacheController = new CacheController();

                // 初始化参数缓存
                Console.WriteLine("正在初始化参数缓存...");
                cacheController.RefreshCanShuCache();
                Console.WriteLine("参数缓存初始化完成。");

                Console.WriteLine("缓存初始化完成。");

                Console.WriteLine("正在向consul集群注册服务...");
                using (ClusterClient clusterClient = new ClusterClient())
                {
                    foreach (var serviceName in serviceNameList)
                    {
                        try
                        {
                            string serviceId = serviceName + "@" + options.BaoId + "@" + Guid.NewGuid();
                            // 注册服务
                            await clusterClient.ReRegister(serviceId, serviceName, "V1", options.DuanKouHao);
                            
                            // 如果命令行参数大于0，代表是Agent自动启动的服务
                            if (args.Length > 0)
                            {
                                // 向数据库写入服务信息
                                CLOUD_FUWU fuwu = new CLOUD_FUWU();
                                fuwu.FUWUID = serviceId;
                                fuwu.FUWUMC = serviceName;
                                fuwu.BAOID = options.BaoId;
                                fuwu.JIEDIANID = options.JieDianId;
                                fuwu.ZHUANGTAI = options.ZhuangTai;
                                fuwu.JIEDIANMC = options.JieDianMc;
                                fuwu.JIEDIANIP = options.JieDianIp;
                                fuwu.ZHUCESJ = DateTime.Now;
                                fuwu.DUANKOU = port;
                                cloudRepository.AddOrUpdateFuWu(fuwu);
                            }

                            Console.WriteLine("注册" + serviceName + "服务成功。");

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("访问服务注册中心失败！注意：开发模式可以忽略该错误提示。" + ex.ToString());
                            Console.WriteLine("-------------------------------------------------------------");
                            Console.WriteLine();
                            throw ex;
                        }
                    }
                }

                Console.WriteLine("注册服务已完成。");

                Console.WriteLine("读取日志控制参数。");
                // 服务端日志级别控制
                Task.Factory.StartNew(() =>
                {
                    var CLOUD_DAIMABAO = cloudRepository.GetDaiMaBao(options.BaoId);//获取服务包
                    string jixianid = CLOUD_DAIMABAO.JIXIANID ;//获取基线id
                    if(string.IsNullOrWhiteSpace(jixianid))
                        jixianid= MediinfoConfig.GetValue("ServiceManifest.xml", "JiXianID")??"JCJG";
                    Mediinfo.Enterprise.Log.LogHelper.InitialJiXian(jixianid);

                    string RiZhiKz = CacheController.GetRiZhiCanShu();

                    if (!string.IsNullOrEmpty(RiZhiKz))
                        Mediinfo.Enterprise.Log.LogHelper.InitialRiZhiKZ(RiZhiKz);

                }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);


                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("服务启动发生错误：" + ex.ToString() + JsonUtil.SerializeObject(ex));
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine();
                LogHelper.Intance.Error("服务部署", "服务部署发生错误！", ex.ToString() + JsonUtil.SerializeObject(ex));
            }

            try
            {
                DbInterception.Add(new EFIntercepterLogging());
            }
            catch (Exception e)
            {
                Console.WriteLine("EF监听器设置失败");
                LogHelper.Intance.Error("EF监听器", "监听器设置失败", e.ToString() + JsonUtil.SerializeObject(e));
            }
        }

        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }
    }
}
