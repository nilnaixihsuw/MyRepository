using Mediinfo.Enterprise;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Mediinfo.Utility;
using Mediinfo.WinForm.HIS.Controls;
using Mediinfo.WinForm.HIS.Core;
using System;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Main
{
    public partial class ResetPasswordForm : MediDialog
    {
        private string[] GuiZeArray;
        public string ZhiGongID;

        public ResetPasswordForm()
        {
            InitializeComponent();
        }

        private void ResetPasswordForm_Shown(object sender, EventArgs e)
        {
            //var miMaGZ = "1|6|1|1|1|1|1";

            //if (string.IsNullOrWhiteSpace(miMaGZ))
            //{
            //    return;
            //}

            //GuiZeArray = miMaGZ.Split('|');
            //if (GuiZeArray.Length <= 0 || GuiZeArray[0] == "0")
            //{
            //    return;
            //}

            //string changDu = string.Empty;
            //string baoHan = string.Empty;

            //int i = 1;
            //if (GuiZeArray.Length >= i && GuiZeArray[i] != "0")
            //{
            //    changDu = string.Format("长度最低{0}位", GuiZeArray[i]);
            //}

            //i++;
            //if (GuiZeArray.Length >= i && GuiZeArray[i] == "1")
            //    baoHan += "字母、";

            //i++;
            //if (GuiZeArray.Length >= i && GuiZeArray[i] == "1")
            //    baoHan += "大写字母、";

            //i++;
            //if (GuiZeArray.Length >= i && GuiZeArray[i] == "1")
            //    baoHan += "小写字母、";

            //i++;
            //if (GuiZeArray.Length >= i && GuiZeArray[i] == "1")
            //    baoHan += "数字";

            //if (baoHan.EndsWith("、"))
            //    baoHan = baoHan.Remove(baoHan.Length - 1, 1);

            //if (!string.IsNullOrWhiteSpace(changDu) && !string.IsNullOrWhiteSpace(baoHan))
            //{
            //    this.mediLabelTips.Text = string.Format("密码{0}，必须包含{1}", changDu, baoHan);
            //}
            //else if (!string.IsNullOrWhiteSpace(changDu))
            //{
            //    this.mediLabelTips.Text = string.Format("密码{0}", changDu);
            //}
            //else if (!string.IsNullOrWhiteSpace(baoHan))
            //{
            //    this.mediLabelTips.Text = string.Format("密码必须包含{0}", baoHan);
            //}
            //if (this.mediLabelTips.Text.Length > 18)
            //    this.mediLabelTips.Text = this.mediLabelTips.Text.Substring(0, 18) + "\r\n" + this.mediLabelTips.Text.Substring(18);
        }

        private void mediButtonSave_Click(object sender, EventArgs e)
        {
            string miMa1 = this.mediTextBoxMiMa1.Text;
            string miMa2 = this.mediTextBoxMiMa2.Text;

            if (string.IsNullOrWhiteSpace(miMa1))
            {
                MediMsgBox.Failure("密码不能为空！");
                this.mediTextBoxMiMa1.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(miMa2))
            {
                MediMsgBox.Failure(this, "请确认新密码！");
                this.mediTextBoxMiMa2.Focus();
                return;
            }

            if (!string.Equals(miMa1, miMa2))
            {
                MediMsgBox.Failure(this, "两次输入的密码不一致！");
                this.mediTextBoxMiMa2.Focus();
                return;
            }

            JCJGZhiGongService service = new JCJGZhiGongService();
            string miMa = this.mediTextBoxMiMa1.Text;
            var ret = service.ResetPassword(ZhiGongID, miMa);
            if (ret.ReturnCode == ReturnCode.SUCCESS)
            {
                MediMsgBox.Success(this, "密码重置成功！");

                this.DialogResult = DialogResult.OK;
                return;
            }
            else
            {
                MediMsgBox.Failure(this, "重置密码不成功，您无法使用系统！");
                this.DialogResult = DialogResult.Cancel;
                return;
            }
        }

        private void mediButtonExit_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
            HISClientHelper.ClientSetting.Save();
            HISClientHelper.GlobalSetting.Save();

            Environment.Exit(0);
        }
    }
}