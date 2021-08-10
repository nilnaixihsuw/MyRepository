using Ionic.Zip;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Update
{
    /// <summary>
    /// 更新页面
    /// </summary>
    public partial class MediinfoUpdateMainForm : Form
    {
        /// <summary>
        /// 所有要更新的文件(通过版本号获取的,不包含时间的过滤)
        /// </summary>
        public List<ServerAllFileDic> AllUpdateFiles { get; set; }
        private long fileSize;
        private long extractedSizeTotal;
        private long compressedSize;
        private string compressedFileName;
        private StringBuilder unUpdateFile;//用于记录进程占用而跳过更新的文件
        private string unUpdateFilePath;//记录未更新文件信息的文本路径

        /// <summary>
        /// 服务端获取文件委托
        /// </summary>
        /// <param name="serverPath"></param>
        /// <returns></returns>
        public delegate List<ServerAllFileDic> GetServerFileGather(string ftpServertp, string serverPath, string ftpUserName, string ftpUserpwd);

        public long TotalFileSize { get; set; }

        public List<HTTPUpdateConfig> HTTPUpdateConfigs { get; set; }

        public long DownLoadFileSize { get; set; }

        /// <summary>
        /// 所有要更新的文件
        /// </summary>
        public List<ServerAllFileDic> ServerAllFileList { get; set; }

        public GetServerFileGather GetServerFileFun;

        public MediinfoUpdateMainForm(List<ServerAllFileDic> allUpdateFileList, string args)
        {
            InitializeComponent();
            this.ServerAllFileList = allUpdateFileList;
            CmdArgs = args;
        }

        public MediinfoUpdateMainForm(List<HTTPUpdateConfig> allUpdateFileList, string args)
        {
            InitializeComponent();
            this.HTTPUpdateConfigs = allUpdateFileList;
            CmdArgsHttp = args;
        }

        #region fields

        // 是否显示阴影
        private bool aeroEnabled;

        #endregion

        #region properties

        /// <summary>
        /// 命令行参数
        /// </summary>
        public string CmdArgs { get; set; }

        /// <summary>
        /// 命令行参数http
        /// </summary>
        public string CmdArgsHttp { get; set; }

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

        /// <summary>
        /// 创建控件句柄时获取所需的创建参数。
        /// </summary>
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

        #region methods

        /// <summary>
        /// 是否显示阴影
        /// </summary>
        /// <returns></returns>
        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
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
            List<NetworkConfig> networkList = NetworkHeler.GetAvailableNetwork();
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
        /// 生成需要执行的bat
        /// </summary>
        private void loadBatFile()
        {
            //添加需要执行的bat文件（更新前删除原子系统的所有dll以及pdb文件）  by xuyi 2021/1/25
            //不存在则添加
            string path = Path.Combine(Application.StartupPath, "AssemblyClient", "HisStart" + HISGlobalSetting.zxt + ".bat");
            if (!File.Exists(path))
            {
                FileStream fs1 = new FileStream(path, FileMode.Create, FileAccess.Write);//创建写入文件
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine("del " + Application.StartupPath + @"\AssemblyClient\" + HISGlobalSetting.zxt + @"\*.dll");//写入值
                sw.WriteLine("del " + Application.StartupPath + @"\AssemblyClient\" + HISGlobalSetting.zxt + @"\*.pdb");//写入值
                sw.Close();
                fs1.Close();
            }
        }

        /// <summary>
        /// 执行删除dll的bat，之后重新下载
        /// </summary>
        private void zhiXingBatFile()
        {
            //存在则执行
            //string path = Path.Combine(Application.StartupPath, "AssemblyClient", "HisStart" + HISGlobalSetting.zxt + ".bat");
            //if (File.Exists(path))
            //{
            //    using (Process proc = new Process())
            //    {
            //        proc.StartInfo.FileName = path;
            //        proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(path);

            //        //run as admin
            //        proc.StartInfo.Verb = "runas";

            //        proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            //        proc.Start();
            //        while (!proc.HasExited)
            //        {
            //            proc.WaitForExit(1000);
            //        }
            //    }
            //}
            HISClientHelper.BatRunCmd("HisStart"+ HISGlobalSetting.zxt + ".bat", AppDomain.CurrentDomain.BaseDirectory+ @"AssemblyClient\", out var errorMsg);
            if (!string.IsNullOrWhiteSpace(errorMsg))
                throw new ApplicationException(errorMsg);
        }

        #endregion

        #region extern

        /// <summary>
        /// 设置窗口的桌面窗口管理器（DWM）非客户端渲染属性的值。
        /// </summary>
        /// <param name="hwnd">要为其设置属性值的窗口的句柄。</param>
        /// <param name="attr">一个标志，描述要设置的值，指定为DWMWINDOWATTRIBUTE枚举的值。</param>
        /// <param name="attrValue">指向包含要设置的属性值的对象的指针。</param>
        /// <param name="attrSize">通过pvAttribute参数设置的属性值的大小（以字节为单位）。</param>
        /// <returns></returns>
        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        /// <summary>
        /// 将窗口框架扩展到工作区。
        /// </summary>
        /// <param name="hWnd">框架将在其中扩展到工作区的窗口的句柄。</param>
        /// <param name="pMarInset">指向MARGINS结构的指针，该指针描述了将框架扩展到客户区时要使用的边距。</param>
        /// <returns></returns>
        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        /// <summary>
        /// 获取一个值，该值指示是否启用了桌面窗口管理器（DWM）合成。
        /// </summary>
        /// <param name="pfEnabled">如果启用了DWM组合，则指向一个值的指针，当该函数成功返回时，该值将接收TRUE。否则为FALSE。</param>
        /// <returns></returns>
        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        #endregion

        #region override

        /// <summary>
        /// 窗口重绘事件
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
                        DwmSetWindowAttribute(this.Handle, 0x02, ref attrValue, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 1,
                            rightWidth = 1,
                            topHeight = 1
                        };
                        DwmExtendFrameIntoClientArea(this.Handle, ref margins);
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

        private void MediinfoUpdateMainForm_Load(object sender, EventArgs e)
        {
            WindowsServiceHelper.StopService("Mediinfo.WinForm.HIS.Monitor");
            WindowsServiceHelper.UninstallService("Mediinfo.WinForm.HIS.Monitor");
            // 加载本机IP地址
            LoadIPAddress();
            // 版权信息
            this.lblCopyRight.Text = String.Format("Copyright © 1999~{0} 联众智慧科技股份有限公司 版权所有", DateTime.Now.Year);

            if (HISGlobalSetting.IsHttp)
            {
                #region HTTP更新

                //loadBatFile();

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
                                    //不管解压出错还是下载出错，都重新下载处理
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

                btnExit.Enabled = false;
                this.BeginInvoke((MethodInvoker)delegate
                {
                    //初始化更新目录
                    UpdateCommonHelper.InitialUserCustomInfo();

                    if (HTTPUpdateConfigs.Count != 0 && HTTPUpdateConfigs != null)
                    {
                        TotalFileSize = 0;
                        foreach (var item in HTTPUpdateConfigs)
                        {
                            TotalFileSize += item.FileSize;
                        }

                        uploadbackgroundWorker.RunWorkerAsync();

                    }
                    else
                    {
                        btnExit.Enabled = true;
                    }
                });

                #endregion HTTP更新
            }
            else
            {
                TotalFileSize = 0;
                foreach (var item in ServerAllFileList)
                {
                    TotalFileSize += item.ServerFileSize;
                }

                #region ftp更新程序暂时开发阶段不用

                unUpdateFile = new StringBuilder($"时间：{DateTime.Now}，未更新文件如下：");
                unUpdateFilePath = Path.Combine(Application.StartupPath, "未更新文件.txt");
                //删除之前的记录
                if (File.Exists(unUpdateFilePath))
                    File.Delete(unUpdateFilePath);

                btnExit.Enabled = false;
                this.BeginInvoke((MethodInvoker)delegate
                {
                    if (!UpdateCommonHelper.InitialUserCustomInfo(new UpdateConfigInfo()
                    {
                        FtpFirstSubDirectoryName = HISGlobalHelper.GlobalSetting.FTPINFO.FtpFirstSubDirectoryName,
                        FtpIp = DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpIp, HISGlobalSetting.Key),
                        FtpPwd = DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpPwd, HISGlobalSetting.Key),
                        //FtpRootDirectoryName = HISGlobalHelper.GlobalSetting.FTPINFO.FtpRootDirectoryName,
                        FtpUser = HISGlobalHelper.GlobalSetting.FTPINFO.FtpUser,
                        UpdateExeName = HISGlobalHelper.GlobalSetting.FTPINFO.UpdateExeName,
                        LoginFormName = HISGlobalHelper.GlobalSetting.FTPINFO.LoginFormName
                    }))
                    {
                        MessageBox.Show("FTP信息未配置!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        FTPConfigFrm fTPConfigFrm = new FTPConfigFrm();
                        fTPConfigFrm.ShowDialog();
                        fTPConfigFrm.Dispose();
                    }

                    string errorMsg = string.Empty;
                    long tempDownLoadFileSize = 0;
                    //若当前集合文件夹为空，则弹出登录窗口
                    if (UpdateDirectory.GetUpdateDirectories(UpdateCommonHelper.FtpFirstSubDirectoryName.Split(new string[] { "^" }, StringSplitOptions.RemoveEmptyEntries), out errorMsg, mediWaitCircle, TotalFileSize, ref tempDownLoadFileSize).Count > 0)
                    {
                        if (!string.IsNullOrWhiteSpace(errorMsg))
                        {
                            MessageBox.Show(errorMsg, "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //MessageBox.Show("更新程序内部出现错误\n请联系管理员", "联众智慧提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                            LogHelper.WriteLog(string.Format("Date {0} ,Class: {1}, Property: {2}, Error: {3}", DateTime.Now, this.GetType().FullName,
                              errorMsg,
                             "error"));
                            using (FTPConfigFrm fTPConfigFrm = new FTPConfigFrm())
                            {
                                fTPConfigFrm.ShowDialog();
                                if (ServerAllFileList != null)
                                    ServerAllFileList.Clear();
                                else
                                    ServerAllFileList = new List<ServerAllFileDic>();
                            }
                        }
                        DownLoadFileSize += tempDownLoadFileSize;

                        List<UpdateDirectories> directorylist = UpdateDirectory.GetUpdateDirectories(UpdateCommonHelper.FtpFirstSubDirectoryName.Split(new string[] { "^" }, StringSplitOptions.RemoveEmptyEntries), out errorMsg, mediWaitCircle, TotalFileSize, ref tempDownLoadFileSize);
                        DownLoadFileSize += tempDownLoadFileSize;
                        uploadbackgroundWorker.RunWorkerAsync();
                    }
                    else
                    {
                        btnExit.Enabled = true;
                    }
                });

                #endregion ftp更新程序暂时开发阶段不用
            }
        }

        private void uploadbackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!HISGlobalSetting.IsHttp)
            {
                #region ftp
                int totalCount = 0;
                //首先处理客户端日志文件
                List<ServerAllFileDic> updateloglist = new List<ServerAllFileDic>();

                //判断如果客户端文件夹下文件更新记录时间和服务端不一致则下载，如果不存在则下载同时创建日志文件，如果日志文件存在但是日志文件不存在改文件名，则下载
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "updateFileLog.txt"))
                {
                    var streamWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "updateFileLog.txt", false, System.Text.Encoding.UTF8);
                    streamWriter.Flush();
                    streamWriter.Close();
                    streamWriter.Dispose();
                    totalCount += 1;
                }
                else
                {
                    FileStream readfs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "updateFileLog.txt", FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(readfs);

                    string readline = string.Empty;
                    while ((readline = sr.ReadLine()) != null)
                    {
                        string[] logstrs = readline.Split(new string[] { "\t\t" }, StringSplitOptions.RemoveEmptyEntries);
                        if (logstrs.Length > 2)
                        {

                            if (updateloglist.Where(o => o.LastUpdateTime != logstrs[1] && o.ServerFileName.Equals(logstrs[0]) && o.ServerFilePath == logstrs[2]).ToList().Count > 0)
                            {
                                foreach (var item in updateloglist.Where(o => o.LastUpdateTime != logstrs[1] && o.ServerFileName.Equals(logstrs[0]) && o.ServerFilePath == logstrs[2]).ToList())
                                {
                                    item.LastUpdateTime = logstrs[1];
                                }
                            }
                            else
                            {
                                //判断文件是否存在，防止删除文件但未修改updateFileLog.txt而出现死循环(update.exe和main.exe死循环运行)
                                string localFilePath = Path.Combine(Application.StartupPath, logstrs[2]);
                                if (File.Exists(localFilePath))
                                {
                                    //取消检查非压缩文件，即每次更新必定下载非压缩文件
                                    if (Path.GetExtension(localFilePath).ToUpper() != ".ZIP")
                                        continue;

                                    updateloglist.Add(new ServerAllFileDic()
                                    {

                                        ServerFileName = logstrs[0],
                                        LastUpdateTime = logstrs[1],
                                        ServerFilePath = logstrs[2]
                                    });
                                }
                            }

                        }
                    }
                    totalCount += 1;
                    readfs.Flush();
                    sr.Close();
                    readfs.Close();
                    readfs.Dispose();

                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "updateFileLog.txt"))
                    {
                        File.Delete(AppDomain.CurrentDomain.BaseDirectory + "updateFileLog.txt");
                        File.Create(AppDomain.CurrentDomain.BaseDirectory + "updateFileLog.txt").Close();
                    }

                    foreach (ServerAllFileDic item in updateloglist)
                    {
                        if (ServerAllFileList.Where(o => o.LastUpdateTime == item.LastUpdateTime && o.ServerFileName == item.ServerFileName && o.ServerFilePath == item.ServerFilePath).ToList().Count > 0)
                        {
                            ServerAllFileList.RemoveAll(o => o.LastUpdateTime == item.LastUpdateTime && o.ServerFileName == item.ServerFileName && o.ServerFilePath == item.ServerFilePath);
                            long downLoadFileSize = 0;
                            string errorMsg = string.Empty;
                            UpdateDirectory.GetUpdateDirectory(AppDomain.CurrentDomain.BaseDirectory, item.ServerFilePath.Substring(0, item.ServerFilePath.IndexOf("\\")), out errorMsg, null, null, 0, ref downLoadFileSize);

                            StreamWriter createlogsw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "updateFileLog.txt", true, System.Text.Encoding.UTF8);
                            createlogsw.WriteLine(item.ServerFileName + "\t\t" + item.LastUpdateTime + "\t\t" + item.ServerFilePath);

                            createlogsw.Flush();
                            createlogsw.Close();

                            totalCount += updateloglist.Count;
                        }
                        else
                        {

                            StreamWriter createlogsw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "updateFileLog.txt", true, System.Text.Encoding.UTF8);
                            createlogsw.WriteLine(item.ServerFileName + "\t\t" + item.LastUpdateTime + "\t\t" + item.ServerFilePath);
                            createlogsw.Flush();
                            createlogsw.Close();
                        }
                    }
                }

                //分批处理服务端文件先更新解压文件，在更新其他文件,遇到进程占用的文件则跳过更新
                List<ServerAllFileDic> serverAllZIPFiles = new List<ServerAllFileDic>();
                List<ServerAllFileDic> serverAllOTHERPFiles = new List<ServerAllFileDic>();
                foreach (ServerAllFileDic item in ServerAllFileList)
                {
                    if (item.ServerFileName.Substring(item.ServerFileName.LastIndexOf(".") + 1).ToLower().Equals("zip"))
                        serverAllZIPFiles.Add(item);
                    else
                        serverAllOTHERPFiles.Add(item);
                }

                #region 压缩文件先获取更新

                for (int i = 0; i < serverAllZIPFiles.Count; i++)
                {
                    string localPathFile = AppDomain.CurrentDomain.BaseDirectory + @"" + serverAllZIPFiles[i].ServerFilePath + "";

                    if (uploadbackgroundWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    this.Invoke((MethodInvoker)delegate
                    {

                    });

                    string localDirectoryName = localPathFile.Remove(localPathFile.LastIndexOf("\\"));
                    long tempDownLoadFileSize = 0;
                    string serverFilePath = serverAllZIPFiles[i].ServerFilePath.Remove(serverAllZIPFiles[i].ServerFilePath.LastIndexOf("\\"));
                    if (!File.Exists(localPathFile))
                    {
                        if (!Directory.Exists(localDirectoryName))
                        {
                            try
                            {
                                Directory.CreateDirectory(localDirectoryName);
                                //文件下载
                                if (UpdateCommonHelper.GetFileNoBinary(serverFilePath, serverAllZIPFiles[i].ServerFileName, localDirectoryName, serverAllZIPFiles[i].ServerFileName, mediWaitCircle, TotalFileSize, ref tempDownLoadFileSize))
                                {

                                    OperateIniFile operateIniFile = new OperateIniFile(localDirectoryName + "\\" + "version.ini");
                                    operateIniFile.WriteString("version", "UpdateOK", "1");
                                    DownLoadFileSize += tempDownLoadFileSize;
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        //mediWaitCircle.Description = string.Format("正在解压{0}", serverAllZIPFiles[i].ServerFileName);
                                    });

                                    UnPack(localPathFile, localDirectoryName);
                                    StreamWriter createsw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "updateFileLog.txt", true, System.Text.Encoding.UTF8);
                                    createsw.WriteLine(serverAllZIPFiles[i].ServerFileName + "\t\t" + serverAllZIPFiles[i].LastUpdateTime + "\t\t" + serverAllZIPFiles[i].ServerFilePath);
                                    createsw.Flush();
                                    createsw.Close();

                                    createsw.Dispose();
                                }
                            }
                            catch (Exception)
                            {

                                OperateIniFile operateIniFile = new OperateIniFile(localDirectoryName + "\\" + "version.ini");
                                operateIniFile.WriteString("version", "UpdateOK", "0");
                            }
                        }
                        else
                        {

                            try
                            {
                                //文件下载
                                if (UpdateCommonHelper.GetFileNoBinary(serverFilePath, serverAllZIPFiles[i].ServerFileName, localDirectoryName, serverAllZIPFiles[i].ServerFileName, mediWaitCircle, TotalFileSize, ref tempDownLoadFileSize))
                                {
                                    OperateIniFile operateIniFile = new OperateIniFile(localDirectoryName + "\\" + "version.ini");
                                    operateIniFile.WriteString("version", "UpdateOK", "1");
                                    DownLoadFileSize += tempDownLoadFileSize;
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        //mediWaitCircle.Description = string.Format("正在解压{0}", serverAllZIPFiles[i].ServerFileName);
                                    });

                                    //清理Mediinfo开头的文件
                                    DeleteLocalFiles(localPathFile);

                                    UnPack(localPathFile, localDirectoryName);
                                    StreamWriter createsw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "updateFileLog.txt", true, System.Text.Encoding.UTF8);
                                    try
                                    {
                                        createsw.WriteLine(serverAllZIPFiles[i].ServerFileName + "\t\t" + serverAllZIPFiles[i].LastUpdateTime + "\t\t" + serverAllZIPFiles[i].ServerFilePath);
                                        createsw.Flush();
                                        createsw.Close();

                                        createsw.Dispose();
                                    }
                                    catch (Exception)
                                    {

                                        throw;
                                    }
                                }
                            }
                            catch (Exception)
                            {

                                OperateIniFile operateIniFile = new OperateIniFile(localDirectoryName + "\\" + "version.ini");
                                operateIniFile.WriteString("version", "UpdateOK", "0");
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            File.Delete(localPathFile);
                            //文件下载
                            if (UpdateCommonHelper.GetFileNoBinary(serverFilePath, serverAllZIPFiles[i].ServerFileName, localDirectoryName, serverAllZIPFiles[i].ServerFileName, mediWaitCircle, TotalFileSize, ref tempDownLoadFileSize))
                            {
                                OperateIniFile operateIniFile = new OperateIniFile(localDirectoryName + "\\" + "version.ini");
                                operateIniFile.WriteString("version", "UpdateOK", "1");
                                DownLoadFileSize += tempDownLoadFileSize;
                                this.Invoke((MethodInvoker)delegate
                                {
                                    //mediWaitCircle.Description = string.Format("正在解压{0}", serverAllZIPFiles[i].ServerFileName);
                                });

                                //清理Mediinfo开头的文件
                                DeleteLocalFiles(localPathFile);

                                UnPack(localPathFile, localDirectoryName);

                                StreamWriter createsw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "updateFileLog.txt", true, System.Text.Encoding.UTF8);
                                createsw.WriteLine(serverAllZIPFiles[i].ServerFileName + "\t\t" + serverAllZIPFiles[i].LastUpdateTime + "\t\t" + serverAllZIPFiles[i].ServerFilePath);
                                createsw.Flush();
                                createsw.Close();

                                createsw.Dispose();
                            }
                        }
                        catch (Exception)
                        {

                            OperateIniFile operateIniFile = new OperateIniFile(localDirectoryName + "\\" + "version.ini");
                            operateIniFile.WriteString("version", "UpdateOK", "0");
                        }
                    }

                    Application.DoEvents();

                    this.mediWaitCircle.Invoke((MethodInvoker)delegate
                    {
                        //this.mediWaitCircle.Description = string.Format("正在下载{0}", serverAllZIPFiles[i].ServerFileName);
                    });


                }

                #endregion 压缩文件先获取更新

                #region 其他文件后更新

                for (int i = 0; i < serverAllOTHERPFiles.Count; i++)
                {
                    string localPathFile = AppDomain.CurrentDomain.BaseDirectory + @"" + serverAllOTHERPFiles[i].ServerFilePath;

                    //被占用文件跳过更新
                    if (IsFileUsing(localPathFile))
                    {
                        unUpdateFile.AppendLine(localPathFile);
                        continue;
                    }

                    if (uploadbackgroundWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    this.Invoke((MethodInvoker)delegate
                    {

                    });
                    string localDirectoryName = localPathFile.Remove(localPathFile.LastIndexOf("\\"));
                    long tempDownLoadFileSize = 0;
                    string serverFilePath = serverAllOTHERPFiles[i].ServerFilePath.Remove(serverAllOTHERPFiles[i].ServerFilePath.LastIndexOf("\\"));
                    if (!File.Exists(localPathFile))
                    {
                        if (!Directory.Exists(localDirectoryName))
                        {
                            Directory.CreateDirectory(localDirectoryName);
                            try
                            {
                                //文件下载
                                if (UpdateCommonHelper.GetFileNoBinary(serverFilePath, serverAllOTHERPFiles[i].ServerFileName, localDirectoryName, serverAllOTHERPFiles[i].ServerFileName, mediWaitCircle, TotalFileSize, ref tempDownLoadFileSize))
                                {
                                    OperateIniFile operateIniFile = new OperateIniFile(localDirectoryName + "\\" + "version.ini");
                                    operateIniFile.WriteString("version", "UpdateOK", "1");
                                    DownLoadFileSize += tempDownLoadFileSize;
                                    StreamWriter createsw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "updateFileLog.txt", true, System.Text.Encoding.UTF8);
                                    createsw.WriteLine(serverAllOTHERPFiles[i].ServerFileName + "\t\t" + serverAllOTHERPFiles[i].LastUpdateTime + "\t\t" + serverAllOTHERPFiles[i].ServerFilePath, mediWaitCircle, TotalFileSize);
                                    createsw.Flush();
                                    createsw.Close();

                                    createsw.Dispose();
                                }
                            }
                            catch (Exception)
                            {

                                OperateIniFile operateIniFile = new OperateIniFile(localDirectoryName + "\\" + "version.ini");
                                operateIniFile.WriteString("version", "UpdateOK", "0");
                            }
                        }
                        else
                        {

                            try
                            {
                                //文件下载
                                if (UpdateCommonHelper.GetFileNoBinary(serverFilePath, serverAllOTHERPFiles[i].ServerFileName, localDirectoryName, serverAllOTHERPFiles[i].ServerFileName, mediWaitCircle, TotalFileSize, ref tempDownLoadFileSize))
                                {
                                    OperateIniFile operateIniFile = new OperateIniFile(localDirectoryName + "\\" + "version.ini");
                                    operateIniFile.WriteString("version", "UpdateOK", "1");
                                    DownLoadFileSize += tempDownLoadFileSize;
                                    StreamWriter createsw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "updateFileLog.txt", true, System.Text.Encoding.UTF8);
                                    createsw.WriteLine(serverAllOTHERPFiles[i].ServerFileName + "\t\t" + serverAllOTHERPFiles[i].LastUpdateTime + "\t\t" + serverAllOTHERPFiles[i].ServerFilePath);
                                    createsw.Flush();
                                    createsw.Close();

                                    createsw.Dispose();
                                }
                            }
                            catch (Exception)
                            {

                                OperateIniFile operateIniFile = new OperateIniFile(localDirectoryName + "\\" + "version.ini");
                                operateIniFile.WriteString("version", "UpdateOK", "0");
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            File.Delete(localPathFile);
                            //文件下载
                            if (UpdateCommonHelper.GetFileNoBinary(serverFilePath, serverAllOTHERPFiles[i].ServerFileName, localDirectoryName, serverAllOTHERPFiles[i].ServerFileName, mediWaitCircle, TotalFileSize, ref tempDownLoadFileSize))
                            {
                                OperateIniFile operateIniFile = new OperateIniFile(localDirectoryName + "\\" + "version.ini");
                                operateIniFile.WriteString("version", "UpdateOK", "1");
                                DownLoadFileSize += tempDownLoadFileSize;
                                StreamWriter createsw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "updateFileLog.txt", true, System.Text.Encoding.UTF8);
                                createsw.WriteLine(serverAllOTHERPFiles[i].ServerFileName + "\t\t" + serverAllOTHERPFiles[i].LastUpdateTime + "\t\t" + serverAllOTHERPFiles[i].ServerFilePath);
                                createsw.Flush();
                                createsw.Close();

                                createsw.Dispose();
                            }
                        }
                        catch (Exception)
                        {

                            OperateIniFile operateIniFile = new OperateIniFile(localDirectoryName + "\\" + "version.ini");
                            operateIniFile.WriteString("version", "UpdateOK", "0");
                        }
                    }

                    Application.DoEvents();

                    this.mediWaitCircle.Invoke((MethodInvoker)delegate
                    {
                        //this.mediWaitCircle.Description = string.Format("正在下载{0}", serverAllOTHERPFiles[i].ServerFileName);
                    });


                }

                #endregion 其他文件后更新


                //记录因进程占用而跳过更新的文件
                if (unUpdateFile.ToString().Contains(Environment.NewLine))
                {
                    OperateIniFile recordunUpdateFile = new OperateIniFile(unUpdateFilePath);
                    recordunUpdateFile.WriteString("FileInfo", "filepath", unUpdateFile.ToString());
                }
                #endregion ftp
            }
            else
            {
                //downLoadFileSize
                //HTTP下载
                foreach (HTTPUpdateConfig item in HTTPUpdateConfigs)
                {
                    string severUrl = MediinfoConfig.GetValue("DownLoadAddress.xml", "ipAddress"); //string ip = Configuration.GetIP()

                    //从服务器读回最新版本号参数
                    string returnMessage = HTTPHelper.DownloadConfigFiles(severUrl, item.BanBenHao, item.JIXIANMC);

                    long ReturnMessage;
                    bool canReadFile = long.TryParse(returnMessage, out ReturnMessage) && returnMessage.Length == 18;//代码包可更新
                    if (canReadFile)
                    {
                        //如果子系统有更新，则执行bat
                        //zhiXingBatFile();
                        long fileSize = 0;


                        //先删除需要下载的子系统.zip
                        FileHelper.DelectFile(Path.Combine(item.localConfigPath, item.JIXIANMC + ".zip"));
                        //if (HTTPHelper.DownloadZipFiles(severUrl, item.localConfigPath, returnMessage, item.JIXIANMC))
                        if (HTTPHelper.DownloadZipFiles_Progress(severUrl, item.localConfigPath, returnMessage, item.JIXIANMC, mediWaitCircle, TotalFileSize, ref fileSize))
                        {
                            try
                            {
                                //解压读取的配置文件
                                //FileHelper.DeZip(Path.Combine(item.localConfigPath, item.JIXIANMC + ".zip"), item.localConfigPath);
                                UnPackHttp(Path.Combine(item.localConfigPath, item.JIXIANMC + ".zip"), item.localConfigPath);
                                //删除压缩包
                                FileHelper.DelectFile(Path.Combine(item.localConfigPath, item.JIXIANMC + ".zip"));
                                //更新本地配置文件版本号
                                GlobalXmlHelper.ModifyAttribute(item.localConfigPath + "HISGlobalSettingHttp.xml", "BanBenHao", returnMessage);
                                //路径重写
                                GlobalXmlHelper.ModifyAttribute(item.localConfigPath + "HISGlobalSettingHttp.xml", "localConfigPath", item.localConfigPath);
                                //基线重写
                                GlobalXmlHelper.ModifyAttribute(item.localConfigPath + "HISGlobalSettingHttp.xml", "JIXIANMC", item.JIXIANMC);

                                DownLoadFileSize += fileSize;
                                //Application.DoEvents();
                            }
                            catch (Exception ex)
                            {
                                using (StreamWriter createlogsw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "unUpdatedFiles.txt", true, Encoding.UTF8))
                                {
                                    createlogsw.WriteLine("解压出错" + "\t\t" + item.BanBenHao + "\t\t" + item.JIXIANMC);
                                    LogHelper.WriteLog("解压出错" + "\t\t" + item.BanBenHao + "\t\t" + item.JIXIANMC);
                                    GlobalXmlHelper.ModifyAttribute(item.localConfigPath, "BanBenHao", "1");//重写 下次继续下载
                                    //发生异常删除子系统zip
                                    FileHelper.DelectFile(Path.Combine(item.localConfigPath, item.JIXIANMC + ".zip"));
                                }
                                continue;
                                //错误 HISGlobalSettingHttp.xml文件要记录  下载出错和解压出错分别处理
                            }
                        }
                        else
                        {
                            using (StreamWriter createlogsw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "unUpdatedFiles.txt", true, Encoding.UTF8))
                            {
                                createlogsw.WriteLine("下载出错" + "\t\t" + item.BanBenHao + "\t\t" + item.JIXIANMC);
                                LogHelper.WriteLog("下载出错" + "\t\t" + item.BanBenHao + "\t\t" + item.JIXIANMC);
                                GlobalXmlHelper.ModifyAttribute(item.localConfigPath, "BanBenHao", "1");
                                //发生异常删除子系统zip
                                FileHelper.DelectFile(Path.Combine(item.localConfigPath, item.JIXIANMC + ".zip"));
                            }
                            continue;
                        }
                    }
                    else if (!string.IsNullOrEmpty(returnMessage))
                    {
                        MessageBox.Show(returnMessage, "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        LogHelper.WriteLog(string.Format("Date {0} ,Class: {1}, Error: {2}", DateTime.Now, this.GetType().FullName,
                          returnMessage));
                    }
                    else
                    {
                        MessageBox.Show("连接超时,请检查网络连接", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);//错误提示暂时这么写
                        LogHelper.WriteLog(string.Format("Date {0} ,Class: {1}, Error: {2}", DateTime.Now, this.GetType().FullName,
                          "连接超时,请检查网络连接"));
                    }
                }
                //this.Close();
            }
        }

        /// <summary>
        /// 清理Mediinfo开头的文件
        /// </summary>
        /// <param name="localPathFile">待清理目录下的文件</param>
        private void DeleteLocalFiles(string localPathFile)
        {
            var localDir = Directory.GetParent(localPathFile).FullName;
            if (HISGlobalHelper.DeleteDirectories.Contains(localDir) && Directory.GetFiles(localDir, "Mediinfo.*").Length > 0)
            {
                //压缩包中是否存在Mediinfo开头的文件，如果存在则进行清理操作，防止未发布Mediinfo文件时进行清理操作
                bool existMediinfos = false;
                using (ZipFile zipFile = ZipFile.Read(localPathFile, new ReadOptions() { Encoding = Encoding.Default }))
                {
                    existMediinfos = zipFile.EntryFileNames.Any(f => f.ToUpper().StartsWith("MEDIINFO."));
                }
                if (existMediinfos)
                {
                    foreach (var mediinfoFile in Directory.GetFiles(localDir, "Mediinfo.*"))
                    {
                        try
                        {
                            File.Delete(mediinfoFile);
                        }
                        catch (Exception ex)
                        {
                            unUpdateFile.AppendLine("清理出错:" + mediinfoFile + Environment.NewLine + ex.Message);
                        }
                    }
                }
            }
            HISGlobalHelper.DeleteDirectories.Remove(localDir);
        }

        /// <summary>
        /// 解压功能(解压压缩文件到指定目录)
        /// </summary>
        /// <param name="rarFilePath">待解压的文件</param>
        /// <param name="unrarDestPath">指定解压目标目录</param>
        private void UnPack(string rarFilePath, string unrarDestPath)
        {
            // 判断本地是否下载了WinRAR绿色版
            // 如果下载了则使用WinRAR解压，如果未下载则使用默认解压方式
            string value = this.ExistsWinRar();
            bool flag = !string.IsNullOrEmpty(value);
            if (flag)
            {
                this.UnRAR(rarFilePath, unrarDestPath);
            }
            else
            {
                this.UnZip(rarFilePath, unrarDestPath);
            }
        }
        /// <summary>
        /// 解压功能（http下载客户端）
        /// </summary>
        /// <param name="rarFilePath"></param>
        /// <param name="unrarDestPath"></param>
        private void UnPackHttp(string rarFilePath, string unrarDestPath)
        {
            // 判断本地是否下载了WinRAR绿色版
            // 如果下载了则使用WinRAR解压，如果未下载则使用默认解压方式
            string value = this.ExistsWinRar();
            bool flag = !string.IsNullOrEmpty(value);
            if (flag)
            {
                this.UnRAR(rarFilePath, unrarDestPath);
            }
            else
            {
                this.UnZipHttp(rarFilePath, unrarDestPath);
            }
        }

        /// <summary>
        /// 判断本地是否存在WinRAR绿色版
        /// </summary>
        /// <returns></returns>
        private string ExistsWinRar()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string text = Path.Combine(baseDirectory, "WinRAR\\WinRAR.exe");
            bool flag = File.Exists(text);
            string result;
            if (flag)
            {
                result = text;
            }
            else
            {
                result = "";
            }
            return result;
        }

        /// <summary>
        /// WinRAR解压
        /// </summary>
        /// <param name="rarFilePath">待解压的文件</param>
        /// <param name="unrarDestPath">指定解压目标目录</param>
        private void UnRAR(string rarFilePath, string unrarDestPath)
        {
            string text = this.ExistsWinRar();
            bool flag = string.IsNullOrEmpty(text);
            if (flag)
            {
                this.UnZip(rarFilePath, unrarDestPath);
            }
            try
            {
                mediWaitCircle.Description = "正在解压文件...";

                string arguments = string.Format("x -o+ \"{0}\" \"{1}\\\"", rarFilePath, unrarDestPath);
                using (Process process = new Process())
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = text,
                        Arguments = arguments,
                        WindowStyle = ProcessWindowStyle.Hidden
                    };
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();
                    process.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 解压功能(解压压缩文件到指定目录)
        /// </summary>
        /// <param name = "FileToUpZip" > 待解压的文件 </ param >
        /// < param name="ZipedFolder">指定解压目标目录</param>
        public void UnZip(string FileToUpZip, string ZipedFolder)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(FileToUpZip);
                fileSize = fileInfo.Length;
                using (ZipFile zipFile = ZipFile.Read(FileToUpZip, new ReadOptions() { Encoding = Encoding.Default }))
                {
                    extractedSizeTotal = 0;
                    int fileAmount = zipFile.Count;
                    int fileIndex = 0;
                    zipFile.ExtractProgress += Zip_ExtractProgress;
                    foreach (ZipEntry ZipEntry in zipFile)
                    {
                        try
                        {
                            // 被占用文件跳过更新
                            if (IsFileUsing(Path.Combine(ZipedFolder, ZipEntry.FileName)))
                            {
                                unUpdateFile.AppendLine("文件占用" + Path.Combine(ZipedFolder, ZipEntry.FileName));
                                continue;
                            }

                            if (File.Exists(ZipedFolder + "\\" + ZipEntry.FileName + ".tmp"))
                                File.Delete(ZipedFolder + "\\" + ZipEntry.FileName + ".tmp");
                            if (File.Exists(ZipedFolder + "\\" + ZipEntry.FileName + ".PendingOverwrite"))
                                File.Delete(ZipedFolder + "\\" + ZipEntry.FileName + ".PendingOverwrite");
                            fileIndex++;
                            compressedFileName = "(" + fileIndex.ToString() + "/" + fileAmount + "): " + ZipEntry.FileName;

                            compressedSize = ZipEntry.CompressedSize;
                            ZipEntry.Extract(ZipedFolder, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently);
                            extractedSizeTotal += compressedSize;
                        }
                        catch (Exception ex)
                        {
                            // 解压缩文件出错则直接跳过该文件
                            unUpdateFile.AppendLine("解压出错:" + Path.Combine(ZipedFolder, ZipEntry.FileName) + ex.Message);
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("更新程序内部出现错误\n请联系管理员", "联众智慧提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                MessageBox.Show(ex.Message + ex.InnerException, "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LogHelper.WriteLog(string.Format("Date {0} ,Class: {1}, Property: {2}, Error: {3}", DateTime.Now, this.GetType().FullName,
                        ex.Message,
                       ex.InnerException));
                return;
            }
        }


        /// <summary>
        /// 解压功能(解压压缩文件到指定目录)
        /// </summary>
        /// <param name = "FileToUpZip" > 待解压的文件 </ param >
        /// < param name="ZipedFolder">指定解压目标目录</param>
        public void UnZipHttp(string FileToUpZip, string ZipedFolder)
        {
            //unUpdateFile = new StringBuilder();
            FileInfo fileInfo = new FileInfo(FileToUpZip);
            fileSize = fileInfo.Length;
            using (ZipFile zipFile = ZipFile.Read(FileToUpZip, new ReadOptions() { Encoding = Encoding.Default }))
            {
                string filename = "";
                extractedSizeTotal = 0;
                int fileAmount = zipFile.Count;
                int fileIndex = 0;
                zipFile.ExtractProgress += Zip_ExtractProgress;
                foreach (ZipEntry ZipEntry in zipFile)
                {
                    try
                    {
                        filename = ZipEntry.FileName;
                        // 被占用文件跳过更新
                        if (IsFileUsing(Path.Combine(ZipedFolder, ZipEntry.FileName)))
                        {
                            //unUpdateFile.AppendLine("文件占用" + Path.Combine(ZipedFolder, ZipEntry.FileName));
                            LogHelper.WriteLog("文件占用: " + "" + Path.Combine(ZipedFolder, ZipEntry.FileName));
                            //MessageBox.Show("文件占用：" + Path.Combine(ZipedFolder, ZipEntry.FileName), "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue;
                        }

                        if (File.Exists(ZipedFolder + "\\" + ZipEntry.FileName + ".tmp"))
                            File.Delete(ZipedFolder + "\\" + ZipEntry.FileName + ".tmp");
                        if (File.Exists(ZipedFolder + "\\" + ZipEntry.FileName + ".PendingOverwrite"))
                            File.Delete(ZipedFolder + "\\" + ZipEntry.FileName + ".PendingOverwrite");
                        fileIndex++;
                        //LogHelper.WriteLog("解压文件: " + "\t\t"+filename);
                        compressedFileName = "(" + fileIndex.ToString() + "/" + fileAmount + "): " + ZipEntry.FileName;

                        compressedSize = ZipEntry.CompressedSize;
                        ZipEntry.Extract(ZipedFolder, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently);
                        extractedSizeTotal += compressedSize;

                        //如果解压时有zip，需再次解压(为了解决运维平台copyfileClient里面加入的压缩文件)
                        if (filename.Contains(".zip"))
                        {
                            UnZipHttpZip(Path.Combine(ZipedFolder, filename), ZipedFolder);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog("解压出错: " + "" +filename +ex.Message);
                        //MessageBox.Show("解压出错：" + filename + ex.Message, "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // 解压缩文件出错则直接跳过该文件
                        //unUpdateFile.AppendLine("解压出错:" + Path.Combine(ZipedFolder, ZipEntry.FileName + ex.Message));
                        continue;
                    }
                }
            }
        }

        /// <summary>
        /// 如果解压时有zip，需再次解压(为了解决运维平台copyfileClient里面加入的压缩文件)
        /// </summary>
        /// <param name="FileToUpZip"></param>
        /// <param name="ZipedFolder"></param>
        public void UnZipHttpZip(string FileToUpZip, string ZipedFolder)
        {
            //unUpdateFile = new StringBuilder();
            FileInfo fileInfo = new FileInfo(FileToUpZip);
            using (ZipFile zipFile = ZipFile.Read(FileToUpZip, new ReadOptions() { Encoding = Encoding.Default }))
            {
                string filename = "";
                zipFile.ExtractProgress += Zip_ExtractProgress;
                foreach (ZipEntry ZipEntry in zipFile)
                {
                    try
                    {
                        filename = ZipEntry.FileName;


                        //LogHelper.WriteLog("解压文件: " + "\t\t"+filename);

                        ZipEntry.Extract(ZipedFolder, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently);


                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog("解压出错: " + "" + filename + ex.Message);
                        continue;
                    }
                }
            }
        }


        public static void DeleteDir(string file)
        {
            
                //去除文件夹和子文件的只读属性
                //去除文件夹的只读属性
                System.IO.DirectoryInfo fileInfo = new DirectoryInfo(file);
                fileInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;

                //去除文件的只读属性
                System.IO.File.SetAttributes(file, System.IO.FileAttributes.Normal);

                //判断文件夹是否还存在
                if (Directory.Exists(file))
                {
                    foreach (string f in Directory.GetFileSystemEntries(file))
                    {
                        if (File.Exists(f))
                        {
                            //如果有子文件删除文件
                            File.Delete(f);
                        }
                        else
                        {
                            //循环递归删除子文件夹
                            DeleteDir(f);
                        }
                    }

                    //删除空文件夹
                    Directory.Delete(file);

            }
        }

        private void Zip_ExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            if (e.TotalBytesToTransfer > 0)
            {
                long percent = e.BytesTransferred / e.TotalBytesToTransfer;
                //Console.WriteLine("Indivual: " + percent);
                uploadbackgroundWorker.ReportProgress((int)percent);
            }
        }

        private void uploadbackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            long totalPercent = ((long)e.ProgressPercentage * compressedSize + extractedSizeTotal) / fileSize;

            double bytecount = ((e.ProgressPercentage * extractedSizeTotal) * 1.0d / fileSize) * 100;
            if (bytecount > 0)
            {
                double progressTotal = 0;
                mediWaitCircle.Invoke(
                                        (MethodInvoker)(() => progressTotal = ((e.ProgressPercentage * extractedSizeTotal) * 1.0d / fileSize) * 100));
                mediWaitCircle.Description = "正在解压文件" + Convert.ToInt32(progressTotal) + "%";
            }
        }

        private void uploadbackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message + e.Error.InnerException, "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //MessageBox.Show("更新程序内部错误\n请联系管理员", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LogHelper.WriteLog(string.Format("Date {0} ,Class: {1}, Property: {2}, Error: {3}", DateTime.Now, this.GetType().FullName,
                           e.Error.Message,
                          e.Error.InnerException));
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("取消更新", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


            uploadbackgroundWorker.CancelAsync();

            btnExit.Enabled = true;

            this.Close();
        }

        private void MediinfoUpdateMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (HISGlobalSetting.IsHttp)
                {
                    this.Hide();
                    System.Diagnostics.ProcessStartInfo processStartInfo = new System.Diagnostics.ProcessStartInfo();
                    if (!string.IsNullOrEmpty(HISGlobalSetting.StartUp_ZXT))
                    {
                        processStartInfo.Arguments = CmdArgsHttp;//CmdArgsHttp;
                        //启动器方式启动
                        if (HISGlobalSetting.StartUp_ZXT == "AssemblyClient")
                        {
                            processStartInfo.FileName = String.Format("{0}\\Mediinfo.WinForm.HIS.Starter.exe", "AssemblyClient");//UpdateCommonHelper.LoginFormName + ".exe";
                        }
                        processStartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + processStartInfo.FileName))
                        {
                            MessageBox.Show(processStartInfo.FileName + "不存在", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    if (!string.IsNullOrEmpty(HISGlobalSetting.zxt))
                    {

                        processStartInfo.Arguments = CmdArgsHttp;
                        var rootPath = Application.StartupPath + "\\AssemblyClient\\";
                        processStartInfo.WorkingDirectory = rootPath + HISGlobalSetting.zxt + "\\";
                        //检测版本没更新打开 启动器.exe
                        processStartInfo.FileName = "Mediinfo.WinForm.HIS.Main.exe";
                        string startDirectory = rootPath + HISGlobalSetting.zxt + "\\" + processStartInfo.FileName;
                        if (!File.Exists(startDirectory))
                        {
                            MessageBox.Show("Mediinfo.WinForm.HIS.Main.exe" + "不存在", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (File.Exists(rootPath + "HISClientSetting.xml") && !File.Exists(rootPath + HISGlobalSetting.zxt + "\\HISClientSetting.xml"))
                        {
                            File.Copy(rootPath + "HISClientSetting.xml", rootPath + HISGlobalSetting.zxt + "\\HISClientSetting.xml");
                        }
                    }

                    processStartInfo.Verb = "runas";
                    processStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    // 处理Process.Start在xp系统中因权限导致执行失败的问题
                    if (Environment.OSVersion.Version.Major >= 6)
                        System.Diagnostics.Process.Start(processStartInfo);
                    else
                        ShellExecute(0,
                            "open",
                            processStartInfo.FileName,
                            CmdArgsHttp,
                            processStartInfo.WorkingDirectory,
                            11);
                }
                else
                {
                    this.Hide();
                    System.Diagnostics.ProcessStartInfo processStartInfo = new System.Diagnostics.ProcessStartInfo();
                    processStartInfo.Arguments = CmdArgs;
                    processStartInfo.Verb = "runas";
                    if (!string.IsNullOrWhiteSpace(FTPConfigFrm.XiTongLoaclPath))
                    {
                        //更新所选择系统后启动对应路径下的 Main.exe
                        processStartInfo.FileName = "Mediinfo.WinForm.HIS.Main.exe";
                        processStartInfo.WorkingDirectory = FTPConfigFrm.XiTongLoaclPath;
                        if (!File.Exists(Path.Combine(processStartInfo.WorkingDirectory, processStartInfo.FileName)))
                        {
                            MessageBox.Show(processStartInfo.FileName + "不存在", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        //更新登录器后，直接启动登录器
                        processStartInfo.FileName = UpdateCommonHelper.LoginFormName + ".exe";
                        processStartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory + "AssemblyClient";
                        if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "AssemblyClient\\" + processStartInfo.FileName))
                        {
                            MessageBox.Show(processStartInfo.FileName + "不存在", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    processStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    // 处理Process.Start在xp系统中因权限导致执行失败的问题
                    if (Environment.OSVersion.Version.Major >= 6)
                        System.Diagnostics.Process.Start(processStartInfo);
                    else
                        ShellExecute(0,
                            "open",
                            processStartInfo.FileName,
                            processStartInfo.Arguments,
                            processStartInfo.WorkingDirectory,
                            11);
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + exception.InnerException, "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LogHelper.WriteLog(string.Format("Date {0} ,Class: {1}, Property: {2}, Error: {3}", DateTime.Now, this.GetType().FullName,
                       exception.Message,
                      exception.InnerException));
                return;
            }
        }

        [DllImport("SHELL32.dll")]
        public static extern int ShellExecute(int hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        #region 检测文件占用

        [DllImport("kernel32.dll")]
        public static extern IntPtr _lopen(string lpPathName, int iReadWrite);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);

        public const int OF_READWRITE = 2;
        public const int OF_SHARE_DENY_NONE = 0x40;
        public readonly IntPtr HFILE_ERROR = new IntPtr(-1);

        /// <summary>
        /// 文件是否被占用
        /// </summary>
        /// <param name="filePath"></param>
        private bool IsFileUsing(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return false;
            }
            IntPtr vHandle = _lopen(filePath, OF_READWRITE | OF_SHARE_DENY_NONE);
            if (vHandle == HFILE_ERROR)
            {
                return true;
            }
            CloseHandle(vHandle);
            return false;
        }

        #endregion
    }

    #region MARGINS

    /// <summary>
    /// 定义视觉样式的窗口的边距
    /// </summary>
    public struct MARGINS
    {
        /// <summary>
        /// 保留其大小的左边框的宽度
        /// </summary>
        public int leftWidth;
        /// <summary>
        /// 保持其大小的顶部边框的高度
        /// </summary>
        public int topHeight;
        /// <summary>
        /// 保持其大小的右边框的宽度
        /// </summary>
        public int rightWidth;
        /// <summary>
        /// 保持其大小的底部边框的高度
        /// </summary>
        public int bottomHeight;
    }

    #endregion
}