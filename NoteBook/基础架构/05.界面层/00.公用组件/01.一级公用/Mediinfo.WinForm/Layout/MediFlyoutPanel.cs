using System.ComponentModel;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 
    /// </summary>
    [ToolboxItem(true)]
    public class MediFlyoutPanel : DevExpress.Utils.FlyoutPanel
    {
        /// <summary>
        /// 
        /// </summary>
        public MediFlyoutPanel()
        {
            
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [ToolboxItem(false)]
    public class MediFlyoutPanelControl : DevExpress.Utils.FlyoutPanelControl
    {
        /// <summary>
        /// 
        /// </summary>
        public MediFlyoutPanelControl() : this(null)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flyoutPanel"></param>
        public MediFlyoutPanelControl(MediFlyoutPanel flyoutPanel)
        {
            this.FlyoutPanel = flyoutPanel;
        }
    }
}
