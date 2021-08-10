using DevExpress.XtraBars;
using DevExpress.XtraBars.Controls;
using DevExpress.XtraBars.Forms;

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Mediinfo.WinForm
{
    /// <summary>
    /// 弹出菜单，由BarManager或RibbonControl管理。
    /// </summary>
    [ToolboxItem(true)]
    public class MediPopupMenu : PopupMenu
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        public MediPopupMenu()
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

        protected override SubMenuControlForm CreateForm(BarManager manager, PopupMenuBarControl pc)
        {
            return new MediSubMenuControlForm(manager, pc, FormBehavior.SubMenu);
        }
    }

    public class MediSubMenuControlForm : SubMenuControlForm
    {
        public MediSubMenuControlForm(BarManager manager, Control containedControl, FormBehavior behavior) : base(manager, containedControl, behavior)
        {

        }

        protected override Size CalcSizeByWidth(int width, int maxFormHeight)
        {
            Size size = Behavior.CalcSizeByWidth(width, maxFormHeight);
            return new Size(size.Width - 22, size.Height);
        }
    }
}