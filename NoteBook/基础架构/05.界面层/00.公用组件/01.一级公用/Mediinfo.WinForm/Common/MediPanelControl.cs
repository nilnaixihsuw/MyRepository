using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 功能控件容器
    /// </summary>
    [ToolboxItem(true)]
    public partial class MediPanelControl : DevExpress.XtraEditors.PanelControl, IExpressionInterface
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        [Browsable(true)]
        [Editor(typeof(UnboundExpressionEditor), typeof(UITypeEditor))]
        [Category("UnboundExpression"), Description("控件自定义表达式,返回值为当前控件的值或者属性或者其他控件的值或属性"), DefaultValue("")]
        public string UnboundExpression { get; set; } = string.Empty;

        Rectangle panelRect;

        public MediPanelControl() : base()
        {
            if (!ControlCommonHelper.IsDesignMode())
            {
                panelRect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                this.Paint += MediPanelControl_Paint;
                this.Resize += MediPanelControl_Resize;
            }
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        bool isHiddedTopBorder = false;
        /// <summary>
        /// 隐藏TOP边框(TabControl专用)
        /// </summary>
        [Browsable(true), Description("隐藏TOP边框(TabControl专用)")]
        public bool IsHiddedTopBorder { get { return isHiddedTopBorder; } set { this.Invalidate(); isHiddedTopBorder = value; } }

        private bool isShowBorderColor = false;
        [Browsable(true), Description("是否显示Panel边框颜色"), Category("自定义分组")]
        public bool IsShowBorderColor { get { return isShowBorderColor; } set { this.Invalidate(); isShowBorderColor = value; } }

        Color customborderColor = Color.White;
        [Browsable(true), Description("Panel边框颜色"), Category("自定义分组")]
        public Color CustomBorderColor { get { return customborderColor; } set { this.Invalidate(); customborderColor = value; } }

        private int _BorderSize = 1;

        [Browsable(true), Description("边框粗细"), Category("自定义分组")]
        public int BorderSize { get { return _BorderSize; } set { _BorderSize = value; this.Invalidate(); } }

        private void MediPanelControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (IsHiddedTopBorder)
                e.Graphics.DrawLine(new Pen(Color.White, 3)
          , new PointF(1.1F, 0), new PointF(panelRect.Width - 1.0F, 0));//top
            {

                #region code
                //e.Graphics.DrawLine(new Pen(Color.Red, 3)
                //  , new PointF(1.1F, panelRect.Height - 1.0f), new PointF(panelRect.Width - 1.0F, panelRect.Height - 1.0f));//bottom
                //e.Graphics.DrawLine(new Pen(Color.Red, 3)
                //  , new PointF(1.1F, 0), new PointF(0f, panelRect.Height - 1.0f));
                //e.Graphics.DrawLine(new Pen(Color.Red, 3)//left
                //  , new PointF(panelRect.Width - 1.0F, 0), new PointF(panelRect.Width - 1.0F, panelRect.Height - 1.0f));//right 
                #endregion
            }

            if (IsShowBorderColor)
            {
                Pen pen = new Pen(CustomBorderColor, BorderSize);
                e.Graphics.DrawRectangle(pen, new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width - (int)pen.Width, e.ClipRectangle.Height - (int)pen.Width));
            }
        }

        private void MediPanelControl_Resize(object sender, System.EventArgs e)
        {
            panelRect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
        }

        public bool IsDoubleBuffer { get; set; } = false;

        public MediPanelControl(IContainer container)
        {
            container.Add(this);

            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }
    }
}