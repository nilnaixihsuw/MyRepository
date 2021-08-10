using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mediinfo.WinForm
{
    public partial class MaskForm : Form
    {


        public void SetOpacity(double opacity)
        {
            this.Opacity = opacity;
        }
        public MaskForm()
        {
            InitializeComponent();
        }

        private void MaskForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.Close();
           // this.Dispose();
        }
    }
}
