using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Mediinfo.WinForm.HIS.Core.RuleEntity;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class ShiShiDialog : MediFormWithQX
    {
  
        private RuleResult jresult;
        private BingRenXx bingRenXx;

        public ShiShiDialog()
        {
            InitializeComponent();
        }


        public ShiShiDialog(BingRenXx bingRenXx, RuleResult jresult)
        {
            InitializeComponent();

            this.bingRenXx = bingRenXx;
            this.jresult = jresult;
        }

        private void ShiShiDialog_Load(object sender, EventArgs e)
        {
            l_jiankanghao.Text = bingRenXx.BINGRENID;
            l_xingming.Text = bingRenXx.XINGMING;
            l_xingbie.Text = bingRenXx.XINGBIEMC;
            l_chushengrq.Text = bingRenXx.CHUSHENGRQ.ToShortDateString();

            if(jresult.FanHuiXx.Any(m=>m.JUECEFS == "1"))
            {
                cancel.Enabled = false;
            }

            foreach (var item in jresult.FanHuiXx)
            {
                switch (item.JUECEFS)
                {
                    case "0":
                        item.JUECEFS = "禁止";
                        break;
                    case "1":
                        item.JUECEFS = "警告";
                        break;
                    case "2":
                        item.JUECEFS = "提醒";
                        break;
                    case "3":
                        item.JUECEFS = "选择";
                        break;
                    default:
                        break;
                }
            }
            this.fanHuiXxBindingSource.DataSource = jresult.FanHuiXx;
        }

        private void mediGridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //switch (e.CellValue)
            //{
            //    case "禁止":
            //        e.Appearance.ForeColor = Color.Red;
            //        break;
            //    case "警告":
            //        e.Appearance.ForeColor = Color.DarkOrange;
            //        break;
            //    default:
            //        break;
            //}
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            
        }

        private void ok_Click(object sender, EventArgs e)
        {

        }
    }
}