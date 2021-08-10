using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Enterprise.Config;
using Mediinfo.Enterprise.Log;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Mediinfo.Utility.Extensions;
using Mediinfo.WinForm.HIS.Controls.SecondaryFramework;
using Mediinfo.WinForm.HIS.Controls.TabForm;
using Mediinfo.WinForm.HIS.Core;
using Microsoft.Win32;

namespace Mediinfo.WinForm.HIS.Controls.FirstLevelFramework
{
    public partial class ShouFeiMainFormBase : MediUniversalMFBase
    {

        [StructLayout(LayoutKind.Sequential)]
        public struct LastInputInfo
        {
            /// <summary>
            /// 指示如何在托管代码和非托管代码之间封送数据
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]   // 4字节无符号整数
            public int cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public int dwTime;
        }

        /// <summary>
        /// 调用windows API获取鼠标键盘空闲时间
        /// </summary>
        /// <param name="lastInputInfo"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool GetLastInputInfo(ref LastInputInfo lastInputInfo);

        /// <summary>
        /// 获取鼠标键盘空闲时间
        /// </summary>
        /// <returns></returns>
        public long GetIdleTick()
        {
            LastInputInfo lastInputInfo = new LastInputInfo();
            lastInputInfo.cbSize = Marshal.SizeOf(lastInputInfo);
            if (!GetLastInputInfo(ref lastInputInfo)) return 0;
            return Environment.TickCount - (long)lastInputInfo.dwTime;
        }


        protected event Action<MainFormOpenEventArgs> BeforeMainFormOpenEevent;
        protected event Action<MainFormOpenEventArgs> ClosingMainFormOpenEevent;
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_MINIMIZEBOX = 0x00020000;
                CreateParams cp = base.CreateParams;
                cp.Style = cp.Style | WS_MINIMIZEBOX;
                return cp;
            }
        }
        /// <summary>
        /// 科室切换事件
        /// </summary>
        protected event Action<object, MainFormOpenEventArgs> ChangeKeShiEevent;

        /// <summary>
        /// 锁定屏幕事件
        /// </summary>
        protected event Action<object, MainFormOpenEventArgs> LockScreenEevent;

        /// <summary>
        /// 操作手册事件
        /// </summary>
        protected event Action<object, MainFormOpenEventArgs> CaoZuoSCEevent;

        public Dictionary<string, dynamic> firstMainTab = new Dictionary<string, dynamic>();

        /// <summary>
        /// 全局窗体一级对象缓存
        /// </summary>
        private Dictionary<string, dynamic> globalFormsCacheDic = new Dictionary<string, dynamic>();
        /// <summary>
        /// 支持相同窗体打开多次(通过菜单),
        /// </summary>
        private Dictionary<KeyValuePair<string, string>, MediForm> superForms = new Dictionary<KeyValuePair<string, string>, MediForm>();

        private List<ChuangKouCD> ckList = new List<ChuangKouCD>();


        private BarSubItem ChuangKou { get; set; }

        private JCJGCaiDanService caiDanService = new JCJGCaiDanService();
        private int index = 1;  // 常用按钮类型

        /// <summary>
        ///  病人ID
        /// </summary>
        internal virtual string BingRenID { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public ShouFeiMainFormBase()
        {
            InitializeComponent();

            // 注册事件
            RegisterEvents();
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvents()
        {
            // 注册锁屏事件
            this.LockScreenEevent -= MediLCMainForm_LockScreenEevent;
            this.LockScreenEevent += MediLCMainForm_LockScreenEevent;
        }

        /// <summary>
        /// 锁屏事件
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        private void MediLCMainForm_LockScreenEevent(object arg1, MainFormOpenEventArgs arg2)
        {
            // 弹出锁屏窗口，锁屏窗口居于当前窗口上层
            using (W_GY_SUODINGPM frm = new W_GY_SUODINGPM())
            {
                IsLockScreen = true;

                frm.Unlock += new EventHandler(LinChuangMainFormBase_Unlock);
                frm.Tag = arg1;
                frm.ShowInTaskbar = false;
                frm.ShowDialog(this);
            }
        }

        /// <summary>
        /// 屏幕解锁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinChuangMainFormBase_Unlock(object sender, EventArgs e)
        {
            if (sender is W_GY_SUODINGPM frm)
            {
                if (frm.Tag is System.Windows.Forms.Timer tm)
                {
                    // 重新启动定时器
                    tm.Start();

                    // 解锁之后刷新数据
                    RefreshData();
                }
            }
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        private void RefreshData()
        {
            foreach (Control page in this.mediLCMainFormTabControl.TabPages)
            {
                foreach (Control ctr in page.Controls)
                {
                    foreach (Control form in ctr.Controls)
                    {
                        if (form is MediFormLCWithQX frm)
                        {
                            // 刷新已经打开的业务数据
                            frm.RefreshBusinessData();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void CaiDanInfomation()
        {
            // 获取当前用户权限数据
            YongHuQXList = GYQuanXianHelper.GetQuanXian();

            BaseFormCommonHelper.Assemblys = Assemblys;
            BaseFormCommonHelper.CaiDanList = CanDanList;
            BaseFormCommonHelper.YongHuQXList = YongHuQXList;
            BaseFormCommonHelper.LinChuangMainForm = this;
            OpenRootGongNeng();

            loadDefaultMenu();

            LoadFavoriteMenu();
        }

        /// <summary>
        /// 关闭所有打开窗体
        /// </summary>
        public void CloseAllOpenWindows()
        {
            firstMainTab.ToList().ForEach(o =>
            {
                if (o.Value is MediForm form)
                {
                    form.Close();
                }
            });
            openwindowsdic.ToList().ForEach(o =>
            {
                o.Value?.Close();
            });
            kuangjiadic.ToList().ForEach(o =>
            {
                o.Value?.Close();
            });
            firstMainTab.Clear();
            openwindowsdic.Clear();
            kuangjiadic.Clear();
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void FireMainEvent(object sender, MainFormOpenEventArgs e)
        {
            if (ChangeKeShiEevent != null && e.ActionStateType == ActionType.ChangeKeShi)
                ChangeKeShiEevent(sender, e);
            if (LockScreenEevent != null && e.ActionStateType == ActionType.LockScreen)
                LockScreenEevent(sender, e);
            if (CaoZuoSCEevent != null && e.ActionStateType == ActionType.CaoZuoShouCe)
                CaoZuoSCEevent(sender, e);
        }
        public override void OpenCaiDanForm()
        {
        }
        /// <summary>
        /// 已打开的窗口集合
        /// </summary>
        public Dictionary<OpenCKInfo, MediForm> openwindowsdic = new Dictionary<OpenCKInfo, MediForm>();
        /// <summary>
        /// 已打开的病人框架
        /// </summary>
        public Dictionary<string, W_CAIDANKJ_BSAE> kuangjiadic = new Dictionary<string, W_CAIDANKJ_BSAE>();

        /// <summary>
        /// 应用名称
        /// </summary>
        [DefaultValue("应用名称"), Browsable(true)]
        public string YingYongMC { get; set; }
        /// <summary>
        /// 当前选中的主菜单ID
        /// </summary>
        public string SelectMainMenuId { get; set; }
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
                        //判断功能权限
                        var yonghuqx = YongHuQXList.FirstOrDefault(o => o.GONGNENGID == gongnengid);
                        if (yonghuqx == null)
                        {
                            CanDanList[i].Delete();
                        }
                    }
                }
            }

            //一级菜单
            CanDanList.Where(o => o.SHANGJICDID == "-").OrderBy(o => o.SHUNXUHAO).ToList().ForEach(o =>
            {
                BarItem item;

                var count = CanDanList.Count(r => r.SHANGJICDID == o.CAIDANID);
                if (count > 0)
                {
                    item = new BarSubItem();
                }
                else
                {
                    item = new BarButtonItem();
                    item.ItemClick += caidanlan_ItemClick;
                }

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

                moreMenuPM.AddItem(item);
            });
            //二三级菜单
            ChuangJianCD(moreMenuPM.LinksPersistInfo, CanDanList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentCaiDanID"></param>
        public virtual void LoadDefaultMenu(string currentCaiDanID)
        {
            moreMenuPM.ClearLinks();
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
                        //判断功能权限
                        var yonghuqx = YongHuQXList.FirstOrDefault(o => o.GONGNENGID == gongnengid);
                        if (yonghuqx == null)
                        {
                            CanDanList[i].Delete();
                        }
                    }
                }
            }

            //一级菜单
            var caidanlist = CanDanList.Where(o => o.SHANGJICDID == currentCaiDanID).OrderBy(o => o.SHUNXUHAO).ToList();
            CanDanList.Where(o => o.SHANGJICDID == currentCaiDanID).OrderBy(o => o.SHUNXUHAO).ToList().ForEach(o =>
            {
                BarItem item;

                var count = CanDanList.Count(r => r.SHANGJICDID == o.CAIDANID);
                if (count > 0)
                {
                    item = new BarSubItem();
                }
                else
                {
                    item = new BarButtonItem();
                    item.ItemClick += new ItemClickEventHandler(caidanlan_ItemClick);
                }

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

                moreMenuPM.AddItem(item);
            });
            //二三级菜单
            ChuangJianCD(moreMenuPM.LinksPersistInfo, CanDanList);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void LoadStatusBar()
        {
            WindowsFormsSettings.DefaultFont = new Font("微软雅黑", 11);

            WindowsFormsSettings.DefaultMenuFont = new Font("微软雅黑", 11);

            WindowsFormsSettings.DefaultPrintFont = new Font("微软雅黑", 11);

            userNamebsi.Caption = HISClientHelper.JIUZHENKSMC + "/" + HISClientHelper.USERNAME;

            ipInfobsi.Caption = "本机 " + HISClientHelper.IP;
            barStaticHosName.Caption = HISClientHelper.YUANQUMC;
            barStaticItemKongBai1.Size = new Size(70, 20);//占位符100
            barStaticItemKongBai2.Size = new Size(70, 20);
            barStaticItemKongBai3.Size = new Size(70, 20);
            barStaticItemKongBai4.Size = new Size(70, 20);
            if (MediinfoConfig.GetValue("WinFormMain.xml", "RunningMode").Trim().Equals("Standalone"))
            {
                barStatic_RunningMode.Caption = "单机模式";
            }
            else if (MediinfoConfig.GetValue("WinFormMain.xml", "RunningMode").Trim().Equals("Cluster"))
            {
                barStatic_RunningMode.Caption = "集群模式";
            }
            barStatic_ServerUrl.Caption = MediinfoConfig.GetValue("WinFormMain.xml", "ServerUrl").Substring(0, MediinfoConfig.GetValue("WinFormMain.xml", "ServerUrl").Length > 0 ? MediinfoConfig.GetValue("WinFormMain.xml", "ServerUrl").IndexOf(':') : 0);
            // 本地客户端版本号
            barStatic_Version.Caption = "版本号:" + MediinfoConfig.Read(Application.StartupPath + "\\version.ini", "version", "version");
        }

        /// <summary>
        /// 
        /// </summary>
        public void OpenRootGongNeng()
        {
            List<E_GY_CAIDAN_NEW> canDanList = CanDanList.Where(o => o.SHANGJICDID.Equals("-") && o.ISOPEN.Equals(1)).OrderBy(o => o.SHUNXUHAO).ToList();

            canDanList.ForEach(o =>
            {
                if (string.IsNullOrEmpty(o.CAIDANID))
                {
                    MediMsgBox.Warn("没有找到【" + HISClientHelper.YINGYONGID + "】对应的菜单ID【" + o.CAIDANID + "】");
                    return;
                }
                this.SuspendLayout();
                CreateForm(o, null);
                this.ResumeLayout(false);
            });
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
                            tsmi = new BarSubItem();
                        else
                            tsmi = new BarButtonItem();
                        tsmi.Name = caidan.CAIDANID;
                        tsmi.Caption = caidan.CAIDANMC;
                        tsmi.Enabled = caidan.QIYONGBZ == 1 ? false : true;
                        tsmi.ItemInMenuAppearance.Hovered.Options.UseBackColor = true;
                        tsmi.ItemInMenuAppearance.Hovered.BackColor = Color.FromArgb(10, 163, 230);
                        tsmi.ItemInMenuAppearance.Hovered.Options.UseForeColor = true;
                        tsmi.ItemInMenuAppearance.Hovered.ForeColor = Color.White;

                        tsmi.ItemClick += caidanlan_ItemClick;
                        BarItem barManag = o.Item;

                        this.moreMenuPM.Manager = this.moreMenuBM;

                        if (breaksplit)
                        {
                            (o.Item as BarSubItem)?.AddItem(tsmi);
                            breaksplit = false;
                        }
                        else
                        {
                            (o.Item as BarSubItem)?.AddItem(tsmi);
                        }
                    }
                });
                if (o.Item is BarSubItem item && item.LinksPersistInfo.Count > 0)
                {
                    ChuangJianCD(item?.LinksPersistInfo, caidanList);
                }
            }
        }


        /// <summary>
        /// 打开常用菜单
        /// </summary>
        private void loadDefaultMenu()
        {
            var changYongCDList = CanDanList.Where(o => o.ISOPEN == 1 && o.SHANGJICDID != "-").ToList();
            if (changYongCDList.Count > 0)
            {
                mediWorkShopTabControl1.Visible = true;
            }
            foreach (var caiDanId in changYongCDList.Select(changYongCd => changYongCd.CAIDANID)
                .TakeWhile(caiDanId => !string.IsNullOrWhiteSpace(caiDanId)))
            {
                OpenFormFromTab(caiDanId, true);
            }
        }

        private void caidanlan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mediWorkShopTabControl1.Visible = true;
            var denglucd = CanDanList.FirstOrDefault(o => o.CAIDANID == e.Item.Name);
            if (denglucd == null && string.IsNullOrEmpty(e.Item.Name))
            {
                MediMsgBox.Warn("没有找到【" + Mediinfo.HIS.Core.HISClientHelper.YINGYONGID + "】对应的菜单ID【" + e.Item.Name + "】");
                return;
            }
            OpenFormFromTab(e.Item.Name);
        }

        /// <summary>
        /// 在选项卡重打开页面
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        public void OpenFormFromTab(string menuId, bool isopen = false)
        {
            var denglucd = CanDanList.FirstOrDefault(o => o.CAIDANID == menuId);
            if (denglucd == null && string.IsNullOrEmpty(menuId))
            {
                MediMsgBox.Warn("没有找到【" + Mediinfo.HIS.Core.HISClientHelper.YINGYONGID + "】对应的菜单ID【" + menuId + "】");
                return;
            }
            this.SuspendLayout();

            openwindowsdic = BaseFormCommonHelper.openwindowsdic;
            BaseFormCommonHelper.CreateForm(this, mediWorkShopTabControl1, denglucd, ref openwindowsdic, isopen);
            this.ResumeLayout(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediFormWithQX"></param>
        public void OpenFormFromTab(MediFormWithQX mediFormWithQX)
        {
            string caiDanMC = mediFormWithQX.Text;

            if (string.IsNullOrWhiteSpace(caiDanMC))
            {
                MediMsgBox.Warn("传入的菜单名称不能为空！");
                return;
            }

            var denglucd = CanDanList.FirstOrDefault(o => o.CAIDANMC == caiDanMC);
            if (denglucd == null)
            {
                MediMsgBox.Warn("没有找到【" + HISClientHelper.YINGYONGID + "】对应的菜单：" + caiDanMC.ToString());
                return;
            }

            #region 删除已打开重复的窗体
            foreach (var s in BaseFormCommonHelper.openwindowsdic)
            {
                if (s.Value.Name == mediFormWithQX.Name)
                {
                    BaseFormCommonHelper.openwindowsdic.Remove(s.Key);
                    break;
                }
            }
            mediWorkShopTabControl1.TabPages.ToList().ForEach(r =>
            {
                if (r.Name.Split('|')[0].ToUpper().Contains(mediFormWithQX.Name))
                {
                    mediWorkShopTabControl1.TabPages.Remove(r);
                }

            });
            #endregion

            this.SuspendLayout();
            mediWorkShopTabControl1.Visible = true;
            openwindowsdic = BaseFormCommonHelper.openwindowsdic;
            BaseFormCommonHelper.CreateForm(this, mediWorkShopTabControl1, denglucd, ref openwindowsdic, mediFormWithQX);
            this.ResumeLayout(false);
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
            // var count = ckList.Count - 1;
            var eChuangKou = new ChuangKouCD() { CAIDANID = caidanid, CAIDANMC = form.Text, KEY = formKey, XForm = form };

            if (!superForms.ContainsKey(formKey) && !globalFormsCacheDic.ContainsKey(form.GetType().Assembly.FullName + form.Name.ToUpper()))
            {
                //ChuangKouCD(eChuangKou, 1);
                superForms.Add(formKey, form);
                //SortFrmFun(panelInnerFrmSort, new KeyValuePair<string, Control>(caidanid, form));
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
                    // SortFrmFun(panelInnerFrmSort, new KeyValuePair<string, Control>(caidanid, superForms[formKey]));
                    superForms[formKey].Show();
                    //SetSelectedCDID(eChuangKou.CAIDANID, eChuangKou.XForm);
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
                    //新改的

                    mediWorkShopTabControl1.SelectedTabPage.Controls.Add(globalFormsCacheDic[form.GetType().Assembly.FullName + form.Name.ToUpper()]);
                    globalFormsCacheDic[form.GetType().Assembly.FullName + form.Name.ToUpper()].WindowState = FormWindowState.Maximized;
                    globalFormsCacheDic[form.GetType().Assembly.FullName + form.Name.ToUpper()].TopMost = true;
                    ((MediFormWithQX)globalFormsCacheDic[form.GetType().Assembly.FullName + form.Name.ToUpper()]).CaiDanID = caidanid;
                    //SortFrmFun(panelInnerFrmSort, new KeyValuePair<string, Control>(caidanid, globalFormsCacheDic[form.GetType().Assembly.FullName + form.Name.ToUpper()]));
                    //ChuangKouCD(eChuangKou, 1);
                    globalFormsCacheDic[form.GetType().Assembly.FullName + form.Name.ToUpper()].Show();

                }
                return false;
            }
        }

        public string SelectedCDID { get; set; }

        private void SetSelectedCDID(string caidanid, MediForm mediForm)
        {
            SelectedCDID = caidanid;
            // SetSelected(mediForm);
        }

        //“窗口”菜单进行添加 leixing 1表示 单击插入一个菜单，2表示循环多次插入菜单
        private void ChuangKouCD(ChuangKouCD eChuangKouCD, int leixing)
        {
            BarButtonItem tsmi;
            int count = 0;
            if (leixing == 1)

            {
                count = ckList.Count(o => o.KEY.Equals(eChuangKouCD.KEY));
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
                if (ckList.Where(o => o.CAIDANMC.Equals(eChuangKouCD.CAIDANMC)).Any())
                {
                    ckList.RemoveAll(o => o.CAIDANMC.Equals(eChuangKouCD.CAIDANMC));

                }
                ckList.Add(eChuangKouCD);

            }

            ChuangKou?.ItemLinks.Remove(ChuangKou.ItemLinks.Where(o => o.Item.Caption == eChuangKouCD.CAIDANMC).ToList().FirstOrDefault());

            tsmi.Tag = eChuangKouCD.KEY;
            tsmi.Id = Convert.ToInt32(eChuangKouCD.CAIDANID);
            tsmi.Name = eChuangKouCD.CAIDANID;
            tsmi.Caption = eChuangKouCD.CAIDANMC;
            tsmi.ItemInMenuAppearance.Hovered.Options.UseBackColor = true;
            tsmi.ItemInMenuAppearance.Hovered.BackColor = Color.FromArgb(10, 163, 230);
            tsmi.ItemInMenuAppearance.Hovered.Options.UseForeColor = true;
            tsmi.ItemInMenuAppearance.Hovered.ForeColor = Color.White;
            tsmi.ItemClick += caidanlan_ItemClick;
            if (ChuangKou == null)
            {
                ChuangKou = new BarSubItem();
            }
            ChuangKou.AddItem(tsmi);
            //这个放到后面是为了  SetSelected();
            if (leixing == 1)
            {
                SetSelectedCDID(eChuangKouCD.CAIDANID, eChuangKouCD.XForm);
            }
        }

        /// <summary>
        /// 按钮打开窗体统一show方法
        /// </summary>
        /// <param name="mediFormWithQX"></param>
        public override void QXWindowShow(MediFormWithQX mediFormWithQX)
        {
            string buttonid = string.Format("{0}\\{1}", HISClientHelper.YINGYONGID, mediFormWithQX.Name);
            string caidanid = ((int)Convert.ToInt64(Hash2MD516(buttonid), 16)).ToString();
            KeyValuePair<string, string> formKey = new KeyValuePair<string, string>(caidanid, HISClientHelper.YINGYONGID);
            mediFormWithQX.TopLevel = false;
            mediFormWithQX.MaximizeBox = false;
            mediFormWithQX.MinimizeBox = false;

            mediFormWithQX.FormBorderStyle = FormBorderStyle.None;
            mediFormWithQX.ControlBox = false;
            mediFormWithQX.Dock = DockStyle.Fill;
            if (!(mediFormWithQX is MediFormWithoutTitleQX))
            {
                this.Text = string.Format("{0}-{1}", HISClientHelper.YINGYONGMC, mediFormWithQX.Text);
            }
            foreach (var control in this.mediWorkShopTabControl1.SelectedTabPage.Controls)
            {
                if (control is MediPanelControl panelControl)
                {
                    panelControl.Controls.Add(mediFormWithQX);
                    //添加到打开菜单中
                    //判断是否加载页面
                    OpenCKInfo openCKInfo = new OpenCKInfo() { openWindowMode = OpenWindowMode.Menu, xitongid = Mediinfo.HIS.Core.HISClientHelper.XITONGID, gongnengId = "gn+" + caidanid + "", caidanId = caidanid, chuangkoumc = mediFormWithQX.Name.ToUpper() };

                    if (!openwindowsdic.ContainsKey(openCKInfo))
                    {
                        openwindowsdic.Add(openCKInfo, mediFormWithQX);
                    }
                    break;
                }
            }
            mediFormWithQX.WindowState = FormWindowState.Maximized;
            mediFormWithQX.TopMost = true;
            mediFormWithQX.CaiDanID = caidanid;
            mediFormWithQX.Show();
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
            superForms[keyName].Show();
        }

        /// <summary>
        /// 创建窗体对象
        /// </summary>
        /// <param name="denglucd"></param>
        /// <param name="gongnengcss"></param>
        /// <param name="fomkeys"></param>
        protected override void CreateForm(DTO.HIS.GY.E_GY_CAIDAN_NEW denglucd, System.Collections.Generic.List<object> gongnengcss, params object[] fomkeys)
        {
            if (denglucd != null)
            {
                if (CreateFormEX(denglucd, gongnengcss, fomkeys))
                    return;
                var formname = denglucd.DIAOYONGCS.Substring(0, denglucd.DIAOYONGCS.IndexOf('|') == -1 ? denglucd.DIAOYONGCS.Length : denglucd.DIAOYONGCS.IndexOf('|'));

                OpenCKInfo openCKInfo = new OpenCKInfo() { openWindowMode = OpenWindowMode.Menu, xitongid = Mediinfo.HIS.Core.HISClientHelper.XITONGID, gongnengId = denglucd.GONGNENGID, caidanId = denglucd.CAIDANID, chuangkoumc = formname.ToUpper() };
                if (openwindowsdic.Count > 0 && openwindowsdic.ContainsKey(openCKInfo))
                {
                    openwindowsdic[openCKInfo].Show();
                    return;
                }
                DevExpress.XtraTab.XtraTabPage firstLevelTabPage = new DevExpress.XtraTab.XtraTabPage();
                MediPanelControl mediPanelControl = new MediPanelControl();
                mediPanelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                firstLevelTabPage.Name = denglucd.CAIDANID;
                firstLevelTabPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.False;
                firstLevelTabPage.Text = denglucd.CAIDANMC;
                mediPanelControl.Dock = DockStyle.Fill;
                firstLevelTabPage.Controls.Add(mediPanelControl);
                var path = AppDomain.CurrentDomain.BaseDirectory;
                if (!Assemblys[path].ContainsKey(formname.ToUpper()))
                {
                    MediMsgBox.Info("Dll中未找到包含" + formname.ToUpper() + "名称的窗体!");
                    return;
                }
                dynamic form = Assemblys[path][formname.ToUpper()].Value.CreateInstance(Assemblys[path][formname.ToUpper()].Key);

                form.TopLevel = false;
                form.Dock = DockStyle.Fill;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Parent = mediPanelControl;
                form.CaiDanList = CanDanList;
                form.YongHuQXList = YongHuQXList;
                form.currentCaiDanId = denglucd.CAIDANID;
                form.InitialCaiDanData();

                OpenTabForm(form);
            }
        }

        /// <summary>
        /// 加载喜爱菜单
        /// </summary>
        private void LoadFavoriteMenu()
        {
            var result = caiDanService.GetALLChangYongCaiDanList();
            if (result.ReturnCode == ReturnCode.SUCCESS)
            {
                List<E_GY_CHANGYONGCAIDAN> list = result.Return;
                var list1 = list.Where((x, i) => list.FindIndex(z => z.CAIDANID == x.CAIDANID) == i);//全局和常用菜单可能会重复，过滤掉一条常用。
                //查询该系统的所有常用菜单
                foreach (var item in list1)
                {
                    AddBarMenu(item.CAIDANID, item.CAIDANMC);
                }
            }
        }


        private void AddBarMenu(string menuId, string menuName)
        {
            BarButtonItem buttonItem = new BarButtonItem();
            buttonItem.Caption = menuName;
            buttonItem.Id = index;
            buttonItem.Tag = menuId;
            buttonItem.Name = "buttonItem" + index;
            //按钮显示图片
            if (File.Exists(@"..\PIC\changyongicon.png"))
            {
                buttonItem.ImageOptions.Image = Image.FromFile(@"..\PIC\changyongicon.png");
                buttonItem.PaintStyle = BarItemPaintStyle.CaptionGlyph;
            }
            buttonItem.ItemClick += ButtonItem_ItemClick;

            this.bar2.AddItem(buttonItem); ;

            index++;
        }


        /// <summary>
        /// 常用菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            mediWorkShopTabControl1.Visible = true;
            OpenFormFromTab(e.Item.Tag.ToString());
        }
        private void LinChuangMainFormBase_Load(object sender, EventArgs e)
        {
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            this.WindowState = FormWindowState.Maximized;

            if (!SkinCat.Instance.IsDesignMode)
            {
                this.TextChanged -= this.LinChuangMainFormBase_TextChanged;
                this.TextChanged += this.LinChuangMainFormBase_TextChanged;
                this.mediWorkShopTabControl1.SelectedPageChanged -= MediWorkShopTabControl1_SelectedPageChanged;
                this.mediWorkShopTabControl1.SelectedPageChanged += MediWorkShopTabControl1_SelectedPageChanged;
            }

        }

        private void MediWorkShopTabControl1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (e.PrevPage == null || e.Page == null) return;

            foreach (Control control in e.Page.Controls)
            {
                foreach (var item in control.Controls)
                {
                    if (item is MediFormWithQX tempForm)
                    {
                        //刷新界面数据
                        tempForm.RefreshFormData();
                    }
                }
            }
        }

        private void LinChuangMainFormBase_TextChanged(object sender, EventArgs e)
        {
            #region 记录日志

            //记录日志=====================================================================;
            SysLogEntity logEntity = new SysLogEntity();
            logEntity.RiZhiID = Guid.NewGuid().ToString();
            logEntity.ChuangJianSj = DateTime.Now.ToInvariantString("yyyy/MM/dd HH:mm:ss");
            logEntity.RiZhiBt = "[" + HISClientHelper.USERNAME + "]打开了[" + ((XtraForm)sender).Text + "]界面。";
            logEntity.RiZhiNr = "[" + HISClientHelper.USERNAME + "]打开了[" + ((XtraForm)sender).Text + "]界面。\r\n上一个界面是：" + HISClientHelper.DANGQIANCKMC;

            logEntity.FuWuMc = "";
            logEntity.QingQiuLy = ((DevExpress.XtraEditors.XtraForm)sender).Text;
            //日志类型：1.菜单打开，2.客户端异常，3.服务调用，4服务端异常，5.SQL日志，6.性能日志
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
            LogHelper.Intance.PutSysInfoLog(logEntity);
            //记录日志=====================================================================

            #endregion 记录日志

            HISClientHelper.DANGQIANCKMC = ((XtraForm)sender).Text;
        }

        //#endregion

        W_BINGRENKJ_BASE kuangJiaBase = new W_BINGRENKJ_BASE();

        /// <param name="gongzuotaistance">相关病人信息</param>
        public bool OpenTabForm(W_CAIDANKJ_BSAE gongzuotaistance)
        {
            List<XtraTabPage> xtraTabPages = this.mediLCMainFormTabControl.TabPages.Where(o => o.Name.Equals(gongzuotaistance.BingRenID)).ToList();
            if (xtraTabPages.Count > 0)
            {
                this.mediLCMainFormTabControl.SelectedTabPage = xtraTabPages[0];
                return true;
            }
            XtraTabPage xtraTabPage = new DevExpress.XtraTab.XtraTabPage();
            xtraTabPage.BackColor = Color.FromArgb(243, 243, 243);
            xtraTabPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.False;
            MediPanelControl mediPanelControl = new MediPanelControl();
            mediPanelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            mediPanelControl.BackColor = Color.FromArgb(243, 243, 243);
            mediPanelControl.Dock = DockStyle.Fill;
            xtraTabPage.Controls.Add(mediPanelControl);
            this.mediLCMainFormTabControl.TabPages.Add(xtraTabPage);
            gongzuotaistance.CaiDanList = CanDanList;
            gongzuotaistance.YongHuQXList = YongHuQXList;
            gongzuotaistance.openwindowsdic = openwindowsdic;
            gongzuotaistance.IsMouseInCloseButton = MouseInCloseButton;
            gongzuotaistance.TopLevel = false;
            gongzuotaistance.Dock = DockStyle.Fill;
            gongzuotaistance.FormBorderStyle = FormBorderStyle.None;
            gongzuotaistance.Parent = mediPanelControl;

            var denglucd = CanDanList.FirstOrDefault(o => o.CAIDANID == gongzuotaistance.currentCaiDanId);
            if (denglucd != null)
            {
                xtraTabPage.Name = denglucd.CAIDANID;
                xtraTabPage.Text = denglucd.CAIDANMC;
            }

            gongzuotaistance.Show();
            kuangjiadic.Add(gongzuotaistance.currentCaiDanId, gongzuotaistance);

            return true;
        }

        /// <summary>
        /// 右键重打开Tab页处理
        /// </summary>
        private void RightClickForm(W_BINGRENKJ_BASE kuangjiainstance)
        {
            #region 重打开定位Tab窗口

            if (kuangjiainstance.MuBiaoCKInfo == null)
                return;

            var denglucd = CanDanList.FirstOrDefault(o =>
                o.DIAOYONGCS.Contains(kuangjiainstance.MuBiaoCKInfo.GongNengCKMC.TrimStart('I') + "|" +
                                      kuangjiainstance.MuBiaoCKInfo.GongNengID));
            {
                MediMsgBox.Warn("没有找到【" + HISClientHelper.YINGYONGID + "】对应的窗口名称【" + kuangjiainstance.MuBiaoCKInfo.GongNengCKMC + "】");
            }

            if (denglucd != null)
            {
                var formname = denglucd.DIAOYONGCS.Substring(0, denglucd.DIAOYONGCS.IndexOf('|') == -1 ? denglucd.DIAOYONGCS.Length : denglucd.DIAOYONGCS.IndexOf('|'));
                OpenCKInfo openCKInfo = denglucd.GONGNENGID.StartsWith("gn")
                    ? new OpenCKInfo() { openWindowMode = OpenWindowMode.Button, xitongid = HISClientHelper.XITONGID, gongnengId = denglucd.GONGNENGID, caidanId = denglucd.CAIDANID, binrenid = kuangjiainstance.BingRenID, chuangkoumc = formname.ToUpper() }
                    : new OpenCKInfo() { openWindowMode = OpenWindowMode.Menu, xitongid = HISClientHelper.XITONGID, gongnengId = denglucd.GONGNENGID, caidanId = denglucd.CAIDANID, binrenid = kuangjiainstance.BingRenID, chuangkoumc = formname.ToUpper() };
                this.SuspendLayout();
                if (!openwindowsdic.ContainsKey(openCKInfo))
                {
                    BaseFormCommonHelper.CreateForm(kuangjiainstance, BaseFormCommonHelper.mediBRTabControl.FirstOrDefault(p => p.Target != null)?.Target, denglucd, ref openwindowsdic, false);

                    BaseFormCommonHelper.openwindowsdic = openwindowsdic;
                    if (openwindowsdic.ContainsKey(openCKInfo))
                    {
                        if (!kuangjiainstance.openedlcdic.ContainsKey(openCKInfo.chuangkoumc))
                        {
                            if (openwindowsdic[openCKInfo] is W_LINCHUANG_BASE lingchuangbase)
                            {
                                lingchuangbase.LinChuangFormClosed += Lingchuangbase_LinChuangFormClosed;
                            }
                            kuangjiainstance.openedlcdic.Add(openCKInfo.chuangkoumc, openwindowsdic[openCKInfo] as W_LINCHUANG_BASE);
                        }
                    }
                    kuangJiaBase = kuangjiainstance;
                }
                else
                {
                    if (denglucd.DIAOYONGCS.Contains("NO"))
                        denglucd.DIAOYONGCS = denglucd.DIAOYONGCS.Substring(0, denglucd.DIAOYONGCS.Length - 3) + "|YES";

                    if (string.IsNullOrEmpty(kuangjiainstance.TabPageName))
                        kuangjiainstance.TabPageName = denglucd.CAIDANMC;
                    BaseFormCommonHelper.CreateForm(kuangjiainstance, BaseFormCommonHelper.mediBRTabControl.Where(p => p.Target != null).ToList()[0].Target, denglucd, ref BaseFormCommonHelper.openwindowsdic, false);

                    if (kuangjiainstance.openedlcdic.ContainsKey(openCKInfo.chuangkoumc) && kuangjiainstance.openedlcdic[openCKInfo.chuangkoumc] == null && BaseFormCommonHelper.openwindowsdic.ContainsKey(openCKInfo))
                    {
                        if (BaseFormCommonHelper.openwindowsdic[openCKInfo] is W_LINCHUANG_BASE lingchuangbase)
                        {
                            kuangjiainstance.openedlcdic[openCKInfo.chuangkoumc] = lingchuangbase;
                        }
                    }
                }
            }

            this.ResumeLayout(false);

            #endregion
        }

        private void Lingchuangbase_LinChuangFormClosed(object sender, LinChuangEventArgs lceventArgs)
        {
            if (sender is W_LINCHUANG_BASE linchuangbase)
            {
                if (kuangJiaBase.openedlcdic.ContainsKey(linchuangbase.Name.ToUpper()))
                {
                    kuangJiaBase.openedlcdic.Remove(linchuangbase.Name.ToUpper());
                }
            }
        }

        private void Kuangjiainstance_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is W_BINGRENKJ_BASE bingrenkjBase)
            {
                var binrenid = bingrenkjBase.BingRenID;

                if (kuangjiadic.ContainsKey(binrenid))
                    kuangjiadic.Remove(binrenid);
            }
        }

        private void mediTabControl1_Click(object sender, EventArgs e)
        {
            var caidanid = mediLCMainFormTabControl.SelectedTabPage.Name;
            BaseTabPageViewInfo baseTabPageViewInfo = new BaseTabPageViewInfo(mediLCMainFormTabControl.SelectedTabPage);
            //tab页的位置
            var tabLocation = baseTabPageViewInfo.ViewInfo.SelectedTabPageViewInfo.Bounds;
            if (string.IsNullOrWhiteSpace(caidanid) || !tabLocation.Contains((e as MouseEventArgs).Location))
                return;
            var tabControlpoint = mediLCMainFormTabControl.PointToScreen(new Point(tabLocation.X, tabLocation.Y));

            LoadDefaultMenu(caidanid);
            Point point = new Point(tabControlpoint.X, tabControlpoint.Y + tabLocation.Height);
            moreMenuPM.ShowPopup(point);
        }

        private void mediTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            foreach (Control control in e.Page.Controls)
            {
                foreach (var item in control.Controls)
                {
                    if (item is MediFormLCWithQX tempForm)
                    {
                        SelectMainMenuId = tempForm.currentCaiDanId;
                    }
                }
            }
        }
        public override void ResetClosingArgs()
        {

        }
        private void LinChuangMainFormBase_Shown(object sender, EventArgs e)
        {
            if (BeforeMainFormOpenEevent != null)
            {
                MainFormOpenEventArgs mainFormOpenEventArgs = new MainFormOpenEventArgs();

                BeforeMainFormOpenEevent(mainFormOpenEventArgs);
                if (mainFormOpenEventArgs.MFOpenResult == MediDialogResult.Close)
                {
                    if (!this.IsDisposed)
                        this.Close();

                }
                else if (mainFormOpenEventArgs.MFOpenResult == MediDialogResult.Open)
                {
                    foreach (var form in firstMainTab.Values)
                        form.Show();
                }
            }
        }

        private void LinChuangMainFormBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ClosingMainFormOpenEevent != null)
            {
                MainFormOpenEventArgs mainFormOpenEventArgs = new MainFormOpenEventArgs();
                ClosingMainFormOpenEevent(mainFormOpenEventArgs);
                if (mainFormOpenEventArgs.MFOpenResult == MediDialogResult.Close)
                {
                    this.Close();
                }
                else if (mainFormOpenEventArgs.MFOpenResult == MediDialogResult.Open)
                {
                    foreach (var form in firstMainTab.Values)
                        form.Show();

                }
            }
            if (this.IsDisposed)
                System.Environment.Exit(0);
        }

        /// <summary>
        /// 关闭tab页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediWorkShopTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            if (e is ClosePageButtonEventArgs closePageButtonEventArgs)
            {
                XtraTabPage xtraTabPage = closePageButtonEventArgs.Page as XtraTabPage;

                CloseTabPage(xtraTabPage);
            }
        }

        /// <summary>
        /// 鼠标点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediWorkShopTabControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                XtraTabPage page = mediWorkShopTabControl1.SelectedTabPage;
                if (page == null) return;
                string menuId = GetMenuIDByTabPage(page);

                //DBA才能有全局常用菜单
                if (HISClientHelper.USERID != "DBA")
                {
                    this.bbiALLFavoriteMenu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                }
                else
                {
                    //全局常用菜单(DBA可以设置)
                    int? isALLFavorite = 0;
                    var quanJvCYCaiDan = caiDanService.GetALLChangYongCaiDanByCaiDanID(menuId);
                    if (quanJvCYCaiDan.ReturnCode == ReturnCode.SUCCESS)
                    {
                        if (quanJvCYCaiDan.Return.Count > 0)
                        {
                            var isquanjvcy = quanJvCYCaiDan.Return[0].ISQUANJVCY;
                            if (isquanjvcy != null)
                                isALLFavorite = isquanjvcy.Value;
                        }
                    }
                    this.bbiALLFavoriteMenu.Caption = isALLFavorite != 0 ? "取消全局常用菜单" : "设为全局常用菜单";
                }

                int? isOpen = 0;
                var caiDan = caiDanService.GetCaiDanNew(menuId);
                if (caiDan.ReturnCode == ReturnCode.SUCCESS)
                    isOpen = caiDan.Return[0].ISOPEN;

                this.bbiFixedMenu.Caption = isOpen != 0 ? "取消固定标签" : "固定标签";

                int? isFavorite = 0;
                var changYongCaiDan = caiDanService.GetChangYongCaiDanByCaiDanID(menuId);
                if (changYongCaiDan.ReturnCode == ReturnCode.SUCCESS && changYongCaiDan.Return.Count > 0)
                {
                    var ischangyong = changYongCaiDan.Return[0].ISCHANGYONG;
                    if (ischangyong != null)
                        isFavorite = ischangyong.Value;
                }

                this.bbiFavoriteMenu.Caption = isFavorite != 0 ? "取消常用菜单" : "设为常用菜单";

                MediWorkShopTabControl mediLCTabControl = sender as MediWorkShopTabControl;
                Point pt = MousePosition;
                if (mediLCTabControl != null)
                {
                    XtraTabHitInfo xtraTabHitInfo = mediLCTabControl.CalcHitInfo(mediLCTabControl.PointToClient(pt));
                    if (xtraTabHitInfo.HitTest == XtraTabHitTest.PageHeader)
                    {
                        mediWorkShopTabControlPM.ShowPopup(pt);
                    }
                }
            }
        }


        /// <summary>
        /// 删除常用菜单
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        private void RemoveBarMenu(string menuId)
        {
            for (int i = bar2.LinksPersistInfo.Count - 1; i >= 0; i--)
            {
                BarItemLink item = bar2.LinksPersistInfo[i].Link;
                if (String.Compare(item.Item.Tag.ToString(), menuId, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    bar2.RemoveLink(item);
                }
            }
        }


        /// <summary>
        /// 获取菜单ID
        /// </summary>
        /// <param name="page">选项卡</param>
        /// <returns></returns>
        private string GetMenuIDByTabPage(XtraTabPage page)
        {
            string menuId = "";     // 菜单ID
            foreach (Control control in page.Controls)
            {
                foreach (var item in control.Controls)
                {
                    if (item is W_LINCHUANG_BASE tempForm1)
                    {
                        menuId = tempForm1.CaiDanID;
                    }

                    if (item is MediFormWithQX tempForm)
                    {
                        menuId = tempForm.CaiDanID;
                    }
                }
            }
            return menuId;
        }

        /// <summary>
        /// 关闭选项卡
        /// </summary>
        /// <param name="page">选项卡</param>
        public void CloseTabPage(XtraTabPage page)
        {
            foreach (Control control in page.Controls)
            {
                if (control is MediPanelControl)
                {
                    for (int i = 0; i < control.Controls.Count; i++)
                    {
                        if (control.Controls[i] is MediFormWithQX)
                        {
                            if (control.Controls[i] is MediFormWithQX tempForm)
                            {
                                tempForm.ValidateDataModifiedBeforeClose(); //界面关闭前校验数据是否更改
                                tempForm.Close();

                                if (tempForm.IsDisposed)
                                {
                                    OpenCKInfo openCKInfo;
                                    if (string.IsNullOrWhiteSpace(tempForm.GongNengId))
                                    {
                                        openCKInfo = new OpenCKInfo()
                                        {
                                            openWindowMode = OpenWindowMode.Menu,
                                            xitongid = HISClientHelper.XITONGID,
                                            gongnengId = "gn+" + tempForm.CaiDanID,
                                            caidanId = tempForm.CaiDanID,
                                            chuangkoumc = tempForm.Name.ToUpper()
                                        };
                                    }
                                    else
                                    {
                                        openCKInfo = new OpenCKInfo()
                                        {
                                            openWindowMode = OpenWindowMode.Menu,
                                            xitongid = HISClientHelper.XITONGID,
                                            gongnengId = tempForm.GongNengId,
                                            caidanId = tempForm.CaiDanID,
                                            chuangkoumc = tempForm.Name.ToUpper()
                                        };
                                    }

                                    if (BaseFormCommonHelper.openAllwindowsdic.ContainsKey(openCKInfo))
                                    {
                                        BaseFormCommonHelper.openAllwindowsdic.Remove(openCKInfo);
                                    }

                                    if (BaseFormCommonHelper.openwindowsdic.ContainsKey(openCKInfo))
                                    {
                                        BaseFormCommonHelper.openwindowsdic.Remove(openCKInfo);
                                    }

                                    if (openwindowsdic.ContainsKey(openCKInfo))
                                        openwindowsdic.Remove(openCKInfo);

                                    i--;
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            if (BaseFormCommonHelper.openedTabFormDic.ContainsKey(page.Name))
            {
                BaseFormCommonHelper.openedTabFormDic.Remove(page.Name);
            }

            page.PageVisible = false;
            page.Dispose();
        }

        /// <summary>
        /// 添加常用菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiFavoriteMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraTabPage page = mediWorkShopTabControl1.SelectedTabPage;

            string menuId = GetMenuIDByTabPage(page);

            int? isFavorite = 0;
            int? isALLFavorite = 0;
            bool update = false;
            var changYongCaiDan = caiDanService.GetChangYongCaiDanByCaiDanID(menuId);
            if (changYongCaiDan.ReturnCode == ReturnCode.SUCCESS
                && changYongCaiDan.Return.Count > 0)
            {
                var ischangyong = changYongCaiDan.Return[0].ISCHANGYONG;
                if (ischangyong != null)
                    isFavorite = ischangyong.Value;
                update = true;
            }

            var allChangYongCaiDan = caiDanService.GetALLChangYongCaiDanByCaiDanID(menuId);
            if (allChangYongCaiDan.ReturnCode == ReturnCode.SUCCESS
                && allChangYongCaiDan.Return.Count > 0)
            {
                var isallchangyong = allChangYongCaiDan.Return[0].ISQUANJVCY;
                if (isallchangyong != null)
                    isALLFavorite = isallchangyong.Value;
            }

            int xuhao = 0;
            var resultXuHao = caiDanService.getChangYongCDXH();
            if (resultXuHao.ReturnCode == ReturnCode.SUCCESS)
            {
                xuhao = resultXuHao.Return;
            }
            else
            {
                xuhao = 0;
            }
            if (update)     // 更新
            {
                if (isFavorite == 0)
                {
                    var caiDanUpdate = caiDanService.EditChangYongCaiDan(menuId, 1, xuhao);
                    if (caiDanUpdate.ReturnCode == ReturnCode.ERROR)
                        MediMsgBox.Warn(this, "设为常用菜单失败！");
                    else
                    {
                        if (isALLFavorite == 0)
                            AddBarMenu(menuId, page.Text);
                    }
                }
                else
                {
                    var caiDanUpdate = caiDanService.EditChangYongCaiDan(menuId, 0, 0);
                    if (caiDanUpdate.ReturnCode == ReturnCode.ERROR)
                        MediMsgBox.Warn(this, "取消常用菜单失败！");
                    else
                    {
                        if (isALLFavorite == 0)
                            RemoveBarMenu(menuId);
                    }
                }
            }
            else   // 新增
            {
                var result = caiDanService.AddChangYongCaiDan(menuId, page.Text, xuhao);
                if (result.ReturnCode == ReturnCode.ERROR)
                    MediMsgBox.Warn(this, "设为常用菜单失败！");
                else
                {
                    if (isALLFavorite == 0)
                        AddBarMenu(menuId, page.Text);
                }
            }
        }


        /// <summary>
        /// 添加全局常用菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiALLFavoriteMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraTabPage page = mediWorkShopTabControl1.SelectedTabPage;

            string menuId = GetMenuIDByTabPage(page);

            int? isFavorite = 0;
            int? isALLFavorite = 0;
            bool update = false;
            var changYongCaiDan = caiDanService.GetALLChangYongCaiDanByCaiDanID(menuId);
            if (changYongCaiDan.ReturnCode == ReturnCode.SUCCESS
                && changYongCaiDan.Return.Count > 0)
            {
                var ischangyong = changYongCaiDan.Return[0].ISQUANJVCY;
                if (ischangyong != null)
                    isFavorite = ischangyong.Value;
                update = true;
            }
            //获取全局菜单变量（添加与删除菜单）
            var qjChangYongCaiDan = caiDanService.GetChangYongCaiDanByCaiDanID(menuId);
            if (qjChangYongCaiDan.ReturnCode == ReturnCode.SUCCESS
                && qjChangYongCaiDan.Return.Count > 0)
            {
                var isALLChangyong = qjChangYongCaiDan.Return[0].ISCHANGYONG;
                if (isALLChangyong != null)
                    isALLFavorite = isALLChangyong.Value;
            }

            //获取序号
            int xuhao = 0;
            var resultXuHao = caiDanService.getQJChangYongCDXH();
            if (resultXuHao.ReturnCode == ReturnCode.SUCCESS)
            {
                xuhao = resultXuHao.Return;
            }
            else
            {
                xuhao = 0;
            }
            if (update)     // 更新
            {
                if (isFavorite == 0)
                {
                    var caiDanUpdate = caiDanService.EditALLChangYongCaiDan(menuId, 1, xuhao);
                    if (caiDanUpdate.ReturnCode == ReturnCode.ERROR)
                        MediMsgBox.Warn(this, "设为常用菜单失败！");
                    else
                    {
                        if (isALLFavorite == 0)//全局不进行操作
                            AddBarMenu(menuId, page.Text);
                    }
                }
                else
                {
                    var caiDanUpdate = caiDanService.EditALLChangYongCaiDan(menuId, 0, 0);
                    if (caiDanUpdate.ReturnCode == ReturnCode.ERROR)
                        MediMsgBox.Warn(this, "取消常用菜单失败！");
                    else
                    {
                        if (isALLFavorite == 0)
                            RemoveBarMenu(menuId);
                    }
                }
            }
            else   // 新增
            {
                var result = caiDanService.AddALLChangYongCaiDan(menuId, page.Text, xuhao);
                if (result.ReturnCode == ReturnCode.ERROR)
                    MediMsgBox.Warn(this, "设为常用菜单失败！");
                else
                {
                    if (isALLFavorite == 0)
                        AddBarMenu(menuId, page.Text);
                }
            }
        }


        /// <summary>
        /// 关闭其他
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiCloseOtherMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraTabPage selectPage = mediWorkShopTabControl1.SelectedTabPage;  // 当前选中
            for (int i = mediWorkShopTabControl1.TabPages.Count - 1; i >= 0; i--)
            {
                XtraTabPage page = mediWorkShopTabControl1.TabPages[i];
                if (string.Compare(selectPage.Name, page.Name, StringComparison.OrdinalIgnoreCase) != 0)
                {
                    CloseTabPage(page);
                }
            }
        }
        /// <summary>
        /// 关闭当前菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiCloseCurrentMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraTabPage page = mediWorkShopTabControl1.SelectedTabPage;
            CloseTabPage(page);
        }

        /// <summary>
        /// 关闭所有菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiCloseAllMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            for (int i = mediWorkShopTabControl1.TabPages.Count - 1; i >= 0; i--)
            {
                XtraTabPage page = mediWorkShopTabControl1.TabPages[i];
                CloseTabPage(page);
            }
        }

        /// <summary>
        /// 固定标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiFixedMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraTabPage page = mediWorkShopTabControl1.SelectedTabPage;
            string menuId = GetMenuIDByTabPage(page);
            int? isOpen = 0;
            var caiDan = caiDanService.GetCaiDanNew(menuId);
            if (caiDan.ReturnCode == ReturnCode.SUCCESS)
                isOpen = caiDan.Return[0].ISOPEN;

            if (isOpen == 0)
            {
                var caiDanUpdate = caiDanService.EditCaiDan(menuId, 1);
                if (caiDanUpdate.ReturnCode == ReturnCode.ERROR)
                    MediMsgBox.Warn(this, "固定失败！");
                else if (caiDanUpdate.ReturnCode == ReturnCode.SUCCESS)
                    CanDanList.First(c => c.CAIDANID == menuId).ISOPEN = 1;
            }
            else
            {
                var caiDanUpdate = caiDanService.EditCaiDan(menuId, 0);
                if (caiDanUpdate.ReturnCode == ReturnCode.ERROR)
                    MediMsgBox.Warn(this, "取消固定失败！");
                else if (caiDanUpdate.ReturnCode == ReturnCode.SUCCESS)
                    CanDanList.First(c => c.CAIDANID == menuId).ISOPEN = 0;
            }
        }

        /// <summary>
        /// 鼠标在最上面标题移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediLCMainFormTabControl_HotTrackedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            var tabPage = mediLCMainFormTabControl.HotTrackedTabPage;

            if (tabPage == null)
                return;
            //先关闭菜单弹出框，防止鼠标移动时弹出框闪
            moreMenuPM.HidePopup();
            mediLCMainFormTabControl.SelectedTabPage = tabPage;
            BaseTabPageViewInfo baseTabPageViewInfo = new BaseTabPageViewInfo(tabPage);
            //tab页的位置
            var tabLocation = baseTabPageViewInfo.ViewInfo.SelectedTabPageViewInfo.Bounds;

            var tabControlpoint = mediLCMainFormTabControl.PointToScreen(new Point(tabLocation.X, tabLocation.Y));
            var caidanid = tabPage.Name;
            if (string.IsNullOrWhiteSpace(caidanid))
                return;
            LoadDefaultMenu(caidanid);

            Point point = new Point(tabControlpoint.X, tabControlpoint.Y + tabLocation.Height);
            moreMenuPM.ShowPopup(point);
        }
        public override void RemoveCloseButtonFireChuangKouCK(XtraForm form)
        {
            if (form is MediFormWithQX mediFormWithQX)
            {

                OpenCKInfo openCKInfo = new OpenCKInfo()
                {
                    openWindowMode = OpenWindowMode.Menu,
                    xitongid = HISClientHelper.XITONGID,
                    gongnengId = mediFormWithQX.GongNengId,
                    caidanId = mediFormWithQX.CaiDanID,
                    chuangkoumc = mediFormWithQX.Name.ToUpper()
                };
                if (BaseFormCommonHelper.openwindowsdic.ContainsKey(openCKInfo))
                    BaseFormCommonHelper.openwindowsdic.Remove(openCKInfo);
                BaseFormCommonHelper.RemovePage(mediFormWithQX);
            }

        }
    }

}