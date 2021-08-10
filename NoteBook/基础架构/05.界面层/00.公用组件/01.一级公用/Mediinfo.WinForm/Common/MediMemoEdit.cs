using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ScrollHelpers;
using DevExpress.XtraEditors.ViewInfo;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 多行文本控件(网格中)
    /// </summary>
    [UserRepositoryItem("RegisterMediMemoEdit")]
    public class RepositoryItemMediMemoEdit : RepositoryItemMemoEdit, IExpressionInterface, IInputIMEMode
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        public virtual event EventHandler Assigned;

        [Browsable(true)]
        [Editor(typeof(UnboundExpressionEditor), typeof(UITypeEditor))]
        [Category("UnboundExpression"), Description("控件自定义表达式,返回值为当前控件的值或者属性或者其他控件的值或属性"), DefaultValue("")]
        public string UnboundExpression { get; set; } = string.Empty;

        static RepositoryItemMediMemoEdit()
        {
            RegisterMediMemoEdit();
        }

        public const string CustomEditName = "MediMemoEdit";

        public RepositoryItemMediMemoEdit()
        {
            if (this.ReadOnly || this.Enabled)
                this.Appearance.BackColor = Color.White;
            else if (!this.Enabled)
                this.Appearance.BackColor = Color.LightGray;
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        public override string EditorTypeName => CustomEditName;

        private int fScrollWidth = 12;
        [DefaultValue(12)]
        public int ScrollWidth
        {
            get { return fScrollWidth; }
            set
            {
                if (fScrollWidth != value)
                {
                    fScrollWidth = value;
                    OnPropertiesChanged();
                    if (OwnerEdit != null)
                        (OwnerEdit as MediMemoEdit).UpdateScrollBars();
                }
            }
        }
        
        /// <summary>
        /// 输入法模式
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        public MediInfoImeMode MediinfoIMEMode { get; set; }

        public static void RegisterMediMemoEdit()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediMemoEdit), typeof(RepositoryItemMediMemoEdit), typeof(MediMemoEditViewInfo), new MediMemoEditPainter(), true, img));
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemMediMemoEdit source = item as RepositoryItemMediMemoEdit;
                if (source == null) return;
                fScrollWidth = source.fScrollWidth;
                Assigned(this, new EventArgs());
            }
            finally
            {
                EndUpdate();
            }
        }
    }

    /// <summary>
    /// 多行文本控件
    /// </summary>
    [ToolboxItem(true)]
    public class MediMemoEdit : MemoEdit, IInputIMEMode
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        /// <summary>
        /// 是否全选文本
        /// </summary>
        [Description("是否全选文本"), DefaultValue(true), Browsable(true)]
        public bool FocusAllText { get; set; } = true;

        protected new MediScrollBarEditorsAPIHelper ScrollHelper { get { return scrollHelper as MediScrollBarEditorsAPIHelper; } }

        static MediMemoEdit()
        {
            RepositoryItemMediMemoEdit.RegisterMediMemoEdit();
        }

        public MediMemoEdit()
        {
            if (!ControlCommonHelper.IsDesignMode())
            {
                this.SetEditorsCustomSkin();
                this.GotFocus -= MediSearchControl_GotFocus;
                this.GotFocus += MediSearchControl_GotFocus;
                this.MouseUp -= MediGridLookUpEdit_MouseUp;
                this.MouseUp += MediGridLookUpEdit_MouseUp;
                this.Enter -= MediGridLookUpEdit_Enter;
                this.Enter += MediGridLookUpEdit_Enter;
                this.MouseDown -= MediGridLookUpEdit_MouseDown;
                this.MouseDown += MediGridLookUpEdit_MouseDown;
                (this as DevExpress.XtraEditors.Controls.IAutoHeightControl).HeightChanged += MyMemoEdit_HeightChanged;
                scrollHelper = new MediScrollBarEditorsAPIHelper();
                scrollHelper.Init(MaskBox, this);
                scrollHelper.ScrollMouseLeave += new EventHandler(OnScroll_MouseLeave);
                scrollHelper.LookAndFeel = LookAndFeel;

                Properties.Assigned += new EventHandler(OnPropertiesAssigned);
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        void MyMemoEdit_HeightChanged(object sender, EventArgs e)
        {
            //DevExpress.XtraEditors.ViewInfo.MemoEditViewInfo vi = ViewInfo as DevExpress.XtraEditors.ViewInfo.MemoEditViewInfo;
            //DevExpress.Utils.Drawing.GraphicsCache cache = new DevExpress.Utils.Drawing.GraphicsCache(this.CreateGraphics());
            //int h = (vi as DevExpress.XtraEditors.ViewInfo.IHeightAdaptable).CalcHeight(cache, vi.MaskBoxRect.Width);
            //DevExpress.Utils.Drawing.ObjectInfoArgs args = new DevExpress.Utils.Drawing.ObjectInfoArgs();
            //args.Bounds = new Rectangle(0, 0, vi.ClientRect.Width, h);
            //Rectangle rect = vi.BorderPainter.CalcBoundsByClientRectangle(args);
            //cache.Dispose();
            //this.BeginInvoke(new MethodInvoker(() => {
            //    ScrollBars scrollBars = rect.Height > Height ? ScrollBars.Vertical : ScrollBars.None;
            //    if (this.Properties.ScrollBars != scrollBars)
            //    {
            //        this.Properties.ScrollBars = scrollBars;
            //    }
            //}));
        }

        protected virtual void OnPropertiesAssigned(object sender, EventArgs e)
        {
            UpdateScrollBars();
        }

        protected void OnScroll_MouseLeave(object sender, EventArgs e)
        {
            CheckMouseHere();
        }
        
        public bool HScrollVisible
        {
            get
            {
                return ((Properties.ScrollBars == ScrollBars.Both)
                        || (Properties.ScrollBars == ScrollBars.Horizontal)
                        )
                    && (!Properties.WordWrap);
            }
        }

        public bool VScrollVisible
        {
            get
            {
                return (Properties.ScrollBars == ScrollBars.Both)
                    || (Properties.ScrollBars == ScrollBars.Vertical);
            }
        }

        public virtual void UpdateScrollBars()
        {
            if (ScrollHelper != null)
            {
                ScrollHelper.SetScrollBarsWidth(Properties.ScrollWidth);
                Refresh();
            }
        }

        /// <summary>
        /// /滚动条
        /// </summary>
        /// <returns></returns>
        protected override ScrollBarEditorsAPIHelper CreateScrollHelper()
        {
            return new MediScrollBarEditorsAPIHelper();
        }
        
        private void MediGridLookUpEdit_Enter(object sender, EventArgs e)
        {
            enter = true;
            BeginInvoke(new MethodInvoker(ResetEnterFlag));
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
        }

        private void MediGridLookUpEdit_MouseDown(object sender, MouseEventArgs e)
        {
            needSelect = enter;
        }
        
        private bool enter = false, needSelect = false;
        private void ResetEnterFlag()
        {
            enter = false;
        }
        
        private void MediGridLookUpEdit_MouseUp(object sender, MouseEventArgs e)
        {
            //if (needSelect)
            //{
            //    if (this.Text.Length < 5000)
            //        (sender as TextEdit).SelectAll();
            //}
        }

        private void MediSearchControl_GotFocus(object sender, System.EventArgs e)
        {
            //if (FocusAllText&&this.Text.Length < 5000)
            //    this.SelectAll();
        }

        //private void MediMemoEdit_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        //{
        //    if (this.Enabled || this.ReadOnly)
        //    {
        //        this.BackColor = Color.White;
        //        this.Properties.Appearance.Options.UseBackColor = true;
        //    }
        //    else
        //    {
        //        this.Properties.Appearance.BackColor = SystemColors.ControlLight;
        //        this.Properties.Appearance.Options.UseBackColor = true;
        //    }
        //}

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMediMemoEdit Properties => base.Properties as RepositoryItemMediMemoEdit;

        public override string EditorTypeName => RepositoryItemMediMemoEdit.CustomEditName;

        /// <summary>
        /// 输入法模式
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        public MediInfoImeMode MediinfoIMEMode { get; set; }
    }

    public class MediMemoEditViewInfo : MemoEditViewInfo
    {
        public MediMemoEditViewInfo(RepositoryItem item) : base(item)
        {
        }

        public new RepositoryItemMediMemoEdit Item => base.Item as RepositoryItemMediMemoEdit;

        public new MediMemoEdit OwnerControl => base.OwnerControl as MediMemoEdit;

        protected override void CalcContentRect(Rectangle bounds)
        {
            base.CalcContentRect(bounds);
            if (OwnerControl != null)
            {
                if (OwnerControl.VScrollVisible)
                    fContentRect.Width = ContentRect.Width - Item.ScrollWidth + SystemInformation.VerticalScrollBarWidth + 1;
                if (OwnerControl.HScrollVisible)
                    fContentRect.Height = ContentRect.Height - Item.ScrollWidth + SystemInformation.VerticalScrollBarWidth + 1;
                fMaskBoxRect = ContentRect;
            }
        }
    }

    public class MediMemoEditPainter : MemoEditPainter
    {
        public MediMemoEditPainter()
        {
        }
    }

    public class MediScrollBarEditorsAPIHelper : ScrollBarEditorsAPIHelper
    {
        private int fWidth;

        public MediScrollBarEditorsAPIHelper()
        {
            Type SHelperType = typeof(ScrollBarAPIHelper);

            FieldInfo fHScroll = SHelperType.GetField("hscroll", BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo fVScroll = SHelperType.GetField("vscroll", BindingFlags.Instance | BindingFlags.NonPublic);

            ScrollBarBase hscroll;
            ScrollBarBase vscroll;

            hscroll = new MediMemoEditHScrollBar();
            vscroll = new MediMemoEditVScrollBar();
            hscroll.Scroll += new ScrollEventHandler(OnHScroll_Scroll);
            vscroll.Scroll += new ScrollEventHandler(OnVScroll_Scroll);
            hscroll.MouseLeave += new EventHandler(OnScroll_Leave);
            vscroll.MouseLeave += new EventHandler(OnScroll_Leave);

            vscroll.Visible = true;
            hscroll.Visible = true;

            fHScroll.SetValue(this, hscroll);
            fVScroll.SetValue(this, vscroll);

            fWidth = 12;
        }

        public override void Init(Control control, Control parent)
        {
            base.Init(control, parent);
        }

        public void SetScrollBarsWidth(int Width)
        {
            if (Width >= 0)
            {
                fWidth = Width;
                UpdateScrollBars();
            }
        }

        public void SetScrollBarsButtonWidth(int Width)
        {
            if (Width >= 0)
            {
                (VScroll as MediMemoEditVScrollBar).ViewInfo.SetButtonWidth(Width);
                (HScroll as MediMemoEditHScrollBar).ViewInfo.SetButtonWidth(Width);
            }
        }

        protected override void UpdateDXScrollBar(bool isHorz)
        {
            if (IsLockUpdate) return;
            ScrollBarBase dxScroll = isHorz ? (ScrollBarBase)HScroll : (ScrollBarBase)VScroll;
            if (SourceControl == null) return;
            if (!SourceControl.IsHandleCreated || !dxScroll.Parent.IsHandleCreated)
            {
                dxScroll.Visible = false;
                return;
            }
            BeginUpdate();
            try
            {
                SCROLLBARINFO sbInfo = new SCROLLBARINFO();
                sbInfo.Init();
                SCROLLBARINFO.GetScrollBarInfo(SourceControl.Handle, isHorz ? SCROLLBARINFO.OBJID_HSCROLL : SCROLLBARINFO.OBJID_VSCROLL, ref sbInfo);
                Rectangle scrollBounds = sbInfo.rcScrollBar.ToRectangle();
                if ((SourceControl != null && !SourceControl.Visible) || scrollBounds.IsEmpty || sbInfo.rgstate0 == SCROLLBARINFO.STATE_SYSTEM_INVISIBLE || sbInfo.rgstate0 == SCROLLBARINFO.STATE_SYSTEM_OFFSCREEN)
                {
                    dxScroll.Visible = false;
                    return;
                }
                scrollBounds = dxScroll.Parent.RectangleToClient(scrollBounds);

                if (isHorz)
                {

                    scrollBounds.Height = fWidth;
                }
                else
                {
                    scrollBounds.Width = fWidth;
                }
                dxScroll.Bounds = scrollBounds;
                ScrollArgs currentArgs = new ScrollArgs(dxScroll), args = new ScrollArgs();
                if (sbInfo.rgstate0 == SCROLLBARINFO.STATE_SYSTEM_UNAVAILABLE)
                {
                    args.Maximum = args.Minimum = 0;
                    args.Value = 0;
                    args.Enabled = false;
                }
                else
                {
                    SCROLLINFO sInfo = new SCROLLINFO();
                    sInfo.Init();
                    SCROLLINFO.GetScrollInfo(SourceControl.Handle, isHorz ? SCROLLINFO.SB_HORZ : SCROLLINFO.SB_VERT, ref sInfo);
                    args.Enabled = true;
                    args.Maximum = sInfo.nMax;
                    args.Minimum = sInfo.nMin;
                    args.LargeChange = sInfo.nPage;
                    args.SmallChange = isHorz ? 8 : 1;
                    args.Value = sInfo.nTrackPos;
                }
                dxScroll.Visible = true;
                if (currentArgs.IsEquals(args)) return;
                args.AssignTo(dxScroll);
            }
            finally
            {
                EndUpdate();
            }
        }
    }

    public class MediMemoEditHScrollBar : DevExpress.XtraEditors.HScrollBar
    {
        public MediMemoEditHScrollBar() : base() { }

        protected override ScrollBarPainterBase CreateScrollBarPainter()
        {
            if (LookAndFeel.ActiveStyle == ActiveLookAndFeelStyle.Skin)
                return new MediScrollBarPainter(LookAndFeel.ActiveLookAndFeel);
            else
                return base.CreateScrollBarPainter();
        }

        protected override ScrollBarViewInfo CreateScrollBarViewInfo()
        {
            return new MediMemoEditScrollBarViewInfo(this);
        }

        public new MediMemoEditScrollBarViewInfo ViewInfo => base.ViewInfo as MediMemoEditScrollBarViewInfo;
    }

    public class MediMemoEditScrollBarViewInfo : ScrollBarViewInfo
    {
        int fWidth;

        public MediMemoEditScrollBarViewInfo(IScrollBar SB)
            : base(SB)
        {
            if (ScrollBarType == ScrollBarType.Horizontal)
                fWidth = SystemInformation.HorizontalScrollBarArrowWidth;
            else
                fWidth = SystemInformation.VerticalScrollBarArrowHeight;
        }

        public override int ButtonWidth
        {
            get
            {
                if (fWidth > ScrollBarWidth * 2)
                    return 0;
                if (fWidth > ScrollBarWidth / 2)
                    fWidth = ScrollBarWidth / 2;
                return fWidth;
            }
        }

        public void SetButtonWidth(int Width)
        {
            if (Width >= 0)
            {
                fWidth = Width;
                CalculateCore();
            }
        }
    }
    
    public class MediScrollBarPainter : SkinScrollBarPainter
    {
        public MediScrollBarPainter(ISkinProvider Skin) : base(Skin) { }

        public override int ThumbMinWidth => 8;
    }

    public class MediMemoEditVScrollBar : DevExpress.XtraEditors.VScrollBar
    {
        public MediMemoEditVScrollBar() : base() { }

        protected override ScrollBarPainterBase CreateScrollBarPainter()
        {
            if (LookAndFeel.ActiveStyle == ActiveLookAndFeelStyle.Skin)
                return new MediScrollBarPainter(LookAndFeel.ActiveLookAndFeel);
            else
                return base.CreateScrollBarPainter();
        }

        protected override ScrollBarViewInfo CreateScrollBarViewInfo()
        {
            return new MediMemoEditScrollBarViewInfo(this);
        }

        public new MediMemoEditScrollBarViewInfo ViewInfo => base.ViewInfo as MediMemoEditScrollBarViewInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SCROLLBARINFO
    {
        public const uint
            STATE_SYSTEM_INVISIBLE = 0x00008000,
            STATE_SYSTEM_OFFSCREEN = 0x00010000,
            STATE_SYSTEM_UNAVAILABLE = 0x00000001,
            OBJID_VSCROLL = 0xFFFFFFFB,
            OBJID_HSCROLL = 0xFFFFFFFA;
        const int CCHILDREN_SCROLLBAR = 5;
        public Int32 cbSize;
        public GDIRect rcScrollBar;
        public Int32 dxyLineButton;
        public Int32 xyThumbTop;
        public Int32 xyThumbBottom;
        public Int32 reserved;
        public Int32 rgstate0, rgstate1, rgstate2, rgstate3, rgstate4, rgstate5;
        public void Init()
        {
            this.rcScrollBar = new GDIRect();
            this.cbSize = Marshal.SizeOf(this);
            this.dxyLineButton = this.xyThumbTop = this.xyThumbBottom = this.reserved = 0;
            this.rgstate0 = this.rgstate1 = this.rgstate2 = this.rgstate3 = this.rgstate4 = this.rgstate5 = 0;
        }
        [DllImport("USER32.dll")]
        internal static extern bool GetScrollBarInfo(IntPtr hwnd, uint idObject, ref SCROLLBARINFO psbi);
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SCROLLINFO
    {
        public const int
            SIF_RANGE = 0x0001,
            SIF_PAGE = 0x0002,
            SIF_POS = 0x0004,
            SIF_DISABLENOSCROLL = 0x0008,
            SIF_TRACKPOS = 0x0010,
            SIF_ALL = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS);
        public const int SB_HORZ = 0, SB_VERT = 1;
        public Int32 cbSize, fMask, nMin, nMax, nPage, nPos, nTrackPos;
        public void Init()
        {
            this.cbSize = Marshal.SizeOf(this);
            this.nMin = this.nMax = this.nPage = this.nPos = this.nTrackPos = 0;
            this.fMask = SIF_ALL;
        }
        [DllImport("USER32.dll")]
        internal static extern int SetScrollInfo(IntPtr handle, int fnBar, ref SCROLLINFO scrollInfo, bool redraw);
        [DllImport("USER32.dll")]
        internal static extern bool GetScrollInfo(IntPtr handle, int fnBar, ref SCROLLINFO scrollInfo);
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct GDIRect
    {
        public int left, top, right, bottom;
        public GDIRect(int l, int t, int r, int b)
        {
            left = l; top = t; right = r; bottom = b;
        }
        public GDIRect(Rectangle r)
        {
            left = r.Left; top = r.Top; right = r.Right; bottom = r.Bottom;
        }
        public Rectangle ToRectangle()
        {
            return new Rectangle(left, top, right - left, bottom - top);
        }
    }
}