using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Mediinfo.WinForm.HIS.Controls.Properties;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class MediCollpasedPanelControlEx : SplitContainerControl
    {
        public MediCollpasedPanelControlEx()
        {
            InitializeComponent();
        }

        public MediCollpasedPanelControlEx(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// button1事件
        /// </summary>
        public event Action<object, EventArgs> Button1Click;
        /// <summary>
        /// button2事件
        /// </summary>
        public event Action<object, EventArgs> Button2Click;
        /// <summary>
        /// button3事件
        /// </summary>
        public event Action<object, EventArgs> Button3Click;
        /// <summary>
        /// button4事件
        /// </summary>
        public event Action<object, EventArgs> Button4Click;


        /// <summary>
        /// 控件的宽度
        /// </summary>
        private int FormWidth { get; set; }

        /// <summary>
        /// button1显示图片
        /// </summary>
        public Image ButtonImage1 { get; set; }

        /// <summary>
        /// button2显示图片
        /// </summary>
        public Image ButtonImage2 { get; set; }

        /// <summary>
        /// button3显示图片
        /// </summary>
        public Image ButtonImage3 { get; set; }

        /// <summary>
        /// button4显示图片
        /// </summary>
        public Image ButtonImage4 { get; set; }

        /// <summary>
        /// 折叠按钮显示图片
        /// </summary>
        public Image ButtonImageZheDie { get; set; }

        private Size button1Size = new Size(30, 120);

        /// <summary>
        /// button1的大小
        /// </summary>
        [Description("button1的大小")]
        public Size Button1Size
        {
            get { return button1Size; }
            set { button1Size = value; }
        }

        private Size button2Size = new Size(30, 120);

        /// <summary>
        /// button2的大小
        /// </summary>
        [Description("button2的大小")]
        public Size Button2Size
        {
            get { return button2Size; }
            set { button2Size = value; }
        }

        private Size button3Size = new Size(30, 120);

        /// <summary>
        /// button3的大小
        /// </summary>
        [Description("button3的大小")]
        public Size Button3Size
        {
            get { return button3Size; }
            set { button3Size = value; }
        }

        private Size button4Size = new Size(30, 120);

        /// <summary>
        /// button4的大小
        /// </summary>
        [Description("button4的大小")]
        public Size Button4Size
        {
            get { return button4Size; }
            set { button4Size = value; }
        }

        /// <summary>
        /// 重写属性，默认为折叠
        /// </summary>
        private bool collapsed = true;

        /// <summary>
        /// 是否折叠
        /// </summary>
        [DefaultValue(true), Description("是否折叠")]
        public override bool Collapsed
        {
            get { return collapsed; }
            set { collapsed = value; }
        }

        /// <summary>
        /// 初始化一些默认值
        /// </summary>
        public override void BeginInit()
        {
            base.BeginInit();
            FormWidth = this.Size.Width;
        }

        /// <summary>
        /// 结束初始化
        /// </summary>
        public override void EndInit()
        {
            if (!SkinCat.Instance.IsDesignMode)
            {
                Init();
                RegisterEvent();
            }
            base.EndInit();

        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void Init()
        {
            this.Panel1.Text = "详细信息"; ;
            this.Panel2.BorderStyle = BorderStyles.NoBorder;
            this.Panel1.BorderStyle = BorderStyles.NoBorder;
            this.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel1;
            this.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2;
            this.BackColor = Color.Transparent;
            this.BorderStyle = BorderStyles.NoBorder;
            FormWidth = this.Size.Width;
            this.IsSplitterFixed = true;

            var fuKongJian = this.Parent.DisplayRectangle;  // 有些panel可能设置了pad，不用this.Parent.Bounds;

            // 开始是否折叠导致控件宽度和位置不一样
            if (Collapsed)
            {
                this.Width = this.SplitterPosition + 3;
            }
            else
            {
                this.Width = FormWidth;
            }

            this.Location = new Point(fuKongJian.Right - this.Width, fuKongJian.Top);

            // 将滚动条漏出来
            //this.Location = new Point(ss.Right - 12-this.SplitterPosition, ss.Top);

            this.mediButton1 = new MediButton();
            this.mediButton2 = new MediButton();
            this.mediButton3 = new MediButton();
            this.mediButton4 = new MediButton();
            this.medizheDie = new MediButton();

            this.medizheDie.Appearance.Options.UseTextOptions = true;
            //this.medizheDie.Appearance.Image = Resources.arrow004;
            this.medizheDie.Appearance.BackColor = Color.Transparent;
            this.medizheDie.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.medizheDie.ButtonStyle = BorderStyles.NoBorder;
            this.medizheDie.ImageOptions.Image = Resources.arrow004;
            this.medizheDie.ImageOptions.Location = ImageLocation.Default;
            this.medizheDie.Location = new Point(0, 0);
            this.medizheDie.LookAndFeel.SkinName = "MediSkinDevExpressStyle";
            this.medizheDie.Name = "medizheDie";
            this.medizheDie.Size = new Size(24, 24);
            this.medizheDie.Text = "";
            this.medizheDie.UnboundExpression = null;
            this.medizheDie.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.Panel1.Controls.Add(this.medizheDie);

            this.mediButton1.Appearance.Options.UseTextOptions = true;
            //this.mediButton1.Appearance.Image = ButtonImage1;
            this.mediButton1.Appearance.BackColor = Color.Transparent;
            this.mediButton1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediButton1.ButtonStyle = BorderStyles.NoBorder;
            this.mediButton1.ImageOptions.Image = ButtonImage1;
            this.mediButton1.ImageOptions.Location = ImageLocation.Default;
            this.mediButton1.Location = new Point(0, 24);
            this.mediButton1.LookAndFeel.SkinName = "MediSkinDevExpressStyle";
            this.mediButton1.Name = "mediButton1";
            this.mediButton1.Size = button1Size;// new System.Drawing.Size(this.SplitterPosition + 3, 38);
            this.mediButton1.Text = "";
            this.mediButton1.UnboundExpression = null;
            this.mediButton1.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.True;
            this.Panel1.Controls.Add(this.mediButton1);

            this.mediButton2.Appearance.Options.UseTextOptions = true;
            //this.mediButton2.Appearance.Image = ButtonImage2;
            this.mediButton2.Appearance.BackColor = Color.Transparent;
            this.mediButton2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediButton2.ButtonStyle = BorderStyles.NoBorder;
            this.mediButton2.ImageOptions.Image = ButtonImage2;
            this.mediButton2.ImageOptions.Location = ImageLocation.Default;
            this.mediButton2.Location = new Point(0, button1Size.Height + 24);
            this.mediButton2.LookAndFeel.SkinName = "MediSkinDevExpressStyle";
            this.mediButton2.Name = "mediButton2";
            this.mediButton2.Size = Button2Size;
            this.mediButton2.Text = "";
            this.mediButton2.UnboundExpression = null;
            this.mediButton2.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.True;
            //this.mediButton2.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Controls.Add(this.mediButton2);

            this.mediButton3.Appearance.Options.UseTextOptions = true;
            // this.mediButton3.Appearance.Image = ButtonImage3;
            this.mediButton3.Appearance.BackColor = Color.Transparent;
            this.mediButton3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediButton3.ButtonStyle = BorderStyles.NoBorder;
            this.mediButton3.ImageOptions.Image = ButtonImage3;
            this.mediButton3.ImageOptions.Location = ImageLocation.Default;
            this.mediButton3.Location = new Point(0, button1Size.Height + button2Size.Height + 24);
            this.mediButton3.LookAndFeel.SkinName = "MediSkinDevExpressStyle";
            this.mediButton3.Name = "mediButton3";
            this.mediButton3.Size = button3Size;
            this.mediButton3.Text = "";
            this.mediButton3.UnboundExpression = null;
            this.mediButton3.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.True;
            //this.mediButton3.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Controls.Add(this.mediButton3);


            this.mediButton4.Appearance.Options.UseTextOptions = true;
            // this.mediButton4.Appearance.Image = ButtonImage4;
            this.mediButton4.Appearance.BackColor = Color.Transparent;
            this.mediButton4.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediButton4.ButtonStyle = BorderStyles.NoBorder;
            this.mediButton4.ImageOptions.Image = ButtonImage4;
            this.mediButton4.ImageOptions.Location = ImageLocation.Default;
            this.mediButton4.Location = new Point(0, button1Size.Height + button2Size.Height + button3Size.Height + 24);
            this.mediButton4.LookAndFeel.SkinName = "MediSkinDevExpressStyle";
            this.mediButton4.Name = "mediButton4";
            this.mediButton4.Size = button4Size;
            this.mediButton4.Text = "";
            this.mediButton4.UnboundExpression = null;
            this.mediButton4.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.True;
            //this.mediButton4.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Controls.Add(this.mediButton4);
            this.mediButton4.Visible = false;

        }

        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvent()
        {
            this.mediButton1.Click -= MediButton1_Click;
            this.mediButton1.Click += MediButton1_Click;
            this.mediButton2.Click -= MediButton2_Click;
            this.mediButton2.Click += MediButton2_Click;
            this.mediButton3.Click -= MediButton3_Click;
            this.mediButton3.Click += MediButton3_Click;
            this.mediButton4.Click -= MediButton4_Click;
            this.mediButton4.Click += MediButton4_Click;
            this.medizheDie.Click -= zheDie_Click;
            this.medizheDie.Click += zheDie_Click;
        }

        /// <summary>
        /// 重写事件，使点击中间分隔条时不触发折叠事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            // base.OnMouseDown(e);
        }

        /// <summary>
        /// 重绘分隔条
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            SolidBrush brush = new SolidBrush(Color.FromArgb(243, 243, 243));
            Rectangle mRectanle = new Rectangle(this.SplitterBounds.X - 1, this.SplitterBounds.Y, this.SplitterBounds.Width + 1, this.SplitterBounds.Height);
            e.Graphics.FillRectangle(brush, mRectanle);
        }

        /// <summary>
        /// 折叠功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediButton1_Click(object sender, EventArgs e)
        {
            Button1Click?.Invoke(sender, e);
        }

        private void MediButton2_Click(object sender, EventArgs e)
        {
            Button2Click?.Invoke(sender, e);
        }

        private void MediButton3_Click(object sender, EventArgs e)
        {
            Button3Click?.Invoke(sender, e);
        }

        private void MediButton4_Click(object sender, EventArgs e)
        {
            Button4Click?.Invoke(sender, e);
        }

        private void zheDie_Click(object sender, EventArgs e)
        {
            collapse();
        }

        private void collapse()
        {
            if (Collapsed)
            {
                this.Collapsed = false;
                this.Width = FormWidth;
                var fuKongJian = this.Parent.Bounds;
                this.Location = new Point(fuKongJian.Right - this.Width, fuKongJian.Top);
                this.medizheDie.ImageOptions.Image = Resources.arrow0001;
            }
            else if (!Collapsed)
            {
                this.Collapsed = true;
                this.Width = this.SplitterPosition + 3;
                var fuKongJian = this.Parent.Bounds;
                this.Location = new Point(fuKongJian.Right - this.SplitterPosition - 3, fuKongJian.Top);
                this.medizheDie.ImageOptions.Image = Resources.arrow004;
            }
        }

        public MediButton mediButton1;
        public MediButton mediButton2;
        public MediButton mediButton3;
        public MediButton mediButton4;
        public MediButton medizheDie;
    }
}
