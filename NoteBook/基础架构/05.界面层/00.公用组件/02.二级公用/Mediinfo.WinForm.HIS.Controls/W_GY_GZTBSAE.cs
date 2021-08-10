using DevExpress.XtraBars;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Mediinfo.WinForm.HIS.Controls.TabForm;
using Mediinfo.WinForm.HIS.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class W_GY_GZTBSAE : MediFormLCWithQX
    {
        #region constructor

        private JCJGCaiDanService caiDanService = new JCJGCaiDanService();
        JCJGYingYongCDService gYYingYongCDService = new JCJGYingYongCDService();
        
        string bbiMenuCanShu = GYCanShuHelper.GetCanShu(HISClientHelper.YINGYONGID, "公用_是否启用右键菜单", "0");
        public W_GY_GZTBSAE()
        {
            InitializeComponent();
            if (bbiMenuCanShu == "1")
            {
                this.bbiFixedMenu.Visibility = BarItemVisibility.Never;
                this.bbiCloseAllMenu.Visibility = BarItemVisibility.Never;
                this.bbiCloseCurrentMenu.Visibility = BarItemVisibility.Never;
                this.bbiCloseOtherMenu.Visibility = BarItemVisibility.Never;
            } 
            RegisterEvents();

            OpenlcWindow = OpenLinChungWindow;
        }

        #endregion

        #region fields

        public Action<MediFormLCWithQX, OpenType> OpenlcWindow;
        private int index = 1;  // 常用按钮类型

        /// <summary>
        ///  病人ID
        /// </summary>
        internal virtual string BingRenID { get; set; }
        /// <summary>
        /// 已打开的窗口集合
        /// </summary>
        public Dictionary<OpenCKInfo, MediForm> openwindowsdic = new Dictionary<OpenCKInfo, MediForm>();
        ///// <summary>
        /////工作台已打开的所有tab页(此窗体基类为W_LINCHUANG_BASE)
        ///// </summary>
        public Dictionary<string, dynamic> openedwpdic = new Dictionary<string, dynamic>();

        /// <summary>
        /// 目标窗口信息
        /// </summary>
        public MuBiaoFormInformation MuBiaoCKInfo { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvents()
        {
            this.Load -= new System.EventHandler(this.W_GY_GZTBSAE_Load);
            this.Load += new System.EventHandler(this.W_GY_GZTBSAE_Load);
            //this.mediWorkShopTabControl.SelectedPageChanged += MediWorkShopTabControl_SelectedPageChanged;
            this.mediWorkShopTabControl.TabPages.CollectionChanged += TabPages_CollectionChanged;
        }

        private void TabPages_CollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            if (mediWorkShopTabControl.TabPages.Count > 0)
                mediWorkShopTabControl.Visible = true;
            else
                mediWorkShopTabControl.Visible = false;
            //没有tabpage时隐藏tabcontrol，从而显示panel上的提示图片
            //mediWorkShopTabControl.Visible = mediWorkShopTabControl.TabPages.Count > 0;
        }

        private void MediWorkShopTabControl_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (e.Page == null)
                return;
            foreach (Control control in e.Page.Controls)
            {
                foreach (var item in control.Controls)
                {
                    if (item is W_LINCHUANG_BASE)
                    {
                        W_LINCHUANG_BASE tempForm = item as W_LINCHUANG_BASE;
                        //HISClientHelper.ower = tempForm;
                    }
                    if (item is MediFormWithQX)
                    {
                        MediFormWithQX tempForm = item as MediFormWithQX;
                        //HISClientHelper.ower = tempForm;
                    }
                }
            }
        }

        /// <summary>
        /// 加载默认菜单和工具条
        /// </summary>
        public void LoadDefaultMenuAndToolBar()
        {
            CaiDanList = gYYingYongCDService.GetYingYongCD().Return;
            currentCaiDanId = CaiDanList.Where(o => o.CAIDANMC == "工作台").FirstOrDefault().CAIDANID;

            if (CaiDanList.Count < 1)
                return;
            int caidanLength = 0;
            if (CaiDanList.Count > 0)
            {
                caidanLength = CaiDanList.Count - 1;
            }
            if (caidanLength < 1) return;
            if (YongHuQXList == null || YongHuQXList.Count > 0)
            {
                YongHuQXList = GYQuanXianHelper.GetQuanXian();
            }
            for (int i = caidanLength; i >= 0; i--)
            {
                string shangjicdid = CaiDanList[i].SHANGJICDID;
                if (shangjicdid == "-" || shangjicdid == null)
                {
                    shangjicdid = "";
                }
                if (shangjicdid.Length > 0)
                {
                    string gongnengid = CaiDanList[i].GONGNENGID;
                    if (YongHuQXList != null)
                    {
                        //判断功能权限
                        var yonghuqx = YongHuQXList.Where(o => o.GONGNENGID == gongnengid).FirstOrDefault();
                        if (yonghuqx == null)
                        {
                            CaiDanList[i].Delete();
                        }
                    }
                }
            }
            //移除没有权限的菜单
            if (GYCanShuHelper.GetCanShu("是否启用菜单权限控制", "0").Equals("1"))
                CaiDanList.RemoveAll(c => c.GetState() == DTOState.Delete);

            //一级菜单
            CaiDanList.Where(o => o.SHANGJICDID.Equals(currentCaiDanId)).OrderBy(o => o.SHUNXUHAO).ToList().ForEach(o =>
            {
                DevExpress.XtraBars.BarItem item;

                var count = CaiDanList.Where(r => r.SHANGJICDID == o.CAIDANID).Count();
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
                //item.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
                item.ItemAppearance.Hovered.Options.UseBackColor = true;
                item.ItemAppearance.Hovered.Options.UseForeColor = true;
                item.ItemAppearance.Hovered.BackColor = Color.FromArgb(180, 215, 245);
                //item.ItemAppearance.Hovered.ForeColor = Color.White;

                item.ItemAppearance.Pressed.Options.UseForeColor = true;
                item.ItemAppearance.Pressed.Options.UseBackColor = true;
                item.ItemAppearance.Pressed.BackColor = Color.FromArgb(5, 145, 206);
                item.ItemAppearance.Pressed.ForeColor = Color.White;


                moreMenuPM.AddItem(item);
            });
            //二三级菜单
            ChuangJianCD(moreMenuPM.LinksPersistInfo, CaiDanList);

            // 加载常用菜单
            LoadFavoriteMenu();

            //加载工作台菜单数据源
            LoadGongZuoTaiMenuDatasource();
        }

        /// <summary>
        /// 打开病人框架内的窗体
        /// </summary>
        /// <param name="mediFormWithQX">窗体实例</param>
        /// <param name="openType">窗体类型</param>
        public void OpenLinChungWindow(MediFormLCWithQX mediFormWithQX, OpenType openType)
        {
            string openFangShi = string.Empty;
            switch (openType)
            {
                case OpenType.Open:
                    openFangShi = "OPEN";
                    break;
                case OpenType.OPENSHEET:
                    openFangShi = "OPENSHEET";
                    break;
                case OpenType.SHOW:
                    openFangShi = "SHOW";
                    break;
                default:
                    break;
            }
            string buttonid = string.Format("{0}\\{1}", HISClientHelper.YINGYONGID, mediFormWithQX.Name);
            string caidanid = ((int)Convert.ToInt64(MediUniversalMFBase.Hash2MD516(buttonid), 16)).ToString();

            DTO.HIS.GY.E_GY_CAIDAN_NEW denglucd = new E_GY_CAIDAN_NEW() { CAIDANMC = mediFormWithQX.Text, CAIDANID = caidanid, GONGNENGID = "gn+" + caidanid + "", DIAOYONGCS = "" + mediFormWithQX.Name.ToUpper() + "|" + caidanid + "||" + openFangShi + "" };
            this.SuspendLayout();
            BaseFormCommonHelper.CreateForm(this, this.mediWorkShopTabControl, denglucd, ref openwindowsdic, false);
            var formname = mediFormWithQX.Name;

            OpenCKInfo openCKInfo = new OpenCKInfo() { openWindowMode = OpenWindowMode.Button, xitongid = Mediinfo.HIS.Core.HISClientHelper.XITONGID, gongnengId = denglucd.GONGNENGID, caidanId = denglucd.CAIDANID, chuangkoumc = formname.ToUpper(), binrenid = BingRenID };
            BaseFormCommonHelper.openwindowsdic = openwindowsdic;
            if (openwindowsdic.ContainsKey(openCKInfo) && !openedwpdic.ContainsKey(openCKInfo.chuangkoumc))
            {
                if (openwindowsdic[openCKInfo] is MediFormWithQX workshopbase)
                {
                    workshopbase.FormClosed += Workshopbase_FormClosed;
                    openedwpdic.Add(openCKInfo.chuangkoumc, openwindowsdic[openCKInfo] as MediFormWithQX);
                }
            }

            this.ResumeLayout(false);
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

                        tsmi.ItemClick += new ItemClickEventHandler(caidanlan_ItemClick);
                        //必须要 要不然子菜单无法显示
                        this.moreMenuPM.Manager = this.moreMenuBM;
                        if (breaksplit)
                        {
                            (o.Item as BarSubItem)?.LinksPersistInfo.Add(new LinkPersistInfo(tsmi, false));
                            breaksplit = false;
                        }
                        else
                        {
                            (o.Item as BarSubItem)?.LinksPersistInfo.Add(new LinkPersistInfo(tsmi, false));
                        }
                    }
                });
                if (o.Item is BarSubItem item && item.LinksPersistInfo.Count > 0)
                {
                    ChuangJianCD(item.LinksPersistInfo, caidanList);
                }
            }
        }

        /// <summary>
        /// 加载常用菜单
        /// </summary>
        private  void LoadFavoriteMenu()
        {
            var result =  caiDanService.GetALLChangYongCaiDanList();
            if (result.ReturnCode == ReturnCode.SUCCESS)
            {
                List<E_GY_CHANGYONGCAIDAN> list = result.Return;
                var list1 = list.Where((x, i) => list.FindIndex(z => z.CAIDANID == x.CAIDANID) == i);//全局和常用菜单可能会重复，过滤掉一条常用。

                //for (int i = 0; i < list.Count - 1; i++)
                //{
                //    for (int j = i + 1; j < list.Count; j++)
                //    {
                //        if (list[i].CAIDANID.Equals(list[j].CAIDANID))
                //        {
                //            list.RemoveAt(j);
                //            j--;
                //        }
                //    }
                //}

                foreach (E_GY_CHANGYONGCAIDAN item in list1)
                {
                    AddBarMenu(item.CAIDANID, item.CAIDANMC);
                }
            }
        }

        /// <summary>
        /// 添加常用菜单按钮到工具条上
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <param name="menuName">菜单名称</param>
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

            this.bar1.AddItem(buttonItem); ;

            index++;
        }

        /// <summary>
        /// 删除常用菜单
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        private void RemoveBarMenu(string menuId)
        {
            for (int i = bar1.LinksPersistInfo.Count - 1; i >= 0; i--)
            {
                BarItemLink item = bar1.LinksPersistInfo[i].Link;
                if (String.Compare(item.Item.Tag.ToString(), menuId, true) == 0)
                {
                    bar1.RemoveLink(item);
                }
            }
        }

        /// <summary>
        /// 在选项卡重打开页面
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <param name="opened">是否默认打开</param>
        public void OpenFormFromTab(string menuId, bool opened = false)
        {
            var denglucd = CaiDanList.FirstOrDefault(o => o.CAIDANID == menuId);
            if (denglucd == null && string.IsNullOrEmpty(menuId))
            {
                MediMsgBox.Warn("没有找到【" + HISClientHelper.YINGYONGID + "】对应的菜单ID【" + menuId + "】");
                return;
            }
            foreach (var item in BaseFormCommonHelper.openwindowsdic)
            if (openwindowsdic.Where(d => d.Key.CustomCompareVaue(item.Key)).Count() == 0)openwindowsdic.Add(item.Key, item.Value);
            BaseFormCommonHelper.CreateForm(this, mediWorkShopTabControl, denglucd, ref openwindowsdic, opened);
        }

        /// <summary>
        /// 加载工作台菜单数据源
        /// </summary>
        private void LoadGongZuoTaiMenuDatasource()
        {
            var resultList = new List<E_GY_CAIDAN_NEW>();

            CaiDanList.Where(o => o.SHANGJICDID.Equals(currentCaiDanId)).OrderBy(o => o.SHUNXUHAO).ToList().ForEach(o =>
            {
                var ziCaiDanList = CaiDanList.FindAll(r => r.SHANGJICDID == o.CAIDANID);//子菜单
                if (ziCaiDanList.Count == 0)
                {
                    resultList.Add(o);
                }
                else
                {
                    resultList.AddRange(ziCaiDanList);
                }

            });
            eGYCAIDANNEWBindingSource.DataSource = resultList;
        }

        /// <summary>
        /// 关闭选项卡
        /// </summary>
        /// <param name="page">选项卡</param>
        public void CloseTabPage(XtraTabPage page)
        {
            foreach (Control control in page.Controls)
            {
                foreach (var item in control.Controls)
                {
                    if (item is W_LINCHUANG_BASE tempForm1)
                    {
                        OpenCKInfo openCKInfo = new OpenCKInfo { openWindowMode = OpenWindowMode.Menu, xitongid = Mediinfo.HIS.Core.HISClientHelper.XITONGID, gongnengId = tempForm1.denglucd.GONGNENGID, caidanId = tempForm1.denglucd.CAIDANID, chuangkoumc = tempForm1.Name.ToUpper(), binrenid = tempForm1.bingRenId };
                        if (BaseFormCommonHelper.openAllwindowsdic.ContainsKey(openCKInfo))
                        {
                            BaseFormCommonHelper.openAllwindowsdic.Remove(openCKInfo);
                        }
                        if (BaseFormCommonHelper.openwindowsdic.ContainsKey(openCKInfo))
                        {
                            BaseFormCommonHelper.openwindowsdic.Remove(openCKInfo);
                        }
                    }
                    if (item is MediFormWithQX tempForm)
                    {
                        tempForm.ValidateDataModifiedBeforeClose();//界面关闭前校验数据是否更改
                        tempForm.Close();

                        if (tempForm.IsDisposed)
                        {
                            OpenCKInfo openCKInfo = new OpenCKInfo() { openWindowMode = OpenWindowMode.Menu, xitongid = Mediinfo.HIS.Core.HISClientHelper.XITONGID, gongnengId = tempForm.GongNengId, caidanId = tempForm.CaiDanID, chuangkoumc = tempForm.Name.ToUpper() };

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
                        }
                        else
                        {
                            return;
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
                    if (item is W_LINCHUANG_BASE)
                    {
                        W_LINCHUANG_BASE tempForm = item as W_LINCHUANG_BASE;
                        menuId = tempForm.CaiDanID;
                    }

                    if (item is MediFormWithQX)
                    {
                        MediFormWithQX tempForm = item as MediFormWithQX;
                        menuId = tempForm.CaiDanID;
                    }
                }
            }
            return menuId;
        }

        /// <summary>
        /// 加载默认打开的界面
        /// </summary>
        public async void InitTabPage()
        {
            //BaseFormCommonHelper.LoadDefaultOpenForm(this, currentCaiDanId, mediWorkShopTabControl, openedwpdic);

            List<string> list = new List<string>();
            var isOpenCd = await caiDanService.GetYingYongCDAsync();
            if (isOpenCd.ReturnCode == ReturnCode.SUCCESS)
            {
                list.AddRange(isOpenCd.Return.Select(cd => cd.CAIDANID));
            }

            foreach (string menuId in list)
            {
                OpenFormFromTab(menuId,true);
            }
        }

        /// <summary>
        /// 关闭所有选项卡
        /// </summary>
        public void CloseAllTabPages()
        {
            for (int i = mediWorkShopTabControl.TabPages.Count - 1; i >= 0; i--)
            {
                XtraTabPage page = mediWorkShopTabControl.TabPages[i];
                CloseTabPage(page);
            }
        }

        /// <summary>
        /// 是否存在未关闭的界面(默认打开的除外)
        /// </summary>
        /// <returns></returns>
        public bool ExistOpenedForms()
        {
            //只需检测是否存在ShowCloseButton为true的tabpage即可
            return mediWorkShopTabControl.TabPages.Count(t => t.ShowCloseButton == DevExpress.Utils.DefaultBoolean.True) > 0;
        }

        #endregion

        #region override

        /// <summary>
        /// 初始化菜单数据
        /// </summary>
        public override void InitialCaiDanData()
        {
            BaseFormCommonHelper.openwindowsdic = openwindowsdic;
            LoadDefaultMenuAndToolBar();
        }

        #endregion

        #region events

        private void W_GY_GZTBSAE_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
            InitTabPage();

            //隐藏“更多菜单”按钮边框
            ddMoreMenuBtn.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            ddMoreMenuBtn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            ddMoreMenuBtn.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
        }

        private void caidanlan_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenFormFromTab(e.Item.Name);
        }

        private void Workshopbase_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is MediFormWithQX mediFormWithQx 
                && openedwpdic.ContainsKey(mediFormWithQx.Name.ToUpper()))
            {
                openedwpdic.Remove(mediFormWithQx.Name.ToUpper());
            }
        }

        /// <summary>
        /// 关闭选项卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediWorkShopTabControl_CloseButtonClick(object sender, EventArgs e)
        {
            if (e is ClosePageButtonEventArgs closePageButtonEventArgs)
            {
                XtraTabPage xtraTabPage = closePageButtonEventArgs.Page as XtraTabPage;

                CloseTabPage(xtraTabPage);
            }
        }

        /// <summary>
        /// 鼠标点击(右击)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediWorkShopTabControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                XtraTabPage page = mediWorkShopTabControl.SelectedTabPage;
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
                if (caiDan.ReturnCode == Enterprise.ReturnCode.SUCCESS)
                    isOpen = caiDan.Return[0].ISOPEN;

                this.bbiFixedMenu.Caption = isOpen != 0 ? "取消固定标签" : "固定标签";

                int? isFavorite = 0;
                var changYongCaiDan = caiDanService.GetChangYongCaiDanByCaiDanID(menuId);
                if (changYongCaiDan.ReturnCode == ReturnCode.SUCCESS)
                {
                    if (changYongCaiDan.Return.Count > 0)
                    {
                        var ischangyong = changYongCaiDan.Return[0].ISCHANGYONG;
                        if (ischangyong != null)
                            isFavorite = ischangyong.Value;
                    }
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
        /// 添加全局常用菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiALLFavoriteMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraTabPage page = mediWorkShopTabControl.SelectedTabPage;

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
        /// 添加常用菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiFavoriteMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraTabPage page = mediWorkShopTabControl.SelectedTabPage;

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
        /// 常用菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenFormFromTab(e.Item.Tag.ToString());
        }

        /// <summary>
        /// 打开选中的菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediGridLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            var caiDanID = mediGridLookUpEdit1.EditValue?.ToString();//选中的菜单ID
            if (!string.IsNullOrWhiteSpace(caiDanID))
                OpenFormFromTab(caiDanID);
        }

        /// <summary>
        /// 设置MediGridLookUpEdit下拉框宽度与控件同步
        /// </summary>
        private void mediGridLookUpEdit1_QueryPopUp(object sender, CancelEventArgs e)
        {
            MediGridLookUpEdit editor = (MediGridLookUpEdit)sender;
            RepositoryItemMediGridLookUpEdit properties = editor.Properties;
            properties.PopupFormSize = new Size(editor.Width - 4, properties.PopupFormSize.Height);
        }

        /// <summary>
        /// 关闭当前菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiCloseCurrentMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraTabPage page = mediWorkShopTabControl.SelectedTabPage;
            CloseTabPage(page);
        }

        /// <summary>
        /// 关闭所有菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiCloseAllMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            for (int i = mediWorkShopTabControl.TabPages.Count - 1; i >= 0; i--)
            {
                XtraTabPage page = mediWorkShopTabControl.TabPages[i];
                CloseTabPage(page);
            }
        }

        /// <summary>
        /// 关闭其他
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiCloseOtherMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraTabPage selectPage = mediWorkShopTabControl.SelectedTabPage;  // 当前选中
            for (int i = mediWorkShopTabControl.TabPages.Count - 1; i >= 0; i--)
            {
                XtraTabPage page = mediWorkShopTabControl.TabPages[i];
                if (String.Compare(selectPage.Name, page.Name, StringComparison.OrdinalIgnoreCase) != 0)
                {
                    CloseTabPage(page);
                }
            }
        }

        /// <summary>
        /// 固定标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiFixedMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            XtraTabPage page = mediWorkShopTabControl.SelectedTabPage;

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
                    CaiDanList.First(c => c.CAIDANID == menuId).ISOPEN = 1;
            }
            else
            {
                var caiDanUpdate = caiDanService.EditCaiDan(menuId, 0);
                if (caiDanUpdate.ReturnCode == ReturnCode.ERROR)
                    MediMsgBox.Warn(this, "取消固定失败！");
                else if (caiDanUpdate.ReturnCode == ReturnCode.SUCCESS)
                    CaiDanList.First(c => c.CAIDANID == menuId).ISOPEN = 0;
            }
        }

        #endregion
    }
}