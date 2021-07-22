using DevExpress.XtraSplashScreen;
using Mediinfo.Enterprise.Config;
using Mediinfo.Enterprise.Log;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.Core;
using Mediinfo.Utility;
using Mediinfo.Utility.Extensions;
using Mediinfo.Utility.Util;
using Mediinfo.WinForm.HIS.Controls;
using Mediinfo.WinForm.HIS.Core;
using Polly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xilium.CefGlue;

namespace Mediinfo.WinForm.HIS.Main
{
    internal static class Program
    {
        static Program()
        {
            #region 异常处理

            //处理未捕获的异常
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            //处理UI线程异常
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

            //处理非UI线程异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            //加载dll异常处理
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            #endregion 异常处理
        }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            // 解决Environment.CurrentDirectory部分情况值为C:\Windows\system32的bug，比如在执行目录下搜索main.exe，然后双击打开 -- add by 余佳平
            Environment.CurrentDirectory = Application.StartupPath;
            InitYBSHDLL();//[HR6-367]诊间_医保审核，dll一定要先于CEF之前做初始化，不然程序就会闪退
            
            Task.Factory.StartNew(() =>
            {
                if (MediinfoConfig.GetValue("WinFormMain.xml", "RunningMode") == "Cluster")
                {
                    try
                    {
                        var policy = Policy
                          .Handle<Exception>()
                          .Retry(3, (ex, count) =>
                          {
                              Enterprise.Log.LogHelper.Intance.Warn("api网关", "登录时，初始化API网关缓存服务失败，正在重试...当前重试次数：" + count,
                                  "API网关请求超时，正在重试...当前重试次数：" + count + ",失败原因：" + ex.ToString());
                          });

                        policy.Execute(() =>
                        {

                            string gatewayJson = RestfulClient.Post("http://" + ServiceClient.ServerUrl + "/GatewayServices");

                            Dictionary<string, string> serviceList = JsonUtil.DeserializeToObject<Dictionary<string, string>>(gatewayJson);

                            if (serviceList != null && serviceList.Count > 0)
                                PermanenceServiceCache.Instance.SaveGatewayInfo(gatewayJson);
                            else
                                throw new Exception("API网关请求失败，直接启用本地缓存");
                        });
                    }
                    catch (Exception ex)
                    {
                        //API网关超时，则直接从本地读取网关信息
                        Enterprise.Log.LogHelper.Intance.Error("api网关", "API网关请求失败", ex.ToString());
                        PermanenceServiceCache.Instance.GetGatewayInfo();
                    }
                }
            });

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DCSoft.Writer.WriterAppHost.PreloadSystem();
            DCSoft.Writer.DCWriterPublish.Start();

            LoginIntialCache.StartStyleRegister();//注册皮肤
            DevExpressLocalizerHelper.SetSimpleChinese();//中文设置
            //Form1 frm1 = new Form1();
            //frm1.ShowDialog();
            // 启动时调用批处理文件hisstart.bat
            string errorMsg = string.Empty;
            Task.Factory.StartNew(() =>
            {
                HISClientHelper.BatRunCmd("hisstart.bat", AppDomain.CurrentDomain.BaseDirectory, out errorMsg);
            });
            if (!string.IsNullOrWhiteSpace(errorMsg))
                throw new ApplicationException(errorMsg);
            // 设置本地缓存起始时间
            HISClientHelper.SetSysDate(HISClientHelper.GetSysDate());
            // 获取本地API网关信息
            PermanenceServiceCache.Instance.GetGatewayInfo();
           
            #region 登录

            if (args.Length > 0)
            {
                SplashScreenManager.ShowForm(typeof(MediWaitForm));
                string[] loginArgs = args;

                if (loginArgs.Length == 3)//应用ID|工号|密码
                {
                    if (!string.IsNullOrWhiteSpace(loginArgs[0]) && !string.IsNullOrWhiteSpace(loginArgs[1]) && !string.IsNullOrWhiteSpace(loginArgs[2]))
                    {
                        LoginIntialCache.LoadArgsLoginInfo(loginArgs[0], loginArgs[1], loginArgs[2], string.Empty, true);
                        if (SplashScreenManager.Default != null)
                            if (SplashScreenManager.Default.IsSplashFormVisible)
                                SplashScreenManager.CloseForm();
                    }
                    else
                    {
                        if (SplashScreenManager.Default != null)
                            if (SplashScreenManager.Default.IsSplashFormVisible)
                                SplashScreenManager.CloseForm();

                        MediMsgBox.Warn("快捷方式中参数中用户名、密码、应用是必输项");
                        Application.Exit();
                        return;
                    }
                }
                else if (loginArgs.Length == 5)//应用ID|工号|密码|病区ID|密码是否加密 为了兼容老系统
                {
                    if (string.IsNullOrWhiteSpace(loginArgs[4]))
                    {
                        if (!string.IsNullOrWhiteSpace(loginArgs[0]) && !string.IsNullOrWhiteSpace(loginArgs[1]) && !string.IsNullOrWhiteSpace(loginArgs[2]))
                        {
                            LoginIntialCache.LoadArgsLoginInfo(loginArgs[0], loginArgs[1], loginArgs[2], loginArgs[3], false);
                            if (SplashScreenManager.Default != null)
                                if (SplashScreenManager.Default.IsSplashFormVisible)
                                    SplashScreenManager.CloseForm();
                        }
                        else
                        {
                            if (SplashScreenManager.Default != null)
                                if (SplashScreenManager.Default.IsSplashFormVisible)
                                    SplashScreenManager.CloseForm();

                            MediMsgBox.Warn("快捷方式中参数中用户名、密码、应用是必输项");
                            Application.Exit();
                            return;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(loginArgs[0]) && !string.IsNullOrWhiteSpace(loginArgs[1]) && !string.IsNullOrWhiteSpace(loginArgs[2]))
                        {
                            LoginIntialCache.LoadArgsLoginInfo(loginArgs[0], loginArgs[1], loginArgs[2], loginArgs[3], loginArgs[4] == "1" ? true : false);
                            if (SplashScreenManager.Default != null)
                                if (SplashScreenManager.Default.IsSplashFormVisible)
                                    SplashScreenManager.CloseForm();
                        }
                        else
                        {
                            if (SplashScreenManager.Default != null)
                                if (SplashScreenManager.Default.IsSplashFormVisible)
                                    SplashScreenManager.CloseForm();

                            MediMsgBox.Warn("快捷方式中参数中用户名、密码、应用是必输项");
                            Application.Exit();
                            return;
                        }
                    }
                }
                else if (loginArgs.Length == 4)//应用ID|工号|密码|密码是否加密
                {
                    if (!string.IsNullOrWhiteSpace(loginArgs[0]) && !string.IsNullOrWhiteSpace(loginArgs[1]) && !string.IsNullOrWhiteSpace(loginArgs[2]))
                    {
                        LoginIntialCache.LoadArgsLoginInfo(loginArgs[0], loginArgs[1], loginArgs[2], string.Empty, loginArgs[3] == "1" ? true : false);
                        if (SplashScreenManager.Default != null)
                            if (SplashScreenManager.Default.IsSplashFormVisible)
                                SplashScreenManager.CloseForm();
                    }
                    else
                    {
                        if (SplashScreenManager.Default != null)
                            if (SplashScreenManager.Default.IsSplashFormVisible)
                                SplashScreenManager.CloseForm();

                        MediMsgBox.Warn("快捷方式中参数中用户名、密码、应用是必输项");
                        Application.Exit();
                        return;
                    }
                }
                else if (loginArgs.Length == 6)      // /Y 应用ID /U 工号 /P 密码
                {
                    if (!string.IsNullOrWhiteSpace(loginArgs[1]) && !string.IsNullOrWhiteSpace(loginArgs[3]) && !string.IsNullOrWhiteSpace(loginArgs[5]))
                    {
                        LoginIntialCache.LoadArgsLoginInfo(loginArgs[1], loginArgs[3], loginArgs[5], string.Empty, true);
                        if (SplashScreenManager.Default != null)
                        {
                            if (SplashScreenManager.Default.IsSplashFormVisible)
                            {
                                SplashScreenManager.CloseForm();
                            }
                        }
                    }
                    else
                    {
                        if (SplashScreenManager.Default != null)
                        {
                            if (SplashScreenManager.Default.IsSplashFormVisible)
                            {
                                SplashScreenManager.CloseForm();
                            }
                        }

                        MediMsgBox.Warn("快捷方式中参数中用户名、密码、应用是必输项");
                        Application.Exit();
                        return;
                    }
                }
            }
            else
            {
                FormLoginFunc(args);
            }

            #endregion
        }
        const string path = "Audit4Hospital.dll";
        [DllImport(path, EntryPoint = "InitXML", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern string InitXML();
        /// <summary>
        /// 初始化医保审核dll
        /// </summary>
        private static void InitYBSHDLL()
        {
            //医保审核的dll和CEF初始化冲突，导致初始化的时候程序闪退，需要先初始化医保审核dll，
            if (File.Exists(Application.StartupPath+ "\\Audit4Hospital.dll"))
            {
                try
                {
                    InitXML();
                }
                catch (Exception ex)//初始化报错不用去处理 
                { 
                }
                
            }
        }
        private static void FormLoginFunc(string[] args)
        {
            DengLu dengLu = new DengLu();
            GlobalExceptionParentForm.MediForm = dengLu;
            Application.Run(dengLu);
        }
        
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str = "";
            string strDateInfo = "出现应用程序未处理的异常：" + HISClientHelper.GetSysDate().ToString() + "\r\n";
            Exception error = e.Exception as Exception;
            if (error != null)
            {
                str = string.Format(strDateInfo + "异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n",
                     error.GetType().Name, error.Message, error.StackTrace);
            }
            else
            {
                str = string.Format("应用程序线程错误:{0}", e);
            }

            str += "\r\njson日志：\r\n";
            str += JsonUtil.SerializeObject(error);

            str += "=======================================================\r\n";
            #region 记录日志

            //记录日志=====================================================================
            var network = NetworkHeler.GetAvailableNetwork()[0];
            ESLog eSLog = new ESLog();
            SysLogEntity logEntity = new SysLogEntity();
            logEntity.RiZhiID = Guid.NewGuid().ToString();
            logEntity.ChuangJianSj = HISClientHelper.GetSysDate().ToInvariantString("yyyy/MM/dd HH:mm:ss");
            logEntity.RiZhiBt = "[" + HISClientHelper.USERNAME + "]在使用[" + HISClientHelper.DANGQIANCKMC + "]界面时发生异常。";
            logEntity.RiZhiNr = str;

            logEntity.FuWuMc = "";
            logEntity.QingQiuLy = HISClientHelper.DANGQIANCKMC;
            //日志类型：1.菜单打开，2.客户端异常，3.服务调用，4服务端异常，5.SQL日志，6.性能日志
            logEntity.RiZhiLx = 2;
            logEntity.YINGYONGID = HISClientHelper.YINGYONGID;
            logEntity.XITONGID = HISClientHelper.XITONGID;
            logEntity.YINGYONGMC = HISClientHelper.YINGYONGMC;
            logEntity.YINGYONGJC = HISClientHelper.YINGYONGJC;
            logEntity.VERSION = HISClientHelper.VERSION;
            logEntity.IP = network.Ip;
            logEntity.MAC = network.PhysicalAddress;
            logEntity.COMPUTERNAME = network.ComputerName;
            logEntity.USERNAME = HISClientHelper.USERNAME;
            logEntity.USERID = HISClientHelper.USERID;
            logEntity.KESHIID = HISClientHelper.KESHIID;
            logEntity.KESHIMC = HISClientHelper.KESHIMC;
            logEntity.BINGQUID = HISClientHelper.BINGQUID;
            logEntity.BINGQUMC = HISClientHelper.BINGQUMC;
            logEntity.JIUZHENKSID = HISClientHelper.JIUZHENKSID;
            logEntity.JIUZHENKSMC = HISClientHelper.JIUZHENKSMC;
            logEntity.YUANQUID = HISClientHelper.YUANQUID;
            logEntity.GONGZUOZID = HISClientHelper.GONGZUOZID;
            eSLog.PutLog(logEntity);
            //记录日志=====================================================================

            #endregion 记录日志
            LocalLog.WriteLog(typeof(Main.Program), str);
            if (SplashScreenManager.Default != null)
                if (SplashScreenManager.Default.IsSplashFormVisible)
                    SplashScreenManager.CloseForm();
            if (GlobalExceptionParentForm.MediForm != null)
            {
                if (error != null)
                    MediMsgBox.Warn(GlobalExceptionParentForm.MediForm, "发生错误，请及时联系管理员！",
                        error.Message + "\r\n" + error.StackTrace, false);
            }
            else if (error != null) MediMsgBox.Warn("发生错误，请及时联系管理员！", error.Message + "\r\n" + error.StackTrace, false);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = "";
            Exception error = e.ExceptionObject as Exception;
            string strDateInfo = "出现应用程序未处理的异常：" + HISClientHelper.GetSysDate().ToString() + "\r\n";

            if (error != null)
            {
                str = string.Format(strDateInfo + "Application UnhandledException:{0};\n\r堆栈信息:{1}", error.Message, error.StackTrace);
            }
            else
            {
                str = string.Format("Application UnhandledError:{0}", e);
            }

            //#region 记录日志

            str += "\r\njson日志：\r\n";
            str += JsonUtil.SerializeObject(error);

            str += "=======================================================\r\n";
            #region 记录日志

            //记录日志=====================================================================
            ESLog eSLog = new ESLog();
            SysLogEntity logEntity = new SysLogEntity();
            logEntity.RiZhiID = Guid.NewGuid().ToString();
            logEntity.ChuangJianSj = HISClientHelper.GetSysDate().ToInvariantString("yyyy/MM/dd HH:mm:ss");
            logEntity.RiZhiBt = "[" + HISClientHelper.USERNAME + "]在使用[" + HISClientHelper.DANGQIANCKMC + "]界面时发生异常。";
            logEntity.RiZhiNr = str;

            logEntity.FuWuMc = "";
            logEntity.QingQiuLy = HISClientHelper.DANGQIANCKMC;
            //日志类型：1.菜单打开，2.客户端异常，3.服务调用，4服务端异常，5.SQL日志，6.性能日志
            logEntity.RiZhiLx = 2;
            logEntity.YINGYONGID = HISClientHelper.YINGYONGID;
            logEntity.XITONGID = HISClientHelper.XITONGID;
            logEntity.YINGYONGMC = HISClientHelper.YINGYONGMC;
            logEntity.YINGYONGJC = HISClientHelper.YINGYONGJC;
            logEntity.VERSION = HISClientHelper.VERSION;
            logEntity.IP = HISClientHelper.IP;
            logEntity.MAC = HISClientHelper.MAC;
            logEntity.COMPUTERNAME = HISClientHelper.COMPUTERNAME;
            logEntity.USERNAME = HISClientHelper.USERNAME;
            logEntity.USERID = HISClientHelper.USERID;
            logEntity.KESHIID = HISClientHelper.KESHIID;
            logEntity.KESHIMC = HISClientHelper.KESHIMC;
            logEntity.BINGQUID = HISClientHelper.BINGQUID;
            logEntity.BINGQUMC = HISClientHelper.BINGQUMC;
            logEntity.JIUZHENKSID = HISClientHelper.JIUZHENKSID;
            logEntity.JIUZHENKSMC = HISClientHelper.JIUZHENKSMC;
            logEntity.YUANQUID = HISClientHelper.YUANQUID;
            logEntity.GONGZUOZID = HISClientHelper.GONGZUOZID;
            eSLog.PutLog(logEntity);
            //记录日志=====================================================================

            #endregion 记录日志
            LocalLog.WriteLog(typeof(Main.Program), str);

            if (SplashScreenManager.Default != null)
                if (SplashScreenManager.Default.IsSplashFormVisible)
                    SplashScreenManager.CloseForm();
            if (GlobalExceptionParentForm.MediForm != null)
            {
                if (error != null)
                    MediMsgBox.Warn(GlobalExceptionParentForm.MediForm, "发生错误，请及时联系管理员！", error.Message, false);
            }
            else if (error != null) MediMsgBox.Warn("发生错误，请及时联系管理员！", error.Message, false);
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                DirectoryInfo startPathInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                String rootPathInfo = startPathInfo.Parent.FullName; //上一级的目录
                string path = Path.Combine(rootPathInfo, @"\DevExpress\");
                path = Path.Combine(rootPathInfo + @"\DevExpress\", args.Name.Split(',')[0]);
                path = String.Format(@"{0}.dll", path);
                if (File.Exists(path))
                    return System.Reflection.Assembly.LoadFrom(path);
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException + ex.Message);
            }
        }
    }
}