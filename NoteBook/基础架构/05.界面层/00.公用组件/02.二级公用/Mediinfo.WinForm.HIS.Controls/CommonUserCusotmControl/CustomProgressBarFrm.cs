using DevExpress.XtraWaitForm;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 自定义加载等候窗体
    /// </summary>
    public partial class CustomProgressBarFrm : WaitForm
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public CustomProgressBarFrm()
        {
            InitializeComponent();

            this.ShowInTaskbar = false;

            SetFormLocation();
            this.Refresh();
        }

        #region Constructed Function

        /// <summary>
        /// 父窗体
        /// </summary>
        public Control ParentForm { get; set; }

        /// <summary>
        /// 上传进度窗体
        /// </summary>
        /// <param name="parentForm">父窗体</param>
        /// <param name="maxProcess">最大进度</param>
        public CustomProgressBarFrm(Control parentForm, int maxProcess = 100)
            : this()
        {
            //mediCustomProgressBar1.MinValue = MinProcess;
            //mediCustomProgressBar1.MaxValue = MaxProcess;
            //mediCustomProgressBar1.Step = 1;
            //mediCustomProgressBar1.PerformStep();
            ParentForm = parentForm;
            this.ShowInTaskbar = false;


            SetFormLocation();
            this.Refresh();
        }

        /// <summary>
        /// 设置窗体位置
        /// </summary>
        private void SetFormLocation()
        {
            Rectangle ScreenArea = Screen.GetWorkingArea(this);

            if (ParentForm != null)
            {
                int frameHeight = ParentForm.Location.Y;
                int frameWidth = ParentForm.Location.X;
                int currentformheight = this.Size.Height;
                int currentformwidth = this.Size.Width;
                int newformx = frameWidth + ParentForm.Width / 2 - currentformwidth / 2;
                int newformy = frameHeight + ParentForm.Height / 2 - currentformheight / 2;
                this.SetDesktopLocation(newformx, newformy);
            }
            else
            {
                this.StartPosition = FormStartPosition.CenterScreen;
            }
        }

        #endregion Constructed Function

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        #region Methods

        /// <summary>
        ///设置上传内容
        /// </summary>
        /// <param name="uploadedFileCount"></param>
        /// <param name="allUploadFileCount"></param>
        public void SetContent(int uploadedFileCount, int allUploadFileCount)
        {
            //this.maxProcess = allUploadFileCount;
            //mediCustomProgressBar1.UploadCountDes = string.Format("{0}/{1}", uploadedFileCount, allUploadFileCount);

            mediCustomProgressBar1.PerformStep();

            this.Refresh();
        }

        /// <summary>
        /// 设置百分数是否可见
        /// </summary>
        public void SetPercentVisible(bool visible)
        {
            mediCustomProgressBar1.IsVisiblePercent = visible;
        }

        /// <summary>
        /// 设置上传内容
        /// </summary>
        public void SetContent(string description)
        {
            mediCustomProgressBar1.UploadCountDes = description;

            mediCustomProgressBar1.PerformStep();
        }

        /// <summary>
        /// 设置上传进度
        /// </summary>
        public void SetPercentProcess(string process)
        {
            this.Invoke((MethodInvoker)delegate
            {
                mediCustomProgressBar1.UploadedFilePercentProcess = process;
                this.Refresh();
            });
        }

        #endregion Methods
        
        private void CustomProgressBarFrm_Paint(object sender, PaintEventArgs e)
        {
            SetFormLocation();
        }
        
        #region Overrides

        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
            mediCustomProgressBar1.UpLoadFileInfo = caption;

            this.Refresh();
        }
        public override void SetDescription(string description)
        {
            base.SetDescription(description);
            mediCustomProgressBar1.UploadCountDes = description;
        }

        /// <summary>
        /// 进度命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="arg"></param>
        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
            if (cmd.ToString() == WaitFormCommand.progressCount.ToString())
            {
                mediCustomProgressBar1.UploadedFilePercentProcess = arg.ToString();
                this.Refresh();
            }
            if (cmd.ToString() == WaitFormCommand.IsVisiblePercent.ToString())
            {
                SetPercentVisible(Convert.ToBoolean(arg));
            }
            if (cmd.ToString() == WaitFormCommand.progressContent.ToString())
            {
                SetContent(arg.ToString());
            }
        }

        #endregion

        /// <summary>
        /// 命令枚举
        /// </summary>
        public enum WaitFormCommand
        {
            /// <summary>
            /// 进度数
            /// </summary>
            progressCount = 0,
            /// <summary>
            /// 百分比是否可见
            /// </summary>
            IsVisiblePercent = 1,
            /// <summary>
            /// 进度类容设置
            /// </summary>
            progressContent = 2
        }
    }
}