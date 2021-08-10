using System.ComponentModel;

namespace Mediinfo.WinForm.Common
{
    /// <summary>
    /// 进度条
    /// </summary>
    [ToolboxItem(true)]
    public class MediProgressBarControl : DevExpress.XtraEditors.ProgressBarControl
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        public MediProgressBarControl()
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
    }
}