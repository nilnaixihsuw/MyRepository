using OpcClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace OpMonitor
{
    public partial class AddBlock : Form
    {
        string blockName;

        public AddBlock()
        {
            InitializeComponent();
        }

        public AddBlock(string blockName)
        {
            InitializeComponent();
            this.blockName = blockName;
        }

        private void btnCannel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            this.Dispose();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
                if (!TagConfig.CreateBlock(blockName, txtBlockName.Text))
                {
                    return;
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                }                
            } 
    }
}
