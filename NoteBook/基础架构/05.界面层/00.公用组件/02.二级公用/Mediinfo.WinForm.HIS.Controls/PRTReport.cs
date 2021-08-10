using DevExpress.XtraReports.UI;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 动态报表处理类
    /// </summary>
    public class PRTReport
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mediXtraReport"></param>
        public PRTReport(MediXtraReport mediXtraReport)
        {
            this.report = mediXtraReport;
        }

        #region "全局变量"

        private MediXtraReport report;

        string TotalCol = string.Empty;

        Dictionary<string, string> keyValues = new Dictionary<string, string>();

        List<string> TotalCols = new List<string>();

        List<ObjectValues> objValues = new List<ObjectValues>();

        #endregion

        /// <summary>
        /// 定位位置
        /// </summary>
        /// <param name="controlName">控件名称</param>
        /// <returns></returns>
        private Band GetBandControl(string controlName)
        {
            Band band = null;
            XRControl control = report.FindControl(controlName, true);
            if (control != null)
            {
                band = control.Band;
                TotalCol = control.Text;
                band.Controls.Remove(control);
            }
            return band;
        }

        /// <summary>
        /// 生成报表
        /// </summary>
        public void InitDetailXRTable()
        {
            //报表大小单位转换
            report.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.HundredthsOfAnInch;
            #region "动态加载内容部分[Detail]"
            //tableHeader
            Band Colband = GetBandControl("Col");
            //tableDetail
            Band Valband = GetBandControl("Val");
            if (Valband == null) return;
            Valband.HeightF = 20;
            if (Colband == null || Valband == null)
                return;
            object obj = null;
            object ObjDetailBand = report.FindControl("DetailReport", true);
            if (ObjDetailBand != null)
            {
                obj = (ObjDetailBand as DetailReportBand).DataSource;
            }
            else
            {
                obj = report.DataSource;
            }
            if (obj == null) return;

            GetObjectWeight(obj);

            if (objValues.Count > 8)
            {
                report.Landscape = true;
            }
            int TableWidth = report.PageWidth - (report.Margins.Left + report.Margins.Right);
            //创建一个列表头
            XRTable tableHeader = new XRTable();
            tableHeader.LocationF = new PointF(0, Colband.HeightF);
            tableHeader.HeightF = 20;
            tableHeader.Width = TableWidth;

            XRTableRow headerRow = new XRTableRow();
            headerRow.Width = TableWidth;
            tableHeader.Rows.Add(headerRow);

            //创建数据
            XRTable tableDetail = new XRTable();
            tableDetail.Height = 20;
            tableDetail.Width = TableWidth;

            XRTableRow detailRow = new XRTableRow();
            detailRow.Width = tableDetail.Width;
            tableDetail.Rows.Add(detailRow);

            int i = 0;
            foreach (ObjectValues item in objValues)
            {
                XRTableCell headerCell = new XRTableCell();
                headerCell.Weight = item.Weight;//列宽
                headerCell.Text = item.ColumnName;

                XRTableCell detailCell = new XRTableCell();
                detailCell.Weight = item.Weight;
                detailCell.Name = item.ColumnName;
                detailCell.DataBindings.Add("Text", null, item.ColumnName);
                headerCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                headerCell.Multiline = true;
                headerCell.Font = new System.Drawing.Font(report.Font.FontFamily, report.Font.Size, System.Drawing.FontStyle.Bold);
                detailCell.Multiline = true;
                if (report.IsDoubleClickCell == EnumShiFou.是)
                {
                    if (string.IsNullOrEmpty(report.ClickCellName))
                    {
                        detailCell.BeforePrint += DetailCell_BeforePrint;
                    }
                    else
                    {
                        if (report.ClickCellName == item.ColumnName)
                        {
                            detailCell.BeforePrint += DetailCell_BeforePrint;
                        }
                    }
                }
                //数字靠右
                if (item.IsBerth)
                {
                    detailCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                }
                else
                {
                    detailCell.ProcessDuplicatesMode = ProcessDuplicatesMode.Merge;
                    detailCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                }
                if (i == 0)
                {
                    //第一列边框[4边]
                    detailCell.Font = new System.Drawing.Font(report.Font.FontFamily, report.Font.Size, System.Drawing.FontStyle.Bold);
                    headerCell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                    detailCell.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                }
                else
                {
                    //后面的边框[3边]
                    headerCell.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                    detailCell.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                }

                //填充到对应位置
                headerRow.Cells.Add(headerCell);
                detailRow.Cells.Add(detailCell);
                i++;
            }
            ///如果有头分组,则不现实col列头
            Band GroupHeader = GetBandControl("GroupHeader");
            CreateGroupHeader(GroupHeader);
            /// 加到report中
            if (keyValues.Count > 0)
            {
                ///增加分组头分组字段排序方式
                GroupField[] groupFields = new GroupField[keyValues.Count];
                for (int Index = 0; Index < keyValues.Count; Index++)
                {
                    var item = keyValues.ElementAt(Index);
                    GroupField groupField = new GroupField();
                    groupField.FieldName = item.Key;
                    groupField.SortOrder = GetXRColumnSortOrder(item.Value);
                    groupFields[Index] = groupField;
                }
                ((GroupHeaderBand)GroupHeader).GroupFields.AddRange(groupFields);
                Band GroupHeaderBandCol = GetBandControl("GroupHeaderCol");
                ///分组头显示字段
                CreateGroupHeaderCol(GroupHeaderBandCol, TableWidth);

            }
            else
            {
                Colband.Controls.Add(tableHeader);
                if (GroupHeader != null)
                {
                    report.Bands.Remove(GroupHeader);
                }
            }
            Valband.Controls.Add(tableDetail);
            #endregion

            #region "加载分组统计部分"

            Band ToalPageband = GetBandControl("TotalPage");
            CreateTotal(ToalPageband, SummaryRunning.Page, TableWidth);
            Band Toalband = GetBandControl("TotalReport");
            CreateTotal(Toalband, SummaryRunning.Report, TableWidth);

            Band ToalGroupband = GetBandControl("TotalGroup");
            CreateTotal(ToalGroupband, SummaryRunning.Group, TableWidth);

            #endregion

        }

        private void DetailCell_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRControl control = (XRControl)sender;
            control.ForeColor = System.Drawing.Color.Black;
            control.Font = new Font(report.Font.FontFamily, report.Font.Size);
            if (control.Report.GetCurrentColumnValue(report.ClickCellName).ToString() != "合计")
            {
                control.ForeColor = System.Drawing.Color.Blue;
                control.Font = new Font(report.Font.FontFamily, report.Font.Size, System.Drawing.FontStyle.Underline);
            }
        }

        /// <summary>
        /// 分组头
        /// </summary>
        /// <param name="GroupHeaderCol"></param>
        /// <param name="TableWidth"></param>
        private void CreateGroupHeaderCol(Band GroupHeaderCol, int TableWidth)
        {
            if (GroupHeaderCol == null) return;
            GroupHeaderCol.HeightF = 20;
            if (!string.IsNullOrWhiteSpace(TotalCol))
            {
                string[] items = TotalCol.Split(',');
                if (items != null && items.Length > 0)
                {
                    XRTable tableHeader = new XRTable();
                    tableHeader.LocationF = new PointF(0, GroupHeaderCol.HeightF);
                    tableHeader.HeightF = 20;
                    tableHeader.Width = TableWidth;
                    XRTableRow headerRow = new XRTableRow();
                    headerRow.Width = TableWidth;
                    tableHeader.Rows.Add(headerRow);
                    headerRow.BackColor = Color.Gray;
                    headerRow.ForeColor = Color.White;
                    foreach (var item in items)
                    {
                        XRTableCell headerCell = new XRTableCell { Weight = (double)TableWidth / items.Count() };
                        headerCell.ExpressionBindings.AddRange(new[] {new ExpressionBinding("BeforePrint", "Text", "["+item+"]")});
                        headerCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        //填充到对应位置
                        headerRow.Cells.Add(headerCell);
                    }
                    GroupHeaderCol.Controls.Add(tableHeader);
                }
            }
            else
            {
                report.Bands.Remove(GroupHeaderCol);
            }
        }

        /// <summary>
        /// 分组头字段查找
        /// </summary>
        /// <param name="GroupHeaderBand"></param>
        private void CreateGroupHeader(Band GroupHeaderBand)
        {

            if (GroupHeaderBand == null)
                return;
            if (!string.IsNullOrWhiteSpace(TotalCol))
            {
                string[] TotalCols = TotalCol.Split('|');
                if (TotalCols.Length > 0)
                {
                    foreach (var item in TotalCols)
                    {
                        string[] items = item.Split(',');
                        if (items.Length > 0)
                        {
                            if (keyValues.ContainsKey(items[0].ToString()))
                            {
                                keyValues.Add(items[0].ToString(), items[1].ToString());
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 分组统计部分
        /// </summary>
        /// <param name="Totalband"></param>
        /// <param name="summaryRunning"></param>
        /// <param name="TableWidth"></param>
        private void CreateTotal(Band Totalband, SummaryRunning summaryRunning, int TableWidth)
        {
            if (Totalband == null) return;
            Totalband.HeightF = 20;
            GetTotalCols();
            if (TotalCols.Count > 0)
            {
                XRTable tableTotal = new XRTable();
                tableTotal.HeightF = 20;
                tableTotal.Width = TableWidth;

                XRTableRow TotaRow = new XRTableRow();
                TotaRow.Width = tableTotal.Width;
                tableTotal.Rows.Add(TotaRow);
                int j = 1;
                foreach (ObjectValues item in objValues)
                {
                    if (j == 1)
                    {
                        XRTableCell totalCell = new XRTableCell();
                        totalCell.Weight = item.Weight;
                        totalCell.Text = "合计";
                        totalCell.Font = new System.Drawing.Font(report.Font.FontFamily, report.Font.Size, System.Drawing.FontStyle.Bold);
                        totalCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                        totalCell.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                        TotaRow.Cells.Add(totalCell);
                    }
                    else
                    {
                        if (TotalCols.Contains(item.ColumnName))
                        {
                            XRTableCell totalCell = new XRTableCell();
                            XRSummary summary = new XRSummary();
                            summary.Running = summaryRunning;
                            summary.Func = DevExpress.XtraReports.UI.SummaryFunc.Sum;
                            totalCell.TextFormatString = "{0}";
                            totalCell.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([" + item.ColumnName + "])") });
                            totalCell.Summary = summary;
                            totalCell.Weight = item.Weight;
                            totalCell.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                            totalCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                            TotaRow.Cells.Add(totalCell);
                        }
                        else
                        {
                            XRTableCell totalCell = new XRTableCell();
                            totalCell.Weight = item.Weight;
                            totalCell.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                            totalCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                            TotaRow.Cells.Add(totalCell);
                        }
                    }
                    j++;
                }
                Totalband.Band.Controls.Add(tableTotal);
            }
            else
            {
                report.Bands.Remove(Totalband);
            }
        }

        private void GetTotalCols()
        {
            if (TotalCol != "" && TotalCol.Length > 0)
            {
                string[] cols = TotalCol.Split(',');
                if (cols.Length > 0)
                {
                    foreach (var item in cols)
                    {
                        TotalCols.Add(item);
                    }
                }
            }
        }

        private void GetObjectWeight(object dataObj)
        {
            double colWidth = 0;
            double Vals = 0;
            try
            {   //根据报表属性取指定列宽;例： 列名|100,列名2|100
                if (!string.IsNullOrWhiteSpace(report.CellWeight) && report.CellWeight.Length > 0)
                {
                    string[] Cells = report.CellWeight.Split(',');
                    if (Cells.Length > 0)
                    {
                        foreach (var item in Cells)
                        {
                            string[] Temp = item.Split('|');
                            if (Temp.Length > 0)
                            {
                                double Val = 0;
                                if (double.TryParse(Temp[1], out Val))
                                {
                                    ObjectValues values = new ObjectValues();
                                    values.ColumnName = Temp[0];
                                    values.Weight = Val;
                                    objValues.Add(values);
                                    Vals += Val;
                                }
                            }
                        }
                    }
                }
                //取出数据源对象里面所有列集合(List  Or   DataSet)
                if (dataObj.GetType() == typeof(DataSet) || dataObj.GetType() == typeof(DataTable))
                {
                    DataColumnCollection dataColumns = dataObj.GetType() == typeof(DataSet) ? ((DataSet)dataObj).Tables[0].Columns : ((DataTable)dataObj).Columns;
                    colWidth = (double)(report.PageWidth - (report.Margins.Left + report.Margins.Right)) / (double)dataColumns.Count;//每列宽
                    foreach (DataColumn Column in dataColumns)
                    {
                        ObjectValues @object = objValues.FirstOrDefault(p => p.ColumnName == Column.ColumnName);
                        if (@object != null)
                        {
                            @object.IsBerth = IsCheckType(Column.DataType);
                        }
                        else
                        {
                            ObjectValues values = new ObjectValues();
                            values.ColumnName = Column.ColumnName;
                            values.Weight = colWidth;
                            values.IsBerth = IsCheckType(Column.DataType);
                            objValues.Add(values);
                        }
                    }
                }
                if (dataObj.GetType().Name.Contains("List"))
                {
                    var obj = dataObj.GetType().GetProperty("Item");
                    if (obj != null)
                    {
                        var ObjItem = obj.PropertyType.GetProperties();
                        colWidth = (double)(report.PageWidth - (report.Margins.Left + report.Margins.Right) - Vals) / (double)ObjItem.Count();
                        foreach (var item in ObjItem)
                        {
                            ObjectValues @object = null;
                            foreach (var p in objValues)
                            {
                                if (p.ColumnName == item.Name)
                                {
                                    @object = p;
                                    break;
                                }
                            }

                            if (@object != null)
                            {
                                @object.IsBerth = IsCheckType(item.PropertyType);

                            }
                            else
                            {
                                ObjectValues values = new ObjectValues();
                                values.ColumnName = item.Name;
                                values.Weight = colWidth;
                                values.IsBerth = IsCheckType(item.PropertyType);
                                objValues.Add(values);
                            }
                        }

                    }
                }

            }
            catch
            {

                objValues.Clear();
            }

        }

        private bool IsCheckType(Type t)
        {
            bool IsBerth = !(t == typeof(string));
            return IsBerth;
        }

        private XRColumnSortOrder GetXRColumnSortOrder(string Sort)
        {
            XRColumnSortOrder sortOrder = XRColumnSortOrder.None;
            switch (Sort)
            {
                case "Asc":
                    sortOrder = XRColumnSortOrder.Ascending;
                    break;
                case "Desc":
                    sortOrder = XRColumnSortOrder.Descending;
                    break;
                default:
                    sortOrder = XRColumnSortOrder.None;
                    break;
            }
            return sortOrder;
        }
    }

    public class ObjectValues
    {
        public string ColumnName { set; get; }

        public double Weight { set; get; }

        public bool IsBerth { set; get; }
    }
}
