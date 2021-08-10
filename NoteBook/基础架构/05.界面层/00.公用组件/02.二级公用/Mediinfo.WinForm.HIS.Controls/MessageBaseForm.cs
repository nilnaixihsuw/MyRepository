using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;
using System.Threading;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class MessageBaseForm : MediForm
    {

       
        public MessageBaseForm()
        {
            InitializeComponent();
          
        }

        private System.Windows.Forms.Timer closeTimer;
        private MediForm parentForm;
        private static ParentType ParentType;

        /// <summary>
        /// 默认窗体显示时间（无任何操作）
        /// </summary>
        private int displayFormTime = 3000;
        /// <summary>
        /// 显示窗体时间
        /// </summary>
        [Browsable(true),DefaultValue(3000)]
        public int DisplayFormTime { get { return displayFormTime; } set
            {

                closeTimer.Interval = value;
                displayFormTime = value;
            }
        }

        private MessageWindowLocation xiaoXiLocation = MessageWindowLocation.RightBottom;
        /// <summary>
        /// 消息弹出窗位置
        /// </summary>
        [Browsable(true), DefaultValue(0),Description("消息弹出窗位置")]
        public MessageWindowLocation XiaoXiLocation { get { return xiaoXiLocation; }set { xiaoXiLocation = value; } }

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="parentWindow">父窗体</param>
        /// <param name="messageForm">自定义消息窗体</param>
        /// <param name="caption">标题名称</param>
        public static void MessageShow(MediForm parentWindow, MessageBaseForm messageForm,string caption)
        {
            if (messageForm != null)
            {
                ParentType = ParentType.Form;
                MessageBaseForm messageform = messageForm;
                messageform.parentForm = parentWindow;
                messageform.Name = caption;

                messageform.Show();
            }
           
           
        }
        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="messageForm">自定义消息窗体</param>
        /// <param name="caption">标题名称</param>
        public static void MessageShow( MessageBaseForm messageForm, string caption)
        {
            if (messageForm != null)
            {
                ParentType = ParentType.Screen;
                MessageBaseForm messageform = messageForm;
                messageform.Name = caption;

                messageform.Show();
            }
        }
        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="messageForm">自定义消息窗体</param>
        /// <param name="caption">标题名称</param>
        public static void MessageShowDialog(MessageBaseForm messageForm, string caption)
        {
            if (messageForm != null)
            {
                ParentType = ParentType.Screen;
                MessageBaseForm messageform = messageForm;
                messageform.Name = caption;

                messageform.ShowDialog();
                messageform.Dispose();
            }
        }
        private void InitializeComponent()
        {
            this.closeTimer = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // closeTimer
            // 
            this.closeTimer.Interval = 3000;
            this.closeTimer.Tick += new System.EventHandler(this.closeTimer_Tick);
            // 
            // MessageFormBase
            // 
            this.Appearance.Options.UseBackColor = true;
            this.ClientSize = new System.Drawing.Size(426, 247);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.LookAndFeel.SkinName = "mediskindevexpressstyle";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageFormBase";
            this.Load += new System.EventHandler(this.MessageFormBase_Load);
            this.Shown += new System.EventHandler(this.MessageFormBase_Shown);
            this.ResumeLayout(false);

        }
       
      

        private void closeTimer_Tick(object sender, EventArgs e)
        {
			 if (ControlCommonHelper.IsDesignMode())
                return;
            this.Dispose();
        }

        private void MessageFormBase_Shown(object sender, EventArgs e)
        {
			 if (ControlCommonHelper.IsDesignMode())
                return;
            closeTimer.Enabled = true;
        }

        private void MessageFormBase_Load(object sender, EventArgs e)
        {
            if (ControlCommonHelper.IsDesignMode())
                return;
            if (XiaoXiLocation == MessageWindowLocation.RightBottom)
            {
                if (ParentType== ParentType.Form)
                {
                    this.Location = new Point(parentForm.Location.X + parentForm.Width - this.Width, parentForm.Location.Y + parentForm.Height - this.Height);
                }
                else if (ParentType == ParentType.Screen)
                {
                    this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Location.X+ Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Location.Y+ Screen.PrimaryScreen.WorkingArea.Height - this.Height);
                }
               
               
            }
            else if(XiaoXiLocation == MessageWindowLocation.RightTop)
            {

                if (ParentType == ParentType.Form)
                {
                    this.Location = new Point(parentForm.Location.X + parentForm.Width - this.Width, parentForm.Location.Y);
                }
                else if (ParentType == ParentType.Screen)
                {
                    this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Location.X + Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Location.Y);
                }
               
            }
            else if (XiaoXiLocation == MessageWindowLocation.LeftTop)
            {

                if (ParentType == ParentType.Form)
                {
                    this.Location = new Point(parentForm.Location.X, parentForm.Location.Y);
                }
                else if (ParentType == ParentType.Screen)
                {
                    this.Location = Screen.PrimaryScreen.WorkingArea.Location;
                }
               
            }
            else if (XiaoXiLocation == MessageWindowLocation.LeftBottom)
            {

                if (ParentType == ParentType.Form)
                {
                    this.Location = new Point(parentForm.Location.X, parentForm.Location.Y + parentForm.Height - this.Height);
                }
                else if (ParentType == ParentType.Screen)
                {
                    this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Location.X, Screen.PrimaryScreen.WorkingArea.Location.Y + Screen.PrimaryScreen.WorkingArea.Height - this.Height);
                }
              
            }else if (XiaoXiLocation == MessageWindowLocation.Center)
            {
                if (ParentType == ParentType.Form)
                {
                    this.Location = new Point((parentForm.Width-this.Width)/2, (parentForm.Height + this.Height)/2);
                }
                else if (ParentType == ParentType.Screen)
                {
                    this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width-this.Width)/2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height)/2);
                }
            }


        }
    }


    public enum ParentType
    {
        Screen = 0,
        Form = 1
    }

    public enum MessageWindowLocation
    {
        /// <summary>
        /// 右下角
        /// </summary>
        RightBottom = 0,
        /// <summary>
        /// 右上角
        /// </summary>
        RightTop = 1,
        /// <summary>
        /// 左下角
        /// </summary>
        LeftBottom = 2,
        /// <summary>
        /// 左上角
        /// </summary>
        LeftTop = 3,
        /// <summary>
        /// 中间位置
        /// </summary>
        Center = 4

    }
}