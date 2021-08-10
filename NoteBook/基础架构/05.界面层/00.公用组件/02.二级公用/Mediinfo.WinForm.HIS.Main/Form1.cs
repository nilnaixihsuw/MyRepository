using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mediinfo.WinForm.HIS.Controls;
using Mediinfo.WinForm.HIS.Core;

namespace Mediinfo.WinForm.HIS.Main
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void mediButton1_Click(object sender, EventArgs e)
        {
            List<string> s = new List<string>();

            s.Where(m => m == ("eeee")).Count();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            RuleEngine.Act();
            stopwatch.Stop();
            MessageBox.Show(stopwatch.Elapsed.TotalSeconds.ToString());
        }
    }
}
