using Mediinfo.Enterprise;
using Mediinfo.HIS.Core;
using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 消息弹出框类
    /// </summary>
    public partial class MediMsgBox : MediDialog
    {
        #region constructor

        /// <summary>
        /// 构造方法
        /// </summary>        
        public MediMsgBox()
        {
            InitializeComponent();
            this.TopLevel = true;
            //TopWindow = this.Window;---基类窗口取值有问题

            this.panelControl1.Visible = false;
            this.KeyPreview = true;
            this.mediLabel1.Font = new Font("微软雅黑", 10, FontStyle.Regular);
            this.mediTextBox1.Font = new Font("微软雅黑", 10, FontStyle.Regular);

            // 初始化高度
            this.FindForm().Size = new Size(this.FindForm().Width, this.FindForm().Height - this.mediTextBox1.Height);
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
            MediParameter.ButType = "None";

            // 设置弹出窗口类型
            this.DialogResultWindowsType = "Common";
        }

        #endregion

        #region fields

        /// <summary>
        /// 当前页面上显示的按钮
        /// </summary>
        private static MediButtonShow buttonShow = MediButtonShow.YesNo;

        #endregion

        #region properties

        /// <summary>
        /// 弹出窗口类型，主要为了判断已有弹窗是否为断网重连窗口
        /// </summary>
        public string DialogResultWindowsType { get; set; }

        public static IWin32Window TopWindow { get; set; }

        #endregion

        #region 按钮事件

        private void mediButton1_Click(object sender, EventArgs e)
        {
            MediParameter.ButType = MediParameter.bluess ? "Button2" : "Button1";
            MediParameter.Operation = this.mediButton1.Text;
            this.Close();
        }

        private void mediButton2_Click(object sender, EventArgs e)
        {
            MediParameter.ButType = MediParameter.bluess ? "Button3" : "Button2";
            MediParameter.Operation = this.mediButton2.Text;
            if (mediButton2.Text == "取消")
                this.Close();
            else if (this.mediButton2.Text == "忽略")
                this.Close();
            else if (mediButton2.Text == "否(&N)")
                this.Close();
            else if (this.mediButton2.Text.Contains("详细信息"))
            {
                this.panelControl1.Visible = !this.panelControl1.Visible;
                this.mediButton2.Text = "详细信息";

                if (this.FindForm().Height <= MediParameter.height)
                {
                    if (this.FindForm().Size == new Size(MediParameter.width, MediParameter.frmheight))
                    {
                        this.FindForm().Size = new Size(MediParameter.width, MediParameter.height);
                        this.mediTextBox1.Text = MediParameter.Information;
                    }
                    else if (this.FindForm().Size == new Size(MediParameter.width, MediParameter.height))
                    {
                        this.FindForm().Size = new Size(MediParameter.width, MediParameter.frmheight);
                    }
                }
                else
                {
                    this.FindForm().Size = new Size(MediParameter.width, MediParameter.frmheight);
                }
            }
            else
            {
                this.Close();
            }
        }

        private void mediButton3_Click(object sender, EventArgs e)
        {
            MediParameter.ButType = MediParameter.bluess ? "Button1" : "Button3";
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

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            Keys key = (keyData & Keys.KeyCode);

            if (key == Keys.Down)
            {
                SendKeys.Send("{Tab}");
                return true;
            }
            if (key == Keys.Up)
            {
                SendKeys.Send("{Tab}");
                return true;
            }
            if (key == Keys.Left)
            {
                SendKeys.Send("{Tab}");
                return true;
            }
            if (key == Keys.Right)
            {
                SendKeys.Send("{Tab}");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        #region 相关功能        

        /// <summary>
        /// 显示具有指定文本的消息框。
        /// </summary>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <returns></returns>
        public static DialogResult Show(string text)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = "\r\n" + text;
            box.mediButton2.Visible = false;
            box.mediButton3.Visible = false;
            box.mediPanelControl3.Visible = false;
            box.mediPanelControl4.Visible = false;
            string path = ImageIco("Information");
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            box.mediButton1.Text = "确定";
            MediSelfAdaption(ref box, text);
            if (TopWindow != null && TopWindow is Form frm && !frm.IsDisposed)
                box.ShowDialog(TopWindow);
            else
            {
                if (HISClientHelper.MainForm != null)
                    box.ShowDialog(HISClientHelper.MainForm);
                else
                {
                    box.StartPosition = FormStartPosition.CenterScreen;
                    box.ShowDialog();
                }
            }
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 显示具有指定文本的消息框。(显示无标题栏)
        /// </summary>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <param name="isTitle">是否显示标题栏</param>
        /// <param name="subText">要在消息框中显示的副文本</param>
        /// <returns></returns>
        public static DialogResult Show(string text, bool isTitle = true, string subText = null)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            if (isTitle)
            {
                return Show(text);
            }
            else
            {
                MediPromptDialog dialog = new MediPromptDialog();
                return dialog.Show(text, subText);
            }
        }

        /// <summary>
        /// 在指定对象的前面显示具有指定文本的消息框
        /// </summary>
        /// <param name="window">将拥有模式对话框的 System.Windows.Forms.IWin32Window 的一个实现</param>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window window, string text)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            box.mediButton2.Visible = false;
            box.mediButton3.Visible = false;
            box.mediPanelControl3.Visible = false;
            box.mediPanelControl4.Visible = false;
            string path = ImageIco("Information");
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            box.mediButton1.Text = "确定";
            MediSelfAdaption(ref box, text);
            box.ShowDialog(window);
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 在指定对象的前面显示具有指定文本的消息框(显示无标题栏)
        /// </summary>
        /// <param name="window">将拥有模式对话框的 System.Windows.Forms.IWin32Window 的一个实现</param>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <param name="isTitle">是否显示标题栏</param>
        /// <param name="subText">要在消息框中显示的副文本</param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window window, string text, bool isTitle = true, string subText = null)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            if (isTitle)
            {
                return Show(text);
            }
            else
            {
                MediPromptDialog dialog = new MediPromptDialog();
                return dialog.Show(window, text, subText);
            }
        }

        /// <summary>
        /// 显示具有指定文本和标题的消息框
        /// </summary>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <param name="caption">要在消息框的标题栏中显示的文本</param>
        /// <returns></returns>
        public static DialogResult Show(string text, string caption)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = "\r\n" + text;
            if (!string.IsNullOrEmpty(caption))
                box.Text = "联众科技-" + caption;
            else
                box.Text = "联众智慧";
            box.mediButton2.Visible = false;
            box.mediButton3.Visible = false;
            box.mediPanelControl3.Visible = false;
            box.mediPanelControl4.Visible = false;
            box.mediButton1.Text = "确定";
            string path = ImageIco("Information");
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            MediSelfAdaption(ref box, text);
            if (TopWindow != null && TopWindow is Form frm && !frm.IsDisposed)
                box.ShowDialog(TopWindow);
            else
            {
                if (HISClientHelper.MainForm != null)
                    box.ShowDialog(HISClientHelper.MainForm);
                else
                {
                    box.StartPosition = FormStartPosition.CenterScreen;
                    box.ShowDialog();
                }
            }
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 显示具有指定文本和标题的消息框
        /// </summary>
        /// <param name="window">将拥有模式对话框的 System.Windows.Forms.IWin32Window 的一个实现</param>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <param name="caption">要在消息框的标题栏中显示的文本</param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window window, string text, string caption)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            if (!string.IsNullOrEmpty(caption))
                box.Text = "联众科技-" + caption;
            else
                box.Text = "联众智慧";
            box.mediButton2.Visible = false;
            box.mediButton3.Visible = false;
            box.mediPanelControl3.Visible = false;
            box.mediPanelControl4.Visible = false;
            box.mediButton1.Text = "确定";
            string path = ImageIco("Information");
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            MediSelfAdaption(ref box, text);
            box.ShowDialog(window);
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 显示具有指定文本、标题和按钮的消息框
        /// </summary>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <param name="caption">要在消息框的标题栏中显示的文本</param>
        /// <param name="btn">System.Windows.Forms.MessageBoxButtons 值之一，可指定在消息框中显示哪些按钮</param>
        /// <returns></returns>
        public static DialogResult Show(string text, string caption, MediButtonShow btn)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = "\r\n" + text;
            if (!string.IsNullOrEmpty(caption))
                box.Text = "联众科技-" + caption;
            else
                box.Text = "联众智慧";
            MediButtonType(btn.ToString(), ref box);
            string path = ImageIco("Information");
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            MediSelfAdaption(ref box, text);
            if (TopWindow != null && TopWindow is Form frm && !frm.IsDisposed)
                box.ShowDialog(TopWindow);
            else
            {
                if (HISClientHelper.MainForm != null)
                    box.ShowDialog(HISClientHelper.MainForm);
                else
                {
                    box.StartPosition = FormStartPosition.CenterScreen;
                    box.ShowDialog();
                }
            }
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 显示具有指定文本、标题、按钮和图标的消息框
        /// </summary>
        /// <param name="window">将拥有模式对话框的 System.Windows.Forms.IWin32Window 的一个实现</param>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <param name="btn">MediButtonShow 值之一，可指定在消息框中显示哪些按钮。</param>
        /// <param name="icon">MediImagesIco 值之一，它指定在消息框中显示哪个图标。</param>
        /// <param name="isTitle">是否显示标题栏</param>
        /// <param name="subText">要在消息框中显示的副文本</param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window window, string text, MediButtonShow btn, MediImagesIco icon, bool isTitle = true, string subText = null)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            if (isTitle)
            {
                return Show(window, text, "联众智慧提示", btn, icon);
            }
            else
            {
                MediPromptDialog dialog = new MediPromptDialog();
                return dialog.Show(text, btn, icon, subText);
            }
        }

        public static DialogResult Show(IWin32Window window, string text, string btn, MediImagesIco icon, bool isTitle = true, string subText = null)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            if (isTitle)
            {
                return Show(window, text, "联众智慧提示", MediButtonShow.OKCancel, icon);
            }
            else
            {
                MediPromptDialog dialog = new MediPromptDialog();
                return dialog.Show(text, btn, icon, subText);
            }
        }


        /// <summary>
        /// 显示具有指定文本、标题和按钮的消息框
        /// </summary>
        /// <param name="window">将拥有模式对话框的 System.Windows.Forms.IWin32Window 的一个实现</param>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <param name="caption">要在消息框的标题栏中显示的文本</param>
        /// <param name="btn">MediButtonShow 值之一，可指定在消息框中显示哪些按钮</param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window window, string text, string caption, MediButtonShow btn)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            if (!string.IsNullOrEmpty(caption))
                box.Text = "联众科技-" + caption;
            else
                box.Text = "联众智慧";
            MediButtonType(btn.ToString(), ref box);
            string path = ImageIco("Information");
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            MediSelfAdaption(ref box, text);
            box.ShowDialog(window);
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 显示具有指定文本、标题、按钮和图标的消息框
        /// </summary>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <param name="caption">要在消息框的标题栏中显示的文本</param>
        /// <param name="btn">MediButtonShow 值之一，可指定在消息框中显示哪些按钮。</param>
        /// <param name="icon">MediImagesIco 值之一，它指定在消息框中显示哪个图标。</param>
        /// <returns></returns>
        public static DialogResult Show(string text, string caption, MediButtonShow btn, MediImagesIco icon)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = "\r\n" + text;
            if (!string.IsNullOrEmpty(caption))
                box.Text = "联众科技-" + caption;
            else
                box.Text = "联众智慧";
            MediButtonType(btn.ToString(), ref box);
            string path = ImageIco(icon.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            MediSelfAdaption(ref box, text);
            if (TopWindow != null && TopWindow is Form frm && !frm.IsDisposed)
                box.ShowDialog(TopWindow);
            else
            {
                if (HISClientHelper.MainForm != null)
                    box.ShowDialog(HISClientHelper.MainForm);
                else
                {
                    box.StartPosition = FormStartPosition.CenterScreen;
                    box.ShowDialog();
                }
            }
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 显示具有指定文本、标题、按钮和图标的消息框
        /// </summary>
        /// <param name="window">将拥有模式对话框的 System.Windows.Forms.IWin32Window 的一个实现</param>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <param name="caption">要在消息框的标题栏中显示的文本</param>
        /// <param name="btn">MediButtonShow 值之一，可指定在消息框中显示哪些按钮。</param>
        /// <param name="icon">MediImagesIco 值之一，它指定在消息框中显示哪个图标。</param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window window, string text, string caption, MediButtonShow btn, MediImagesIco icon)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            if (!string.IsNullOrEmpty(caption))
                box.Text = "联众科技-" + caption;
            else
                box.Text = "联众智慧";
            MediButtonType(btn.ToString(), ref box);
            string path = ImageIco(icon.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            MediSelfAdaption(ref box, text);
            box.ShowDialog(window);
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 显示具有指定文本、标题、按钮和图标的消息框
        /// </summary>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <param name="caption">要在消息框的标题栏中显示的文本</param>
        /// <param name="btn">MediButtonShow 值之一，可指定在消息框中显示哪些按钮。</param>
        /// <param name="icon">MediImagesIco 值之一，它指定在消息框中显示哪个图标。</param>
        /// <param name="defalt">System.Windows.Forms.MessageBoxDefaultButton 值之一，可指定消息框中的默认按钮</param>
        /// <param name="position">System.Windows.Forms.MessageBoxOptions 值之一，可指定将对消息框使用哪些显示和关联选项。若要使用默认值，请传入 0</param>
        /// <returns></returns>
        public static DialogResult Show(string text, string caption, MediButtonShow btn, MediImagesIco icon, MessageBoxDefaultButton defalt, MessageBoxOptions position)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = "\r\n" + text;
            if (!string.IsNullOrEmpty(caption))
                box.Text = "联众科技-" + caption;
            else
                box.Text = "联众智慧";
            MediButtonType(btn.ToString(), ref box);
            string path = ImageIco(icon.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            if (box.mediButton1.Text == "Button1")
                box.ActiveControl = box.mediButton1;
            else if (box.mediButton1.Text == "Button2")
                box.ActiveControl = box.mediButton2;
            MediStyle(position.ToString(), ref box);
            MediSelfAdaption(ref box, text);
            if (TopWindow != null && TopWindow is Form frm && !frm.IsDisposed)
                box.ShowDialog(TopWindow);
            else
            {
                if (HISClientHelper.MainForm != null)
                    box.ShowDialog(HISClientHelper.MainForm);
                else
                {
                    box.StartPosition = FormStartPosition.CenterScreen;
                    box.ShowDialog();
                }
            }
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 显示具有指定文本、标题、按钮和图标的消息框
        /// </summary>
        /// <param name="window">将拥有模式对话框的 System.Windows.Forms.IWin32Window 的一个实现</param>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <param name="caption">要在消息框的标题栏中显示的文本</param>
        /// <param name="btn">MediButtonShow 值之一，可指定在消息框中显示哪些按钮。</param>
        /// <param name="icon">MediImagesIco 值之一，它指定在消息框中显示哪个图标。</param>
        /// <param name="defalt">System.Windows.Forms.MessageBoxDefaultButton 值之一，可指定消息框中的默认按钮</param>
        /// <param name="position">System.Windows.Forms.MessageBoxOptions 值之一，可指定将对消息框使用哪些显示和关联选项。若要使用默认值，请传入 0</param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window window, string text, string caption, MediButtonShow btn, MediImagesIco icon, MessageBoxDefaultButton defalt, MessageBoxOptions position)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            if (!string.IsNullOrEmpty(caption))
                box.Text = "联众科技-" + caption;
            else
                box.Text = "联众智慧";
            MediButtonType(btn.ToString(), ref box);
            string path = ImageIco(icon.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            if (box.mediButton1.Text == "Button1")
                box.ActiveControl = box.mediButton1;
            else if (box.mediButton1.Text == "Button2")
                box.ActiveControl = box.mediButton2;
            MediStyle(position.ToString(), ref box);
            MediSelfAdaption(ref box, text);
            box.ShowDialog(window);
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 显示具有指定文本、标题、按钮和图标的消息框
        /// </summary>        
        /// <param name="text">要在消息框中显示的文本</param>
        /// <param name="caption">要在消息框的标题栏中显示的文本</param>
        /// <param name="btn">MediButtonShow 值之一，可指定在消息框中显示哪些按钮。</param>
        /// <param name="icon">MediImagesIco 值之一，它指定在消息框中显示哪个图标。</param>
        /// <param name="defalt">System.Windows.Forms.MessageBoxDefaultButton 值之一，可指定消息框中的默认按钮</param>
        /// <param name="position">System.Windows.Forms.MessageBoxOptions 值之一，可指定将对消息框使用哪些显示和关联选项。若要使用默认值，请传入 0</param>
        /// <param name="helpPath">用户单击“帮助”按钮时显示的“帮助”文件的路径和名称</param>
        /// <returns></returns>
        public static DialogResult Show(string text, string caption, MediButtonShow btn, MediImagesIco icon, MessageBoxDefaultButton defalt, MessageBoxOptions position, string helpPath)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = "\r\n" + text;
            if (!string.IsNullOrEmpty(caption))
                box.Text = "联众科技-" + caption;
            else
                box.Text = "联众智慧";
            MediButtonType(btn.ToString(), ref box);
            string path = ImageIco(icon.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            if (box.mediButton1.Text == "Button1")
                box.ActiveControl = box.mediButton1;
            else if (box.mediButton1.Text == "Button2")
                box.ActiveControl = box.mediButton2;
            MediParameter.HelpPath = helpPath;
            MediStyle(position.ToString(), ref box);
            MediSelfAdaption(ref box, text);
            if (TopWindow != null && TopWindow is Form frm && !frm.IsDisposed)
                box.ShowDialog(TopWindow);
            else
            {
                if (HISClientHelper.MainForm != null)
                    box.ShowDialog(HISClientHelper.MainForm);
                else
                {
                    box.StartPosition = FormStartPosition.CenterScreen;
                    box.ShowDialog();
                }
            }
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 显示具有指定文本、标题、按钮和图标的消息框
        /// </summary>
        /// <param name="window">将拥有模式对话框的 System.Windows.Forms.IWin32Window 的一个实现</param>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <param name="caption">要在消息框的标题栏中显示的文本</param>
        /// <param name="btn">MediButtonShow 值之一，可指定在消息框中显示哪些按钮。</param>
        /// <param name="icon">MediImagesIco 值之一，它指定在消息框中显示哪个图标。</param>
        /// <param name="defalt">System.Windows.Forms.MessageBoxDefaultButton 值之一，可指定消息框中的默认按钮</param>
        /// <param name="position">System.Windows.Forms.MessageBoxOptions 值之一，可指定将对消息框使用哪些显示和关联选项。若要使用默认值，请传入 0</param>
        /// <param name="helpPath">用户单击“帮助”按钮时显示的“帮助”文件的路径和名称</param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window window, string text, string caption, MediButtonShow btn, MediImagesIco icon, MessageBoxDefaultButton defalt, MessageBoxOptions position, string helpPath)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            if (!string.IsNullOrEmpty(caption))
                box.Text = "联众科技-" + caption;
            else
                box.Text = "联众智慧";
            MediButtonType(btn.ToString(), ref box);
            string path = ImageIco(icon.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            if (box.mediButton1.Text == "Button1")
                box.ActiveControl = box.mediButton1;
            else if (box.mediButton1.Text == "Button2")
                box.ActiveControl = box.mediButton2;
            MediParameter.HelpPath = helpPath;
            MediStyle(position.ToString(), ref box);
            MediSelfAdaption(ref box, text);
            box.ShowDialog(window);
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 显示具有指定文本、标题和按钮的消息框(显示无标题栏)
        /// </summary>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <param name="btn">System.Windows.Forms.MessageBoxButtons 值之一，可指定在消息框中显示哪些按钮</param>
        /// <param name="subText">要在消息框中显示的副文本</param>
        /// <returns></returns>
        public static DialogResult Show(string text, MediButtonShow btn, string subText = null)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediPromptDialog dialog = new MediPromptDialog();
            return dialog.Show(text, btn, subText);
        }

        /// <summary>
        /// 显示具有指定文本、标题和按钮的消息框(显示无标题栏)
        /// </summary>
        /// <param name="window">将拥有模式对话框的 System.Windows.Forms.IWin32Window 的一个实现</param>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <param name="btn">System.Windows.Forms.MessageBoxButtons 值之一，可指定在消息框中显示哪些按钮</param>
        /// <param name="subText">要在消息框中显示的副文本</param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window window, string text, MediButtonShow btn, string subText = null)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediPromptDialog dialog = new MediPromptDialog();
            return dialog.Show(window, text, btn, subText);
        }

        /// <summary>
        /// 显示具有指定文本、标题、按钮和图标的消息框(显示无标题栏)
        /// </summary>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <param name="btn">MediButtonShow 值之一，可指定在消息框中显示哪些按钮。</param>
        /// <param name="icon">MediImagesIco 值之一，它指定在消息框中显示哪个图标。</param>
        /// <param name="subText">要在消息框中显示的副文本</param>
        /// <returns></returns>
        public static DialogResult Show(string text, MediButtonShow btn, MediImagesIco icon, string subText = null)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediPromptDialog dialog = new MediPromptDialog();
            return dialog.Show(text, btn, icon, subText);
        }

        /// <summary>
        /// 显示具有指定文本、标题、按钮和图标的消息框(显示无标题栏)
        /// </summary>
        /// <param name="window">将拥有模式对话框的 System.Windows.Forms.IWin32Window 的一个实现</param>
        /// <param name="text">要在消息框中显示的文本</param>
        /// <param name="btn">MediButtonShow 值之一，可指定在消息框中显示哪些按钮。</param>
        /// <param name="icon">MediImagesIco 值之一，它指定在消息框中显示哪个图标。</param>
        /// <param name="subText">要在消息框中显示的副文本</param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window window, string text, MediButtonShow btn, MediImagesIco icon, string subText = null)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediPromptDialog dialog = new MediPromptDialog();
            return dialog.Show(window, text, btn, icon, subText);
        }

        /// <summary>
        /// 显示指定文字浮动窗口,指定秒数后自动关闭,单位毫秒
        /// </summary>
        public static void FloatMsg(string text, int seconds)
        {
            //var box = new MediFloatMsgBox(text, seconds);
            //box.Show();
            if (seconds < 10) seconds = seconds * 1000; //兼容以前的写法,如果传入过小,*1000,因为以前是当作秒传的.
            MediPromptBox box = new MediPromptBox(text, "", "0", seconds);
            box.TopMost = true;
            box.Show();
        }
        /// <summary>
        /// 显示指定文字浮动窗口,指定秒数后自动关闭,单位毫秒
        /// </summary>
        /// <param name="window">当前窗口，传入this</param>
        public static void FloatMsg(IWin32Window window, string text, int seconds)
        {
            //var box = new MediFloatMsgBox(text, seconds);
            //box.Show();
            if (seconds < 10) seconds = seconds * 1000; //兼容以前的写法,如果传入过小,*1000,因为以前是当作秒传的.
            MediPromptBox box = new MediPromptBox(text, "", "0", seconds);
            box.Show(window);
        }
        /// <summary>
        /// 显示指定文字浮动窗口,固定3秒关闭
        /// </summary>
        public static void FloatMsg(string text)
        {
            FloatMsg(text, 3000);
        }
        /// <summary>
        /// 显示指定文字浮动窗口,固定3秒关闭
        /// </summary>
        /// <param name="window">当前窗口，传入this</param>
        public static void FloatMsg(IWin32Window window, string text)
        {
            FloatMsg(window, text, 3000);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tiShiXX">提示信息</param>
        /// <param name="tiShiNR">提示内容</param>
        /// <param name="isImage">图片显示类型(0、不显示;1、成功;2、失败.)</param>
        /// <param name="seconds">关闭时间，默认2秒，传入毫秒数据(1秒等于1000毫秒)</param>
        public static void FloatMsg(string tiShiXX, string tiShiNR, string isImage, double seconds = 2000)
        {
            IsDisposedBox();
            MediPromptBox box = new MediPromptBox(tiShiXX, tiShiNR, isImage, seconds);
            box.TopMost = true;
            box.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window">当前窗口，传入this</param>
        /// <param name="tiShiXX">提示信息</param>
        /// <param name="tiShiNR">提示内容</param>
        /// <param name="isImage">图片显示类型(0、不显示;1、成功;2、失败.)</param>
        /// <param name="seconds">关闭时间，默认2秒，传入毫秒数据(1秒等于1000毫秒)</param>
        public static void FloatMsg(IWin32Window window, string tiShiXX, string tiShiNR, string isImage, double seconds = 2000)
        {
            IsDisposedBox();
            MediPromptBox box = new MediPromptBox(tiShiXX, tiShiNR, isImage, seconds);
            box.Show(window);
        }

        /// <summary>
        /// 处理重叠弹窗(强制关闭上一个弹出框)
        /// </summary>
        private static void IsDisposedBox()
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "MediPromptBox")
                {
                    f.Close();
                    break;
                }
            }
        }

        /// <summary>
        /// 处理网络连接失败的情况
        /// </summary>
        /// <param name="window"></param>
        /// <param name="returnValue"></param>
        /// <param name="mediResultButton"></param>
        /// <returns></returns>
        private static bool DealWithDisConnection(IWin32Window window, BaseResult returnValue, out DialogResult mediResultButton)
        {
            if (returnValue.ExceptionContent.Contains("无法连接到远程服务器"))
            {
                foreach (Form p in Application.OpenForms)
                {
                    // 判断界面上是否存在请求失败窗口，存在则关闭
                    if (p is MediMsgBox ms && ms.mediLabel1.Text.Contains("在 WebClient 请求期间发生异常"))
                    {
                        ms.Close();
                    }
                    // 判断界面上是否已经弹出此窗口,存在则不继续往下走，弹窗一个就够了
                    if (p is MediMsgBox mb && mb.DialogResultWindowsType != null && mb.DialogResultWindowsType == "DealWithDisConnection")
                    {
                        mediResultButton = DialogResult.Abort;
                        return true;
                    }
                }
                MediMsgBox yesNoBox = new MediMsgBox();
                const string text = "与服务器的连接出现异常,是否继续使用? 选择否将退出系统";
                yesNoBox.mediLabel1.Text = text;
                yesNoBox.mediButton3.Visible = false;
                yesNoBox.mediPanelControl3.Visible = false;
                yesNoBox.mediPictureEdit1.Visible = false;
                MediButtonType(MediButtonShow.YesNo.ToString(), ref yesNoBox);

                MediSelfAdaption(ref yesNoBox, text);
                yesNoBox.DialogResultWindowsType = "DealWithDisConnection";
                if (window != null)
                {
                    yesNoBox.ShowDialog(window);
                }
                else
                {
                    if (TopWindow != null && TopWindow is Form frm && !frm.IsDisposed)
                        yesNoBox.ShowDialog(TopWindow);
                    else
                    {
                        if (HISClientHelper.MainForm != null)
                            yesNoBox.ShowDialog(HISClientHelper.MainForm);
                        else
                        {
                            yesNoBox.StartPosition = FormStartPosition.CenterScreen;
                            yesNoBox.ShowDialog();
                        }
                    }
                }

                yesNoBox.Dispose();
                {
                    mediResultButton = MediResultButton(MediParameter.Operation);
                    if (mediResultButton == DialogResult.No)
                    {
                        // 选择不继续连接，关闭程序
                        Environment.Exit(0);
                    }
                    return true;
                }
            }

            mediResultButton = DialogResult.None;
            return false;
        }

        /// <summary>
        /// 判断已存在的消息弹框
        /// </summary>
        /// <returns></returns>
        private static bool CheckExistDialogWindow()
        {
            foreach (Form form in Application.OpenForms)
            {
                // 如果界面上已存在断网处理消息弹窗了，就不在继续弹出窗口
                if (form is MediMsgBox mb && mb.DialogResultWindowsType != null && mb.DialogResultWindowsType == "DealWithDisConnection")
                {
                    return false;
                }
                // 关闭已存在的一般消息弹框
                if (form is MediMsgBox ms && ms.DialogResultWindowsType == "Common")
                {
                    ms.Close();
                }
            }
            return true;
        }

        #region 特殊

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        /// <summary>
        /// 成功提示弹出框
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DialogResult Success(string text)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            box.mediButton1.Visible = false;
            box.mediButton2.Visible = false;
            box.mediPanelControl4.Visible = false;
            box.mediPanelControl5.Visible = false;
            string path = ImageIco(MediImagesIco.Exclamation.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            box.mediButton3.Text = "确定";
            MediSelfAdaption(ref box, text);
            if (TopWindow != null && TopWindow is Form frm && !frm.IsDisposed)
                box.ShowDialog(TopWindow);
            else
            {
                if (HISClientHelper.MainForm != null)
                    box.ShowDialog(HISClientHelper.MainForm);
                else
                {
                    box.StartPosition = FormStartPosition.CenterScreen;
                    box.ShowDialog();
                }
            }
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 成功提示弹出框重载伴随主窗体
        /// </summary>
        /// <param name="window"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DialogResult Success(IWin32Window window, string text)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            box.mediButton1.Visible = false;
            box.mediButton2.Visible = false;
            box.mediPanelControl4.Visible = false;
            box.mediPanelControl5.Visible = false;
            string path = ImageIco(MediImagesIco.Exclamation.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            box.mediButton3.Text = "确定";
            MediSelfAdaption(ref box, text);
            box.ShowDialog(window);
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 自定义按钮显示值弹出框
        /// </summary>
        /// <param name="text"></param>
        /// <param name="btnText"></param>
        /// <returns></returns>
        public static DialogResult Success(string text, string btnText)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            box.mediButton1.Visible = false;
            box.mediButton2.Visible = false;
            box.mediPanelControl4.Visible = false;
            box.mediPanelControl5.Visible = false;
            string path = ImageIco(MediImagesIco.Exclamation.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            box.mediButton3.Text = btnText;
            MediSelfAdaption(ref box, text);
            if (TopWindow != null && TopWindow is Form frm && !frm.IsDisposed)
                box.ShowDialog(TopWindow);
            else
            {
                if (HISClientHelper.MainForm != null)
                    box.ShowDialog(HISClientHelper.MainForm);
                else
                {
                    box.StartPosition = FormStartPosition.CenterScreen;
                    box.ShowDialog();
                }
            }
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 自定义按钮显示值弹出框重载伴随主窗体
        /// </summary>
        /// <param name="window"></param>
        /// <param name="text"></param>
        /// <param name="btnText"></param>
        /// <returns></returns>
        public static DialogResult Success(IWin32Window window, string text, string btnText)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            box.mediButton1.Visible = false;
            box.mediButton2.Visible = false;
            box.mediPanelControl4.Visible = false;
            box.mediPanelControl5.Visible = false;
            string path = ImageIco(MediImagesIco.Exclamation.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            box.mediButton3.Text = btnText;
            MediSelfAdaption(ref box, text);
            box.ShowDialog(window);
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 显示详细信息弹出框
        /// </summary>
        /// <param name="text"></param>
        /// <param name="retCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [Obsolete("不建议继续使用该方法，请使用其他扩展方法（如：DialogResult Failure(string msg, BaseResult returnValue)）")]
        public static DialogResult Failure(string text, string retCode, string errMsg, bool iserrMsg = true)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            MediButtonType(MediButtonShow.YesInformation.ToString(), ref box);
            string path = ImageIco(MediImagesIco.Error.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            box.ActiveControl = box.mediButton1;
            if (iserrMsg)
                box.mediLabel1.Text = text + "\r\n" + errMsg;//加载错误信息
            else
                box.mediLabel1.Text = text + "\r\n" + "请查看详细错误信息......";
            MediParameter.Information = errMsg;
            MediParameter.width = box.Width;
            MediParameter.frmheight = box.Height;
            MediParameter.txtheight = box.panelControl1.Height;
            MediParameter.height = MediParameter.frmheight + MediParameter.txtheight;
            MediSelfAdaption(ref box, errMsg);
            if (TopWindow != null && TopWindow is Form frm && !frm.IsDisposed)
                box.ShowDialog(TopWindow);
            else
            {
                if (HISClientHelper.MainForm != null)
                    box.ShowDialog(HISClientHelper.MainForm);
                else
                {
                    box.StartPosition = FormStartPosition.CenterScreen;
                    box.ShowDialog();
                }
            }
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 显示详细信息弹出框重载伴随主窗体
        /// </summary>
        /// <param name="window"></param>
        /// <param name="text"></param>
        /// <param name="retCode"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        [Obsolete("不建议继续使用该方法，请使用其他扩展方法（如：DialogResult Failure(IWin32Window window,string msg, BaseResult returnValue)）")]
        public static DialogResult Failure(IWin32Window window, string text, string retCode, string errMsg, bool iserrMsg = true)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            MediButtonType(MediButtonShow.YesInformation.ToString(), ref box);
            string path = ImageIco(MediImagesIco.Error.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            box.ActiveControl = box.mediButton1;
            if (iserrMsg)
                box.mediLabel1.Text = text + "\r\n" + errMsg;//加载错误信息
            else
                box.mediLabel1.Text = text + "\r\n" + "请查看详细错误信息......";
            MediParameter.Information = errMsg;
            MediParameter.width = box.Width;
            MediParameter.frmheight = box.Height;
            MediParameter.txtheight = box.panelControl1.Height;
            MediParameter.height = MediParameter.frmheight + MediParameter.txtheight;
            MediSelfAdaption(ref box, errMsg);
            box.ShowDialog(window);
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 根据服务端返回对象自适应区分详细信息异常
        /// </summary>
        /// <param name="ReturnValue"></param>
        /// <returns></returns>
        public static DialogResult Failure(BaseResult ReturnValue)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            if (!string.IsNullOrEmpty(ReturnValue.ExceptionContent))
            {
                // 返回为true的情况下，即为不继续下面的代码
                if (DealWithDisConnection(null, ReturnValue, out var mediResultButton))
                {
                    return mediResultButton;
                }

                MediMsgBox box = new MediMsgBox();
                MediButtonType(MediButtonShow.YesInformation.ToString(), ref box);
                string path = ImageIco(MediImagesIco.Error.ToString());
                if (!string.IsNullOrEmpty(path))
                    box.mediPictureEdit1.Image = Image.FromFile(path);
                else
                    box.mediPictureEdit1.Visible = false;
                box.ActiveControl = box.mediButton1;
                string errorText = ErrorInfoExtraction(ReturnValue.ExceptionContent);
                if (String.IsNullOrWhiteSpace(errorText))
                    box.mediLabel1.Text = ReturnValue.ReturnMessage;        // 加载错误信息
                else
                    box.mediLabel1.Text = errorText;
                MediParameter.Information = ReturnValue.ExceptionContent + Environment.NewLine + ReturnValue.ExceptionPosition;
                MediParameter.width = box.Width;
                MediParameter.frmheight = box.Height;
                MediParameter.txtheight = box.panelControl1.Height;
                MediParameter.height = MediParameter.frmheight + MediParameter.txtheight;
                MediSelfAdaption(ref box, ReturnValue.ExceptionContent + Environment.NewLine + ReturnValue.ExceptionPosition);
                if (TopWindow != null && TopWindow is Form frm && !frm.IsDisposed)
                    box.ShowDialog(TopWindow);
                else
                {
                    if (HISClientHelper.MainForm != null)
                        box.ShowDialog(HISClientHelper.MainForm);
                    else
                    {
                        box.StartPosition = FormStartPosition.CenterScreen;
                        box.ShowDialog();
                    }
                }
                box.Dispose();
                return MediResultButton(MediParameter.Operation);
            }
            else
            { return Show(ReturnValue.ReturnMessage); }
        }

        /// <summary>
        /// 错误提示
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="returnValue">详细信息</param>
        /// <returns></returns>
        public static DialogResult Failure(string msg, BaseResult returnValue)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            if (!string.IsNullOrEmpty(returnValue.ExceptionContent))
            {
                if (DealWithDisConnection(null, returnValue, out var mediResultButton)) return mediResultButton;

                MediMsgBox box = new MediMsgBox();
                MediButtonType(MediButtonShow.YesInformation.ToString(), ref box);
                string path = ImageIco(MediImagesIco.Error.ToString());
                if (!string.IsNullOrEmpty(path))
                    box.mediPictureEdit1.Image = Image.FromFile(path);
                else
                    box.mediPictureEdit1.Visible = false;
                box.ActiveControl = box.mediButton1;
                box.mediLabel1.Text = ErrorInfoExtraction(returnValue.ExceptionContent) + msg;//加载错误信息
                MediParameter.Information = returnValue.ExceptionContent + Environment.NewLine + returnValue.ExceptionPosition;
                MediParameter.width = box.Width;
                MediParameter.frmheight = box.Height;
                MediParameter.txtheight = box.panelControl1.Height;
                MediParameter.height = MediParameter.frmheight + MediParameter.txtheight;
                MediSelfAdaption(ref box, returnValue.ExceptionContent + Environment.NewLine + returnValue.ExceptionPosition);
                if (TopWindow != null && TopWindow is Form frm && !frm.IsDisposed)
                    box.ShowDialog(TopWindow);
                else
                {
                    if (HISClientHelper.MainForm != null)
                        box.ShowDialog(HISClientHelper.MainForm);
                    else
                    {
                        box.StartPosition = FormStartPosition.CenterScreen;
                        box.ShowDialog();
                    }
                }
                box.Dispose();
                return MediResultButton(MediParameter.Operation);
            }
            else
            { return Show(returnValue.ReturnMessage); }
        }

        /// <summary>
        /// 根据服务端返回对象自适应区分详细信息异常伴随主窗体
        /// </summary>
        /// <param name="window"></param>
        /// <param name="returnValue"></param>
        /// <returns></returns>
        public static DialogResult Failure(IWin32Window window, BaseResult returnValue)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            if (!string.IsNullOrEmpty(returnValue.ExceptionContent))
            {
                if (DealWithDisConnection(window, returnValue, out var mediResultButton)) return mediResultButton;

                MediMsgBox box = new MediMsgBox();
                MediButtonType(MediButtonShow.YesInformation.ToString(), ref box);
                string path = ImageIco(MediImagesIco.Error.ToString());
                if (!string.IsNullOrEmpty(path))
                    box.mediPictureEdit1.Image = Image.FromFile(path);
                else
                    box.mediPictureEdit1.Visible = false;
                box.ActiveControl = box.mediButton1;
                string errorText = ErrorInfoExtraction(returnValue.ExceptionContent);
                if (String.IsNullOrWhiteSpace(errorText))
                    box.mediLabel1.Text = returnValue.ReturnMessage;        // 加载错误信息
                else
                    box.mediLabel1.Text = errorText;
                MediParameter.Information = returnValue.ExceptionContent + Environment.NewLine + returnValue.ExceptionPosition;
                MediParameter.width = box.Width;
                MediParameter.frmheight = box.Height;
                MediParameter.txtheight = box.panelControl1.Height;
                MediParameter.height = MediParameter.frmheight + MediParameter.txtheight;
                MediSelfAdaption(ref box, returnValue.ExceptionContent + Environment.NewLine + returnValue.ExceptionPosition);
                box.ShowDialog(window);
                box.Dispose();
                return MediResultButton(MediParameter.Operation);
            }
            else
            { return Show(returnValue.ReturnMessage); }
        }

        /// <summary>
        /// 错误提醒
        /// </summary>
        /// <param name="window">父窗口</param>
        /// <param name="msg">消息</param>
        /// <param name="returnValue">详细消息</param>
        /// <returns></returns>
        public static DialogResult Failure(IWin32Window window, string msg, BaseResult returnValue)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            if (!string.IsNullOrEmpty(returnValue.ExceptionContent))
            {
                if (DealWithDisConnection(window, returnValue, out var mediResultButton)) return mediResultButton;
                MediMsgBox box = new MediMsgBox();
                MediButtonType(MediButtonShow.YesInformation.ToString(), ref box);
                string path = ImageIco(MediImagesIco.Error.ToString());
                if (!string.IsNullOrEmpty(path))
                    box.mediPictureEdit1.Image = Image.FromFile(path);
                else
                    box.mediPictureEdit1.Visible = false;
                box.ActiveControl = box.mediButton1;
                box.mediLabel1.Text = ErrorInfoExtraction(returnValue.ExceptionContent) + msg;//加载错误信息
                MediParameter.Information = returnValue.ExceptionContent + Environment.NewLine + returnValue.ExceptionPosition;
                MediParameter.width = box.Width;
                MediParameter.frmheight = box.Height;
                MediParameter.txtheight = box.panelControl1.Height;
                MediParameter.height = MediParameter.frmheight + MediParameter.txtheight;
                MediSelfAdaption(ref box, returnValue.ExceptionContent + Environment.NewLine + returnValue.ExceptionPosition);
                box.ShowDialog(window);
                box.Dispose();
                return MediResultButton(MediParameter.Operation);
            }
            else
            { return Show(returnValue.ReturnMessage); }
        }

        /// <summary>
        /// 失败提示弹出框
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DialogResult Failure(string text)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            box.mediButton1.Visible = false;
            box.mediButton2.Visible = false;
            box.mediPanelControl4.Visible = false;
            box.mediPanelControl5.Visible = false;
            string path = ImageIco(MediImagesIco.Error.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            box.mediButton3.Text = "确定";
            MediSelfAdaption(ref box, text);
            if (TopWindow != null && TopWindow is Form frm && !frm.IsDisposed)
                box.ShowDialog(TopWindow);
            else
            {
                if (HISClientHelper.MainForm != null)
                    box.ShowDialog(HISClientHelper.MainForm);
                else
                {
                    box.StartPosition = FormStartPosition.CenterScreen;
                    box.ShowDialog();
                }
            }
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 失败提示弹出框重载伴随主窗体
        /// </summary>
        /// <param name="window"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DialogResult Failure(IWin32Window window, string text)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            box.mediButton1.Visible = false;
            box.mediButton2.Visible = false;
            box.mediPanelControl4.Visible = false;
            box.mediPanelControl5.Visible = false;
            string path = ImageIco(MediImagesIco.Error.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            box.mediButton3.Text = "确定";
            MediSelfAdaption(ref box, text);
            box.ShowDialog(window);
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 警告提示
        /// </summary>
        /// <param name="text"></param>
        /// <param name="defalt"></param>
        /// <returns></returns>
        public static DialogResult WarnYesNo(string text, MessageBoxDefaultButton defalt)
        {
            IsDisposedBox();
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            box.mediButton3.Visible = false;
            box.mediPanelControl3.Visible = false;
            string path = ImageIco(MediImagesIco.Warning.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            MediButtonType(MediButtonShow.YesNo.ToString(), ref box);
            if (defalt.ToString() == "Button1")
            { box.ActiveControl = box.mediButton1; }
            else if (defalt.ToString() == "Button2")
            { box.ActiveControl = box.mediButton2; }
            MediSelfAdaption(ref box, text);
            if (TopWindow != null && TopWindow is Form frm && !frm.IsDisposed)
                box.ShowDialog(TopWindow);
            else
            {
                if (HISClientHelper.MainForm != null)
                    box.ShowDialog(HISClientHelper.MainForm);
                else
                {
                    box.StartPosition = FormStartPosition.CenterScreen;
                    box.ShowDialog();
                }
            }
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 是否弹出框
        /// </summary>
        /// <param name="text"></param>
        /// <param name="defalt"></param>
        /// <returns></returns>
        public static DialogResult YesNo(string text, MessageBoxDefaultButton defalt)
        {
            IsDisposedBox();
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            box.mediButton3.Visible = false;
            box.mediPanelControl3.Visible = false;
            string path = ImageIco(MediImagesIco.Question.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            MediButtonType(MediButtonShow.YesNo.ToString(), ref box);
            if (defalt.ToString() == "Button1")
            { box.ActiveControl = box.mediButton1; }
            else if (defalt.ToString() == "Button2")
            { box.ActiveControl = box.mediButton2; }
            MediSelfAdaption(ref box, text);
            if (TopWindow != null && TopWindow is Form frm && !frm.IsDisposed)
                box.ShowDialog(TopWindow);
            else
            {
                if (HISClientHelper.MainForm != null)
                    box.ShowDialog(HISClientHelper.MainForm);
                else
                {
                    box.StartPosition = FormStartPosition.CenterScreen;
                    box.ShowDialog();
                }
            }
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 是否弹出框重载伴随主窗体
        /// </summary>
        /// <param name="window"></param>
        /// <param name="text"></param>
        /// <param name="defalt"></param>
        /// <returns></returns>
        public static DialogResult YesNo(IWin32Window window, string text, MessageBoxDefaultButton defalt)
        {
            IsDisposedBox();
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            box.mediButton3.Visible = false;
            box.mediPanelControl3.Visible = false;
            string path = ImageIco(MediImagesIco.Question.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            MediButtonType(MediButtonShow.YesNo.ToString(), ref box);
            if (defalt.ToString() == "Button1")
            { box.ActiveControl = box.mediButton1; }
            else if (defalt.ToString() == "Button2")
            { box.ActiveControl = box.mediButton2; }
            MediSelfAdaption(ref box, text);
            box.ShowDialog(window);
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 确定取消弹出框
        /// </summary>
        /// <param name="text"></param>
        /// <param name="defalt"></param>
        /// <returns></returns>
        public static DialogResult OKCancel(string text, MessageBoxDefaultButton defalt)
        {
            IsDisposedBox();
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            box.mediButton3.Visible = false;
            box.mediPanelControl3.Visible = false;
            string path = ImageIco(MediImagesIco.Question.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            MediButtonType(MediButtonShow.OKCancel.ToString(), ref box);
            if (defalt.ToString() == "Button1")
            { box.ActiveControl = box.mediButton1; }
            else if (defalt.ToString() == "Button2")
            { box.ActiveControl = box.mediButton2; }
            MediSelfAdaption(ref box, text);
            if (TopWindow != null && TopWindow is Form frm && !frm.IsDisposed)
                box.ShowDialog(TopWindow);
            else
            {
                if (HISClientHelper.MainForm != null)
                    box.ShowDialog(HISClientHelper.MainForm);
                else
                {
                    box.StartPosition = FormStartPosition.CenterScreen;
                    box.ShowDialog();
                }
            }
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 确定取消弹出框重载伴随主窗体
        /// </summary>
        /// <param name="window"></param>
        /// <param name="text"></param>
        /// <param name="defalt"></param>
        /// <returns></returns>
        public static DialogResult OKCancel(IWin32Window window, string text, MessageBoxDefaultButton defalt)
        {
            IsDisposedBox();
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            box.mediButton3.Visible = false;
            box.mediPanelControl3.Visible = false;
            string path = ImageIco(MediImagesIco.Question.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            MediButtonType(MediButtonShow.YesNo.ToString(), ref box);
            if (defalt.ToString() == "Button1")
            { box.ActiveControl = box.mediButton1; }
            else if (defalt.ToString() == "Button2")
            { box.ActiveControl = box.mediButton2; }
            MediSelfAdaption(ref box, text);
            box.ShowDialog(window);
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 自定义是否按钮文字弹出框
        /// </summary>
        /// <param name="text"></param>
        /// <param name="btnText"></param>
        /// <param name="defalt"></param>
        /// <returns></returns>
        public static DialogResult YesNo(string text, string[] btnText, MessageBoxDefaultButton defalt)
        {
            IsDisposedBox();
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            box.mediButton3.Visible = false;
            box.mediPanelControl3.Visible = false;
            string path = ImageIco(MediImagesIco.Question.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            box.mediButton1.Text = btnText[0].ToString();
            box.mediButton2.Text = btnText[1].ToString();
            if (defalt.ToString() == "Button1")
            { box.ActiveControl = box.mediButton1; }
            else if (defalt.ToString() == "Button2")
            { box.ActiveControl = box.mediButton2; }
            MediSelfAdaption(ref box, text);
            if (TopWindow != null && TopWindow is Form frm && !frm.IsDisposed)
                box.ShowDialog(TopWindow);
            else
            {
                if (HISClientHelper.MainForm != null)
                    box.ShowDialog(HISClientHelper.MainForm);
                else
                {
                    box.StartPosition = FormStartPosition.CenterScreen;
                    box.ShowDialog();
                }
            }
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 自定义是否按钮文字弹出框重载伴随主窗体
        /// </summary>
        /// <param name="window"></param>
        /// <param name="text"></param>
        /// <param name="btnText"></param>
        /// <param name="defalt"></param>
        /// <returns></returns>
        public static DialogResult YesNo(IWin32Window window, string text, string[] btnText, MessageBoxDefaultButton defalt)
        {
            IsDisposedBox();
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            box.mediButton3.Visible = false;
            box.mediPanelControl3.Visible = false;
            string path = ImageIco(MediImagesIco.Question.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            box.mediButton1.Text = btnText[0].ToString();
            box.mediButton2.Text = btnText[1].ToString();
            if (defalt.ToString() == "Button1")
            { box.ActiveControl = box.mediButton1; }
            else if (defalt.ToString() == "Button2")
            { box.ActiveControl = box.mediButton2; }
            MediSelfAdaption(ref box, text);
            box.ShowDialog(window);
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 是、否、取消弹出框
        /// </summary>
        /// <param name="text"></param>
        /// <param name="defalt"></param>
        /// <returns></returns>
        public static DialogResult YesNoCancel(string text, MessageBoxDefaultButton defalt)
        {
            IsDisposedBox();
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            string path = ImageIco(MediImagesIco.Question.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            MediButtonType(MediButtonShow.YesNoCancel.ToString(), ref box);
            if (defalt.ToString() == "Button1")
            { box.ActiveControl = box.mediButton3; }
            else if (defalt.ToString() == "Button2")
            { box.ActiveControl = box.mediButton1; }
            else if (defalt.ToString() == "Button3")
            { box.ActiveControl = box.mediButton2; }
            MediSelfAdaption(ref box, text);
            if (TopWindow != null && TopWindow is Form frm && !frm.IsDisposed)
                box.ShowDialog(TopWindow);
            else
            {
                if (HISClientHelper.MainForm != null)
                    box.ShowDialog(HISClientHelper.MainForm);
                else
                {
                    box.StartPosition = FormStartPosition.CenterScreen;
                    box.ShowDialog();
                }
            }
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 是、否、取消弹出框重载伴随主窗体
        /// </summary>
        /// <param name="window"></param>
        /// <param name="text"></param>
        /// <param name="defalt"></param>
        /// <returns></returns>
        public static DialogResult YesNoCancel(IWin32Window window, string text, MessageBoxDefaultButton defalt)
        {
            IsDisposedBox();
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            string path = ImageIco(MediImagesIco.Question.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            MediButtonType(MediButtonShow.YesNoCancel.ToString(), ref box);
            if (defalt.ToString() == "Button1")
            { box.ActiveControl = box.mediButton3; }
            else if (defalt.ToString() == "Button2")
            { box.ActiveControl = box.mediButton1; }
            else if (defalt.ToString() == "Button3")
            { box.ActiveControl = box.mediButton2; }
            MediSelfAdaption(ref box, text);
            box.ShowDialog(window);
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 自定义是、否、取消按钮显示内容弹出框
        /// </summary>
        /// <param name="text"></param>
        /// <param name="btnText"></param>
        /// <param name="defalt"></param>
        /// <returns></returns>
        public static DialogResult YesNoCancel(string text, string[] btnText, MessageBoxDefaultButton defalt)
        {
            IsDisposedBox();
            MediMsgBox box = new MediMsgBox();
            MediParameter.bluess = true;
            box.mediLabel1.Text = text;
            string path = ImageIco(MediImagesIco.Question.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            box.mediButton3.Text = btnText[0].ToString();
            box.mediButton1.Text = btnText[1].ToString();
            box.mediButton2.Text = btnText[2].ToString();
            if (defalt.ToString() == "Button1")
            { box.ActiveControl = box.mediButton3; }
            else if (defalt.ToString() == "Button2")
            { box.ActiveControl = box.mediButton1; }
            else if (defalt.ToString() == "Button3")
            { box.ActiveControl = box.mediButton2; }
            MediSelfAdaption(ref box, text);
            if (TopWindow != null && TopWindow is Form frm && !frm.IsDisposed)
                box.ShowDialog(TopWindow);
            else
            {
                if (HISClientHelper.MainForm != null)
                    box.ShowDialog(HISClientHelper.MainForm);
                else
                {
                    box.StartPosition = FormStartPosition.CenterScreen;
                    box.ShowDialog();
                }
            }
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 自定义是、否、取消按钮显示内容弹出框重载伴随主窗体
        /// </summary>
        /// <param name="window"></param>
        /// <param name="text"></param>
        /// <param name="btnText"></param>
        /// <param name="defalt"></param>
        /// <returns></returns>
        public static DialogResult YesNoCancel(IWin32Window window, string text, string[] btnText, MessageBoxDefaultButton defalt)
        {
            IsDisposedBox();
            MediMsgBox box = new MediMsgBox();
            MediParameter.bluess = true;
            box.mediLabel1.Text = text;
            string path = ImageIco(MediImagesIco.Question.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            box.mediButton3.Text = btnText[0].ToString();
            box.mediButton1.Text = btnText[1].ToString();
            box.mediButton2.Text = btnText[2].ToString();
            if (defalt.ToString() == "Button1")
            { box.ActiveControl = box.mediButton3; }
            else if (defalt.ToString() == "Button2")
            { box.ActiveControl = box.mediButton1; }
            else if (defalt.ToString() == "Button3")
            { box.ActiveControl = box.mediButton2; }
            MediSelfAdaption(ref box, text);
            box.ShowDialog(window);
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 保存、不保存、取消弹出框
        /// </summary>
        /// <param name="text"></param>
        /// <param name="defalt"></param>
        /// <returns></returns>
        public static DialogResult SaveNotSaveCancel(string text, MessageBoxDefaultButton defalt)
        {
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            string path = ImageIco(MediImagesIco.Question.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            MediButtonType(MediButtonShow.SaveNotSaveCancel.ToString(), ref box);
            if (defalt.ToString() == "Button1")
            { box.ActiveControl = box.mediButton3; }
            else if (defalt.ToString() == "Button2")
            { box.ActiveControl = box.mediButton1; }
            else if (defalt.ToString() == "Button3")
            { box.ActiveControl = box.mediButton2; }
            MediSelfAdaption(ref box, text);
            if (TopWindow != null && TopWindow is Form frm && !frm.IsDisposed)
                box.ShowDialog(TopWindow);
            else
            {
                if (HISClientHelper.MainForm != null)
                    box.ShowDialog(HISClientHelper.MainForm);
                else
                {
                    box.StartPosition = FormStartPosition.CenterScreen;
                    box.ShowDialog();
                }
            }
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 保存、不保存、取消弹出框重载伴随主窗体
        /// </summary>
        /// <param name="window"></param>
        /// <param name="text"></param>
        /// <param name="defalt"></param>
        /// <returns></returns>
        public static DialogResult SaveNotSaveCancel(IWin32Window window, string text, MessageBoxDefaultButton defalt)
        {
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            string path = ImageIco(MediImagesIco.Question.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            MediButtonType(MediButtonShow.SaveNotSaveCancel.ToString(), ref box);
            if (defalt.ToString() == "Button1")
            { box.ActiveControl = box.mediButton3; }
            else if (defalt.ToString() == "Button2")
            { box.ActiveControl = box.mediButton1; }
            else if (defalt.ToString() == "Button3")
            { box.ActiveControl = box.mediButton2; }
            MediSelfAdaption(ref box, text);
            box.ShowDialog(window);
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 警告弹出框
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DialogResult Warn(string text)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            box.mediButton1.Visible = false;
            box.mediButton2.Visible = false;
            box.mediPanelControl4.Visible = false;
            box.mediPanelControl5.Visible = false;
            string path = ImageIco(MediImagesIco.Warning.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            box.mediButton3.Text = "确定";
            MediSelfAdaption(ref box, text);
            if (TopWindow != null && TopWindow is Form frm && !frm.IsDisposed)
                box.ShowDialog(TopWindow);
            else
            {
                if (HISClientHelper.MainForm != null)
                    box.ShowDialog(HISClientHelper.MainForm);
                else
                {
                    box.StartPosition = FormStartPosition.CenterScreen;
                    box.ShowDialog();
                }
            }
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 警告弹出框重载伴随主窗体
        /// </summary>
        /// <param name="window"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DialogResult Warn(IWin32Window window, string text)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            box.mediButton1.Visible = false;
            box.mediButton2.Visible = false;
            box.mediPanelControl4.Visible = false;
            box.mediPanelControl5.Visible = false;
            string path = ImageIco(MediImagesIco.Warning.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            box.mediButton3.Text = "确定";
            MediSelfAdaption(ref box, text);
            box.ShowDialog(window);
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 包含详细警告信息弹出窗体
        /// </summary>
        /// <param name="text">文本内容</param>
        /// <param name="errMsg">详细错误细信息</param>
        /// <param name="iserrMsg">是否显示详细错误信息</param>
        /// <returns></returns>
        public static DialogResult Warn(string text, string errMsg, bool iserrMsg = true)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            MediButtonType(MediButtonShow.YesInformation.ToString(), ref box);
            string path = ImageIco(MediImagesIco.Warning.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            box.ActiveControl = box.mediButton1;
            if (iserrMsg)
                box.mediLabel1.Text = text + "\r\n" + errMsg;//加载错误信息
            else
                box.mediLabel1.Text = text + "\r\n" + "请查看详细错误信息......";
            MediParameter.Information = errMsg;
            MediParameter.width = box.Width;
            MediParameter.frmheight = box.Height;
            MediParameter.txtheight = box.panelControl1.Height;
            MediParameter.height = MediParameter.frmheight + MediParameter.txtheight;
            MediSelfAdaption(ref box, errMsg);
            if (TopWindow != null && TopWindow is Form frm && !frm.IsDisposed)
                box.ShowDialog(TopWindow);
            else
            {
                if (HISClientHelper.MainForm != null)
                    box.ShowDialog(HISClientHelper.MainForm);
                else
                {
                    box.StartPosition = FormStartPosition.CenterScreen;
                    box.ShowDialog();
                }
            }
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 自定义警告按钮内容弹出框
        /// </summary>
        /// <param name="text"></param>
        /// <param name="btnText"></param>
        /// <returns></returns>
        public static DialogResult Warn(string text, string btnText)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            box.mediButton1.Visible = false;
            box.mediButton2.Visible = false;
            box.mediPanelControl4.Visible = false;
            box.mediPanelControl5.Visible = false;
            string path = ImageIco(MediImagesIco.Warning.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            box.mediButton3.Text = btnText;
            MediSelfAdaption(ref box, text);
            if (TopWindow != null && TopWindow is Form frm && !frm.IsDisposed)
                box.ShowDialog(TopWindow);
            else
            {
                if (HISClientHelper.MainForm != null)
                    box.ShowDialog(HISClientHelper.MainForm);
                else
                {
                    box.StartPosition = FormStartPosition.CenterScreen;
                    box.ShowDialog();
                }
            }
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 自定义警告按钮内容弹出框重载伴随主窗体
        /// </summary>
        /// <param name="window"></param>
        /// <param name="text"></param>
        /// <param name="btnText"></param>
        /// <returns></returns>
        public static DialogResult Warn(IWin32Window window, string text, string btnText)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            box.mediButton1.Visible = false;
            box.mediButton2.Visible = false;
            box.mediPanelControl4.Visible = false;
            box.mediPanelControl5.Visible = false;
            string path = ImageIco(MediImagesIco.Warning.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            box.mediButton3.Text = btnText;
            MediSelfAdaption(ref box, text);
            box.ShowDialog(window);
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }
        /// <summary>
        /// 包含详细警告信息弹出窗体
        /// </summary>
        /// <param name="window"></param>
        /// <param name="text">文本内容</param>
        /// <param name="errMsg">详细错误细信息</param>
        /// <param name="iserrMsg">是否显示详细错误信息</param>
        /// <returns></returns>
        public static DialogResult Warn(IWin32Window window, string text, string errMsg, bool iserrMsg = true)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            MediButtonType(MediButtonShow.YesInformation.ToString(), ref box);
            string path = ImageIco(MediImagesIco.Warning.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            box.ActiveControl = box.mediButton1;
            if (iserrMsg)
                box.mediLabel1.Text = text + "\r\n" + errMsg;//加载错误信息
            else
                box.mediLabel1.Text = text + "\r\n" + "请查看详细错误信息......";
            MediParameter.Information = errMsg;
            MediParameter.width = box.Width;
            MediParameter.frmheight = box.Height;
            MediParameter.txtheight = box.panelControl1.Height;
            MediParameter.height = MediParameter.frmheight + MediParameter.txtheight;
            MediSelfAdaption(ref box, errMsg);
            box.ShowDialog(window);
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }
        /// <summary>
        /// 信息显示弹出框
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DialogResult Info(string text)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            box.mediButton1.Visible = false;
            box.mediButton2.Visible = false;
            box.mediPanelControl4.Visible = false;
            box.mediPanelControl5.Visible = false;
            string path = ImageIco(MediImagesIco.Information.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            box.mediButton3.Text = "确定";
            MediSelfAdaption(ref box, text);
            if (TopWindow != null && TopWindow is Form frm && !frm.IsDisposed)
                box.ShowDialog(TopWindow);
            else
            {
                if (HISClientHelper.MainForm != null)
                    box.ShowDialog(HISClientHelper.MainForm);
                else
                {
                    box.StartPosition = FormStartPosition.CenterScreen;
                    box.ShowDialog();
                }
            }
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 信息显示弹出框重载伴随主窗体
        /// </summary>
        /// <param name="window"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DialogResult Info(IWin32Window window, string text)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            box.mediButton1.Visible = false;
            box.mediButton2.Visible = false;
            box.mediPanelControl4.Visible = false;
            box.mediPanelControl5.Visible = false;
            string path = ImageIco(MediImagesIco.Information.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            box.mediButton3.Text = "确定";
            MediSelfAdaption(ref box, text);
            box.ShowDialog(window);
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 自定义信息按钮显示内容弹出框
        /// </summary>
        /// <param name="text"></param>
        /// <param name="btnText"></param>
        /// <returns></returns>
        public static DialogResult Info(string text, string btnText)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            box.mediButton1.Visible = false;
            box.mediButton2.Visible = false;
            box.mediPanelControl4.Visible = false;
            box.mediPanelControl5.Visible = false;
            string path = ImageIco(MediImagesIco.Information.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            box.mediButton3.Text = btnText;
            MediSelfAdaption(ref box, text);
            if (TopWindow != null && TopWindow is Form frm && !frm.IsDisposed)
                box.ShowDialog(TopWindow);
            else
            {
                if (HISClientHelper.MainForm != null)
                    box.ShowDialog(HISClientHelper.MainForm);
                else
                {
                    box.StartPosition = FormStartPosition.CenterScreen;
                    box.ShowDialog();
                }
            }
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        /// <summary>
        /// 自定义信息按钮显示内容弹出框重载伴随主窗体
        /// </summary>
        /// <param name="window"></param>
        /// <param name="text"></param>
        /// <param name="btnText"></param>
        /// <returns></returns>
        public static DialogResult Info(IWin32Window window, string text, string btnText)
        {
            if (!CheckExistDialogWindow()) { return DialogResult.Abort; }
            MediMsgBox box = new MediMsgBox();
            box.mediLabel1.Text = text;
            box.mediButton1.Visible = false;
            box.mediButton2.Visible = false;
            box.mediPanelControl4.Visible = false;
            box.mediPanelControl5.Visible = false;
            string path = ImageIco(MediImagesIco.Information.ToString());
            if (!string.IsNullOrEmpty(path))
                box.mediPictureEdit1.Image = Image.FromFile(path);
            else
                box.mediPictureEdit1.Visible = false;
            box.mediButton3.Text = btnText;
            MediSelfAdaption(ref box, text);
            box.ShowDialog(window);
            box.Dispose();
            return MediResultButton(MediParameter.Operation);
        }

        #endregion

        #endregion

        #region 相关方法

        /// <summary>
        /// 错误信息提取
        /// </summary>
        /// <param name="str">错误内容</param>
        /// <returns></returns>
        private static string ErrorInfoExtraction(string str)
        {
            Match match = Regex.Match(str, "(?i:ORA-).*?(?=\",)");
            if (match.Success)
            {
                return match.Value.Replace("\\n", "").Replace("\\", "") + " ";
            }
            return "";
        }

        /// <summary>
        /// 设置图片类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ImageIco(string str)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\').LastIndexOf('\\')) + @"\PIC\";
            switch (str)
            {
                case "Hand":
                    str = @"icon_error.png";
                    break;
                case "Stop":
                    str = @"icon_error.png";
                    break;
                case "Error":
                    str = @"icon_error.png";
                    break;
                case "Question":
                    str = @"icon_help.png";
                    break;
                case "Exclamation":
                    str = @"icon_success.png";
                    break;
                case "Warning":
                    str = @"icon_warn.png";
                    break;
                case "Asterisk":
                    str = @"icon_normal.png";
                    break;
                case "Information":
                    str = @"icon_normal.png";
                    break;
            }
            if (!Directory.Exists(path) || !FindFile(path, str))
            {
                if (!FindFile(AppDomain.CurrentDomain.BaseDirectory, str))
                    return "";
                else
                    return AppDomain.CurrentDomain.BaseDirectory + str;
            }
            else
                return path + str;
        }

        /// <summary>
        /// 遍历文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static bool FindFile(string path, string fileName)
        {
            bool bol = false;
            DirectoryInfo TheFolder = new DirectoryInfo(path);
            //遍历文件
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                if (NextFile.Name == fileName)
                    bol = true;
            }

            return bol;
        }

        /// <summary>
        /// 设置图片停靠方式
        /// </summary>
        /// <param name="str"></param>
        /// <param name="box"></param>
        public static void MediStyle(string str, ref MediMsgBox box)
        {
            try
            {
                switch (str)
                {
                    case "RightAlign":
                        box.RightToLeft = RightToLeft.Yes;
                        break;
                    case "RtlReading":
                        box.RightToLeft = RightToLeft.No;
                        break;
                    case "ServiceNotification":
                        box.ShowInTaskbar = true;
                        break;
                }
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// 设置按钮文字
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="box"></param>
        public static void MediButtonType(string btn, ref MediMsgBox box)
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
                        box.mediButton2.Text = "详细信息";
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
                        box.mediButton1.Text = "是(&Y)";
                        box.mediButton2.Text = "否(&N)";
                        box.mediButton3.Text = "取消";
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
                    case "":
                        box.mediButton2.Text = "取消";
                        box.mediButton3.Text = "保存(&S)";
                        box.mediButton1.Text = "不保存(&N)";
                        buttonShow = MediButtonShow.SaveNotSaveCancel;
                        break;
                }
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// 按钮返回结果
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static DialogResult MediResultButton(string txt)
        {
            DialogResult result = new DialogResult();
            // 设置默认值(应用于关闭提示框)
            switch (buttonShow)
            {
                case MediButtonShow.YesNo:
                    break;
                case MediButtonShow.YesInformation:
                    break;
                case MediButtonShow.YesNoHelp:
                    break;
                case MediButtonShow.RetryCancel:
                    break;
                case MediButtonShow.YesNoCancel:
                    result = DialogResult.Cancel;
                    break;
                case MediButtonShow.AbortRetryIgnore:
                    break;
                case MediButtonShow.OKCancel:
                    break;
                case MediButtonShow.SaveNotSaveCancel:
                    break;
                default:
                    break;
            }


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
            }
            if (result == DialogResult.None)
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
                    case "None":
                        result = DialogResult.None;
                        break;
                }
            }
            return result;
        }

        /// <summary>
        /// 动态计算文字换行，显示方式(目前已弃用)
        /// </summary>
        /// <param name="box"></param>
        /// <param name="text"></param>
        /// <param name="ss"></param>
        public static void MediSelfAdaption(ref MediMsgBox box, string text, string ss)
        {
            try
            {
                box.mediLabel1.Text = text;
                if (box.mediLabel1.Text.Contains("\r") || box.mediLabel1.Text.Contains("\n"))
                {
                    string[] str = box.mediLabel1.Text.Replace("\n", "").Split('\r');
                    int rows = 0;
                    string result = string.Empty;
                    int flenth = 0;
                    SizeF sizef = new SizeF();
                    int count = 0;
                    foreach (string s in str)
                    {
                        Font font = new Font("微软雅黑", 10);
                        Graphics grap = box.CreateGraphics();
                        sizef = grap.MeasureString(s, font);

                        if (sizef.Width >= 300)
                        {
                            rows = (int)sizef.Width / 300 + 1;
                            flenth = (int)(s.Length / (sizef.Width / 300));
                            for (int i = 1; i <= rows; i++)
                            {
                                if (string.IsNullOrEmpty(s))
                                    break;
                                else if (s.Length - (i - 1) * flenth > flenth)
                                    result += s.Substring((i - 1) * flenth, flenth) + "\r\n";
                                else
                                    result += s.Substring((i - 1) * flenth, s.Length - (i - 1) * flenth) + "\r\n";
                            }
                            count += rows;
                        }
                        else
                        { result += s.ToString() + "\r\n"; count++; }
                    }
                    box.mediPictureEdit1.Location = new Point(0, 10);
                    box.mediLabel1.Text = "\r\n" + result;
                    if (count >= 3)
                    {
                        box.Height += ((count - 3) * (box.mediLabel1.Height / count));
                        box.mediLabel1.Dock = DockStyle.Left;
                    }
                }
                else if (box.mediLabel1.Width <= (box.mediButton2.Width * 3))
                {
                    if (box.mediButton2.Text.Contains("详细信息") || (box.mediButton2.Visible && box.mediButton3.Visible) || box.mediButton2.Visible && box.mediButton1.Visible)
                    {
                        box.Width = box.mediButton2.Width * 3 + box.mediPictureEdit1.Width;
                        box.mediLabel1.Text = "\r\n" + text;
                    }
                    else if (!box.mediButton1.Visible && !box.mediButton2.Visible && !box.mediButton3.Visible)
                    {
                        box.Width = box.mediButton2.Width * 3 + box.mediPictureEdit1.Width;
                        box.mediLabel1.Text = "\r\n\r\n" + text;
                    }
                    else
                    {
                        box.Width = (box.mediLabel1.Width + 50) + box.mediPictureEdit1.Width;
                        box.mediLabel1.Text = "\r\n\r\n" + text;
                    }

                }
                else
                {
                    SizeF sizef = new SizeF();
                    Font font = new Font("微软雅黑", 10);
                    Graphics grap = box.CreateGraphics();
                    sizef = grap.MeasureString(box.mediLabel1.Text, font);

                    int rows = (int)sizef.Width / 300 + 1;
                    string result = string.Empty;
                    int flenth = (int)(box.mediLabel1.Text.Length / (sizef.Width / 300));
                    for (int i = 1; i <= rows; i++)
                    {
                        if (box.mediLabel1.Text.Length - (i - 1) * flenth >= flenth)
                            result += box.mediLabel1.Text.Substring((i - 1) * flenth, flenth) + "\r\n";
                        else
                            result += box.mediLabel1.Text.Substring((i - 1) * flenth, box.mediLabel1.Text.Length - (i - 1) * flenth);
                    }
                    int x = box.mediPictureEdit1.Width;
                    if (rows == 1)
                        box.mediLabel1.Location = new Point(x, 10);
                    box.mediPictureEdit1.Location = new Point(0, 10);
                    box.mediLabel1.Text = "\r\n" + result;
                    if (rows >= 3)
                    {
                        box.Height += ((rows - 3) * (box.mediLabel1.Height / rows));
                        box.mediLabel1.Dock = DockStyle.Left;
                    }
                }
                MediParameter.width = box.Width;
                MediParameter.frmheight = box.Height;
                MediParameter.txtheight = box.panelControl1.Height;
                MediParameter.height = MediParameter.frmheight + MediParameter.txtheight;
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// 新的文字换行计算方式
        /// </summary>
        /// <param name="box"></param>
        /// <param name="text"></param>
        public static void MediSelfAdaption(ref MediMsgBox box, string text)
        {
            string content = box.mediLabel1.Text.TrimStart('\r').TrimStart('\n');
            if (content.Length > 500)
                box.mediLabel1.Text = content.Substring(0, 500) + ".....";
            else
                box.mediLabel1.Text = box.mediLabel1.Text.TrimStart('\r').TrimStart('\n');
            box.Height = SystemInformation.CaptionHeight + box.mediLabel1.Height + box.mediButton1.Height + box.mediLabel1.Padding.Top;
            MediParameter.width = box.Width;
            MediParameter.frmheight = box.Height;
            MediParameter.txtheight = box.panelControl1.Height;
            MediParameter.height = MediParameter.frmheight + MediParameter.txtheight;
        }

        /// <summary>
        /// 回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediMsgBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Escape)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// 复制功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediMsgBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.C)
                {
                    Clipboard.SetDataObject(this.mediLabel1.Text);
                }
            }
        }

        /// <summary>
        /// 关闭处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediMsgBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MediParameter.ButType == null)
            {
                MediParameter.ButType = "None";
            }
        }

        #endregion
    }

    /// <summary>
    /// 图标枚举
    /// </summary>
    public enum MediImagesIco
    {
        None = 0,
        Hand = 1,
        Stop = 2,
        Error = 3,
        Question = 4,
        Exclamation = 5,
        Warning = 6,
        Asterisk = 7,
        Information = 8
    }

    /// <summary>
    /// 默认显示按钮枚举
    /// </summary>
    public enum MediButtonShow
    {
        /// <summary>
        /// 是否
        /// </summary>
        YesNo = 0,
        /// <summary>
        /// 是详细信息
        /// </summary>
        YesInformation = 1,
        /// <summary>
        /// 是不是帮助
        /// </summary>
        YesNoHelp = 2,
        /// <summary>
        /// 重试取消
        /// </summary>
        RetryCancel = 3,
        /// <summary>
        /// 是不是取消
        /// </summary>
        YesNoCancel = 4,
        /// <summary>
        /// 中止重试忽略
        /// </summary>
        AbortRetryIgnore = 5,
        /// <summary>
        /// 确定取消
        /// </summary>
        OKCancel = 6,
        /// <summary>
        /// 保存不保存取消
        /// </summary>
        SaveNotSaveCancel = 7,

    }

    /// <summary>
    /// 操作变量数据
    /// </summary>
    public class MediParameter
    {
        public static string Information { get; set; }

        public static string HelpPath { get; set; }

        public static string Operation { get; set; }

        public static int width { get; set; }

        public static int frmheight { get; set; }

        public static int txtheight { get; set; }

        public static int height { get; set; }

        public static bool bluess { get; set; }

        public static string ButType { get; set; }
    }
}
