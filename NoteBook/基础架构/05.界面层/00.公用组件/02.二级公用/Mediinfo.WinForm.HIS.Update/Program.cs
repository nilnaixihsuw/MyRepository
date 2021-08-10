using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Update
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Process process = RunningInstance();
            if (process != null)
            {
                HandleRunningInstance(process);
                Environment.Exit(1);
            }

            #region 异常处理

            // 指示应用程序如何响应未处理的异常
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // 处理UI线程异常
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

            // 处理未捕获异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            #endregion

            try
            {
                string argument = "";
                // 获取参数，参数格式：/Y 应用ID /U 工号 /P 密码 /M FTP
                if (args.Length > 0)
                {
                    // 判断是否为Http下载方式
                    if ("HTTP".Equals(args[args.Length - 1].ToUpper()))
                    {
                        // 设置为Http更新模式
                        HISGlobalSetting.IsHttp = true;
                        for (int i = 0; i < args.Length; i++)
                        {
                            if (args[i] == "/M")
                                break;
                            if (i == 1)
                                args[i] = args[i].Substring(0, 2) + "01";
                            argument += " " + args[i];
                        }
                        HISGlobalSetting.zxt = args[1].Substring(0, 2);
                    }
                    else if ("FTP".Equals(args[args.Length - 1].ToUpper()))     // 判断是否为FTP下载方式
                    {
                        // 设置为FTP更新模式
                        HISGlobalSetting.IsHttp = false;
                        // 参数格式：登录信息,子系统FTP文件夹路径,子系统本地文件夹路径，下载方式
                        // 转换登录信息
                        args[0] = args[0].Replace("|", " ");
                        HISGlobalHelper.GlobalSetting.FTPINFO.FtpFirstSubDirectoryName = args[1];
                        FTPConfigFrm.FtpFirstSubDirectoryName = args[1];
                        FTPConfigFrm.XiTongLoaclPath = args[2];
                    }
                }

                // Http下载
                if (HISGlobalSetting.IsHttp)
                {
                    if (File.Exists(Path.Combine(Application.StartupPath, "DownLoadAddress.xml")))
                    {
                        string starturl = MediinfoConfig.GetValue("DownLoadAddress.xml", "startUrl");
                        string updatemode = MediinfoConfig.GetValue("DownLoadAddress.xml", "upDateMode");
                        if (!string.IsNullOrEmpty(starturl))
                            HISGlobalSetting.StartUp_ZXT = starturl;
                        if (!string.IsNullOrEmpty(updatemode))
                            HISGlobalSetting.IsHttp = true;
                    }
                    else
                    {
                        using (HttpConfig hTTPConfig = new HttpConfig())
                        {
                            hTTPConfig.ShowDialog();
                            if (hTTPConfig.DialogResult == DialogResult.Cancel)
                                return;
                        }
                    }

                    using (MediInfoUpdate mediInfoUpdate = new MediInfoUpdate())
                    {
                        if (mediInfoUpdate.ShowDialog() == DialogResult.OK)
                        {
                            if (mediInfoUpdate.Tag != null && ((List<HTTPUpdateConfig>)mediInfoUpdate.Tag).Count > 0)
                            {
                                MediinfoUpdateMainForm mediinfoUpdateMainForm = null;
                                if (args.Length > 0)
                                    mediinfoUpdateMainForm = new MediinfoUpdateMainForm((List<HTTPUpdateConfig>)mediInfoUpdate.Tag, argument);
                                else
                                    mediinfoUpdateMainForm = new MediinfoUpdateMainForm((List<HTTPUpdateConfig>)mediInfoUpdate.Tag, string.Empty);

                                KillProcess(UpdateCommonHelper.LoginFormName);
                                if (!string.IsNullOrWhiteSpace(HISGlobalSetting.zxt))
                                {
                                    string zxtTaskKillBat = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "AssemblyClient\\" + HISGlobalSetting.zxt,"taskkill.bat");
                                    RunBatCmd(zxtTaskKillBat);
                                }
                                Application.Run(mediinfoUpdateMainForm);
                            }
                            else
                            {
                                try
                                {
                                    KillProcess("Mediinfo.WinForm.HIS.Main.exe");
                                    ProcessStartInfo processStartInfo = new ProcessStartInfo();
                                    if (!string.IsNullOrEmpty(HISGlobalSetting.zxt))
                                    {
                                        processStartInfo.Arguments = argument;
                                        var rootPath = Application.StartupPath + "\\AssemblyClient\\";
                                        processStartInfo.WorkingDirectory = rootPath + HISGlobalSetting.zxt + "\\";
                                        // 检测版本没更新打开 启动器.exe
                                        processStartInfo.FileName = "Mediinfo.WinForm.HIS.Main.exe";
                                        string startDirectory = rootPath + HISGlobalSetting.zxt + "\\" + processStartInfo.FileName;
                                        if (!File.Exists(startDirectory))
                                        {
                                            MessageBox.Show("Mediinfo.WinForm.HIS.Main.exe" + "不存在", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        if (argument.Length > 0)
                                            processStartInfo.Arguments = argument;
                                        // 检测版本没更新打开启动器.exe
                                        processStartInfo.FileName = "Mediinfo.WinForm.HIS.Starter.exe";
                                        processStartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory + "AssemblyClient\\";
                                        if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "AssemblyClient\\" + processStartInfo.FileName))
                                        {
                                            MessageBox.Show("Mediinfo.WinForm.HIS.Starter.exe" + "不存在", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                    }

                                    processStartInfo.Verb = "runas";
                                    processStartInfo.WindowStyle = ProcessWindowStyle.Normal;

                                    // 处理XP系统报错问题
                                    if (Environment.OSVersion.Version.Major >= 6)
                                        Process.Start(processStartInfo);
                                    else
                                        ShellExecute(0,
                                              "open",
                                              processStartInfo.FileName,
                                              argument,
                                              processStartInfo.WorkingDirectory,
                                              11);
                                }
                                catch (Exception exception)
                                {
                                    MessageBox.Show(exception.Message + exception.InnerException, "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    LogHelper.WriteLog(string.Format("Date {0} ,Class: {1}, Property: {2}, Error: {3}", DateTime.Now, "program",
                                           exception.Message,
                                          exception.InnerException));
                                    return;
                                }
                            }
                        }
                    }
                }
                else   // FTP下载
                {
                    using (MediInfoUpdate mediInfoUpdate = new MediInfoUpdate())
                    {
                        if (mediInfoUpdate.ShowDialog() == DialogResult.OK)
                        {
                            if (mediInfoUpdate.Tag != null && ((List<ServerAllFileDic>)mediInfoUpdate.Tag).Count > 0)
                            {
                                MediinfoUpdateMainForm mediinfoUpdateMainForm = null;
                                if (args.Length > 0)
                                    mediinfoUpdateMainForm = new MediinfoUpdateMainForm((List<ServerAllFileDic>)mediInfoUpdate.Tag, args[0]);
                                else
                                    mediinfoUpdateMainForm = new MediinfoUpdateMainForm((List<ServerAllFileDic>)mediInfoUpdate.Tag, string.Empty);
                                if (!string.IsNullOrWhiteSpace(FTPConfigFrm.XiTongLoaclPath))
                                {
                                    string zxtTaskKillBat = Path.Combine(FTPConfigFrm.XiTongLoaclPath,"taskkill.bat");
                                    RunBatCmd(zxtTaskKillBat);
                                }
                                KillProcess(UpdateCommonHelper.LoginFormName);
                                Application.Run(mediinfoUpdateMainForm);
                            }
                            else
                            {
                                try
                                {
                                    KillProcess(UpdateCommonHelper.LoginFormName);
                                    ProcessStartInfo processStartInfo = new ProcessStartInfo
                                    {
                                        FileName = UpdateCommonHelper.LoginFormName + ".exe"
                                    };
                                    if (args.Length > 0)
                                        processStartInfo.Arguments = args[0];

                                    processStartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory + "AssemblyClient";
                                    processStartInfo.Verb = "runas";
                                    if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "AssemblyClient\\" + processStartInfo.FileName))
                                    {
                                        MessageBox.Show(processStartInfo.FileName + "不存在", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                    processStartInfo.WindowStyle = ProcessWindowStyle.Normal;

                                    // 处理XP系统报错问题
                                    if (Environment.OSVersion.Version.Major >= 6)
                                        Process.Start(processStartInfo).WaitForExit();
                                    else
                                        ShellExecute(0,
                                            "open",
                                            processStartInfo.FileName,
                                            args[0],
                                            processStartInfo.WorkingDirectory,
                                            11);
                                }
                                catch (Exception exception)
                                {
                                    MessageBox.Show(exception.Message + exception.InnerException, "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    LogHelper.WriteLog(string.Format("Date {0} ,Class: {1}, Property: {2}, Error: {3}", DateTime.Now, "program",
                                           exception.Message,
                                          exception.InnerException));
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region const

        private const int SW_SHOWNOMAL = 1;

        #endregion

        #region extern

        /// <summary>
        /// 设置由不同线程产生的窗口的显示状态。
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="cmdShow">指定窗口如何显示</param>
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零。</returns>
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        /// <summary>  
        /// 该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。  
        /// 系统给创建前台窗口的线程分配的权限稍高于其他线程。   
        /// </summary>  
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄</param>  
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零</returns>  
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// 运行一个外部程序
        /// </summary>
        /// <param name="hwnd">指定父窗口句柄</param>
        /// <param name="lpOperation">指定动作, 譬如: open、runas、print、edit、explore、find</param>
        /// <param name="lpFile">指定要打开的文件或程序</param>
        /// <param name="lpParameters">给要打开的程序指定参数</param>
        /// <param name="lpDirectory">目录</param>
        /// <param name="nShowCmd">打开选项</param>
        /// <returns></returns>
        [DllImport("SHELL32.dll")]
        public static extern int ShellExecute(int hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);

        #endregion

        #region static methods

        /// <summary>
        /// 获取当前进程
        /// </summary>
        /// <returns></returns>
        private static Process RunningInstance()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] Processes = Process.GetProcessesByName(currentProcess.ProcessName);
            foreach (Process process in Processes)
            {
                if (process.Id != currentProcess.Id && process.MainModule.FileName.Equals(currentProcess.MainModule.FileName))
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == currentProcess.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 调用bat文件kill进程
        /// </summary>
        /// <param name="zxtTaskKillBat">子系统路径下bat文件</param>
        private static void RunBatCmd(string zxtTaskKillBat)
        {
            if (File.Exists(zxtTaskKillBat))
            {
                Process hisstartprocess = new Process();
                hisstartprocess.StartInfo = new ProcessStartInfo(zxtTaskKillBat, null);
                hisstartprocess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                hisstartprocess.Start();
            }
        }
        /// <summary>
        /// 当前进程事件
        /// </summary>
        /// <param name="instance"></param>
        private static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, SW_SHOWNOMAL);       // 显示  
            SetForegroundWindow(instance.MainWindowHandle);                 // 当到最前端  
        }

        /// <summary>
        /// 关闭进程
        /// </summary>
        /// <param name="processName">进程名称</param>
        private static void KillProcess(string processName)
        {
            try
            {
                foreach (Process thisproc in Process.GetProcessesByName(processName))
                {
                    if (thisproc.MainModule != null && (thisproc.MainModule.FileName.Equals(AppDomain.CurrentDomain.BaseDirectory + "AssemblyClient\\Mediinfo.WinForm.HIS.Starter.exe") && !thisproc.CloseMainWindow()))
                    {
                        thisproc.Kill();
                    }
                }

                Process[] prcs = Process.GetProcesses();
                foreach (Process p in prcs)
                {
                    if (p.MainModule != null && (p.ProcessName.Equals(processName) && p.MainModule.FileName.Equals(AppDomain.CurrentDomain.BaseDirectory + "AssemblyClient\\Mediinfo.WinForm.HIS.Starter.exe")))
                    {
                        p.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(string.Format("Date {0} ,Class: {1}, Property: {2}, Error: {3}", DateTime.Now, "Program",
                          ex.Message,
                         ex.InnerException));
            }
        }

        #endregion

        #region events

        /// <summary>
        /// UI线程异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception error = e.Exception;

            LogHelper.WriteLog(string.Format("Date {0} ,Class: {1}, Property: {2}, Error: {3}", DateTime.Now, "Program",
                           error?.Message,
                          error?.InnerException));

            MessageBox.Show(error?.Message + error?.InnerException, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 未捕获异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception error = e.ExceptionObject as Exception;

            LogHelper.WriteLog(string.Format("Date {0} ,Class: {1}, Property: {2}, Error: {3}", DateTime.Now, "Program",
                         error?.Message,
                        error?.InnerException));

            MessageBox.Show(error?.Message + error?.InnerException, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion
    }
}
