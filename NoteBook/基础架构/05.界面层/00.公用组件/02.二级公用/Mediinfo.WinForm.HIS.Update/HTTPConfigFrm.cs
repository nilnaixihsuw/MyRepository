using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Update
{
    public partial class HttpConfig : Form
    {
        private string configAdress { get; }
        private string httpConfigFile { get; }
        public HttpConfig()
        {
            InitializeComponent();
            configAdress = Path.Combine(Application.StartupPath, "DownLoadAddress.xml");
            httpConfigFile = Path.Combine(Application.StartupPath, "HISGlobalSettingHttp.xml");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
