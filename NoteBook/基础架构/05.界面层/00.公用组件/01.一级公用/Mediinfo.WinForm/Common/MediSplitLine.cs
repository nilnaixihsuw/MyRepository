using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Mediinfo.WinForm.Common
{
    /// <summary>
    /// 渐变线控件
    /// </summary>
    [ToolboxItem(true)]
    public class MediSplitLine : Control
    {
        #region 构造函数

        public MediSplitLine()
        {
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            this.BackColor = Color.FromArgb(212, 212, 212);
            this.Height = 5;
            this.Width = 500;
        }

        #endregion

        #region 变量

        private Color backColor2 = Color.White;
        private LinearGradientMode gradientMode = LinearGradientMode.Vertical;

        #endregion

        #region 属性

        [Browsable(true)]
        public Color BackColor2
        {
            get
            {
                return this.backColor2;
            }
            set
            {
                if (this.backColor2 != value)
                {
                    this.backColor2 = value;
                    base.Invalidate();
                }
            }
        }

        #endregion

        #region 重写

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.BackColor != Color.Empty && this.BackColor2 != Color.Empty && this.BackColor != this.BackColor2)
            {
                using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(0, 0, base.Width, base.Height), this.BackColor, this.BackColor2, this.gradientMode))
                {
                    e.Graphics.FillRectangle(linearGradientBrush, new Rectangle(0, 0, base.Width, base.Height));
                }
            }

            base.OnPaint(e);
        }

        #endregion
    }
}
