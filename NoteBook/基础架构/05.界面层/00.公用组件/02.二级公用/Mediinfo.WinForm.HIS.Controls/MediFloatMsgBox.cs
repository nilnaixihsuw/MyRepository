using Mediinfo.HIS.Core;

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{/// <summary>
/// 浮动显示提示信息的窗口
/// </summary>
    public partial class MediFloatMsgBox : Form
    {
        /// <summary>
        /// win32类,用于支持窗口动画
        /// </summary>
        public class MsgWin32Animation
        {
            public const Int32 AW_HOR_POSITIVE = 0x00000001;    // 从左到右打开窗口
            public const Int32 AW_HOR_NEGATIVE = 0x00000002;    // 从右到左打开窗口
            public const Int32 AW_VER_POSITIVE = 0x00000004;    // 从上到下打开窗口
            public const Int32 AW_VER_NEGATIVE = 0x00000008;    // 从下到上打开窗口
            public const Int32 AW_CENTER = 0x00000010;          // 若使用了AW_HIDE标志，则使窗口向内重叠；若未使用AW_HIDE标志，则使窗口向外扩展。
            public const Int32 AW_HIDE = 0x00010000;            // 隐藏窗口，缺省则显示窗口。
            public const Int32 AW_ACTIVATE = 0x00020000;        // 激活窗口。在使用了AW_HIDE标志后不要使用这个标志。
            public const Int32 AW_SLIDE = 0x00040000;           // 使用滑动类型。缺省则为滚动动画类型。当使用AW_CENTER标志时，这个标志就被忽略。
            public const Int32 AW_BLEND = 0x00080000;           // 使用淡出效果。只有当hWnd为顶层窗口的时候才可以使用此标志。

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern bool AnimateWindow(
                IntPtr hwnd, // handle to window
                int dwTime, // duration of animation
                int dwFlags // animation type
                  );
        }

        public MediFloatMsgBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 指定显示信息,并规定时间内自动关闭
        /// </summary>
        /// <param name="text">需要显示的提示文字</param>
        /// <param name="seconds">指定的自动关闭时间</param>
        public MediFloatMsgBox(string text, int seconds)
        {
            InitializeComponent();

            this.Location = new Point() { X = (HISClientHelper.MainForm.Width - this.Width) / 2, Y = HISClientHelper.MainForm.Height - this.Height - 30 };
            //mediMsgText.BackColor = Color.FromArgb(0, 122, 204);
            //mediMsgText.Properties.Appearance.BackColor = Color.FromArgb(0, 122, 204);
            //mediMsgText.Properties.Appearance.BackColor2 = Color.FromArgb(0, 122, 204);
            //mediMsgText.Properties.Appearance.Options.UseBackColor = true;
            //mediMsgText.ReadOnly = true;
            this.BackColor = Color.FromArgb(0, 122, 204);   // 设置蓝底
            this.TopMost = true;        // 设置最高层显示
            mediMsgText.Text = text;
            mediTimer1.Interval = seconds * 1000;   // 设置自动关闭时间
            mediTimer1.Start();
        }

        private void MediTimer1_Tick(object sender, EventArgs e)
        {
            Close();
        }

        private void MediFloatMsgBox_Load(object sender, EventArgs e)
        {
            MsgWin32Animation.AnimateWindow(this.Handle, 500, MsgWin32Animation.AW_BLEND);
        }
    }
}
