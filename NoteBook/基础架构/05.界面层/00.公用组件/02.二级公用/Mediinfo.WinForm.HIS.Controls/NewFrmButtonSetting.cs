using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Customization;
using Mediinfo.ServiceProxy.HIS.GongYong;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.DTO.Core;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class NewFrmButtonSetting : MediDialog
    {
        private List<ControlItemProperty> controlItemList = null;
        private string formNameSpace;
        private string formName;
        private string formText;
        private GYChuangKouZYService chuangKouZYService = new GYChuangKouZYService();
        private List<Control> ControlList;
        /// <summary>
        /// 子窗口控件集合
        /// </summary>
        private MediTraceList<E_GY_CHUANGKOUZY_NEW> childrenChuangkouNew { get; set; }
        public NewFrmButtonSetting(string nameSpace, string fromName, List<Control> controlList)
        {
            InitializeComponent();

            this.formNameSpace = nameSpace;
            this.formName = fromName;
            this.ControlList = controlList;
            childrenChuangkouNew = new MediTraceList<E_GY_CHUANGKOUZY_NEW>();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonSave_Click(object sender, EventArgs e)
        {
            List<E_GY_CHUANGKOUZY_NEW> gychuangkounewList = new  List<E_GY_CHUANGKOUZY_NEW>();
            var gychuangkounewListtemp = (this.mediGridControl1.DataSource as MediTraceList<E_GY_CHUANGKOUZY_NEW>);
            gychuangkounewList = gychuangkounewListtemp.GetChanges();

            var returnchuangkousaveresult = chuangKouZYService.Save(gychuangkounewList, null);
            if (returnchuangkousaveresult.Return)
            {
                MediMsgBox.Show("保存成功！");

            }else
            {
                MediMsgBox.Show("保存失败！");
            }
        }
        /// <summary>
        /// 复位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediButtonReset_Click(object sender, EventArgs e)
        {
            var returnResetResult = chuangKouZYService.Reset(formNameSpace, formName);
            if (returnResetResult.Return)
            {
                this.mediGridControl1.DataSource = null;
                this.mediGridControl1.Refresh();
                MediMsgBox.Show("保存成功！");

            }
            else
            {
                MediMsgBox.Show("保存失败！");
            }
        }
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewFrmButtonSetting_Load(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.formText = this.Owner.Text;
            }

            mediLabel1.Text = string.Format("对象名:{0}",this.formName);

          

            chuangKouZYService = new GYChuangKouZYService();

            var ret = chuangKouZYService.GetByFromName(this.formNameSpace, this.formName);
            if (ret.ReturnCode == ReturnCode.SUCCESS && ret.Return.Count > 0)
                this.mediGridControl1.DataSource = new MediTraceList<E_GY_CHUANGKOUZY_NEW>(ret.Return);

                chuangKouZYList = new List<E_GY_CHUANGKOUZY_NEW>();





                foreach (var item in ControlList)
                {
                    if (!(item is SimpleButton))
                        continue;

                    MediButton mediButton = (MediButton)item;
                    E_GY_CHUANGKOUZY_NEW gychuangkounew = new E_GY_CHUANGKOUZY_NEW();
                    gychuangkounew.NAMESPACE = formNameSpace;
                    gychuangkounew.FORMNAME = formName;
                    //gychuangkounew.CHUANGKOUZYID = formName;
                    gychuangkounew.CONTROLNAME = mediButton.Name;
                    gychuangkounew.WENZI = mediButton.Text;
                    gychuangkounew.TUPIAN = "";
                    gychuangkounew.YANSE = "ARGB(" + mediButton.BackColor.A + "," + mediButton.BackColor.R + "," + mediButton.BackColor.G + "," + mediButton.BackColor.B + ")";
                    gychuangkounew.ZITIDX = (int)mediButton.Font.Size;
                    gychuangkounew.XIANSHIBZ = mediButton.Visible == true ? 1 : 0;
                    gychuangkounew.XIANSHIKZ = 1;
                    gychuangkounew.QUANXIANKZ = 1;
                    if (chuangKouZYList.Where(o => o.CONTROLNAME == gychuangkounew.CONTROLNAME).ToList().Count > 0) continue;

                    chuangKouZYList.Add(gychuangkounew);

                }
            var chuangKouZYList1 = new MediTraceList<E_GY_CHUANGKOUZY_NEW>(chuangKouZYList);
            this.mediButtonRootGridControl.DataSource = chuangKouZYList1;


          





            //this.mediButtonSave.Enabled = false;
        }
        /// <summary>
        /// 聚焦行改变刷新相关权限属性面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediGridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
           
        }
        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void medibtnUpMove_Click(object sender, EventArgs e)
        {

            if (mediGridView2.FocusedRowHandle > -1)
            {

                var tempchuangKouZYList = (MediTraceList<E_GY_CHUANGKOUZY_NEW>)this.mediButtonRootGridControl.DataSource;
                string mediGridViewcolumn = (string)mediGridView2.GetFocusedRowCellValue("CONTROLNAME");
              
                tempchuangKouZYList.Add(childrenChuangkouNew.Where(o => o.CONTROLNAME == mediGridViewcolumn).FirstOrDefault());
                childrenChuangkouNew.Remove(childrenChuangkouNew.Where(o => o.CONTROLNAME == mediGridViewcolumn).FirstOrDefault());
                this.mediButtonRootGridControl.DataSource = tempchuangKouZYList;

                this.mediGridControl1.DataSource = childrenChuangkouNew;
                this.mediButtonRootGridControl.RefreshDataSource();
                this.mediGridControl1.RefreshDataSource();



            }

        }
        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void medibtnDownMove_Click(object sender, EventArgs e)
        {

            if (mediGridView1.FocusedRowHandle>-1)
            {

                MediTraceList<E_GY_CHUANGKOUZY_NEW> tempchuangKouZYList = (MediTraceList<E_GY_CHUANGKOUZY_NEW>)this.mediButtonRootGridControl.DataSource;
                string mediGridViewcolumn = (string)mediGridView1.GetFocusedRowCellValue("CONTROLNAME");
                childrenChuangkouNew.Add(tempchuangKouZYList.Where(o => o.CONTROLNAME == mediGridViewcolumn).FirstOrDefault());
                tempchuangKouZYList.Remove(tempchuangKouZYList.Where(o => o.CONTROLNAME == mediGridViewcolumn).FirstOrDefault());
                foreach (var item in tempchuangKouZYList)
                    item.SetState(DTO.Core.DTOState.New);
                this.mediButtonRootGridControl.DataSource = tempchuangKouZYList;
              
                this.mediGridControl1.DataSource = childrenChuangkouNew;
                this.mediButtonRootGridControl.RefreshDataSource();
                this.mediGridControl1.RefreshDataSource();


            }
           
        }
    }
}