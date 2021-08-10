using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Mediinfo.Utility;
using Mediinfo.WinForm.HIS.Controls;
using Mediinfo.HIS.Core;
using Oracle.ManagedDataAccess.Client;
using Mediinfo.Enterprise;
using Mediinfo.Utility.Extensions;

namespace Mediinfo.WinForm.HIS.Main
{
    public partial class HISConnectForm : MediDialog
    {
        public HISConnectForm()
        {
            InitializeComponent();
        }

        private string oldConString = string.Empty;

        private void HISConnectForm_Shown(object sender, EventArgs e)
        {
            oldConString = HISClientHelper.GlobalSetting.GetConfigItemValue(HISGlobalSetting.LoginDBUser, "连接字符串", "");

            if (!string.IsNullOrWhiteSpace(oldConString))
            {
                string realString = DESHelper.Decrypt(oldConString,HISGlobalSetting.Key);

                OracleConnectionStringBuilder oraConn = new OracleConnectionStringBuilder(realString);
                this.mediTextBoxDataSource.EditValue = oraConn.DataSource;
                this.mediTextBoxUserName.EditValue = oraConn.UserID;
                this.mediTextBoxPassword.EditValue = oraConn.Password;
            }
        }

        private bool CheckInput()
        {
            if (this.mediTextBoxDataSource.EditValue.IsNullOrWhiteSpace())
            {
                MediMsgBox.Failure(this,"请输入数据源");
                this.mediTextBoxDataSource.Focus();
                return false;
            }

            if (this.mediTextBoxUserName.EditValue.IsNullOrWhiteSpace())
            {
                MediMsgBox.Failure(this,"请输入用户名");
                this.mediTextBoxUserName.Focus();
                return false;
            }

            if (this.mediTextBoxPassword.EditValue.IsNullOrWhiteSpace())
            {
                MediMsgBox.Failure(this,"请输入密码");
                this.mediTextBoxPassword.Focus();
                return false;
            }

            return true;
        }

        private bool TestConnect()
        {
            //if (!CheckInput())
            //    return false;

            ////先把连接字符串保存起来
            //OracleConnectionStringBuilder oraConn = new OracleConnectionStringBuilder();
            //oraConn.DataSource = this.mediTextBoxDataSource.EditValue.ToString();
            //oraConn.UserID = this.mediTextBoxUserName.EditValue.ToString();
            //oraConn.Password = this.mediTextBoxPassword.EditValue.ToString();

            //HISClientHelper.GlobalSetting.SetConfigItemValue(HISGlobalSetting.LoginDBUser, "连接字符串",DESHelper.Encrypt(oraConn.ConnectionString,HISGlobalSetting.Key));
            
            //EFInitializeService service = ServiceFactory.Create<EFInitializeService>();
            //var ret = service.TestDBConnection();
            //if (ret.ReturnCode ==  ReturnCode.LOGINDBCONNECTERROR)
            //{
            //    HISClientHelper.GlobalSetting.SetConfigItemValue(HISGlobalSetting.LoginDBUser, "连接字符串", oldConString);
            //    MediMsgBox.Failure(this,"数据库连接不成功！");
            //    return false;
            //}
            //else if (ret.ReturnCode ==  ReturnCode.MAINDBCONNECTERROR)
            //{
            //    HISClientHelper.GlobalSetting.SetConfigItemValue(HISGlobalSetting.LoginDBUser, "连接字符串", oldConString);
            //    MediMsgBox.Failure(this,"数据库连接成功，但连接主数据库时发生错误。请与系统管理员联系！");
            //    return false;
            //}
            //else if (ret.ReturnCode == ReturnCode.ERROR)
            //{
            //    HISClientHelper.GlobalSetting.SetConfigItemValue(HISGlobalSetting.LoginDBUser, "连接字符串", oldConString);
            //    MediMsgBox.Failure(this,"连接数据库发生未知错误！"+ret.ReturnMessage, ret.ReturnCode.ToString(), ret.ReturnMessage);
            //    return false;
            //}

            return true;
        }

        private void mediButtonTest_Click(object sender, EventArgs e)
        {
            if (TestConnect())
            {
                MediMsgBox.Success(this,"数据库连接成功！");
            }
        }

        private void mediButtonSave_Click(object sender, EventArgs e)
        {
            if (!TestConnect())
                return;

            //保存文件
            HISClientHelper.GlobalSetting.Save();

            this.DialogResult = DialogResult.OK;
        }

        private void mediButtonExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            Close();
        }
    }
}