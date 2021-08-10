using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 标题容器控件
    /// </summary>
    [ToolboxItem(true)]
    public partial class MediTitleBar : DevExpress.XtraEditors.PanelControl, IExpressionInterface
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        [Browsable(true)]
        [Editor(typeof(UnboundExpressionEditor), typeof(UITypeEditor))]
        [Category("UnboundExpression"), Description("控件自定义表达式,返回值为当前控件的值或者属性或者其他控件的值或属性"), DefaultValue("")]
        public string UnboundExpression { get; set; }

        private void InitializeComponent()
        {
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(99, 20);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "labelControl1";
            // 
            // MediTitleBar
            // 
            this.Controls.Add(this.labelControl1);
            this.Name = "MediTitleControl";
            this.Size = new System.Drawing.Size(200, 25);
            this.SizeChanged += new System.EventHandler(this.MediTitleBar_SizeChanged);
            this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.MediTitleBar_ControlAdded);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        
        private DevExpress.XtraEditors.LabelControl labelControl1;

        public MediTitleBar()
        {
            InitializeComponent();

            this.labelControl1.SendToBack();
            //this.labelControl1.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DoubleClick += MediTitleBar_DoubleClick;
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        private void MediTitleBar_DoubleClick(object sender, EventArgs e)
        {
            //Assembly asm = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "/Mediinfo.WinForm.HIS.Controls.dll");
            //Type typeMediFormWithQX = asm.GetType("Mediinfo.WinForm.HIS.Controls.MediFormWithQX");

            //MethodInfo methodInfo = Convert.ChangeType(this.FindForm(), typeMediFormWithQX).GetType().GetMethod("DoubleButtonFrm");
            //methodInfo.Invoke(,);
            //(MediFormWithQX)this.FindForm
        }

        /// <summary>
        /// 标签文本内容
        /// </summary>
        [Browsable(true)]
        [Category("自定义")]
        public string LabelText
        {
            get { return this.labelControl1.Text; }
            set { this.labelControl1.Text = value; }
        }

        /// <summary>
        /// 标签是否可见
        /// </summary>
        [Browsable(true)]
        [Category("自定义")]
        public bool LabelVisible
        {
            get { return this.labelControl1.Visible; }
            set { this.labelControl1.Visible = value; }
        }

        /// <summary>
        /// 是否禁用标签
        /// </summary>
        [Browsable(true)]
        [Category("自定义")]
        public bool LabelEnabled
        {
            get { return this.labelControl1.Enabled; }
            set { this.labelControl1.Enabled = value; }
        }

        /// <summary>
        /// 标签字体大小
        /// </summary>
        [Browsable(true)]
        [Category("自定义")]
        public Font LabelFont
        {
            get { return this.labelControl1.Font; }
            set { this.labelControl1.Font = value; }
        }

        /// <summary>
        /// 标签大小
        /// </summary>
        [Browsable(true)]
        [Category("自定义")]
        public Size LabelSize
        {
            get { return this.labelControl1.Size; }
            set { this.labelControl1.Size = value; }
        }

        /// <summary>
        /// 标签内边间距
        /// </summary>
        [Browsable(true)]
        [Category("自定义")]
        public Padding LabePadding
        {
            get { return this.labelControl1.Padding; }
            set { this.labelControl1.Padding = value; }
        }

        /// <summary>
        /// 标签字体颜色
        /// </summary>
        [Browsable(true)]
        [Category("自定义")]
        public Color LabeForeColor
        {
            get { return this.labelControl1.ForeColor; }
            set { this.labelControl1.ForeColor = value; }
        }

        /// <summary>
        /// 标签背景颜色
        /// </summary>
        [Browsable(true)]
        [Category("自定义")]
        public Color LabeBackColor
        {
            get { return this.labelControl1.BackColor; }
            set { this.labelControl1.BackColor = value; }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            Point pt = new Point(this.labelControl1.Location.X, this.labelControl1.Location.Y);
            pt.Y = (this.Height - this.labelControl1.Height) / 2 > 0 ? (this.Height - this.labelControl1.Height) / 2 : 0;

            this.labelControl1.Location = pt;
        }

        private void MediTitleBar_SizeChanged(object sender, EventArgs e)
        {
            //this.labelControl1.Top = ((this.Height - this.labelControl1.Height) / 2)-0;
            // this.labelControl1.Height = this.Height;
            //this.labelControl1.Width = this.Width;
        }

        private void MediTitleBar_ControlAdded(object sender, ControlEventArgs e)
        {
            e.Control.BringToFront();
        }
    }
}