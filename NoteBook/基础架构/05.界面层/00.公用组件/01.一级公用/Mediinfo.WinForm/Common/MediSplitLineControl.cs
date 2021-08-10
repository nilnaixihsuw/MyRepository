using System.ComponentModel;

namespace Mediinfo.WinForm.Common
{
    [ToolboxItem(true)]
    public class MediSplitLineControl : DevExpress.XtraEditors.SeparatorControl
    {
        public MediSplitLineControl()
        {
            this.BackColor = System.Drawing.Color.White;
            this.LineColor = System.Drawing.Color.Gray;
            this.LineStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.LineThickness = 2;
            this.Padding = new System.Windows.Forms.Padding(0, 9, 0, 9);
        }
    }
}