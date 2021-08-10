using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Mediinfo.WinForm.HIS.Core;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class MediXiTongFZForm : MediFormWithQX
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public MediXiTongFZForm()
        {
            InitializeComponent();
        }

       
     
        private string formNameSpace;
        private string formName;
        private string GongNengID { get; set; }
       
        private List<Control> ControlList;
       /// <summary>
       /// 有参构造函数
       /// </summary>
       /// <param name="nameSpace"></param>
       /// <param name="fromName"></param>
       /// <param name="controlList"></param>
       /// <param name="gongNengId"></param>
        public MediXiTongFZForm(string nameSpace, string fromName, List<Control> controlList,string gongNengId)
        {
            InitializeComponent();

            this.formNameSpace = nameSpace;
            this.formName = fromName;
            this.ControlList = controlList;
            this.GongNengID = gongNengId;
            //this.DiaoYongCS = 
        }
        /// <summary>
        /// 二级权限项目自定义
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void medibtnSecondRootProjectManager_Click(object sender, EventArgs e)
        {
           

        }
        /// <summary>
        /// 控件项目自定义
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void medibtnControlCustom_Click(object sender, EventArgs e)
        {
            //弹出管理界面
            FrmButtonSetting form = new FrmButtonSetting(formNameSpace, formName, ControlList,null);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                GYChuangKouZYHelper.RefreshChuangKouInfo(formNameSpace, formName);
            }
            form.Dispose();
        }


        private void medibtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}