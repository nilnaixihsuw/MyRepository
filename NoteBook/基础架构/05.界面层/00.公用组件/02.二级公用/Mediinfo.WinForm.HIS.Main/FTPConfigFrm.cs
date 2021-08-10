using Mediinfo.HIS.Core;
using Mediinfo.Utility;
using Mediinfo.WinForm.HIS.Controls;

using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Main
{
    public partial class FTPConfigFrm : MediForm
    {
        public FTPConfigFrm()
        {
            InitializeComponent();
            this.Shown += FTPConfigFrm_Shown;
        }

        private void FTPConfigFrm_Shown(object sender, EventArgs e)
        {
            this.TopMost = true;
            mediftpiptb.Focus();
        }

        private void FTPConfigFrm_Load(object sender, EventArgs e)
        {
            string ipponit = DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpIp, HISGlobalSetting.Key);

            string spareipponit = DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpSpareIp, HISGlobalSetting.Key);
            if (!string.IsNullOrWhiteSpace(ipponit))
            {
                this.mediftpiptb.Text = ipponit.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[0];
                this.meditbftppoint.Text = ipponit.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1];
                if (string.IsNullOrWhiteSpace(this.meditbftppoint.Text))
                {
                    this.meditbftppoint.Text = "21";
                }
            }
            else
            {
                this.meditbftppoint.Text = "21";
            }

            if (!string.IsNullOrWhiteSpace(spareipponit))
            {
                this.mediftpspareiptb.Text = spareipponit.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[0];
                this.medispareftppoint.Text = spareipponit.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1];
                if (string.IsNullOrWhiteSpace(this.medispareftppoint.Text))
                {
                    this.medispareftppoint.Text = "21";
                }
            }
            else
            {
                this.medispareftppoint.Text = "21";
            }

            // this.mediftpiptb.Text = DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpIp,HISGlobalSetting.Key);
            //this.medirootbt.Text = HISGlobalHelper.GlobalSetting.FTPINFO.FtpRootDirectoryName;
            //this.medisubNametb.Text = HISGlobalHelper.GlobalSetting.FTPINFO.FtpFirstSubDirectoryName;
            //this.mediMainNametb.Text = HISGlobalHelper.GlobalSetting.FTPINFO.LoginFormName;
            // this.mediUpdatetb.Text = HISGlobalHelper.GlobalSetting.FTPINFO.UpdateExeName;
            this.mediusertb.Text = HISGlobalHelper.GlobalSetting.FTPINFO.FtpUser;
            this.medipwdtb.Text = DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpPwd, HISGlobalSetting.Key);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void medisbtnSave_Click(object sender, EventArgs e)
        {
            string pattern = @"^[0-9]*$";

            if (!IsValidIP(this.mediftpiptb.Text))
            {
                MediMsgBox.Warn(this, "输入FTP地址不合法!");
                mediftpiptb.Focus();
                return;
            }

            if (!(Regex.IsMatch(meditbftppoint.Text, pattern)))
            {
                MediMsgBox.Warn(this, "输入IP端口号不合法!");
                meditbftppoint.Focus();
                return;
            }
            if (!string.IsNullOrWhiteSpace(dxErrorProvider1.GetError((Control)this.mediusertb)))
            {
                MediMsgBox.Warn(this, "输入用户名不合法!");
                mediusertb.Focus();
                return;
            }
            if (!string.IsNullOrWhiteSpace(dxErrorProvider1.GetError((Control)this.medipwdtb)))
            {
                MediMsgBox.Warn(this, "输入密码不合法!");
                medipwdtb.Focus();
                return;
            }

            if (!IsValidIP(this.mediftpspareiptb.Text))
            {
                MediMsgBox.Warn(this, "输入备用FTP地址不合法!");
                mediftpspareiptb.Focus();
                return;
            }
            if (!(Regex.IsMatch(medispareftppoint.Text, pattern)))
            {
                MediMsgBox.Warn(this, "输入备用IP端口号不合法!");
                medispareftppoint.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(this.mediftpiptb.Text))
            {
                MediMsgBox.Warn(this, "FTP地址不允许为空!");
                mediftpiptb.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(meditbftppoint.Text))
            {
                MediMsgBox.Warn(this, "FTP端口号不允许为空!");
                meditbftppoint.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(this.mediusertb.Text))
            {
                MediMsgBox.Warn(this, "用户名不允许为空!");
                mediusertb.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(this.medipwdtb.Text))
            {
                MediMsgBox.Warn(this, "密码不允许为空!");
                medipwdtb.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(this.mediftpspareiptb.Text))
            {
                MediMsgBox.Warn(this, "备用FTP不允许为空!");
                mediftpspareiptb.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(medispareftppoint.Text))
            {
                MediMsgBox.Warn(this, "备用FTP端口号不允许为空!", "联众智慧提示");
                medispareftppoint.Focus();
                return;
            }
            try
            {
                HISGlobalHelper.GlobalSetting.FTPINFO.FtpIp = DESHelper.Encrypt(this.mediftpiptb.Text + ":" + meditbftppoint.Text, HISGlobalSetting.Key);
                HISGlobalHelper.GlobalSetting.FTPINFO.FtpSpareIp = DESHelper.Encrypt(this.mediftpspareiptb.Text + ":" + medispareftppoint.Text, HISGlobalSetting.Key);
                //HISGlobalHelper.GlobalSetting.FTPINFO.FtpRootDirectoryName = "HIS6";
                HISGlobalHelper.GlobalSetting.FTPINFO.FtpFirstSubDirectoryName = "AssemblyClient^AssemblyClient/PIC";
                HISGlobalHelper.GlobalSetting.FTPINFO.LoginFormName = "Mediinfo.WinForm.HIS.Starter";
                HISGlobalHelper.GlobalSetting.FTPINFO.UpdateExeName = "Mediinfo.WinForm.HIS.Update";
                HISGlobalHelper.GlobalSetting.FTPINFO.FtpUser = this.mediusertb.Text;
                HISGlobalHelper.GlobalSetting.FTPINFO.FtpPwd = DESHelper.Encrypt(this.medipwdtb.Text, HISGlobalSetting.Key);

                HISClientHelper.GlobalSetting.SetConfigItemValue(HISGlobalSetting.MainFTP, "连接字符串", DESHelper.Encrypt(this.mediftpiptb.Text + ":" + meditbftppoint.Text, HISGlobalSetting.Key));
                HISClientHelper.GlobalSetting.SetConfigItemValue(HISGlobalSetting.BakFTP, "连接字符串", DESHelper.Encrypt(this.mediftpspareiptb.Text + ":" + medispareftppoint.Text, HISGlobalSetting.Key));
                HISClientHelper.GlobalSetting.Save();
                MediMsgBox.Success(this, "保存成功!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                this.DialogResult = DialogResult.Cancel;
                MediMsgBox.Failure(this, "保存失败!原因:" + ex.Message);
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void medisbtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 测试连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediTestConnection_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.mediftpiptb.Text))
            {
                MediMsgBox.Warn(this, "FTP地址不允许为空!");
                mediftpiptb.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(meditbftppoint.Text))
            {
                MediMsgBox.Warn(this, "FTP端口号不允许为空!");
                meditbftppoint.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(this.mediusertb.Text))
            {
                MediMsgBox.Warn(this, "用户名不允许为空!");
                mediusertb.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(this.medipwdtb.Text))
            {
                MediMsgBox.Warn(this, "密码不允许为空!");
                medipwdtb.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(this.mediftpspareiptb.Text))
            {
                MediMsgBox.Warn(this, "备用FTP不允许为空!");
                mediftpspareiptb.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(medispareftppoint.Text))
            {
                MediMsgBox.Warn(this, "备用FTP端口号不允许为空!");
                medispareftppoint.Focus();
                return;
            }
            FTPHelper ftpHelper = new FTPHelper("ftp://" + this.mediftpiptb.Text + ":" + meditbftppoint.Text + "", this.mediusertb.Text, this.medipwdtb.Text);
            if (ftpHelper.TestFtpConnection())
            {
                FTPHelper ftpspareHelper = new FTPHelper("ftp://" + this.mediftpspareiptb.Text + ":" + medispareftppoint.Text + "", this.mediusertb.Text, this.medipwdtb.Text);
                if (!ftpspareHelper.TestFtpConnection())
                {
                    MediMsgBox.Failure(this, "备用ftp服务器测试连接失败!");

                    return;
                }
                else
                {
                    MediMsgBox.Success(this, "连接成功!");
                }
            }
            else
            {
                MediMsgBox.Failure(this, "主ftp服务器测试连接失败!");
                return;
            }
        }

        /// <summary>
        /// ftp地址验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediftpiptb_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(mediftpiptb.Text))
            {
                if (!IsValidIP(mediftpiptb.Text))
                {
                    mediftpiptb.ErrorText = "ftp地址不合法!";
                }
            }
        }

        private void mediusertb_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(mediusertb.Text))
            {
                mediusertb.ErrorText = "用户名不允许为空!";
            }
        }

        private void medipwdtb_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(medipwdtb.Text))
            {
                medipwdtb.ErrorText = "密码不允许为空!";
            }
        }

        private void mediftpspareiptb_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(mediftpspareiptb.Text))
            {
                if (!IsValidIP(mediftpspareiptb.Text))
                {
                    mediftpspareiptb.ErrorText = "备用FTP地址不合法!";
                }
            }
        }

        private void meditbftppoint_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(meditbftppoint.Text))
            {
                meditbftppoint.ErrorText = "FTP地址端口不允许为空!";
            }
        }

        private void medispareftppoint_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(medispareftppoint.Text))
            {
                medispareftppoint.ErrorText = "备用FTP地址端口不允许为空!";
            }
        }

        private void meditbftppoint_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(meditbftppoint.Text))
            {
                const string pattern = @"^[0-9]*$";
                string content = ((MediTextBox)sender).Text;

                if (!(Regex.IsMatch(content, pattern)))
                {
                    dxErrorProvider1.SetError((Control)sender, "端口号只能输入数字!");
                    e.Cancel = false;
                }
                else
                {
                    dxErrorProvider1.ClearErrors();
                    e.Cancel = false;
                }
            }
            else
            {
                dxErrorProvider1.ClearErrors();
                e.Cancel = false;
            }
        }

        private void medispareftppoint_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(medispareftppoint.Text))
            {
                const string pattern = @"^[0-9]*$";
                string content = ((MediTextBox)sender).Text;

                if (!(Regex.IsMatch(content, pattern)))
                {
                    dxErrorProvider1.SetError((Control)sender, "端口号只能输入数字!");
                    e.Cancel = false;
                }
                else
                {
                    dxErrorProvider1.ClearErrors();
                    e.Cancel = false;
                }
            }
            else
            {
                dxErrorProvider1.ClearErrors();
                e.Cancel = false;
            }
        }

        public bool IsValidIP(string ip)
        {
            try
            {
                if (Regex.IsMatch(ip, @"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$"))
                {
                    string[] ips = ip.Split('.');
                    if (ips.Length == 4 || ips.Length == 6)
                    {
                        if (Int32.Parse(ips[0]) < 256 && Int32.Parse(ips[1]) < 256 & Int32.Parse(ips[2]) < 256 & Int32.Parse(ips[3]) < 256)

                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}