using System.ComponentModel;

namespace Mediinfo.WinForm.Common
{
    [ToolboxItem(true)]
    public class MediLayoutControl : DevExpress.XtraLayout.LayoutControl
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        public MediLayoutControl()
        {
            this.LookAndFeel.SkinName = "MediSkinDevExpressStyle";
            this.OptionsFocus.EnableAutoTabOrder = false;
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