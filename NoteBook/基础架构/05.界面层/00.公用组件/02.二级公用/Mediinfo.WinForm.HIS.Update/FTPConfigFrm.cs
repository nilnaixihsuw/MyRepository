using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Update
{
    public partial class FTPConfigFrm : Form
    {
        #region properties

        /// <summary>
        /// ftp文件夹，仅用于更新单一指定系统
        /// </summary>
        public static string FtpFirstSubDirectoryName { get; set; }

        /// <summary>
        /// 选定系统本地文件夹路径
        /// </summary>
        public static string XiTongLoaclPath { get; set; }

        #endregion

        #region constructor

        public FTPConfigFrm()
        {
            InitializeComponent();
        }

        #endregion

        #region methods

        /// <summary>
        /// IP校验
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <returns></returns>
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

        #endregion

        #region events

        private void FTPConfigFrm_Load(object sender, EventArgs e)
        {
            string ipponit = DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpIp, HISGlobalSetting.Key);

            string spareipponit = DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpSpareIp, HISGlobalSetting.Key);

            if (!string.IsNullOrWhiteSpace(ipponit) && ipponit.Contains(":"))
            {
                this.mediftpiptb.Text = ipponit.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[0];
                this.tbftppoint.Text = ipponit.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1];
            }

            if (!string.IsNullOrWhiteSpace(spareipponit) && spareipponit.Contains(":"))
            {

                this.mediftpspareiptb.Text = spareipponit.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[0];
                this.tbspareftppoint.Text = spareipponit.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[1];
            }
            //this.medirootbt.Text = HISGlobalHelper.GlobalSetting.FTPINFO.FtpRootDirectoryName;
            //this.medisubNametb.Text = HISGlobalHelper.GlobalSetting.FTPINFO.FtpFirstSubDirectoryName;
            //this.mediMainNametb.Text = HISGlobalHelper.GlobalSetting.FTPINFO.LoginFormName;
            //this.mediMainNametb.Text = HISGlobalHelper.GlobalSetting.FTPINFO.UpdateExeName;
            this.mediusertb.Text = HISGlobalHelper.GlobalSetting.FTPINFO.FtpUser;
            this.medipwdtb.Text = DESHelper.Decrypt(HISGlobalHelper.GlobalSetting.FTPINFO.FtpPwd, HISGlobalSetting.Key);

            if (string.IsNullOrWhiteSpace(tbftppoint.Text))
                tbftppoint.Text = "21";

            if (string.IsNullOrWhiteSpace(tbspareftppoint.Text))
                tbspareftppoint.Text = "21";
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MedisbtnSave_Click(object sender, EventArgs e)
        {
            string pattern = @"^[0-9]*$";

            if (!IsValidIP(this.mediftpiptb.Text))
            {
                MessageBox.Show("输入FTP地址不合法!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!(Regex.IsMatch(this.tbftppoint.Text, pattern)))
            {
                MessageBox.Show("输入IP端口号不合法!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrWhiteSpace(errorProvider1.GetError((Control)this.mediusertb)))
            {
                MessageBox.Show("输入用户名不合法!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrWhiteSpace(errorProvider1.GetError((Control)this.medipwdtb)))
            {
                MessageBox.Show("输入密码不合法!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrWhiteSpace(errorProvider1.GetError((Control)this.medipwdtb)))
            {
                MessageBox.Show("输入密码不合法!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidIP(this.mediftpspareiptb.Text))
            {
                MessageBox.Show("输入备用FTP地址不合法!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!(Regex.IsMatch(this.tbspareftppoint.Text, pattern)))
            {
                MessageBox.Show("输入备用IP端口号不合法!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(this.mediftpiptb.Text))
            {
                MessageBox.Show("FTP地址不允许为空!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(tbftppoint.Text))
            {
                MessageBox.Show("FTP端口号不允许为空!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(this.mediusertb.Text))
            {
                MessageBox.Show("用户名不允许为空!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(this.medipwdtb.Text))
            {
                MessageBox.Show("密码不允许为空!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(this.mediftpspareiptb.Text))
            {
                MessageBox.Show("备用FTP地址不允许为空!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(tbspareftppoint.Text))
            {
                MessageBox.Show("备用FTP端口号不允许为空!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                HISGlobalHelper.GlobalSetting.FTPINFO.FtpIp = DESHelper.Encrypt(this.mediftpiptb.Text + ":" + tbftppoint.Text, HISGlobalSetting.Key);
                HISGlobalHelper.GlobalSetting.FTPINFO.FtpSpareIp = DESHelper.Encrypt(this.mediftpspareiptb.Text + ":" + tbspareftppoint.Text, HISGlobalSetting.Key);

                //HISGlobalHelper.GlobalSetting.FTPINFO.FtpRootDirectoryName = "HIS6";
                HISGlobalHelper.GlobalSetting.FTPINFO.FtpFirstSubDirectoryName = "WinRAR^AssemblyClient^AssemblyClient/PIC";
                HISGlobalHelper.GlobalSetting.FTPINFO.LoginFormName = "Mediinfo.WinForm.HIS.Starter";
                HISGlobalHelper.GlobalSetting.FTPINFO.UpdateExeName = "Mediinfo.WinForm.HIS.Update";
                HISGlobalHelper.GlobalSetting.FTPINFO.FtpUser = this.mediusertb.Text;
                HISGlobalHelper.GlobalSetting.FTPINFO.FtpPwd = DESHelper.Encrypt(this.medipwdtb.Text, HISGlobalSetting.Key);
                HISClientHelper.GlobalSetting.SetConfigItemValue(HISGlobalSetting.MainFTP, "连接字符串", DESHelper.Encrypt(this.mediftpiptb.Text + ":" + tbftppoint.Text, HISGlobalSetting.Key));
                HISClientHelper.GlobalSetting.SetConfigItemValue(HISGlobalSetting.BakFTP, "连接字符串", DESHelper.Encrypt(this.mediftpspareiptb.Text + ":" + tbspareftppoint.Text, HISGlobalSetting.Key));
                HISClientHelper.GlobalSetting.Save();

                //临时记录ftp路径，用于更新登录时所选择的系统
                if (!string.IsNullOrWhiteSpace(FtpFirstSubDirectoryName))
                    HISGlobalHelper.GlobalSetting.FTPINFO.FtpFirstSubDirectoryName = FtpFirstSubDirectoryName;

                MessageBox.Show(this, "保存成功!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, "保存失败,请联系管理员", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LogHelper.WriteLog(string.Format("Date {0} ,Class: {1}, Property: {2}, Error: {3}", DateTime.Now, this.GetType().FullName,
                          ex.Message,
                         ex.InnerException));
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MedisbtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 测试连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediTestConnection_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.mediftpiptb.Text))
            {
                MessageBox.Show("FTP地址不允许为空!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(tbftppoint.Text))
            {
                MessageBox.Show("FTP端口号不允许为空!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(this.mediusertb.Text))
            {
                MessageBox.Show("用户名不允许为空!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(this.medipwdtb.Text))
            {
                MessageBox.Show("密码不允许为空!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(this.mediftpspareiptb.Text))
            {
                MessageBox.Show("备用FTP不允许为空!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(tbspareftppoint.Text))
            {
                MessageBox.Show("备用FTP端口号不允许为空!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FTPHelper ftpHelper = new FTPHelper("ftp://" + this.mediftpiptb.Text + ":" + tbftppoint.Text + "", this.mediusertb.Text, this.medipwdtb.Text);
            string errorMsg = string.Empty;
            if (ftpHelper.TestFtpConnection(ref errorMsg))
            {
                FTPHelper ftpspareHelper = new FTPHelper("ftp://" + this.mediftpspareiptb.Text + ":" + tbspareftppoint.Text + "", this.mediusertb.Text, this.medipwdtb.Text);
                if (!ftpspareHelper.TestFtpConnection(ref errorMsg))
                {
                    MessageBox.Show("备用ftp服务器测试连接失败!\n请联系管理员", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LogHelper.WriteLog(string.Format("Date {0} ,Class: {1}, Property: {2}, Error: {3}", DateTime.Now, this.GetType().FullName,
                          errorMsg,
                         "error"));
                    return;
                }
                else
                {
                    MessageBox.Show("连接成功!", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                MessageBox.Show("ftp服务器测试连接失败!\n请联系管理员", "联众智慧提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LogHelper.WriteLog(string.Format("Date {0} ,Class: {1}, Property: {2}, Error: {3}", DateTime.Now, this.GetType().FullName,
                         errorMsg,
                        "error"));
                return;
            }
        }

        private void Tbftppoint_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbftppoint.Text))
            {
                const string pattern = @"^[0-9]*$";
                string content = ((TextBox)sender).Text;

                if (!(Regex.IsMatch(content, pattern)))
                {
                    errorProvider1.SetError((Control)sender, "端口号只能输入数字!");
                    e.Cancel = false;
                }
                else
                {
                    errorProvider1.Clear();
                    e.Cancel = false;
                }
            }
            else
            {
                errorProvider1.Clear();
                e.Cancel = false;
            }
        }

        private void tbspareftppoint_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbspareftppoint.Text))
            {
                const string pattern = @"^[0-9]*$";
                string content = ((TextBox)sender).Text;

                if (!(Regex.IsMatch(content, pattern)))
                {
                    errorProvider1.SetError((Control)sender, "端口号只能输入数字!");
                    e.Cancel = false;
                }
                else
                {
                    errorProvider1.Clear();
                    e.Cancel = false;
                }
            }
            else
            {
                errorProvider1.Clear();
                e.Cancel = false;
            }
        }

        private void Mediftpiptb_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(mediftpiptb.Text))
            {
                if (!IsValidIP(mediftpiptb.Text))
                {
                    errorProvider1.SetError((Control)sender, "IP地址不合法!");
                    e.Cancel = false;
                }
                else
                {
                    errorProvider1.Clear();
                    e.Cancel = false;
                }
            }
            else
            {
                errorProvider1.Clear();
                e.Cancel = false;
            }
        }

        private void mediftpspareiptb_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(mediftpspareiptb.Text))
            {
                if (!IsValidIP(mediftpspareiptb.Text))
                {
                    errorProvider1.SetError((Control)sender, "备用IP地址不合法!");
                    e.Cancel = false;
                }
                else
                {
                    errorProvider1.Clear();
                    e.Cancel = false;
                }
            }
            else
            {
                errorProvider1.Clear();
                e.Cancel = false;
            }
        }

        private void Mediftpiptb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void Tbftppoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void Mediftpspareiptb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void Tbspareftppoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void Mediusertb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void Medipwdtb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void MedisbtnSave_Enter(object sender, EventArgs e)
        {
            medisbtnCancel.Focus();
        }

        private void MedisbtnCancel_Enter(object sender, EventArgs e)
        {
            mediftpiptb.Focus();
        }

        #endregion
    }
}