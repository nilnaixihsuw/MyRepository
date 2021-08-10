using DevExpress.Data.Filtering;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.HIS.Core;
using Mediinfo.Utility.Extensions;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 高级Gridview
    /// </summary>
    public class MediAdvBandedGridView : DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        #region 属性


        private Color colorType = Color.Red;
        /// <summary>
        /// 边框颜色（1红色0蓝色）
        /// </summary>
        [DefaultValue("1"), Description("边框颜色")]
        public Color ColorType
        {
            get { return colorType; }
            set { colorType = value; }
        }

        /// <summary>
        /// 合并后的矩形
        /// </summary>
        public Rectangle TargetRect = new Rectangle();

        private List<int> heBingList = new List<int>();
        /// <summary>
        /// 需要合并的行集合
        /// </summary>
        [Description("合并的行集合")]
        public List<int> HeBingList
        {
            get { return heBingList; }
            set { heBingList = value; }
        }

        private int rowSpace = 3;

        /// <summary>
        /// 行间距
        /// </summary>
        [DefaultValue(3), Description("行间距")]
        public int RowSpace
        {
            get
            {
                return rowSpace;
            }
            set
            {
                if (rowSpace != value)
                {
                    rowSpace = value;
                }
            }
        }

        /// <summary>
        /// 获取用户选择行数据
        /// </summary>
        [Browsable(false)]
        public DataRow GetDataRowInfo { get; set; }

        /// <summary>
        /// 返回选择行数组
        /// </summary>
        [Browsable(false)]
        public DataRow[] GetDataRowList { get; set; }

        /// <summary>
        /// 返回用户选择行索引
        /// </summary>
        [Browsable(false)]
        public int[] GetList { get; set; }

        /// <summary>
        /// 是否显示行号
        /// </summary>
        private bool _IsShowLineNumber = false;

        /// <summary>
        /// 是否显示行号
        /// </summary>
        [DefaultValue(false), DescriptionAttribute("是否显示行号")]
        public bool IsShowLineNumber
        {
            get { return _IsShowLineNumber; }
            set
            {
                _IsShowLineNumber = value;
                if (!SkinCat.Instance.IsDesignMode)
                    SetSerialNo();
            }
        }

        private string _RequiredFieldItem = "";

        /// <summary>
        /// 非空字段设置
        /// </summary>
        [DescriptionAttribute("非空字段设置，多个字段以逗号隔开")]
        [Browsable(true)]
        [Editor(typeof(GridViewEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string RequiredFieldItem
        {
            get { return _RequiredFieldItem; }
            set { _RequiredFieldItem = value; }
        }

        #endregion 属性

        #region 构造函数

        /// <summary>
        /// 初始化构造函数
        /// </summary>
        /// <param name="ownerGrid"></param>
        public MediAdvBandedGridView(GridControl ownerGrid) : base(ownerGrid)
        {
            GlobelAttribute();
            InitGridViewEx();
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
            HeBingList = new List<int>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MediAdvBandedGridView()
        {
            InitGridViewEx();
            this.Focus();
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
            //if(!SkinCat.Instance.IsDesignMode)
            //   _GYDataLayoutService = ServiceFactory.Create<GYDataLayoutService>();
        }

        #endregion 构造函数

        #region 初始化样式

        /// <summary>
        /// 初始化
        /// </summary>
        public void InitGridViewEx()
        {
            //属性设置
            GridViewAttributeSet();

            //设置行样式
            RowAppearenceSet();

            //皮肤相关
            GridViewSkinSet();
        }

        private void GlobelAttribute()
        {
            this.OptionsView.ShowFooter = true;
            //滚动条设置
            //if (_ScrollBars == ScrollBars.Horizontal || _ScrollBars == ScrollBars.Both)
            //    this.OptionsView.ColumnAutoWidth = false;
            //else
            //    this.OptionsView.ColumnAutoWidth = true;
            this.OptionsView.ColumnAutoWidth = false;

            this.OptionsDetail.EnableMasterViewMode = false;

            //this.BestFitColumns();

            //列头背景色
            //this.PaintStyleName = "web";

            //焦点框在单元格里面
            // this.FocusRectStyle = DrawFocusRectStyle.CellFocus;

            //选择整行
            this.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            //this.OptionsSelection.EnableAppearanceFocusedCell = false;

            this.OptionsNavigation.EnterMoveNextColumn = true;
            this.OptionsNavigation.AutoFocusNewRow = true;

            //是否显示左侧面板
            this.OptionsView.ShowIndicator = false;

            //焦点框在单元格里面
            this.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;

            //获取或设置最终用户是否允许调用单元格编辑器。
            this.OptionsBehavior.Editable = true;
            //this.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDown;

            // this.OptionsBehavior.EditingMode = GridEditingMode.Default;

            //获取或设置最终用户是否可以调用列表头上下文菜单
            this.OptionsMenu.EnableColumnMenu = true;

            //是否允许多选
            this.OptionsSelection.MultiSelect = true;

            //多选模式
            this.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;

            //隐藏分组面板
            this.OptionsView.ShowGroupPanel = false;
        }

        /// <summary>
        /// GridView属性设置
        /// </summary>
        public void GridViewAttributeSet()
        {
            this.OptionsDetail.EnableMasterViewMode = false;

            this.BorderStyle = BorderStyles.NoBorder;

            //获取或设置是否启用焦点单元格的外观设置。
            //this.OptionsSelection.EnableAppearanceFocusedCell = true;
            //this.Appearance.FocusedCell.Options.UseBackColor = true;
            //this.Appearance.FocusedCell.Options.UseForeColor = true;
            //this.Appearance.FocusedCell.BackColor = Color.FromArgb(30, 186, 255);
            //this.Appearance.FocusedCell.ForeColor = Color.White;

            //列头居中显示
            this.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Appearance.HeaderPanel.Options.UseFont = false;
            //this.Appearance.HeaderPanel.Font = new Font(AppearanceObject.DefaultFont.FontFamily.Name, this.Appearance.HeaderPanel.Font.Size);

            //行居中显示
            //this.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            //this.BorderStyle = BorderStyles.NoBorder;
            //this.Appearance.Row.Options.UseFont = true;
            //this.Appearance.Row.Font = new Font("微软雅黑", this.Appearance.FocusedRow.Font.Size);

            //this.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Regular;

            //显示页脚

            //this.OptionsView.ShowFooter = true;
            //this.FocusedRowHandle = 0;
            //this.Focus();
            //显示筛选
            //this.OptionsView.ShowAutoFilterRow = true;

            //隐藏提示
            //this.OptionsView.ShowGroupPanel = false;

            //this.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never; //是否显示过滤面板

            //this.OptionsCustomization.AllowColumnMoving = false; //是否允许移动列
            //this.OptionsCustomization.AllowColumnResizing = false; //是否允许调整列宽
            //this.OptionsCustomization.AllowGroup = false; //是否允许分组
            //this.OptionsCustomization.AllowFilter = false; //是否允许过滤
            //this.OptionsCustomization.AllowSort = true; //是否允许排序
        }

        /// <summary>
        /// 皮肤相关
        /// </summary>
        private void GridViewSkinSet()
        {
            //以下样式在皮肤中设置
            //奇偶行设置
            //this.Appearance.EvenRow.Options.UseBackColor = true;
            //this.Appearance.EvenRow.BackColor = Color.FromArgb(245, 245, 245);// ColorTranslator.FromHtml("#f2f2f2");

            //this.Appearance.OddRow.Options.UseBackColor = true;
            //this.Appearance.OddRow.BackColor = Color.FromArgb(255, 255, 255);
            //焦点行的颜色
            //this.OptionsSelection.EnableAppearanceFocusedRow = true;
            //this.Appearance.FocusedRow.Options.UseForeColor = true;
            //this.Appearance.FocusedRow.Options.UseBackColor = true;
            //this.Appearance.FocusedRow.Options.UseFont = true;
            //this.Appearance.FocusedRow.BackColor = Color.FromArgb(10, 163, 230);// ColorTranslator.FromHtml("#199ed8");
            //this.Appearance.FocusedRow.ForeColor = Color.White;
            //this.Appearance.FocusedRow.Font = new Font(AppearanceObject.DefaultFont.FontFamily.Name, this.Appearance.FocusedRow.Font.Size, this.Appearance.FocusedRow.Font.Style);

            //选择行颜色
            //this.Appearance.SelectedRow.Options.UseBackColor = true;
            //this.Appearance.SelectedRow.Options.UseForeColor = true;
            //this.Appearance.SelectedRow.BackColor = Color.FromArgb(10, 163, 230);
            //this.Appearance.SelectedRow.ForeColor = Color.White;

            //头背景色
            //this.Appearance.HeaderPanel.Options.UseBackColor = true;
            //this.Appearance.HeaderPanel.BackColor = ColorTranslator.FromHtml("#efefef");

            //横线
            //this.Appearance.HorzLine.BackColor = ColorTranslator.FromHtml("#efefef");
            //this.Appearance.VertLine.BackColor = ColorTranslator.FromHtml("#f2f2f2");
            //this.Appearance.VertLine.BackColor = Color.White;
        }

        /// <summary>
        /// 设置行外观
        /// </summary>
        private void RowAppearenceSet()
        {
            //奇偶行
            this.OptionsView.EnableAppearanceEvenRow = true;
            this.OptionsView.EnableAppearanceOddRow = true;
            //行高
            //this.OptionsView.RowAutoHeight = false;
            //this.RowHeight = 24;
            //头高度
            this.ColumnPanelRowHeight = 24;
            //表格线
            this.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.True;
            this.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.True;
        }

        /// <summary>
        /// 设置单元格颜色
        /// </summary>
        //private void SetStyleColor()
        //{
        //    if (_EDataLayout2 != null)
        //   {
        //        var list = _EDataLayout2.Where(o => o.BACKCOLOREXPRISSION != null).ToList();
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            string fieldName = list[i].FIELDNAME;
        //            string expression = list[i].BACKCOLOREXPRISSION;

        //            GridFormatRule gridFormatRule = new GridFormatRule();
        //            FormatConditionRuleExpression formatConditionRuleExpression = new FormatConditionRuleExpression();
        //            GridColumn columnName = this.Columns[fieldName];

        //            //var temp = expression.Split('|');
        //            //string PredefinedName = "Red Fill, Red Text";
        //            //if (temp.Count() >= 2)
        //            //{
        //            //    expression = temp[0];
        //            //    PredefinedName = temp[1];
        //            //    PredefinedName = PredefinedName + " Fill, " + PredefinedName + " Text";
        //            //    gridFormatRule.Column = columnName;
        //            //    gridFormatRule.ApplyToRow = true;
        //            //    formatConditionRuleExpression.PredefinedName = PredefinedName;
        //            //    formatConditionRuleExpression.Expression = expression;
        //            //}
        //            //else
        //            //{
        //            gridFormatRule.Column = columnName;
        //            gridFormatRule.ApplyToRow = true;
        //            //formatConditionRuleExpression.PredefinedName = "Lime Fill, Lime Text";
        //            formatConditionRuleExpression.PredefinedName = "Red Fill, Red Text";
        //            formatConditionRuleExpression.Expression = expression;
        //            //}
        //            gridFormatRule.Rule = formatConditionRuleExpression;
        //            this.FormatRules.Add(gridFormatRule);
        //        }
        //    }

        //}

        //设置ColumnFormat
        //private void SetColumnFormatCell(object value1, object value2, Color backColor1, Color backColor2, GridColumn gridColumn, FormatConditionEnum formatType, GridView gridView)
        //{
        //    var styleFormatCondition1 = new StyleFormatCondition();
        //    styleFormatCondition1.Appearance.BackColor = backColor1;
        //    styleFormatCondition1.Appearance.BackColor2 = backColor2;
        //    styleFormatCondition1.Appearance.Options.UseBackColor = true;
        //    styleFormatCondition1.Column = gridColumn;
        //    styleFormatCondition1.Condition = formatType;
        //    styleFormatCondition1.Expression = "true";
        //    styleFormatCondition1.Value1 = value1;
        //    styleFormatCondition1.Value2 = value2;
        //    gridView.FormatConditions.Add(styleFormatCondition1);
        //}

        #endregion 初始化样式

        #region 全局变量

        /// <summary>
        /// 存储布局信息
        /// </summary>
        private E_GY_DATALAYOUT1 _EDataLayout1 = null;

        /// <summary>
        /// 存储布局信息详情
        /// </summary>
        private List<E_GY_DATALAYOUT2> _EDataLayout2 = null;

        /// <summary>
        /// 创建服务实例
        /// </summary>
       // private GYDataLayoutService _GYDataLayoutService = null;

        /// <summary>
        /// 存储非空字段项
        /// </summary>
        private List<string> _NonEmptyFields = new List<string>();

        /// <summary>
        /// 数据集
        /// </summary>
        private DTOBase _DTOBase;

        #endregion 全局变量

        #region 公共方法

        /// <summary>
        /// 添加非空字段
        /// </summary>
        /// <param name="fieldName"></param>
        public void AddRequiredField(string fieldName)
        {
            if (fieldName.IsNullOrEmpty()) return;

            if (_NonEmptyFields.Count == 0)
                _NonEmptyFields.Add(fieldName.ToUpper());
            else
            {
                if (!_NonEmptyFields.Contains(fieldName.ToUpper()))
                    _NonEmptyFields.Add(fieldName.ToUpper());
            }
            FormatRequiredField();
        }

        /// <summary>
        /// 清除非空项
        /// </summary>
        public void ResetRequiredFields()
        {
            if (_NonEmptyFields != null && _NonEmptyFields.Count > 0)
            {
                _NonEmptyFields.Clear();
            }
        }

        /// <summary>
        /// 保存时验证
        /// </summary>
        /// <returns></returns>
        public bool CheckRequiredFields(bool trimSpace = true, bool showErrorMsg = true)
        {
            if (this.DataRowCount == 0) return true;

            //存储必填项提示信息
            List<string> requiredFieldsMsg = new List<string>();
            //存储第一个非空项
            BandedGridColumn gridColumnFirst = null;
            //记录验证失败的首行
            int indexFirst = 0;

            #region 非空项验证 有效性检查 非空表达式

            for (int i = 0; i < this.DataRowCount; i++)
            {
                DTOBase dtoBase = this.GetRow(i) as DTOBase;
                if (dtoBase.GetState() == DTOState.New || dtoBase.GetState() == DTOState.Update)
                {
                    BandedGridColumnCollection gridColumnCollection = this.Columns;

                    foreach (BandedGridColumn gridColumn in gridColumnCollection)
                    {
                        object cellValue = this.GetRowCellValue(i, gridColumn);

                        string captionName = gridColumn.Caption;

                        #region 非空项验证

                        if (_NonEmptyFields != null && _NonEmptyFields.Count > 0)
                        {
                            if (_NonEmptyFields.Contains(gridColumn.FieldName.ToUpper()))
                            {
                                if (cellValue == null || (trimSpace ? cellValue.ToString().Trim().IsNullOrWhiteSpace() : cellValue.ToString().IsNullOrEmpty()))
                                {
                                    requiredFieldsMsg.Add(string.Format("第{0}行，【{1}】列，不能为空！\r\n", (i + 1).ToString(), captionName));
                                    if (requiredFieldsMsg.Count == 1)
                                    {
                                        gridColumnFirst = gridColumn;
                                        indexFirst = i;
                                    }
                                }
                            }
                        }

                        #endregion 非空项验证

                        #region 有效性检查、非空表达式

                        if (_EDataLayout2 != null && _EDataLayout2.Count > 0)
                        {
                            List<E_GY_DATALAYOUT2> eDataLayout2 = _EDataLayout2.Where(e => e.FIELDNAME.ToUpper() == gridColumn.FieldName.ToUpper()).ToList();

                            if (eDataLayout2 != null && eDataLayout2.Count > 0)
                            {
                                E_GY_DATALAYOUT2 e = eDataLayout2[0];

                                //非空表达式验证
                                if (!e.NONEMPTY.IsNullOrWhiteSpace())
                                {
                                    try
                                    {
                                        Exception evaluatorCreateException = null;
                                        //计算表达式
                                        ExpressionEvaluator expressionEvaluator;
                                        expressionEvaluator = gridColumn.View.DataController.CreateExpressionEvaluator(CriteriaOperator.TryParse(e.NONEMPTY.ToString()), true, out evaluatorCreateException);
                                        int indexrow = gridColumn.View.GetDataSourceRowIndex(i);
                                        object obj = expressionEvaluator.Evaluate(indexrow);
                                        if (obj != null)
                                        {
                                            bool b = Convert.ToBoolean(obj);
                                            if (!b)
                                            {
                                                requiredFieldsMsg.Add(string.Format("第{0}行，【{1}】列，验证失败！\r\n", (i + 1).ToString(), captionName));
                                                if (requiredFieldsMsg.Count == 1)
                                                {
                                                    gridColumnFirst = gridColumn;
                                                    indexFirst = i;
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    { }
                                }

                                //正则表达式验证
                                if (!e.VALIDATEEXPRISSION.IsNullOrWhiteSpace())
                                {
                                    if (cellValue != null)
                                    {
                                        Regex reg = new Regex(e.VALIDATEEXPRISSION);
                                        Match m = reg.Match(cellValue.ToString().Trim());
                                        if (!m.Success)
                                        {
                                            requiredFieldsMsg.Add(string.Format("第{0}行，【{1}】列，验证失败！\r\n", (i + 1).ToString(), captionName));
                                            if (requiredFieldsMsg.Count == 1)
                                            {
                                                gridColumnFirst = gridColumn;
                                                indexFirst = i;
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion 有效性检查、非空表达式
                        }
                    }
                }
            }

            #endregion 非空项验证 有效性检查 非空表达式

            #region 处理错误信息

            if (requiredFieldsMsg.Count == 0)
                return true;
            else
            {
                if (showErrorMsg)
                {
                    string msg = "";
                    requiredFieldsMsg.ForEach(o =>
                    {
                        msg += o;
                    });
                    MediMsgBox.Show(msg);
                }
                this.Focus();
                this.FocusedColumn = gridColumnFirst;

                int selectIndex = this.GetFocusedDataSourceRowIndex();
                int moveRows = indexFirst - selectIndex;
                this.MoveBy(moveRows);
                return false;
            }

            #endregion 处理错误信息
        }

        /// <summary>
        /// 设置字段默认值 需先调用此方法再调用AddNew
        /// </summary>
        /// <typeparam name="T">DTOBase</typeparam>
        /// <param name="t">数据值</param>
        public void SetFieldDefaultValue<T>(T t) where T : DTOBase
        {
            if (_EDataLayout2 == null || _EDataLayout2.Count == 0)
            {
                _DTOBase = t; return;
            }

            Type type = typeof(T);

            foreach (PropertyInfo p in type.GetProperties())
            {
                foreach (E_GY_DATALAYOUT2 e in _EDataLayout2)
                {
                    if (e.DEFAULTVALUE.IsNullOrEmpty()) continue;

                    if (e.FIELDNAME.ToUpper() == p.Name.ToUpper())
                    {
                        Type typeValue = p.PropertyType;
                        p.SetValue(t, ConvertTo(e.DEFAULTVALUE, typeValue), null);//Convert.ChangeType(e.DEFAULTVALUE,p.PropertyType));
                        break;
                    }
                }
            }
            _DTOBase = t;

            //SetStyleColor();
        }

        /// <summary>
        /// 转化类型
        /// </summary>
        /// <param name="convertibleValue"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private object ConvertTo(IConvertible convertibleValue, Type t)
        {
            if (string.IsNullOrEmpty(convertibleValue.ToString()))
            {
                return null;
            }
            if (!t.IsGenericType)
            {
                return Convert.ChangeType(convertibleValue, t);
            }
            else
            {
                Type genericTypeDefinition = t.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(Nullable<>))
                {
                    return Convert.ChangeType(convertibleValue, Nullable.GetUnderlyingType(t));
                }
            }
            return null;
            //throw new InvalidCastException(string.Format("Invalid cast from type \"{0}\" to type \"{1}\".", convertibleValue.GetType().FullName, typeof(T).FullName));
        }

        /// <summary>
        /// 设置可编辑状态
        /// </summary>
        public bool EditableState
        {
            get
            {
                return this.OptionsBehavior.Editable;
            }
            set
            {
                this.OptionsBehavior.Editable = value;
                this.OptionsSelection.EnableAppearanceFocusedCell = value;
            }
        }

        /// <summary>
        /// 设置焦点行
        /// </summary>
        /// <param name="rowHandle"></param>
        public void SetFocusedRow(int rowHandle)
        {
            foreach (var row in this.GetSelectedRows())
                this.UnselectRow(row);

            this.FocusedRowHandle = (rowHandle > 0 ? rowHandle : 0);
            this.SelectRows(this.FocusedRowHandle, this.FocusedRowHandle);
        }

        /// <summary>
        /// 清除选中行
        /// </summary>
        /// <param name="rowHandle"></param>
        public void ClearFocuseRow(int rowHandle = -1)
        {
            if (rowHandle == -1)
            {
                foreach (var row in this.GetSelectedRows())
                    this.UnselectRow(row);
            }
            else
                this.UnselectRow(rowHandle);
        }

        /// <summary>
        /// 设置焦点单元格
        /// </summary>
        /// <param name="fieldName"></param>
        public void SetFocusedColumn(string fieldName)
        {
            if (fieldName.IsNullOrEmpty()) return;
            this.OptionsNavigation.AutoFocusNewRow = true;
            this.FocusedColumn = this.Columns[fieldName];
            this.ShowEditor();
        }

        #endregion 公共方法

        #region 行号

        /// <summary>
        /// 设置行号
        /// </summary>
        private void SetSerialNo()
        {
            BandedGridColumnCollection gridColumnCollection = this.Columns;

            bool exist = false;
            for (int i = 0; i < gridColumnCollection.Count; i++)
            {
                BandedGridColumn gridColumn = gridColumnCollection[i];

                if (gridColumn.FieldName.IsNullOrWhiteSpace() && gridColumn.VisibleIndex == -1)
                {
                    exist = true;
                    break;
                }

                if (gridColumn.FieldName.ToUpper() == "SERIALNO")
                {
                    exist = true;
                    //gridColumn.VisibleIndex = 0;
                    if (_IsShowLineNumber)
                        gridColumn.Visible = true;
                    else
                        gridColumn.Visible = false;
                    break;
                }
            }

            if (exist) return;

            AddSerialNO();
        }

        /// <summary>
        /// 添加序列号 初始化一次
        /// </summary>
        private void AddSerialNO()
        {
            if (!_IsShowLineNumber) return;

            GridBand gridBand1 = new GridBand();
            BandedGridColumn colSerialNum = new BandedGridColumn();
            this.Bands.Insert(0, gridBand1);
            gridBand1.AppearanceHeader.Options.UseTextOptions = true;
            gridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridBand1.Width = 35;
            gridBand1.Caption = "";
            gridBand1.Columns.Insert(0, colSerialNum);
            gridBand1.Name = "gridBand1";
            gridBand1.VisibleIndex = 0;
            //gridBand1.Fixed = FixedStyle.Left;
            //gridBand1.View.BestFitColumns();
            colSerialNum.Width = 35;
            colSerialNum.FieldName = "SerialNo";//列绑定字段
            colSerialNum.Caption = "序号";//列名称
            colSerialNum.Name = "colSerialNo";
            colSerialNum.Visible = true;
            colSerialNum.VisibleIndex = 0;

            colSerialNum.GetBestWidth();

            colSerialNum.AppearanceHeader.Options.UseTextOptions = true;
            colSerialNum.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            colSerialNum.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;

            colSerialNum.AppearanceCell.Options.UseTextOptions = true;
            colSerialNum.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            colSerialNum.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            //是否可以拖动列标题
            colSerialNum.OptionsColumn.AllowMove = false;
            //固定列头
            colSerialNum.Fixed = FixedStyle.None;
            //排序
            colSerialNum.OptionsColumn.AllowSort = DefaultBoolean.False;
            colSerialNum.OptionsColumn.AllowEdit = false;
            colSerialNum.OptionsColumn.AllowFocus = false;

            colSerialNum.OptionsFilter.AllowFilter = false;
            colSerialNum.OptionsColumn.AllowMerge = DefaultBoolean.False;
            this.Columns.Insert(0, colSerialNum);
        }

        #endregion 行号

        #region 列属性 加载数据库设置

        /// <summary>
        /// 格式化非空项样式
        /// </summary>
        private void FormatRequiredField()
        {
            //程序设置的非空项
            if (_NonEmptyFields != null && _NonEmptyFields.Count > 0)
            {
                //列集合
                BandedGridColumnCollection columnColl = this.Columns;
                if (columnColl == null || columnColl.Count == 0) return;

                foreach (BandedGridColumn col in columnColl)
                {
                    if (_NonEmptyFields.Contains(col.FieldName.ToUpper()))
                    {
                        col.AppearanceHeader.Options.UseForeColor = true;
                        col.AppearanceHeader.ForeColor = Color.Red;
                    }
                }
            }
        }

        /// <summary>
        /// 获取数据布局信息从数据库
        /// </summary>
        private void GetDataLayoutForDB()
        {
            MediGridControl grid = this.GridControl as MediGridControl;
            if (grid == null) return;
            Form form = grid.FindForm();

            if (form == null) return;

            //Stopwatch watch = new Stopwatch();

            //watch.Start();

            string sFormName = grid.FindForm().Name.ToString();
            string sNameSpace = grid.FindForm().GetType().Namespace;
            //设置界面就不在取了
            if (sFormName == "FrmDataLayoutSet" || sFormName == "FrmButtonSetting")
            {
                return;
            }
            // var service = ServiceFactory.Create<GYDataLayoutService>();

            //var ret = service.GetDataLayoutInfo(this.Name, sFormName, sFormName, HISClientHelper.YINGYONGID);

            //var ret = GYDataLayoutHelper.GetDataLayoutInfo(this.Name, sFormName, sNameSpace, HISClientHelper.YINGYONGID);

            //if (ret != null)
            //{
            //    _EDataLayout1 = ret.DataLayout1;
            //    _EDataLayout2 = ret.DataLayout2;
            //}

            //watch.Stop();
            //Console.WriteLine("GridView service.GetDataLayoutInfo:{0}", watch.ElapsedTimeString());
        }

        /// <summary>
        /// 运行时处理列绑定问题
        /// </summary>
        private void RunViewColunmProcess()
        {
            if (_EDataLayout2 == null || _EDataLayout2.Count == 0)
                return;

            //列集合
            BandedGridColumnCollection columnColl = this.Columns;

            if (columnColl == null || columnColl.Count == 0) return;

            //遍历gridview中包含的所有列
            for (int i = 0; i < columnColl.Count; i++)
            {
                BandedGridColumn gridColumn = columnColl[i];
                foreach (E_GY_DATALAYOUT2 eDataLayout2 in _EDataLayout2)
                {
                    if (gridColumn.FieldName.ToUpper() == eDataLayout2.FIELDNAME.ToUpper())
                    {
                        SetColunmStyleByPara(ref gridColumn, eDataLayout2);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 根据参数设置行样式
        /// </summary>
        /// <param name="col"></param>
        /// <param name="cm"></param>
        private void SetColunmStyleByPara(ref BandedGridColumn col, E_GY_DATALAYOUT2 eDataLayout2)
        {
            //列宽度
            col.Width = eDataLayout2.WIDTH.ToInt(100);
            //是否可以拖动列标题
            col.OptionsColumn.AllowMove = false;
            //列头固定
            col.Fixed = eDataLayout2.FIXED.ToInt(0).ToEnum<FixedStyle>();
            //排序号
            col.VisibleIndex = eDataLayout2.SORTNO.ToInt(0);
            //显示标志
            col.Visible = eDataLayout2.VISIBLE.ToInt(1) == 0 ? false : true;
            //中文名称
            col.Caption = eDataLayout2.CAPTION;
            //受保护
            col.OptionsColumn.AllowEdit = eDataLayout2.READONLY.ToInt(0) == 0 ? true : false;
            //单元格文本对齐方式
            col.AppearanceCell.Options.UseTextOptions = true;
            col.AppearanceCell.TextOptions.HAlignment = eDataLayout2.CELLHALIGNMENT.ToInt(0).ToEnum<DevExpress.Utils.HorzAlignment>();
            //单元格字体大小
            if (eDataLayout2.CELLFONTSIZE.HasValue)
            {
                col.AppearanceCell.Options.UseFont = true;
                col.AppearanceCell.Font = new Font(AppearanceObject.DefaultFont.FontFamily.Name, eDataLayout2.CELLFONTSIZE.ToInt(9));
            }
            //非空表示式 编辑时生效
            if (!eDataLayout2.NONEMPTY.ToString().IsNullOrEmpty())
            {
                col.AppearanceHeader.Options.UseForeColor = true;
                col.AppearanceHeader.ForeColor = Color.Red;
            }
            //输入法模式 编辑时生效

            //跳转顺序 编辑时生效

            //显示格式
            col.DisplayFormat.FormatString = eDataLayout2.FORMATSTRING;
            //显示格式类型
            col.DisplayFormat.FormatType = eDataLayout2.FORMATTYPE.ToInt(0).ToEnum<FormatType>();
            //头标题对齐方式
            col.AppearanceHeader.Options.UseTextOptions = true;
            col.AppearanceHeader.TextOptions.HAlignment = eDataLayout2.HEADERHALIGNMENT.ToInt(0).ToEnum<HorzAlignment>();
            //头字体大小
            if (eDataLayout2.HEADERFONTSIZE.HasValue)
            {
                col.AppearanceHeader.Options.UseFont = true;
                col.AppearanceHeader.Font = new Font(col.AppearanceHeader.Font.FontFamily, eDataLayout2.HEADERFONTSIZE.ToInt(9));
            }
            //排序列设置
            col.SortMode = ColumnSortMode.Custom;
            if (_EDataLayout1 != null && !_EDataLayout1.ORDERBYFIELD.IsNullOrWhiteSpace())
            {
                string[] arryFieldSort = _EDataLayout1.ORDERBYFIELD.Trim().Replace("，", ",").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                if (arryFieldSort == null || arryFieldSort.Length == 0) return;

                foreach (string sort in arryFieldSort)
                {
                    if (sort.IsNullOrWhiteSpace()) continue;

                    string[] arry = sort.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

                    if (arry == null || arry.Length == 0 || arry.Length != 2) continue;

                    if (arry[0].ToUpper() == col.FieldName.ToUpper())
                    {
                        if (arry[1].ToUpper() == "ASC")
                            col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                        else if (arry[1].ToUpper() == "DESC")
                            col.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                        break;
                    }
                }
            }
        }

        #endregion 列属性 加载数据库设置

        #region 行属性绑定

        /// <summary>
        /// 绑定行属性
        /// </summary>
        private void RowAttributeSetForDB()
        {
            if (_EDataLayout1 == null) return;

            // E_GY_DATALAYOUT1 eDataLayout1 = _EDataLayout1[0];
            //行号
            _IsShowLineNumber = _EDataLayout1.LINENUMBER.ToInt(1) == 0 ? false : true;
            //行字体
            if (_EDataLayout1.ROWFONTSIZE.HasValue)
            {
                this.Appearance.Row.Options.UseFont = true;
                this.Appearance.Row.Font = new Font(this.Appearance.Row.Font.FontFamily, _EDataLayout1.ROWFONTSIZE.ToInt(9));
            }
            //是否显示分组面板
            this.OptionsView.ShowGroupPanel = _EDataLayout1.SHOWGROUPPANEL.ToInt(0) == 0 ? false : true;
            //是否允许过滤
            this.OptionsCustomization.AllowFilter = _EDataLayout1.ALLOWFILTER.ToInt(1) == 0 ? false : true;
            //是否允许排序
            this.OptionsCustomization.AllowSort = _EDataLayout1.ALLOWSORT.ToInt(1) == 0 ? false : true;
            //是显示列菜单
            this.OptionsMenu.EnableColumnMenu = _EDataLayout1.ENABLECOLUMNMENU.ToInt(1) == 0 ? false : true;
        }

        #endregion 行属性绑定

        #region 重写初始化页面

        /// <summary>
        /// 初始化一些默认值
        /// </summary>
        public override void BeginInit()
        {
            base.BeginInit();
        }

        /// <summary>
        /// 结束初始化
        /// </summary>
        public override void EndInit()
        {
            RegisterEvent();

            if (!SkinCat.Instance.IsDesignMode)
            {
                SetSerialNo();
                GetDataLayoutForDB();
                RunViewColunmProcess();
                RowAttributeSetForDB();
            }
            base.EndInit();
        }

        #endregion 重写初始化页面

        #region 事件相关

        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvent()
        {
            //注册行号事件
            this.CustomDrawRowIndicator -= GridView_CustomDrawRowIndicator;
            this.CustomDrawRowIndicator += GridView_CustomDrawRowIndicator;
            //注册单元格样式事件
            this.CustomColumnDisplayText -= GridView_CustomColumnDisplayText;
            this.CustomColumnDisplayText += GridView_CustomColumnDisplayText;
            //行外观事件
            this.RowStyle -= GridView_RowStyle;
            this.RowStyle += GridView_RowStyle;

            //弹出菜单设置
            this.PopupMenuShowing -= GridView_PopupMenuShowing;
            this.PopupMenuShowing += GridView_PopupMenuShowing;
            //必输项验证
            this.InvalidRowException -= gridview_InvalidRowException;
            this.InvalidRowException += gridview_InvalidRowException;
            //选择事件
            this.SelectionChanged -= MyGridView_SelectionChanged;
            this.SelectionChanged += MyGridView_SelectionChanged;
            //单元格数据改变事件
            this.CellValueChanged -= gridview_CellValueChanged;
            this.CellValueChanged += gridview_CellValueChanged;
            //右键双击事件
            this.DoubleClick -= GridViewEx_DoubleClick;
            this.DoubleClick += GridViewEx_DoubleClick;
            //右键点击
            this.Click -= GridViewEx_Click;
            this.Click += GridViewEx_Click;

            //计算行高
            this.CalcRowHeight -= MediGridView_CalcRowHeight;
            this.CalcRowHeight += MediGridView_CalcRowHeight;
            //设置默认值
            this.InitNewRow -= MediGridView_InitNewRow;
            this.InitNewRow += MediGridView_InitNewRow;
            this.CustomDrawCell -= MediGridView_CustomDrawCell;
            this.CustomDrawCell += MediGridView_CustomDrawCell;

            //新增行事件
            MediGridControl grid = this.GridControl as MediGridControl;
            if (this.GridControl.DataSource != null)
            {
                (this.GridControl.DataSource as BindingSource).AddingNew -= MediGridView_AddingNew;
                (this.GridControl.DataSource as BindingSource).AddingNew += MediGridView_AddingNew;
            }
        }

        /// <summary>
        /// 单元格样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediGridView_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            ////if (e.Column.FieldName == "ZHIGONGGH")
            ////{
            ////    if (e.CellValue != null && e.CellValue.ToString() == "3")
            ////    {
            ////        e.Appearance.Options.UseForeColor = true;
            ////        e.Appearance.ForeColor = Color.Red;
            ////    }
            ////}

            //if (_EDataLayout2 == null || _EDataLayout2.Count == 0) return;
            ////单元格背景颜色
            //List<E_GY_DATALAYOUT2> list = _EDataLayout2.Where(o => o.FIELDNAME.ToUpper() == e.Column.FieldName.ToUpper()).ToList();

            //if (list == null || list.Count == 0) return;

            //Color color = new Color();
            ////单元格字体颜色
            ////if (!list[0].CELLFORECOLOREXPRISSION.IsNullOrEmpty())
            ////{
            ////    if (ExpressionToColor(e.Column, e.RowHandle, list[0].CELLFORECOLOREXPRISSION, ref color))
            ////    {
            ////        e.Appearance.Options.UseForeColor = true;
            ////        e.Appearance.ForeColor = color;
            ////    }
            ////}

            //////单元格背景颜色
            ////if (!list[0].BACKCOLOREXPRISSION.IsNullOrEmpty())
            ////{
            ////    if (ExpressionToColor(e.Column, e.RowHandle, list[0].BACKCOLOREXPRISSION, ref color))
            ////    {
            ////        e.Appearance.Options.UseBackColor = true;
            ////        e.Appearance.BackColor = color;
            ////    }
            ////}

            // modify by songxl on 2020-3-5 没有修改具体的逻辑，只是对流程中增加了一些实例为空的判断，使其更严谨
            if (sender is GridView view)
            {
                if (view.GetViewInfo() is GridViewInfo vi)
                {
                    GridCellInfo gridCellInfo2 = vi.GetGridCellInfo(e.RowHandle, view.VisibleColumns[view.VisibleColumns.Count - 1]);
                    var gridCellInfo1 = vi.GetGridCellInfo(e.RowHandle, _IsShowLineNumber ? view.VisibleColumns[1] : view.VisibleColumns[0]);
                    if (gridCellInfo1 != null && gridCellInfo2 != null)
                    {
                        TargetRect = Rectangle.Union(gridCellInfo1.Bounds, gridCellInfo2.Bounds);
                    }
                }
            }

            var type = colorType;

            if (HeBingList == null || !HeBingList.Contains(e.RowHandle)) return;
            DrawCellBorder(e, type);
            e.Handled = true;
        }

        /// <summary>
        /// 表达式转化为
        /// </summary>
        /// <param name="gridColumn"></param>
        /// <param name="rowHandle"></param>
        /// <param name="expression"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private bool ExpressionToColor(BandedGridColumn gridColumn, int rowHandle, string expression, ref Color color, bool bGridColumn = true)
        {
            try
            {
                Exception evaluatorCreateException = null;
                //计算表达式
                ExpressionEvaluator expressionEvaluator;

                object obj = null;

                if (bGridColumn)
                {
                    expressionEvaluator = gridColumn.View.DataController.CreateExpressionEvaluator(CriteriaOperator.TryParse(expression), true, out evaluatorCreateException);
                    int indexrow = gridColumn.View.GetDataSourceRowIndex(rowHandle);
                    obj = expressionEvaluator.Evaluate(indexrow);
                }
                else
                {
                    //计算表达式
                    expressionEvaluator = this.Columns.View.DataController.CreateExpressionEvaluator(CriteriaOperator.TryParse(expression), true, out evaluatorCreateException);
                    int indexrow = this.GetDataSourceRowIndex(rowHandle);
                    obj = expressionEvaluator.Evaluate(indexrow);
                }

                if (obj != null)
                {
                    #region 判断颜色值

                    string colorString = obj.ToString();
                    if (colorString.IsNullOrWhiteSpace()) return false;
                    else
                    {
                        if (colorString.IndexOf("#") > -1)
                        {
                            color = ColorTranslator.FromHtml(colorString);
                            return true;
                        }
                        else
                        {
                            if (colorString.Replace("，", ",").IndexOf(",") > -1)
                            {
                                string[] aryColorList = colorString.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                if (aryColorList.Length == 3)
                                {
                                    color = Color.FromArgb(int.Parse(aryColorList[0]), int.Parse(aryColorList[1]), int.Parse(aryColorList[2]));
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                if (colorString.IsInt())
                                {
                                    color = ColorTranslator.FromWin32(Convert.ToInt32(colorString));
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    #endregion 判断颜色值
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 新增行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediGridView_AddingNew(object sender, AddingNewEventArgs e)
        {
            e.NewObject = _DTOBase;
            this.FocusedEditForm();
        }

        /// <summary>
        /// 设置默认值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediGridView_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            if (_EDataLayout2 == null || _EDataLayout2.Count == 0) return;

            //if (e.RowHandle < 0) return;
            //设置默认值
            MediAdvBandedGridView mediGridView = sender as MediAdvBandedGridView;
            foreach (E_GY_DATALAYOUT2 o in _EDataLayout2)
            {
                if (!o.DEFAULTVALUE.IsNullOrEmpty())
                    mediGridView.SetRowCellValue(e.RowHandle, mediGridView.Columns[o.FIELDNAME], o.DEFAULTVALUE);
            }
            mediGridView.OptionsBehavior.Editable = true;
        }

        /// <summary>
        /// 计算行高
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediGridView_CalcRowHeight(object sender, RowHeightEventArgs e)
        {
            e.RowHeight += RowSpace;
        }

        /// <summary>
        /// 右键单击弹出菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridViewEx_Click(object sender, EventArgs e)
        {
            //MouseEventArgs Mouse_e = (MouseEventArgs)e;
            //if (Mouse_e.Button == MouseButtons.Right && HISClientHelper.USERID == "DBA")
            //{
            //    MediGridControl grid = this.GridControl as MediGridControl;
            //    string sFormName = grid.FindForm().Name.ToString();
            //    string sNameSpace = grid.FindForm().GetType().Namespace;
            //    FrmDataLayoutSet frm = new FrmDataLayoutSet(sFormName, this.Name, sNameSpace, this);

            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        //先清空缓存
            //        if (null != _EDataLayout1 && _EDataLayout1.DATALAYOUTID != null)
            //            GYDataLayoutHelper.RefreshDataLayoutInfo(sFormName, this.Name, sNameSpace);

            //        GetDataLayoutForDB();
            //        RunViewColunmProcess();
            //        RowAttributeSetForDB();
            //    }
            //}
        }

        /// <summary>
        /// 右键双击弹出菜单
        /// </summary>
        /// <param name = "sender" ></ param >
        /// < param name="e"></param>
        private void GridViewEx_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs Mouse_e = (MouseEventArgs)e;
            if (Mouse_e.Button == MouseButtons.Right && HISClientHelper.USERID == "DBA")
            {
                MediGridControl grid = this.GridControl as MediGridControl;
                string sFormName = grid.FindForm().Name.ToString();
                string sNameSpace = grid.FindForm().GetType().Namespace;
                //NewFrmDataLayOutSet frm = new NewFrmDataLayOutSet(sFormName, this.Name, sNameSpace, this);

                //if (frm.ShowDialog() == DialogResult.OK)
                //{
                //    //先清空缓存
                //    if (null != _EDataLayout1 && null != _EDataLayout1.DATALAYOUTID)
                //        GYDataLayoutHelper.RefreshDataLayoutInfo(sFormName, this.Name, sNameSpace);

                //    GetDataLayoutForDB();
                //    RunViewColunmProcess();
                //    RowAttributeSetForDB();
                //}
            }
        }

        /// <summary>
        /// 选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyGridView_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            int[] grvarry = this.GetSelectedRows();
            if (grvarry.Length > 1)
            {
                /* for (int i = 0; i < grvarry.Length; i++)
                 {
                     GetDataRowList[i] = this.GetDataRow(grvarry[i]);
                 }*/
                GetList = grvarry;
            }
            else
            {
                GetDataRowInfo = this.GetFocusedDataRow();
            }
            //GetDataRowInfo = this.GetFocusedDataRow();
        }

        /// <summary>
        /// 弹出列头菜单设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == GridMenuType.Column && (e.HitInfo.InGroupPanel || e.HitInfo.InFilterPanel))
            {
                e.Allow = false;
                return;
            }
            //获取菜单
            GridViewMenu temp = e.Menu;
            if (e.MenuType == GridMenuType.Column)
            {
                if (e.HitInfo.Column == null)
                    return;

                //过滤菜单
                //foreach (DXMenuItem dx in e.Menu.Items)
                //{
                //    if (dx.Caption == "降序排列" || dx.Caption == "升序排列" || dx.Caption == "清除排序" || dx.Caption == "移除此列" || dx.Caption == "过滤设置")
                //    { }
                //    else
                //        dx.Visible = false;
                //}

                DXMenuItem dxMenuItem = null;

                #region 列头设置

                //if (_IsShowColumnSetMenum)
                //{
                //    dxMenuItem = new DXMenuItem("列头设置");
                //    dxMenuItem.Click += new EventHandler(delegate (object sd, EventArgs ce)
                //    {
                //        MediGridControl grid = this.GridControl as MediGridControl;
                //        string sFormName = grid.FindForm().Name.ToString();

                //        frmSysColumnSet frm = new frmSysColumnSet(sFormName, this.Name, this);
                //        frm.ShowDialog();

                //        RunViewColunmProcess();
                //    });

                //    e.Menu.Items.Add(dxMenuItem);
                //}

                #endregion 列头设置

                #region 导出数据

                dxMenuItem = new DXMenuItem("导出数据到Excel");
                dxMenuItem.Click += new EventHandler(delegate (object sd, EventArgs ce)
                {
                    SaveFileDialog save = new SaveFileDialog();
                    save.Title = "导出Excel";
                    save.Filter = "Excel文件(*.xls)|*.xls";
                    if (save.ShowDialog() == DialogResult.OK)
                    {
                        (sender as GridView).ExportToXls(save.FileName);
                    }
                });
                e.Menu.Items.Add(dxMenuItem);

                #endregion 导出数据

                #region 打印数据

                dxMenuItem = new DXMenuItem("打印数据");
                dxMenuItem.Click += new EventHandler(delegate (object sd, EventArgs ce)
                {
                    (sender as GridView).PrintDialog();
                });
                e.Menu.Items.Add(dxMenuItem);

                #endregion 打印数据
            }
        }

        /// <summary>
        /// 行号 自带行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 自定义单元格的数据展示 是否显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (_IsShowLineNumber)
            {
                if (e.Column.FieldName == "SerialNo")
                {
                    int rowHandle = e.Column.View.GetRowHandle(e.ListSourceRowIndex);
                    if (rowHandle < 0)
                        e.DisplayText = (e.ListSourceRowIndex + 1).ToString();
                    else
                        e.DisplayText = (rowHandle + 1).ToString();
                }
            }
        }

        /// <summary>
        /// 行样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void GridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            GridView gridViewEx = sender as GridView;

            //if (e.RowHandle == this.FocusedRowHandle)
            //{
            //    e.Appearance.ForeColor = Color.Red;
            //    e.Appearance.BackColor = Color.Linen;
            //}

            //DataRow dr = this.GetDataRow(e.RowHandle);

            //if (dr == null) return;

            //行背景色
            if (_EDataLayout1 != null)
            {
                string backColorExpression = _EDataLayout1.ROWBACKCOLOREXPRESSION;

                if (!backColorExpression.IsNullOrWhiteSpace())
                {
                    Color color = new Color();
                    bool bSet = false;

                    if (ExpressionToColor(null, e.RowHandle, backColorExpression, ref color, false))
                    {
                        bSet = true;
                    }

                    if (bSet)
                    {
                        //e.Appearance.ForeColor = color;
                        e.Appearance.BackColor = color;
                        //e.Appearance.Options.UseBackColor = true;
                        //e.Appearance.BackColor = color;
                    }
                }
            }

            //e.Appearance.Options.UseBackColor = true;
            //e.Appearance.BackColor = Color.Red;
        }

        /// <summary>
        /// 必输项验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridview_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;//不弹出消息框
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridview_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (e.RowHandle >= 0 && ((((dynamic)sender).DataSource as IList)[e.RowHandle] as Enterprise.Base.DTOBase).State == DTOState.UnChange)
            //{
            //    ((((dynamic)sender).DataSource as IList)[e.RowHandle] as Enterprise.Base.DTOBase).State = DTOState.Update;
            //}
        }

        #endregion 事件相关

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="type"></param>
        public void DrawCellBorder(RowCellCustomDrawEventArgs e, Color type)
        {
            SolidBrush brush = new SolidBrush(type);
            //Brush brush;
            //if (type == "1")
            //{
            //    brush = Brushes.Red;
            //    brush=Brushes.
            //}
            //else
            //{
            //    brush = Brushes.Blue;
            //}

            e.Graphics.FillRectangle(brush, new Rectangle(TargetRect.X - 1, TargetRect.Y - 1, TargetRect.Width + 2, 2));
            e.Graphics.FillRectangle(brush, new Rectangle(TargetRect.Right - 1, TargetRect.Y - 1, 2, TargetRect.Height + 2));
            e.Graphics.FillRectangle(brush, new Rectangle(TargetRect.X - 1, TargetRect.Bottom - 1, TargetRect.Width + 2, 2));
            e.Graphics.FillRectangle(brush, new Rectangle(TargetRect.X - 1, TargetRect.Y - 1, 2, TargetRect.Height + 2));
        }

        /// <summary>
        /// 横向合并单元格类
        /// </summary>
        public class MyMergedCell
        {
            public MyMergedCell(int rowHandle, GridColumn col1, GridColumn col2)
            {
                _RowHandle = rowHandle;
                _Column1 = col1;
                _Column2 = col2;
            }

            private GridColumn _Column2;
            private GridColumn _Column1;
            private int _RowHandle;

            /// <summary>
            /// 行序号
            /// </summary>
            public int RowHandle
            {
                get { return _RowHandle; }
                set { _RowHandle = value; }
            }

            /// <summary>
            /// 合并的列1
            /// </summary>
            public GridColumn Column1
            {
                get { return _Column1; }
                set
                {
                    _Column1 = value;
                }
            }

            /// <summary>
            /// 合并的列2
            /// </summary>
            public GridColumn Column2
            {
                get { return _Column2; }
                set
                {
                    _Column2 = value;
                }
            }
        }

        /// <summary>
        /// 横向合并单元格重绘类
        /// </summary>
        public class MyGridPainter : GridPainter
        {
            MediAdvBandedGridView _view;
            private bool _IsCustomPainting;

            public bool IsCustomPainting
            {
                get
                {
                    return _IsCustomPainting;
                }
                set
                {
                    _IsCustomPainting = value;
                }
            }

            public MyGridPainter(MediAdvBandedGridView view)
                : base(view)
            {
                _view = view;
            }

            public void DrawMergedCell(MyMergedCell cell, PaintExEventArgs e)
            {
                int delta = cell.Column1.VisibleIndex - cell.Column2.VisibleIndex;
                if (Math.Abs(delta) > 1)
                    return;
                GridViewInfo vi = View.GetViewInfo() as GridViewInfo;
                GridCellInfo gridCellInfo1 = vi.GetGridCellInfo(cell.RowHandle, cell.Column1);
                GridCellInfo gridCellInfo2 = vi.GetGridCellInfo(cell.RowHandle, cell.Column2);
                if (gridCellInfo1 == null || gridCellInfo2 == null)
                    return;
                Rectangle targetRect = Rectangle.Union(gridCellInfo1.Bounds, gridCellInfo2.Bounds);
                gridCellInfo1.Bounds = targetRect;
                gridCellInfo1.CellValueRect = targetRect;
                gridCellInfo2.Bounds = targetRect;
                gridCellInfo2.CellValueRect = targetRect;
                if (delta < 0)
                    gridCellInfo1 = gridCellInfo2;
                Rectangle bounds = gridCellInfo1.ViewInfo.Bounds;
                bounds.Width = targetRect.Width;
                bounds.Height = targetRect.Height;
                gridCellInfo1.ViewInfo.Bounds = bounds;
                gridCellInfo1.ViewInfo.CalcViewInfo(e.Cache.Graphics);
                //if (_view.ColorType == "1")
                //    gridCellInfo1.Appearance.BorderColor = Color.Red;
                //if(_view.ColorType=="0")
                //    gridCellInfo1.Appearance.BorderColor = Color.Blue;

                //string name = gridCellInfo1.CellValue.ToString();
                //string name1 = gridCellInfo1.ViewInfo.DisplayText;
                //gridCellInfo1.ViewInfo.SetDisplayText("kkk");
                IsCustomPainting = true;
                gridCellInfo1.Appearance.FillRectangle(e.Cache, gridCellInfo1.Bounds);
                DrawRowCell(new GridViewDrawArgs(e.Cache, vi, vi.ViewRects.Bounds), gridCellInfo1);
                IsCustomPainting = false;
            }
        }

        /// <summary>
        /// 横向合并单元格帮助类
        /// </summary>
        public class MyCellMergeHelper
        {
            public string xianShiValue;
            MyGridPainter painter;
            MediAdvBandedGridView _view;

            private List<MyMergedCell> _MergedCells = new List<MyMergedCell>();

            public List<MyMergedCell> MergedCells
            {
                get
                {
                    return _MergedCells;
                }
            }

            public MyCellMergeHelper(MediAdvBandedGridView view)
            {
                _view = view;
                view.CustomDrawCell += new RowCellCustomDrawEventHandler(view_CustomDrawCell);
                view.GridControl.PaintEx += GridControl_PaintEx;
                view.CellValueChanged += new CellValueChangedEventHandler(view_CellValueChanged);
                painter = new MyGridPainter(view);
            }

            private void GridControl_PaintEx(object sender, PaintExEventArgs e)
            {
                DrawMergedCells(e);
            }

            public void MergedCell(MediAdvBandedGridView gridView1, int rowHandle, string value)
            {
                int startIndex = 0;
                if (gridView1.IsShowLineNumber)
                    startIndex = 1;
                for (int j = startIndex; j < gridView1.VisibleColumns.Count - 1; j++)
                {
                    //int index1= gridView1.VisibleColumns[j].VisibleIndex;
                    //int index2 = gridView1.VisibleColumns[j+1].VisibleIndex;
                    //var item = gridView1.VisibleColumns[j];
                    AddMergedCell(rowHandle, gridView1.VisibleColumns[j], gridView1.VisibleColumns[j + 1], value);
                }
                gridView1.RefreshRow(rowHandle);
            }

            public MyMergedCell AddMergedCell(int rowHandle, GridColumn col1, GridColumn col2)
            {
                MyMergedCell cell = new MyMergedCell(rowHandle, col1, col2);
                _MergedCells.Add(cell);
                return cell;
            }

            //public void AddMergedCell(int rowHandle, int col1, int col2, object value)
            //{
            //    AddMergedCell(rowHandle, _view.Columns[col1], _view.Columns[col2]);
            //}

            public void AddMergedCell(int rowHandle, GridColumn col1, GridColumn col2, object value)
            {
                MyMergedCell cell = AddMergedCell(rowHandle, col1, col2);
                SafeSetMergedCellValue(cell, value);
            }

            public void SafeSetMergedCellValue(MyMergedCell cell, object value)
            {
                if (cell != null)
                {
                    SafeSetCellValue(cell.RowHandle, cell.Column1, value);
                    SafeSetCellValue(cell.RowHandle, cell.Column2, value);
                }
            }

            public void SafeSetCellValue(int rowHandle, GridColumn column, object value)
            {
                if (_view.GetRowCellValue(rowHandle, column) != value)
                {
                    _view.SetRowCellValue(rowHandle, column, value);
                }
            }

            private MyMergedCell GetMergedCell(int rowHandle, GridColumn column)
            {
                foreach (MyMergedCell cell in _MergedCells)
                {
                    if (cell.RowHandle == rowHandle && (column == cell.Column1 || column == cell.Column2))
                        return cell;
                }
                return null;
            }

            private bool IsMergedCell(int rowHandle, GridColumn column)
            {
                return GetMergedCell(rowHandle, column) != null;
            }

            private void DrawMergedCells(PaintExEventArgs e)
            {
                foreach (MyMergedCell cell in _MergedCells)
                {
                    painter.DrawMergedCell(cell, e);
                }
            }

            void view_CellValueChanged(object sender, CellValueChangedEventArgs e)
            {
                SafeSetMergedCellValue(GetMergedCell(e.RowHandle, e.Column), e.Value);
            }

            void view_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
            {
                if (IsMergedCell(e.RowHandle, e.Column))
                    e.Handled = !painter.IsCustomPainting;
            }
        }
    }
}