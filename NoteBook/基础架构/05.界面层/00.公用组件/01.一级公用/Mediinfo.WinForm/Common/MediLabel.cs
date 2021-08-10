using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 标签控件
    /// </summary>
    [ToolboxItem(true)]
    public partial class MediLabel : DevExpress.XtraEditors.LabelControl, IExpressionInterface
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

        /// <summary>
        /// 只显示底边框属性(下划线)
        /// </summary>
        [Category("UnderLine"), Description("只显示下划线"), DefaultValue(false)]
        public bool UnderLine { get; set; } = false;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        public MediLabel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.Size = new Size(0, 17);
            this.Paint += MediLabel_Paint;
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }
       
        private void MediLabel_Paint(object sender, PaintEventArgs e)
        {
            if (UnderLine&&!this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition)))
            {
                this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                RectangleF rec = e.ClipRectangle;
               // rec.Inflate(-1, -1);
                this.BackColor = Color.Transparent;
                Pen pen = new Pen(Color.FromArgb(0, 0, 0), 6);
                e.Graphics.DrawLine(pen,
                    rec.Left - 1,
                    rec.Height + 3,
                    rec.Width + 1,
                    rec.Height + 3);
            }
        }
    }
}