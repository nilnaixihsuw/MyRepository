using DevExpress.CodeParser;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.ButtonPanel;
using DevExpress.XtraTab;
using DevExpress.XtraTab.Drawing;
using DevExpress.XtraTab.Registrator;
using DevExpress.XtraTab.ViewInfo;

using Mediinfo.HIS.Core;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using static Mediinfo.WinForm.MediSkinTabHeaderViewInfo;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 显示可以放置控件的选项卡式页面控件
    /// </summary>
    [ToolboxItem(true)]
    public partial class MediTabControl : XtraTabControl
    {
        #region constructor

        public MediTabControl()
        {
            InitializeComponent();

            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        #endregion

        #region properties

        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        /// <summary>
        /// 居左距离
        /// </summary>
        [Browsable(true), DefaultValue(20), Description("tabpage靠左的距离")]
        public int LeftDistance { get; set; } = 20;

        /// <summary>
        /// 是否停靠两边
        /// </summary>
        [Browsable(false), DefaultValue(0), Description("是否停靠两边")]
        public bool IsAdsSide { get; set; } = false;

        /// <summary>
        /// tab页间距
        /// </summary>
        [Browsable(true), DefaultValue(2), Description("tab页间距")]
        public int Interval { get; set; } = 2;

        /// <summary>
        /// 主题样式
        /// </summary>
        [Browsable(true), DefaultValue(0), Description("主题样式")]
        public MediTabControlStyle MediTabControlTheme { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true), Description("设置Tab标题背景色")]
        public bool SetCustomHeaderColor { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true), Description("Tab标题背景色-激活状态")]
        public Color MediHeadActiveColor { get; set; } = Color.Transparent;

        /// <summary>
        /// 
        /// </summary>
        [Browsable(true), Description("Tab标题背景色-非激活状态")]
        public Color MediHeadDisableColor { get; set; } = Color.Transparent;

        #endregion

        #region override

        protected override void CheckInfo()
        {
            FieldInfo fi = typeof(XtraTabControl).GetField("view", BindingFlags.Instance | BindingFlags.NonPublic);

            if (SetCustomHeaderColor)
            {
                MediSkinViewInfoRegistrator skinViewInfoRegistrator = new MediSkinViewInfoRegistrator();
                skinViewInfoRegistrator.ActiveColor = MediHeadActiveColor;
                skinViewInfoRegistrator.DisableColor = MediHeadDisableColor;
                skinViewInfoRegistrator.SetHeaderColor = SetCustomHeaderColor;
                fi.SetValue(this, skinViewInfoRegistrator);
            }
            else
            {
                fi.SetValue(this, new MediSkinViewInfoRegistrator());
            }
            CreateView();
        }

        protected override void UpdatePageSize()
        {
            base.UpdatePageSize();

            // 判断是否为线性Tab页
            if (MediTabControlTheme == MediTabControlStyle.LineTabHeader)
            {
                for (int i = 0; i < this.TabPages.Count; i++)
                {
                    if (this.TabPages[i].Padding.Equals(new Padding(0)))
                        this.TabPages[i].Padding = new Padding(0, 2, 0, 0);

                    this.TabPages[i].Paint -= MediTabControl_Paint;
                    this.TabPages[i].Paint += MediTabControl_Paint;
                }
            }
        }

        #endregion

        #region events

        private void MediTabControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // 通过重绘的形式设置背景色为白色
            using (Brush brush = new SolidBrush(Color.White))
            {
                e.Graphics.FillRectangle(brush, e.ClipRectangle);
            }

            // 线性Tab页添加一条灰实线
            Rectangle rectangle = e.ClipRectangle;
            Pen pen = new Pen(Color.FromArgb(230, 230, 230), 2);    // 画笔
            e.Graphics.DrawLine(pen, rectangle.X, rectangle.Y, rectangle.X + rectangle.Width, rectangle.Y);
            pen.Dispose();
        }

        #endregion
    }

    /// <summary>
    /// TabControl样式
    /// </summary>
    public enum MediTabControlStyle
    {
        /// <summary>
        /// 线性tabheader(选中)
        /// </summary>
        LineTabHeader = 0,
        /// <summary>
        /// 蓝色按钮(选中)
        /// </summary>
        TabHeaderBlueButton = 1,
        /// <summary>
        /// 蓝色按钮和蓝色底线(选中)
        /// </summary>
        TabHeaderBlueButtonAndLine = 2,
        /// <summary>
        /// 灰底设置(选中)
        /// </summary>
        LineTabHeaderTrans = 3
    }

    /// <summary>
    /// 注册视图
    /// </summary>
    public class MediSkinViewInfoRegistrator : SkinViewInfoRegistrator
    {

        public Color ActiveColor { get; set; }

        public Color DisableColor { get; set; }

        public bool SetHeaderColor { get; set; }

        #region constructor

        /// <summary>
        /// 构造函数
        /// </summary>
        public MediSkinViewInfoRegistrator()
            : base()
        {

        }

        #endregion

        #region properties

        public override string ViewName
        {
            get
            {
                return "MediTabControl";
            }
        }

        #endregion

        #region override

        /// <summary>
        /// 创建头部视图
        /// </summary>
        /// <param name="viewInfo"></param>
        /// <returns></returns>
        public override BaseTabHeaderViewInfo CreateHeaderViewInfo(BaseTabControlViewInfo viewInfo)
        {
            return new MediSkinTabHeaderViewInfo(viewInfo);
        }

        public override BaseTabPainter CreatePainter(IXtraTab tabControl)
        {
            if (SetHeaderColor)
            {
                MediSkinTabPainter mediSkinTabPainter = new MediSkinTabPainter(tabControl);
                mediSkinTabPainter.ActiveColor = ActiveColor;
                mediSkinTabPainter.DisableColor = DisableColor;
                mediSkinTabPainter.SetHeaderColor = SetHeaderColor;
                return mediSkinTabPainter;
            }
            return base.CreatePainter(tabControl);
        }


        /// <summary>
        /// 创建视图
        /// </summary>
        /// <param name="tabControl"></param>
        /// <returns></returns>
        public override BaseTabControlViewInfo CreateViewInfo(IXtraTab tabControl)
        {
            return new MediViewInfo(tabControl);
        }

        public override BaseButtonsPanelPainter CreateControlBoxPainter(IXtraTab tabControl)
        {
            return new MediTabButtonsPanelSkinPainter(tabControl.LookAndFeel);
        }

        #endregion
    }

    public class MediTabButtonsPanelSkinPainter : TabButtonsPanelSkinPainter
    {
        #region constructor

        public MediTabButtonsPanelSkinPainter(ISkinProvider provider)
            : base(provider)
        {

        }

        #endregion

        #region override

        public override BaseButtonPainter GetButtonPainter()
        {
            return new MediTabButtonSkinPainter(Provider);
        }

        #endregion
    }

    public class MediTabButtonSkinPainter : TabButtonSkinPainter
    {
        #region constructor

        public MediTabButtonSkinPainter(ISkinProvider provider)
            : base(provider)
        {
            if (((UserLookAndFeel)provider).OwnerControl is MediTabControl)
            {
                mediTabControl = ((UserLookAndFeel)provider).OwnerControl as MediTabControl;
            }
        }

        #endregion

        #region fields

        private MediTabControl mediTabControl;

        #endregion

        #region override

        protected override void DrawBackground(GraphicsCache cache, BaseButtonInfo info)
        {
            SkinElement skinElement = TabSkins.GetSkin(SkinProvider)["TabPageButton"];
            if (mediTabControl.MediTabControlTheme == MediTabControlStyle.LineTabHeader)
            {
                if (((BaseTabPageViewInfo)info.ButtonPanelOwner).IsActiveState)
                    skinElement = TabSkins.GetSkin(SkinProvider)["ActiveSuperTabPageButton"] ?? TabSkins.GetSkin(SkinProvider)["TabPageButton"];
                else
                    skinElement = TabSkins.GetSkin(SkinProvider)["InActiveTabPageButton"] ?? TabSkins.GetSkin(SkinProvider)["TabPageButton"];
            }
            SkinElementInfo skinElementInfo = new SkinElementInfo(skinElement, info.Bounds);
            skinElementInfo.State = info.State;
            skinElementInfo.Cache = cache;
            skinElementInfo.ImageIndex = CalcImageIndexCore(info.State, skinElementInfo);
            skinElementInfo.GlyphIndex = CalcGlyphIndex(info.State, info);
            ObjectPainter.DrawObject(cache, SkinElementPainter.Default, skinElementInfo);
        }

        #endregion
    }

    /// <summary>
    /// 创建视图
    /// </summary>
    public class MediViewInfo : SkinTabControlViewInfo
    {
        #region constructor

        public MediViewInfo(IXtraTab tabControl)
            : base(tabControl)
        {
            if (tabControl is MediTabControl)
            {
                if (ControlCommonHelper.IsDesignMode())
                {
                    if (Skin["TabPane"] != null && Skin["TabHeader"] != null)
                    {
                        skinPane = Skin["TabPane"];
                        skinHeader = Skin["TabHeader"];
                    }
                    return;
                }

                MediTabControl mediTabControl = tabControl as MediTabControl;
                if (mediTabControl.MediTabControlTheme == MediTabControlStyle.LineTabHeader)
                {

                    mediTabControl.AppearancePage.Header.ForeColor = Color.FromArgb(51, 51, 51);
                    mediTabControl.AppearancePage.HeaderActive.ForeColor = Color.FromArgb(0, 115, 195);
                    if (Skin["ZJTabPane"] != null && Skin["ZJTabHeader"] != null)
                    {
                        skinPane = Skin["ZJTabPane"];
                        skinHeader = Skin["ZJTabHeader"];
                    }
                    else
                    {
                        skinPane = Skin["TabPane"];
                        skinHeader = Skin["TabHeader"];
                    }

                }
                else if (mediTabControl.MediTabControlTheme == MediTabControlStyle.TabHeaderBlueButton)
                {
                    mediTabControl.AppearancePage.Header.ForeColor = Color.FromArgb(51, 51, 51);
                    mediTabControl.AppearancePage.HeaderActive.ForeColor = Color.FromArgb(255, 255, 255);
                    if (Skin["YSTabPane"] != null && Skin["YSTabHeader"] != null)
                    {
                        skinPane = Skin["YSTabPane"];
                        skinHeader = Skin["YSTabHeader"];
                    }
                    else
                    {
                        skinPane = Skin["TabPane"];
                        skinHeader = Skin["TabHeader"];
                    }

                }
                else if (mediTabControl.MediTabControlTheme == MediTabControlStyle.LineTabHeaderTrans)
                {
                    mediTabControl.AppearancePage.Header.ForeColor = Color.FromArgb(51, 51, 51);
                    mediTabControl.AppearancePage.HeaderActive.ForeColor = Color.FromArgb(0, 115, 195);
                    if (Skin["ZJTransTabPane"] != null && Skin["ZJTransTabHeader"] != null)
                    {
                        skinPane = Skin["ZJTransTabPane"];
                        skinHeader = Skin["ZJTransTabHeader"];
                    }
                    else
                    {
                        skinPane = Skin["TabPane"];
                        skinHeader = Skin["TabHeader"];
                    }
                }
                else if (mediTabControl.MediTabControlTheme == MediTabControlStyle.TabHeaderBlueButtonAndLine)
                {
                    mediTabControl.AppearancePage.Header.ForeColor = Color.FromArgb(51, 51, 51);
                    mediTabControl.AppearancePage.HeaderActive.ForeColor = Color.FromArgb(255, 255, 255);
                    if (Skin["TabPane"] != null && Skin["TabHeader"] != null)
                    {
                        skinPane = Skin["TabPane"];
                        skinHeader = Skin["TabHeader"];
                    }
                }
            }
        }

        #endregion

        #region fields

        private SkinElement skinPane;
        public SkinElement skinHeader;

        #endregion

        #region properties

        public override SkinElement SkinPane => skinPane;
        public override SkinElement SkinHeader => skinHeader;

        #endregion
    }

    public class MediSkinTabHeaderViewInfo : SkinTabHeaderViewInfo
    {
        #region constructor

        public MediSkinTabHeaderViewInfo(BaseTabControlViewInfo viewInfo)
            : base(viewInfo)
        {

        }

        #endregion
        /// <summary>
        /// UpdatePageInfo
        /// </summary>
        /// <param name="pInfo"></param>
        /// <returns></returns>
        public MediSkinTabPageObjectInfo UpdatePageInfo(BaseTabPageViewInfo pInfo)
        {
            SkinTabControlViewInfo vi = ViewInfo as SkinTabControlViewInfo;
            MediSkinTabPageObjectInfo info = new MediSkinTabPageObjectInfo(vi.SkinHeader, pInfo);
            info.HeaderLocation = HeaderLocation;
            info.ImageIndex = 0;
            return info;
        }

        #region override

        /// <summary>
        /// CalcPageClientSize
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override Size CalcPageClientSize(BaseTabPageViewInfo info)
        {
            Size size = base.CalcPageClientSize(info);
            Size min = CalcMinPageSize();
            size.Width = Math.Max(size.Width, min.Width);
            if (this.TabControl is MediTabControl)
            {

                MediTabControl mediTabControl = (MediTabControl)TabControl;
                if (mediTabControl.IsAdsSide)
                    size.Width = (mediTabControl.Width - (mediTabControl.TabPages.Count - 1) * mediTabControl.Interval) / 2 - 23;
            }

            size.Height = 28;
            size.Height = Math.Max(size.Height, min.Height);
            return size;
        }

        /// <summary>
        /// UpdatePageBounds
        /// </summary>
        /// <param name="info"></param>
        protected override void UpdatePageBounds(BaseTabPageViewInfo info)
        {
            if (!string.IsNullOrWhiteSpace(HISClientHelper.XITONGID) && !HISClientHelper.LINCHUANGBZ.Equals(0))
            {
                SkinTabControlViewInfo vi = ViewInfo as SkinTabControlViewInfo;
                Rectangle r = info.Bounds;

                if (vi != null)
                {
                    Skin skin = vi.Skin;
                    Rectangle controlBoxRect = info.ControlBox;
                    int hgrow = skin.Properties.GetInteger(TabSkinProperties.SelectedHeaderHGrow);

                    int delta = 0;
                    int contentIndent = skin.Properties.GetInteger(GetContentIndent());
                    if ((info.PageState & ObjectState.Selected) == 0)
                    {
                        hgrow = 0;
                        delta = 0;
                    }
                    if (IsSideLocation)
                    {
                        r.Height += hgrow * 2; r.Y -= hgrow;
                        r.Width += delta + skin.Properties.GetInteger(GetHeaderDownGrow(info));
                        if (HeaderLocation == TabHeaderLocation.Left)
                        {
                            delta *= -1;
                            r.X += delta;
                        }
                        else
                        {
                            r.X -= skin.Properties.GetInteger(GetHeaderDownGrow(info));
                        }
                        info.Text = info.OffsetRect(info.Text, delta + contentIndent, 0);
                        info.Image = info.OffsetRect(info.Image, delta + contentIndent, 0);
                        if (!controlBoxRect.IsEmpty) controlBoxRect = info.OffsetRect(controlBoxRect, delta + contentIndent, 0);
                    }
                    else
                    {
                        r.Width += hgrow * 2; r.X -= hgrow;
                        r.Height = 28;

                        if (HeaderLocation == TabHeaderLocation.Top)
                        {
                            delta *= -1;
                            r.Y += delta;
                        }
                        else
                        {
                            r.Y -= skin.Properties.GetInteger(GetHeaderDownGrow(info));
                        }
                        info.Image = info.OffsetRect(info.Image, 0, delta / 2 + contentIndent);
                        if (this.TabControl is MediTabControl mediTabControl)
                        {
                            info.Text = info.OffsetRect(info.Text, mediTabControl.IsAdsSide ? 41 : 3, -4);
                        }

                        if (CanShowCloseButtonForPage(info) || CanShowPinButtonForPage(info))
                        {
                            controlBoxRect = info.OffsetRect(controlBoxRect, 0, delta / 2 + contentIndent - 4);
                        }
                    }
                    if (!controlBoxRect.IsEmpty)
                    {
                        info.ButtonsPanel.ViewInfo.SetDirty();
                        info.ButtonsPanel.ViewInfo.Calc(GraphicsInfo.Cache, controlBoxRect);
                    }
                }

                info.Bounds = r;
            }
            else
            {
                base.UpdatePageBounds(info);
            }
        }

        protected override Size UpdateHeaderBoundsSize(Size size)
        {
            if (!string.IsNullOrWhiteSpace(HISClientHelper.XITONGID) && !HISClientHelper.LINCHUANGBZ.Equals(0))
            {
                if (IsSideLocation)
                    size.Width += 2;
                else
                    size.Height -= 6;
            }
            return size;
        }

        protected override void CalcPageViewInfo(BaseTabRowViewInfo row, BaseTabPageViewInfo info, ref Point topLeft)
        {
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (!string.IsNullOrWhiteSpace(HISClientHelper.XITONGID) && !HISClientHelper.LINCHUANGBZ.Equals(0))
                {
                    if (HeaderLocation == TabHeaderLocation.Bottom
                        || HeaderLocation == TabHeaderLocation.Top)
                    {
                        if (info.Page.TabControl.GetTabPage(0).Equals(info.Page))
                        {
                            if (this.TabControl is MediTabControl control)
                            {
                                topLeft = new Point(topLeft.X + (control).LeftDistance, topLeft.Y);
                            }
                        }
                        else
                        {
                            topLeft = new Point(topLeft.X + ((MediTabControl)TabControl).Interval, topLeft.Y);
                        }
                    }
                    else
                    {
                        topLeft = new Point(topLeft.X, topLeft.Y + 150);
                    }
                }
            }
            base.CalcPageViewInfo(row, info, ref topLeft);
        }
        #endregion
        public class MediSkinTabPainter : SkinTabPainter
        {
            public Color ActiveColor { get; set; }

            public Color DisableColor { get; set; }

            public bool SetHeaderColor { get; set; }

            public MediSkinTabPainter(IXtraTab tabControl)
                : base(tabControl)
            {
            }
            protected override void DrawHeaderPage(TabDrawArgs e, DevExpress.XtraTab.ViewInfo.BaseTabRowViewInfo row, DevExpress.XtraTab.ViewInfo.BaseTabPageViewInfo pInfo)
            {

                if (SetHeaderColor)
                {
                    MediSkinTabHeaderViewInfo header = e.ViewInfo.HeaderInfo as MediSkinTabHeaderViewInfo;
                    SkinTabControlViewInfo vi = e.ViewInfo as SkinTabControlViewInfo;
                    MediSkinTabPageObjectInfo pObjInfo = header.UpdatePageInfo(pInfo);
                    ObjectPainter.DrawObject(e.Cache, vi.PagePainter, pObjInfo);
                    Rectangle bounds = pInfo.Bounds;
                    bounds.Inflate(-1, -1);

                    if (pInfo.IsActiveState)
                    {
                        e.Cache.FillRectangle(new SolidBrush(ActiveColor), bounds);
                    }
                    else
                    {
                        e.Cache.FillRectangle(new SolidBrush(DisableColor), bounds);
                    }
                    DrawHeaderPageImageText(e, pInfo);
                    DrawHeaderFocus(e, pInfo);
                }
                //else
                //{
                //    if (pInfo.IsActiveState)
                //    {
                //        e.Cache.FillRectangle(new SolidBrush(Color.FromArgb(0,115,195)), bounds);
                //    }
                //    else
                //    {
                //        e.Cache.FillRectangle(new SolidBrush(Color.FromArgb(225,225,225)), bounds);
                //    }
                //}


            }
        }

        public class MediSkinTabPageObjectInfo : SkinTabPageObjectInfo
        {
            public MediSkinTabPageObjectInfo(SkinElement element, BaseTabPageViewInfo pInfo) : base(element, pInfo)
            {
                //this.colorCore = System.Drawing.Color.Red;
            }
        }

    }
}