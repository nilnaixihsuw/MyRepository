using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 表示 Windows 列表视图控件，该控件显示可用四种不同视图之一显示的项集合。
    /// </summary>
    [ToolboxItem(true)]
    public partial class MediListView : ListView, IExpressionInterface
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        public MediListView()
        {
            InitializeComponent();
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        [Browsable(true)]
        [Editor(typeof(UnboundExpressionEditor), typeof(UITypeEditor))]
        [Category("UnboundExpression"), Description("控件自定义表达式,返回值为当前控件的值或者属性或者其他控件的值或属性"), DefaultValue("")]
        public string UnboundExpression { get; set; } = string.Empty;
    }
}