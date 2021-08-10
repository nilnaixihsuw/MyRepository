using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 用户自定义进度条控件
    /// </summary>
    public partial class mediCustomProgressBar : MediUserControl
    {
        public mediCustomProgressBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 上传文件信息
        /// </summary>
        public string UploadCountDes { get { return this.circleProgressControl1.Description; } set { this.circleProgressControl1.Description = value; } }

        /// <summary>
        /// 上传文件标题
        /// </summary>
        public string UpLoadFileInfo { get { return this.circleProgressControl1.Caption; } set { this.circleProgressControl1.Caption = value; } }

        /// <summary>
        ///设置百分数是否可见
        /// </summary>
        public bool IsVisiblePercent { get { return this.circleProgressControl1.IsShowPercentage; } set { this.circleProgressControl1.IsShowPercentage = value; } }

        /// <summary>
        /// 显示标题
        /// </summary>
        public bool ShowCaption { get { return this.circleProgressControl1.ShowCaption; } set { this.circleProgressControl1.ShowCaption = value; } }

        /// <summary>
        /// 显示标题
        /// </summary>
        public bool ShowDescription { get { return this.circleProgressControl1.ShowDescription; } set { this.circleProgressControl1.ShowDescription = value; } }

        /// <summary>
        /// 已上传进度
        /// </summary>
        public string UploadedFilePercentProcess { get { return this.circleProgressControl1.PercentProcess; } set { this.circleProgressControl1.PercentProcess = value; } }

        /// <summary>
        /// 总文件数
        /// </summary>
        public int Totaluploadedcount { get; set; }

        /// <summary>
        /// 上传进度值
        /// </summary>
        //public int Position { get { return this.mediProgressBarControl1.Position; } set { this.mediProgressBarControl1.Position = value; } }

        /// <summary>
        /// 进度最小值
        /// </summary>
        //public int MinValue { get { return this.mediProgressBarControl1.Properties.Minimum; } set { this.mediProgressBarControl1.Properties.Minimum = value; } }

        /// <summary>
        /// 进度最大值
        /// </summary>
        //public int MaxValue { get { return this.mediProgressBarControl1.Properties.Maximum; } set { this.mediProgressBarControl1.Properties.Maximum = value; } }

        /// <summary>
        /// 进度每次增加的步数
        /// </summary>
        //public int Step { get { return this.mediProgressBarControl1.Properties.Step; } set { this.mediProgressBarControl1.Properties.Step = value; } }

        /// <summary>
        /// 进度每次增加的步数
        /// </summary>
        //public bool ShowTitle { get { return this.mediProgressBarControl1.Properties.ShowTitle; } set { this.mediProgressBarControl1.Properties.ShowTitle = value; } }

        /// <summary>
        /// 进度每次增加的步数
        /// </summary>
        //public bool PercentView { get { return this.mediProgressBarControl1.Properties.PercentView; } set { this.mediProgressBarControl1.Properties.PercentView = value; } }

        /// <summary>
        /// 上传文件数/总文件数
        /// </summary>
        //public string UploadCountDes { get { return uploadCountDes.Text; } set { uploadCountDes.Text = value; } }

        public void PerformStep()
        {
        }

        private void mediProgressBarControl1_Paint(object sender, PaintEventArgs e)
        {
            //Draw(e.ClipRectangle, e.Graphics, 10, true, Color.FromArgb(204, 204, 204), Color.FromArgb(204, 204, 204));
            // if (Position>0)
            // {
            //     Rectangle rectangle = new Rectangle();
            //     rectangle.Height = e.ClipRectangle.Height;
            //     if (MaxValue > 0)
            //     {
            //         rectangle.Width = Convert.ToInt32(e.ClipRectangle.Width * (Position * 1.0 / MaxValue));
            //     }
            //     else
            //     {
            //         rectangle.Width = 0;
            //     }

            //     if (rectangle.Height > 0 && rectangle.Width > 0)
            //     {
            //         Draw(rectangle, e.Graphics, 10, true, Color.FromArgb(63, 194, 238), Color.FromArgb(63, 194, 238));
            //     }

            // }

            //Rectangle rectangle = new Rectangle();
            //rectangle.Height = 8;
            //rectangle.Width = 10;
            //Draw(rectangle, e.Graphics, 10, true, Color.FromArgb(63, 194, 238), Color.FromArgb(63, 194, 238));
        }

        private void Draw(Rectangle rectangle, Graphics g, int _radius, bool cusp, Color begin_color, Color end_color)
        {
            //int span = 0;
            ////抗锯齿
            //g.SmoothingMode = SmoothingMode.AntiAlias;
            ////渐变填充
            //LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush(rectangle, begin_color, end_color, LinearGradientMode.Vertical);
            ////画尖角
            //if (cusp)
            //{
            //    span = 0;
            //    PointF p1 = new PointF(rectangle.Width - 12, rectangle.Y + 10);
            //    PointF p2 = new PointF(rectangle.Width - 12, rectangle.Y + 30);
            //    PointF p3 = new PointF(rectangle.Width, rectangle.Y + 20);
            //    PointF[] ptsArray = { p1, p2, p3 };
            //    g.FillPolygon(myLinearGradientBrush, ptsArray);
            //}
            ////填充
            //g.FillPath(myLinearGradientBrush, DrawRoundRect(rectangle.X, rectangle.Y, rectangle.Width - span, rectangle.Height - 1, _radius));
        }

        //public static GraphicsPath DrawRoundRect(int x, int y, int width, int height, int radius)
        //{
        //    ////四边圆角
        //    //GraphicsPath gp = new GraphicsPath();
        //    //gp.AddArc(x, y, radius, radius, 180, 90);
        //    //gp.AddArc(width - radius, y, radius, radius, 270, 90);
        //    //gp.AddArc(width - radius, height - radius, radius, radius, 0, 90);
        //    //gp.AddArc(x, height - radius, radius, radius, 90, 90);
        //    //gp.CloseAllFigures();
        //    //return gp;
        //}

        private void mediProgressBarControl1_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void mediPanelControl1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void mediCustomProgressBar_Load(object sender, EventArgs e)
        {
            //uploadCountDes.Text = string.Format("{0}/{1}", UploadedCount, Totaluploadedcount);
        }

        private void upLoadFileInfo_Click(object sender, EventArgs e)
        {
        }
    }
}