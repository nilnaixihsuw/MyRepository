using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class MediPromptBox : Form
    {
        private string tiShiBT = string.Empty;
        private string tiShiNR = string.Empty;
        private string isImage = string.Empty;
        private double shiJian = 2000;
        private System.Timers.Timer t = null;

        public MediPromptBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="topTS">提示信息</param>
        /// <param name="buttomTS">提示内容</param>
        /// <param name="image">图片显示类型(0、不显示;1、成功;2、失败.)</param>
        /// <param name="time">关闭时间，默认2秒，传入毫秒数据(1秒等于1000毫秒)</param>
        public MediPromptBox(string topTS, string buttomTS, string image, double time = 2000)
        {
            InitializeComponent();
            tiShiBT = topTS;
            tiShiNR = buttomTS;
            isImage = image;
            shiJian = time;

            //处理窗体高度
            List<string> tiShiBTList = GetSeparateSubString(tiShiBT, 16);
            List<string> tiShiNRList = GetSeparateSubString(tiShiNR, 16);
            if (tiShiBTList.Count >= 2)
            {
                this.Height += (tiShiBTList.Count - 2) * 25;
                this.Height += tiShiNRList.Count * 25;
            }
            else if (tiShiNRList.Count >= 2)
            {
                this.Height += (tiShiNRList.Count - 2) * 25;
            }
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediPromptBox_Load(object sender, EventArgs e)
        {
            SetWindowRegion();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediPromptBox_Shown(object sender, EventArgs e)
        {
            t = new System.Timers.Timer(shiJian);   //实例化Timer类，设置间隔时间为10000毫秒；
            t.Elapsed += new System.Timers.ElapsedEventHandler(theout); //到达时间的时候执行事件；   
            t.AutoReset = false;   //设置是执行一次（false）还是一直执行(true)；   
            t.Enabled = true;     //是否执行System.Timers.Timer.Elapsed事件；  
            //设置不透明颜色
            this.BackColor = Color.FromArgb(38, 38, 38);
        }

        /// <summary>
        /// 关闭弹窗
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void theout(object source, System.Timers.ElapsedEventArgs e)
        {
            if (this.IsHandleCreated)
            {
                if (!this.IsDisposed)
                {
                    Invoke(new MethodInvoker(delegate ()
                    {
                        t.Stop();
                        t.Dispose();
                        this.Close();
                    }));
                }
            }
        }

        /// <summary>
        /// 页面重绘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediPanelControl1_Paint(object sender, PaintEventArgs e)
        {
            float height = 0;
            Image img = null;
            //if (!tiShiNR.Contains("|"))
            if (tiShiBT.Length <= 16)
            {
                if (isImage == "3")
                    e.Graphics.DrawImage(Properties.Resources.tip, 28, 22);
                else if (isImage == "0" || isImage == "1")
                    e.Graphics.DrawImage(Properties.Resources.tick, 28, 22);
                else if (isImage == "2")
                    e.Graphics.DrawImage(Properties.Resources.cross, 28, 22);
                if (!string.IsNullOrEmpty(tiShiBT))
                    e.Graphics.DrawString(tiShiBT, new Font("微软雅黑", 12), Brushes.White, new PointF(62, 24));
            }
            else
            {
                List<string> strList = new List<string>();
                if (tiShiBT.Length > 16)
                {
                    strList = GetSeparateSubString(tiShiBT, 16);
                }
                else
                {
                    strList.Add(tiShiBT);
                }
                int index = 0;
                float stringX = 0;
                if (isImage == "3")
                    e.Graphics.DrawImage(Properties.Resources.tip, 16, 24);
                else if (isImage == "0" || isImage == "1")
                    e.Graphics.DrawImage(Properties.Resources.tick, 16, 24);
                else if (isImage == "2")
                    e.Graphics.DrawImage(Properties.Resources.cross, 16, 24);
                foreach (string str in strList)
                {
                    LabelControl lab = new LabelControl();
                    lab.Text = str;
                    Font f = lab.Font;
                    SizeF z = e.Graphics.MeasureString(lab.Text, f);

                    if (index > 0 && index < 2)
                    {
                        height += z.Height;
                    }
                    else if (index >= 2)
                    {
                        height += z.Height;
                    }
                    else
                    {
                        stringX = (this.Width - z.Width) / 2;
                    }
                    index++;
                    e.Graphics.DrawString(str, new Font("微软雅黑", 12), Brushes.White, new PointF(stringX, 24 + height));
                }
            }
            if (!string.IsNullOrEmpty(tiShiNR))
            {
                List<string> strList = new List<string>();
                if (tiShiNR.Length > 16)
                {
                    strList = GetSeparateSubString(tiShiNR, 16);
                }

                if (tiShiNR.Contains("|"))
                {
                    string[] str = tiShiNR.Split('|');
                    int index = 0;
                    foreach (string s in str)
                    {
                        LabelControl lab = new LabelControl();
                        lab.Text = s;
                        Font f = lab.Font;
                        SizeF z = e.Graphics.MeasureString(lab.Text, f);

                        if (index > 0)
                        {
                            height += z.Height;
                        }
                        else if (index >= 2)
                        {
                            height += z.Height;
                        }
                        if (!string.IsNullOrEmpty(tiShiBT))
                            e.Graphics.DrawString(s, new Font("微软雅黑", 10), Brushes.White, new PointF(62, 47 + height));
                        else
                            e.Graphics.DrawString(s, new Font("微软雅黑", 10), Brushes.White, new PointF(62, 24 + height));
                        index++;
                    }
                }
                else if (strList.Count > 0)
                {
                    int index = 0;
                    foreach (string str in strList)
                    {
                        LabelControl lab = new LabelControl();
                        lab.Text = str;
                        Font f = lab.Font;
                        SizeF z = e.Graphics.MeasureString(lab.Text, f);

                        if (index > 0 && index < 2)
                        {
                            height += z.Height;
                        }
                        else if (index >= 2)
                        {
                            height += z.Height;
                        }
                        if (!string.IsNullOrEmpty(tiShiBT))
                            e.Graphics.DrawString(str, new Font("微软雅黑", 10), Brushes.White, new PointF(62, 47 + height));
                        else
                            e.Graphics.DrawString(str, new Font("微软雅黑", 10), Brushes.White, new PointF(62, 24 + height));
                        index++;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(tiShiBT))
                        e.Graphics.DrawString(tiShiNR, new Font("微软雅黑", 10), Brushes.White, new PointF(62, 47));
                    else
                        e.Graphics.DrawString(tiShiNR, new Font("微软雅黑", 10), Brushes.White, new PointF(62, 24));
                }
            }
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="txtString"></param>
        /// <param name="charNumber">为要截取的每段的长度</param>
        /// <returns></returns>
        private List<string> GetSeparateSubString(string txtString, int charNumber)
        {
            List<string> arrlist = new List<string>();
            string tempStr = txtString;
            for (int i = 0; i < tempStr.Length; i += charNumber)//首先判断字符串的长度，循环截取，进去循环后首先判断字符串是否大于每段的长度
            {
                arrlist.Add((tempStr.Length - i) > charNumber
                    ? tempStr.Substring(i, charNumber)
                    : tempStr.Substring(i));
            }
            return arrlist;
        }

        #region 圆角处理

        /// <summary>
        /// 
        /// </summary>
        public void SetWindowRegion()
        {
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            var FormPath = GetRoundedRectPath(rect);
            this.Region = new Region(FormPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect">窗体大小</param>
        /// <returns></returns>
        private GraphicsPath GetRoundedRectPath(Rectangle rect)
        {
            int diameter = 20;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();

            path.AddArc(arcRect, 180, 90);//左上角

            arcRect.X = rect.Right - diameter;//右上角
            path.AddArc(arcRect, 270, 90);

            arcRect.Y = rect.Bottom - diameter;// 右下角
            path.AddArc(arcRect, 0, 90);

            arcRect.X = rect.Left;// 左下角
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();
            return path;
        }

        #endregion
    }
}
