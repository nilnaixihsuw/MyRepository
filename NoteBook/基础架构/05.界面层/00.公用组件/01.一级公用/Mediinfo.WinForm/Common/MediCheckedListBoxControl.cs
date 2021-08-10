using DevExpress.XtraEditors;

using System.ComponentModel;
using System.Drawing.Design;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 复选框列表控件(主要用于多条件选择过滤)
    /// </summary>
    [ToolboxItem(true)]
    public partial class MediCheckedListBoxControl : CheckedListBoxControl, IExpressionInterface
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        /// <summary>
        /// 表达式属性
        /// </summary>
        [Browsable(true)]
        [Editor(typeof(UnboundExpressionEditor), typeof(UITypeEditor))]
        [Category("UnboundExpression"), Description("控件自定义表达式,返回值为当前控件的值或者属性或者其他控件的值或属性"), DefaultValue("")]
        public string UnboundExpression { get; set; }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public MediCheckedListBoxControl()
        {
            this.CheckOnClick = true;
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        public MediCheckedListBoxControl(IContainer container)
        {
            container.Add(this);

            this.CheckOnClick = true;
        }
    }
}