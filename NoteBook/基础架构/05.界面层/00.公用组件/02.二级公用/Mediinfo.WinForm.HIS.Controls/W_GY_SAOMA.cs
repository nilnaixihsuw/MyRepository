using Mediinfo.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using static Mediinfo.WinForm.HIS.Core.Enum.GYEnum;
namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class W_GY_SAOMA : MediForm
    {
        #region constructor
        public string TiaoMa { get; set; }
        public DuKaFS DuKaFS { get; set; }
        public W_GY_SAOMA()
        {
            m_aeroEnabled = false;

            InitializeComponent();

            this.TopLevel = true;
            //处理双缓冲,减少(窗体)页面闪烁
            SetStyle(ControlStyles.OptimizedDoubleBuffer
                     | ControlStyles.ResizeRedraw
                     | ControlStyles.Selectable
                     | ControlStyles.AllPaintingInWmPaint
                     | ControlStyles.UserPaint
                     | ControlStyles.SupportsTransparentBackColor,
                     true);
            MediParameter.Operation = "None";
        }

        public W_GY_SAOMA(string zfje)
        {
            m_aeroEnabled = false;

            InitializeComponent();

            this.TopLevel = true;
            //处理双缓冲,减少(窗体)页面闪烁
            SetStyle(ControlStyles.OptimizedDoubleBuffer
                     | ControlStyles.ResizeRedraw
                     | ControlStyles.Selectable
                     | ControlStyles.AllPaintingInWmPaint
                     | ControlStyles.UserPaint
                     | ControlStyles.SupportsTransparentBackColor,
                     true);
            MediParameter.Operation = "None";
            mediLabel2.Visible = true;
            mediLabel1.Text = "账户余额不足，还需支付金额：" + zfje + "元!";
        }
        #endregion

        #region extern

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
         );

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        #endregion

        #region fields

        private bool m_aeroEnabled;                     // variables for box shadow
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        public struct MARGINS                           // struct for box shadow
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        private const int WM_NCHITTEST = 0x84;          // variables for dragging the form
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        private const int WM_NCLBUTTONDOWN = 161;
        private const int HT_CAPTION = 2;

        #endregion

        #region methods

        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }

        /// <summary>
        /// 加载关闭按钮
        /// </summary>
        private void InitCloseBox()
        {
            PictureBox closeBox = new PictureBox();
            closeBox.Image = Properties.Resources.close_small;
            closeBox.Location = new Point(this.mediPanelControl1.Width - 22, 10);
            closeBox.SizeMode = PictureBoxSizeMode.Normal;
            closeBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closeBox.Click += CloseBox_Click;
            closeBox.MouseEnter += CloseBox_MouseEnter;
            closeBox.MouseLeave += CloseBox_MouseLeave;
            this.mediPanelControl1.Controls.Add(closeBox);
            closeBox.BringToFront();
        }
 

          
          
         
        #endregion

        #region override

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!SkinCat.Instance.IsDesignMode)
            {
                InitCloseBox();
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();

                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;

                return cp;
            }
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:                        // box shadow
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 1,
                            rightWidth = 1,
                            topHeight = 1
                        };
                        DwmExtendFrameIntoClientArea(this.Handle, ref margins);

                    }
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)     // drag the form
                m.Result = (IntPtr)HTCAPTION;

        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                case Keys.Up:
                    //将焦点移动到上一个控件 
                    this.SelectNextControl(this.ActiveControl, false, true, true, true);
                    break;
                case Keys.Right:
                case Keys.Down:
                    //将焦点移动到下一个控件 
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);
                    break;
            }
            return base.ProcessDialogKey(keyData);
        }

        #endregion

        #region events

        private void mediPanelControl1_MouseDown(object sender, MouseEventArgs e)
        { 
            if (e.Button == MouseButtons.Left)
            {
                switch (e.Clicks)
                {
                    case 1:
                        ReleaseCapture();
                        SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, (int)IntPtr.Zero);
                        break;
                }
            }
        }

        private void W_GY_SAOMA_Shown(object sender, EventArgs e)
        {
            mediTextBox1.ImeMode = ImeMode.Disable;
            mediTextBox1.MediinfoIMEMode = MediInfoImeMode.EN;
            mediTextBox1.Focus();
            if (DuKaFS == DuKaFS.ShuaLian)
            {
                this.Text = "刷脸";
                mediLabel1.Text = "请输入手机号码";
            }
        }

        private void mediTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TiaoMa = mediTextBox1.Text.ToStringEx();
                if (DuKaFS == DuKaFS.ShuaLian && TiaoMa.IsNullOrWhiteSpace())
                {
                    MediMsgBox.FloatMsg("请输入手机号码再回车调用刷脸!");
                    return;
                }

                DialogResult = DialogResult.Yes;
                this.Close();
            }
        }

        private void CloseBox_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CloseBox_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            pictureBox.Image = Properties.Resources.close_small_hover;
        }

        private void CloseBox_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            pictureBox.Image = Properties.Resources.close_small;
        }

        private void mediButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion
    }
}
