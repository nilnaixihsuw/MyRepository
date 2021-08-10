using Mediinfo.WinForm.HIS.Controls.Properties;

using System.Collections.Generic;
using System.Drawing;

namespace Mediinfo.WinForm.HIS.Controls
{
    public class LabelControlCloseBase
    {
        private int refX = 0;
        private int refY = 0;
        private int refWidth = 0;
        private int refHeight = 0;
        private MediPanelControl mediPanel = new MediPanelControl();
        private Dictionary<string, Rectangle> dictry = new Dictionary<string, Rectangle>();
        private string content = string.Empty;
        private Brush brushColor;
        private Dictionary<string, Rectangle> rectangles = new Dictionary<string, Rectangle>();

        /// <summary>
        /// 带关闭按钮的Label
        /// </summary>
        /// <param name="panelControl">label容器</param>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="width">控件宽度</param>
        /// <param name="height">控件高度</param>
        /// <param name="brush">控件颜色,传入方式(Brushes.Red)</param>
        /// <param name="text">控件内容</param>
        /// <param name="rectangles">输出内容</param>
        private void DrawLabelControlClose(MediPanelControl panelControl, int x, int y, int width, int height, Brush brush,string text, ref Dictionary<string, Rectangle> rectangles)
        {
            refX = x;
            refY = y;
            refWidth = width;
            refHeight = height;
            mediPanel = panelControl;
            content = text;
            brushColor = brush;
            mediPanel.Paint += MediPanel_Paint;
            rectangles = dictry;
        }

        private void MediPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // 绘制矩形边框和关闭按钮
            e.Graphics.DrawRectangle(new Pen(brushColor), new Rectangle(refX, refY, refWidth, refHeight));
            MediLabel lab = new MediLabel();
            lab.Text = content.Split('|')[0];
            lab.ToolTip = content.Split('|')[1];
            Font f = lab.Font;
            SizeF z = e.Graphics.MeasureString(lab.Text, f);
            float width = refX + ((refWidth - z.Width) / 2);
            float height = refY + ((refHeight - z.Height) / 2);
            e.Graphics.DrawString(lab.Text, new Font("微软雅黑", 9), Brushes.Red, new PointF(width, height));
            e.Graphics.DrawImage(Resources.close_gm, refX + refWidth - (Resources.close_gm.Width / 2), refY - (Resources.close_gm.Height / 2));
            Point pt = mediPanel.PointToScreen(new Point(refY + refWidth - (Resources.close_gm.Width / 2), refY - (Resources.close_gm.Height / 2)));
            Rectangle rectangle = new Rectangle(pt.X, pt.Y, Resources.close_gm.Width, Resources.close_gm.Height);
            if (!dictry.ContainsKey(lab.ToolTip))
                dictry.Add(lab.ToolTip, rectangle);
        }
    }
}
