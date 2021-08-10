using DevExpress.XtraEditors;

using System.ComponentModel;
using System.Drawing.Design;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 该控件由两个由分割器分隔的面板组成，可由最终用户拖动以调整面板大小。
    /// </summary>
    [ToolboxItem(true)]
    public partial class MediSplitContainerControl : SplitContainerControl, IExpressionInterface
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

        public MediSplitContainerControl()
        {
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "";
            this.Panel1.Size = new System.Drawing.Size(100, 100);
            this.Panel1.TabIndex = 0;
            this.Panel2.Location = new System.Drawing.Point(105, 0);
            this.Panel2.Name = "";
            this.Panel2.Size = new System.Drawing.Size(95, 100);
            this.Panel2.TabIndex = 1;
            this.SizeChanged += new System.EventHandler(this.MediSplitContainerControl_SizeChanged);
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }
        public bool IsSplitLineCenter { get; set; } = false;

        public MediSplitContainerControl(IContainer container)
        {
            container.Add(this);

            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "";
            this.Panel1.Size = new System.Drawing.Size(100, 100);
            this.Panel1.TabIndex = 0;
            this.Panel2.Location = new System.Drawing.Point(105, 0);
            this.Panel2.Name = "";
            this.Panel2.Size = new System.Drawing.Size(95, 100);
            this.Panel2.TabIndex = 1;
            this.SizeChanged += new System.EventHandler(this.MediSplitContainerControl_SizeChanged);
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }
        
        private void MediSplitContainerControl_SizeChanged(object sender, System.EventArgs e)
        {
            if (IsSplitLineCenter)
            {
                if (this.Horizontal)
                {
                    this.SplitterPosition = this.Width / 2;
                }
                else
                {
                    this.SplitterPosition = this.Height / 2;
                }
            }
        }
    }
}