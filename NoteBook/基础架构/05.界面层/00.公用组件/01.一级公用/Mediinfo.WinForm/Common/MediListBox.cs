using System.ComponentModel;
using System.Drawing.Design;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 列表框控件，显示用户可以选择的项目列表。 可以使用数据源中的项填充。
    /// </summary>
    [ToolboxItem(true)]
    public partial class MediListBox : DevExpress.XtraEditors.ListBoxControl, IExpressionInterface
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

        public MediListBox()
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