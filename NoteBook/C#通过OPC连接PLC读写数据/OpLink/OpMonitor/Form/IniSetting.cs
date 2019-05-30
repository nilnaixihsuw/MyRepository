using OpcClient;
using OpMonitor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpMonitor
{
    public partial class IniSetting : Form
    {
        string IP;
        string ServerName;
        IOpcClient client;

        public IniSetting()
        {
            InitializeComponent();
        }

        private void IniSetting_Load(object sender, EventArgs e)
        {
            IP = IniReader.Instance.G_IP;
            ServerName = IniReader.Instance.G_SERVERNAME;
            lblIP.Text = IP;
            lblServerName.Text = ServerName;
            if (IP.Equals(""))
            {
                txtIP.Text = "127.0.0.1";
            }
            else
            {
                txtIP.Text = IP;
            }
           
            //cmbInterval.SelectedIndex = 0;
        }

        private void btnGetServNames_Click(object sender, EventArgs e)
        {
            client = new DaOpc(txtIP.Text);
            List<string> list = client.ServerList;
            foreach (string turn in list)
            {
                cmbServerName.Items.Add(turn);
            }
            cmbServerName.SelectedIndex = 0;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {

            IniReader.Instance.G_IP = txtIP.Text.ToString();
            IniReader.Instance.G_SERVERNAME = cmbServerName.Text.ToString();
            IniReader.Instance.Save();
            this.DialogResult = DialogResult.OK;
        }

        private void btnCannel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;            
            this.Dispose();
        }
    }
}
