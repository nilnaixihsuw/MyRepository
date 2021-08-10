using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Mediinfo.WinForm.HIS.Controls;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class W_GY_TISIXX : MediDialog
    {
        
        /// <summary>
        /// 返回结果
        /// </summary>
        public int TiShiXX_Result = 1;
        private int FocusButton = 1;
        public W_GY_TISIXX()
        {
            InitializeComponent(); 
        }
        public void Init(List<string> buttonText, List<string> tiShiXX,int deafult=1)
        {
            FocusButton = deafult;
            switch (buttonText.Count)
            {
                case 2: 
                    mediButtonLeft.Location = new Point(130, 100);
                    mediButtonLeft.Text = buttonText[0];
                    mediButtonCenter.Location = new Point(270, 100);
                    mediButtonCenter.Text = buttonText[1];
                    mediButtonRight.Visible = false;
                    break;
                case 3:
                    mediButtonLeft.Location = new Point(40, 100);
                    mediButtonLeft.Text = buttonText[0];
                    mediButtonCenter.Location = new Point(190, 100);
                    mediButtonCenter.Text = buttonText[1];
                    mediButtonRight.Location = new Point(340, 100);
                    mediButtonRight.Text = buttonText[2];
                    break;
            }
          
            richTextBox1.Text = tiShiXX[0];
            if (tiShiXX.Count>1)
            {
                var first = tiShiXX[0].IndexOf(tiShiXX[1]); 
                richTextBox1.Select(first, tiShiXX[1].Length);
                richTextBox1.SelectionColor = Color.Red;
            }
          
            
        } 
        private void W_ZJ_DAIGUAHTS_Shown(object sender, EventArgs e)
        {
            switch (FocusButton)
            {
                case 1:
                    mediButtonLeft.Focus();
                    break;
                case 2:
                    mediButtonCenter.Focus();
                    break;
                case 3:
                    mediButtonRight.Focus();
                    break;
            }
        }
        #region 界面按钮
        /// <summary>
        /// 自定义左边的按钮 返回1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonLeft_Click(object sender, EventArgs e)
        {
            TiShiXX_Result = 1;
            DialogResult = DialogResult.OK;
            this.Close();
        }
        /// <summary>
        /// 自定义中间的按钮 返回2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonCenter_Click(object sender, EventArgs e)
        {
            TiShiXX_Result = 2;
            DialogResult = DialogResult.OK;
            this.Close(); ;
        }
        /// <summary>
        /// 自定义右边的按钮 返回3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonRight_Click(object sender, EventArgs e)
        {
            TiShiXX_Result = 3;
            DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

    }
}