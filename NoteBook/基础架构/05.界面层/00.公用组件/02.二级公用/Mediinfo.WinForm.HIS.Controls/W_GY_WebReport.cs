using Mediinfo.HIS.Core;
using Mediinfo.Utility.Extensions;
using Mediinfo.WinForm.HIS.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class W_GY_WEBREPORT : MediFormWithQX
    {
        public W_GY_WEBREPORT()
        {
            InitializeComponent();
        }
         

        private void W_GY_WebReport_Shown(object sender, EventArgs e)
        {
            string webUrl = this.ChuangKouCS;
            webUrl = webUrl.Replace("用户ID", HISClientHelper.USERID);
            webUrl = webUrl.Replace("用户姓名", HISClientHelper.USERNAME);
            webUrl = webUrl.Replace("用户密码", HISClientHelper.USERPWD); 
            this.webBrowser1.Navigate(new Uri(webUrl.ToStringEx()));
        }
    }
}
