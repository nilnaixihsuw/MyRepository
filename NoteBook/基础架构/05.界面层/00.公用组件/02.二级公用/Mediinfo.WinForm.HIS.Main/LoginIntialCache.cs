using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.XtraSplashScreen;

using Mediinfo.DTO.HIS.GY;
using Mediinfo.DTO.HIS.XT;
using Mediinfo.Enterprise;
using Mediinfo.Enterprise.Token;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Mediinfo.Utility;
using Mediinfo.WinForm.HIS.Controls;
using Mediinfo.WinForm.HIS.Controls.TabForm;
using Mediinfo.WinForm.HIS.Core;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Main
{
    public static class LoginIntialCache
    {
        #region fields

        public delegate DialogResult DialogResultDelegate();

        public static DialogResultDelegate DialogResultEvent;
        public static string cmdArgs = string.Empty;

        public static Dictionary<string, string> lcXiTongIDSet = new Dictionary<string, string>();
        public static Dictionary<string, string> yfXiTongIDSet = new Dictionary<string, string>();
        public static Dictionary<string, string> lc2XiTongIDSet = new Dictionary<string, string>();


        #endregion

        #region static methods

        /// <summary>
        /// 皮肤注册(多个皮肤注册并设置默认皮肤)
        /// </summary>
        public static void StartStyleRegister()
        {
            // 皮肤相关
            DevExpress.UserSkins.BonusSkins.Register();
            SkinManager.DisableFormSkins();
            SkinRegistrator.Register();

            // 加载自定义皮肤
            Assembly assembly = typeof(DevExpress.UserSkins.MediSkinDevExpressStyle).Assembly;
            SkinManager.Default.RegisterAssembly(assembly);

            // 临床皮肤样式
            Assembly asm = typeof(DevExpress.UserSkins.MediSkinDevLCStyle).Assembly;
            SkinManager.Default.RegisterAssembly(asm);

            // 设置默认皮肤
            UserLookAndFeel.Default.SetSkinStyle("MediSkinDevExpressStyle");

            // 默认字体
            AppearanceObject.DefaultFont = new Font("微软雅黑", 11);
        }

        /// <summary>
        /// 设置默认皮肤为 MediSkinDevLCStyle
        /// </summary>
        public static void StartStyleRegisterForLC()
        {
            // 设置默认皮肤
            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle("MediSkinDevLCStyle");
        }

        /// <summary>
        /// 关闭进程
        /// </summary>
        /// <param name="processName">进程名称</param>
        private static void KillProcess(string processName)
        {
            Process myproc = new Process();
            foreach (Process thisproc in Process.GetProcessesByName(processName))
            {
                if (!thisproc.CloseMainWindow())
                {
                    thisproc.Kill();
                    GC.Collect();
                }
                Process[] prcs = Process.GetProcesses();
                foreach (Process p in prcs)
                {
                    if (p.ProcessName.Equals(processName))
                    {
                        p.Kill();
                    }
                }
            }
        }

        /// <summary>
        /// 关闭更新进程
        /// </summary>
        /// <param name="rootPath">程序目录</param>
        private static void KillUpdateProcess(string rootPath)
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
        /// 下载文件
        /// </summary>
        /// <param name="rootPath">程序目录</param>
        private static void DownLoadFile(string rootPath)
        {
            try
            {
                if (!File.Exists(rootPath + "\\" + UpdateHelper.UpdateExeName + ".exe"))
                {
                    if (UpdateHelper.GetFileNoBinary("\\UPDATE", "" + UpdateHelper.UpdateExeName + ".zip", rootPath, "" + UpdateHelper.UpdateExeName + ".zip"))
                    {
                        bool isUpdatedStarted = false;
                        KillProcess(UpdateHelper.UpdateExeName);
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
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = "" + UpdateHelper.UpdateExeName + ".exe";
                processStartInfo.Arguments = cmdArgs;   // 参数登录系统
                processStartInfo.WorkingDirectory = rootPathInfo;
                processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
                //processStartInfo.UseShellExecute = false;
                Process.Start(processStartInfo).WaitForExit();
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

                return;
            }
        }

        /// <summary>
        /// 下载更新文件
        /// </summary>
        /// <param name="rootPath">程序目录</param>
        private static void DownLoadUpadteExe(string rootPath)
        {
            try
            {
                if (!File.Exists(rootPath + "\\" + UpdateHelper.UpdateExeName + ".exe"))
                {
                    if (UpdateHelper.GetFileNoBinary("\\UPDATE", "" + UpdateHelper.UpdateExeName + ".zip", rootPath, "" + UpdateHelper.UpdateExeName + ".zip"))
                    {
                        bool isUpdatedStarted = false;
                        KillProcess(UpdateHelper.UpdateExeName);
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
                        KillProcess(UpdateHelper.UpdateExeName);
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
                String rootPathInfo = startPathInfo.Parent.FullName;    // 上一级的目录
                OperateIniFile operateIniFile = new OperateIniFile(rootPathInfo + "\\" + "version.ini");
                operateIniFile.WriteString("version", "UpdateOK", "1");
                KillUpdateProcess(rootPath);
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.Arguments = cmdArgs;   // 参数登录系统
                processStartInfo.FileName = "" + UpdateHelper.UpdateExeName + ".exe";
                processStartInfo.WorkingDirectory = rootPathInfo;
                processStartInfo.WindowStyle = ProcessWindowStyle.Normal;

                Process.Start(processStartInfo).WaitForExit();
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

                return;
            }
        }

        /// <summary>
        /// 对话框
        /// </summary>
        public static DialogResult DialogResultFun()
        {
            using (FTPConfigFrm fTPConfigFrm = new FTPConfigFrm())
            {
                fTPConfigFrm.TopMost = true;
                fTPConfigFrm.ShowDialog();
                if (fTPConfigFrm.DialogResult == DialogResult.OK)
                    return DialogResult.OK;
                else
                    return DialogResult.Cancel;
            }
        }

        /// <summary>
        /// 加载登录信息
        /// </summary>
        /// <param name="yingYongID">应用ID</param>
        /// <param name="yongHuID">用户ID</param>
        /// <param name="userMiMa">用户密码</param>
        /// <param name="bingQuID">病区ID</param>
        /// <param name="isEncrypt">是否加密</param>
        public static void LoadArgsLoginInfo(string yingYongID, string yongHuID, string userMiMa, string bingQuID, bool isEncrypt)
        {
            cmdArgs = yingYongID + "|" + yongHuID + "|" + userMiMa + "|" + bingQuID + "|" + (isEncrypt == true ? "1" : "0");

            if (string.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["IsDevelopmentMode"]))
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
                String rootPathInfo = startPathInfo.Parent.FullName; //上一级的目录

                if (!UpdateHelper.InitialUserCustomInfo(updateConfigInfo) || !isTestSuccess)
                {
                    if (DialogResultFun() == DialogResult.OK)
                    {
                        CallBackUpdateProgram(updateConfigInfo, rootPathInfo);
                    }
                }
                else
                {
                    CallBackUpdateProgram(updateConfigInfo, rootPathInfo);
                }

                #endregion
            }

            JCJGLoginService loginService = new JCJGLoginService();
            JCJGGongYongService gongYongService = new JCJGGongYongService();

            // 获取可用网卡信息
            List<NetworkConfig> networkList = NetworkHeler.GetAvailableNetwork();
            string miMa = userMiMa;
            var result = loginService.Login(yongHuID, miMa, yingYongID, networkList);
            if (result.ReturnCode != ReturnCode.SUCCESS)
            {
                if (result.ReturnMessage.Length > 20)
                {
                    MediMsgBox.Failure("服务端连接异常!", result.ReturnCode.ToString(), result.ReturnMessage, false);
                    return;
                }
                else
                {
                    MediMsgBox.Failure(result.ReturnMessage);
                    return;
                }
            }
            var yonghuResult = loginService.GetYongHuXByGH(yongHuID, networkList);

            LoginDTO loginDTO = yonghuResult.Return;
            if (loginDTO == null)
            {
                MediMsgBox.Failure("当前登录用户信息异常：原因如下:\r\n(1)当前用户名密码有误!\r\n(2)数据库连接异常!");
                return;
            }
            var yingYong = loginDTO.YingYongList.Where(c => c.YINGYONGID == yingYongID).FirstOrDefault();
            // 院区信息
            HISClientHelper.YUANQUID = yingYong.YUANQUID;     //院区ID
            HISClientHelper.YUANQUMC = yingYong.YUANQUMC;     //院区名称
            HISClientHelper.YIYUANMC = yingYong.YIYUANQC;     //医院全称
            HISClientHelper.YIYUANJC = yingYong.YIYUANJC;     //医院简称
            // 应用信息
            HISClientHelper.YINGYONGID = yingYong.YINGYONGID; //应用ID
            HISClientHelper.KUCUNYYID = yingYong.YINGYONGID;
            HISClientHelper.YINGYONGMC = yingYong.YINGYONGMC; //应用名称
            HISClientHelper.YINGYONGJC = yingYong.YINGYONGJC; //应用简称
            HISClientHelper.XITONGID = yingYong.XITONGID;     //系统ID
            HISClientHelper.YINGWENJC = yingYong.YINGWENJC;     //英文简称jixianid

            HISClientHelper.MENZHENJGTX = yingYong.MENZHENJGTX; //门诊价格体系
            HISClientHelper.ZHUYUANJGTX = yingYong.ZHUYUANJGTX; //住院价格体系
            HISClientHelper.YINGYONGKSID = yingYong.KESHIID;    //应用科室ID
            HISClientHelper.KUCUNGLLX = yingYong.KUCUNGLLX;//库存管理类型

            // 设置本地缓存起始时间(默认调服务器时间)
            var date = gongYongService.GetSysDate();
            if (date.ReturnCode == ReturnCode.SUCCESS)
                HISClientHelper.SetSysDate(date.Return);
            else
                HISClientHelper.SetSysDate(DateTime.Now);

            // 用户ID
            HISClientHelper.USERIDNEW = loginDTO.YongHuXX.YONGHUID;     // 用户ID
            HISClientHelper.YISHENGDJ = loginDTO.ZhiGongXX.YISHENGDJ;   // 医生等级
            HISClientHelper.USERID = loginDTO.ZhiGongXX.ZHIGONGID;      // 职工ID
            HISClientHelper.ZHIGONGGH = loginDTO.ZhiGongXX.ZHIGONGGH;   // 职工工号
            HISClientHelper.USERPWD = userMiMa;                         // 职工密码
            HISClientHelper.USERNAME = loginDTO.ZhiGongXX.ZHIGONGXM;    // 职工姓名
            HISClientHelper.SHURUMLX = loginDTO.YongHuXX.SHURUMA;       // 输入码类型
            HISClientHelper.KESHIID = loginDTO.ZhiGongXX.KESHIID;       // 科室ID
            HISClientHelper.KESHIMC = gongYongService.GetKeShiMCByKeShiID(loginDTO.ZhiGongXX.KESHIID).Return;
            HISClientHelper.RENSHIKS = (string.IsNullOrWhiteSpace(loginDTO.ZhiGongXX.RENSHIKS) ? loginDTO.ZhiGongXX.KESHIID : loginDTO.ZhiGongXX.RENSHIKS);
            HISClientHelper.HESUANKS = (string.IsNullOrWhiteSpace(loginDTO.ZhiGongXX.HESUANKS) ? loginDTO.ZhiGongXX.KESHIID : loginDTO.ZhiGongXX.HESUANKS);

            // 记录上次登录的信息
            HISClientHelper.ClientSetting.LastLoginInfo.ZhiGongID = HISClientHelper.USERID;
            HISClientHelper.ClientSetting.LastLoginInfo.ZhiGongXM = HISClientHelper.USERNAME;
            HISClientHelper.ClientSetting.LastLoginInfo.ZhiGongGH = HISClientHelper.ZHIGONGGH;
            HISClientHelper.ClientSetting.LastLoginInfo.YingYongID = HISClientHelper.YINGYONGID;
            HISClientHelper.ClientSetting.LastLoginInfo.YingYongMC = HISClientHelper.YINGYONGMC;

            //// 消息内容初始化
            //HISClientHelper.XiaoXiRightNR = new Dictionary<string, string>();
            //JCJGXiaoXiService xxService = new JCJGXiaoXiService();
            //var xiaoXi = xxService.GetXiaoXiChuLiCKDZ();
            //if (xiaoXi.ReturnCode == ReturnCode.SUCCESS)
            //    HISClientHelper.XiaoXiRightNR = xiaoXi.Return;

            //工作站信息
            HISClientHelper.GONGZUOZID = result.Return.GONGZUOZID;
            HISClientHelper.COMPUTERNAME = networkList[0].ComputerName;
            HISClientHelper.IP = networkList[0].Ip;
            HISClientHelper.MAC = networkList[0].PhysicalAddress;

            HISClientHelper.ClientSetting.Workstation.GongZuoZhanID = result.Return.GONGZUOZID;
            HISClientHelper.ClientSetting.Workstation.GongZuoZhanMC = result.Return.GONGZUOZM;
            HISClientHelper.ClientSetting.Workstation.WeiZhiID = result.Return.WEIZHIID;
            HISClientHelper.ClientSetting.Workstation.WeiZhiMC = result.Return.WEIZHISM;

            string[] yingYongIDs = new string[] { "1201", "1001", "0401" };
            if (!yingYongIDs.Contains(HISClientHelper.KUCUNYYID))
            {
                // 注册在线状态
                E_XT_ZAIXIANZT zaiXianZt = new E_XT_ZAIXIANZT();
                zaiXianZt.ZHIGONGID = HISClientHelper.USERID;
                zaiXianZt.ZHIGONGGH = HISClientHelper.ZHIGONGGH;
                zaiXianZt.IP = HISClientHelper.IP;
                zaiXianZt.MAC = HISClientHelper.MAC;
                zaiXianZt.XITONGID = HISClientHelper.XITONGID;
                zaiXianZt.YINGYONGID = HISClientHelper.YINGYONGID;
                zaiXianZt.KESHIID = HISClientHelper.DANGQIANKS;
                zaiXianZt.BINGQUID = HISClientHelper.DANGQIANBQ;
                zaiXianZt.YILIAOZID = HISClientHelper.YILIAOZID;
                zaiXianZt.YUANQUID = HISClientHelper.YUANQUID;

                JCJGZhiGongService gYZhiGongService = new JCJGZhiGongService();
                var jueSeYH = gYZhiGongService.GetJueSeYHEXByYongHuID(HISClientHelper.USERID);
                if (jueSeYH.ReturnCode == ReturnCode.SUCCESS)
                {
                    zaiXianZt.JUESEQX = string.Join("|", jueSeYH.Return.Select(m => m.JUESEID));
                }

                var xuZhuResult = loginService.ZaiXianZTXZ(zaiXianZt);
                if (xuZhuResult.ReturnCode == ReturnCode.SUCCESS)
                {
                    HISClientHelper.ZAIXIANZTID = xuZhuResult.Return.ZHUANGTAIID;
                }
            }

            // 生成JWT Token(一般情况下生成Token建议在服务端生成)
            MediToken mediToken = new MediToken("HIS", "HIS", loginDTO.ZhiGongXX.ZHIGONGID,
                new AuthInfo() { UserID = loginDTO.ZhiGongXX.ZHIGONGID });
            TokenLocator.Instance.SetToken(mediToken.CreateToken());

            GYDataLayoutHelper.InitializeCache();
            //Task.Factory.StartNew(GYDataHelper.InitWhenLogin);

            // 加载主窗口
            MediUniversalMFBase mainForm = null;
            string path = Environment.CurrentDirectory;
            DirectoryInfo dirInfo = new DirectoryInfo(path);

            JCJGYingYongService gYYingYongService = new JCJGYingYongService();

            #region 查询框架信息

            // 查询启用临床框架的系统信息
            var linChuangKJList = gYYingYongService.GetLinChuangKJList();
            if (linChuangKJList.ReturnCode != ReturnCode.SUCCESS)
            {
                MediMsgBox.Failure(linChuangKJList.ReturnMessage);
            }
            else
            {
                var yingYongList = linChuangKJList.Return;
                foreach (var item in yingYongList)
                {
                    var xiTongID = item.XITONGID;
                    var yingWenJC = item.YINGWENJC;
                    if (!lcXiTongIDSet.ContainsKey(xiTongID) && !String.IsNullOrWhiteSpace(yingWenJC))
                        lcXiTongIDSet.Add(xiTongID.ToString(), "Mediinfo.WinForm.HIS." + yingWenJC + ".MainForm.dll");
                }
            }

            // 查询启用临床框架2的系统信息
            var linChuangKJ2List = gYYingYongService.GetLinChuangKJ2List();
            if (linChuangKJ2List.ReturnCode != ReturnCode.SUCCESS)
            {
                MediMsgBox.Failure(linChuangKJ2List.ReturnMessage);
            }
            else
            {
                var yingYongList = linChuangKJ2List.Return;
                foreach (var item in yingYongList)
                {
                    var xiTongID = item.XITONGID;
                    var yingWenJC = item.YINGWENJC;
                    if (!lc2XiTongIDSet.ContainsKey(xiTongID) && !String.IsNullOrWhiteSpace(yingWenJC))
                        lc2XiTongIDSet.Add(xiTongID.ToString(), "Mediinfo.WinForm.HIS." + yingWenJC + ".MainForm.dll");
                }
            }

            #endregion

            if (dirInfo.Exists)
            {
                if (lcXiTongIDSet.ContainsKey(HISClientHelper.XITONGID))
                {
                    foreach (var item in dirInfo.GetFiles(lcXiTongIDSet[HISClientHelper.XITONGID], SearchOption.TopDirectoryOnly))
                    {
                        Assembly assembly = Assembly.LoadFrom(item.FullName);

                        List<Type> typeList = assembly.GetTypes().ToList().Where(p => p.IsSubclassOf(typeof(MediUniversalMFBase))).ToList();

                        //如果找不到相关主窗口的基类，则打开默认的窗口
                        if (typeList.Count <= 0)
                            continue;

                        var form = (MediUniversalMFBase)assembly.CreateInstance(typeList[0].FullName);
                        if (form.XiTongID == HISClientHelper.XITONGID)
                        {
                            StartStyleRegisterForLC();

                            mainForm = form;
                            ButtonForOpenChuangKou.GlobalClientMainForm = mainForm;
                            // var winFormLocator = WinFormLocator.Instance;
                            break;
                        }
                    }
                }
                else if (lc2XiTongIDSet.ContainsKey(HISClientHelper.XITONGID))
                {
                    foreach (var item in dirInfo.GetFiles(lc2XiTongIDSet[HISClientHelper.XITONGID], SearchOption.TopDirectoryOnly))
                    {
                        Assembly assembly = Assembly.LoadFrom(item.FullName);

                        List<Type> typeList = assembly.GetTypes().ToList().Where(p => p.IsSubclassOf(typeof(MediUniversalMFBase))).ToList();

                        //如果找不到相关主窗口的基类，则打开默认的窗口
                        if (typeList.Count <= 0)
                            continue;

                        var form = (MediUniversalMFBase)assembly.CreateInstance(typeList[0].FullName);
                        if (form.XiTongID == HISClientHelper.XITONGID)
                        {
                            LoginIntialCache.StartStyleRegisterForLC();

                            mainForm = form;
                            ButtonForOpenChuangKou.GlobalClientMainForm = mainForm;
                            // var winFormLocator = WinFormLocator.Instance;
                            break;
                        }
                    }
                }
                else if (yfXiTongIDSet.ContainsKey(HISClientHelper.XITONGID))
                {
                    foreach (var item in dirInfo.GetFiles(yfXiTongIDSet[HISClientHelper.XITONGID], SearchOption.TopDirectoryOnly))
                    {
                        Assembly assembly = Assembly.LoadFrom(item.FullName);

                        List<Type> typeList = assembly.GetTypes().ToList().Where(p => p.IsSubclassOf(typeof(MediUniversalMFBase))).ToList();

                        //如果找不到相关主窗口的基类，则打开默认的窗口
                        if (typeList.Count <= 0)
                            continue;

                        var form = (MediUniversalMFBase)assembly.CreateInstance(typeList[0].FullName);
                        if (form.XiTongID == HISClientHelper.XITONGID)
                        {
                            StartStyleRegisterForLC();

                            mainForm = form;
                            ButtonForOpenChuangKou.GlobalClientMainForm = mainForm;
                            break;
                        }
                    }
                }
            }
            if (lcXiTongIDSet.ContainsKey(HISClientHelper.XITONGID))
            {
                if (mainForm == null)
                {
                    //mainForm = new MediLCMainForm();
                }
            }
            else if (lc2XiTongIDSet.ContainsKey(HISClientHelper.XITONGID))
            {
                if (mainForm == null)
                {
                    //mainForm = new MediLCMainForm();
                }
            }
            else
            {
                if (mainForm == null)
                {
                    mainForm = new MediMainForm();
                }
            }

            HISClientHelper.MainForm = mainForm;
            ButtonForOpenChuangKou.GlobalClientMainForm = mainForm;
            TabFormHelper.LingChuangMainFormBase = mainForm;

            var __WinFormLocator = ButtonForOpenChuangKouInit.LoginInstance;    // 加载程序集

            mainForm.FormInitialize(HISClientHelper.YINGYONGMC);

            #region 写共享数据

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
            List<string> yingyongidList = new List<string>();

            foreach (string yingyongids in yingYongIdList)
            {
                string[] yingyongid = yingyongids.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                yingyongidList.Add(yingyongid[0]);
            }
            if (yingyongidList != null)
            {
                if (yingyongidList.Contains(HISClientHelper.YINGYONGID))
                {
                    MediMsgBox.Warn(HISClientHelper.YINGYONGID + "应用已登录");
                    return;
                }
                else
                {
                    MemoryMappedFileHelper.CreateClipBoard(HISClientHelper.YINGYONGID, Process.GetCurrentProcess().Id.ToString());
                }
            }
            else
            {
                MemoryMappedFileHelper.CreateClipBoard(HISClientHelper.YINGYONGID, Process.GetCurrentProcess().Id.ToString());
            }

            #endregion 写共享数据

            HISClientHelper.ClientSetting.Save();

            ButtonForOpenChuangKou.GlobalClientMainForm = mainForm;
            // 启动应用调用批处理文件appstart.bat
            string errorMsg = string.Empty;
            HISClientHelper.BatRunCmd("appstart.bat", AppDomain.CurrentDomain.BaseDirectory, out errorMsg);
            if (!string.IsNullOrWhiteSpace(errorMsg))
                throw new ApplicationException(errorMsg);
            if (SplashScreenManager.Default != null)
                if (SplashScreenManager.Default.IsSplashFormVisible)
                    SplashScreenManager.CloseForm();
            mainForm.ResetLoginSysFun = ResetLoginSystem;
            mainForm.ShowDialog();
            mainForm.Dispose();

            if (!ButtonForOpenChuangKou.GlobalClientMainForm.IsLockScreen)
            {
                HISClientHelper.ClientSetting.Save();
                HISClientHelper.GlobalSetting.Save();

                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 重新登录系统
        /// </summary>
        public static void ResetLoginSystem()
        {
            // 获取启动程序的上一级文件目录
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = UpdateHelper.LoginFormName + ".exe";
            processStartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
            Process.Start(processStartInfo);
        }

        /// <summary>
        /// 更新程序
        /// </summary>
        /// <param name="updateConfigInfo">更新配置文件</param>
        /// <param name="rootPathInfo">程序目录</param>
        private static void CallBackUpdateProgram(UpdateConfigInfo updateConfigInfo, string rootPathInfo)
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
                    DownLoadUpadteExe(rootPathInfo);
                }
                catch (Exception ex)
                {
                    LogHelper.Default.Error("下载version.ini失败!", ex);
                    DownLoadUpadteExe(rootPathInfo);
                }
            }
            else  // 判断版本号是否一致
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
                }

                OperateIniFile tempoperateIniFile = new OperateIniFile(rootPathInfo + "\\Temp\\version.ini");
                OperateIniFile operateIniFile = new OperateIniFile(rootPathInfo + "\\version.ini");
                string tempVersionNo = tempoperateIniFile.ReadString("version", "version", string.Empty);
                string versionNo = operateIniFile.ReadString("version", "version", string.Empty);

                if (!tempVersionNo.Equals(versionNo))
                {
                    if (File.Exists(rootPathInfo + "\\Temp\\version.ini"))
                        File.Copy(rootPathInfo + "\\Temp\\version.ini", rootPathInfo + "\\version.ini", true);

                    DownLoadUpadteExe(rootPathInfo);
                }
                else if (directorylist != null)
                {
                    if (directorylist.Count > 0)
                    {
                        DownLoadFile(rootPathInfo);
                    }
                }
            }
        }

        #endregion
    }
}