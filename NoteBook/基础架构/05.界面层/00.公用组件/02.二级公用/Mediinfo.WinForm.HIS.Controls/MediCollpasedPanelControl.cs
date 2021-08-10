using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class MediCollpasedPanelControl : SplitContainerControl
    {
        public MediCollpasedPanelControl()
        {
            InitializeComponent();
        }

        public MediCollpasedPanelControl(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public event Action<object, EventArgs> ButtonClick;

        /// <summary>
        /// 控件的宽度
        /// </summary>
        private int FormWidth { get; set; }

        /// <summary>
        /// button显示图片
        /// </summary>
        public Image ButtonImage { get; set; }

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
            set
            {
                collapsed = value;
                ZiDongZD();
            }
        }

        /// <summary>
        /// 初始化一些默认值
        /// </summary>
        public override void BeginInit()
        {
            base.BeginInit();
            FormWidth = this.Size.Width;
        }

        //public override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
        //{
        //    base.OnPaintBackground();
        //}

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    if (this._opacity > 0)
        //    {
        //        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, this.BackColor)),
        //       this.ClientRectangle);
        //    }
        //    if (this._borderWidth > 0)
        //    {
        //        Pen pen = new Pen(this._borderColor, this._borderWidth);
        //        pen.DashStyle = this._borderStyle;
        //        e.Graphics.DrawRectangle(pen, e.ClipRectangle.Left, e.ClipRectangle.Top, this.Width - 1, this.Height - 1);
        //        pen.Dispose();
        //    }
        //}

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
            // this.Panel1.Width = 26;
            this.Panel1.Text = "详细信息";
            // this.Panel1.MaximumSize = new Size(26, 0);
            //this.Panel1.MinimumSize = new Size(26, 0);
            //this.Panel2.Appearance.Image = CreateCollapseImage(true, Color.Blue);
            this.Panel2.BorderStyle = BorderStyles.NoBorder;
            this.Panel1.BorderStyle = BorderStyles.NoBorder;
            //this.Dock = System.Windows.Forms.DockStyle.Right;
            //this.SplitterPosition = this.Width - this.Panel2.Width;
            this.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel1;
            this.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2;
            this.BackColor = Color.Transparent;
            //this.Panel1.BackColor = Color.Transparent;
            this.BorderStyle = BorderStyles.NoBorder;
            //this.SplitterPosition = 26;
            FormWidth = this.Size.Width;
            this.IsSplitterFixed = true;

            //this.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));

            var fuKongJian = this.Parent.DisplayRectangle;//有些panel可能设置了pad，不用this.Parent.Bounds;

            // var ddd = this.Parent.Controls;

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

            this.mediButton1.Appearance.Options.UseTextOptions = true;
            this.mediButton1.Appearance.Image = ButtonImage;
            this.mediButton1.Appearance.BackColor = Color.Transparent;
            this.mediButton1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.mediButton1.ButtonStyle = BorderStyles.NoBorder;
            //  this.mediButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("mediButton1.ImageOptions.Image")));
            this.mediButton1.ImageOptions.Location = ImageLocation.Default;
            this.mediButton1.Location = new Point(0, 0);
            this.mediButton1.LookAndFeel.SkinName = "MediSkinDevExpressStyle";
            this.mediButton1.Name = "mediButton1";
            this.mediButton1.Size = new Size(this.SplitterPosition + 3, 128);
            this.mediButton1.Text = "";
            this.mediButton1.UnboundExpression = null;
            this.mediButton1.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.mediButton1.Dock = DockStyle.Top;
            //this.Panel2.Appearance.BorderColor = Color.FromArgb(0, 155, 195);
            //this.Panel2.Appearance.Options.UseBorderColor = true;
            this.Panel1.Controls.Add(this.mediButton1);
            //this.Width = 26;
            //System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resources));
            //this.Panel1.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("icon-gengduocaozuo.png")));
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvent()
        {
            this.mediButton1.Click -= MediButton_Click;
            this.mediButton1.Click += MediButton_Click;
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
            //if (Collapsed)
            //{
            SolidBrush brush = new SolidBrush(Color.FromArgb(243, 243, 243));
            Rectangle mRectanle = new Rectangle(this.SplitterBounds.X - 1, this.SplitterBounds.Y, this.SplitterBounds.Width + 1, this.SplitterBounds.Height);
            e.Graphics.FillRectangle(brush, mRectanle);
            //}
            //else
            //{
            //    SolidBrush brush = new SolidBrush(Color.FromArgb(0, 155, 195));
            //    Rectangle mRectanle = new Rectangle(this.SplitterBounds.X - 1, this.SplitterBounds.Y, this.SplitterBounds.Width + 1, this.SplitterBounds.Height);
            //    e.Graphics.FillRectangle(brush, mRectanle);
            //}

            ////Pen pen = new Pen(Color.FromArgb(0, 155, 195));
            //Pen pen = new Pen(Color.Red, 1);
            ////e.Graphics.DrawLine(pen, this.Panel2.Location.X, this.Panel2.Location.Y+60, this.Panel2.Bounds.Right, this.Panel2.Bounds.Y+60);
            ////e.Graphics.DrawLine(pen, this.Panel2.Location.X+20, this.Panel2.Location.Y, this.Panel2.Bounds.X+20, this.Panel2.Bounds.Bottom);
            //e.Graphics.DrawLine(pen, this.Panel2.Bounds.Right, this.Panel2.Location.Y, this.Panel2.Bounds.Right, this.Panel2.Bounds.Bottom);
            //e.Graphics.DrawLine(pen, this.Panel2.Bounds.X, this.Panel2.Bounds.Bottom, this.Panel2.Bounds.Right, this.Panel2.Bounds.Bottom);
            ////pen.Dispose();

            //System.Windows.Forms.ControlPaint.DrawBorder(e.Graphics,
            //                this.Panel2.ClientRectangle,
            //                Color.Red,
            //                1,
            //                System.Windows.Forms.ButtonBorderStyle.Solid,
            //                Color.Red,
            //                1,
            //                System.Windows.Forms.ButtonBorderStyle.Solid,
            //                Color.Red,
            //                1,
            //                System.Windows.Forms.ButtonBorderStyle.Solid,
            //                 Color.Red,
            //                1,
            //                System.Windows.Forms.ButtonBorderStyle.Solid);
        }

        /// <summary>
        /// 折叠功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediButton_Click(object sender, EventArgs e)
        {
            //if (this.Width< FormWidth)
            //{
            //    this.Collapsed = false;
            //    this.PanelVisibility = SplitPanelVisibility.Both;
            //    this.Width = FormWidth;
            //    // this.Panel2.Hide();
            //}
            //else if ( this.Width>this.SplitterPosition)
            //{
            //    this.Collapsed = true;
            //    this.PanelVisibility = SplitPanelVisibility.Panel1;
            //    this.Width = this.SplitterPosition + 2;
            //}

            if (Collapsed)
            {
                this.collapsed = false;
                // this.PanelVisibility = SplitPanelVisibility.Both;
                this.Width = FormWidth;
                var fuKongJian = this.Parent.Bounds;
                this.Location = new Point(fuKongJian.Right - this.Width, fuKongJian.Top);
                //this.Location = new Point(ss.Right - 12 - this.Width, ss.Top);
                // this.Panel2.Hide();
            }
            else if (!Collapsed)
            {
                this.collapsed = true;
                //this.PanelVisibility = SplitPanelVisibility.Panel1;
                this.Width = this.SplitterPosition + 3;
                var fuKongJian = this.Parent.Bounds;
                this.Location = new Point(fuKongJian.Right - this.SplitterPosition - 3, fuKongJian.Top);
            }

            ButtonClick?.Invoke(sender, e);
        }

        /// <summary>
        /// 折叠功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZiDongZD()
        {
            if (!collapsed)
            {
                if (this.Width == FormWidth)
                    return;
                this.Width = FormWidth;
                var fuKongJian = this.Parent.Bounds;
                this.Location = new Point(fuKongJian.Right - this.Width, fuKongJian.Top);
            }
            else if (collapsed)
            {
                this.Width = this.SplitterPosition + 3;
                var fuKongJian = this.Parent.Bounds;
                this.Location = new Point(fuKongJian.Right - this.SplitterPosition - 3, fuKongJian.Top);
            }
        }

        //public void Panel1_Click(object sender, EventArgs e)
        //{         
        //    if (this.CollapsePanel == DevExpress.XtraEditors.SplitCollapsePanel.Panel2 && this.Collapsed == true)
        //    {
        //        this.Collapsed = false;
        //        this.Width = FormWidth;
        //        // this.Panel2.Hide();
        //    }
        //    else if (this.CollapsePanel == DevExpress.XtraEditors.SplitCollapsePanel.Panel2 && this.Collapsed == false)
        //    {
        //        this.Collapsed = true;
        //        this.Width = this.SplitterPosition-5;
        //    }
        //}

        public MediButton mediButton1;
    }
}
