using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Mediinfo.HIS.Core;
using Mediinfo.Utility;

using Mediinfo.Enterprise;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Mediinfo.WinForm.HIS.Core;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class W_GY_XIUGAIMM : MediFormWithQX
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public W_GY_XIUGAIMM()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体初始化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void W_GY_XIUGAIMM_Load(object sender, EventArgs e)
        {
            this.yonghuMedilb.Text = HISClientHelper.USERID;
            currentpwdtb.Focus();
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelmedibtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifyPwdmedibtn_Click(object sender, EventArgs e)
        {
            var miMa = this.currentpwdtb.Text;
            string miMa1 = this.newpwdtb.Text;
            string miMa2 = this.confirmnewpwdbtn.Text;

            if (string.IsNullOrWhiteSpace(miMa))
            {
                MediMsgBox.Failure("当前密码不能为空！");
                this.currentpwdtb.Focus();
                return;
            }

            if (GYCanShuHelper.GetCanShu("公用_用户密码是否大写", "0") == "1")
            {
                miMa = miMa.ToUpper();
                miMa1 = miMa1.ToUpper();
                miMa2 = miMa2.ToUpper();
            }
            //中山医院】halo中修改密码时，提示当前密码不正确
            if (GYCanShuHelper.GetCanShu("公用_用户密码是否加密", "0") != "0")
                miMa = SHA256.Encrypt(miMa);

            if (!miMa.Equals(HISClientHelper.USERPWD))
            {
                currentpwdtb.Text = string.Empty;
                currentpwdtb.Focus();
                MediMsgBox.Warn("当前密码不正确!");
            }
            else
            {
                if (newpwdtb.Text.Equals(confirmnewpwdbtn.Text))
                {
                    if (string.IsNullOrWhiteSpace(miMa1))
                    {
                        MediMsgBox.Failure("密码不能为空！");
                        this.newpwdtb.Focus();
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(miMa2))
                    {
                        MediMsgBox.Failure(this, "请确认新密码");
                        this.confirmnewpwdbtn.Focus();
                        return;
                    }

                    if (!string.Equals(miMa1, miMa2))
                    {
                        MediMsgBox.Failure(this, "新密码与确认密码不一致\r\n请改正!");
                        this.confirmnewpwdbtn.Focus();
                        return;
                    }

                    JCJGZhiGongService service = new JCJGZhiGongService();

                    var ret = service.ResetPassword(HISClientHelper.USERID, miMa1);

                    if (ret.ReturnCode == ReturnCode.SUCCESS)
                    {
                        // 判断密码是否加密
                        if (GYCanShuHelper.GetCanShu("公用_用户密码是否加密", "0") != "0")
                            miMa1 = SHA256.Encrypt(miMa1);

                        // 客户端重新赋值
                        HISClientHelper.USERPWD = miMa1;


                        MediMsgBox.Success(this, "修改密码成功！");

                        this.DialogResult = DialogResult.OK;
                        return;
                    }
                    else
                    {
                        MediMsgBox.Failure(this, "密码修改失败！");
                        this.DialogResult = DialogResult.Cancel;
                        return;
                    }
                }
                else
                {
                    MediMsgBox.Warn("新密码与确认密码不一致\r\n请改正!");
                }
            }
        }
    }
}