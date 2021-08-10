using System.ComponentModel;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 加载等候进度条（dev自带扩展）
    /// </summary>
    [ToolboxItem(true)]
    public partial class MediProgressPanel : DevExpress.XtraWaitForm.ProgressPanel
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public MediProgressPanel()
        {
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
        }
    }
}