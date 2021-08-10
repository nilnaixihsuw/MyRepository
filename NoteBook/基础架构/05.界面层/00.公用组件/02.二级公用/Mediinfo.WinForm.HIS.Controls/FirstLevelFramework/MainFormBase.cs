using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.DTO.HIS.XT;
using Mediinfo.Enterprise;
using Mediinfo.Enterprise.Config;
using Mediinfo.Enterprise.Log;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Mediinfo.Utility.Extensions;
using Mediinfo.WinForm.HIS.Core;

namespace Mediinfo.WinForm.HIS.Controls.FirstLevelFramework
{
    /// <summary>
    /// 应用主窗体基类
    /// </summary>
    public partial class MainFormBase : MediUniversalMFBase
    {
        /// <summary>
        /// 每个应用系统需要指定自己的系统ID，此属性只用来加载主窗口时使用
        /// </summary>
        public override string XiTongID { get; }

        /// <summary>
        /// 是否使用默认的系统菜单
        /// </summary>
        [DefaultValue(true), Browsable(true)]
        public bool UseDefaultMenu { get; set; }

        /// <summary>
        /// 重绘窗体
        /// </summary>
        [DefaultValue(true), Browsable(false)]
        public bool IsReDrawControl { get; set; }

        /// <summary>
        /// 菜单栏可见性
        /// </summary>
        public bool BarMainMenuVisible
        {
            set
            {
                MainMenu.Visible = value;
            }
        }

        /// <summary>
        /// 工具栏可见性
        /// </summary>
        public bool BarToolsVisible
        {
            set
            {
                Tools.Visible = value;
            }
        }

        /// <summary>
        /// 是否使用默认的系统工具栏
        /// </summary>
        [DefaultValue(true), Browsable(true)]
        public bool UseDefualtStatusBar { get; set; }

        /// <summary>
        /// 全局窗体一级对象缓存
        /// </summary>
        private Dictionary<string, dynamic> globalFormsCacheDic = new Dictionary<string, dynamic>();
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);
        private const int WM_SETREDRAW = 0xB;

        /// <summary>
        /// 支持相同窗体打开多次(通过菜单),
        /// </summary>
        private Dictionary<KeyValuePair<string, string>, MediForm> superForms = new Dictionary<KeyValuePair<string, string>, MediForm>();

        /// <summary>
        /// 窗体对象二级缓存
        /// </summary>
        private ConcurrentDictionary<string, dynamic> globalFormscache = new ConcurrentDictionary<string, dynamic>();

        private List<ChuangKouCD> ckList = new List<ChuangKouCD>();

        private ChuangKouCD dangqianCD = new ChuangKouCD();

        // 定义一个调用参数回传委托事件
        public delegate void DIAOYONGCANSHUDELE(string formName);

        public DIAOYONGCANSHUDELE DiaoYongCS;   // 调用参数方法
        public Form SubForm { get; set; }
        private List<E_GY_CAIDANGJL_NEW> caidangjlList;

        private BarSubItem ChuangKou { get; set; }

        private ConcurrentDictionary<string, object> gongJuLanFormCacheDictionary = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// 窗体排序
        /// </summary>
        public void SortFrmFun(Dictionary<int, KeyValuePair<string, Control>> panelInnerFrmSort, KeyValuePair<string, Control> keyValuePair)
        {
            if (panelInnerFrmSort.ToList().Where(o => o.Value.Key.Equals(keyValuePair.Key)).ToList().Count > 0)
            {
                panelInnerFrmSort.ToList().ForEach(o =>
                {
                    if (o.Value.Key.Equals(keyValuePair.Key))
                    {
                        if (o.Key == 0)
                            return;
                        else
                        {
                            KeyValuePair<string, Control> tempFrm1 = panelInnerFrmSort[0];
                            KeyValuePair<string, Control> tempFrm2 = panelInnerFrmSort[o.Key];
                            panelInnerFrmSort.Remove(0);
                            panelInnerFrmSort.Remove(o.Key);
                            panelInnerFrmSort.Add(0, keyValuePair);
                            panelInnerFrmSort.Add(o.Key, tempFrm1);
                            return;
                        }
                    }
                });
            }
            else
            {
                panelInnerFrmSort.ToList().ForEach(o =>
                {
                    if (o.Value.Value.Equals(keyValuePair.Value))
                    {
                        if (keyValuePair.Value is MediFormWithQX)
                            ((MediFormWithQX)keyValuePair.Value).CaiDanID = keyValuePair.Key;
                        panelInnerFrmSort[o.Key] = keyValuePair;
                        return;
                    }
                });

                Dictionary<int, KeyValuePair<string, Control>> tempdic
                    = panelInnerFrmSort.OrderBy(p => p.Key).ToDictionary(p => p.Key, o => o.Value);
                panelInnerFrmSort.Clear();
                int i = 0;
                tempdic.ToList().ForEach(p =>
                {
                    if (panelInnerFrmSort.Values.ToList().Where(o => o.Key == p.Value.Key).ToList().Count < 1)
                    {
                        if (tempdic.Values.ToList().Where(o => o.Key == keyValuePair.Key).ToList().Count < 1)
                        {
                            panelInnerFrmSort.Add(i + 1, p.Value);
                        }
                        else
                        {
                            panelInnerFrmSort.Add(i, p.Value);
                        }

                        i++;
                    }
                });
                if (tempdic.Count < 1)
                {
                    panelInnerFrmSort.Add(0, keyValuePair);
                }
                else
                {
                    if (panelInnerFrmSort.Values.ToList().Where(o => o.Key == keyValuePair.Key).ToList().Count < 1)
                    {
                        panelInnerFrmSort.Add(0, keyValuePair);
                    }
                }
            }
        }

        /// <summary>
        /// 移除指定元素
        /// </summary>
        /// <param name="panelInnerFrmSort"></param>
        /// <param name="keyValuePair"></param>
        public void RemoveSortFrmElement(Dictionary<int, KeyValuePair<string, Control>> panelInnerFrmSort, KeyValuePair<string, Control> keyValuePair)
        {
            List<int> keysList = new List<int>();
            if (panelInnerFrmSort.ToList().Where(o => o.Value.Equals(keyValuePair)).ToList().Count > 0)
            {
                panelInnerFrmSort.ToList().ForEach(o =>
                {
                    if (o.Value.Equals(keyValuePair))
                    {
                        keysList.Add(o.Key);
                    }
                });
            }

            foreach (int key in keysList)
            {
                panelInnerFrmSort.Remove(key);
            }

            if (panelInnerFrmSort.ContainsKey(0))
            {
                Dictionary<int, KeyValuePair<string, Control>> tempPanelInnerFrmSort = new Dictionary<int, KeyValuePair<string, Control>>();
                foreach (var item in panelInnerFrmSort.Keys)
                {
                    tempPanelInnerFrmSort.Add(item + 1, panelInnerFrmSort[item]);
                }
                panelInnerFrmSort = tempPanelInnerFrmSort;
            }
            else
            {
                if (panelInnerFrmSort.Count > 0)
                {
                    int minkey = panelInnerFrmSort.Keys.Min();
                    panelInnerFrmSort.Add(0, panelInnerFrmSort[minkey]);

                    panelInnerFrmSort.Remove(minkey);
                }
            }
        }

        /// <summary>
        /// 嵌套子窗体
        /// </summary>
        public Form CurrentSubForm { get; set; }

        private System.Threading.Timer zaiXianZtTimer = null;

        public MainFormBase()
        {
            InitializeComponent();

            this.UseDefaultMenu = true;
            this.UseDefualtStatusBar = true;

            DiaoYongCS = DiaoYongCSFun;
            this.ResizeBegin += (s, e) => { this.SuspendLayout(); };
            this.ResizeEnd += (s, e) => { this.ResumeLayout(true); };

            // 在线状态定时器，20分钟一次
            zaiXianZtTimer = new System.Threading.Timer(new TimerCallback(ZaiXianZtXt), null, Timeout.Infinite, 1200000);
        }

        /// <summary>
        /// 在线状态心跳
        /// </summary>
        /// <param name="value"></param>
        private void ZaiXianZtXt(object value)
        {
            // 注册在线状态
            E_XT_ZAIXIANZT zaiXianZt = new E_XT_ZAIXIANZT();
            zaiXianZt.ZHUANGTAIID = HISClientHelper.ZAIXIANZTID;
            zaiXianZt.ZHIGONGID = HISClientHelper.USERID;
            zaiXianZt.ZHIGONGGH = HISClientHelper.ZHIGONGGH;
            zaiXianZt.IP = HISClientHelper.IP;
            zaiXianZt.MAC = HISClientHelper.MAC;
            zaiXianZt.XITONGID = HISClientHelper.XITONGID;
            zaiXianZt.YINGYONGID = HISClientHelper.YINGYONGID;
            zaiXianZt.KESHIID = HISClientHelper.KESHIID;
            zaiXianZt.BINGQUID = HISClientHelper.BINGQUID;
            zaiXianZt.YILIAOZID = HISClientHelper.YILIAOZID;
            zaiXianZt.YUANQUID = HISClientHelper.YUANQUID;

            JCJGZhiGongService gYZhiGongService = new JCJGZhiGongService();
            var jueSeYH = gYZhiGongService.GetJueSeYHEXByYongHuID(HISClientHelper.USERID);
            if (jueSeYH.ReturnCode == ReturnCode.SUCCESS)
            {
                zaiXianZt.JUESEQX = string.Join("|", jueSeYH.Return.Select(m => m.JUESEID));
            }

            JCJGLoginService loginService = new JCJGLoginService();
            var xuZhuResult = loginService.ZaiXianZTXZ(zaiXianZt);
            if (xuZhuResult.ReturnCode == ReturnCode.SUCCESS)
            {
                HISClientHelper.ZAIXIANZTID = xuZhuResult.Return.ZHUANGTAIID;
            }
        }

        /// <summary>
        /// 加载默认的菜单及工具栏
        /// </summary>
        public virtual void LoadDefaultMenuAndToolBar()
        {
            if (CanDanList.Count < 1)
                return;
            int caidanLength = 0;
            if (CanDanList.Count > 0)
            {
                caidanLength = CanDanList.Count - 1;
            }
            if (caidanLength < 1) return;
            for (int i = caidanLength; i >= 0; i--)
            {
                string shangjicdid = CanDanList[i].SHANGJICDID;
                if (shangjicdid == "-" || shangjicdid == null)
                {
                    shangjicdid = "";
                }
                if (shangjicdid.Length > 0)
                {
                    string gongnengid = CanDanList[i].GONGNENGID;
                    if (YongHuQXList != null)
                    {
                        // 判断功能权限
                        var yonghuqx = YongHuQXList.FirstOrDefault(o => o.GONGNENGID == gongnengid);
                        if (yonghuqx == null)
                        {
                            CanDanList[i].Delete();
                        }
                    }
                }
            }

            // 一级菜单
            CanDanList.Where(o => o.SHANGJICDID == "-").OrderBy(o => o.SHUNXUHAO).ToList().ForEach(o =>
            {
                BarSubItem item = new BarSubItem();

                item.Name = o.CAIDANID;
                item.Caption = o.CAIDANMC;
                item.Enabled = o.QIYONGBZ == 1 ? false : true;
                item.ItemAppearance.Hovered.Options.UseBackColor = true;
                item.ItemAppearance.Hovered.Options.UseForeColor = true;
                item.ItemAppearance.Hovered.BackColor = Color.FromArgb(180, 215, 245);

                item.ItemAppearance.Pressed.Options.UseForeColor = true;
                item.ItemAppearance.Pressed.Options.UseBackColor = true;
                item.ItemAppearance.Pressed.BackColor = Color.FromArgb(5, 145, 206);
                item.ItemAppearance.Pressed.ForeColor = Color.White;
                if (o.CAIDANMC == "窗口()" || o.CAIDANMC == "窗口")
                {
                    ChuangKou = item;
                    if (CanDanList.Where(s => s.CAIDANMC.Contains("下一个窗口")).ToList().Count > 0)
                        CanDanList.RemoveAll(a => a.CAIDANMC.Contains("下一个窗口"));

                    CanDanList.Add(new E_GY_CAIDAN_NEW() { CAIDANID = o.CAIDANID + "001", CAIDANMC = "下一个窗口()", DIAOYONGCS = "||NEXTSHEET|EXECUTE" });

                    var nextWindowCD = new ChuangKouCD() { CAIDANID = o.CAIDANID + "001", CAIDANMC = "下一个窗口()", KEY = new KeyValuePair<string, string>(o.CAIDANID, o.GONGNENGID) };

                    ChuangKouCD(nextWindowCD, 1);
                }

                MainMenu.AddItem(item);
            });

            // 二三级菜单
            ChuangJianCD(MainMenu.LinksPersistInfo, CanDanList);

            // 工具栏
            if (caidangjlList.Count == 0)
            {
                Tools.Visible = false;
                return;
            }

            caidangjlList.ForEach(o =>
            {
                BarLargeButtonItem item = new BarLargeButtonItem();
                item.Name = o.GONGNENGID;
                item.Caption = o.GONGJULWZ + "  ";
                item.ItemAppearance.Hovered.Options.UseBackColor = true;
                item.ItemAppearance.Hovered.Options.UseForeColor = true;
                item.ItemAppearance.Hovered.BackColor = Color.FromArgb(180, 215, 245);
                string sImagePath = AppDomain.CurrentDomain.BaseDirectory + o.XIAOTUPIAN;
                if (File.Exists(sImagePath))
                    item.LargeGlyph = Image.FromFile(sImagePath);
                item.CaptionAlignment = BarItemCaptionAlignment.Right;
                Tools.AddItem(item);
                item.ItemClick += gongjulan_ItemClick;
            });
        }

        /// <summary>
        /// 加载程序集
        /// </summary>
        public override void LoadAssemblys()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            caidangjlList = gYYingYongCDService.GetYingYongGJLNewByYYID(HISClientHelper.YINGYONGID).Return.OrderBy(o => o.SHUNXUHAO).ToList();

            caidangjlList.ForEach(o =>
            {
                string chuangkoumc = o.DIAOYONGCS.Split('|').Length > 0 ? o.DIAOYONGCS.Split('|')[0] : string.Empty;
                if (!string.IsNullOrWhiteSpace(chuangkoumc) && !string.IsNullOrWhiteSpace(path) && Assemblys.ContainsKey(path) && Assemblys[path].ContainsKey(chuangkoumc.ToUpper()) && !gongJuLanFormCacheDictionary.ContainsKey(chuangkoumc.ToUpper()))
                {
                    gongJuLanFormCacheDictionary.TryAdd(chuangkoumc.ToUpper(), Assemblys[path][chuangkoumc.ToUpper()].Value.CreateInstance(Assemblys[path][chuangkoumc.ToUpper()].Key));
                }
            });
        }

        /// <summary>
        /// 加载皮肤信息
        /// </summary>
        public override void LoadSkinInfo()
        {
            if (eYongHuPFXX != null && eYongHuPFXX.Count > 0)
            {
                defaultLookAndFeel1.LookAndFeel.SkinName = eYongHuPFXX[0].PIFUMC;
                AppearanceObject.DefaultFont = new Font(eYongHuPFXX[0].ZITIMC, float.Parse(eYongHuPFXX[0].ZITIDX.ToString()));
            }

            if (eYongHuPFXX != null && eYongHuPFXX.Count > 0)
            {
                barAndDockingController1.AppearancesBar.ItemsFont = new Font(eYongHuPFXX[0].ZITIMC, float.Parse(eYongHuPFXX[0].ZITIDX.ToString()));
            }
        }

        /// <summary>
        /// 设置字体样式
        /// </summary>
        /// <param name="fontStyle">字体样式</param>
        /// <param name="fontSize">字体大小</param>
        public override void SetBarFontStyle(string fontStyle, float fontSize)
        {
            barAndDockingController1.AppearancesBar.ItemsFont = new Font(fontStyle, fontSize);
        }

        /// <summary>
        /// 加载默认的状态栏
        /// </summary>
        public override void LoadStatusBar()
        {
            barStatic_User.Caption = HISClientHelper.USERNAME;
            barStatic_KeShi.Caption = HISClientHelper.YINGYONGMC;

            barStatic_IP.Caption = HISClientHelper.IP;
            if (MediinfoConfig.GetValue("WinFormMain.xml", "RunningMode").Trim().Equals("Standalone"))
            {
                barStatic_RunningMode.Caption = "单机模式";
            }
            else if (MediinfoConfig.GetValue("WinFormMain.xml", "RunningMode").Trim().Equals("Cluster"))
            {
                barStatic_RunningMode.Caption = "集群模式";
            }
            barStatic_ServerUrl.Caption = MediinfoConfig.GetValue("WinFormMain.xml", "ServerUrl").Substring(0, MediinfoConfig.GetValue("WinFormMain.xml", "ServerUrl").Length > 0 ? MediinfoConfig.GetValue("WinFormMain.xml", "ServerUrl").IndexOf(':') : 0);
        }

        /// <summary>
        ///  主窗体初始化
        /// </summary>
        /// <param name="yingYongMC">应用名称</param>
        public override void FormInitialize(string yingYongMC)
        {
            base.FormInitialize(yingYongMC);
            this.ShowInTaskbar = true;
            this.Text = yingYongMC;
            if (UseDefaultMenu)
                LoadDefaultMenuAndToolBar();
            LoadStatusBar();
        }

        private void ChuangJianCD(LinksInfo items, List<E_GY_CAIDAN_NEW> caidanList)
        {
            foreach (LinkPersistInfo o in items)
            {
                bool breaksplit = false;
                caidanList.Where(p => p.SHANGJICDID == o.Item.Name).OrderBy(p => int.Parse(p.SHUNXUHAO)).GroupBy(p => p.CAIDANID).ToList().ForEach(q =>
                {
                    var caidan = q.FirstOrDefault();
                    if (caidan != null && caidan.CAIDANMC == "-")
                    {
                        breaksplit = true;
                    }
                    else
                    {
                        BarItem tsmi;
                        var count = caidanList.Count(r => r.SHANGJICDID == caidan.CAIDANID);
                        if (count > 0)
                        {
                            tsmi = new BarSubItem();
                        }
                        else
                        {
                            tsmi = new BarButtonItem();
                        }
                        tsmi.Name = caidan.CAIDANID;
                        tsmi.Caption = caidan.CAIDANMC;
                        tsmi.Enabled = caidan.QIYONGBZ == 1 ? false : true;
                        tsmi.ItemInMenuAppearance.Hovered.Options.UseBackColor = true;
                        tsmi.ItemInMenuAppearance.Hovered.BackColor = Color.FromArgb(10, 163, 230);
                        tsmi.ItemInMenuAppearance.Hovered.Options.UseForeColor = true;
                        tsmi.ItemInMenuAppearance.Hovered.ForeColor = Color.White;

                        tsmi.ItemClick += caidanlan_ItemClick;
                        BarItem barManag = o.Item;
                        // 必须要 要不然子菜单无法显示
                        barManag.Manager = barManager;
                        if (breaksplit)
                        {
                            (o.Item as BarSubItem)?.LinksPersistInfo.Add(new DevExpress.XtraBars.LinkPersistInfo((BarItem)tsmi, true));
                            breaksplit = false;
                        }
                        else
                        {
                            (o.Item as BarSubItem)?.LinksPersistInfo.Add(new DevExpress.XtraBars.LinkPersistInfo((BarItem)tsmi, false));
                        }
                    }
                });
                if (o.Item is BarSubItem item && item.LinksPersistInfo.Count > 0)
                {
                    ChuangJianCD(item.LinksPersistInfo, caidanList);
                }
            }
        }

        private void caidanlan_ItemClick(object sender, ItemClickEventArgs e)
        {
            var denglucd = CanDanList.FirstOrDefault(o => o.CAIDANID == e.Item.Name);
            if (denglucd == null && string.IsNullOrEmpty(e.Item.Name))
            {
                MediMsgBox.Warn("没有找到【" + HISClientHelper.YINGYONGID + "】对应的菜单ID【" + e.Item.Name + "】");
                return;
            }

            CreateForm(denglucd, null, e.Item.Tag ?? "", e.Item.Name);
        }

        /// <summary>
        /// 按钮方式打开窗体
        /// </summary>
        /// <param name="gongnengid"></param>
        /// <param name="gongnengcss"></param>
        public void OpenCaiDanFormByButton(string gongnengid, params object[] gongnengcss)
        {
            var denglucd = CanDanList.FirstOrDefault(o => o.GONGNENGID.Equals(gongnengid) && o.YINGYONGID.Equals(HISClientHelper.YINGYONGID));
            if (denglucd == null && string.IsNullOrEmpty(gongnengid))
            {
                MediMsgBox.Warn("没有找到【" + HISClientHelper.YINGYONGID + "】对应的功能ID【" + gongnengid + "】");
                return;
            }
            List<object> gongnengcs = gongnengcss.ToList();


            if (denglucd != null) CreateForm(denglucd, gongnengcs, denglucd.CAIDANID);
            else
            {
                MediMsgBox.Warn(this, "系统内部发生错误(菜单为空)，请联系管理员");
            }
        }

        private void gongjulan_ItemClick(object sender, ItemClickEventArgs e)
        {
            var denglucd = CanDanList.FirstOrDefault(o => o.GONGNENGID == e.Item.Name);
            if (denglucd == null)
            {
                MediMsgBox.Warn("没有找到【" + HISClientHelper.YINGYONGID + "】对应的功能ID【" + e.Item.Name + "】");
                return;
            }

            CreateForm(denglucd, null);
        }

        /// <summary>
        /// 异步创建窗体
        /// </summary>
        /// <param name="denglucd"></param>
        /// <param name="gongnengcss"></param>
        /// <param name="fomkeys"></param>
        protected override async void CreateForm(E_GY_CAIDAN_NEW denglucd, List<object> gongnengcss, params object[] fomkeys)
        {
            if (denglucd != null)
            {
                var formname = denglucd.DIAOYONGCS.Substring(0, denglucd.DIAOYONGCS.IndexOf('|') == -1 ? denglucd.DIAOYONGCS.Length : denglucd.DIAOYONGCS.IndexOf('|'));
                if (formname == "" || formname == "-")  // 二级主菜单(下面还有三级菜单)
                {
                    var dakaifs = denglucd.DIAOYONGCS.Split('|');
                    if (dakaifs.Length > 3 && !string.IsNullOrWhiteSpace(dakaifs[3]) && dakaifs[3] == "EXECUTE")
                    {
                        string cmd = dakaifs[2];
                        Execute(cmd, denglucd);
                    }
                    return;
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

                    return;
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

                    return;
                }

                KeyValuePair<string, string> key = new KeyValuePair<string, string>(denglucd.CAIDANID, denglucd.GONGNENGID);
                if (superForms.ContainsKey(new KeyValuePair<string, string>(denglucd.CAIDANID, denglucd.GONGNENGID)))
                {
                    superForms.Values.ToList().ForEach(o => o.Hide());
                    if (!(superForms[new KeyValuePair<string, string>(denglucd.CAIDANID, denglucd.GONGNENGID)] is MediFormWithoutTitleQX))
                    {
                        this.Text = string.Format(@"{0}-{1}", HISClientHelper.YINGYONGMC, superForms[new KeyValuePair<string, string>(denglucd.CAIDANID, denglucd.GONGNENGID)].Text);
                    }

                    if (superForms[new KeyValuePair<string, string>(denglucd.CAIDANID, denglucd.GONGNENGID)].IsDisposed)
                        return;
                    superForms[new KeyValuePair<string, string>(denglucd.CAIDANID, denglucd.GONGNENGID)].TopMost = true;
                    superForms[new KeyValuePair<string, string>(denglucd.CAIDANID, denglucd.GONGNENGID)].Visible = true;
                    foreach (Control ctr in this.GetPanel().Controls)
                    {
                        if (ctr.Equals(superForms[new KeyValuePair<string, string>(denglucd.CAIDANID, denglucd.GONGNENGID)]))
                        {
                            superForms[new KeyValuePair<string, string>(denglucd.CAIDANID, denglucd.GONGNENGID)].Show();

                            this.Text = string.Format(@"{0}-{1}", HISClientHelper.YINGYONGMC, superForms[new KeyValuePair<string, string>(denglucd.CAIDANID, denglucd.GONGNENGID)].Text);
                        }
                        else
                        {
                            ctr.Hide();
                        }
                    }

                    SortFrmFun(panelInnerFrmSort, new KeyValuePair<string, Control>(denglucd.CAIDANID, superForms[new KeyValuePair<string, string>(denglucd.CAIDANID, denglucd.GONGNENGID)]));
                    SetSelectedCDID(denglucd.CAIDANID, superForms[new KeyValuePair<string, string>(denglucd.CAIDANID, denglucd.GONGNENGID)]);
                }
                else
                {
                    var dakaifs = denglucd.DIAOYONGCS.Split('|');
                    if (dakaifs.Length > 3 && !string.IsNullOrWhiteSpace(dakaifs[3]) && dakaifs[3] == "EXECUTE")
                    {
                        string cmd = dakaifs[2];
                        Execute(cmd, denglucd);
                    }
                    else
                    {
                        if (!dakaifs[3].ToUpper().Equals("OPEN"))
                            superForms.Remove(new KeyValuePair<string, string>(denglucd.CAIDANID, denglucd.GONGNENGID));
                        var path = AppDomain.CurrentDomain.BaseDirectory;

                        if (Assemblys[path].ContainsKey(formname.ToUpper()))
                        {
                            dynamic form;
                            if (globalFormsCacheDic.Count > 0)
                            {
                                if (globalFormsCacheDic.ContainsKey(Assemblys[path][formname.ToUpper()].Value.FullName + formname.ToUpper()))
                                {
                                    form = globalFormsCacheDic[Assemblys[path][formname.ToUpper()].Value.FullName + formname.ToUpper()];

                                    if (form is MediFormWithQX qx && qx.GongNengId != dakaifs[1])
                                    {
                                        form = Assemblys[path][formname.ToUpper()].Value.CreateInstance(Assemblys[path][formname.ToUpper()].Key);
                                    }
                                }
                                else
                                {
                                    if (gongJuLanFormCacheDictionary.ContainsKey(formname.ToUpper()))
                                    {
                                        form = gongJuLanFormCacheDictionary[formname.ToUpper()];
                                    }
                                    else
                                    {
                                        if (globalFormscache.ContainsKey(formname.ToUpper()))
                                        {
                                            gongJuLanFormCacheDictionary.TryAdd(formname.ToUpper(), globalFormscache[formname.ToUpper()]);
                                            form = gongJuLanFormCacheDictionary[formname.ToUpper()];

                                            globalFormscache.TryRemove(formname.ToUpper(), out _);
                                        }
                                        else
                                        {
                                            form = Assemblys[path][formname.ToUpper()].Value.CreateInstance(Assemblys[path][formname.ToUpper()].Key);
                                        }
                                    }

                                    if (form is MediFormWithQX)
                                    {
                                        globalFormsCacheDic.Add(Assemblys[path][formname.ToUpper()].Value.FullName + formname.ToUpper(), form);
                                    }
                                }
                            }
                            else
                            {
                                if (gongJuLanFormCacheDictionary.ContainsKey(formname.ToUpper()))
                                {
                                    form = gongJuLanFormCacheDictionary[formname.ToUpper()];
                                }
                                else
                                {
                                    if (globalFormscache.ContainsKey(formname.ToUpper()))
                                    {
                                        gongJuLanFormCacheDictionary.TryAdd(formname.ToUpper(), globalFormscache[formname.ToUpper()]);
                                        form = gongJuLanFormCacheDictionary[formname.ToUpper()];
                                        globalFormscache.TryRemove(formname.ToUpper(), out _);
                                    }
                                    else
                                    {
                                        form = Assemblys[path][formname.ToUpper()].Value.CreateInstance(Assemblys[path][formname.ToUpper()].Key);
                                    }
                                }
                                if (form is MediFormWithQX)
                                    globalFormsCacheDic.Add(Assemblys[path][formname.ToUpper()].Value.FullName + formname.ToUpper(), form);
                            }
                            if (IsCompelete)
                            {
                                IsCompelete = false;
                                clientFrmCacheWorker.RunWorkerAsync(formname.ToUpper());
                            }

                            SubForm = form;
                            if (form != null)
                            {
                                form.MdiParent = this;
                                form.MaximizeBox = false;
                                form.MinimizeBox = false;
                                if (((Control)form).Name == "W_GY_DANJUCX")
                                {
                                    ((Control)form).Text = denglucd.CAIDANMC;
                                }

                                form.FormBorderStyle = FormBorderStyle.None;
                                form.ControlBox = false;
                                form.Dock = DockStyle.Fill;
                                form.DiaoYongCS = denglucd.DIAOYONGCS;
                                form.CaiDanID = denglucd.CAIDANID;
                                form.GongNengCS = gongnengcss;
                                form.FormClosed += new FormClosedEventHandler(MainFormBase_FormClosed);
                                HISClientHelper.ower = new WeakReference(form);
                                if (!(form is MediFormWithoutTitleQX))
                                {
                                    this.Text = string.Format("{0}-{1}", HISClientHelper.YINGYONGMC, form.Text);
                                }

                                panel.Controls.Add(form);
                                SortFrmFun(panelInnerFrmSort,
                                    new KeyValuePair<string, Control>(denglucd.CAIDANID, form));
                                if (dakaifs.Length > 3 && !string.IsNullOrWhiteSpace(dakaifs[3]))
                                {
                                    switch (dakaifs[3])
                                    {
                                        case "OPEN":
                                            form.MdiParent = null;
                                            form.WindowState = FormWindowState.Normal;
                                            form.MinimizeBox = false;
                                            form.MaximizeBox = false;
                                            form.FormBorderStyle = FormBorderStyle.FixedSingle;
                                            form.StartPosition = FormStartPosition.CenterParent;

                                            if (form is MediFormWithQX frm)
                                            {
                                                frm.TextChanged += Frm_TextChanged;
                                                frm.Load += Frm_Load;
                                            }

                                            form.ShowDialog(this);
                                            form.Dispose();
                                            break;
                                        case "OPENSHEET":
                                            superForms.Values.ToList().ForEach(o =>
                                            {
                                                o.Hide();
                                            });
                                            form.WindowState = FormWindowState.Maximized;
                                            form.TopMost = true;
                                            superForms.Add(new KeyValuePair<string, string>(denglucd.CAIDANID, denglucd.GONGNENGID), form);

                                            // 加载到“窗口”显示
                                            LoadChuangKouXX(denglucd, key, form);
                                            SendMessage(form.Handle, WM_SETREDRAW, 0, IntPtr.Zero); //禁止重绘
                                            form.Show();
                                            break;
                                    }
                                }
                                else
                                {
                                    superForms.Values.ToList().ForEach(o => { o.Hide(); });
                                    form.WindowState = FormWindowState.Maximized;
                                    form.TopMost = true;
                                    superForms.Add(
                                        new KeyValuePair<string, string>(denglucd.CAIDANID, denglucd.GONGNENGID), form);
                                    LoadChuangKouXX(denglucd, key, form);
                                    form.Show();
                                }
                            }
                        }
                        else
                        {
                            MediMsgBox.Info(formname + "窗体在DLL中没有找到");
                        }
                    }
                }
            }
            else
            {
                if (fomkeys.Length > 1)
                {
                    if (superForms.ContainsKey((KeyValuePair<string, string>)fomkeys[0]) && superForms[(KeyValuePair<string, string>)fomkeys[0]] != null && !superForms[(KeyValuePair<string, string>)fomkeys[0]].IsDisposed)
                    {
                        superForms.Values.ToList().ForEach(o => { o.Hide(); });
                        superForms[(KeyValuePair<string, string>)fomkeys[0]].TopMost = true;
                        if (!(superForms[(KeyValuePair<string, string>)fomkeys[0]] is MediFormWithoutTitleQX))
                        {
                            this.Text = string.Format(@"{0}-{1}", HISClientHelper.YINGYONGMC, superForms[(KeyValuePair<string, string>)fomkeys[0]].Text);
                        }
                        superForms[(KeyValuePair<string, string>)fomkeys[0]].Visible = true;
                        superForms[(KeyValuePair<string, string>)fomkeys[0]].Show();
                        SortFrmFun(panelInnerFrmSort, new KeyValuePair<string, Control>(((KeyValuePair<string, string>)fomkeys[0]).Key, superForms[(KeyValuePair<string, string>)fomkeys[0]]));
                        SetSelectedCDID(fomkeys[1].ToString(), superForms[(KeyValuePair<string, string>)fomkeys[0]]);
                    }
                }
            }
        }

        private void Frm_Load(object sender, EventArgs e)
        {
            SendMessage(((Control)sender).Handle, WM_SETREDRAW, 1, IntPtr.Zero);//取消禁止
            ((Control)sender).Refresh();
        }

        /// <summary>
        /// 窗体text改变触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_TextChanged(object sender, EventArgs e)
        {
            if (sender is MediFormWithQX frm && !(frm is MediFormWithoutTitleQX))
            {
                this.Text = string.Format(@"{0}-{1}", HISClientHelper.YINGYONGMC, frm.Text);
            }
        }

        private void LoadChuangKouXX(E_GY_CAIDAN_NEW denglucd, KeyValuePair<string, string> key, dynamic form)
        {
            var eChuangKou = new ChuangKouCD() { CAIDANID = denglucd.CAIDANID, CAIDANMC = denglucd.CAIDANMC, KEY = key, XForm = form };

            ChuangKouCD(eChuangKou, 1);
        }

        /// <summary>
        /// 按钮打开窗体统一show方法
        /// </summary>
        /// <param name="mediFormWithQX"></param>
        public override void QXWindowShow(MediFormWithQX mediFormWithQX)
        {
            string buttonid = string.Format("{0}\\{1}", HISClientHelper.YINGYONGID, mediFormWithQX.Name);
            string caidanid = ((int)Convert.ToInt64(Hash2MD516(buttonid), 16)).ToString();
            SortFrmFun(panelInnerFrmSort, new KeyValuePair<string, Control>(caidanid, mediFormWithQX));
            if (mediFormWithQX.CaiDanID == null || !mediFormWithQX.CaiDanID.Equals(SelectedCDID))
            {
                foreach (BarItemLink item in ChuangKou.ItemLinks)
                    item.Item.PaintStyle = BarItemPaintStyle.CaptionInMenu;
            }
            mediFormWithQX.MdiParent = this;
            mediFormWithQX.MaximizeBox = false;
            mediFormWithQX.MinimizeBox = false;

            mediFormWithQX.FormBorderStyle = FormBorderStyle.None;
            mediFormWithQX.ControlBox = false;
            mediFormWithQX.Dock = DockStyle.Fill;
            mediFormWithQX.FormClosed += MediFormWithQX_FormClosed;
            if (!(mediFormWithQX is MediFormWithoutTitleQX))
            {
                this.Text = string.Format(@"{0}-{1}", HISClientHelper.YINGYONGMC, mediFormWithQX.Text);
            }
            panel.Controls.Add(mediFormWithQX);

            superForms.Values.ToList().ForEach(o => { o.Hide(); });

            mediFormWithQX.WindowState = FormWindowState.Maximized;
            mediFormWithQX.TopMost = true;
            mediFormWithQX.CaiDanID = caidanid;
            mediFormWithQX.Show();
        }

        /// <summary>
        /// 判断当前窗体是否打开
        /// </summary>
        /// <param name="formName"></param>
        /// <returns></returns>
        public override bool IsOpenQXWindow(string formName)
        {
            if (globalFormsCacheDic.ContainsKey(formName.ToUpper()))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 按钮打开窗体统一show方法
        /// </summary>
        /// <param name="mediFormWithQX">窗体名称</param>
        /// <param name="MethodName">方法名称</param>
        /// <param name="value">参数</param>
        public override void QXWindowShow(MediFormWithQX mediFormWithQX, string MethodName, object value)
        {
            MediFormWithQX tempMediFormWithQX = null;
            if (globalFormsCacheDic.Count > 0)
            {
                if (globalFormsCacheDic.ContainsKey(mediFormWithQX.GetType().Assembly.FullName + mediFormWithQX.Name.ToUpper()))
                {
                    tempMediFormWithQX = globalFormsCacheDic[mediFormWithQX.GetType().Assembly.FullName + mediFormWithQX.Name.ToUpper()];
                }
                else
                {
                    tempMediFormWithQX = mediFormWithQX;
                    globalFormsCacheDic.Add(mediFormWithQX.GetType().Assembly.FullName + mediFormWithQX.Name.ToUpper(), mediFormWithQX);
                }
            }
            else
            {
                tempMediFormWithQX = mediFormWithQX;
                globalFormsCacheDic.Add(mediFormWithQX.GetType().Assembly.FullName + mediFormWithQX.Name.ToUpper(), mediFormWithQX);
            }
            LoadButtonFireChuangKouXX(tempMediFormWithQX);

            if (MethodName != null)
            {
                SetQXWindowValue(tempMediFormWithQX, MethodName, value);
            }
        }

        /// <summary>
        /// 打开窗体时候调用方法
        /// </summary>
        /// <param name="mediFormWithQX">窗体名称</param>
        /// <param name="MethodName">方法名称</param>
        /// <param name="value">参数</param>
        private void SetQXWindowValue(MediFormWithQX mediFormWithQX, string MethodName, object value)
        {
            MethodInfo method = mediFormWithQX.GetType().GetMethod(MethodName);
            if (method != null)
            {
                object[] obj = new object[1];
                obj[0] = value;
                method.Invoke(mediFormWithQX, obj);
            }
        }

        private void MediFormWithQX_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (globalFormsCacheDic.Count > 0)
            {
                Form form = sender as Form;
                if (globalFormsCacheDic.ContainsKey(form.GetType().Assembly.FullName + form.Name.ToUpper()))
                    globalFormsCacheDic.Remove(form.GetType().Assembly.FullName + form.Name.ToUpper());
            }
            if (superForms == null || superForms.Count == 0) return;
            KeyValuePair<string, string> currentFormKey = new KeyValuePair<string, string>("", "");
            foreach (KeyValuePair<string, string> key in superForms.Keys)
            {
                if (superForms[key] == sender as XtraForm)
                {
                    currentFormKey = key;
                }
            }

            superForms.Remove(currentFormKey);

            // 对窗口菜单进行删除
            var currentForm = ckList.Where(o => o.KEY.Equals(currentFormKey)).FirstOrDefault();
            Execute("NEXTSHEET", new E_GY_CAIDAN_NEW());
            if (currentForm != null)
            {
                ckList.Remove(currentForm);
            }
            ChuangKou.ItemLinks.Clear();
            if (ckList.Count > 0)
            {
                for (int i = 0; i < ckList.Count; i++)
                {
                    //ckList[i].CAIDANMC = i + ckList[i].CAIDANMC;
                    ChuangKouCD(ckList[i], 2);
                }
            }

            if (superForms == null || superForms.Count == 0)
            {
                //HB6-204(418072)【退出菜单】关闭菜单回到系统主界面，左上角的菜单名称清空
                this.Text = HISClientHelper.YINGYONGMC;

                return;
            }

            int icount = superForms.Count;
            int index = 0;
            KeyValuePair<string, string> keyName = new KeyValuePair<string, string>("", "");
            foreach (KeyValuePair<string, string> key in superForms.Keys)
            {
                if (index == icount - 1)
                {
                    keyName = key;
                }
                index++;
            }
            if (superForms[keyName].IsDisposed)
                return;
            superForms.Values.ToList().ForEach(o => { o.Hide(); });
            superForms[keyName].TopMost = true;
            if (!(superForms[keyName] is MediFormWithoutTitleQX))
            {
                this.Text = string.Format("{0}-{1}", HISClientHelper.YINGYONGMC, superForms[keyName].Text);
            }
            // superForms[keyName].WindowState = FormWindowState.Normal;
            superForms[keyName].Show();
        }

        /// <summary>
        ///  按钮点击触发添加窗口菜单
        /// </summary>
        /// <param name="form"></param>
        /// <returns>是-未打开，否-已打开</returns>
        public override bool LoadButtonFireChuangKouXX(MediForm form)
        {
            string buttonid = string.Format("{0}\\{1}", HISClientHelper.YINGYONGID, form.Name);
            string caidanid = ((int)Convert.ToInt64(Hash2MD516(buttonid), 16)).ToString();
            KeyValuePair<string, string> formKey = new KeyValuePair<string, string>(caidanid, HISClientHelper.YINGYONGID);
            var count = ckList.Count - 1;
            var eChuangKou = new ChuangKouCD() { CAIDANID = caidanid, CAIDANMC = form.Text, KEY = formKey, XForm = form };

            if (!superForms.ContainsKey(formKey) && !globalFormsCacheDic.ContainsKey(form.GetType().Assembly.FullName + form.Name.ToUpper()))
            {
                ChuangKouCD(eChuangKou, 1);
                superForms.Add(formKey, form);
                SortFrmFun(panelInnerFrmSort, new KeyValuePair<string, Control>(caidanid, form));
                return true;
            }
            else
            {
                if (superForms.ContainsKey(formKey) && superForms[formKey] != null && !superForms[formKey].IsDisposed)
                {
                    superForms.Values.ToList().ForEach(o => { o.Hide(); });
                    superForms[formKey].TopMost = true;

                    if (!(superForms[formKey] is MediFormWithoutTitleQX))
                    {
                        this.Text = string.Format("{0}-{1}", HISClientHelper.YINGYONGMC, superForms[formKey].Text);
                    }

                    superForms[formKey].Visible = true;
                    ((MediFormWithQX)superForms[formKey]).CaiDanID = caidanid;
                    SortFrmFun(panelInnerFrmSort, new KeyValuePair<string, Control>(caidanid, superForms[formKey]));
                    superForms[formKey].Show();
                    SetSelectedCDID(eChuangKou.CAIDANID, eChuangKou.XForm);
                    //SetSelected();
                }
                else if (globalFormsCacheDic.ContainsKey(form.GetType().Assembly.FullName + form.Name.ToUpper()))
                {
                    globalFormsCacheDic.Values.ToList().ForEach(o => { o.Hide(); });
                    globalFormsCacheDic[form.GetType().Assembly.FullName + form.Name.ToUpper()].TopMost = true;

                    if (!(globalFormsCacheDic[form.GetType().Assembly.FullName + form.Name.ToUpper()] is MediFormWithoutTitleQX))
                    {
                        this.Text = string.Format("{0}-{1}", HISClientHelper.YINGYONGMC, globalFormsCacheDic[form.GetType().Assembly.FullName + form.Name.ToUpper()].Text);
                    }
                    superForms.Add(formKey, form);
                    globalFormsCacheDic.Values.ToList().ForEach(o => o.Hide());

                    globalFormsCacheDic[form.GetType().Assembly.FullName + form.Name.ToUpper()].MdiParent = this;
                    // globalFormsCacheDic[form.GetType().Assembly.FullName + form.Name.ToUpper()].Visible = true;
                    globalFormsCacheDic[form.GetType().Assembly.FullName + form.Name.ToUpper()].FormBorderStyle = FormBorderStyle.None;
                    globalFormsCacheDic[form.GetType().Assembly.FullName + form.Name.ToUpper()].Dock = DockStyle.Fill;
                    this.panel.Controls.Add(globalFormsCacheDic[form.GetType().Assembly.FullName + form.Name.ToUpper()]);
                    globalFormsCacheDic[form.GetType().Assembly.FullName + form.Name.ToUpper()].WindowState = FormWindowState.Maximized;
                    globalFormsCacheDic[form.GetType().Assembly.FullName + form.Name.ToUpper()].TopMost = true;
                    ((MediFormWithQX)globalFormsCacheDic[form.GetType().Assembly.FullName + form.Name.ToUpper()]).CaiDanID = caidanid;
                    SortFrmFun(panelInnerFrmSort, new KeyValuePair<string, Control>(caidanid, globalFormsCacheDic[form.GetType().Assembly.FullName + form.Name.ToUpper()]));
                    ChuangKouCD(eChuangKou, 1);
                    globalFormsCacheDic[form.GetType().Assembly.FullName + form.Name.ToUpper()].Show();

                }
                return false;
            }
        }

        /// <summary>
        /// 移除相关应用
        /// </summary>
        /// <param name="form"></param>
        public override void RemoveCloseButtonFireChuangKouCK(XtraForm form)
        {
            if (globalFormsCacheDic.Count > 0)
            {

                if (globalFormsCacheDic.ContainsKey(form.GetType().Assembly.FullName + form.Name.ToUpper()))
                    globalFormsCacheDic.Remove(form.GetType().Assembly.FullName + form.Name.ToUpper());
            }
            if (gongJuLanFormCacheDictionary.Count > 0)
            {
                if (gongJuLanFormCacheDictionary.ContainsKey(form.Name.ToUpper()))
                {
                    dynamic retform;
                    gongJuLanFormCacheDictionary.TryRemove(form.Name.ToUpper(), out retform);
                }
            }

            if (panelInnerFrmSort.Values.Contains(new KeyValuePair<string, Control>(((MediFormWithQX)form).CaiDanID, form)))
            {
                RemoveSortFrmElement(panelInnerFrmSort, new KeyValuePair<string, Control>(((MediFormWithQX)form).CaiDanID, form));
                if (panelInnerFrmSort.Count > 0)
                    panelInnerFrmSort[0].Value.Show();

            }

            if (superForms == null || superForms.Count == 0)
            {
                //HB6-204(418072)【退出菜单】关闭菜单回到系统主界面，左上角的菜单名称清空
                this.Text = HISClientHelper.YINGYONGMC;

                if (this.GetPanel().Controls.Count - 1 > 0 && this.GetPanel().Controls.Count > this.GetPanel().Controls.IndexOf(form) + 1)
                {
                    this.GetPanel().Controls[this.GetPanel().Controls.IndexOf(form) + 1].Show();
                    this.Text = string.Format("{0}-{1}", HISClientHelper.YINGYONGMC, this.GetPanel().Controls[0].Text);
                }
                return;
            }
            KeyValuePair<string, string> currentFormKey = new KeyValuePair<string, string>("", "");
            foreach (KeyValuePair<string, string> key in superForms.Keys)
            {
                if (superForms[key] == form)
                {
                    currentFormKey = key;
                }
            }

            if (string.IsNullOrWhiteSpace(currentFormKey.Key) || string.IsNullOrWhiteSpace(currentFormKey.Value))
            {
                if (this.GetPanel().Controls.Count < 1)
                {
                    this.Text = HISClientHelper.YINGYONGMC;
                    return;
                }
                if (this.GetPanel().Controls.Contains(form))
                {
                    form.Dispose();
                    this.GetPanel().Controls.Remove(form);
                }

                foreach (Control control in this.GetPanel().Controls)
                {
                    if (control.Visible)
                    {
                        this.Text = string.Format("{0}-{1}", HISClientHelper.YINGYONGMC, control.Text);
                    }
                }
                return;
            }

            superForms.Remove(currentFormKey);

            // 对窗口菜单进行删除
            var currentForm = ckList.Where(o => o.KEY.Equals(currentFormKey)).FirstOrDefault();
            Execute("NEXTSHEET", new E_GY_CAIDAN_NEW());
            if (currentForm != null)
            {
                ckList.Remove(currentForm);
            }
            ChuangKou.ItemLinks.Clear();
            if (ckList.Count > 0)
            {
                for (int i = 0; i < ckList.Count; i++)
                {
                    //ckList[i].CAIDANMC = i + ckList[i].CAIDANMC;
                    ChuangKouCD(ckList[i], 2);
                }
            }

            if (superForms == null || superForms.Count == 0)
            {
                // HB6-204(418072)【退出菜单】关闭菜单回到系统主界面，左上角的菜单名称清空
                this.Text = HISClientHelper.YINGYONGMC;

                if (this.GetPanel().Controls.Count - 1 > 0 && this.GetPanel().Controls.Count > this.GetPanel().Controls.IndexOf(form) + 1)
                {
                    this.GetPanel().Controls[this.GetPanel().Controls.IndexOf(form) + 1].Show();
                    this.Text = string.Format("{0}-{1}", HISClientHelper.YINGYONGMC, this.GetPanel().Controls[0].Text);
                }

                return;
            }

            int icount = superForms.Count;
            int index = 0;
            KeyValuePair<string, string> keyName = new KeyValuePair<string, string>("", "");
            foreach (KeyValuePair<string, string> key in superForms.Keys)
            {
                if (index == icount - 1)
                {
                    keyName = key;
                }
                index++;
            }
            superForms.Values.ToList().ForEach(o => { o.Hide(); });
            superForms[keyName].TopMost = true;
            superForms[keyName].Show();
        }

        protected override void Execute(string cmd, E_GY_CAIDAN_NEW denglucd)
        {
            base.Execute(cmd, denglucd);
            if (cmd == "OPEN" || cmd == "NEXTSHEET")//“窗口”菜单中 下一个菜单
            {
                if (ckList.Where(o => o.CAIDANMC.Equals("下一个窗口()")).ToList().Count > 0)
                {
                    if (ckList.Count <= 2)
                    {
                        return;
                    }
                }

                var index = ckList.FindIndex(o => o.CAIDANID == SelectedCDID);
                if (index < 0)
                {
                    return;
                }
                else
                {
                    index = index + 1;
                }

                if (index == ckList.Count) //
                {
                    KeyValuePair<string, string> key;
                    if (ckList.Where(o => o.CAIDANMC.Equals("下一个窗口()")).ToList().Count > 0)
                    {
                        key = ckList[1].KEY;
                        if (!key.Value.Equals("") && !key.Key.Equals(""))
                        {
                            if (superForms.ContainsKey(key) && superForms[key] != null && !superForms[key].IsDisposed)
                            {
                                superForms.Values.ToList().ForEach(o => { o.Hide(); });
                                superForms[key].TopMost = true;
                                if (!(superForms[key] is MediFormWithoutTitleQX))
                                {
                                    this.Text = string.Format("{0}-{1}", HISClientHelper.YINGYONGMC, superForms[key].Text);
                                }

                                superForms[key].Show();

                                SetSelectedCDID(ckList[1].CAIDANID, ckList[1].XForm);
                            }
                        }
                    }
                    else
                    {
                        if (superForms != null && superForms.Count > 0)
                        {
                            superForms.Values.ToList().ForEach(o => { o.Hide(); });
                            superForms.Last().Value.TopMost = true;
                            if (!(superForms.Last().Value is MediFormWithoutTitleQX))
                            {
                                this.Text = string.Format("{0}-{1}", HISClientHelper.YINGYONGMC, superForms.Last().Value.Text);
                            }

                            superForms.Last().Value.Show();
                            if (ckList.Where(o => o.CAIDANMC.Contains("下一个窗口")).ToList().Count > 0)
                                SetSelectedCDID(ckList[1].CAIDANID, ckList[1].XForm);
                            else
                                SetSelectedCDID(ckList[0].CAIDANID, ckList[0].XForm);

                        }
                    }
                }
                else if (index < ckList.Count)
                {
                    if (ckList.Where(o => o.CAIDANMC.Equals("下一个窗口()")).ToList().Count > 0)
                    {
                        var key = ckList[index].KEY;
                        if (superForms.ContainsKey(key) && superForms[key] != null && !superForms[key].IsDisposed)
                        {
                            superForms.Values.ToList().ForEach(o => { o.Hide(); });
                            superForms[key].TopMost = true;
                            if (!(superForms[key] is MediFormWithoutTitleQX))
                            {
                                this.Text = string.Format("{0}-{1}", HISClientHelper.YINGYONGMC, superForms[key].Text);
                            }

                            superForms[key].Show();
                            SetSelectedCDID(ckList[index].CAIDANID, ckList[index].XForm);
                        }
                    }
                    else
                    {
                        if (superForms != null && superForms.Count > 0)
                        {
                            superForms.Values.ToList().ForEach(o => { o.Hide(); });
                            superForms.Last().Value.TopMost = true;
                            if (!(superForms.Last().Value is MediFormWithoutTitleQX))
                            {
                                this.Text = string.Format("{0}-{1}", HISClientHelper.YINGYONGMC, superForms.Last().Value.Text);
                            }

                            superForms.Last().Value.Show();

                            SetSelectedCDID(ckList[1].CAIDANID, ckList[1].XForm);
                        }
                    }
                }
            }
            //else if (cmd.ToUpper() == "HELP")
            //{
            //}
            //else if (cmd.ToUpper().Equals("RESTART"))
            //{
            //    IsRestartLogin = true;
            //    this.Close();
            //}
            //else if (cmd.ToUpper() == "EXIT")
            //{
            //    //Application.Exit();

            //    //退出应用调用批处理文件appshutdown.bat
            //    string errorMsg = string.Empty;
            //    string errorMsgForMemoryMapped = string.Empty;
            //    HISClientHelper.BatRunCmd("appshutdown.bat", AppDomain.CurrentDomain.BaseDirectory, out errorMsg);
            //    if (!string.IsNullOrWhiteSpace(errorMsg))
            //        throw new ApplicationException(errorMsg);

            //    List<string> yingYongIdList = MemoryMappedFileHelper.GetClipBoardData();
            //    if (yingYongIdList != null)
            //    {
            //        if (yingYongIdList.Contains(HISClientHelper.YINGYONGID + ":" + Process.GetCurrentProcess().Id.ToString()))
            //        {
            //            MemoryMappedFileHelper.RemoveClipBoardData(HISClientHelper.YINGYONGID, Process.GetCurrentProcess().Id.ToString());
            //            yingYongIdList.Remove(HISClientHelper.YINGYONGID + ":" + Process.GetCurrentProcess().Id.ToString());
            //        }
            //        if (yingYongIdList.Count < 1)
            //            MemoryMappedFileHelper.ClearClipBoardData();
            //    }
            //    this.Close();
            //}
        }

        public string SelectedCDID { get; set; }

        private void SetSelectedCDID(string caidanid, MediForm mediForm)
        {
            SelectedCDID = caidanid;
            SetSelected(mediForm);
        }

        /// <summary>
        /// 刷新下一个窗口名称
        /// </summary>
        public override void ReFreshChuangkouText(MediForm mediForm)
        {
            SetSelected(mediForm);
        }

        //“窗口”菜单进行添加 leixing 1表示 单击插入一个菜单，2表示循环多次插入菜单
        private void ChuangKouCD(ChuangKouCD eChuangKouCD, int leixing)
        {
            BarButtonItem tsmi;
            int count = 0;
            if (leixing == 1)
            {
                count = ckList.Where(o => o.KEY.Equals(eChuangKouCD.KEY)).Count();
            }

            if (count > 0)
            {
                return;
            }
            else
            {
                tsmi = new BarButtonItem();
            }
            if (leixing == 1)
            {
                if (ckList.Where(o => o.CAIDANMC.Equals(eChuangKouCD.CAIDANMC)).Count() > 0)
                {
                    ckList.RemoveAll(o => o.CAIDANMC.Equals(eChuangKouCD.CAIDANMC));

                }
                ckList.Add(eChuangKouCD);

            }

            if (ChuangKou != null)
                ChuangKou.ItemLinks.Remove(ChuangKou.ItemLinks.Where(o => o.Item.Caption == eChuangKouCD.CAIDANMC).ToList().FirstOrDefault());

            tsmi.Tag = eChuangKouCD.KEY;
            tsmi.Id = Convert.ToInt32(eChuangKouCD.CAIDANID);
            tsmi.Name = eChuangKouCD.CAIDANID;
            tsmi.Caption = eChuangKouCD.CAIDANMC;
            //tsmi.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            tsmi.ItemInMenuAppearance.Hovered.Options.UseBackColor = true;
            tsmi.ItemInMenuAppearance.Hovered.BackColor = Color.FromArgb(10, 163, 230);
            tsmi.ItemInMenuAppearance.Hovered.Options.UseForeColor = true;
            tsmi.ItemInMenuAppearance.Hovered.ForeColor = Color.White;
            tsmi.ItemClick += new ItemClickEventHandler(caidanlan_ItemClick);
            if (ChuangKou == null)
            {
                ChuangKou = new BarSubItem();
            }
            ChuangKou.AddItem(tsmi);
            // 这个放到后面是为了  SetSelected();
            if (leixing == 1)
            {
                SetSelectedCDID(eChuangKouCD.CAIDANID, eChuangKouCD.XForm);
            }

            //ChuangKou.LinksPersistInfo.Add(new DevExpress.XtraBars.LinkPersistInfo((BarItem)tsmi, false));
        }

        //设置“窗口”菜单中当前选择项 前面的 对号 SetSelected();
        private void SetSelected(MediForm mediForm)
        {
            if (ChuangKou == null || ChuangKou.ItemLinks == null)
                return;
            for (int i = 0; i < ChuangKou.ItemLinks.Count; i++)
            {
                if (ChuangKou.ItemLinks[i].Item.Name == SelectedCDID)
                {
                    ChuangKou.ItemLinks[i].Item.PaintStyle = BarItemPaintStyle.CaptionGlyph;
                    ChuangKou.ItemLinks[i].Item.Glyph = imageList1.Images[0];
                    for (int j = 0; j < ChuangKou.ItemLinks.Count; j++)
                    {
                        BarItem barItem = ChuangKou.ItemLinks[i].Item;
                        if (barItem.Name.Equals(SelectedCDID))
                        {
                            if (superForms.ContainsValue(mediForm))
                            {
                                barItem.Caption = this.Text.Substring(this.Text.IndexOf('-') + 1);
                            }
                            else
                            {
                                ChuangKou.ItemLinks[i].Item.PaintStyle = BarItemPaintStyle.CaptionInMenu;
                            }
                        }
                    }
                }
                else
                {
                    ChuangKou.ItemLinks[i].Item.PaintStyle = BarItemPaintStyle.CaptionInMenu;
                }
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
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
        }

        /// <summary>
        /// 获取panel
        /// </summary>
        /// <returns></returns>
        public override PanelControl GetPanel()
        {
            return this.panel;
        }

        /// <summary>
        /// 窗体关闭时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFormBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (globalFormsCacheDic.Count > 0)
            {
                Form form = sender as Form;
                if (globalFormsCacheDic.ContainsKey(form.GetType().Assembly.FullName + form.Name.ToUpper()))
                    globalFormsCacheDic.Remove(form.GetType().Assembly.FullName + form.Name.ToUpper());
            }
            if (superForms == null || superForms.Count == 0) return;

            KeyValuePair<string, string> currentFormKey = new KeyValuePair<string, string>("", "");
            foreach (KeyValuePair<string, string> key in superForms.Keys)
            {
                if (superForms[key] == sender as XtraForm)
                {
                    currentFormKey = key;
                }
            }

            if (string.IsNullOrWhiteSpace(currentFormKey.Key) || string.IsNullOrWhiteSpace(currentFormKey.Value))
                return;
            superForms.Remove(currentFormKey);

            // 对窗口菜单进行删除
            var currentForm = ckList.Where(o => o.KEY.Equals(currentFormKey)).FirstOrDefault();
            Execute("NEXTSHEET", new E_GY_CAIDAN_NEW());
            if (currentForm != null)
            {
                ckList.Remove(currentForm);
            }
            ChuangKou.ItemLinks.Clear();
            if (ckList.Count > 0)
            {
                for (int i = 0; i < ckList.Count; i++)
                {
                    //ckList[i].CAIDANMC = i + ckList[i].CAIDANMC;
                    ChuangKouCD(ckList[i], 2);
                }
            }

            if (superForms == null || superForms.Count == 0)
            {
                //HB6-204(418072)【退出菜单】关闭菜单回到系统主界面，左上角的菜单名称清空
                this.Text = HISClientHelper.YINGYONGMC;

                return;
            }

            int icount = superForms.Count;
            int index = 0;
            KeyValuePair<string, string> keyName = new KeyValuePair<string, string>("", "");
            foreach (KeyValuePair<string, string> key in superForms.Keys)
            {
                if (index == icount - 1)
                {
                    keyName = key;
                }
                index++;
            }
            superForms.Values.ToList().ForEach(o => { o.Hide(); });
            superForms[keyName].TopMost = true;
            if (!(superForms[keyName] is MediFormWithoutTitleQX))
            {
                this.Text = string.Format("{0}-{1}", HISClientHelper.YINGYONGMC, superForms[keyName].Text);
            }

            superForms[keyName].Show();
        }

        public void DiaoYongCSFun(string formText)
        {
            if (CurrentSubForm != null)
            {
                this.Text = formText;
            }
        }

        private void MainFormBase_TextChanged(object sender, EventArgs e)
        {
            #region 记录日志

            // 记录日志=====================================================================

            //ESLog eSLog = new ESLog();
            SysLogEntity logEntity = new SysLogEntity();
            logEntity.RiZhiID = Guid.NewGuid().ToString();
            logEntity.ChuangJianSj = DateTime.Now.ToInvariantString("yyyy/MM/dd HH:mm:ss");
            logEntity.RiZhiBt = "[" + HISClientHelper.USERNAME + "]打开了[" + ((XtraForm)sender).Text + "]界面。";
            logEntity.RiZhiNr = "[" + HISClientHelper.USERNAME + "]打开了[" + ((XtraForm)sender).Text + "]界面。\r\n上一个界面是：" + HISClientHelper.DANGQIANCKMC;

            logEntity.FuWuMc = "";
            logEntity.QingQiuLy = ((XtraForm)sender).Text;
            // 日志类型：1.菜单打开，2.客户端异常，3.服务调用，4服务端异常，5.SQL日志，6.性能日志
            logEntity.RiZhiLx = 1;
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
            LogHelper.Intance.PutSysInfoLog(logEntity);
            // 记录日志=====================================================================

            #endregion 记录日志

            HISClientHelper.DANGQIANCKMC = ((XtraForm)sender).Text;
        }

        private void panel_Resize(object sender, EventArgs e)
        {
            foreach (Control control in this.panel.Controls)
            {
                Form subFrm = (Form)control;

                subFrm.WindowState = FormWindowState.Normal;
            }
        }

        private void MainFormBase_Load(object sender, EventArgs e)
        {
            if (!SkinCat.Instance.IsDesignMode)
            {
                this.TextChanged -= new System.EventHandler(this.MainFormBase_TextChanged);
                this.TextChanged += new System.EventHandler(this.MainFormBase_TextChanged);
                DateTime dateTime = gongYongService.GetSysDate().Return;
                barStatic_date.Caption = dateTime.ToString("yyyy-MM-dd HH:mm:ss") + " " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dateTime.DayOfWeek);

            }

        }

        /// <summary>
        /// 重写销毁对象
        /// </summary>
        public override void DesTroyPanelControls()
        {
            foreach (Control item in GetPanel().Controls)
            {
                Control mediFormWithQX = item;
                if (mediFormWithQX is MediFormWithQX qx)
                {
                    if (qx.IsDisposed)
                    {
                        IsCloseAllQXForm = true;
                        continue;
                    }

                    qx.Visible = true;
                    qx.Close();
                    if (!qx.IsExistFormClosingEventArgs)
                    {
                        qx.IsCloseQXForm = true;
                    }
                    if (!qx.IsCloseQXForm)
                    {
                        IsCloseAllQXForm = false;
                    }
                    if (qx.IsCloseQXForm)
                    {
                        MainFormBase_FormClosed(qx, null);
                        qx.Dispose();
                    }
                }
            }
        }
        

        private void clientFrmCacheWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            ConcurrentDictionary<string, dynamic> keyValuePairs = new ConcurrentDictionary<string, dynamic>();

            var ass = Assemblys[path][e.Argument.ToString()].Value;

            string ssemname = Assemblys[path][e.Argument.ToString()].Key;
            var form = ass.CreateInstance(ssemname);
            keyValuePairs.TryAdd(e.Argument.ToString(), form);
            e.Result = keyValuePairs;
        }

        private void clientFrmCacheWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private bool IsCompelete = false;
        private void clientFrmCacheWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                ConcurrentDictionary<string, dynamic> keyValuePairs = e.Result as ConcurrentDictionary<string, dynamic>;
                if (!globalFormscache.ContainsKey(keyValuePairs.First().Key))
                {
                    globalFormscache.TryAdd(keyValuePairs.First().Key, keyValuePairs.First().Value);
                }
                IsCompelete = true;
            }
        }
    }
}