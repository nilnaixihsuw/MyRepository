using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using Mediinfo.WinForm;
using System.Reflection;
using System.Threading;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.WinForm.HIS.Core;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 根据方案，弹出对应结果集供选择相应数据
    /// </summary>
    public partial class MediSelect : MediForm
    {
        public DataRow returnVal { get; set; }
        public string returnCode { get; set; }

        public bool isClose = false;

        /// <summary>
        /// 根据方案，弹出对应结果集供选择相应数据
        /// </summary>
        /// <param name="parm">方案配置参数</param>
        /// <param name="keyText">方案查询关键字</param>
        /// <param name="isCanSearch">是否能够输入查找</param>
        /// <param name="DataSource">对应数据源，如果传入了数据源，方案就不在通过sql语句进行查询，以传入数据源为准</param>
        public MediSelect(E_GY_FANGANPZ_INPARM parm, string keyText, bool isCanSearch = true, DataTable DataSource = null)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            InitDate(parm, keyText, isCanSearch, DataSource);
            this.mediGridView1.OptionsSelection.EnableAppearanceFocusedRow = true;
            this.ControlBox = false;

        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="keyText"></param>
        /// <param name="isCanSearch"></param>
        /// <param name="DataSource"></param>
        private void InitDate(E_GY_FANGANPZ_INPARM parm, string keyText, bool isCanSearch, DataTable DataSource)
        {
            returnCode = "0";
            ControlsQuery query = new ControlsQuery();
            FanganPeizhi fanganPeizhi = query.GetFanAn(parm.XIANGMU[0], parm.FANGANMING[0], false);
            string Sql = fanganPeizhi.QuerySQL;
            //弹出框需显示的列信息设置
            if (fanganPeizhi.ColumnInfo.Count > 0)
            {
                for (int i = 0; i < fanganPeizhi.ColumnInfo.Count; i++)
                {
                    GridColumn columnInfo = new GridColumn();
                    columnInfo.FieldName = fanganPeizhi.ColumnInfo[i][0];
                    if (fanganPeizhi.ColumnInfo[i][1] != "")
                    {
                        columnInfo.Caption = fanganPeizhi.ColumnInfo[i][1];
                    }
                    columnInfo.Width = Convert.ToInt32(fanganPeizhi.ColumnInfo[i][2]) * 5;
                    columnInfo.VisibleIndex = i;
                    columnInfo.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 10.5F);
                    columnInfo.AppearanceCell.Options.UseFont = true;
                    this.mediGridView1.Columns.Add(columnInfo);
                }

                //需排序的列未在显示列中，添加对应列并设为不可见
                if (fanganPeizhi.OrderList != null)
                {
                    foreach (string[] item in fanganPeizhi.OrderList)
                    {
                        var col = this.mediGridView1.Columns.Where(o => o.FieldName == item[0]);
                        if (col.Count() == 0)
                        {
                            GridColumn columnInfo = new GridColumn();
                            columnInfo.FieldName = item[0];
                            columnInfo.VisibleIndex = 100;
                            columnInfo.Visible = false;
                            this.mediGridView1.Columns.Add(columnInfo);
                        }
                    }
                }
            }
            this.Width = Convert.ToInt16((fanganPeizhi.PopformWidth * 1.6).ToString("f0"));
            this.Text = parm.XIANGMU[0]+"  请选择......(按ESC - 退出)";

            if (parm.CANSHU != null)
            {
                Dictionary<string, string> inParamDic = new Dictionary<string, string>();
                string[] RuCan = parm.CANSHU[0].Split('|');
                for (int i = 0; i < RuCan.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(RuCan[i]))
                    {
                        string xuhao = "@" + (i + 1).ToString().PadLeft(2, '0');
                        inParamDic.Add(xuhao, RuCan[i]);
                    }
                    else
                    {
                        string xuhao = "@" + (i + 1).ToString().PadLeft(2, '0');
                        inParamDic.Add(xuhao, "");
                    }
                }
                foreach (var item in inParamDic)
                {
                    Sql = Sql.Replace(item.Key, item.Value);
                }
            }
            int rowCount = 0;
            // if (DataSource!=null && DataSource.GetType().Name == "List`1" )
            if (DataSource != null && DataSource.GetType().Name == "DataTable")
            {
                // IEnumerable<object> list = DataSource as IEnumerable<object>; 
                // rowCount = list.Count(); 
                rowCount = DataSource.Rows.Count;
            }
            if (DataSource != null)
            {

                this.mediGridControl1.DataSource = DataSource;
            }
            else
            {
                Debug.WriteLine("【" + parm.XIANGMU[0] + "】方案执行查询语句：【" + Sql + "】");
                DataTable dt = query.QuerySql(Sql);
                this.mediGridControl1.DataSource = dt;

                rowCount = dt.Rows.Count;
                this.mediGridView1.SetFocusedRow(0);
            }
            if (isCanSearch)
            {
                mediSearchControl1.Text = keyText;
            }
            else
            {
                mediSearchControl1.Enabled = false;
            }

            if (rowCount < 2)
            {
                setValue();
            }
            else
            {
                returnCode = "1";
            }

        }

        private void MediSelect_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                setValue();

            }
        }


        private void setValue()
        {
            if (mediGridView1.FocusedRowHandle >= 0)
            {
                string NeiRong = "";
                var view = ((DataRowView)mediGridView1.GetRow(mediGridView1.FocusedRowHandle)).Row;
                //for (int i = 1; i < mediGridView1.Columns.Count; i++)
                //{
                //    NeiRong += mediGridView1.GetRowCellValue(mediGridView1.FocusedRowHandle, mediGridView1.Columns[i]) + "|";
                //}
                returnVal = view;
            }
            else
            {
                returnVal = null;
                returnCode = "-1";
            }
            this.Close();
        }

        private void mediGridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            setValue();
        }



        private void mediGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (mediGridView1.FocusedRowHandle >= 0)
                {
                    string NeiRong = "";
                    var view = ((DataRowView)mediGridView1.GetRow(mediGridView1.FocusedRowHandle)).Row;
                    returnVal = view;
                    this.Close();
                }
            }
        }

        private void MediSelect_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isClose)
            {
                returnVal = null;
                returnCode = "-1";
            }
        }


        /// <summary>
        /// 按esc事件
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            int WM_KEYDOWN = 256;
            int WM_SYSKEYDOWN = 260;
            if (keyData == Keys.Enter)
            {
                setValue();
                return true;
            }

            //if (keyData == Keys.Down)
            //{
            //    if (mediGridView1.FocusedRowHandle == mediGridView1.DataRowCount - 1)
            //        mediGridView1.SetFocusedRow(mediGridView1.FocusedRowHandle);
            //    else
            //    {
            //        mediGridView1.SetFocusedRow(mediGridView1.FocusedRowHandle + 1);
            //    }
               
            //}
            //if (keyData == Keys.Up)
            //{
            //    if (mediGridView1.FocusedRowHandle == mediGridView1.DataRowCount + 1)
            //        mediGridView1.SetFocusedRow(mediGridView1.FocusedRowHandle);
            //    else
            //    {
            //        mediGridView1.SetFocusedRow(mediGridView1.FocusedRowHandle - 1);
            //    }
                

            //}
            if (msg.Msg == WM_KEYDOWN | msg.Msg == WM_SYSKEYDOWN)
            {
                switch (keyData)
                {
                    case Keys.Escape:
                            isClose = true;
                            this.Close();//csc关闭窗体                        
                        break;                  
                }

            }
            return false;

        }
    }
}
