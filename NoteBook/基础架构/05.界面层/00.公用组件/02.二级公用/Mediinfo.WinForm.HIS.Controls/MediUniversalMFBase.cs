using DevExpress.XtraEditors;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Mediinfo.Utility;
using Mediinfo.WinForm.HIS.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    public delegate void setBadgeValue(string xiaoXiZS);
    /// <summary>
    /// 通用基类主窗体
    /// </summary>
    public partial class MediUniversalMFBase : MediForm
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MediUniversalMFBase()
        {
            InitializeComponent();
            if (!SkinCat.Instance.IsDesignMode)
                InitializeCustom();
        }

        /// <summary>
        /// panel面板内部窗体排序
        /// </summary>
        public Dictionary<int, KeyValuePair<string, Control>> panelInnerFrmSort = new Dictionary<int, KeyValuePair<string, Control>>();

        private void InitializeCustom()
        {
            gYYingYongCDService = new JCJGYingYongCDService();
            gYYongHuGRXXService = new JCJGYongHuGRXXService();
            gongYongService = new JCJGGongYongService();
            CanDanList = gYYingYongCDService.GetYingYongCD().Return;

            this.FormClosing -= this.MainFormBase_FormClosing;
            this.FormClosing += this.MainFormBase_FormClosing;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // 获取当前用户权限数据
            YongHuQXList = GYQuanXianHelper.GetQuanXian();
        }

        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        private void MainFormBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MainClosing != null)
            {
                MainClosing(sender, e);
                if (e.Cancel)
                    return;
            }

            if (CustomCloseMFInfo)
                return;
            if (IsLockScreen)
            {
                e.Cancel = true;
                return;
            }
            if (this.WindowState != FormWindowState.Maximized && this.WindowState != FormWindowState.Normal)
            {
                ShowWindow(this.Handle, 4);
            }
            this.Activate();
            string exitDesc = "确定退出系统?";
            if (ResetLoginSysFun != null && IsCloseAllQXForm && IsRestartLogin)
                exitDesc = "您确定要关闭当前会话,并重新登录系统?";

            DialogResult result = MediMsgBox.Show(this, "操作提醒！", MediButtonShow.OKCancel, MediImagesIco.Warning, false, exitDesc);
            if (IsExitDirectly || result == DialogResult.OK)
            {
                if (HISClientHelper.ClientSetting.GetConfigItemValue("CASM", "ISSM") == "1")
                {
                    if (CAFactory.CALoginOut())
                        Enterprise.Log.ClientLogHelper.Intance.WriteLog("4.3.3自动签名授权 - 退出授权接口成功");
                    else
                        Enterprise.Log.ClientLogHelper.Intance.WriteLog("4.3.3自动签名授权 - 退出授权接口失败");
                    HISClientHelper.ClientSetting.SetConfigItemValue("CASM", "ISSM", "0");
                    HISClientHelper.ClientSetting.Save();
                }

                DesTroyPanelControls();
                if (IsCloseAllQXForm)
                {
                    WaiBuClosing?.Invoke(sender, e);

                    e.Cancel = false;
                    IsCloseBaseForm = true;
                    if (ResetLoginSysFun != null && IsRestartLogin)
                    {
                        ResetLoginSysFun();
                    }

                    HISClientHelper.BatRunCmd("appshutdown.bat", AppDomain.CurrentDomain.BaseDirectory, out var errorMsg);
                    if (!string.IsNullOrWhiteSpace(errorMsg))
                        throw new ApplicationException(errorMsg);
                    List<string> yingYongIdList = MemoryMappedFileHelper.GetClipBoardData();
                    if (yingYongIdList != null)
                    {
                        if (yingYongIdList.Contains(HISClientHelper.YINGYONGID + ":" + Process.GetCurrentProcess().Id.ToString()))
                        {
                            MemoryMappedFileHelper.RemoveClipBoardData(HISClientHelper.YINGYONGID, Process.GetCurrentProcess().Id.ToString());
                            yingYongIdList.Remove(HISClientHelper.YINGYONGID + ":" + Process.GetCurrentProcess().Id.ToString());
                        }
                        if (yingYongIdList.Count < 1)
                            MemoryMappedFileHelper.ClearClipBoardData();
                    }

                    // 退出系统时调用
                    ClosedProcess();

                    this.Dispose();
                    // 关闭进程
                    DisposeCurrentProcess();
                }
                else
                {
                    e.Cancel = true;
                    IsCloseBaseForm = false;
                    IsRestartLogin = false;
                }
            }
            else
            {
                e.Cancel = true;
                IsCloseBaseForm = false;
                IsRestartLogin = false;
                ResetClosingArgs();
            }
        }

        /// <summary>
        /// 重置窗体关闭信息
        /// </summary>
        public virtual void ResetClosingArgs()
        {

        }

        /// <summary>
        /// 判断当前窗体是否打开
        /// </summary>
        /// <param name="formName"></param>
        /// <returns></returns>
        public virtual bool IsOpenQXWindow(string formName)
        {
            return false;
        }
        /// <summary>
        /// 移除关闭的窗体对象
        /// </summary>
        public virtual void DesTroyPanelControls()
        {

        }

        /// <summary>
        /// 无功能id随机生成
        /// </summary>
        /// <param name="word"></param>
        /// <param name="toUpper"></param>
        /// <returns></returns>
        public static string Hash2MD516(string word, bool toUpper = true)
        {
            try
            {
                MD5CryptoServiceProvider MD5CSP = new MD5CryptoServiceProvider();

                byte[] bytValue = Encoding.UTF8.GetBytes(word);
                byte[] bytHash = MD5CSP.ComputeHash(bytValue);
                string sHash = "", sTemp = "";
                for (int counter = 0; counter < bytHash.Count(); counter++)
                {
                    long i = bytHash[counter] / 16;
                    if (i > 9)
                    {
                        sTemp = ((char)(i - 10 + 0x41)).ToString();
                    }
                    else
                    {
                        sTemp = ((char)(i + 0x30)).ToString();
                    }
                    i = bytHash[counter] % 16;
                    if (i > 9)
                    {
                        sTemp += ((char)(i - 10 + 0x41)).ToString();
                    }
                    else
                    {
                        sTemp += ((char)(i + 0x30)).ToString();
                    }
                    sHash += sTemp;
                }

                sHash = sHash.Substring(8, 16);

                bytValue = Encoding.UTF8.GetBytes(sHash);
                bytHash = MD5CSP.ComputeHash(bytValue);
                MD5CSP.Clear();
                sHash = "";
                for (int counter = 0; counter < bytHash.Count(); counter++)
                {
                    long i = bytHash[counter] / 16;
                    if (i > 9)
                    {
                        sTemp = ((char)(i - 10 + 0x41)).ToString();
                    }
                    else
                    {
                        sTemp = ((char)(i + 0x30)).ToString();
                    }
                    i = bytHash[counter] % 16;
                    if (i > 9)
                    {
                        sTemp += ((char)(i - 10 + 0x41)).ToString();
                    }
                    else
                    {
                        sTemp += ((char)(i + 0x30)).ToString();
                    }
                    sHash += sTemp;
                }

                sHash = sHash.Substring(8, 16);
                return toUpper ? sHash : sHash.ToLower();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        ///  按钮点击触发添加窗口菜单
        /// </summary>
        /// <param name="form"></param>
        /// <returns>是-未打开，否-已打开</returns>
        public virtual bool LoadButtonFireChuangKouXX(MediForm form) { return false; }
        /// <summary>
        /// 按钮打开窗体统一show方法
        /// </summary>
        /// <param name="mediFormWithQX"></param>
        public virtual void QXWindowShow(MediFormWithQX mediFormWithQX) { }
        /// <summary>
        /// 按钮打开窗体统一show方法
        /// </summary>
        /// <param name="mediFormWithQX">窗体名称</param>
        /// <param name="MethodName">方法名称</param>
        /// <param name="value">参数</param>
        public virtual void QXWindowShow(MediFormWithQX mediFormWithQX, string MethodName, object value) { }
        /// <summary>
        /// 移除相关应用
        /// </summary>
        /// <param name="form"></param>
        public virtual void RemoveCloseButtonFireChuangKouCK(XtraForm form)
        {

        }
        /// <summary>
        ///  主窗体初始化
        /// </summary>
        /// <param name="yingYongMC">应用名称</param>
        public virtual void FormInitialize(string yingYongMC)
        {
            this.ShowInTaskbar = true;
            this.Text = yingYongMC;

            Task.Factory.StartNew(() =>
            {
                ReportPrint.InitializeCache();//报表初始化特别慢，单独拿出来
            });

            InitializeCache();// 加载缓存数据(内部使用异步加载，如果外部使用异步加载可能会造成首页先加载完而缓存还在加载中，比如医生站床位牌加载)


            // 加载dll程序集
            LoadAssemblys();

            // 加载菜单信息
            CaiDanInfomation();

            // 加载皮肤信息
            //LoadSkinInfo();

            // 加载状态栏
            LoadStatusBar();

            //异步初始化，防止首次调用初始化耗时过长影响性能
            Task.Factory.StartNew(() =>
            {
                //初始化LogHelper
                var logHelper = Enterprise.Log.LogHelper.Intance;
                //初始化控制日志级别
                string RiZhiKz = GYCanShuHelper.GetCanShu("日志_IP段_客户端", "空值");
                if (!string.IsNullOrWhiteSpace(RiZhiKz) && RiZhiKz != "空值")
                    Enterprise.Log.LogHelper.InitialRiZhiKZ(RiZhiKz);
                Enterprise.Log.LogHelper.InitialJiXian(HISClientHelper.YINGWENJC);
                //WinFormLocator
                var locator = WinFormLocator.Instance;
            });
        }

        /// <summary>
        /// 通过菜单打开窗体
        /// </summary>
        public virtual void OpenCaiDanForm() { }


        /// <summary>
        /// 加载皮肤信息
        /// </summary>
        public virtual void LoadSkinInfo()
        {
            eYongHuPFXX = gYYongHuGRXXService.GetYongHuPFXXByID(HISClientHelper.USERID).Return;
        }
        /// <summary>
        /// 设置字体样式
        /// </summary>
        /// <param name="fontStyle"></param>
        /// <param name="fontSize"></param>
        public virtual void SetBarFontStyle(string fontStyle, float fontSize) { }

        #region virtual methods

        /// <summary>
        /// 初始化缓存
        /// </summary>
        public virtual void InitializeCache()
        {
            //不要使用异步，后续需要根据权限和参数加载菜单
            GYQuanXianHelper.InitializeCache(); //加载权限缓存
            GYCanShuHelper.InitializeCache(); // 加载参数缓存

            Task.Factory.StartNew(() =>
            {
                GYDataLayoutHelper.InitializeCache(); // 加载布局信息缓存
                GYChuangKouZYHelper.InitlizeCache();  // 加载窗口缓存
                GYYuanQuHelper.InitializeCache(); // 加载院区缓存
                GYYingYongHelper.InitializeCache(); // 加载应用缓存
                GYFangAnHelper.InitializeCache();  // 加载方案缓存
                ShuRuMaHelper.InitializeCache(); // 加载输入码缓存  
                GYShuJuZDHelper.InitializeCache(); // 加载数据字典缓存 
            });

        }

        /// <summary>
        /// 初始化菜单信息
        /// </summary>
        public virtual void CaiDanInfomation()
        {

        }

        #endregion

        /// <summary>
        /// 是否处于锁屏状态
        /// </summary>
        public bool IsLockScreen { get; set; }
        /// <summary>
        /// 自定义关闭程序退出提示
        /// </summary>
        [DefaultValue(false), Browsable(true)]
        public bool CustomCloseMFInfo { get; set; }
        /// <summary>
        /// 主窗体是否关闭
        /// </summary>
        public bool IsCloseBaseForm { get; set; }
        /// <summary>
        /// 鼠标是否在关闭按钮上
        /// </summary>
        public bool MouseInCloseButton { get; set; }

        /// <summary>
        /// 是否直接退出程序(无需弹窗确认)
        /// </summary>
        public bool IsExitDirectly { get; set; }

        /// <summary>
        /// 所有子窗体是否全部关闭
        /// </summary>
        public bool IsCloseAllQXForm = true;

        /// <summary>
        /// 是否重新登录
        /// </summary>
        public bool IsRestartLogin = false;

        /// <summary>
        /// 重新登录系统委托
        /// </summary>
        public delegate void ResetLoginSysDelegate();

        /// <summary>
        /// 重新登录系统变量
        /// </summary>
        public ResetLoginSysDelegate ResetLoginSysFun;
        /// <summary>
        /// 应用服务
        /// </summary>
        public JCJGYingYongCDService gYYingYongCDService;

        /// <summary>
        /// 获取用户信息
        /// </summary>
        public JCJGYongHuGRXXService gYYongHuGRXXService;
        /// <summary>
        /// 公用信息服务
        /// </summary>
        public JCJGGongYongService gongYongService;
        /// <summary>
        /// 用户皮肤信息集合
        /// </summary>
        public List<E_GY_YONGHUPFXX> eYongHuPFXX;

        /// <summary>
        /// 用户权限集合
        /// </summary>
        public List<E_GY_YONGHUQX> YongHuQXList;
        /// <summary>
        /// 系统ID
        /// </summary>
        public virtual string XiTongID { get; }

        /// <summary>
        /// 菜单集合
        /// </summary>
        public List<E_GY_CAIDAN_NEW> CanDanList;

        /// <summary>
        ///应用菜单集合
        /// </summary>
        public List<E_GY_CAIDAN_NEW> YingYongCDList;
        /// <summary>
        /// 程序集预加载
        /// </summary>
        public HashSet<Assembly> loadedAssmblies = new HashSet<Assembly>();
        /// <summary>
        /// 程序集预加载
        /// </summary>
        public ConcurrentDictionary<string, ConcurrentDictionary<string, KeyValuePair<string, Assembly>>> Assemblys = new ConcurrentDictionary<string, ConcurrentDictionary<string, KeyValuePair<string, Assembly>>>();

        protected event Action<Object, FormClosingEventArgs> MainClosing;

        //用于关闭外部接口程序
        protected event Action<Object, FormClosingEventArgs> WaiBuClosing;

        private bool GetBaseType(Type type)
        {
            if (type.BaseType == null)
            {
                return false;
            }
            else
            {
                if (type.BaseType == typeof(XtraForm))
                {
                    return true;
                }
                else
                {
                    return GetBaseType(type.BaseType);
                }
            }
        }

        /// <summary>
        /// 检查程序集
        /// </summary>
        /// <param name="path"></param>
        public virtual void CheckAssemblys(string path)
        {
            if (!Assemblys.ContainsKey(path))
            {
                Assemblys.TryAdd(path, new ConcurrentDictionary<string, KeyValuePair<string, Assembly>>());
                DirectoryInfo dirInfo = new DirectoryInfo(path);

                dirInfo.GetFiles("Mediinfo.*.*.*.*.dll", SearchOption.TopDirectoryOnly).ToList().ForEach(o =>
                {
                    Assembly assembly = Assembly.LoadFile(o.FullName); // 加载程序集（EXE 或 DLL） 
                    assembly.GetTypes().ToList().Where(p => p.BaseType == typeof(Form) || GetBaseType(p)).ToList().ForEach(p =>
                    {
                        try
                        {
                            if (!Assemblys[path].ContainsKey(p.Name.ToUpper()))
                                Assemblys[path].TryAdd(p.Name.ToUpper(), new KeyValuePair<string, Assembly>(p.FullName, assembly));
                        }
                        catch
                        {
                            MediMsgBox.Warn(this, o.FullName + "文件夹有的DLL中有重名的类");
                        }
                    });
                });
            }
        }
        /// <summary>
        /// 加载应用菜单程序集
        /// </summary>
        public virtual void LoadAssemblys()
        {
            //var path = Path.Combine(Environment.CurrentDirectory,HISClientHelper.XITONGID);
            var path = AppDomain.CurrentDomain.BaseDirectory;
            if (!Assemblys.ContainsKey(path))
            {
                Assemblys.TryAdd(path, new ConcurrentDictionary<string, KeyValuePair<string, Assembly>>());
                DirectoryInfo dirInfo = new DirectoryInfo(path);

                dirInfo.GetFiles("Mediinfo.HIS.L5.*.*.dll", SearchOption.TopDirectoryOnly).ToList().ForEach(o =>
                {
                    Assembly assembly = Assembly.LoadFile(o.FullName); // 加载程序集（EXE 或 DLL） 
                    assembly.GetTypes().ToList().Where(p => p.BaseType == typeof(Form) || GetBaseType(p)).ToList().ForEach(p =>
                    {
                        try
                        {
                            if (p.Name.ToUpper() != "MAINFORM")
                            {
                                if (!Assemblys[path].ContainsKey(p.Name.ToUpper()))
                                {
                                    Assemblys[path].TryAdd(p.Name.ToUpper(), new KeyValuePair<string, Assembly>(p.FullName, assembly));
                                }
                                else
                                {
                                    MediMsgBox.Warn(this, "窗体有重名,请联系管理员!!!\n窗体名称:" + p.Name.ToUpper() + "\n程序集名称:" + o.FullName + "\n" + "窗体名称:" + p.Name.ToUpper() + "\n程序集名称:" + Assemblys[path][p.Name.ToUpper()].Value.FullName);
                                    return;
                                }
                            }
                        }
                        catch
                        {
                            MediMsgBox.Show(o.FullName + "文件夹有的DLL中有重名的类");
                        }
                    });
                });
                dirInfo.GetFiles("Mediinfo.WinForm.*.*.dll", SearchOption.TopDirectoryOnly).ToList().ForEach(o =>
                {
                    Assembly assembly = Assembly.LoadFile(o.FullName); // 加载程序集（EXE 或 DLL） 
                    assembly.GetTypes().ToList().Where(p => p.BaseType == typeof(Form) || GetBaseType(p)).ToList().ForEach(p =>
                    {
                        try
                        {
                            if (p.Name.ToUpper() != "MAINFORM")
                            {
                                if (!Assemblys[path].ContainsKey(p.Name.ToUpper()))
                                {
                                    Assemblys[path].TryAdd(p.Name.ToUpper(), new KeyValuePair<string, Assembly>(p.FullName, assembly));
                                }
                                else
                                {
                                    MediMsgBox.Show("窗体有重名,请联系管理员!!!\n窗体名称:" + p.Name.ToUpper() + "\n程序集名称:" + o.FullName + "\n" + "窗体名称:" + p.Name.ToUpper() + "\n程序集名称:" + Assemblys[path][p.Name.ToUpper()].Value.FullName);
                                    return;
                                }
                            }
                        }
                        catch
                        {
                            MediMsgBox.Show(o.FullName + "文件夹有的DLL中有重名的类");
                        }
                    });
                });
            }
        }

        public virtual void FireMainEvent(object sender, MainFormOpenEventArgs e)
        {

        }

        /// <summary>
        /// 获取panel
        /// </summary>
        /// <returns></returns>
        public virtual PanelControl GetPanel()
        {
            return null;
        }
        /// <summary>
        /// 加载默认的状态栏
        /// </summary>
        public virtual void LoadStatusBar() { }
        /// <summary>
        /// 刷新下一个窗口名称
        /// </summary>
        public virtual void ReFreshChuangkouText(MediForm mediForm)
        {

        }

        /// <summary>
        /// 创建窗体
        /// </summary>
        /// <param name="denglucd"></param>
        /// <param name="gongnengcss"></param>
        /// <param name="fomkeys"></param>
        /// <returns></returns>
        public bool CreateFormEX(E_GY_CAIDAN_NEW denglucd, List<object> gongnengcss, params object[] fomkeys)
        {
            if (denglucd != null && denglucd.DIAOYONGCS != null)
            {
                var formname = denglucd.DIAOYONGCS.Substring(0, denglucd.DIAOYONGCS.IndexOf('|') == -1 ? denglucd.DIAOYONGCS.Length : denglucd.DIAOYONGCS.IndexOf('|'));
                var dakaifs = denglucd.DIAOYONGCS.Split('|');
                if (formname == "" || formname == "-") //二级主菜单(下面还有三级菜单)
                {

                    if (dakaifs.Length >= 3 && !string.IsNullOrWhiteSpace(dakaifs[3]) && dakaifs[3] == "EXECUTE")
                    {
                        string cmd = dakaifs[2];
                        return CommonExcute(cmd, denglucd);
                    }
                    return false;
                }

                if (dakaifs.Length >= 3 && !string.IsNullOrWhiteSpace(dakaifs[3]) && dakaifs[3] == "EXECUTE")
                {
                    string cmd = dakaifs[2];
                    return CommonExcute(cmd, denglucd);
                }
                if (formname.ToUpper().Equals("W_GY_SUODINGPM"))
                {
                    IsLockScreen = true;
                    this.Hide();
                    var path = AppDomain.CurrentDomain.BaseDirectory;
                    dynamic form = Assemblys[path][formname.ToUpper()].Value.CreateInstance(Assemblys[path][formname.ToUpper()].Key);
                    if (form != null)
                    {
                        form.ShowDialog();
                        form.Dispose();
                    }

                    return true;
                }
                if (formname.ToUpper().Equals("W_GY_XIUGAIMM"))
                {
                    var path = AppDomain.CurrentDomain.BaseDirectory;
                    dynamic form = Assemblys[path][formname.ToUpper()].Value.CreateInstance(Assemblys[path][formname.ToUpper()].Key);
                    if (form != null)
                    {
                        form.ShowDialog();
                        form.Dispose();
                    }

                    return true;
                }

            }
            return false;
        }
        /// <summary>
        /// 创建窗体
        /// </summary>
        /// <param name="denglucd"></param>
        /// <param name="gongnengcss"></param>
        /// <param name="fomkeys"></param>
        protected virtual void CreateForm(E_GY_CAIDAN_NEW denglucd, List<object> gongnengcss, params object[] fomkeys)
        {

        }

        /// <summary>
        /// 执行特殊窗体加载
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="denglucd"></param>
        protected virtual void Execute(string cmd, E_GY_CAIDAN_NEW denglucd)
        {
            CommonExcute(cmd, denglucd);
        }

        private bool CommonExcute(string cmd, E_GY_CAIDAN_NEW denglucd)
        {
            if (cmd.ToUpper() == "HELP")
            {
                return true;
            }
            else if (cmd.ToUpper().Equals("RESTART"))
            {
                IsRestartLogin = true;
                this.Close();
                return true;
            }
            else if (cmd.ToUpper() == "EXIT")
            {
                //Application.Exit();

                //退出应用调用批处理文件appshutdown.bat
                string errorMsg = string.Empty;
                string errorMsgForMemoryMapped = string.Empty;
                HISClientHelper.BatRunCmd("appshutdown.bat", AppDomain.CurrentDomain.BaseDirectory, out errorMsg);
                if (!string.IsNullOrWhiteSpace(errorMsg))
                    throw new ApplicationException(errorMsg);

                List<string> yingYongIdList = MemoryMappedFileHelper.GetClipBoardData();
                if (yingYongIdList != null)
                {
                    if (yingYongIdList.Contains(HISClientHelper.YINGYONGID + ":" + Process.GetCurrentProcess().Id.ToString()))
                    {
                        MemoryMappedFileHelper.RemoveClipBoardData(HISClientHelper.YINGYONGID, Process.GetCurrentProcess().Id.ToString());
                        yingYongIdList.Remove(HISClientHelper.YINGYONGID + ":" + Process.GetCurrentProcess().Id.ToString());
                    }
                    if (yingYongIdList.Count < 1)
                        MemoryMappedFileHelper.ClearClipBoardData();
                }
                this.Close();
                return true;
            }
            return false;
        }
        #region virtual

        /// <summary>
        /// 退出系统(退出系统时调用)
        /// </summary>
        public virtual void ClosedProcess()
        {

        }
        /// <summary>
        /// 发跨进程发自定义消息
        /// </summary>
        public virtual void SendCustomMessagForProcess() { }
        #endregion
    }
}
