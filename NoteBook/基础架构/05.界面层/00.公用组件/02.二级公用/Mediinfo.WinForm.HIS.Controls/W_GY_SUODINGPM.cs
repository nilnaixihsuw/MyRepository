using Mediinfo.Enterprise;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 锁屏窗口
    /// </summary>
    public partial class W_GY_SUODINGPM : MediForm
    {
        #region constructor

        /// <summary>
        /// 锁屏窗口
        /// </summary>
        public W_GY_SUODINGPM()
        {
            InitializeComponent();
        }

        #endregion

        #region fields

        /// <summary>
        /// 解锁事件
        /// </summary>
        public event EventHandler Unlock;

        #endregion

        #region properties

        /// <summary>
        /// 菜单参数
        /// </summary>
        [Browsable(false)]
        public List<object> GongNengCS { get; set; }

        /// <summary>
        /// 菜单id
        /// </summary>
        [Browsable(false)]
        public string CaiDanID { get; set; }

        /// <summary>
        /// 窗口调用参数
        /// </summary>
        [Browsable(false)]
        public string DiaoYongCS { get; set; }

        #endregion

        #region events

        /// <summary>
        /// 初始化加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void W_GY_SUODINGPM_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            this.Text = ButtonForOpenChuangKou.GlobalClientMainForm.Text;
            currentSystemStateInfo.Text = HISClientHelper.YINGYONGMC + "  已被锁定";
            mediUsertb.Text = HISClientHelper.USERID;
        }

        /// <summary>
        /// 解锁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediBtnUnlock_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.mediUsertb.Text) && !string.IsNullOrWhiteSpace(this.mediPasswordtb.Text))
            {
                JCJGLoginService jCJGLoginService = new JCJGLoginService();
                var result = jCJGLoginService.JieSuoJY(this.mediUsertb.Text.Trim(), this.mediPasswordtb.Text.Trim());
                if (result.ReturnCode == ReturnCode.SUCCESS)
                {
                    bool jiaoyan = result.Return;
                    if (jiaoyan)
                    {
                        ButtonForOpenChuangKou.GlobalClientMainForm.IsLockScreen = false;
                        ButtonForOpenChuangKou.GlobalClientMainForm.Show();
                        this.Close();
                        this.Dispose();

                        Unlock?.Invoke(this, e);
                        return;
                    }
                }
            }

            MediMsgBox.Warn("密码不正确!");
            this.mediPasswordtb.Text = string.Empty;
            this.mediPasswordtb.Focus();
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediBtnExit_Click(object sender, EventArgs e)
        {
            if (MediMsgBox.Show(this, "强制退出未保存的数据将会丢失!\r\n确定退出系统?", "联众智慧", MediButtonShow.OKCancel) == DialogResult.OK)
            {
                ButtonForOpenChuangKou.GlobalClientMainForm.IsLockScreen = false;
                ButtonForOpenChuangKou.GlobalClientMainForm.Close();
                ButtonForOpenChuangKou.GlobalClientMainForm.Dispose();
                Application.Exit();
            }
            else
            {
                ButtonForOpenChuangKou.GlobalClientMainForm.IsLockScreen = true;
            }
        }

        /// <summary>
        /// 用户名键盘键被按下时触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediUsertb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
                mediPasswordtb.Focus();
        }

        #endregion

        #region override

        /// <summary>
        /// 处理Windows消息
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            // 拦截双击标题栏、移动窗体的系统消息  
            if (m.Msg != 0xA3 && m.Msg != 0x0003 && m.WParam != (IntPtr)0xF012)
            {
                base.WndProc(ref m);
            }
        }

        #endregion
    }
}