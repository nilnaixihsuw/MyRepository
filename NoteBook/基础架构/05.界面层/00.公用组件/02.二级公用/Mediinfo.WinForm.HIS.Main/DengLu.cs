using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Enterprise.Config;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Mediinfo.Utility;
using Mediinfo.Utility.Extensions;
using Mediinfo.WinForm.HIS.Controls;
using Mediinfo.WinForm.HIS.Core;
using Mediinfo.WinForm.HIS.Main.Properties;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Mediinfo.WinForm.HIS.Main
{
    public partial class DengLu : MediForm
    {
        #region constructor

        public DengLu()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint, true);

            loginService = new JCJGLoginService();

            gridLookUpEdit1.Tag = false;
            gridLookUpEdit1.GotFocus += GridLookUpEdit1_GotFocus;
        }

        #endregion

        #region fields

        /// <summary>
        /// 临床系统ID
        /// </summary>
        public Dictionary<string, string> lcXiTongIDSet = new Dictionary<string, string>();
        public Dictionary<string, string> lc2XiTongIDSet = new Dictionary<string, string>();
        public Dictionary<string, string> yfXiTongIDSet = new Dictionary<string, string>();


        private Rectangle rectangle = new Rectangle();

        private Stopwatch stopwatch;
        private Timer timer;

        #region 扫码登录
        private bool isclick = true;
        private bool isSm = true;//true扫码;false正常
        private int ErWmTimeCount;
        #endregion

        private bool isStartUpdate = false;
        private List<HTTPUpdateConfig> needUpdatedirectorys = new List<HTTPUpdateConfig>();

        /// <summary>
        /// 当前用户是否是从上次登录的用户信息里面读取的
        /// </summary>
        private bool LoadFromLastLoginInfo = false;

        /// <summary>
        /// 用户
        /// </summary>
        private LoginDTO loginDTO = null;

        /// <summary>
        /// 本机网络配置
        /// </summary>
        private List<NetworkConfig> networkList = null;

        private bool IsClickDengLuButton = false;
        public delegate DialogResult DialogResultDelegate();
        protected DialogResultDelegate DialogResultEvent;

        private ManualResetEvent manualReset = new ManualResetEvent(false);

        /// <summary>
        /// 登陆服务
        /// </summary>
        private JCJGLoginService loginService = null;

        // 是否显示阴影
        private bool aeroEnabled;

        /// <summary>
        /// 是否切换系统
        /// </summary>
        public bool SwitchSystem { get; set; }

        /// <summary>
        /// 是否重新登录
        /// </summary>
        public bool AgainSystem { get; set; }

        #endregion

        #region properties

        /// <summary>
        /// 是否显示窗体边框。
        /// </summary>
        [Browsable(true)]
        [DefaultValue(true)]
        [Description("是否显示窗体边框。")]
        public bool ShowBorder { get; set; } = false;

        /// <summary>
        /// 窗口默认边框颜色。
        /// </summary>
        [Browsable(true)]
        [DefaultValue(typeof(Color), "225, 225, 225")]
        [Description("窗口默认边框颜色。")]
        public Color BorderColor { get; set; } = Color.FromArgb(225, 225, 225);

        /// <summary>
        /// 窗口激活时的边框颜色。
        /// </summary>
        [Browsable(true)]
        [DefaultValue(typeof(Color), "221, 221, 221")]
        [Description("窗口激活时的边框颜色。")]
        public Color ActiveBorderColor = Color.FromArgb(221, 221, 221);

        protected override CreateParams CreateParams
        {
            get
            {
                aeroEnabled = CheckAeroEnabled();

                CreateParams cp = base.CreateParams;
                if (!aeroEnabled)
                    cp.ClassStyle |= 0x00020000;

                return cp;
            }
        }

        #endregion

        #region private methods

        /// <summary>
        /// 关闭更新进程
        /// </summary>
        /// <param name="rootPath">更新文件程序所在路径</param>
        private void KillUpdateProcess(string rootPath)
        {
            try
            {
                Process myproc = new Process();
                foreach (Process thisproc in Process.GetProcessesByName("Mediinfo.WinForm.HIS.Update"))
                {
                    if (thisproc.MainModule.FileName.Equals(rootPath + @"\Mediinfo.WinForm.HIS.Update.exe"))
                    {
                        if (!thisproc.CloseMainWindow())
                        {
                            thisproc.Kill();
                            GC.Collect();
                        }
                        Process[] prcs = Process.GetProcesses();
                        foreach (Process p in prcs)
                        {
                            if (p.ProcessName.Equals("Mediinfo.WinForm.HIS.Update") && p.MainModule.FileName.Equals(rootPath + @"\Mediinfo.WinForm.HIS.Update.exe"))
                            {
                                p.Kill();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Default.Error("ftpupdate进程杀死!", ex);
            }
        }

        /// <summary>
        /// 杀掉应用进程
        /// </summary>
        /// <param name="filePath"></param>
        private void KillYingYongJC(string filePath)
        {
            try
            {
                Process[] proList = Process.GetProcessesByName("Mediinfo.WinForm.HIS.Main");
                foreach (Process thisproc in proList)
                {
                    if (!thisproc.HasExited)
                    {
                        if (thisproc.MainModule.FileName.Equals(filePath + @"\Mediinfo.WinForm.HIS.Main.exe"))
                        {
                            if (!thisproc.HasExited)
                            {
                                thisproc.Kill();
                                GC.Collect();
                                KillYingYongJC(filePath);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Default.Error($"【{Path.Combine(filePath, "Mediinfo.WinForm.HIS.Main.exe")}】应用进程关闭失败", ex);
            }
        }

        /// <summary>
        /// 根据进程ID关闭当前进程
        /// </summary>
        /// <param name="id">进程ID</param>
        private void KillProcess(int id)
        {
            /*
             * 说明：此处根据进程ID关闭进程是由于客户端是可以多次启动，
             *      如果用名称关闭进程时可能存在关闭多个进程的问题
             */
            Process[] allProcess = Process.GetProcesses();
            foreach (Process p in allProcess)
            {
                if (p.Id == id)
                {
                    for (int i = 0; i < p.Threads.Count; i++)
                        p.Threads[i].Dispose();
                    p.Kill();

                    break;
                }
            }
        }

        /// <summary>
        /// 重绘控件
        /// </summary>
        /// <param name="control">需要被重绘的控件</param>
        private void PaintControl(Control control)
        {
            if (control is SimpleButton)
            {
                SimpleButton button = control as SimpleButton;

                float radius = 7f;
                int width = button.Width;
                int height = button.Height;

                GraphicsPath graphicsPath = new GraphicsPath();
                // 左上角
                graphicsPath.AddArc(0, 0, radius, radius, 183, 93);
                // 右上角
                graphicsPath.AddArc(width - radius, 0, radius, radius, 273, 90);
                // 右下角
                graphicsPath.AddArc(width - radius, height - radius, radius, radius, 0, 90);
                // 左下角
                graphicsPath.AddArc(0, height - radius, radius, radius, 93, 90);
                graphicsPath.CloseAllFigures();

                button.Region = new Region(graphicsPath);
            }
        }

        /// <summary>
        /// FTP配置
        /// </summary>
        /// <returns></returns>
        private DialogResult DialogResultFun()
        {
            using (FTPConfigFrm fTPConfigFrm = new FTPConfigFrm())
            {
                this.TopMost = false;

                fTPConfigFrm.TopMost = true;

                fTPConfigFrm.ShowDialog();
                if (fTPConfigFrm.DialogResult == DialogResult.OK)
                {
                    return DialogResult.OK;
                }
                else
                {
                    return DialogResult.Cancel;
                }
            }
        }

        /// <summary>
        /// 服务端连接失败重试
        /// </summary>
        /// <param name="exceptionstr">错误信息</param>
        private void ConnectionServer(string exceptionstr)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                using (ServerConnectTestForm serverConnectTestForm = new ServerConnectTestForm(networkList, exceptionstr))
                {
                    serverConnectTestForm.StartPosition = FormStartPosition.CenterParent;
                    if (serverConnectTestForm.ShowDialog(this) == DialogResult.OK)
                    {
                        loginDTO = serverConnectTestForm.result.Return;
                        this.LoadFromLastLoginInfo = true;

                        AutoWriteYongHuMing();

                        if (this.Handle != WinApiHelper.GetF())  // 如果本窗口没有获得焦点
                            WinApiHelper.SetF(this.Handle);      // 设置本窗口获得焦点
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }));
        }

        /// <summary>
        /// 从注册表中读取 是否启用自动登录 填写密码并点击登录
        /// </summary>
        private void AutoWritePassWordAndLogin()
        {
            try
            {
                RegistryKey location = Registry.LocalMachine;
                RegistryKey soft = location.OpenSubKey("SOFTWARE", false);//可写 
                RegistryKey myPass = soft.OpenSubKey("FTLiang", false);

                bool ifSave = Convert.ToBoolean(myPass?.GetValue("s3"));

                if (ifSave)
                {
                    textBox_MiMa.Text = myPass.GetValue("s2").ToString();

                    mediButtonDengLu.PerformClick();
                }
            }
            catch (Exception ex)
            {
                //todo something
            }
        }

        /// <summary>
        /// 从注册表中读取 是否启用自动登录 填写用户名
        /// </summary>
        private void AutoWriteYongHuMing()
        {
            try
            {
                RegistryKey location = Registry.LocalMachine;
                RegistryKey soft = location.OpenSubKey("SOFTWARE", false);  // 可写 
                RegistryKey myPass = soft.OpenSubKey("FTLiang", false);

                bool ifSave = Convert.ToBoolean(myPass?.GetValue("s3"));

                if (ifSave && HISClientHelper.ClientSetting.IsAutoWriteLastLoginInfo)
                {
                    textBox_YongHuMing.Text = myPass.GetValue("s1").ToString();
                }
            }
            catch (Exception ex)
            {
                //todo something
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        private void Exit()
        {
            HISClientHelper.ClientSetting.Save();
            HISClientHelper.GlobalSetting.Save();
            // 隐藏界面，避免UI卡住，达到秒退的效果
            this.Hide();
            // 关闭进程
            DisposeCurrentProcess();
        }

        private void CallBackUpdateProgram(UpdateConfigInfo updateConfigInfo, string rootPathInfo, BackgroundWorker bgWorker, BackgroundWokerState bgWorkerState, DoWorkEventArgs e)
        {
            UpdateHelper.InitialUserCustomInfo(updateConfigInfo);
            if (!File.Exists(rootPathInfo + "\\Temp\\version.ini")) // 如果当前根目录下没有该HIS6Version.ini文件，则下载，同时下载exe
            {
                if (!Directory.Exists(rootPathInfo + "\\Temp"))
                    Directory.CreateDirectory(rootPathInfo + "\\Temp");

                try
                {
                    UpdateHelper.GetFileNoBinary("UPDATE", "version.ini", rootPathInfo + "\\Temp", "version.ini");

                    File.Copy(rootPathInfo + "\\Temp\\version.ini", rootPathInfo + "\\version.ini", true);
                    DownLoadUpadteExe(rootPathInfo, bgWorker, bgWorkerState, e);

                    //Application.Exit();
                }
                catch (Exception ex)
                {
                    LogHelper.Default.Error("下载version.ini失败!", ex);
                    DownLoadUpadteExe(rootPathInfo, bgWorker, bgWorkerState, e);
                    if (bgWorker.CancellationPending)
                    {
                        bgWorkerState.State = -1;
                        bgWorkerState.ErrorMsg = string.Empty;
                        bgWorkerState.Tips = string.Empty;
                        bgWorkerState.Exit = true;
                        e.Result = bgWorkerState;
                        return;
                    }

                    bgWorker.ReportProgress(100, "SUCCESS");

                    bgWorkerState.State = 0;
                    bgWorkerState.ErrorMsg = string.Empty;
                    bgWorkerState.Tips = string.Empty;
                    bgWorkerState.Exit = false;
                    e.Result = bgWorkerState;
                    //Application.Exit();
                }
            }
            else   // 判断版本号是否一致
            {
                try
                {
                    UpdateHelper.GetFileNoBinary("UPDATE", "version.ini", rootPathInfo + "\\Temp", "version.ini");
                }
                catch (Exception ex)
                {
                    LogHelper.Default.Error("下载version.ini失败!", ex);
                }
                // 比较本地和temp文件夹下版本号是否一致
                string errorMsg = string.Empty;
                List<UpdateDirectories> directorylist = null;
                if (!string.IsNullOrWhiteSpace(UpdateHelper.FtpFirstSubDirectoryName))
                {
                    directorylist = UpdateDirectory.GetUpdateDirectories(rootPathInfo, UpdateHelper.FtpFirstSubDirectoryName.Split(new string[] { "^" }, StringSplitOptions.RemoveEmptyEntries), out errorMsg);
                    if (!string.IsNullOrWhiteSpace(errorMsg))
                    {
                        MediMsgBox.Warn(errorMsg);
                        Application.Exit();
                        return;
                    }
                }

                OperateIniFile tempoperateIniFile = new OperateIniFile(rootPathInfo + "\\Temp\\version.ini");
                OperateIniFile operateIniFile = new OperateIniFile(rootPathInfo + "\\version.ini");
                string tempVersionNo = tempoperateIniFile.ReadString("version", "version", string.Empty);
                string versionNo = operateIniFile.ReadString("version", "version", string.Empty);

                if (!tempVersionNo.Equals(versionNo))
                {
                    if (File.Exists(rootPathInfo + "\\Temp\\version.ini"))
                        File.Copy(rootPathInfo + "\\Temp\\version.ini", rootPathInfo + "\\version.ini", true);

                    DownLoadUpadteExe(rootPathInfo, bgWorker, bgWorkerState, e);

                    //Application.Exit();
                }
                else if (directorylist != null)
                {
                    if (directorylist.Count > 0)
                    {
                        DownLoadFile(rootPathInfo, bgWorker, bgWorkerState, e);

                        // Application.Exit();
                    }
                    else
                    {
                        if (bgWorker.CancellationPending)
                        {
                            bgWorkerState.State = -1;
                            bgWorkerState.ErrorMsg = string.Empty;
                            bgWorkerState.Tips = string.Empty;
                            bgWorkerState.Exit = true;
                            e.Result = bgWorkerState;
                            return;
                        }

                        bgWorker.ReportProgress(100, "SUCCESS");

                        bgWorkerState.State = 0;
                        bgWorkerState.ErrorMsg = string.Empty;
                        bgWorkerState.Tips = string.Empty;
                        bgWorkerState.Exit = false;
                        e.Result = bgWorkerState;
                    }
                }
                else
                {
                    if (bgWorker.CancellationPending)
                    {
                        bgWorkerState.State = -1;
                        bgWorkerState.ErrorMsg = string.Empty;
                        bgWorkerState.Tips = string.Empty;
                        bgWorkerState.Exit = true;
                        e.Result = bgWorkerState;
                        return;
                    }

                    bgWorker.ReportProgress(100, "SUCCESS");

                    bgWorkerState.State = 0;
                    bgWorkerState.ErrorMsg = string.Empty;
                    bgWorkerState.Tips = string.Empty;
                    bgWorkerState.Exit = false;
                    e.Result = bgWorkerState;
                }
            }
        }

        private void DownLoadFile(string rootPath, BackgroundWorker bgWorker, BackgroundWokerState bgWorkerState, DoWorkEventArgs e)
        {
            try
            {
                if (!File.Exists(rootPath + "\\" + UpdateHelper.UpdateExeName + ".exe"))
                {
                    if (UpdateHelper.GetFileNoBinary("\\UPDATE", "" + UpdateHelper.UpdateExeName + ".zip", rootPath, "" + UpdateHelper.UpdateExeName + ".zip"))
                    {
                        bool isUpdatedStarted = false;
                        ProcessHelper.KillProcess(UpdateHelper.UpdateExeName);
                        ZipCommon.UnZip(rootPath + "\\" + UpdateHelper.UpdateExeName + ".zip", rootPath, ref isUpdatedStarted);
                        if (isUpdatedStarted)
                        {
                            MediMsgBox.Warn("更新程序已启动,请等待更新完成...");
                            Application.Exit();
                        }
                    }
                }

                // 获取启动程序的上一级文件目录
                DirectoryInfo startPathInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                String rootPathInfo = startPathInfo.Parent.FullName;    // 上一级的目录
                OperateIniFile operateIniFile = new OperateIniFile(rootPathInfo + "\\" + "version.ini");
                operateIniFile.WriteString("version", "UpdateOK", "1");
                KillUpdateProcess(rootPath);
                //启动监控服务
                if (!string.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["StartMonitor"]) && System.Configuration.ConfigurationManager.AppSettings["StartMonitor"].ToUpper() == "Y")
                {
                    WindowsServiceHelper.InstallService("Mediinfo.WinForm.HIS.Monitor");
                    WindowsServiceHelper.StartService("Mediinfo.WinForm.HIS.Monitor");
                }
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = "" + UpdateHelper.UpdateExeName + ".exe";
                processStartInfo.WorkingDirectory = rootPathInfo;
                processStartInfo.Verb = "runas";
                processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
                //processStartInfo.UseShellExecute = false;

                // 处理XP系统报错问题
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    this.Hide();
                    Process.Start(processStartInfo).WaitForExit();
                }
                else
                    WinApiHelper.ShellExecute(0, "open", processStartInfo.FileName, null, processStartInfo.WorkingDirectory, 11);
            }
            catch (Exception ex)
            {
                // 获取启动程序的上一级文件目录
                DirectoryInfo startPathInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                String rootPathInfo = startPathInfo.Parent.FullName;    // 上一级的目录
                OperateIniFile operateIniFile = new OperateIniFile(rootPathInfo + "\\" + "version.ini");
                operateIniFile.WriteString("version", "UpdateOK", "0");
                File.Delete(rootPathInfo + "\\Temp\\version.ini");
                //File.Delete(rootPathInfo + "\\HIS6Version.ini");

                LogHelper.Default.Error("获取更新程序失败!", ex);
                if (bgWorker.CancellationPending)
                {
                    bgWorkerState.State = -1;
                    bgWorkerState.ErrorMsg = string.Empty;
                    bgWorkerState.Tips = string.Empty;
                    bgWorkerState.Exit = true;
                    e.Result = bgWorkerState;
                    return;
                }

                bgWorker.ReportProgress(100, "SUCCESS");

                bgWorkerState.State = 0;
                bgWorkerState.ErrorMsg = string.Empty;
                bgWorkerState.Tips = string.Empty;
                bgWorkerState.Exit = false;
                e.Result = bgWorkerState;
                return;
            }
        }

        private void DownLoadUpadteExe(string rootPath, BackgroundWorker bgWorker, BackgroundWokerState bgWorkerState, DoWorkEventArgs e)
        {
            try
            {
                if (!File.Exists(rootPath + "\\" + UpdateHelper.UpdateExeName + ".exe"))
                {
                    if (UpdateHelper.GetFileNoBinary("\\UPDATE", "" + UpdateHelper.UpdateExeName + ".zip", rootPath, "" + UpdateHelper.UpdateExeName + ".zip"))
                    {
                        bool isUpdatedStarted = false;
                        ProcessHelper.KillProcess(UpdateHelper.UpdateExeName);
                        ZipCommon.UnZip(rootPath + "\\" + UpdateHelper.UpdateExeName + ".zip", rootPath, ref isUpdatedStarted);
                        if (isUpdatedStarted)
                        {
                            MediMsgBox.Warn("更新程序已启动,请等待更新完成...");
                            Application.Exit();
                        }
                    }
                }
                else
                {
                    if (UpdateHelper.GetFileNoBinary("\\UPDATE", "" + UpdateHelper.UpdateExeName + ".zip", rootPath, "" + UpdateHelper.UpdateExeName + ".zip"))
                    {
                        bool isUpdatedStarted = false;
                        ProcessHelper.KillProcess(UpdateHelper.UpdateExeName);
                        File.Delete(rootPath + "\\" + UpdateHelper.UpdateExeName);
                        ZipCommon.UnZip(rootPath + "\\" + UpdateHelper.UpdateExeName + ".zip", rootPath, ref isUpdatedStarted);
                        if (isUpdatedStarted)
                        {
                            MediMsgBox.Warn("更新程序已启动,请等待更新完成...");
                            Application.Exit();
                        }
                    }
                }

                // 获取启动程序的上一级文件目录
                DirectoryInfo startPathInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                String rootPathInfo = startPathInfo.Parent.FullName; //上一级的目录
                OperateIniFile operateIniFile = new OperateIniFile(rootPathInfo + "\\" + "version.ini");
                operateIniFile.WriteString("version", "UpdateOK", "1");
                KillUpdateProcess(rootPath);
                //启动监控服务
                if (!string.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["StartMonitor"]) && System.Configuration.ConfigurationManager.AppSettings["StartMonitor"].ToUpper() == "Y")
                {
                    WindowsServiceHelper.InstallService("Mediinfo.WinForm.HIS.Monitor");
                    WindowsServiceHelper.StartService("Mediinfo.WinForm.HIS.Monitor");
                }
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = "" + UpdateHelper.UpdateExeName + ".exe";
                processStartInfo.WorkingDirectory = rootPathInfo;
                processStartInfo.Verb = "runas";
                processStartInfo.WindowStyle = ProcessWindowStyle.Normal;

                // 处理XP系统报错问题
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    this.Hide();
                    Process.Start(processStartInfo).WaitForExit();
                }
                else
                    WinApiHelper.ShellExecute(0, "open", processStartInfo.FileName, null, processStartInfo.WorkingDirectory, 11);
            }
            catch (Exception ex)
            {
                // 获取启动程序的上一级文件目录
                DirectoryInfo startPathInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                String rootPathInfo = startPathInfo.Parent.FullName;    // 上一级的目录
                OperateIniFile operateIniFile = new OperateIniFile(rootPathInfo + "\\" + "version.ini");
                operateIniFile.WriteString("version", "UpdateOK", "0");
                File.Delete(rootPathInfo + "\\Temp\\version.ini");
                //File.Delete(rootPathInfo + "\\HIS6Version.ini");

                LogHelper.Default.Error("获取更新程序失败!", ex);
                if (bgWorker.CancellationPending)
                {
                    bgWorkerState.State = -1;
                    bgWorkerState.ErrorMsg = string.Empty;
                    bgWorkerState.Tips = string.Empty;
                    bgWorkerState.Exit = true;
                    e.Result = bgWorkerState;
                    return;
                }

                bgWorker.ReportProgress(100, "SUCCESS");

                bgWorkerState.State = 0;
                bgWorkerState.ErrorMsg = string.Empty;
                bgWorkerState.Tips = string.Empty;
                bgWorkerState.Exit = false;
                e.Result = bgWorkerState;
                return;
            }
        }

        /// <summary>
        /// 是否显示阴影
        /// </summary>
        /// <returns></returns>
        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                WinApiHelper.DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1);
            }
            else
            {
                this.ShowBorder = true;
            }
            return false;
        }

        /// <summary>
        /// 加载本机的IP地址
        /// </summary>
        private void LoadIPAddress()
        {
            // 获取可用网卡信息
            networkList = NetworkHeler.GetAvailableNetwork();
            // 判断是否获取到本机的IP地址列表
            if (networkList != null && networkList.Count > 0)
            {
                // 去网卡类型是以太网的类型(包含网线、虚拟网络)，如果网卡名称是以太网则默认使用的是网线
                var model = networkList.Where(p => p.NetworkInterfaceType == NetworkInterfaceType.Ethernet && p.Name.Contains("以太网") && !String.IsNullOrWhiteSpace(p.Ip));
                // 判断是否使用的是以太网，如果是则使用该IP，如果不是则默认使用列表中第一个IP
                if (model == null)
                    this.lblIP.Text = "本机：" + networkList.FirstOrDefault().Ip;
                else
                {
                    var firstIp = model.FirstOrDefault();
                    if (firstIp != null)
                        this.lblIP.Text = "本机：" + firstIp.Ip;
                    else
                        this.lblIP.Text = "本机：" + networkList.FirstOrDefault().Ip;
                }
            }
            else
            {
                this.lblIP.Text = "";
            }
        }

        /// <summary>
        /// 加载版本号
        /// </summary>
        private void LoadVersion()
        {
            // 本地客户端版本号
            string version = MediinfoConfig.Read(Application.StartupPath + "\\version.ini", "version", "version");
            if (String.IsNullOrWhiteSpace(version))
                this.lblVersion.Text = "版本号：V6.1";
            else
                this.lblVersion.Text = "版本号：" + version;
        }

        #endregion

        #region override

        /// <summary>
        /// 鼠标按下时触发事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            // 设置按下鼠标左键时可以拖动窗口
            if (e.Button == MouseButtons.Left)
            {
                Capture = false;
                WinApiHelper.SendMessage(this.Handle, WinApiHelper.WM_NCLBUTTONDOWN, WinApiHelper.HTCAPTION, 0);
            }

            base.OnMouseDown(e);
        }

        /// <summary>
        /// 重绘事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this.ShowBorder)
            {
                Color color = (Form.ActiveForm == this) ? this.ActiveBorderColor : this.BorderColor;
                using (SolidBrush brush = new SolidBrush(color))
                {
                    e.Graphics.FillRectangle(brush, 440, 0, 440, 1);
                    e.Graphics.FillRectangle(brush, this.Width - 1, 0, 1, this.Height);
                    e.Graphics.FillRectangle(brush, 440, this.Height - 1, this.Width, 1);
                }
            }

            // 重绘登录和退出按钮
            PaintControl(this.mediButtonDengLu);
            PaintControl(this.mediButtonExit);
        }

        /// <summary>
        /// 处理Windows消息。
        /// </summary>
        /// <param name="m">Windows消息要处理。</param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0085:       // 绘制窗空框架(窗口四边阴影)
                    if (aeroEnabled)
                    {
                        int attrValue = 2;
                        WinApiHelper.DwmSetWindowAttribute(this.Handle, 0x02, ref attrValue, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 1,
                            rightWidth = 1,
                            topHeight = 1
                        };
                        WinApiHelper.DwmExtendFrameIntoClientArea(this.Handle, ref margins);
                    }
                    break;
                case 0x0201:   // 鼠标左键按下的消息
                    m.Msg = 0x00A1;                 // 更改消息为非客户区按下鼠标
                    m.LParam = IntPtr.Zero;         // 默认值
                    m.WParam = new IntPtr(2);       // 鼠标放在标题栏内
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        #endregion

        #region events

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DengLu_Load(object sender, EventArgs e)
        {
            isSm = false;
            this.picEdit_ErWm.Visible = true;

            if (!string.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["StartMonitor"]) && System.Configuration.ConfigurationManager.AppSettings["StartMonitor"].ToUpper() == "Y")
            {
                WindowsServiceHelper.InstallService("Mediinfo.WinForm.HIS.Monitor");
                WindowsServiceHelper.StartService("Mediinfo.WinForm.HIS.Monitor");
            }
            // 加载本机IP地址
            LoadIPAddress();
            // 加载版本号
            LoadVersion();
            // 版权信息
            this.lblCopyRight.Text = String.Format("Copyright © 1999~{0} 联众智慧科技股份有限公司 版权所有", DateTime.Now.Year);

            this.progressPanelTips.Show();

            if (string.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["IsDevelopmentMode"]))
            {
                if (!string.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["IsUpdateMode"]) && System.Configuration.ConfigurationManager.AppSettings["FileFolder"].ToUpper() == "UPDATE")
                {
                    // 是否是HTTP更新 以及更新update.exe
                    HISGlobalSetting.UpdateDirectorys = System.Configuration.ConfigurationManager.AppSettings["FileFolder"];
                    HISGlobalSetting.IsHttp = true;
                }
            }

            // 异步处理初始化工作
            this.backgroundWorker_XiTong.RunWorkerAsync();
        }

        /// <summary>
        /// 登陆点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonDengLu_Click(object sender, EventArgs e)
        {
            //HISClientHelper.ClientSettingNew.SetConfigItemValue();
            //hISClientHelper.ClientSettingNew.SetConfigItemValue()

            if (!IsClickDengLuButton)
            {
                IsClickDengLuButton = true;
                try
                {
                    if ((!string.IsNullOrWhiteSpace(CAFactory.GetCanshuById("公用_是否强制扫码登录"))) && CAFactory.GetCanshuById("公用_是否强制扫码登录") == "1" && !isSm)
                    {
                        MediMsgBox.Failure("请选择扫码登录");
                        return;
                    }

                    if (Application.StartupPath.Contains(" "))
                    {
                        MediMsgBox.Failure("程序运行目录不允许包含空格" + Environment.NewLine + $"运行目录【{Application.StartupPath}】");
                        return;
                    }

                    string userId = "";

                    this.progressPanelTips.Show();
                    this.progressPanelTips.Description = "正在登录...";
                    progressPanelTips.Update();

                    if (this.textBox_YongHuMing.EditValue.IsNullOrWhiteSpace())
                    {
                        MediMsgBox.Failure(this, "请输入工号！");
                        this.textBox_YongHuMing.Focus();
                        return;
                    }
                    else
                    {
                        userId = this.textBox_YongHuMing.EditValue.ToString();
                    }

                    if (this.textBox_MiMa.EditValue.IsNullOrWhiteSpace() && !isSm)
                    {
                        MediMsgBox.Failure(this, "请输入密码！");
                        this.textBox_MiMa.Focus();
                        return;
                    }

                    if (this.gridLookUpEdit1.EditValue.IsNullOrWhiteSpace())
                    {
                        MediMsgBox.Failure(this, "请选择应用！");
                        this.gridLookUpEdit1.Focus();
                        return;
                    }
                    if (!this.gridLookUpEdit1.EditValue.IsNullOrWhiteSpace())
                    {
                        var yingYong = loginDTO.YingYongList.Where(c => c.YINGYONGID == this.gridLookUpEdit1.EditValue.ToString()).FirstOrDefault();
                        // 应用信息
                        HISClientHelper.YINGYONGID = yingYong.YINGYONGID; //应用ID
                        HISClientHelper.KUCUNYYID = yingYong.YINGYONGID;
                        HISClientHelper.YINGYONGMC = yingYong.YINGYONGMC; //应用名称
                        HISClientHelper.YINGYONGJC = yingYong.YINGYONGJC; //应用简称
                        HISClientHelper.XITONGID = yingYong.XITONGID;     //系统ID
                        HISClientHelper.YINGWENJC = yingYong.YINGWENJC;     //英文简称jixianid

                    }

                    HISClientHelper.USERID = loginDTO.ZhiGongXX.ZHIGONGID;    //职工ID
                    HISClientHelper.ZHIGONGGH = loginDTO.ZhiGongXX.ZHIGONGGH; //职工工号
                    HISClientHelper.USERNAME = loginDTO.ZhiGongXX.ZHIGONGXM;  //职工姓名

                    // 记录上次登录的信息
                    HISClientHelper.ClientSetting.LastLoginInfo.ZhiGongID = HISClientHelper.USERID;
                    HISClientHelper.ClientSetting.LastLoginInfo.ZhiGongXM = HISClientHelper.USERNAME;
                    HISClientHelper.ClientSetting.LastLoginInfo.ZhiGongGH = HISClientHelper.ZHIGONGGH;
                    HISClientHelper.ClientSetting.LastLoginInfo.YingYongID = HISClientHelper.YINGYONGID;
                    HISClientHelper.ClientSetting.LastLoginInfo.YingYongMC = HISClientHelper.YINGYONGMC;
                    string miMa = this.textBox_MiMa.EditValue.ToString();
                    //if (GYCanShuHelper.GetCanShu("公用_用户密码是否加密", "0") != "0")
                    //    miMa = SHA256.Encrypt(miMa);
                    // 日志级别控制
                    string zdyRiZhi = GYCanShuHelper.GetCanShu("自定义日志_IP段", "127.0.0.1-127.0.0.1：0");
                    string xtRiZhi = GYCanShuHelper.GetCanShu("系统日志_IP段", "127.0.0.1-127.0.0.1：0");
                    if (!string.IsNullOrEmpty(zdyRiZhi) || !string.IsNullOrEmpty(xtRiZhi))
                    {
                        Mediinfo.Enterprise.Log.LogHelper.InitialRiZhiKZ(zdyRiZhi, xtRiZhi, networkList[0].Ip);
                    }
                    this.progressPanelTips.Description = "正在验证用户名密码...";

                    dynamic result;
                    if (isSm)//扫码登录 不需要密码
                    {
                        result = loginService.GetYongHuXByGH(loginDTO.ZhiGongXX.ZHIGONGID, networkList);
                        if (result.ReturnCode == ReturnCode.SUCCESS)
                        {
                            miMa = result.Return.YongHuXX.MIMA;
                            string xiTongPath = Path.Combine(Application.StartupPath, HISClientHelper.YINGYONGID.SubString(0, 2));
                            HISClientHelper.ClientSettingNew = HISClientSetting.LoadNew(xiTongPath);
                            HISClientHelper.ClientSettingNew.SetConfigItemValue("CASM", "ISSM", "1");
                            HISClientHelper.ClientSettingNew.SetConfigItemValue("CASM", "DHHM", CAFactory.DHHM);
                            HISClientHelper.ClientSettingNew.SetConfigItemValue("CASM", "ZGGH", CAFactory.ZGGH);
                            HISClientHelper.ClientSettingNew.SetConfigItemValue("CASM", "SFZH", CAFactory.SFZH);
                            HISClientHelper.ClientSettingNew.Save();
                            Enterprise.Log.ClientLogHelper.Intance.WriteLog("扫码登录程序：" + "密码：" + miMa);
                        }
                        else
                        {
                            MediMsgBox.Failure(this, "CA验证密码失败！");
                            return;
                        }
                            
                    }
                    else
                    {
                        result = loginService.Login(loginDTO.ZhiGongXX.ZHIGONGID, miMa, this.gridLookUpEdit1.EditValue.ToString(), networkList);
                        if (result.ReturnCode != ReturnCode.SUCCESS)
                        {
                            MediMsgBox.Failure(this, result.ReturnMessage);
                            this.textBox_MiMa.Text = string.Empty;
                            this.textBox_MiMa.Focus();
                            return;
                        }
                    }

                    // 登录成功之后保存登录信息
                    HISClientHelper.ClientSetting.Save();
                    HISClientHelper.GlobalSetting.Save();

                    #region 检测所选应用是否登录
                    //add by 余佳平
                    string tongYiYYDL = GYCanShuHelper.GetCanShu(HISClientHelper.YINGYONGID, "公用_同一应用不同用户允许登陆", "0");

                    if (tongYiYYDL.ToStringEx() == "0")
                    {
                        List<string> yingYongIdList = MemoryMappedFileHelper.GetClipBoardData();

                        Process[] processes = Process.GetProcessesByName("Mediinfo.WinForm.HIS.Main");
                        List<string> processIds = new List<string>();
                        foreach (Process item in processes)
                            processIds.Add(item.Id.ToString());
                        foreach (string yingyongids in yingYongIdList)
                        {
                            string[] yingyongid = yingyongids.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                            if (yingyongid.Length > 1 && !processIds.Contains(yingyongid[1]))
                                MemoryMappedFileHelper.RemoveClipBoardData(yingyongid[0], yingyongid[1]);
                        }
                        yingYongIdList = MemoryMappedFileHelper.GetClipBoardData();
                        // 定义键值对存储当前已打开应用
                        Dictionary<string, string> dicYingYongIDList = new Dictionary<string, string>();

                        foreach (string yingyongids in yingYongIdList)
                        {
                            string[] yingyongid = yingyongids.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                            if (yingyongid.Length > 1)
                            {
                                dicYingYongIDList.Add(yingyongid[0], yingyongid[1]);
                            }
                        }
                        if (dicYingYongIDList.Count > 0)
                        {
                            if (dicYingYongIDList.ContainsKey(HISClientHelper.YINGYONGID))
                            {
                                if (MediMsgBox.YesNo(this, String.Format("{0}应用已登录，是否关闭其他已登录的程序？", HISClientHelper.YINGYONGID), MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                                    return;

                                // 关闭已打开的程序
                                foreach (string key in dicYingYongIDList.Keys)
                                {
                                    // 强制关闭应用ID与当前登录应用ID一样的进程
                                    if (String.Compare(key, HISClientHelper.YINGYONGID, true) == 0)
                                    {
                                        int processID = Convert.ToInt32(dicYingYongIDList[key]);
                                        KillProcess(processID);
                                        MemoryMappedFileHelper.RemoveClipBoardData(HISClientHelper.YINGYONGID, processID.ToStringEx());
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    #region 检查更新子系统
                    if (string.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["IsDevelopmentMode"]))
                    {
                        if (System.Configuration.ConfigurationManager.AppSettings["IsUpdateMode"].ToUpper() != "HTTP")
                        {
                            //需要更新的子系统ftp文件夹路径
                            string ftpPath = $"AssemblyClient/{ HISClientHelper.YINGYONGID.SubString(0, 2)}";
                            string errorMsg = string.Empty;
                            //需要更新的文件
                            var rootPath = Directory.GetParent(Application.StartupPath).FullName;
                            var directorylist = UpdateDirectory.GetUpdateDirectories(rootPath, new string[] { ftpPath }, out errorMsg);
                            if (!string.IsNullOrWhiteSpace(errorMsg))
                            {
                                MediMsgBox.Warn(errorMsg);
                                Application.Exit();
                                return;
                            }
                            if (directorylist.Count > 0)
                            {
                                //关闭选中系统所登录的进程，否则会因为文件被进程占用导致无法更新
                                KillYingYongJC(Path.Combine(Application.StartupPath, HISClientHelper.YINGYONGID.SubString(0, 2)));

                                //启动更新程序
                                KillUpdateProcess(rootPath);
                                //启动监控服务
                                if (!string.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["StartMonitor"]) && System.Configuration.ConfigurationManager.AppSettings["StartMonitor"].ToUpper() == "Y")
                                {
                                    WindowsServiceHelper.InstallService("Mediinfo.WinForm.HIS.Monitor");
                                    WindowsServiceHelper.StartService("Mediinfo.WinForm.HIS.Monitor");
                                }
                                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                                processStartInfo.FileName = UpdateHelper.UpdateExeName + ".exe";
                                processStartInfo.WorkingDirectory = rootPath;
                                processStartInfo.Verb = "runas";
                                processStartInfo.WindowStyle = ProcessWindowStyle.Normal;

                                //ftp下载传参:登录信息,子系统ftp文件夹路径,子系统本地文件夹路径，下载方式为ftp
                                //登录信息
                                string dengLuXX = String.Format("/Y|{0}|/U|{1}|/P|{2}", HISClientHelper.YINGYONGID, loginDTO.ZhiGongXX.ZHIGONGID, miMa);
                                //子系统本地文件夹路径
                                string xiTongPath = Path.Combine(Application.StartupPath, HISClientHelper.YINGYONGID.SubString(0, 2));
                                processStartInfo.Arguments = dengLuXX + " " + ftpPath + " " + xiTongPath + " " + "FTP";

                                // 处理XP系统报错问题
                                if (Environment.OSVersion.Version.Major >= 6)
                                {
                                    this.Hide();
                                    Process.Start(processStartInfo).WaitForExit();
                                }
                                else
                                    WinApiHelper.ShellExecute(0, "open", processStartInfo.FileName, processStartInfo.Arguments, processStartInfo.WorkingDirectory, 11);

                                Exit();
                            }
                        }
                        else
                        {
                            var rootpath = Directory.GetParent(Application.StartupPath).FullName;
                            HISGlobalSetting.zxt = HISClientHelper.YINGYONGID.SubString(0, 2);
                            bool needUpdate_zxt = CheckUpdate_ZXT(rootpath + "\\AssemblyClient", HISGlobalSetting.zxt);

                            if (isStartUpdate || needUpdate_zxt)
                            {
                                string args = String.Format(" /Y {0} /U {1} /P {2}", HISClientHelper.YINGYONGID.SubString(0, 2), loginDTO.ZhiGongXX.ZHIGONGID, miMa);
                                var rootP = Directory.GetParent(Application.StartupPath).FullName;
                                // 系统所在文件夹
                                KillUpdateProcess(rootP);
                                //启动监控服务
                                if (!string.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["StartMonitor"]) && System.Configuration.ConfigurationManager.AppSettings["StartMonitor"].ToUpper() == "Y")
                                {
                                    WindowsServiceHelper.InstallService("Mediinfo.WinForm.HIS.Monitor");
                                    WindowsServiceHelper.StartService("Mediinfo.WinForm.HIS.Monitor");
                                }
                                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                                processStartInfo.FileName = UpdateHelper.UpdateExeName + ".exe";
                                processStartInfo.WorkingDirectory = rootP + "\\";
                                processStartInfo.Verb = "runas";
                                processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
                                processStartInfo.Arguments = args + " /M HTTP";

                                processStartInfo.UseShellExecute = true;
                                processStartInfo.CreateNoWindow = true;
                                // 处理XP系统报错问题
                                if (Environment.OSVersion.Version.Major >= 6)
                                {
                                    this.Hide();
                                    Process.Start(processStartInfo).WaitForExit();
                                }
                                else
                                    WinApiHelper.ShellExecute(0, "open", processStartInfo.FileName, args + " /M HTTP", processStartInfo.WorkingDirectory, 11);

                                Exit();
                            }
                        }
                    }
                    #endregion

                    #region 更新版本号
                    //更新GY_CAOZUORZ表的版本号字段
                    //var ip = result.Return.IP??"127.0.0.1";
                    //var banbenhao = LoadZXTVersion();
                    //loginService.LoginBanBenHao(ip, HISClientHelper.YINGYONGID, banbenhao);
                    Task.Factory.StartNew(() =>
                    {
                        var ip = result.Return.IP ?? "127.0.0.1";
                        var banbenhao = LoadZXTVersion();
                        loginService.LoginBanBenHao(ip, HISClientHelper.YINGYONGID, banbenhao);
                    });
                    #endregion

                    // 系统所在文件夹
                    string folder = Path.Combine(Application.StartupPath, HISClientHelper.YINGYONGID.SubString(0, 2));
                    if (!Directory.Exists(folder))
                        folder = Application.StartupPath;
                 
                    string fileName = String.Format("{0}\\Mediinfo.WinForm.HIS.Main.exe", folder);
                    string arguments = String.Format(" /Y {0} /U {1} /P {2}", HISClientHelper.YINGYONGID, loginDTO.ZhiGongXX.ZHIGONGID, miMa);
                    //if (isSm)//扫码登录
                    //    arguments = String.Format("{0} {1} {2}", HISClientHelper.YINGYONGID, loginDTO.ZhiGongXX.ZHIGONGID, miMa);
                    //else
                    //    arguments = String.Format(" /Y {0} /U {1} /P {2}", HISClientHelper.YINGYONGID, loginDTO.ZhiGongXX.ZHIGONGID, miMa);

                    //Enterprise.Log.ClientLogHelper.Intance.WriteLog("登录程序方式：" + (isSm ? "扫码" : "正常") + "入参：" + arguments);
                    
                    // 判断当前系统是否为Windows 7之后的系统
                    if (Environment.OSVersion.Version.Major >= 6)
                        Process.Start(fileName, arguments);
                    else
                        WinApiHelper.ShellExecute(0, "open", "Mediinfo.WinForm.HIS.Main.exe", arguments, folder, 11);
                }
                finally
                {
                    this.progressPanelTips.Hide();
                    this.mediButtonDengLu.Enabled = true;
                    this.mediButtonExit.Enabled = true;
                    IsClickDengLuButton = false;
                }

                // 退出当前程序
                Exit();
            }
        }
        /// <summary>
        /// 加载版本号
        /// </summary>
        private string LoadZXTVersion()
        {
            // 本地客户端版本号
            string version = MediinfoConfig.Read(Application.StartupPath + "\\" + HISClientHelper.YINGYONGID.SubString(0, 2) + "\\version.ini", "version", "version");
            if (String.IsNullOrWhiteSpace(version))
                return "V6.1";
            else
                return version;
        }
        /// <summary>
        /// 是否需要更新子系统
        /// </summary>
        /// <param name="rootpath"></param>
        /// <param name="zxt"></param>
        /// <returns true需要更新,false不需要更新></returns>
        private bool CheckUpdate_ZXT(string rootpath, string zxt)
        {
            //先判断目录是否存在
            if (!Directory.Exists(Path.Combine(rootpath, zxt))) return true;
            //判断文件是否存在
            string filePath = Path.Combine(rootpath, zxt, "Mediinfo.WinForm.HIS.Main.exe");
            if (File.Exists(filePath))
            {
                //判断配置文件是否存在
                if (!File.Exists(Path.Combine(rootpath, zxt, "HISGlobalSettingHttp.xml"))) return true;
                //读取本地配置文件
                List<HTTPUpdateConfig> httpconfigs = HISGlobalSetting.LoadZxtInfos(Path.Combine(rootpath, zxt, "HISGlobalSettingHttp.xml"));
                int count = 0;
                List<HTTPUpdateConfig> Needupdatedconfigs = new List<HTTPUpdateConfig>();
                if (httpconfigs != null && httpconfigs.Count != 0)
                {
                    DirectoryInfo startPathInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                    string httpAddress = startPathInfo.Parent.FullName + "\\DownLoadAddress.xml";
                    if (!File.Exists(httpAddress)) return false;
                    foreach (HTTPUpdateConfig item in httpconfigs)
                    {
                        string severUrl = MediinfoConfig.GetXmlNodeValue(httpAddress, "ipAddress");
                        string returnMessage = HTTPHelper.DownloadConfigFiles(severUrl, item.BanBenHao, item.JIXIANMC); //最新版本号
                        if (!string.IsNullOrEmpty(returnMessage))
                        {
                            if (returnMessage.Equals(item.BanBenHao))
                            {
                                // 版本号没有更新
                                count++;
                                continue;
                            }
                        }
                    }
                    if (count == httpconfigs.Count)
                    {
                        //不需要下载更新
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {  //不存在需要更新
                return true;
            }
        }

        /// <summary>
        /// 用户名验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_YongHuMing_Validating(object sender, CancelEventArgs e)
        {
            //string dirPath = AppDomain.CurrentDomain.BaseDirectory + "ServiceClient";
            // 判断退出按钮时不做用户名验证
            Point pt = PointToClient(MousePosition);
            if (rectangle.Contains(pt))
            {
                mediButtonExit_Click(null, null);
                return;
            }

            if (this.textBox_YongHuMing.EditValue == this.textBox_YongHuMing.OldEditValue ||
                this.textBox_YongHuMing.EditValue.IsNullOrWhiteSpace())
            {
                return;
            }

            if (this.LoadFromLastLoginInfo && loginDTO != null)
            {
                // 重置密码
                if (string.IsNullOrWhiteSpace(loginDTO.YongHuXX.MIMA))
                {
                    ResetPasswordForm frm = new ResetPasswordForm();
                    frm.ZhiGongID = this.textBox_YongHuMing.EditValue.ToString();
                    DialogResult result = frm.ShowDialog();
                    //if (result == DialogResult.Cancel)
                    //{
                    //    MediMsgBox.Failure(this, "重置密码不成功，您无法使用系统！");
                    //    return;
                    //}
                    frm.Dispose();
                }

                this.textBox_YongHuMing.Text = loginDTO.ZhiGongXX.ZHIGONGXM;
                CAFactory.ZGGH = loginDTO.ZhiGongXX.ZHIGONGGH;
                CAFactory.SFZH = loginDTO.ZhiGongXX.SHENFENZH;
                CAFactory.DHHM = loginDTO.ZhiGongXX.DIANHUA;
                this.eGYYINGYONGBindingSource.DataSource = loginDTO.YingYongList;
                this.eGYYINGYONGBindingSource.ResetBindings(false);
                this.mediButtonDengLu.Enabled = true;

                var lastYingYongID = loginDTO.YingYongList.Where(c => c.YINGYONGID == HISClientHelper.ClientSetting.LastLoginInfo.YingYongID).FirstOrDefault();
                if (null != lastYingYongID && !ConfigurationManager.AppSettings["RememberYingYong"].Equals("N"))
                    this.gridLookUpEdit1.EditValue = lastYingYongID.YINGYONGID;
            }
            else
            {
                var result = loginService.GetYongHuXByGH(this.textBox_YongHuMing.EditValue.ToString(), networkList);

                if (result.ReturnCode != ReturnCode.SUCCESS)
                {
                    //MediMsgBox.Failure(this, "服务端连接异常!", result.ReturnCode.ToString(), result.ReturnMessage, false);
                    MediMsgBox.FloatMsg(this, "", result.ReturnMessage, "2");
                    this.textBox_MiMa.EditValue = DBNull.Value;
                    this.eGYYINGYONGBindingSource.DataSource = null;

                    e.Cancel = true;
                }
                else
                {
                    if (result.Return.YingYongList.Count <= 0)
                    {
                        this.mediButtonDengLu.Enabled = false;
                        MediMsgBox.Failure(this, "无权限登录该系统，请联系管理员!");
                        this.textBox_YongHuMing.Focus();
                        this.textBox_YongHuMing.SelectAll();
                        return;
                    }
                    else
                    {
                        // 重置密码
                        if (string.IsNullOrWhiteSpace(result.Return.YongHuXX.MIMA))
                        {
                            using (ResetPasswordForm frm = new ResetPasswordForm())
                            {
                                frm.ZhiGongID = this.textBox_YongHuMing.EditValue.ToString();
                                DialogResult ret = frm.ShowDialog();
                                if (ret == DialogResult.Cancel)
                                {
                                    MediMsgBox.Failure(this, "重置密码不成功，您无法使用系统！");
                                    return;
                                }
                            }
                        }

                        loginDTO = result.Return;
                        CAFactory.ZGGH = result.Return.ZhiGongXX.ZHIGONGGH;
                        CAFactory.SFZH = result.Return.ZhiGongXX.SHENFENZH;
                        CAFactory.DHHM = result.Return.ZhiGongXX.DIANHUA;
                        this.textBox_YongHuMing.Text = result.Return.ZhiGongXX.ZHIGONGXM;
                        if (HISClientHelper.IsSwitchUser)
                        {
                            var yingYongList1 = result.Return.YingYongList.Where(o => o.YINGYONGID == HISClientHelper.PreviewApp).ToList();
                            if (yingYongList1.Count <= 0)
                            {
                                this.mediButtonDengLu.Enabled = false;
                                MediMsgBox.FloatMsg(this, "", "您无权限登录该应用，请联系管理员!", "3");
                                this.textBox_YongHuMing.Text = null;
                                this.textBox_MiMa.Text = null;
                                this.textBox_YongHuMing.Focus();
                                this.textBox_YongHuMing.SelectAll();
                                return;
                            }
                            this.gridLookUpEdit1.Properties.SetBindConfig(yingYongList1, "YINGYONGID", "YINGYONGMC");
                            this.gridLookUpEdit1.EditValue = yingYongList1[0].YINGYONGID;
                        }
                        else
                        {
                            var yingYongList = result.Return.YingYongList;
                            //如果有可登录应用配置，则以配置为准
                            if (!string.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["YingYong"]))
                            {
                                var yingYongIDs = System.Configuration.ConfigurationManager.AppSettings["YingYong"].Split(',').ToList();
                                yingYongList = yingYongList.FindAll(y => yingYongIDs.Contains(y.YINGYONGID));
                            }
                            this.eGYYINGYONGBindingSource.DataSource = yingYongList;
                            this.eGYYINGYONGBindingSource.ResetBindings(false);

                            if (yingYongList.Count > 0)
                            {
                                if (!ConfigurationManager.AppSettings["RememberYingYong"].Equals("N") &&
                                    yingYongList.Any(y => y.YINGYONGID.Equals(HISClientHelper.ClientSetting.LastLoginInfo.YingYongID)))
                                {
                                    this.gridLookUpEdit1.EditValue = HISClientHelper.ClientSetting.LastLoginInfo.YingYongID;
                                }
                                else
                                {
                                    this.gridLookUpEdit1.EditValue = yingYongList[0].YINGYONGID;
                                }
                            }
                        }

                        this.mediButtonDengLu.Enabled = true;
                    }
                }
            }

            //[HB6-11388] 系统登录界面，输入工号后，登录系统没有选项
            if (this.eGYYINGYONGBindingSource.Count == 0 && loginDTO?.YingYongList.Count > 0)
            {
                var yingYongList = loginDTO.YingYongList;
                //如果有可登录应用配置，则以配置为准
                if (!string.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["YingYong"]))
                {
                    var yingYongIDs = System.Configuration.ConfigurationManager.AppSettings["YingYong"].Split(',').ToList();
                    yingYongList = yingYongList.FindAll(y => yingYongIDs.Contains(y.YINGYONGID));
                }
                this.eGYYINGYONGBindingSource.DataSource = yingYongList;
                this.eGYYINGYONGBindingSource.ResetBindings(false);

                this.gridLookUpEdit1.EditValue = yingYongList[0].YINGYONGID;

                //如果存在上次登录信息
                if (this.LoadFromLastLoginInfo)
                {
                    var lastYingYongID = loginDTO.YingYongList.Where(c => c.YINGYONGID == HISClientHelper.ClientSetting.LastLoginInfo.YingYongID).FirstOrDefault();
                    if (null != lastYingYongID)
                        this.gridLookUpEdit1.EditValue = lastYingYongID.YINGYONGID;
                }
            }

            Point exitButton = PointToScreen(this.mediButtonExit.Location);
            rectangle = new Rectangle(exitButton.X + this.mediButtonExit.Width, exitButton.Y, this.mediButtonExit.Width, this.mediButtonExit.Height);
        }

        /// <summary>
        /// 用户名验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_YongHuMing_Validated(object sender, EventArgs e)
        {
            if (this.textBox_YongHuMing.EditValue == null)
                this.textBox_MiMa.EditValue = string.Empty;
            else if (this.textBox_YongHuMing.EditValue != this.textBox_YongHuMing.OldEditValue)
                this.textBox_MiMa.EditValue = string.Empty;

            this.LoadFromLastLoginInfo = false;
        }

        /// <summary>
        /// 用户名值验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_YongHuMing_InvalidValue(object sender, InvalidValueExceptionEventArgs e)
        {
            if (this.LoadFromLastLoginInfo)
                e.ExceptionMode = ExceptionMode.Ignore;
            else
                e.ExceptionMode = ExceptionMode.NoAction;
        }

        /// <summary>
        /// 用户名回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_YongHuMing_Properties_Enter(object sender, EventArgs e)
        {
            this.textBox_YongHuMing.SelectAll();
        }

        /// <summary>
        /// 用户名双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_YongHuMing_DoubleClick(object sender, EventArgs e)
        {
            this.textBox_YongHuMing.SelectAll();
        }

        /// <summary>
        /// 应用获取焦点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridLookUpEdit1_GotFocus(object sender, EventArgs e)
        {
            gridLookUpEdit1.Tag = true;
            gridLookUpEdit1.SelectAll();
        }

        /// <summary>
        /// 应用点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridLookUpEdit1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && (bool)gridLookUpEdit1.Tag == true)
            {
                gridLookUpEdit1.SelectAll();
            }

            gridLookUpEdit1.Tag = false;
        }

        /// <summary>
        /// 退出点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonExit_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker_XiTong.IsBusy)
                this.backgroundWorker_XiTong.CancelAsync();

            Exit();
        }

        /// <summary>
        /// 密码框按下键盘事件(检查用户是否按了键盘上指定的键触发事件)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_MiMa_KeyDown(object sender, KeyEventArgs e)
        {
            /*************** 取消密码上回车直接登录 ******************
            // 在密码框输入回车焦点直接跳槽登录按钮, 按Tab继续跳到下一个
            if (e.KeyCode == Keys.Enter)
            {
                // 回车转Tab直接跳到焦点登录按钮
                //SendKeys.Send("{Tab}");

                // 由于当密码输入错误时需要重新聚焦到密码输入框，所以此处密码框关闭回车跳到下一个输入框
                this.textBox_MiMa.EnterMoveNextControl = false;

                // 回车键时直接触发登录事件
                mediButtonDengLu_Click(mediButtonDengLu, new EventArgs());
            }
            *******************************************************/
        }

        /// <summary>
        /// 键盘按下某个键时触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DengLu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.M)
            {
                CustomDownLoadFileFrm customDownLoadFileFrm = new CustomDownLoadFileFrm();
                customDownLoadFileFrm.ShowDialog();
                customDownLoadFileFrm.Dispose();
            }
        }

        /// <summary>
        /// 窗口首次打开时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Denglu_Shown(object sender, EventArgs e)
        {
            if (!this.Focused)
            {
                this.Activate();
                this.Focus();
            }
            Rectangle ScreenArea = Screen.GetWorkingArea(this);
            WinApiHelper.SetCursorPos(ScreenArea.Width / 2, ScreenArea.Height / 2);  // 设置鼠标位置

            // 获取用户名输入框位置+10
            uint X = (uint)this.textBox_YongHuMing.Location.X + (uint)this.mediPanelControl1.Location.X + 10;
            uint Y = (uint)this.textBox_YongHuMing.Location.X + (uint)this.mediPanelControl1.Location.Y + 10;

            WinApiHelper.mouse_event(WinApiHelper.MOUSEEVENTF_LEFTDOWN | WinApiHelper.MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }

        /// <summary>
        /// 异步执行更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_XiTong_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgWorker = sender as BackgroundWorker;

            BackgroundWokerState bgWorkerState = new BackgroundWokerState();

            bgWorker.ReportProgress(0, "系统正在初始化...");

            if (networkList == null)
            {
                // 获取可用网卡信息
                networkList = NetworkHeler.GetAvailableNetwork();
            }
            // 取上次登录的信息
            if (!SwitchSystem && !string.IsNullOrWhiteSpace(HISClientHelper.ClientSetting.LastLoginInfo.ZhiGongGH) && !ConfigurationManager.AppSettings["RememberGongHao"].Equals("N"))
            {
                Result<LoginDTO> ret = null;
                try
                {
                    ret = loginService.GetYongHuXByGH(HISClientHelper.ClientSetting.LastLoginInfo.ZhiGongGH, networkList);
                    if (ret != null && ret.ReturnCode == ReturnCode.SUCCESS)
                    {
                        loginDTO = ret.Return;
                        this.LoadFromLastLoginInfo = true;

                        AutoWriteYongHuMing();

                        this.Invoke(new Action(() =>
                        {
                            if (this.Handle != WinApiHelper.GetF())  // 如果本窗口没有获得焦点
                                WinApiHelper.SetF(this.Handle);      // 设置本窗口获得焦点
                        }));
                    }
                    else if (ret?.ReturnCode == ReturnCode.SERVICEERROR)
                    {
                        // 上次登录工号信息异常时进行提示，后续可以切换账号
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new Action(() =>
                            {
                                MediMsgBox.Failure(this, $"获取上次登录工号【{HISClientHelper.ClientSetting.LastLoginInfo.ZhiGongGH}】信息异常，请重新输入工号", ret);
                            }));
                        }
                        else
                            MediMsgBox.Failure(this, $"获取上次登录工号【{HISClientHelper.ClientSetting.LastLoginInfo.ZhiGongGH}】信息异常，请重新输入工号", ret);
                    }
                    else
                    {

                        ConnectionServer(ret.ExceptionContent);
                    }
                }
                catch
                {
                    ConnectionServer(ret.ExceptionContent);
                }
            }

            //// 服务初始化，预加载EF
            //bgWorker.ReportProgress(50, "正在检查更新...");
            //efService.EFInitialize();

            bgWorker.ReportProgress(2, "正在检测更新...");
            // 检测FTP设置

            //FTPHelper ftp;
            //string ftpStr = HISClientHelper.GlobalSetting.GetConfigItemValue(HISGlobalSetting.MainFTP, "连接字符串");
            //ftp = new FTPHelper(DESHelper.Decrypt(ftpStr, HISGlobalSetting.Key));
            //FTPHelper ftpHelper = new FTPHelper("ftp://" + DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpIp, HISGlobalSetting.Key), HISGlobalHelper.GlobalSetting.FTPINFO.FtpUser, DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpPwd, HISGlobalSetting.Key));
            //if (!ftpHelper.TestFtpConnection())
            //{
            //        manualReset.Reset();
            //        bgWorker.ReportProgress(-20, "FTP无法正常连接..");
            //        manualReset.WaitOne();

            //}

            if (string.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["IsDevelopmentMode"]))
            {
                if (HISGlobalSetting.IsHttp)
                {
                    #region Http更新

                    UpdateHelper.InitialUserCustomInfo();
                    try
                    {
                        List<HTTPUpdateConfig> httpconfigs = HISGlobalHelper.HttpConfigs;//读取本地配置文件
                        List<HTTPUpdateConfig> jcjghttpconfig = HISGlobalHelper.JcjgHttpConfigs;//读取基础架构本地配置文件
                        List<HTTPUpdateConfig> UNupdatedconfigs = HISGlobalHelper.CheckHttpConfig(httpconfigs);
                        List<HTTPUpdateConfig> availableJcjgConfigs = HISGlobalHelper.CheckHttpConfig(jcjghttpconfig);
                        int count = 0, jcjgcount = 0;
                        List<HTTPUpdateConfig> Needupdatedconfigs = new List<HTTPUpdateConfig>();   // 需要更新得配置
                        if (UNupdatedconfigs != null && UNupdatedconfigs.Count != 0)
                        {
                            DirectoryInfo startPathInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                            string httpAddress = startPathInfo.Parent.FullName + "\\DownLoadAddress.xml";
                            if (!File.Exists(httpAddress))
                            {
                                using (HttpConfig hTTPConfig = new HttpConfig())
                                {
                                    hTTPConfig.ShowDialog();
                                    if (hTTPConfig.DialogResult == DialogResult.Cancel)
                                        return;
                                }
                            };
                            string severUrl = MediinfoConfig.GetXmlNodeValue(httpAddress, "ipAddress");
                            foreach (HTTPUpdateConfig item in UNupdatedconfigs)
                            {
                                string returnMessage = HTTPHelper.DownloadConfigFiles(severUrl, item.BanBenHao, item.JIXIANMC); //最新版本号
                                if (!string.IsNullOrEmpty(returnMessage))
                                {
                                    if (returnMessage.Equals(item.BanBenHao))
                                    {
                                        // 版本号没有更新
                                        count++;
                                        continue;
                                    }
                                    else
                                    {
                                        // 版本号更新
                                        long ReturnMessage;
                                        if (long.TryParse(returnMessage, out ReturnMessage) && returnMessage.Length == 18)
                                        {
                                            HTTPUpdateConfig hTTPUpdateConfig = new HTTPUpdateConfig(returnMessage, item.JIXIANMC, item.localConfigPath);
                                            Needupdatedconfigs.Add(hTTPUpdateConfig);
                                        }
                                        else
                                        {
                                            // 服务器返回错误信息写入日志
                                            HTTPHelper.WriteLog(string.Format("{0},Date:{1}", returnMessage, DateTime.Now));
                                        }
                                    }
                                }
                            }
                            if (count == UNupdatedconfigs.Count)
                            {
                                //不需要下载更新
                                bgWorker.ReportProgress(100, "SUCCESS");
                                bgWorkerState.State = 0;
                                bgWorkerState.ErrorMsg = string.Empty;
                                bgWorkerState.Tips = string.Empty;
                                bgWorkerState.Exit = false;
                                e.Result = bgWorkerState;
                            }
                            else
                            {
                                isStartUpdate = true;
                                needUpdatedirectorys = Needupdatedconfigs;
                                //下载并启动更新程序
                                HTTPCallBackUpdateProgram(Needupdatedconfigs, bgWorker, bgWorkerState, e);
                                //manualReset.Reset();
                                e.Result = bgWorkerState;
                                bgWorker.ReportProgress(100, "SUCCESS");
                                //manualReset.WaitOne();
                            }

                        }
                        if (availableJcjgConfigs != null && availableJcjgConfigs.Count != 0)
                        {
                            DirectoryInfo startPathInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                            string httpAddress = startPathInfo.Parent.FullName + "\\DownLoadAddress.xml";
                            if (!File.Exists(httpAddress)) return;
                            string severUrl = MediinfoConfig.GetXmlNodeValue(httpAddress, "ipAddress");
                            foreach (HTTPUpdateConfig item in availableJcjgConfigs)
                            {
                                string returnMessage = HTTPHelper.DownloadConfigFiles(severUrl, item.BanBenHao, item.JIXIANMC); //最新版本号
                                if (!string.IsNullOrEmpty(returnMessage) && returnMessage.Equals(item.BanBenHao)) jcjgcount++;
                            }
                            if (jcjgcount != availableJcjgConfigs.Count) RunUpdateExe();

                        }
                    }
                    catch (ApplicationException ex)
                    {
                        if (bgWorker.CancellationPending)

                        {
                            bgWorkerState.State = -1;
                            bgWorkerState.ErrorMsg = string.Empty;
                            bgWorkerState.Tips = string.Empty;
                            bgWorkerState.Exit = true;
                            e.Result = bgWorkerState;
                            return;
                        }

                        bgWorker.ReportProgress(100, "SUCCESS");

                        bgWorkerState.State = 0;
                        bgWorkerState.ErrorMsg = string.Empty;
                        bgWorkerState.Tips = string.Empty;
                        bgWorkerState.Exit = false;
                        e.Result = bgWorkerState;

                        MessageBox.Show(ex.Message + ex.InnerException, "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        HTTPHelper.WriteLog(string.Format("Date {0} ,Class: {1}, Property: {2}, Error: {3}", DateTime.Now, this.GetType().FullName,
                                  ex.Message,
                                 ex.InnerException));
                        Application.Exit();
                        this.Invoke((MethodInvoker)delegate { this.DialogResult = DialogResult.Cancel; });
                    }

                    #region 更新子系统
                    if (AgainSystem)
                    {
                        var rootpath = Directory.GetParent(Application.StartupPath).FullName;
                        HISGlobalSetting.zxt = HISClientHelper.YINGYONGID.SubString(0, 2);
                        bool needUpdate_zxt = CheckUpdate_ZXT(rootpath + "\\AssemblyClient", HISGlobalSetting.zxt);

                        if (isStartUpdate || needUpdate_zxt)
                        {
                            string args = String.Format(" /Y {0} /U {1} /P {2}", HISClientHelper.YINGYONGID.SubString(0, 2), HISClientHelper.USERID, HISClientHelper.USERPWD);
                            var rootP = Directory.GetParent(Application.StartupPath).FullName;
                            // 系统所在文件夹
                            KillUpdateProcess(rootP);
                            //启动监控服务
                            if (!string.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["StartMonitor"]) && System.Configuration.ConfigurationManager.AppSettings["StartMonitor"].ToUpper() == "Y")
                            {
                                WindowsServiceHelper.InstallService("Mediinfo.WinForm.HIS.Monitor");
                                WindowsServiceHelper.StartService("Mediinfo.WinForm.HIS.Monitor");
                            }
                            ProcessStartInfo processStartInfo = new ProcessStartInfo();
                            processStartInfo.FileName = UpdateHelper.UpdateExeName + ".exe";
                            processStartInfo.WorkingDirectory = rootP + "\\";
                            processStartInfo.Verb = "runas";
                            processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
                            processStartInfo.Arguments = args + " /M HTTP";

                            processStartInfo.UseShellExecute = true;
                            processStartInfo.CreateNoWindow = true;
                            // 处理XP系统报错问题
                            if (Environment.OSVersion.Version.Major >= 6)
                            {
                                this.Hide();
                                Process.Start(processStartInfo).WaitForExit();
                            }
                            else
                                WinApiHelper.ShellExecute(0, "open", processStartInfo.FileName, args + " /M HTTP", processStartInfo.WorkingDirectory, 11);

                            Exit();
                        }


                    }
                    #endregion


                    #endregion Http更新
                }
                else
                {
                    #region ftp更新程序暂时开发阶段不用

                    UpdateConfigInfo updateConfigInfo = new UpdateConfigInfo();
                    updateConfigInfo.FtpFirstSubDirectoryName = HISGlobalHelper.GlobalSetting.FTPINFO.FtpFirstSubDirectoryName;
                    updateConfigInfo.FtpIp = DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpIp, HISGlobalSetting.Key);
                    updateConfigInfo.FtpPwd = DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpPwd, HISGlobalSetting.Key);
                    //updateConfigInfo.FtpRootDirectoryName = HISGlobalHelper.GlobalSetting.FTPINFO.FtpRootDirectoryName;
                    updateConfigInfo.FtpUser = HISGlobalHelper.GlobalSetting.FTPINFO.FtpUser;
                    updateConfigInfo.UpdateExeName = HISGlobalHelper.GlobalSetting.FTPINFO.UpdateExeName;
                    updateConfigInfo.LoginFormName = HISGlobalHelper.GlobalSetting.FTPINFO.LoginFormName;
                    FTPHelper ftpHelper = new FTPHelper("ftp://" + DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpIp, HISGlobalSetting.Key), HISGlobalHelper.GlobalSetting.FTPINFO.FtpUser, DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpPwd, HISGlobalSetting.Key));
                    bool isTestSuccess = false;
                    if (!ftpHelper.TestFtpConnection())
                    {
                        updateConfigInfo.FtpIp = DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpSpareIp, HISGlobalSetting.Key);

                        ftpHelper = new FTPHelper("ftp://" + DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpSpareIp, HISGlobalSetting.Key), HISGlobalHelper.GlobalSetting.FTPINFO.FtpUser, DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpPwd, HISGlobalSetting.Key));
                        if (!ftpHelper.TestFtpConnection())
                        {
                            isTestSuccess = false;
                            manualReset.Reset();
                            bgWorker.ReportProgress(-20, "FTP无法正常连接..");
                            manualReset.WaitOne();
                        }
                        else
                        {
                            isTestSuccess = true;
                        }
                    }
                    else
                    {
                        isTestSuccess = true;
                    }

                    // 获取启动程序的上一级文件目录
                    DirectoryInfo startPathInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                    String rootPathInfo = startPathInfo.Parent.FullName;    // 上一级的目录

                    if (!UpdateHelper.InitialUserCustomInfo(updateConfigInfo) || !isTestSuccess)
                    {
                        DialogResultEvent = DialogResultFun;
                        var result = this.BeginInvoke(DialogResultEvent);
                        object dialogResult = this.EndInvoke(result);
                        if ((DialogResult)dialogResult == DialogResult.OK)
                        {
                            CallBackUpdateProgram(updateConfigInfo, rootPathInfo, bgWorker, bgWorkerState, e);
                        }
                        else
                        {
                            if (bgWorker.CancellationPending)
                            {
                                bgWorkerState.State = -1;
                                bgWorkerState.ErrorMsg = string.Empty;
                                bgWorkerState.Tips = string.Empty;
                                bgWorkerState.Exit = true;
                                e.Result = bgWorkerState;
                                return;
                            }

                            bgWorker.ReportProgress(100, "SUCCESS");

                            bgWorkerState.State = 0;
                            bgWorkerState.ErrorMsg = string.Empty;
                            bgWorkerState.Tips = string.Empty;
                            bgWorkerState.Exit = false;
                            e.Result = bgWorkerState;
                        }
                    }
                    else
                    {
                        CallBackUpdateProgram(updateConfigInfo, rootPathInfo, bgWorker, bgWorkerState, e);
                    }

                    #endregion ftp更新程序暂时开发阶段不用
                }

            }
            else
            {
                if (bgWorker.CancellationPending)
                {
                    bgWorkerState.State = -1;
                    bgWorkerState.ErrorMsg = string.Empty;
                    bgWorkerState.Tips = string.Empty;
                    bgWorkerState.Exit = true;
                    e.Result = bgWorkerState;
                    return;
                }

                bgWorker.ReportProgress(100, "SUCCESS");

                bgWorkerState.State = 0;
                bgWorkerState.ErrorMsg = string.Empty;
                bgWorkerState.Tips = string.Empty;
                bgWorkerState.Exit = false;
                e.Result = bgWorkerState;
            }
        }

        /// <summary>
        /// 基础架构更新
        /// </summary>
        private void RunUpdateExe()
        {
            this.Hide();
            this.Close();
            DirectoryInfo startPathInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            string rootPathInfo = startPathInfo.Parent.FullName; // 上一级的目录
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = "" + UpdateHelper.UpdateExeName + ".exe";
            processStartInfo.WorkingDirectory = rootPathInfo;
            processStartInfo.Verb = "runas";
            processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
            // 处理XP系统报错问题
            if (Environment.OSVersion.Version.Major >= 6)
            {

                Process.Start(processStartInfo);
            }
            else
                WinApiHelper.ShellExecute(0, "open", processStartInfo.FileName, null, processStartInfo.WorkingDirectory, 11);
        }

        private void backgroundWorker_XiTong_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case -1:   //表示数据库连接错误，需要重连数据库
                    {
                        manualReset.Set();
                    }
                    break;
                case -20:       // FTP连接错误
                    DialogResult ret = MediMsgBox.YesNo(this,
                                                        string.Format("系统更新失败，您可能无法使用最新程序！\r\n 选择 继续使用 您将使用当前版本的程序\r\n 选择 退出系统 您将退出系统"),
                                                        new string[] { "继续使用", "退出系统" },
                                                        MessageBoxDefaultButton.Button1);
                    if (ret != DialogResult.Yes)
                    {
                        Exit();
                    }
                    this.textBox_YongHuMing.Validated -= this.textBox_YongHuMing_Validated;
                    this.textBox_YongHuMing.Validated += this.textBox_YongHuMing_Validated;
                    this.textBox_YongHuMing.Validating -= this.textBox_YongHuMing_Validating;
                    this.textBox_YongHuMing.Validating += this.textBox_YongHuMing_Validating;
                    manualReset.Set();

                    break;
                case 0:
                    this.progressPanelTips.Visible = true;
                    this.textBox_YongHuMing.ReadOnly = true;
                    this.textBox_MiMa.ReadOnly = true;
                    this.gridLookUpEdit1.ReadOnly = true;

                    this.mediButtonDengLu.Enabled = false;
                    this.progressPanelTips.Description = "系统正在初始化...";

                    break;
                case 100:
                    this.progressPanelTips.Visible = false;
                    this.progressPanelTips.Visible = false;
                    this.textBox_YongHuMing.ReadOnly = false;
                    this.textBox_MiMa.ReadOnly = false;
                    this.gridLookUpEdit1.ReadOnly = false;
                    this.mediButtonDengLu.Enabled = true;
                    this.textBox_YongHuMing.Validated -= this.textBox_YongHuMing_Validated;
                    this.textBox_YongHuMing.Validated += this.textBox_YongHuMing_Validated;
                    this.textBox_YongHuMing.Validating -= this.textBox_YongHuMing_Validating;
                    this.textBox_YongHuMing.Validating += this.textBox_YongHuMing_Validating;

                    break;
                default:
                    this.progressPanelTips.Visible = true;
                    this.progressPanelTips.Description = e.UserState.IsNullOrWhiteSpace() ? "系统正在初始化..." : e.UserState.ToString();
                    break;
            }
        }

        private void backgroundWorker_XiTong_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.progressPanelTips.Visible = false;

            if (e.Cancelled || e.Result == null)
                return;

            BackgroundWokerState state = (BackgroundWokerState)e.Result;
            if (state.State != 0 && !string.IsNullOrWhiteSpace(state.Tips))
            {
                MediMsgBox.Failure(this, state.Tips, "-1", state.ErrorMsg);
            }

            if (state.Exit)
            {
                this.Exit();
            }

            if (SwitchSystem || AgainSystem)
            {
                //切换系统
                textBox_YongHuMing.Focus();
                textBox_YongHuMing.EditValue = HISClientHelper.USERID;
                gridLookUpEdit1.Focus();
                gridLookUpEdit1.EditValue = HISClientHelper.YINGYONGID;
                textBox_MiMa.Focus();
                textBox_MiMa.EditValue = HISClientHelper.USERPWD;
                mediButtonDengLu.PerformClick();
            }
            else
            {
                #region 检查当前程序是否获得系统焦点，避免光标闪烁造成可输入的假象

                timer = new Timer();
                timer.Tick += Timer_Tick;
                stopwatch = new Stopwatch();
                timer.Start();
                stopwatch.Start();

                #endregion
            }
        }

        /// <summary>
        /// HTTP更新启动
        /// </summary>
        /// <param name="updateConfigInfo"></param>
        /// <param name="bgWorker"></param>
        /// <param name="bgWorkerState"></param>
        /// <param name="e"></param>
        private void HTTPCallBackUpdateProgram(List<HTTPUpdateConfig> updateConfigInfo, BackgroundWorker bgWorker, BackgroundWokerState bgWorkerState, DoWorkEventArgs e)
        {
            DirectoryInfo startPathInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            string httpAddress = startPathInfo.Parent.FullName + "\\DownLoadAddress.xml";
            if (!File.Exists(httpAddress)) return;
            string severUrl = MediinfoConfig.GetXmlNodeValue(httpAddress, "ipAddress");
            bool canDelete = false;
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "unUpdatedFiles.txt"))
            {
                using (FileStream readfs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "unUpdatedFiles.txt", FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(readfs))
                    {
                        string readline = string.Empty;
                        while ((readline = sr.ReadLine()) != null)
                        {
                            long BanBenHao;
                            string[] logstrs = readline.Split(new string[] { "\t\t" }, StringSplitOptions.RemoveEmptyEntries);
                            if (logstrs[0] != null && logstrs[1] != null && logstrs[2] != null)
                            {
                                // 不管解压出错还是下载出错，都重新下载处理
                                if (long.TryParse(logstrs[1], out BanBenHao) && logstrs[1].Length == 18)
                                {
                                    canDelete = true;
                                }
                            }
                        }
                    }
                }
            }
            if (canDelete)
            {
                FileHelper.DelectFile(AppDomain.CurrentDomain.BaseDirectory + "unUpdatedFiles.txt");
            }
            //btnExit.Enabled = false;
            this.BeginInvoke((MethodInvoker)delegate
            {
                UpdateHelper.InitialUserCustomInfo();   // 初始化更新目录

                if (updateConfigInfo.Count != 0 && updateConfigInfo != null)
                {
                    foreach (HTTPUpdateConfig item in updateConfigInfo)
                    {
                        // 从服务器读回最新版本号参数
                        string returnMessage = HTTPHelper.DownloadConfigFiles(severUrl, item.BanBenHao, item.JIXIANMC);

                        long ReturnMessage;
                        bool canReadFile = long.TryParse(returnMessage, out ReturnMessage) && returnMessage.Length == 18;//代码包可更新
                        if (canReadFile)
                        {
                            if (HTTPHelper.DownloadZipFiles(severUrl, item.localConfigPath, returnMessage, item.JIXIANMC))
                            {
                                try
                                {
                                    string path = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName;
                                    // 解压读取的配置文件 解压到上级目录
                                    FileHelper.DeZip(Path.Combine(item.localConfigPath, item.JIXIANMC + ".zip"), path);

                                    // 删除压缩包
                                    FileHelper.DelectFile(Path.Combine(item.localConfigPath, item.JIXIANMC + ".zip"));
                                    //重写配置文件
                                    GlobalXmlHelper.ModifyAttribute(item.localConfigPath + "HISGlobalSettingHttp.xml", "BanBenHao", returnMessage);

                                    GlobalXmlHelper.ModifyAttribute(item.localConfigPath + "\\HISGlobalSettingHttp.xml", "localConfigPath", item.localConfigPath);

                                }
                                catch (Exception ex)
                                {
                                    using (StreamWriter createlogsw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "unUpdatedFiles.txt", true, Encoding.UTF8))
                                    {
                                        createlogsw.WriteLine("解压出错" + "\t\t" + item.BanBenHao + "\t\t" + item.JIXIANMC);
                                        HTTPHelper.WriteLog("解压出错" + "\t\t" + item.BanBenHao + "\t\t" + item.JIXIANMC);
                                        GlobalXmlHelper.ModifyAttribute(item.localConfigPath + "\\HISGlobalSettingHttp.xml", "BanBenHao", "1");//重写 下次继续下载
                                    }
                                    continue;
                                    // 错误 HISGlobalSettingHttp.xml文件要记录  下载出错和解压出错分别处理
                                }
                            }
                            else
                            {
                                using (StreamWriter createlogsw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "unUpdatedFiles.txt", true, Encoding.UTF8))
                                {
                                    createlogsw.WriteLine("下载出错" + "\t\t" + item.BanBenHao + "\t\t" + item.JIXIANMC);
                                    HTTPHelper.WriteLog("下载出错" + "\t\t" + item.BanBenHao + "\t\t" + item.JIXIANMC);
                                    GlobalXmlHelper.ModifyAttribute(item.localConfigPath + "\\HISGlobalSettingHttp.xml", "BanBenHao", "1");
                                }
                                continue;
                            }
                        }
                        else if (!string.IsNullOrEmpty(returnMessage))
                        {
                            MessageBox.Show(returnMessage, "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            HTTPHelper.WriteLog(string.Format("Date {0} ,Class: {1}, Error: {2}", DateTime.Now, this.GetType().FullName,
                              returnMessage));
                        }
                        else
                        {
                            MessageBox.Show("连接超时,请检查网络连接", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);//错误提示暂时这么写
                            HTTPHelper.WriteLog(string.Format("Date {0} ,Class: {1}, Error: {2}", DateTime.Now, this.GetType().FullName,
                              "连接超时,请检查网络连接"));
                        }
                    }
                    //启动update.exe
                    StartUpadteExe(severUrl, bgWorker, bgWorkerState, e);

                    if (bgWorker.CancellationPending)
                    {
                        bgWorkerState.State = -1;
                        bgWorkerState.ErrorMsg = string.Empty;
                        bgWorkerState.Tips = string.Empty;
                        bgWorkerState.Exit = true;
                        e.Result = bgWorkerState;
                        return;
                    }

                }
                else
                {
                    if (bgWorker.CancellationPending)
                    {
                        bgWorkerState.State = -1;
                        bgWorkerState.ErrorMsg = string.Empty;
                        bgWorkerState.Tips = string.Empty;
                        bgWorkerState.Exit = true;
                        e.Result = bgWorkerState;
                        return;
                    }

                    //bgWorker.ReportProgress(100, "SUCCESS");

                    bgWorkerState.State = 0;
                    bgWorkerState.ErrorMsg = string.Empty;
                    bgWorkerState.Tips = string.Empty;
                    bgWorkerState.Exit = false;
                    e.Result = bgWorkerState;
                }
            });
        }

        /// <summary>
        /// 下载并启动update.exe
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="bgWorker"></param>
        /// <param name="bgWorkerState"></param>
        /// <param name="e"></param>
        private void StartUpadteExe(string severUrl, BackgroundWorker bgWorker, BackgroundWokerState bgWorkerState, DoWorkEventArgs e)
        {
            DirectoryInfo startPathInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            string rootPath = startPathInfo.FullName;
            try
            {
                if (!File.Exists(startPathInfo.Parent.FullName + "\\" + UpdateHelper.UpdateExeName + ".exe"))
                {
                    string returnMessage = HTTPHelper.DownloadConfigFiles(severUrl, "1", "AssemblyClient_UPDATE_Dev");
                    if (HTTPHelper.DownloadZipFiles(severUrl, rootPath, returnMessage, "AssemblyClient_UPDATE_Dev"))
                    {
                        ProcessHelper.KillProcess(UpdateHelper.UpdateExeName);
                        //ZipCommon.UnZip(rootPath + "\\" + UpdateHelper.UpdateExeName + ".zip", rootPath, ref isUpdatedStarted);
                        try
                        {
                            //解压到上一级目录
                            FileHelper.DeZip(rootPath + "AssemblyClient_UPDATE_Dev" + ".zip", startPathInfo.Parent.FullName);
                            //MediMsgBox.Warn("更新程序已启动,请等待更新完成...");
                            Application.Exit();
                        }
                        catch (Exception ex)
                        {
                            // 解压出错配置文件要重写
                            throw;
                        }
                    }
                }

                if (!File.Exists(Path.Combine(startPathInfo.Parent.FullName, "HISGlobalSettingHttp.xml")))
                {
                    if (File.Exists(Path.Combine(rootPath, "HISGlobalSettingHttp.xml")))
                    {
                        File.Copy(Path.Combine(rootPath, "HISGlobalSettingHttp.xml"), Path.Combine(startPathInfo.Parent.FullName, "HISGlobalSettingHttp.xml"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            IntPtr activeHandle = WinApiHelper.GetForegroundWindow();
            int processID = 0;
            WinApiHelper.GetWindowThreadProcessId(activeHandle, ref processID);
            Process currentProcess = Process.GetProcessById(processID); // 系统焦点进程
            if (currentProcess?.Id == Process.GetCurrentProcess().Id && this.LoadFromLastLoginInfo)
            {
                timer.Dispose();
                stopwatch.Stop();
                // 判断是否关联最后一次登录信息，如果关联自动写入鼠标聚焦密码框
                if (HISClientHelper.ClientSetting.IsAutoWriteLastLoginInfo)
                {
                    if (!ConfigurationManager.AppSettings["RememberGongHao"].Equals("N"))
                    {
                        this.textBox_YongHuMing.EditValue = HISClientHelper.ClientSetting.LastLoginInfo.ZhiGongGH;
                        this.textBox_MiMa.Focus();
                    }
                }
                else
                {
                    this.textBox_YongHuMing.Focus();
                }

                AutoWritePassWordAndLogin();
            }
        }

        #endregion

        /// <summary>
        /// 获取二维码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picEdit_ErWm_Click(object sender, EventArgs e)
        {

            if (isclick)
            {
                try
                {
                    Enterprise.Log.ClientLogHelper.Intance.WriteLog("开始获取二维码");
                    if (string.IsNullOrWhiteSpace(this.textBox_YongHuMing.Text))
                    {
                        MediMsgBox.Failure(this, "请输入用户名！");
                        this.textBox_YongHuMing.Focus();
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(this.gridLookUpEdit1.EditValue.ToString()))
                    {
                        MediMsgBox.Failure(this, "请选择应用！");
                        this.gridLookUpEdit1.Focus();
                        return;
                    }
                    #region 注释
                    //Enterprise.Log.ClientLogHelper.Intance.WriteLog("开始获取二维码4.3.1自动签名授权-请求自动签名授权接口");
                    //CAFactory cAFactory = new CAFactory("4.3.1自动签名授权-请求自动签名授权接口", "", "", "");
                    //cAFactory.selfSignRequest.body.openId = GetOpenId();
                    //Enterprise.Log.ClientLogHelper.Intance.WriteLog("开始获取二维码4.3.1Openid：" + cAFactory.selfSignRequest.body.openId);
                    //cAFactory.selfSignRequest.head.clientId = tuple.Item1;
                    //cAFactory.selfSignRequest.head.clientSecret = tuple.Item2;
                    //string action3 = "gateway/selfSign/request";
                    //cAFactory.Url = tuple.Item3 + action3;
                    //var json = JsonConvert.SerializeObject(cAFactory.selfSignRequest);
                    //Enterprise.Log.ClientLogHelper.Intance.WriteLog("4.3.1自动签名授权 - 请求自动签名授权接口入参：" + json.ToString());
                    //ServiceProxy.Core.RestfulClient client = new ServiceProxy.Core.RestfulClient(cAFactory.Url, ServiceProxy.Core.HttpVerbNew.POST, "application/json", json);

                    //string result = string.Empty;
                    //Utility.ResultErWm resultErWm = new Utility.ResultErWm();
                    //result = client.MakeRequest();
                    #endregion
                    CAFactory cAFactory = new CAFactory("4.3.1自动签名授权-请求自动签名授权接口", "", "", "");
                    if (CAFactory.resultErWm.status == "0")
                    {
                        Enterprise.Log.ClientLogHelper.Intance.WriteLog("4.3.1自动签名授权成功");
                        ErWmTimeCount = 0;//0开始计时
                        this.mediTimer.Start();//计时器开始
                        PictureEdit picture = new PictureEdit();
                        this.mediPanelControl1.Controls.Add(picture);
                        picture.Dock = DockStyle.Fill;
                        picture.BorderStyle = BorderStyles.NoBorder;
                        picture.Properties.AllowFocused = false;
                        picture.BringToFront();
                        picture.Image = ConvertBase64ToImage(CAFactory.resultErWm.data);
                        this.picEdit_ErWm.Image = Resources.computer;
                    }

                   
                }
                catch (Exception ex)
                {
                    Enterprise.Log.ClientLogHelper.Intance.WriteLog("4.3.1自动签名授权接口异常：" + ex.Message);
                    this.mediTimer.Dispose();
                    this.mediTimer.Stop();
                    ErWmTimeCount = 0;
                }      
                isclick = false;
            }
            else
            {
                foreach (Control s in this.mediPanelControl1.Controls)
                {
                    if (s is PictureEdit)
                    {
                        this.mediPanelControl1.Controls.Remove(s);
                        break;
                    }
                }
                this.mediTimer.Dispose();
                this.mediTimer.Stop();
                ErWmTimeCount = 0;
                this.picEdit_ErWm.Image = Resources.ErWeiMa;
                isclick = true;
                isSm = false;
            }
        }

        /// <summary>
        /// 转换图片
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public Image ConvertBase64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                ms.Write(imageBytes, 0, imageBytes.Length);
                return Image.FromStream(ms, true);
            }
        }

        internal class BackgroundWokerState
        {
            public BackgroundWokerState()
            {
                State = 0;
                Tips = string.Empty;
                ErrorMsg = string.Empty;
                Exit = false;
            }

            /// <summary>
            /// 0:正常退出
            /// </summary>
            public int State { get; set; }

            /// <summary>
            /// 是否退出系统
            /// </summary>
            public bool Exit { get; set; }

            /// <summary>
            /// 提示消息
            /// </summary>
            public string Tips { get; set; }

            /// <summary>
            /// 错误消息
            /// </summary>
            public string ErrorMsg { get; set; }
        }

        /// <summary>
        /// 计时二维码有效时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediTimer_Tick(object sender, EventArgs e)
        {
            ErWmTimeCount++;
            try
            {
                #region 注释
                //Enterprise.Log.ClientLogHelper.Intance.WriteLog("开始4.3.2自动签名授权-获取授权结果接口");
                //CAFactory cAFactory = new CAFactory("4.3.2自动签名授权-获取授权结果接口", "", "", "");
                //cAFactory.selfSignRequest.body.openId = CAFactory.GetOpenId();
                //cAFactory.selfSignRequest.head.clientId = tuple.Item1;
                //cAFactory.selfSignRequest.head.clientSecret = tuple.Item2;
                //string action2 = "gateway/selfSign/getResult";
                //cAFactory.Url = tuple.Item3 + action2;
                //var json = JsonConvert.SerializeObject(cAFactory.selfSignRequest);
                //Enterprise.Log.ClientLogHelper.Intance.WriteLog("4.3.2自动签名授权-获取授权结果接口入参：" + json.ToString());
                //ServiceProxy.Core.RestfulClient client = new ServiceProxy.Core.RestfulClient(cAFactory.Url, ServiceProxy.Core.HttpVerbNew.POST, "application/json", json);

                //string result = string.Empty;
                //GetResult resultGet = new GetResult();
                //result = client.MakeRequest();
                //resultGet = JsonConvert.DeserializeObject<GetResult>(result);
                #endregion
                CAFactory cAFactory = new CAFactory("4.3.2自动签名授权-获取授权结果接口", "", "", "");
                if (CAFactory.resultGet.status == "0" && CAFactory.resultGet.data.grantStep == 1)
                {
                    Enterprise.Log.ClientLogHelper.Intance.WriteLog("4.3.2自动签名授权-获取授权结果接口成功---已授权");
                    isSm = true;
                    this.mediTimer.Dispose();
                    this.mediTimer.Stop();
                    mediButtonDengLu_Click(null, null);
                    return;
                }
                else if (CAFactory.resultGet.status == "0" && CAFactory.resultGet.data.grantStep == 2)
                {
                    Enterprise.Log.ClientLogHelper.Intance.WriteLog("4.3.2自动签名授权-获取授权结果接口成功---未授权");
                    this.mediTimer.Dispose();
                    this.mediTimer.Stop();
                    isSm = false;
                    MediMsgBox.Failure(this, "用户未授权，请用密码登录！");
                    return;
                }
                else
                {
                    if (ErWmTimeCount >= 119)//120秒有效
                    {
                        this.mediTimer.Dispose();
                        this.mediTimer.Stop();
                        isSm = false;
                        foreach (Control s in this.mediPanelControl1.Controls)
                        {
                            if (s is PictureEdit)
                            {
                                this.mediPanelControl1.Controls.Remove(s);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isSm = false;
                Enterprise.Log.ClientLogHelper.Intance.WriteLog("4.3.2自动签名授权-获取授权结果接口异常：" + ex.Message);
                this.mediTimer.Dispose();
                this.mediTimer.Stop();
            }
        }
    }
}