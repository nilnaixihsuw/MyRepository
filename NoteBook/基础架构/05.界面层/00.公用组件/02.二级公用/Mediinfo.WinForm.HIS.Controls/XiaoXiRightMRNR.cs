using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mediinfo.HIS.Core;
using Newtonsoft.Json.Linq;
using Mediinfo.Utility.Extensions;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Mediinfo.WinForm.HIS.Core;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class XiaoXiRightMRNR : MediUserControl
    {
        public HISMessageBody message { get; set; }
        JObject xiaoXiNR = null;
        private JCJGXiaoXiService jCJGXiaoXiService;

        public XiaoXiRightMRNR()
        {
            InitializeComponent();            
            jCJGXiaoXiService = new JCJGXiaoXiService();
        }

        /// <summary>
        /// 内容初始化
        /// </summary>
        /// <param name="message"></param>
        private void Init()
        {
            string canShuZhi = GYCanShuHelper.GetCanShu("会诊消息标题内容取值字段设置", "");
            string xiaoXiBT = string.Empty;
            string neiRong = string.Empty;
            if (!string.IsNullOrEmpty(message.XiaoXiNR.ToStringEx()) && JsonSplit.IsJson(message.XiaoXiNR.ToString()))
            {
                xiaoXiNR = (JObject)message.XiaoXiNR;
                xiaoXiBT = xiaoXiNR["BT"].ToStringEx();
                neiRong = xiaoXiNR["NR"].ToStringEx();

                if (string.IsNullOrEmpty(xiaoXiBT) && !string.IsNullOrEmpty(canShuZhi))
                {
                    string str = canShuZhi.Split('|')[0];
                    if (!str.Contains(","))
                    {
                        xiaoXiBT = xiaoXiNR[str].ToStringEx();
                    }
                    else
                    {
                        foreach (string s in str.Split(','))
                        {
                            xiaoXiBT += xiaoXiNR[s].ToStringEx() + "\r\n";
                        }
                    }
                    //xiaoXiBT = xiaoXiNR["HUIZHENMD"].ToStringEx();
                }
                if (string.IsNullOrEmpty(neiRong) && !string.IsNullOrEmpty(canShuZhi))
                {
                    string str = canShuZhi.Split('|')[1];
                    if (!str.Contains(","))
                    {
                        neiRong = xiaoXiNR[str].ToStringEx();
                    }
                    else
                    {
                        foreach (string s in str.Split(','))
                        {
                            neiRong += xiaoXiNR[s].ToStringEx() + "\r\n";
                        }
                    }
                    //neiRong = xiaoXiNR["HUIZHENYJ"].ToStringEx() + "\r\n" + xiaoXiNR["BINGQINGZY"].ToStringEx() + "\r\n" + xiaoXiNR["LINCHUANGZD"].ToStringEx();
                }
            }
            if (string.IsNullOrEmpty(neiRong) || string.IsNullOrEmpty(xiaoXiBT))
            {
                xiaoXiBT = message.XiaoXiBT.ToStringEx();
                neiRong = message.XiaoXiNR.ToStringEx();
            }

            this.mediLabel3.Text = xiaoXiBT;

            this.mediLabel1.Text = xiaoXiBT;
            if (message.BingRenID == null)
            {
                this.mediLabel3.Visible = false;
                mediLabel2.Visible = false;
            }
            //else
            //{
            //    var result = jCJGXiaoXiService.GetBingRenXMByID(message.MenZhenZyBz, message.BingRenID);
            //    if (result.ReturnCode == Enterprise.ReturnCode.SUCCESS)
            //    {
            //        this.mediLabel3.Text = result.Return;
            //    }
            //}

            this.mediLabel7.Text = message.FaSongSj.Year + "年" + message.FaSongSj.Month + "月" + message.FaSongSj.Day + "日";
            this.mediMemoEdit1.Text = neiRong;
        }

        private void XiaoXiRightMRNR_Load(object sender, EventArgs e)
        {
            Init();
        }
    }
}
