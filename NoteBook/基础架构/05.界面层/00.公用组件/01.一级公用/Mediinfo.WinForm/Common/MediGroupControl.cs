using System.ComponentModel;

namespace Mediinfo.WinForm.Common
{
    [ToolboxItem(true)]
    public partial class MediGroupControl : DevExpress.XtraEditors.GroupControl
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        public MediGroupControl()
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