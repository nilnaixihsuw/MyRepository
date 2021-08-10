using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Mediinfo.DTO.HIS.XT;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserControl_XiaoXi : UserControl
    {
        /// <summary>
        /// 消息体
        /// </summary>
        public HISMessageBody message { get; set; }
        /// <summary>
        /// 收件人列表
        /// </summary>
        public List<E_XT_SHOUJIANREN_NEW> shouJianRenList = new List<E_XT_SHOUJIANREN_NEW>();
        /// <summary>
        /// ctor
        /// </summary>
        public UserControl_XiaoXi()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            var list = new List<string>();
            if (message.Receivers != null)
            {
                list = message.Receivers.ToList();
                this.mediMemoEditNR.Text = message.XiaoXiNR.ToString();
            }

            string jieShouRens = "";
            foreach (var jieShouRen in list)
            {
                jieShouRens += jieShouRen;
            }
            mediTextBoxShouJianRen.Text = jieShouRens;
            this.mediTextBoxBT.Text = message.XiaoXiBT;
        }

        private void mediButtonTianJiaSJR_Click(object sender, EventArgs e)
        {
            using (W_Msg_ChooseReceiver form = new W_Msg_ChooseReceiver(shouJianRenList))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    shouJianRenList = form.shouJianRenList;
                    string shouJianRxm = shouJianRenList.Aggregate("", (current, shouJianRen) => current + shouJianRen.SHOUJIANRXM + ";");
                    mediTextBoxShouJianRen.Text = shouJianRxm;
                }
            }
        }

        private void mediBlueButtonFaSong_Click(object sender, EventArgs e)
        {
            string xiaoXiBT = mediTextBoxBT.Text;
            string xiaoXiNR = mediMemoEditNR.Text;

            if (string.IsNullOrWhiteSpace(xiaoXiBT))
            {
                MediMsgBox.FloatMsg("消息标题不能为空！");
                mediTextBoxBT.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(xiaoXiNR))
            {
                MediMsgBox.FloatMsg("消息内容不能为空！");
                mediMemoEditNR.Focus();
                return;
            }

            //收件人
            var xiaoXiList = shouJianRenList.Select(shouJianRen =>
                    new E_XT_XIAOXIDY_NEW
                    {
                        SHOUJIANRID = shouJianRen.SHOUJIANRID,
                        SHOUJIANRXM = shouJianRen.SHOUJIANRXM
                    }).ToList();

            if (xiaoXiList.Count <= 0)
            {
                MediMsgBox.FloatMsg("收件人不能为空！");
                return;
            }

            //附件
            var xiaoXiFj = new Dictionary<string, string>();

            var ret = new JCJGXiaoXiService().FaSongXX(xiaoXiBT, xiaoXiBT, xiaoXiNR, xiaoXiList, xiaoXiFj);

            if (ret.ReturnCode != Enterprise.ReturnCode.SUCCESS)
            {
                MediMsgBox.Warn("发送失败：" + ret.ReturnMessage);
            }
            else
            {
                MediMsgBox.Success("发送成功");
            }
        }
    }
}
