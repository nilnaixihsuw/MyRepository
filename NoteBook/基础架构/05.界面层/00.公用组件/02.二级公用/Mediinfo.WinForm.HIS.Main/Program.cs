using DevExpress.XtraSplashScreen;
using Mediinfo.Enterprise.Config;
using Mediinfo.Enterprise.Exceptions;
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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using LogHelper = Mediinfo.Enterprise.Log.LogHelper;

namespace Mediinfo.WinForm.HIS.Main
{
    internal static class Program
    {
        #region static constructor

        static Program()
        {
            // 未捕获的异常
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // 线程异常
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

            // 未处理异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            // 加载dll异常处理
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        #endregion

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Environment.CurrentDirectory = Application.StartupPath;
            if (Environment.OSVersion.Version.Major >= 6)
                Application.EnableVisualStyles();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!System.Diagnostics.Debugger.IsAttached) CreateShortcutOnDesktop();
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
                        // API网关超时，则直接从本地读取网关信息
                        Enterprise.Log.LogHelper.Intance.Error("api网关", "API网关请求失败", ex.ToString());
                        PermanenceServiceCache.Instance.GetGatewayInfo();
                    }
                }
            });


            LoginIntialCache.StartStyleRegister();          // 注册皮肤
            DevExpressLocalizerHelper.SetSimpleChinese();   // 中文设置

            // 获取本地API网关信息
            PermanenceServiceCache.Instance.GetGatewayInfo();

            #region 登录

            if (args.Contains("SwitchSystem"))
            {
                //切换系统
                HISClientHelper.USERID = args[1];
                HISClientHelper.USERPWD = args[2];
                HISClientHelper.YINGYONGID = args[3];
                FormLoginFunc(args, true);
            }
            else if (args.Contains("SwitchUser"))
            {
                //切换用户
                HISClientHelper.IsSwitchUser = true;
                HISClientHelper.PreviewApp = args[1];
                FormLoginFunc(args);
            }
            else if (args.Contains("AgainSystem"))
            {
                //重新登录
                HISClientHelper.USERID = args[1];
                HISClientHelper.USERPWD = args[2];
                HISClientHelper.YINGYONGID = args[3];
                FormLoginFunc(args, false, true);

            }
            else if (args.Length == 0)
            {
                FormLoginFunc(args);
            }
            else
            {
                SplashScreenManager.ShowForm(typeof(MediWaitForm));
                string[] loginArgs = args;
                MediMsgBox.Warn("canshu" + args);
                if (loginArgs.Length == 3) // 应用ID | 工号 | 密码
                {
                    if (!string.IsNullOrWhiteSpace(loginArgs[0]) && !string.IsNullOrWhiteSpace(loginArgs[1]) &&
                        !string.IsNullOrWhiteSpace(loginArgs[2]))
                    {
                        LoginIntialCache.LoadArgsLoginInfo(loginArgs[0], loginArgs[1], loginArgs[2], string.Empty,
                            true);
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
                else if (loginArgs.Length == 4) // 应用ID | 工号 | 密码 | 密码是否加密
                {
                    if (!string.IsNullOrWhiteSpace(loginArgs[0]) && !string.IsNullOrWhiteSpace(loginArgs[1]) &&
                        !string.IsNullOrWhiteSpace(loginArgs[2]))
                    {
                        LoginIntialCache.LoadArgsLoginInfo(loginArgs[0], loginArgs[1], loginArgs[2], string.Empty,
                            loginArgs[3] == "1" ? true : false);
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
                else if (loginArgs.Length == 5) // 应用ID | 工号 | 密码 | 病区ID | 密码是否加密 为了兼容老系统
                {
                    if (string.IsNullOrWhiteSpace(loginArgs[4]))
                    {
                        if (!string.IsNullOrWhiteSpace(loginArgs[0]) && !string.IsNullOrWhiteSpace(loginArgs[1]) &&
                            !string.IsNullOrWhiteSpace(loginArgs[2]))
                        {
                            LoginIntialCache.LoadArgsLoginInfo(loginArgs[0], loginArgs[1], loginArgs[2], loginArgs[3],
                                false);
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
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(loginArgs[0]) && !string.IsNullOrWhiteSpace(loginArgs[1]) &&
                            !string.IsNullOrWhiteSpace(loginArgs[2]))
                        {
                            LoginIntialCache.LoadArgsLoginInfo(loginArgs[0], loginArgs[1], loginArgs[2], loginArgs[3],
                                loginArgs[4] == "1" ? true : false);
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
                else if (loginArgs.Length == 6) // /Y 应用ID /U 工号 /P 密码
                {
                    if (!string.IsNullOrWhiteSpace(loginArgs[1]) && !string.IsNullOrWhiteSpace(loginArgs[3]) &&
                        !string.IsNullOrWhiteSpace(loginArgs[5]))
                    {
                        LoginIntialCache.LoadArgsLoginInfo(loginArgs[1], loginArgs[3], loginArgs[5], string.Empty,
                            true);
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

            #endregion
        }

        #region private methods

        /// <summary>
        /// 执行登录
        /// </summary>
        /// <param name="args"></param>
        /// <param name="switchSystem">是否切换系统</param>
        /// <param name="againSystem">是否重登系统</param>
        private static void FormLoginFunc(string[] args, bool switchSystem = false, bool againSystem = false)
        {
            DengLu dengLu = new DengLu()
            {
                SwitchSystem = switchSystem,
                AgainSystem = againSystem
            };
            GlobalExceptionParentForm.MediForm = dengLu;
            Application.Run(dengLu);
        }
        /// <summary>
        /// 创建当前正在运行程序的快捷方式
        /// </summary>
        public static void CreateShortcutOnDesktop()
        {
            string shortcutFullFile = AppDomain.CurrentDomain.BaseDirectory + "HISGlobalSettingHttp.xml";
            string shortcutname = "联众HALO";
            string description = "Copyright © 1999-2020 联众智慧科技股份有限公司 版权所有";
            if (System.IO.File.Exists(shortcutFullFile))
            {
                System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
                xmlDocument.Load(shortcutFullFile);
                if (xmlDocument.SelectSingleNode("HISGlobalSetting") != null && xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo") != null && xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo/name") != null)
                {
                    shortcutname = xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo/name").InnerText;
                    if (string.IsNullOrWhiteSpace(shortcutname))
                        xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo/name").InnerText = "联众HALO";
                }
                else//创建节点
                {
                    if (xmlDocument.SelectSingleNode("HISGlobalSetting") == null)
                    {
                        XmlElement xmlElement = xmlDocument.CreateElement("HISGlobalSetting");
                        xmlDocument.AppendChild(xmlElement);
                    }
                    if (xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo") == null)
                    {
                        XmlNode parentXmlNode = xmlDocument.SelectSingleNode("HISGlobalSetting");
                        XmlNode xmlNode = xmlDocument.CreateNode(XmlNodeType.Element, "shortcutinfo", null);
                        parentXmlNode.AppendChild(xmlNode);
                    }
                    if (xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo/name") == null)
                    {
                        XmlNode parentXmlNode = xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo");

                        XmlNode xmlNode = xmlDocument.CreateNode(XmlNodeType.Element, "name", null);
                        xmlNode.InnerText = shortcutname;
                        parentXmlNode.AppendChild(xmlNode);
                    }
                }

                if (xmlDocument.SelectSingleNode("HISGlobalSetting") != null && xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo") != null && xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo/description") != null)
                {
                    description = xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo/description").InnerText;
                    if (string.IsNullOrWhiteSpace(description))
                        xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo/description").InnerText = "Copyright © 1999-2017 联众智慧科技股份有限公司 版权所有";
                }
                else//创建节点
                {
                    if (xmlDocument.SelectSingleNode("HISGlobalSetting") == null)
                    {
                        XmlElement xmlElement = xmlDocument.CreateElement("HISGlobalSetting");
                        xmlDocument.AppendChild(xmlElement);
                    }
                    if (xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo") == null)
                    {
                        XmlNode parentXmlNode = xmlDocument.SelectSingleNode("HISGlobalSetting");
                        XmlNode xmlNode = xmlDocument.CreateNode(XmlNodeType.Element, "shortcutinfo", null);
                        parentXmlNode.AppendChild(xmlNode);
                    }
                    if (xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo/description") == null)
                    {
                        XmlNode parentXmlNode = xmlDocument.SelectSingleNode("HISGlobalSetting/shortcutinfo");

                        XmlNode xmlNode = xmlDocument.CreateNode(XmlNodeType.Element, "description", null);
                        xmlNode.InnerText = description;
                        parentXmlNode.AppendChild(xmlNode);
                    }
                }

                xmlDocument.Save(shortcutFullFile);


            }

            String shortcutPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), shortcutname + ".lnk");
            String exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            IWshRuntimeLibrary.IWshShell shell = new IWshRuntimeLibrary.WshShell();
            foreach (var item in System.IO.Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "*.lnk"))
            {
                IWshRuntimeLibrary.WshShortcut tempShortcut = (IWshRuntimeLibrary.WshShortcut)shell.CreateShortcut(item);
                if (tempShortcut.TargetPath == exePath) return;
            }
            IWshRuntimeLibrary.WshShortcut shortcut = (IWshRuntimeLibrary.WshShortcut)shell.CreateShortcut(shortcutPath);
            shortcut.TargetPath = exePath;
            shortcut.Arguments = "";
            shortcut.Description = description;
            shortcut.WorkingDirectory = Environment.CurrentDirectory;
            shortcut.IconLocation = exePath;
            shortcut.WindowStyle = 1;
            shortcut.Save();
        }

        #endregion

        #region private events

        /// <summary>
        /// 线程异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            string str = "";
            string strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now.ToString() + "\r\n";
            BaseException error = e.Exception as BaseException;
            if (error != null)
            {
                str = string.Format(strDateInfo + "异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n",
                     error.GetType().Name, error.Message, error.StackTrace);
            }
            else
            {
                str = string.Format("应用程序线程错误:{0}", e.Exception.Source + e.Exception.Message);
            }

            str += "\r\njson日志：\r\n";
            str += JsonUtil.SerializeObject(error);

            str += "=======================================================\r\n";

            #region 记录日志

            // 记录日志=====================================================================

            var network = NetworkHeler.GetAvailableNetwork()[0];
            //ESLog eSLog = new ESLog();
            SysLogEntity logEntity = new SysLogEntity();
            logEntity.RiZhiID = Guid.NewGuid().ToString();
            logEntity.ChuangJianSj = DateTime.Now.ToInvariantString("yyyy/MM/dd HH:mm:ss");
            logEntity.RiZhiBt = "[" + HISClientHelper.USERNAME + "]在使用[" + HISClientHelper.DANGQIANCKMC + "]界面时发生异常。";
            logEntity.RiZhiNr = str;

            logEntity.FuWuMc = "";
            logEntity.QingQiuLy = HISClientHelper.DANGQIANCKMC;
            // 日志类型：1.菜单打开，2.客户端异常，3.服务调用，4服务端异常，5.SQL日志，6.性能日志
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
            //eSLog.PutLog(logEntity);
            LogHelper.Intance.PutSysErrorLog(logEntity);
            // 记录日志=====================================================================

            #endregion 记录日志

            LocalLog.WriteLog(typeof(Main.Program), str);
            if (SplashScreenManager.Default != null)
                if (SplashScreenManager.Default.IsSplashFormVisible)
                    SplashScreenManager.CloseForm();
            if (GlobalExceptionParentForm.MediForm != null)
            {
                if (error != null)
                {
                    if (error?.Level == 1)
                        MediMsgBox.Warn(GlobalExceptionParentForm.MediForm, error?.Message, "确定");
                    else
                        MediMsgBox.Warn(GlobalExceptionParentForm.MediForm, error?.Message, error?.Message + "\r\n" + error?.StackTrace, false);
                }
            }
            else
            {
                if (error != null)
                {
                    if (error.Level == 1)
                        MediMsgBox.Warn(error?.Message);
                    else
                        MediMsgBox.Warn(error?.Message, error?.Message + "\r\n" + error?.StackTrace, false);
                }
            }
        }

        /// <summary>
        /// 未处理异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = "";
            BaseException error = e.ExceptionObject as BaseException;
            string strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now.ToString(CultureInfo.InvariantCulture) + "\r\n";

            str = error != null ? string.Format(strDateInfo + "Application UnhandledException:{0};\n\r堆栈信息:{1}", error.Message, error.StackTrace) : string.Format("Application UnhandledError:{0}", e);

            str += "\r\njson日志：\r\n";
            str += JsonUtil.SerializeObject(error);

            str += "=======================================================\r\n";

            #region 记录日志

            // 记录日志=====================================================================

            //ESLog eSLog = new ESLog();
            SysLogEntity logEntity = new SysLogEntity();
            logEntity.RiZhiID = Guid.NewGuid().ToString();
            logEntity.ChuangJianSj = DateTime.Now.ToInvariantString("yyyy/MM/dd HH:mm:ss");
            logEntity.RiZhiBt = "[" + HISClientHelper.USERNAME + "]在使用[" + HISClientHelper.DANGQIANCKMC + "]界面时发生异常。";
            logEntity.RiZhiNr = str;

            logEntity.FuWuMc = "";
            logEntity.QingQiuLy = HISClientHelper.DANGQIANCKMC;
            // 日志类型：1.菜单打开，2.客户端异常，3.服务调用，4服务端异常，5.SQL日志，6.性能日志
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
            //eSLog.PutLog(logEntity);
            LogHelper.Intance.PutSysErrorLog(logEntity);
            //记录日志=====================================================================

            #endregion 记录日志

            LocalLog.WriteLog(typeof(Main.Program), str);

            if (SplashScreenManager.Default != null)
                if (SplashScreenManager.Default.IsSplashFormVisible)
                    SplashScreenManager.CloseForm();
            if (GlobalExceptionParentForm.MediForm != null)
            {
                if (error != null)
                {
                    if (error?.Level == 1)
                        MediMsgBox.Warn(GlobalExceptionParentForm.MediForm, error?.Message, "确定");
                    else
                        MediMsgBox.Warn(GlobalExceptionParentForm.MediForm, error?.Message, error?.Message + "\r\n" + error?.StackTrace, false);
                }
            }
            else
            {
                if (error != null)
                {
                    if (error?.Level == 1)
                        MediMsgBox.Warn(error?.Message);
                    else
                        MediMsgBox.Warn(error?.Message, error?.Message + "\r\n" + error?.StackTrace, false);
                }
            }
        }

        /// <summary>
        /// 加载dll异常处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                DirectoryInfo startPathInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                String rootPathInfo = startPathInfo.Parent.FullName;    // 上一级的目录
                string path = Path.Combine(rootPathInfo, @"\DevExpress\");
                path = Path.Combine(rootPathInfo + @"\DevExpress\", args.Name.Split(',')[0]);
                path = String.Format(@"{0}.dll", path);
                if (File.Exists(path))
                    return Assembly.LoadFrom(path);
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException + ex.Message);
            }
        }

        #endregion
    }
}