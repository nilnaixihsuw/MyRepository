using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Skins;
using DevExpress.XtraGrid.Columns;

namespace Mediinfo.WinForm.HIS.Controls
{
    public partial class MediUcGridControl : UserControl
    {
        /// <summary>
        /// 挂号预约专用
        /// </summary>
        public MediUcGridControl()
        {
            InitializeComponent();
            Init();
        }
        /// <summary>
        /// 双击事件返回参数委托
        /// </summary>
        public Action<object,string> GetGridDataRow;
        private void Init()
        {
            this.gridView1.OptionsView.AllowCellMerge = true;
            this.gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.CellMerge += MediGridView1_CellMerge;
            this.gridView1.CustomDrawColumnHeader += GridView1_CustomDrawColumnHeader;
            this.gridView1.MouseMove += GridView1_MouseMove;
            this.gridView1.MouseDown += GridView1_MouseDown;
            this.gridView1.CustomDrawCell += gridView1_CustomDrawCell;
            this.gridView1.CalcRowHeight += GridView1_CalcRowHeight;

        }
        /// <summary>
        /// 设置行高
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView1_CalcRowHeight(object sender, DevExpress.XtraGrid.Views.Grid.RowHeightEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                e.RowHeight = 28;
            }
        }
        /// <summary>
        /// 鼠标双击双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView1_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo gridHitInfo = this.gridView1.CalcHitInfo(e.Location);
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                if (gridHitInfo.RowHandle < 0) return;
                if (gridHitInfo.Column.FieldName != "gridColumn1" && gridHitInfo.Column.FieldName != "gridColumn2" && gridHitInfo.Column.FieldName != "gridColumn3")
                {
                    string value = this.gridView1.GetRowCellValue(gridHitInfo.RowHandle, gridHitInfo.Column.FieldName).ToString();
                    if (!string.IsNullOrWhiteSpace(value) && value.IndexOf("已满") < 0)
                    {
                        object obj = this.gridView1.GetRow(gridHitInfo.RowHandle);
                        GetGridDataRow?.Invoke(obj, gridHitInfo.Column.FieldName);
                    }
                }
            }
        }
        /// <summary>
        /// 内容行线条重绘、字体背景颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            int height = e.Bounds.Height;
            int width = e.Bounds.Width + 1;
            if (e.Column.FieldName != "gridColumn1"  && e.Column.FieldName != "gridColumn2" && e.Column.FieldName != "gridColumn3")
            {
                if ( e.Column.FieldName == "gridColumn4")
                {
                    e.Appearance.ForeColor = System.Drawing.Color.FromArgb(136, 136, 136);
                }
                else
                {
                    if (e.CellValue != null)
                    {
                        if (e.CellValue.ToString().Contains("预约"))
                        {
                            e.Appearance.ForeColor = System.Drawing.Color.FromArgb(80,146,3);
                            e.Appearance.BackColor = System.Drawing.Color.FromArgb(221,242,207);


                        }
                        else if (e.CellValue.ToString().Contains("满"))
                        {
                            e.Appearance.ForeColor = System.Drawing.Color.FromArgb(11, 149, 209);
                            e.Appearance.BackColor = System.Drawing.Color.FromArgb(220, 235, 251);
                        }
                    }
                }

            }
            e.DefaultDraw();
            Color color = Color.Silver;
            float[] dashValues = { 2, 3 };
            Pen blackPen = new Pen(color, 0);
            blackPen.DashPattern = dashValues;
            if (e.Column.FieldName == "gridColumn1" || e.Column.FieldName == "gridColumn2" || e.Column.FieldName == "gridColumn3")
            {
                e.Cache.DrawLine(blackPen, new Point(e.Bounds.X, e.Bounds.Y + height), new Point(e.Bounds.X + width, e.Bounds.Y + height));
                e.Handled = true;
            }
            else
            {
                if (e.RowHandle % 2 == 0)
                {
                    color = Color.White;
                    blackPen = new Pen(color, 0);
                }
                e.Cache.DrawLine(blackPen, new Point(e.Bounds.X, e.Bounds.Y + height), new Point(e.Bounds.X + width, e.Bounds.Y + height));
                e.Handled = true;
            }


        }
        /// <summary>
        /// 鼠标移动到单元格时鼠标的形状设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView1_MouseMove(object sender, MouseEventArgs e)
        {
            GridHitInfo gridHitInfo = this.gridView1.CalcHitInfo(e.X, e.Y);
            this.gridControl1.Cursor = Cursors.Default;
            if (gridHitInfo.InRowCell)
            {
                if (gridHitInfo.Column.FieldName != "gridColumn1" && gridHitInfo.Column.FieldName != "gridColumn2" && gridHitInfo.Column.FieldName != "gridColumn3"&& gridHitInfo.Column.FieldName != "gridColumn4")
                {
                    object value = this.gridView1.GetRowCellValue(gridHitInfo.RowHandle, gridHitInfo.Column.FieldName);
                    if (value == null || value.ToString() == "") return;
                    this.gridControl1.Cursor = Cursors.Hand;
                }
            }
        }
        /// <summary>
        /// 表头重绘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView1_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            if (e.Column == null)
                return;
            e.Graphics.FillRectangle(new SolidBrush(GetSystemColor()), e.Bounds);
            e.Appearance.DrawString(e.Cache, e.Info.Caption, e.Info.CaptionRect);
            if (e.Column.FieldName != "gridColumn1" && e.Column.FieldName != "gridColumn2" && e.Column.FieldName != "gridColumn3"|| e.Column.FieldName != "gridColumn4")
            {
                //    e.Graphics.DrawLine(new Pen(Color.Snow, 1.0f), e.Bounds.Right - 1, e.Bounds.Y, e.Bounds.Right - 1, e.Bounds.Bottom);
                if (e.Column.VisibleIndex != 0)
                {
                    e.Graphics.DrawLine(new Pen(GetSystemColor(), 1.0f), e.Bounds.Right, e.Bounds.Y, e.Bounds.Right, e.Bounds.Bottom);
                    e.Graphics.DrawLine(new Pen(GetSystemColor(), 1.0f), e.Bounds.Left, e.Bounds.Y, e.Bounds.Left, e.Bounds.Bottom);
                }

            }
            e.Handled = true;

        }
        /// <summary>
        /// 合并指定单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediGridView1_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.Column.FieldName == "gridColumn1" || e.Column.FieldName == "gridColumn2"|| e.Column.FieldName == "gridColumn3")
            {
                int row1 = e.RowHandle1;
                int row2 = e.RowHandle2;
                string value1 = gridView1.GetDataRow(row1)["gridColumn1"].ToString();
                string value2 = gridView1.GetDataRow(row2)["gridColumn1"].ToString();
                if (value1 != value2)
                {
                    e.Handled = true;
                }

            }
        }
        /// <summary>
        /// 列集合
        /// </summary>
        private GridColumn[] gridColumns;
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { set; get; } = DateTime.Now;

        private DateTime endTime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get { return endTime; }
            set
            {
                endTime = value;
                if (endTime != null && endTime > StartTime)
                {
                    CreateColumns();
                }
            }
        }
        private string captiontext;
        /// <summary>
        /// 第一列显示名称
        /// </summary>
        public string CaptionText
        {
            get { return captiontext; }
            set {
                captiontext = value;
                this.gridColumn1.Caption = captiontext;
            }
        }
        /// <summary>
        /// 绑定数据源(DataSet)
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="TableName"></param>
        public void SetDataSource(DataSet dataSet, string TableName)
        {
            this.gridControl1.DataSource = dataSet;
            this.gridControl1.DataMember = TableName;
        }
        /// <summary>
        /// 绑定数据源(Table)
        /// </summary>
        /// <param name="Table"></param>
        public void SetDataSource(DataTable Table)
        {
            this.gridControl1.DataSource = Table; ;
            Sort();
        }
        /// <summary>
        /// 系统颜色设定
        /// </summary>
        /// <returns></returns>
        public Color GetSystemColor()
        {
            SkinElement element = SkinManager.GetSkinElement(SkinProductId.Grid, DevExpress.LookAndFeel.UserLookAndFeel.Default, "Header");
            using (Bitmap bmp = new Bitmap(element.Image.Image))
            {
                Color pixelColor = bmp.GetPixel(1, 1);

                return Color.FromArgb(pixelColor.A, pixelColor.R, pixelColor.G, pixelColor.B);
            }
        }
        /// <summary>
        /// 动态创建日期列
        /// </summary>
        private void CreateColumns()
        {
            if (gridColumns != null && gridColumns.Length > 0)
            {
                for (int i = 0; i < gridColumns.Length; i++)
                {
                    this.gridView1.Columns.Remove(gridColumns[i]);
                }
            }

            int date = DateDiff(StartTime, EndTime);
            date = date + 1;
            gridColumns = new GridColumn[date];
            for (int i = 0; i < date; i++)
            {
                DateTime time = StartTime.AddDays(i);
                GridColumn gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
                gridColumn.FieldName = time.ToString("yyyy/MM/dd").ToString();
                gridColumn.Caption = time.ToString("yyyy/MM/dd") + Environment.NewLine + GetWeek(time);
                gridColumn.AppearanceHeader.Options.UseTextOptions = true;
                gridColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gridColumn.VisibleIndex = this.gridView1.Columns.Count;
                gridColumn.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                gridColumn.AppearanceCell.Options.UseTextOptions = true;
                gridColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
             
                gridColumns[i] = gridColumn;
                this.gridView1.Columns.Add(gridColumn);
            }
        }
        private void Sort()
        {

            //int titleWidth = this.gridView1.OptionsView
            int width = gridColumn1.Width + gridColumn2.Width + gridColumn3.Width + gridColumn4.Width;
            //foreach (var item in this.gridColumns)
            //{
            //    item
            //}
            int avgeWidth = (this.Width - width-30)/7;

            for (int i = 0; i < this.gridColumns.Length; i++)
            {
                var temp = this.gridColumns[i];
                if (temp.Caption.Contains("星期"))
                {
                    this.gridColumns[i].Width = avgeWidth;
                }
            }

        }
        /// <summary>
        /// 计算日期差
        /// </summary>
        /// <param name="StartDateTime"></param>
        /// <param name="EndDateTime"></param>
        /// <returns></returns>
        private int DateDiff(DateTime StartDateTime, DateTime EndDateTime)
        {
            int dateDiff = 0;
            try
            {
                TimeSpan ts1 = new TimeSpan(StartDateTime.Ticks);
                TimeSpan ts2 = new TimeSpan(EndDateTime.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                dateDiff = ts.Days;
            }
            catch
            {

            }
            return dateDiff;
        }
        /// <summary>
        /// 取当前日期周
        /// </summary>
        /// <param name="dtTime"></param>
        /// <returns></returns>
        private string GetWeek(DateTime dtTime)
        {
            string dt = dtTime.DayOfWeek.ToString();
            string week = string.Empty;
            switch (dt)
            {
                case "Monday":
                    week = "星期一";
                    break;
                case "Tuesday":
                    week = "星期二";
                    break;
                case "Wednesday":
                    week = "星期三";
                    break;
                case "Thursday":
                    week = "星期四";
                    break;
                case "Friday":
                    week = "星期五";
                    break;
                case "Saturday":
                    week = "星期六";
                    break;
                case "Sunday":
                    week = "星期日";
                    break;
            }
            return week;
        }
    }
}
