using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 无边框提示弹窗
    /// </summary>
    public partial class MediPromptDialog : MediForm
    {
        #region constructor

        /// <summary>
        /// 无边框提示弹窗
        /// </summary>
        public MediPromptDialog()
        {
            m_aeroEnabled = false;

            InitializeComponent();

            this.TopLevel = true;
            // 处理双缓冲,减少(窗体)页面闪烁
            SetStyle(ControlStyles.OptimizedDoubleBuffer
                     | ControlStyles.ResizeRedraw
                     | ControlStyles.Selectable
                     | ControlStyles.AllPaintingInWmPaint
                     | ControlStyles.UserPaint
                     | ControlStyles.SupportsTransparentBackColor,
                     true);
            MediParameter.Operation = "None";
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

        /// <summary>
        /// 当前页面上显示的按钮
        /// </summary>
        private static MediButtonShow buttonShow = MediButtonShow.YesNo;

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

        /// <summary>
        /// 获取默认返回状态
        /// </summary>
        /// <param name="buttonShow">显示按钮类型</param>
        private DialogResult GetDefaultStatus(MediButtonShow buttonShow)
        {
            DialogResult result = new DialogResult();
            switch (buttonShow)
            {
                case MediButtonShow.YesNo:
                    result = DialogResult.No;
                    break;
                case MediButtonShow.YesInformation:
                    result = DialogResult.No;
                    break;
                case MediButtonShow.YesNoHelp:
                    result = DialogResult.No;
                    break;
                case MediButtonShow.RetryCancel:
                    result = DialogResult.Cancel;
                    break;
                case MediButtonShow.YesNoCancel:
                    result = DialogResult.Cancel;
                    break;
                case MediButtonShow.AbortRetryIgnore:
                    result = DialogResult.Ignore;
                    break;
                case MediButtonShow.OKCancel:
                    result = DialogResult.Cancel;
                    break;
                case MediButtonShow.SaveNotSaveCancel:
                    result = DialogResult.Cancel;
                    break;
                default:
                    result = DialogResult.OK;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 显示具有指定文本的消息框。
        /// </summary>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <param name="subText">要在消息框中显示的副文本</param>
        /// <returns></returns>
        public DialogResult Show(string text, string subText = null)
        {
            MediPromptDialog dialog = new MediPromptDialog();
            dialog.mediLabel1.Text = "\r\n" + text;
            dialog.mediLabel2.Text = "\r\n" + subText;
            dialog.mediButton2.Visible = false;
            dialog.mediButton3.Visible = false;
            dialog.mediPanelControl3.Visible = false;
            dialog.mediPanelControl4.Visible = false;
            Image image = ImageIco(MediImagesIco.Information.ToString());
            if (image != null)
                dialog.mediPictureEdit1.Image = image;
            else
                dialog.mediPictureEdit1.Visible = false;
            dialog.mediButton1.Text = "确定";
            MediSelfAdaption(ref dialog, text, subText);
            dialog.ShowDialog();
            dialog.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 在指定对象的前面显示具有指定文本的消息框
        /// </summary>
        /// <param name="window">将拥有模式对话框的 System.Windows.Forms.IWin32Window 的一个实现</param>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <param name="subText">要在消息框中显示的副文本</param>
        /// <returns></returns>
        public DialogResult Show(IWin32Window window, string text, string subText = null)
        {
            MediPromptDialog dialog = new MediPromptDialog();
            dialog.mediLabel1.Text = "\r\n" + text;
            dialog.mediLabel2.Text = "\r\n" + subText;
            dialog.mediButton2.Visible = false;
            dialog.mediButton3.Visible = false;
            dialog.mediPanelControl3.Visible = false;
            dialog.mediPanelControl4.Visible = false;
            Image image = ImageIco(MediImagesIco.Information.ToString());
            if (image != null)
                dialog.mediPictureEdit1.Image = image;
            else
                dialog.mediPictureEdit1.Visible = false;
            dialog.mediButton1.Text = "确定";
            MediSelfAdaption(ref dialog, text, subText);
            dialog.ShowDialog(window);
            dialog.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 显示具有指定文本、标题和按钮的消息框
        /// </summary>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <param name="btn">System.Windows.Forms.MessageBoxButtons 值之一，可指定在消息框中显示哪些按钮</param>
        /// <param name="subText">要在消息框中显示的副文本</param>
        /// <returns></returns>
        public DialogResult Show(string text, MediButtonShow btn, string subText = null)
        {
            MediPromptDialog dialog = new MediPromptDialog();
            dialog.mediLabel1.Text = "\r\n" + text;
            dialog.mediLabel2.Text = "\r\n" + subText;
            MediButtonType(btn.ToString(), ref dialog);
            Image image = ImageIco(MediImagesIco.Information.ToString());
            if (image != null)
                dialog.mediPictureEdit1.Image = image;
            else
                dialog.mediPictureEdit1.Visible = false;
            MediSelfAdaption(ref dialog, text, subText);
            dialog.ShowDialog();
            dialog.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 显示具有指定文本、标题和按钮的消息框
        /// </summary>
        /// <param name="window">将拥有模式对话框的 System.Windows.Forms.IWin32Window 的一个实现</param>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <param name="btn">System.Windows.Forms.MessageBoxButtons 值之一，可指定在消息框中显示哪些按钮</param>
        /// <param name="subText">要在消息框中显示的副文本</param>
        /// <returns></returns>
        public DialogResult Show(IWin32Window window, string text, MediButtonShow btn, string subText = null)
        {
            MediPromptDialog dialog = new MediPromptDialog();
            dialog.mediLabel1.Text = "\r\n" + text;
            dialog.mediLabel2.Text = "\r\n" + subText;
            MediButtonType(btn.ToString(), ref dialog);
            Image image = ImageIco(MediImagesIco.Information.ToString());
            if (image != null)
                dialog.mediPictureEdit1.Image = image;
            else
                dialog.mediPictureEdit1.Visible = false;
            MediSelfAdaption(ref dialog, text, subText);
            dialog.ShowDialog(window);
            dialog.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 显示具有指定文本、标题、按钮和图标的消息框
        /// </summary>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <param name="caption">要在消息框的标题栏中显示的文本</param>
        /// <param name="btn">MediButtonShow 值之一，可指定在消息框中显示哪些按钮。</param>
        /// <param name="icon">MediImagesIco 值之一，它指定在消息框中显示哪个图标。</param>
        /// <param name="subText">要在消息框中显示的副文本</param>
        /// <returns></returns>
        public DialogResult Show(string text, MediButtonShow btn, MediImagesIco icon, string subText = null)
        {
            MediPromptDialog dialog = new MediPromptDialog();
            dialog.mediLabel1.Text = "\r\n" + text;
            dialog.mediLabel2.Text = "\r\n" + subText;
            MediButtonType(btn.ToString(), ref dialog);
            Image image = ImageIco(icon.ToString());
            if (image != null)
                dialog.mediPictureEdit1.Image = ImageIco(icon.ToString());
            else
                dialog.mediPictureEdit1.Visible = false;
            MediSelfAdaption(ref dialog, text, subText);
            dialog.ShowDialog();
            dialog.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 显示具有指定文本、标题、按钮和图标的消息框
        /// </summary>
        /// <param name="window">将拥有模式对话框的 System.Windows.Forms.IWin32Window 的一个实现</param>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <param name="btn">MediButtonShow 值之一，可指定在消息框中显示哪些按钮。</param>
        /// <param name="icon">MediImagesIco 值之一，它指定在消息框中显示哪个图标。</param>
        /// <param name="subText">要在消息框中显示的副文本</param>
        /// <returns></returns>
        public DialogResult Show(IWin32Window window, string text, MediButtonShow btn, MediImagesIco icon, string subText = null)
        {
            MediPromptDialog dialog = new MediPromptDialog();
            dialog.mediLabel1.Text = "\r\n" + text;
            dialog.mediLabel2.Text = "\r\n" + subText;
            MediButtonType(btn.ToString(), ref dialog);
            Image image = ImageIco(icon.ToString());
            if (image != null)
                dialog.mediPictureEdit1.Image = image;
            else
                dialog.mediPictureEdit1.Visible = false;
            MediSelfAdaption(ref dialog, text, subText);
            dialog.ShowDialog(window);
            dialog.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 添加一个自定义按钮文字的提示框
        /// </summary>
        /// <param name="text"></param>
        /// <param name="btn"></param>
        /// <param name="icon"></param>
        /// <param name="subText"></param>
        /// <returns></returns>
        public DialogResult Show(string text, string btn, MediImagesIco icon, string subText = null)
        {
            MediPromptDialog dialog = new MediPromptDialog();
            dialog.mediLabel1.Text = "\r\n" + text;
            dialog.mediLabel2.Text = "\r\n" + subText;
            MediButtonType(btn.ToString(), ref dialog);
            Image image = ImageIco(icon.ToString());
            if (image != null)
                dialog.mediPictureEdit1.Image = ImageIco(icon.ToString());
            else
                dialog.mediPictureEdit1.Visible = false;
            MediSelfAdaption(ref dialog, text, subText);
            dialog.ShowDialog();
            dialog.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 文字换行计算方式
        /// </summary>
        /// <param name="dialog"></param>
        /// <param name="text"></param>
        public void MediSelfAdaption(ref MediPromptDialog dialog, string text, string subText)
        {
            string content = dialog.mediLabel1.Text.TrimStart('\r').TrimStart('\n');
            if (content.Length > 15)
                dialog.mediLabel1.Text = content.Substring(0, 15) + "...";
            else
                dialog.mediLabel1.Text = dialog.mediLabel1.Text.TrimStart('\r').TrimStart('\n');

            if (String.IsNullOrWhiteSpace(subText))
                dialog.mediLabel2.Visible = false;
            else
            {
                string txt = dialog.mediLabel2.Text.TrimStart('\r').TrimStart('\n');
                if (txt.Length > 20)
                    dialog.mediLabel2.Text = txt.Substring(0, 20) + "...";
                else
                    dialog.mediLabel2.Text = dialog.mediLabel2.Text.TrimStart('\r').TrimStart('\n');
            }
        }

        /// <summary>
        /// 按钮返回结果
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public DialogResult MediResultButton(string txt)
        {
            DialogResult result = new DialogResult();
            switch (txt)
            {
                case "确定":
                    result = DialogResult.OK;
                    break;
                case "取消":
                    result = DialogResult.Cancel;
                    break;
                case "是(&Y)":
                    result = DialogResult.Yes;
                    break;
                case "否(&N)":
                    result = DialogResult.No;
                    break;
                case "中止":
                    result = DialogResult.Abort;
                    break;
                case "忽略":
                    result = DialogResult.Retry;
                    break;
                case "重试":
                    result = DialogResult.Ignore;
                    break;
                case "保存(&S)":
                    result = DialogResult.Yes;
                    break;
                case "不保存(&N)":
                    result = DialogResult.No;
                    break;
                default:
                    result = GetDefaultStatus(buttonShow);
                    break;
            }
            if (result.ToString() == "None")
            {
                switch (MediParameter.ButType)
                {
                    case "Button1":
                        result = DialogResult.Yes;
                        break;
                    case "Button2":
                        result = DialogResult.No;
                        break;
                    case "Button3":
                        result = DialogResult.Cancel;
                        break;
                }
            }
            return result;
        }

        /// <summary>
        /// 设置按钮文字
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="box"></param>
        public void MediButtonType(string btn, ref MediPromptDialog box)
        {
            try
            {
                switch (btn)
                {
                    case "YesNo":
                        box.mediButton1.Text = "是(&Y)";
                        box.mediButton2.Text = "否(&N)";
                        box.mediButton3.Visible = false;
                        box.mediPanelControl3.Visible = false;
                        buttonShow = MediButtonShow.YesNo;
                        break;
                    case "YesInformation":
                        box.mediButton1.Text = "确定";
                        box.mediButton2.Text = "详细信息>>";
                        box.mediButton3.Visible = false;
                        box.mediPanelControl3.Visible = false;
                        buttonShow = MediButtonShow.YesInformation;
                        break;
                    case "YesNoHelp":
                        box.mediButton1.Text = "确定";
                        box.mediButton3.Text = "帮助";
                        box.mediButton2.Text = "取消";
                        buttonShow = MediButtonShow.YesNoHelp;
                        break;
                    case "RetryCancel":
                        box.mediButton1.Text = "重试";
                        box.mediButton2.Text = "取消";
                        box.mediButton3.Visible = false;
                        box.mediPanelControl3.Visible = false;
                        buttonShow = MediButtonShow.RetryCancel;
                        break;
                    case "YesNoCancel":
                        box.mediButton1.Text = "否(&N)";
                        box.mediButton2.Text = "取消";
                        box.mediButton3.Text = "是(&Y)";
                        buttonShow = MediButtonShow.YesNoCancel;
                        break;
                    case "AbortRetryIgnore":
                        box.mediButton1.Text = "重试";
                        box.mediButton2.Text = "忽略";
                        box.mediButton3.Text = "中止";
                        buttonShow = MediButtonShow.AbortRetryIgnore;
                        break;
                    case "OKCancel":
                        box.mediButton2.Text = "取消";
                        box.mediButton3.Visible = false;
                        box.mediPanelControl3.Visible = false;
                        box.mediButton1.Text = "确定";
                        buttonShow = MediButtonShow.OKCancel;
                        break;
                    case "SaveNotSaveCancel":
                        box.mediButton2.Text = "取消";
                        box.mediButton3.Text = "保存(&S)";
                        box.mediButton1.Text = "不保存(&N)";
                        buttonShow = MediButtonShow.SaveNotSaveCancel;
                        break;
                    default:
                        box.mediButton2.Visible = box.mediButton3.Visible = false;
                        box.mediPanelControl3.Visible = false;
                        box.mediPanelControl4.Visible = false;
                        box.mediButton1.Text = btn;
                        if (btn.Length == 4)
                        {
                            mediPanelControl5.Size = new Size(85, 46);
                            mediButton1.Size = new Size(80, 26);
                            box.mediButton1.Size = new Size(80, 26);

                            mediPanelControl4.Location = new Point(mediPanelControl4.Location.X - 15, mediPanelControl4.Location.Y);
                            mediPanelControl3.Location = new Point(mediPanelControl3.Location.X - 15, mediPanelControl3.Location.Y);
                        }
                        break;
                }
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// 设置图片类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public Image ImageIco(string str)
        {
            Image image = null;
            switch (str)
            {
                case "Hand":
                case "Stop":
                case "Error":
                    image = Properties.Resources.cross;
                    break;
                case "Question":
                    break;
                case "Exclamation":
                    image = Properties.Resources.tick;
                    break;
                case "Warning":
                    image = Properties.Resources.tip;
                    break;
                case "Asterisk":
                case "Information":
                    image = Properties.Resources.tixingicon;
                    break;
            }
            return image;
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
            if (MediParameter.bluess)
                MediParameter.ButType = "Button2";
            else
                MediParameter.ButType = "Button1";
            MediParameter.Operation = this.mediButton1.Text;

            this.Close();
        }

        private void mediButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (MediParameter.bluess)
                    MediParameter.ButType = "Button3";
                else
                    MediParameter.ButType = "Button2";
                MediParameter.Operation = this.mediButton2.Text;
                if (mediButton2.Text == "取消")
                    this.Close();
                else if (this.mediButton2.Text == "忽略")
                    this.Close();
                else if (mediButton2.Text == "否(&N)")
                    this.Close();
                else
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            { }
        }

        private void mediButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (MediParameter.bluess)
                    MediParameter.ButType = "Button1";
                else
                    MediParameter.ButType = "Button3";
                MediParameter.Operation = this.mediButton3.Text;
                if (this.mediButton3.Text == "是(&Y)")
                    this.Close();
                else if (this.mediButton3.Text == "中止")
                    this.Close();
                if (this.mediButton3.Text == "确定")
                    this.Close();
                else if (this.mediButton3.Text == "帮助")
                    System.Diagnostics.Process.Start(MediParameter.HelpPath);
                else
                    this.Close();
            }
            catch (Exception ex)
            { }

        }

        #endregion
    }
}
