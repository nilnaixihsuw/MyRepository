using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraLayout;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Mediinfo.WinForm.HIS.Core;

namespace Mediinfo.WinForm.HIS.Controls.Message
{
    // 声明委托 和 事件
    public delegate void retxiaoXiYD(HISMessageBody xiaoxXiMessage);
    public delegate void retxiaoXiID(HISMessageBody xiaoxXiMessage);

    public partial class MediLayoutXiaoXiZS : MediUserControl
    {
        public event retxiaoXiID retMsgEvent;
        public event retxiaoXiYD retxiaoXiYDEvent;
        private JCJGXiaoXiService xiaoXiService = new JCJGXiaoXiService();
        private HISMessageBody xiaoXi = new HISMessageBody();

        public MediLayoutXiaoXiZS()
        {
            InitializeComponent();
        }

        public MediLayoutXiaoXiZS(HISMessageBody hisMessage)
        {
            InitializeComponent();
            XiaoXiNRJZ(hisMessage);
            xiaoXi = hisMessage;
        }

        private void XiaoXiNRJZ(HISMessageBody hisMessage)
        {
            this.SuspendLayout();
            this.mediLabel1.Text = "●";
            this.mediLabel2.Text = hisMessage.XiaoXiMc;
            this.mediLabel3.Text = hisMessage.XiaoXiJc;
            this.mediLabel4.ToolTipTitle = hisMessage.XiaoXiID + "|" + hisMessage.XiaoXiBM + "|" + hisMessage.BaoMiXxBz;
            this.mediLabel4.Text = "";
            this.mediLabel5.Text = hisMessage.XiaoXiBT;
            this.mediLabel6.Text = hisMessage.FaSongSj.ToString("yy/MM/dd");

            if (hisMessage.XiaoXiJc == "危急")
            {
                this.mediLabel1.ForeColor = Color.FromArgb(248, 024, 038);
                this.mediLabel2.ForeColor = Color.FromArgb(248, 024, 038);
                this.mediLabel3.BackColor = Color.FromArgb(255, 153, 000);
                this.mediLabel3.ForeColor = Color.White;
            }
            else if (hisMessage.XiaoXiJc == "通知")
            {
                this.mediLabel1.ForeColor = Color.FromArgb(204, 204, 204);
            }
            else
            {
                this.mediLabel1.ForeColor = Color.FromArgb(255, 153, 000);
            }

            Font f = this.mediLabel1.Font;
            Graphics g = this.CreateGraphics();
            SizeF z = g.MeasureString(this.mediLabel1.Text, f);
            this.layoutControlItem1.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem1.MinSize = new System.Drawing.Size() { Height = Convert.ToInt32(z.Height), Width = Convert.ToInt32(z.Width) };
            this.layoutControlItem1.MaxSize = new System.Drawing.Size() { Height = Convert.ToInt32(z.Height), Width = Convert.ToInt32(z.Width) };

            this.layoutControlItem4.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem4.MinSize = new System.Drawing.Size() { Height = Convert.ToInt32(z.Height), Width = Convert.ToInt32(z.Width) };
            this.layoutControlItem4.MaxSize = new System.Drawing.Size() { Height = Convert.ToInt32(z.Height), Width = Convert.ToInt32(z.Width) };

            Font f1 = this.mediLabel3.Font;
            Graphics g1 = this.CreateGraphics();
            SizeF z1 = g1.MeasureString(this.mediLabel3.Text, f1);
            this.layoutControlItem3.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem3.MinSize = new System.Drawing.Size() { Height = Convert.ToInt32(z1.Height), Width = Convert.ToInt32(z1.Width) };
            this.layoutControlItem3.MaxSize = new System.Drawing.Size() { Height = Convert.ToInt32(z1.Height), Width = Convert.ToInt32(z1.Width) };

            Font f2 = this.mediLabel6.Font;
            Graphics g2 = this.CreateGraphics();
            SizeF z2 = g2.MeasureString(this.mediLabel6.Text, f2);
            this.layoutControlItem6.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem6.MinSize = new System.Drawing.Size() { Height = Convert.ToInt32(z2.Height), Width = Convert.ToInt32(z2.Width) };
            this.layoutControlItem6.MaxSize = new System.Drawing.Size() { Height = Convert.ToInt32(z2.Height), Width = Convert.ToInt32(z2.Width) };

            this.ResumeLayout(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediLabel2_MouseClick(object sender, MouseEventArgs e)
        {
            string[] xiaoXiID = this.mediLabel4.ToolTipTitle.Split('|');
            if (xiaoXiID[2] == "1")
            {
                using (ChaKanXXRZ rzFrom = new ChaKanXXRZ())
                {
                    rzFrom.ShowDialog();
                    if (!rzFrom.RenZhenJG)
                        return;
                }
            }
            var xiaoXiData = xiaoXiService.GetYiDuXX(HISClientHelper.ZAIXIANZTID, HISClientHelper.GetSysDate());
            if (xiaoXiData.ReturnCode == Enterprise.ReturnCode.SUCCESS)
            {
                if (!xiaoXiData.Return.Where(p => p.XiaoXiID == xiaoXiID[0]).Any())
                {
                    if (HISClientHelper.XiaoXiRightNR.ContainsKey(xiaoXi.XiaoXiBM))
                    {
                        if (HISClientHelper.XiaoXiRightNR[xiaoXi.XiaoXiBM].Split('|')[1] == "0" || string.IsNullOrEmpty(HISClientHelper.XiaoXiRightNR[xiaoXi.XiaoXiBM].Split('|')[1]))
                        {
                            var yueDuXX = xiaoXiService.YueDuXiaoXi(Convert.ToInt32(xiaoXiID[0]), HISClientHelper.USERID);
                        }
                    }
                }
            }

            if (HISClientHelper.XIAOXIYDNR.Where(p => p.XiaoXiID == xiaoXiID[0] && p.XiaoXiBM == xiaoXiID[1]).Any())
            {
                HISMessageBody message = HISClientHelper.XIAOXIYDNR.Where(p => p.XiaoXiID == xiaoXiID[0] && p.XiaoXiBM == xiaoXiID[1]).ToList()[0];
                retxiaoXiYDEvent?.Invoke(message);
            }
            else
            {
                HISMessageBody message = HISClientHelper.XIAOXINR.Where(p => p.XiaoXiID == xiaoXiID[0] && p.XiaoXiBM == xiaoXiID[1]).ToList()[0];
                retMsgEvent?.Invoke(message);
            }
        }

        #region 颜色处理

        private void mediLayoutControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.mediLayoutControl1.BackColor != Color.FromArgb(171, 214, 255))
            {
                this.mediLayoutControl1.BackColor = Color.FromArgb(233, 236, 239);
                this.mediLabel1.BackColor = Color.FromArgb(233, 236, 239);
                this.mediLabel2.BackColor = Color.FromArgb(233, 236, 239);
                this.mediLabel4.BackColor = Color.FromArgb(233, 236, 239);
                this.mediLabel5.BackColor = Color.FromArgb(233, 236, 239);
                this.mediLabel6.BackColor = Color.FromArgb(233, 236, 239);
            }
        }

        private void mediLayoutControl1_MouseLeave(object sender, EventArgs e)
        {
            if (this.mediLayoutControl1.BackColor != Color.FromArgb(171, 214, 255))
                BackColorSZ(false);
        }

        /// <summary>
        /// 颜色处理
        /// </summary>
        /// <param name="bol"></param>
        public void BackColorSZ(bool bol)
        {
            if (bol)
            {
                this.mediLayoutControl1.BackColor = Color.FromArgb(171, 214, 255);
                this.mediLabel1.BackColor = Color.FromArgb(171, 214, 255);
                this.mediLabel2.BackColor = Color.FromArgb(171, 214, 255);
                this.mediLabel4.BackColor = Color.FromArgb(171, 214, 255);
                this.mediLabel5.BackColor = Color.FromArgb(171, 214, 255);
                this.mediLabel6.BackColor = Color.FromArgb(171, 214, 255);
            }
            else
            {
                this.mediLayoutControl1.BackColor = Color.White;
                this.mediLabel1.BackColor = Color.White;
                this.mediLabel2.BackColor = Color.White;
                this.mediLabel4.BackColor = Color.White;
                this.mediLabel5.BackColor = Color.White;
                this.mediLabel6.BackColor = Color.White;
            }
        }

        #endregion
    }
}
