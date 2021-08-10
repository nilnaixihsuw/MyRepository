using DevExpress.LookAndFeel;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Mediinfo.WinForm
{
    [ToolboxItem(true)]
    public class MediToggleSwitch : DevExpress.XtraEditors.ToggleSwitch
    {
        static MediToggleSwitch()
        {
            RepositoryItemMediToggleSwitch.RegisterMediToggleSwitch();
        }

        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        public MediToggleSwitch()
        {
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMediToggleSwitch Properties => base.Properties as RepositoryItemMediToggleSwitch;

        public override string EditorTypeName => RepositoryItemMediToggleSwitch.CustomEditName;

        private ToggleButtonState toggleState = ToggleButtonState.OFF;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ToggleButtonState ToggleState
        {
            get
            {
                return toggleState;
            }
            set
            {
                if (toggleState != value)
                {
                    RaiseButtonStateChanged();
                    toggleState = value;
                    this.Invalidate();
                    this.Refresh();
                }
            }
        }

        public delegate void ToggleButtonStateChanged(object sender, ToggleButtonStateEventArgs e);

        public class ToggleButtonStateEventArgs : EventArgs
        {
            public ToggleButtonStateEventArgs(ToggleButtonState ButtonState)
            {
            }
        }

        public event ToggleButtonStateChanged ButtonStateChanged;

        protected void RaiseButtonStateChanged()
        {
            if (this.ButtonStateChanged != null)
                ButtonStateChanged(this, new ToggleButtonStateEventArgs(this.ToggleState));
        }

        #region Event Handlers

        public Rectangle ContentRectangle { get; set; }

        [DefaultValue(2)]
        public int Ipadx { get; set; }

        [DefaultValue(false)]
        public bool IsMouseMoved { get; set; }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            sliderPoint = downpos;

            if (downpos.X <= ContentRectangle.Width / 4)
            {
                Ipadx = 2;
                this.ToggleState = ToggleButtonState.OFF;
            }
            else
            {
                Ipadx = Ipadx = ContentRectangle.Right - (ContentRectangle.Height - 3);
                this.ToggleState = ToggleButtonState.ON;
            }
            this.Refresh();
        }

        private bool isMouseDown = false; private Point downpos = Point.Empty;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (!this.DesignMode)
            {
                isMouseDown = true;
                downpos = e.Location;
            }
            this.Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Space)
            {
                if (this.ToggleState == ToggleButtonState.ON)
                    this.ToggleState = ToggleButtonState.OFF;
                else
                    this.ToggleState = ToggleButtonState.ON;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left && !this.DesignMode)
            {
                sliderPoint = e.Location;
                IsMouseMoved = true;
                Ipadx = e.X;
                if (Ipadx <= 2)
                {
                    Ipadx = 2;
                    this.ToggleState = ToggleButtonState.OFF;
                }

                if (Ipadx >= ContentRectangle.Right - (ContentRectangle.Height - 3))
                {
                    Ipadx = ContentRectangle.Right - (ContentRectangle.Height - 3);
                    this.ToggleState = ToggleButtonState.ON;
                }
                Refresh();
            }
        }

        private Point sliderPoint = Point.Empty;

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (!this.DesignMode)
            {
                this.Invalidate();
                if (IsMouseMoved)
                {
                    Ipadx = e.Location.X;
                    if (Ipadx < ContentRectangle.Width / 2)
                    {
                        Ipadx = 2;
                        this.ToggleState = ToggleButtonState.OFF;
                    }
                    else
                    {
                        Ipadx = ContentRectangle.Right - (ContentRectangle.Height - 3);
                        this.ToggleState = ToggleButtonState.ON;
                    }
                    Invalidate();
                    Update();
                }

                IsMouseMoved = false;
                isMouseDown = false;
            }
        }

        #endregion Event Handlers
    }

    public class MediToggleSwitchViewInfo : ToggleSwitchViewInfo
    {
        public MediToggleSwitchViewInfo(RepositoryItem item) : base(item)
        {
        }

        protected override BaseCheckObjectPainter CreateCheckPainter()
        {
            if (IsPrinting) return TogglePainterHelper.GetPainter(ActiveLookAndFeelStyle.Flat);
            return TogglePainterHelper.GetPainter(LookAndFeel);
        }
    }

    public class MediToggleSwitchPainter : ToggleSwitchPainter
    {
        public MediToggleSwitchPainter()
        {
        }

        protected override void DrawContent(ControlGraphicsInfoArgs info)
        {
            base.DrawContent(info);
            DrawCheck(info);
        }

        private void MediToggleSwitchPainter_MouseDown(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void Draw(ControlGraphicsInfoArgs info)
        {
            base.Draw(info);
        }

        protected override void DrawCheck(ControlGraphicsInfoArgs info)
        {
            BaseCheckEditViewInfo vi = info.ViewInfo as BaseCheckEditViewInfo;
            vi.CheckInfo.Cache = info.Cache;
            vi.CheckInfo.IsDrawOnGlass = info.IsDrawOnGlass;
            Rectangle r = vi.CheckInfo.CaptionRect;
            try
            {
                Rectangle cr = new Rectangle(0, 0, info.ViewInfo.Bounds.Width, info.ViewInfo.Bounds.Height);
                ((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).ContentRectangle = cr;
                DrawToggleButtonStyle(info);
            }
            finally
            {
                vi.CheckInfo.Cache = null;
            }
        }

        #region variables

        private Point[] pts2 = new Point[4];
        private Rectangle controlBounds = Rectangle.Empty;
        private bool justRefresh = false;

        #endregion variables

        #region Style

        private void DrawToggleButtonStyle(ControlGraphicsInfoArgs info)
        {
            ((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).BackColor = Color.LightGray;
            info.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            info.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Rectangle r = new Rectangle(0, 0, info.ViewInfo.Bounds.Width, info.ViewInfo.Bounds.Height);
            ((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).ContentRectangle = r;
            if (!((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).IsMouseMoved)
            {
                if ((((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).ToggleState == ToggleButtonState.ON))
                    ((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).Ipadx = ((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).ContentRectangle.Right - (((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).ContentRectangle.Height - 3);
                else
                    ((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).Ipadx = 2;
            }
            Rectangle rect = new Rectangle(((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).Ipadx, r.Y, r.Height - 3, r.Height);
            Rectangle r2 = new Rectangle(info.ViewInfo.Bounds.Width / 6 - 10, info.ViewInfo.Bounds.Height / 2, (info.ViewInfo.Bounds.Width / 6 - 10) + (rect.X + rect.Width / 2), info.ViewInfo.Bounds.Height / 2);

            GraphicsPath gp = new GraphicsPath();
            int d = info.ViewInfo.Bounds.Height;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            ((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).Region = new Region(gp);

            if (((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).Ipadx < ((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).ContentRectangle.Width / 2)
                isSelected = false;
            else if (((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).Ipadx == ((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).ContentRectangle.Right - (((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).ContentRectangle.Height - 3) || ((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).Ipadx > ((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).ContentRectangle.Width / 2)
                isSelected = true;

            Rectangle ar1 = new Rectangle(r.X, r.Y, r.X + rect.Right, r.Height);

            LinearGradientBrush br3 = new LinearGradientBrush(ar1, Color.FromArgb(255, 96, 174, 241), Color.FromArgb(255, 96, 174, 241), LinearGradientMode.Vertical);

            LinearGradientBrush br = new LinearGradientBrush(ar1, Color.FromArgb(0, 127, 234), Color.FromArgb(96, 174, 241), LinearGradientMode.Vertical);
            if (isSelected)
            {
                info.Graphics.FillRectangle(br, ar1);
            }
            if (((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).Properties.ShowText)
            {
                if (isSelected)
                    info.Graphics.DrawString(((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).Properties.OnText, ((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).Font, Brushes.White, new PointF(r.Width / 4, ((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).ContentRectangle.Y + (((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).ContentRectangle.Height / 4)));
                else
                    info.Graphics.DrawString(((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).Properties.OffText, ((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).Font, new SolidBrush(Color.FromArgb(123, 123, 123)), new PointF(r.Width / 2, ((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).ContentRectangle.Y + (((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).ContentRectangle.Height / 4)));
            }

            #region Center Ellipse

            Color c = ((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).Parent != null ? ((MediToggleSwitch)((IToggleAnimationInfo)info.ViewInfo).Owner).Parent.BackColor : Color.White;

            LinearGradientBrush br2 = new LinearGradientBrush(rect, Color.White, Color.White, LinearGradientMode.Vertical);
            if (isSelected)
            {
                Rectangle rectangle = new Rectangle(rect.X - 2, rect.Y + 3, rect.Width - 3, rect.Height - 6);
                info.Graphics.FillEllipse(br2, rectangle);
            }
            else
            {
                Rectangle rectangle = new Rectangle(rect.X + 3, rect.Y + 3, rect.Width - 3, rect.Height - 6);
                info.Graphics.FillEllipse(br2, rectangle);
            }
            info.Graphics.DrawPath(new Pen(c, 2f), gp);

            info.Graphics.ResetClip();

            #endregion Center Ellipse
        }

        #endregion Style

        private bool isSelected = false;

        private bool dblclick = false;
        private int padx = 0; private bool switchrec = false;

        #region properties

        private string activeText = "ON";
        private int slidingAngle = 5;

        #endregion properties
    }

    public enum ToggleButtonState
    {
        ON,
        OFF
    }

    [UserRepositoryItem("RegisterMediToggleSwitch")]
    public class RepositoryItemMediToggleSwitch : RepositoryItemToggleSwitch
    {
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        static RepositoryItemMediToggleSwitch()
        {
            RegisterMediToggleSwitch();
        }

        public const string CustomEditName = "MediToggleSwitch";

        public RepositoryItemMediToggleSwitch()
        {
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

        public static void RegisterMediToggleSwitch()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediToggleSwitch), typeof(RepositoryItemMediToggleSwitch), typeof(MediToggleSwitchViewInfo), new MediToggleSwitchPainter(), true, img));
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemMediToggleSwitch source = item as RepositoryItemMediToggleSwitch;
                if (source == null) return;
            }
            finally
            {
                EndUpdate();
            }
        }
    }
}