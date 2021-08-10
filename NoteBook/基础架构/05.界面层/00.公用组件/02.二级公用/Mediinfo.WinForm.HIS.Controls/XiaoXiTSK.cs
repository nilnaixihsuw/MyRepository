using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Mediinfo.HIS.Core;
using Mediinfo.Utility.Extensions;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 打开消息处理
    /// </summary>
    public delegate void YzXiaoXiTSKEvent();
    /// <summary>
    /// 消息提示框
    /// </summary>
    public partial class XiaoXiTSK : MediDialog
    {
        /// <summary>
        /// 提示框打开委托处理
        /// </summary>
        public event YzXiaoXiTSKEvent YzXxSLEvent;

        /// <summary>
        /// 倒计时记录数据
        /// </summary>
        private int count = 0;
        //add by hujian@2020/11/24
        /// height 距离屏幕底部高度，嘉兴反应消息挡住了保存按钮。
        private int height = 0;

        #region 参数引用

        [DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        private static int AW_HIDE = 0x00010000;//该变量表示动画隐藏窗体
        private static int AW_SLIDE = 0x00040000;//该变量表示出现滑行效果的窗体
        private static int AW_VER_NEGATIVE = 0x00000008;//该变量表示从下向上开屏
        private static int AW_VER_POSITIVE = 0x00000004;//该变量表示从上向下开屏
        private const int AW_ACTIVE = 0x20000;//激活窗口
        private const int AW_BLEND = 0x80000;//应用淡入淡出结果

        #endregion

        /// <summary>
        /// 构造函数
        /// height 距离屏幕底部高度，嘉兴反应消息挡住了保存按钮。
        /// </summary>
        public XiaoXiTSK(int height = 0)
        {
            InitializeComponent();
            this.height = height;
        }

        /// <summary>
        /// 加载数量
        /// </summary>
        public void InitData()
        {
            this.mediLabel_YZSL.Text = HISClientHelper.initYzTS;
        }

        /// <summary>
        /// 窗体Load事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XiaoXiTSK_Load(object sender, EventArgs e)
        {
            //初始化数据
            InitData();

            int x = Screen.PrimaryScreen.WorkingArea.Right - this.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height;
            this.SetDesktopLocation(x, y - height);//设置窗体在屏幕右下角显示
            AnimateWindow(this.Handle, 1000, AW_SLIDE | AW_ACTIVE | AW_VER_NEGATIVE);
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XiaoXiTSK_Shown(object sender, EventArgs e)
        {            
            //时间倒计时处理            
            this.mediTimer1.Tick += MediTimer1_Tick;
            this.mediTimer1.Start();
        }

        /// <summary>
        /// 时间倒计时处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediTimer1_Tick(object sender, EventArgs e)
        {
            count++;
            if (count == HISClientHelper.timeDate)
            {
                this.mediTimer1.Stop();
                mediButton1_Click(null, null);
            }
        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediBlueButton_CK_Click(object sender, EventArgs e)
        {
            if (!this.IsDisposed && this.Visible)
            {
                YzXxSLEvent();
                HISClientHelper.yzBianDongXXRSTable = new DataTable();
                HISClientHelper.initYzTS = "0";
                this.mediLabel_YZSL.Text = "0";
                AnimateWindow(this.Handle, 1000, AW_BLEND | AW_HIDE);
                this.Close();
                this.Dispose();
            }
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButton1_Click(object sender, EventArgs e)
        {
            if (!this.IsDisposed && this.Visible)
            {
                //AnimateWindow(this.Handle, 1000, AW_BLEND | AW_HIDE);
                this.Close();
                this.Dispose();
            }
        }
    }
}
