using DevExpress.Data;
using DevExpress.Data.Filtering;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.Utils.Paint;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Scrolling;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using Mediinfo.DTO.Core;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.HIS.Core;
using Mediinfo.ServiceProxy.JCJG.GongYong;
using Mediinfo.Utility.Extensions;
using Mediinfo.WinForm.HIS.Controls.Properties;
using Mediinfo.WinForm.HIS.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 以表格形式显示数据控件
    /// </summary>
    public class MediGridView : DevExpress.XtraGrid.Views.Grid.GridView
    {
        /// <summary>
        /// 创建服务实例
        /// </summary>
        private JCJGDataLayoutService _GYDataLayoutService = null;

        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }
        /// <summary>
        /// 表格中文名称
        /// </summary>
        [Browsable(true), DefaultValue(null), Description("表格中文名称，用于提示时标识是哪个表格")]
        public string BiaoGeMC { get; set; }
        #region 属性

        private int rowSpace = 8;

        /// <summary>
        /// 选中行行号（用于记录焦点离开gridview后）
        /// </summary>
        private int rowIndex = -1;

        private ImageCollection GridviewImageCollection;
        /// <summary>
        /// 新增事件
        /// </summary>
        public event Action<object, EventArgs> XinZengClick;

        /// <summary>
        /// 删除事件
        /// </summary>
        public event Action<object, EventArgs> ShanChuClick;
        /// <summary>
        /// 更多操作
        /// </summary>
        public event Action<object, EventArgs> GengDuoCZClick;
        /// <summary>
        /// 停嘱事件
        /// </summary>
        public event Action<object, EventArgs> ZanTingClick;
        /// <summary>
        ///报表事件
        /// </summary>
        public event Action<object, EventArgs> BaoBiaoClick;
        /// <summary>
        ///报表事件
        /// </summary>
        public event Action<object, EventArgs> BianJiClick;
        /// <summary>
        /// 替换事件
        /// </summary>
        public event Action<object, EventArgs> TiHuanClick;
        /// <summary>
        /// 取消替换事件
        /// </summary>
        public event Action<object, EventArgs> QuXiaoTHClick;
        /// <summary>
        /// 箭头向上事件
        /// </summary>
        public event Action<object, EventArgs> UpClick;
        /// <summary>
        /// 箭头向下事件
        /// </summary>
        public event Action<object, EventArgs> DownClick;
        /// <summary>
        /// 增加子诊断事件
        /// </summary>
        public event Action<object, EventArgs> ZiZhenDuanClick;
        /// <summary>
        /// 水平滚动条
        /// </summary>
        public bool showMediHorizontalLines = false;
        /// <summary>
        /// 垂直滚动条
        /// </summary>
        public bool showMediVerticalLines = false;
        /// <summary>
        /// 显示水平分割线
        /// </summary>
        [DefaultValue(true), Description("自带属性由于控价兼容之前代码，不能满足，所以新添加该属性控制水平线，当IsShowLine设置true时，这个属性无效果")]
        public bool ShowMediHorizontalLines
        {
            get { return showMediHorizontalLines; }
            set
            {
                showMediHorizontalLines = value;
                if (showMediHorizontalLines)
                    this.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.True;
                else
                    this.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            }
        }
        /// <summary>
        /// 显示水垂直分割线
        /// </summary>
        [DefaultValue(true), Description("自带属性由于控价兼容之前代码，不能满足，所以新添加该属性控制垂直线，当IsShowLine设置true时，这个属性无效果")]
        public bool ShowMediVerticalLines
        {
            get { return showMediVerticalLines; }
            set
            {
                showMediVerticalLines = value;

                if (showMediVerticalLines)
                    this.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.True;
                else
                    this.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            }
        }
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
                    this.RowSeparatorHeight = value;
                    rowSpace = value;
                }
            }
        }

        private bool customRowHeight = false;
        private bool enableNewRowHeight = false;
        private bool enableNewAppearanceEvenRow = true;
        private bool enableNewAppearanceOddRow = true;
        private bool enableFocuseStyle = true;
        private int vScrollBarXValue = 0;
        /// <summary>
        /// 垂直滚动条位置
        /// </summary>
        [DefaultValue(0), Description("垂直滚动条位置")]
        public int VScrollBarXValue
        {
            get
            {
                return vScrollBarXValue;
            }
            set
            {
                vScrollBarXValue = value;
            }
        }
        /// <summary>
        /// 自定义行高
        /// </summary>
        [DefaultValue(false), Description("自定义行高")]
        public bool CustomRowHeight
        {
            get
            {
                return customRowHeight;
            }
            set
            {
                customRowHeight = value;
            }
        }
        /// <summary>
        /// 自定义行高(之前代码太烂不敢改新加一个属性)
        /// </summary>
        [DefaultValue(false), Description("自定义行高(新)")]
        public bool EnableNewRowHeight
        {
            get
            {
                return enableNewRowHeight;
            }
            set
            {
                enableNewRowHeight = value;
            }
        }

        private int mediNumberIndex = 0;
        /// <summary>
        /// medi复选框固定列宽
        /// </summary>
        [DefaultValue(0), Description("medi序号列索引设置")]
        public int MediNumberIndex
        {
            get
            {
                return mediNumberIndex;
            }
            set
            {
                mediNumberIndex = value;
            }
        }

        private int mediCheckBoxIndex = 0;
        /// <summary>
        /// medi复选框固定列宽
        /// </summary>
        [DefaultValue(0), Description("medi复选框列索引设置")]
        public int MediCheckBoxIndex
        {
            get
            {
                return mediCheckBoxIndex;
            }
            set
            {
                mediCheckBoxIndex = value;
            }
        }

        private int mediCheckBoxFixSize = 50;
        /// <summary> 
        /// medi复选框固定列宽
        /// </summary>
        [DefaultValue(50), Description("medi复选框固定列宽")]
        public int MediCheckBoxFixSize
        {
            get
            {
                return mediCheckBoxFixSize;
            }
            set
            {
                mediCheckBoxFixSize = value;
            }
        }
        /// <summary>
        /// 是否禁用聚焦样式
        /// </summary>
        [DefaultValue(true), Description("是否禁用聚焦样式")]
        public bool EnableFocuseStyle
        {
            get
            {
                return enableFocuseStyle;
            }
            set
            {
                enableFocuseStyle = value;
                if (SkinCat.Instance.IsDesignMode) return;
                if (!enableFocuseStyle) ClearFocuseRow();
            }
        }


        /// <summary>
        /// 偶行属性
        /// </summary>
        [DefaultValue(true), Description("启用偶行颜色(新)")]
        public bool EnableNewAppearanceEvenRow
        {
            get
            {
                return enableNewAppearanceEvenRow;
            }
            set
            {
                enableNewAppearanceEvenRow = value;
                if (enableNewAppearanceEvenRow)
                    this.OptionsView.EnableAppearanceEvenRow = true;
                else
                    this.OptionsView.EnableAppearanceEvenRow = false;
            }
        }
        /// <summary>
        /// 奇行属性
        /// </summary>
        [DefaultValue(true), Description("启用奇行颜色(新)")]
        public bool EnableNewAppearanceOddRow
        {
            get
            {
                return enableNewAppearanceOddRow;
            }
            set
            {
                enableNewAppearanceOddRow = value;
                if (enableNewAppearanceOddRow)
                    this.OptionsView.EnableAppearanceOddRow = true;
                else
                    this.OptionsView.EnableAppearanceOddRow = false;
            }
        }
        /// <summary>
        /// 垂直滚动条位置属性
        /// </summary>
        [Browsable(true), DefaultValue(0), Description("设置垂直滚动条位置")]
        public VerticalSrollLocation VerSrollBarLocation { get; set; } = VerticalSrollLocation.Default;


        private bool autoSelect = false;

        /// <summary>
        /// 是否自动全选聚焦单元格数据
        /// </summary>
        [DefaultValue(false), Description("是否自动全选聚焦单元格数据")]
        public bool AutoSelect
        {
            get
            {
                return autoSelect;
            }
            set
            {
                autoSelect = value;
            }
        }

        /// <summary>
        /// 单元格获取焦点时是否自定义边框
        /// </summary>
        [DefaultValue(false), Description("单元格获取焦点时是否自定义边框")]
        public bool CustomBorderOnRowCellFocus { get; set; }

        private Dictionary<string, MediInfoImeMode> immdic = new Dictionary<string, MediInfoImeMode>();

        /// <summary>
        /// 获取聚焦单元格信息
        /// </summary>
        [Browsable(true), Description("聚焦单元格信息")]
        public GridCellInfo CurrentCell
        {
            get
            {
                GridCellInfo gridCellInfo = null;
                if (this.FocusedRowHandle > -1 && this.FocusedColumn != null)
                {
                    gridCellInfo = ((GridViewInfo)this.GetViewInfo()).GetGridCellInfo(this.FocusedRowHandle, this.FocusedColumn);
                }
                return gridCellInfo;
            }
        }

        private bool isNoneFocusedWhenLostFocus = false;
        [DefaultValue(false), Description("当失去焦点时是否显示聚焦行")]
        public bool IsNoneFocusedWhenLostFocus
        {
            get { return isNoneFocusedWhenLostFocus; }
            set
            {
                isNoneFocusedWhenLostFocus = value; if (isNoneFocusedWhenLostFocus) this.ClearFocuseRow(); else this.EnabledFocusedRow();
            }
        }

        protected internal void DoMouseWheelScroll(MouseWheelScrollClientArgs e)
        {
        }

        private void gridView1_MouseWheel(object sender, MouseEventArgs e)
        {
        }
        protected override string ViewName { get { return "MediGridView"; } }

        protected override ScrollInfo CreateScrollInfo()
        {
            return new MediGridviewScrollInfo(this);
        }
        protected virtual int GetScrollLinesCount()
        {
            int res = SystemInformation.MouseWheelScrollLines == -1 ? this.ScrollPageSize : SystemInformation.MouseWheelScrollLines;
            if (this.ViewInfo.ActualDataRowMinRowHeight > 40 || this.ViewInfo.RowsInfo.Count < 15)
            {
                if (res > 2) return res / 2;
                return 1;
            }
            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridColumns"></param>
        public void AddColumns(params GridColumn[] gridColumns)
        {

            gridColumns.ToList().ForEach(p =>
            {
                if (this.Columns.ToList().Where(o => o.FieldName.ToUpper().Equals(p.FieldName.ToUpper())).Count() < 1)
                {
                    this.Columns.Add(p);
                }


            });

        }

        /// <summary>
        /// 默认布局
        /// </summary>
        public E_GY_DATALAYOUTDTO DataLayoutDefaultValue { get; set; }

        /// <summary>
        /// 自定义布局
        /// </summary>
        public E_GY_DATALAYOUTDTO DataLayoutCustomValue { get; set; }

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
        private bool _IsShowLineNumber = true;

        /// <summary>
        /// 是否显示行号
        /// </summary>
        [DefaultValue(true), Description("是否显示行号")]
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

        /// <summary>
        /// 是否固定列,默认创建固定列
        /// </summary>
        private bool _IsFixedColumn = false;

        /// <summary>
        /// 是否固定列
        /// </summary>
        [DefaultValue(true), Description("是否固定列")]
        public bool IsFixedColumn
        {
            get { return _IsFixedColumn; }
            set
            {
                _IsFixedColumn = value;

            }
        }

        /// <summary>
        /// 是否显示序号
        /// </summary>
        [DefaultValue(true), Description("是否显示序号")]
        public bool IsShowIndexNumber { get; set; } = true;

        /// <summary>
        /// 非空字段设置
        /// </summary>
        [DescriptionAttribute("非空字段设置，多个字段以逗号隔开")]
        [Browsable(true)]
        [Editor(typeof(GridViewEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string RequiredFieldItem { get; set; } = "";

        #endregion 属性

        #region 构造函数

        /// <summary>
        /// 初始化构造函数
        /// </summary>
        /// <param name="ownerGrid"></param>
        public MediGridView(GridControl ownerGrid) : base(ownerGrid)
        {
            InitializeComponent();
            GlobelAttribute();
            InitGridViewEx();
            if (!SkinCat.Instance.IsDesignMode)
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MediGridView()
        {
            InitializeComponent();
            InitGridViewEx();
            this.Focus();
            //if (!SkinCat.Instance.IsDesignMode)
            //    _GYDataLayoutService = new GYDataLayoutService();
            if (!SkinCat.Instance.IsDesignMode)
            {

                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
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
            // this.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            //this.OptionsSelection.EnableAppearanceFocusedCell = false;

            this.OptionsNavigation.EnterMoveNextColumn = true;
            this.OptionsNavigation.AutoFocusNewRow = true;

            //是否显示左侧面板
            this.OptionsView.ShowIndicator = false;

            //焦点框在单元格里面
            //this.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;

            //获取或设置最终用户是否允许调用单元格编辑器。
            this.OptionsBehavior.Editable = true;
            //this.OptionsBehavior.EditorShowMode = EditorShowMode.MouseUp;

            // this.OptionsBehavior.EditingMode = GridEditingMode.Default;

            //获取或设置最终用户是否可以调用列表头上下文菜单
            this.OptionsMenu.EnableColumnMenu = true;

            //是否允许多选
            // this.OptionsSelection.MultiSelect = true;

            //多选模式
            // this.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;

            //隐藏分组面板
            this.OptionsView.ShowGroupPanel = false;
        }

        /// <summary>
        /// GridView属性设置
        /// </summary>
        public void GridViewAttributeSet()
        {
            this.OptionsDetail.EnableMasterViewMode = false;

            //this.BorderStyle = BorderStyles.NoBorder;
            this.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDown;
            //获取或设置是否启用焦点单元格的外观设置。
            //this.OptionsSelection.EnableAppearanceFocusedCell = true;
            //this.Appearance.FocusedCell.Options.UseBackColor = true;
            //this.Appearance.FocusedCell.Options.UseForeColor = true;
            //this.Appearance.FocusedCell.BackColor = Color.FromArgb(30, 186, 255);
            //this.Appearance.FocusedCell.ForeColor = Color.White;

            // 列头居中显示
            this.Appearance.HeaderPanel.TextOptions.VAlignment = VertAlignment.Center;
            this.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Near;
            // 设置列头默认字体
            this.Appearance.HeaderPanel.Options.UseFont = true;
            this.Appearance.HeaderPanel.Font = new Font(AppearanceObject.DefaultFont.FontFamily.Name, this.Appearance.HeaderPanel.Font.Size, FontStyle.Bold);
            // 设置列表默认字体颜色
            this.Appearance.HeaderPanel.ForeColor = Color.FromArgb(51, 51, 51);
            this.Appearance.HeaderPanel.Options.UseForeColor = true;
            // 行居中显示
            this.Appearance.Row.TextOptions.VAlignment = VertAlignment.Center;
            this.Appearance.Row.TextOptions.HAlignment = HorzAlignment.Near;
            this.OptionsBehavior.ImmediateUpdateRowPosition = false;
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
            //this.OptionsView.RowAutoHeight = true;
            //this.RowHeight = 24;
            //头高度
            this.ColumnPanelRowHeight = 24;
            //表格线
            this.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
        }

        protected override void OnColumnOptionsChanged(GridColumn column, DevExpress.Utils.Controls.BaseOptionChangedEventArgs e)
        {
            base.OnColumnOptionsChanged(column, e);
            column.OptionsColumn.AllowFocus = column.OptionsColumn.AllowEdit;
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
       //private GYDataLayoutService _GYDataLayoutService = null;

        /// <summary>
        /// 存储非空字段项
        /// </summary>
        private List<string> _NonEmptyFields = new List<string>();

        /// <summary>
        /// 数据集
        /// </summary>
        private DTOBase _DTOBase;

        /// <summary>
        /// 单元格边框实例
        /// </summary>
        private BorderXPaint xPaint = new BorderXPaint();

        #endregion 全局变量

        #region 公共方法

        /// <summary>
        /// 添加非空字段
        /// </summary>
        /// <param name="fieldName">字段名称</param>
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
        /// 保存验证
        /// </summary>
        /// <param name="trimSpace">是否移除空值(默认去除)</param>
        /// <param name="showErrorMsg">是否显示错误信息(默认显示)</param>
        ///  /// <param name="nullColumns">用来定义该行是否为空，如果传入该字段的值为空，表示行为空行</param>
        /// <returns></returns>
        public bool CheckRequiredFields(bool trimSpace = true, bool showErrorMsg = true, string nullColumns = "")
        {
            if (this.DataRowCount == 0) return true;

            //存储必填项提示信息
            List<string> requiredFieldsMsg = new List<string>();
            //存储第一个非空项
            GridColumn gridColumnFirst = null;
            //记录验证失败的首行
            int indexFirst = 0;

            #region 非空项验证 有效性检查 非空表达式

            for (int i = 0; i < this.DataRowCount; i++)
            {
                DTOBase dtoBase = this.GetRow(i) as DTOBase;
                if (dtoBase.GetState() == DTOState.New || dtoBase.GetState() == DTOState.Update)
                {
                    GridColumnCollection gridColumnCollection = this.Columns;
                    //add by zhukunpin 根据传入的字段判断该行是否为空，为空行的话就不去检查
                    foreach (GridColumn gridColumn in gridColumnCollection)
                    {
                        if (gridColumn.FieldName == nullColumns &&
                             this.GetRowCellValue(i, nullColumns).ToStringEx().IsNullOrWhiteSpace())
                        {
                            goto NULL;
                        }
                    }
                    foreach (GridColumn gridColumn in gridColumnCollection)
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
                                    if (string.IsNullOrEmpty(BiaoGeMC))
                                    {
                                        requiredFieldsMsg.Add(string.Format("第{0}行，【{1}】列，不能为空！\r\n", (i + 1).ToString(), captionName));
                                    }
                                    else
                                    {
                                        requiredFieldsMsg.Add(string.Format("{0}:第{1}行，【{2}】列，不能为空！\r\n", BiaoGeMC, (i + 1).ToString(), captionName));
                                    }
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


                        }
                        #endregion 有效性检查、非空表达式

                    }

                }

            NULL: { }
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
                #region Modify by SunChao on 2019.12 for [HR6-1710] 优化-门诊病历-西医诊断发病日期提示语修改
                //int selectIndex = this.GetFocusedDataSourceRowIndex();
                //int moveRows = indexFirst - selectIndex;
                //this.FocusedRowHandle = moveRows;
                this.FocusedRowHandle = indexFirst;
                //this.MoveBy(moveRows);
                this.ShowEditor();
                #endregion
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
        ///  清除聚焦行针对双表格
        /// </summary>
        public void EnabledFocusedRow()
        {
            this.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.OptionsSelection.EnableAppearanceFocusedCell = true;

            this.OptionsSelection.EnableAppearanceFocusedRow = true;

            this.OptionsSelection.EnableAppearanceHideSelection = true;


        }

        /// <summary>
        /// 设置焦点行
        /// </summary>
        /// <param name="rowHandle">行句柄</param>
        public void SetFocusedRow(int rowHandle)
        {
            this.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.OptionsSelection.EnableAppearanceFocusedCell = true;

            this.OptionsSelection.EnableAppearanceFocusedRow = true;

            this.OptionsSelection.EnableAppearanceHideSelection = true;
            foreach (var row in this.GetSelectedRows())
                this.UnselectRow(row);

            this.FocusedRowHandle = (rowHandle > 0 ? rowHandle : 0);
            this.SelectRows(this.FocusedRowHandle, this.FocusedRowHandle);
        }

        /// <summary>
        /// 清除选中行
        /// </summary>
        /// <param name="rowHandle">行句柄(默认值-1)</param>
        public void ClearFocuseRow(int rowHandle = -1)
        {
            //if (rowHandle == -1)
            //{
            //    foreach (var row in this.GetSelectedRows())
            //        this.UnselectRow(row);
            //}
            //else
            //    this.UnselectRow(rowHandle);

            //this.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            //this.OptionsSelection.EnableAppearanceFocusedCell = false;

            //this.OptionsSelection.EnableAppearanceFocusedRow = false;

            //this.OptionsSelection.EnableAppearanceHideSelection = false;
            //this.CustomDrawRowIndicator += MediGridView_CustomDrawRowIndicator;
            // this.EditingCell

            //this.GridControl.FindForm().SuspendLayout();

            this.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.OptionsSelection.EnableAppearanceFocusedCell = false;

            this.OptionsSelection.EnableAppearanceFocusedRow = false;

            this.OptionsSelection.EnableAppearanceHideSelection = false;
            //this.GridControl.FindForm().ResumeLayout();
        }

        private void MediGridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            e.Info.ImageIndex = -1;
        }

        /// <summary>
        /// 设置焦点单元格
        /// </summary>
        /// <param name="fieldName">聚焦字段名称</param>
        public void SetFocusedColumn(string fieldName)
        {
            if (fieldName.IsNullOrEmpty()) return;
            this.OptionsNavigation.AutoFocusNewRow = true;
            int focuseIndex = 0;
            foreach (GridColumn column in this.Columns)
            {
                if (column.Visible && column.FieldName.Equals(fieldName))
                {
                    focuseIndex = column.AbsoluteIndex;
                    break;
                }
            }
            this.FocusedColumn = this.Columns[focuseIndex];
            this.ShowEditor();
        }

        /// <summary>
        /// 重新聚焦当前聚焦列
        /// </summary>
        public void ReSetCurrentColumn()
        {
            if (this.FocusedColumn != null)
            {
                this.SetFocusedColumn(this.FocusedColumn.FieldName);
            }
        }

        #endregion 公共方法

        #region 行号

        /// <summary>
        /// 设置行号
        /// </summary>
        private void SetSerialNo()
        {
            GridColumnCollection gridColumnCollection = this.Columns;

            bool exist = false;
            for (int i = 0; i < gridColumnCollection.Count; i++)
            {
                GridColumn gridColumn = gridColumnCollection[i];
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
        private void AddFixSerialNo()
        {
            GridColumn colSerialNum = new GridColumn();
            colSerialNum.FieldName = "SerialNo";//列绑定字段
            colSerialNum.Caption = "序号";//列名称
            colSerialNum.Name = "colSerialNo";
            colSerialNum.Visible = true;
            colSerialNum.VisibleIndex = 0;
            colSerialNum.UnboundType = UnboundColumnType.Integer;
            colSerialNum.AppearanceHeader.Options.UseTextOptions = true;
            colSerialNum.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            colSerialNum.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;

            colSerialNum.AppearanceCell.Options.UseTextOptions = true;
            colSerialNum.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            colSerialNum.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            //列宽
            colSerialNum.Width = 40;
            colSerialNum.MinWidth = 40;
            //是否可以拖动列标题
            colSerialNum.OptionsColumn.AllowMove = false;
            //固定列头
            colSerialNum.Fixed = FixedStyle.Left;

            //排序
            colSerialNum.OptionsColumn.AllowSort = DefaultBoolean.False;
            colSerialNum.OptionsColumn.AllowEdit = false;
            colSerialNum.OptionsColumn.AllowFocus = false;

            colSerialNum.OptionsFilter.AllowFilter = false;
            colSerialNum.OptionsColumn.AllowMerge = DefaultBoolean.False;
            this.Columns.Insert(0, colSerialNum);
        }
        /// <summary>
        /// 添加序列号 初始化一次
        /// </summary>
        private void AddSerialNO()
        {
            if (!_IsShowLineNumber) return;

            GridColumn colSerialNum = new GridColumn();
            colSerialNum.FieldName = "SerialNo";//列绑定字段
            colSerialNum.Caption = "序号";//列名称
            colSerialNum.Name = "colSerialNo";
            colSerialNum.Visible = true;
            colSerialNum.VisibleIndex = 0;
            colSerialNum.UnboundType = UnboundColumnType.Integer;
            colSerialNum.AppearanceHeader.Options.UseTextOptions = true;
            colSerialNum.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            colSerialNum.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;

            colSerialNum.AppearanceCell.Options.UseTextOptions = true;
            colSerialNum.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            colSerialNum.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            //列宽
            colSerialNum.Width = 40;
            colSerialNum.MinWidth = 40;
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
            this.Columns.Insert(this.mediNumberIndex, colSerialNum);

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
                GridColumnCollection columnColl = this.Columns;
                if (columnColl == null || columnColl.Count == 0) return;

                foreach (GridColumn col in columnColl)
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
        /// 运行时处理列绑定问题
        /// </summary>
        private void RunViewColunmProcess()
        {
            if (_EDataLayout2 == null || _EDataLayout2.Count == 0)
                return;

            //列集合
            GridColumnCollection columnColl = this.Columns;

            if (columnColl == null || columnColl.Count == 0) return;

            //遍历gridview中包含的所有列
            for (int i = 0; i < columnColl.Count; i++)
            {
                GridColumn gridColumn = columnColl[i];
                foreach (E_GY_DATALAYOUT2 eDataLayout2 in _EDataLayout2)
                {
                    if (!string.IsNullOrEmpty(eDataLayout2.FIELDNAME))
                    {
                        if (gridColumn.FieldName.ToUpper() == eDataLayout2.FIELDNAME.ToUpper())
                        {
                            SetColunmStyleByPara(ref gridColumn, eDataLayout2);
                            break;
                        }
                        if (eDataLayout2.NONEMPTY == 1)//必输项添加相关字段
                            AddRequiredField(eDataLayout2.FIELDNAME.ToUpper());
                    }
                }
            }
            if (this.Name.Contains("GridSplitContainer"))
            {
                //GridColumnCollection spiltColumnColl = ((DevExpress.XtraGrid.Views.Base.ColumnView)this.SplitOtherView).Columns;
                //GridColumnCollection spiltColumnColl = ((DevExpress.XtraGrid.Views.Base.ColumnView)this.SplitContainer.SplitChildView).Columns;
                //GridColumnCollection spiltColumnColl1 = ((DevExpress.XtraGrid.Views.Base.ColumnView)this.SplitContainer.View).Columns;
                GridColumnCollection spiltColumnColl2 = ((DevExpress.XtraGrid.Views.Base.ColumnView)this.SplitOtherView).Columns;
                if (spiltColumnColl2 == null || spiltColumnColl2.Count == 0) return;
                //遍历gridview中包含的所有列
                for (int i = 0; i < spiltColumnColl2.Count; i++)
                {
                    GridColumn gridColumn = spiltColumnColl2[i];
                    foreach (E_GY_DATALAYOUT2 eDataLayout2 in _EDataLayout2)
                    {
                        if (!string.IsNullOrEmpty(eDataLayout2.FIELDNAME))
                        {
                            if (gridColumn.FieldName.ToUpper() == eDataLayout2.FIELDNAME.ToUpper())
                            {
                                SetColunmStyleByPara(ref gridColumn, eDataLayout2);
                                break;
                            }

                        }
                    }
                }

            }
        }

        /// <summary>
        /// 重绘排序
        /// </summary>
        /// <param name="columnColls"></param>
        private void RunViewColunmProcess(GridColumnCollection columnColls)
        {
            if (_EDataLayout2 == null || _EDataLayout2.Count == 0)
                return;

            //列集合
            GridColumnCollection columnColl = columnColls;

            if (columnColl == null || columnColl.Count == 0) return;

            //遍历gridview中包含的所有列
            for (int i = 0; i < columnColl.Count; i++)
            {
                GridColumn gridColumn = columnColl[i];
                foreach (E_GY_DATALAYOUT2 eDataLayout2 in _EDataLayout2)
                {
                    if (!string.IsNullOrEmpty(eDataLayout2.FIELDNAME))
                    {
                        if (gridColumn.FieldName.ToUpper() == eDataLayout2.FIELDNAME.ToUpper())
                        {
                            SetColunmStyleByPara(ref gridColumn, eDataLayout2);
                            break;
                        }
                        if (eDataLayout2.NONEMPTY == 1)//必输项添加相关字段
                            AddRequiredField(eDataLayout2.FIELDNAME.ToUpper());
                    }
                }
            }
            if (this.Name.Contains("GridSplitContainer"))
            {
                //GridColumnCollection spiltColumnColl = ((DevExpress.XtraGrid.Views.Base.ColumnView)this.SplitOtherView).Columns;
                //GridColumnCollection spiltColumnColl = ((DevExpress.XtraGrid.Views.Base.ColumnView)this.SplitContainer.SplitChildView).Columns;
                //GridColumnCollection spiltColumnColl1 = ((DevExpress.XtraGrid.Views.Base.ColumnView)this.SplitContainer.View).Columns;
                GridColumnCollection spiltColumnColl2 = ((DevExpress.XtraGrid.Views.Base.ColumnView)this.SplitOtherView).Columns;
                if (spiltColumnColl2 == null || spiltColumnColl2.Count == 0) return;
                //遍历gridview中包含的所有列
                for (int i = 0; i < spiltColumnColl2.Count; i++)
                {
                    GridColumn gridColumn = spiltColumnColl2[i];
                    foreach (E_GY_DATALAYOUT2 eDataLayout2 in _EDataLayout2)
                    {
                        if (!string.IsNullOrEmpty(eDataLayout2.FIELDNAME))
                        {
                            if (gridColumn.FieldName.ToUpper() == eDataLayout2.FIELDNAME.ToUpper())
                            {
                                SetColunmStyleByPara(ref gridColumn, eDataLayout2);
                                break;
                            }

                        }
                    }
                }

            }

        }

        /// <summary>
        ///  根据参数设置行样式
        /// </summary>
        /// <param name="col"></param>
        /// <param name="eDataLayout2"></param>
        private void SetColunmStyleByPara(ref GridColumn col, E_GY_DATALAYOUT2 eDataLayout2)
        {
            //列宽度
            //col.Width = eDataLayout2.WIDTH.ToInt(100);//设置宽度导致格式乱掉（应该是自适应问题）
            //是否可以拖动列标题
            col.OptionsColumn.AllowMove = true;
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
            if (eDataLayout2.NONEMPTY == 1)
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
                col.AppearanceHeader.Font = new Font(AppearanceObject.DefaultFont.FontFamily.Name, this.Appearance.HeaderPanel.Font.Size, FontStyle.Bold);
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
                        {
                            if (_EDataLayout1.ALLOWSORT != 1)
                            {
                                col.SortOrder = DevExpress.Data.ColumnSortOrder.None;
                            }
                            else
                            {
                                col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                            }
                        }
                        else if (arry[1].ToUpper() == "DESC")
                        {
                            if (_EDataLayout1.ALLOWSORT != 1)
                            {
                                col.SortOrder = DevExpress.Data.ColumnSortOrder.None;
                            }
                            else
                            {
                                col.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                            }
                        }

                        break;
                    }
                }
            }

            //排序
            if (eDataLayout2.SHENGJIANGXH == null || eDataLayout2.YOUXIANJI == null || eDataLayout2.SHENGJIANGXH == 0) return;
            if (eDataLayout2.SHENGJIANGXH == 1)
            {
                col.SortOrder = ColumnSortOrder.Ascending;
            }
            else if (eDataLayout2.SHENGJIANGXH == 2)
            {
                col.SortOrder = ColumnSortOrder.Descending;
            }

            //数据排序
            this.SortInfo.Clear();
            List<GridColumnSortInfo> gridColumnSortInfos = new List<GridColumnSortInfo>();

            DataLayoutDefaultValue.DataLayout2.ForEach(r =>
            {
                if (r.FIELDNAME == eDataLayout2.FIELDNAME)
                {
                    r.SHENGJIANGXH = eDataLayout2.SHENGJIANGXH;
                    r.YOUXIANJI = eDataLayout2.YOUXIANJI;
                }
                 
            });
            //_EDataLayout2
            var eDataLayoutOrder2 = DataLayoutDefaultValue.DataLayout2.Where(c => c.YOUXIANJI != null && c.YOUXIANJI != 0).OrderBy(c => c.YOUXIANJI);

            foreach (var e_GY_DATALAYOUT2 in eDataLayoutOrder2)
            {
                if (e_GY_DATALAYOUT2.YOUXIANJI != 0
                    && e_GY_DATALAYOUT2.YOUXIANJI != null
                    && e_GY_DATALAYOUT2.SHENGJIANGXH != 0
                    && e_GY_DATALAYOUT2.SHENGJIANGXH != null)
                {
                    GridColumn columnName = this.Columns[e_GY_DATALAYOUT2.FIELDNAME];
                    var info = new GridColumnSortInfo(columnName, e_GY_DATALAYOUT2.SHENGJIANGXH == 1 ? ColumnSortOrder.Ascending : ColumnSortOrder.Descending);
                    gridColumnSortInfos.Add(info);
                }

            }
            eDataLayoutOrder2 = DataLayoutDefaultValue.DataLayout2.Where(c => c.YOUXIANJI != null && c.YOUXIANJI == 0).OrderBy(c => c.YOUXIANJI);
            foreach (var e_GY_DATALAYOUT2 in eDataLayoutOrder2)
            {
                if (e_GY_DATALAYOUT2.YOUXIANJI == 0 && e_GY_DATALAYOUT2.YOUXIANJI != null && e_GY_DATALAYOUT2.SHENGJIANGXH != 0 && e_GY_DATALAYOUT2.SHENGJIANGXH != null)
                {
                    GridColumn columnName = this.Columns[e_GY_DATALAYOUT2.FIELDNAME];
                    var info = new GridColumnSortInfo(columnName, e_GY_DATALAYOUT2.SHENGJIANGXH == 1 ? ColumnSortOrder.Ascending : ColumnSortOrder.Descending);
                    gridColumnSortInfos.Add(info);
                }
            }

            var sortInfo = gridColumnSortInfos.ToArray();
            this.SortInfo.AddRange(sortInfo);
        }

        #endregion 列属性 加载数据库设置

        #region 行属性绑定

        /// <summary>
        /// 绑定行属性
        /// </summary>
        private void SetRowStyle()
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
            //this.OptionsView.ShowGroupPanel = _EDataLayout1.SHOWGROUPPANEL.ToInt(0) == 0 ? false : true;
            //是否允许过滤
            //this.OptionsCustomization.AllowFilter = _EDataLayout1.ALLOWFILTER.ToInt(1) == 0 ? false : true;
            //是否允许排序
            // this.OptionsCustomization.AllowSort = _EDataLayout1.ALLOWSORT.ToInt(1) == 0 ? false : true;
            //是显示列菜单
            // this.OptionsMenu.EnableColumnMenu = _EDataLayout1.ENABLECOLUMNMENU.ToInt(1) == 0 ? false : true;
        }

        // private Dictionary<string, ImeMode> imeModeDic = new Dictionary<string, ImeMode>();

        /// <summary>
        /// 列设置输入法模式
        /// </summary>
        /// <param name="columnFieldName">字段名</param>
        /// <param name="imeMode">输入模式</param>
        public void SetColumnImeMode(string columnFieldName, MediInfoImeMode imeMode)
        {
            if (immdic.ContainsKey(columnFieldName.ToUpper()))
            {
                immdic[columnFieldName.ToUpper()] = imeMode;
            }
            else
            {
                immdic.Add(columnFieldName.ToUpper(), imeMode);
            }
        }

        /// <summary>
        /// 移除列输入法模式
        /// </summary>
        /// <param name="columnFieldName"></param>
        /// <param name="imeMode"></param>
        public void RemoveColumnImeMode(string columnFieldName, MediInfoImeMode imeMode)
        {
            if (immdic.ContainsKey(columnFieldName.ToUpper()))
            {
                immdic.Remove(columnFieldName.ToUpper());
            }
        }

        /// <summary>
        /// 重置列输入法模式
        /// </summary>
        public void ResetColumnImeMode()
        {
            immdic.Clear();
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
        /// gridview布局信息
        /// </summary>
        protected E_GY_DATALAYOUTDTO dataLayoutInfo;

        //表达式属性不为空的控件集合
        private Dictionary<string, string> unBoundExpressionDic = new Dictionary<string, string>();

        /// <summary>
        /// TABINDEX TABLE
        /// </summary>
        public DataTable GridviewTabIndexdt { get; set; }

        /// <summary>
        /// 列默认值
        /// </summary>
        public DataTable GridviewColumnDefaultValuedt { get; set; }

        private DataTable UnBoundGridviewExpressiondt { get; set; }

        /// <summary>
        /// 创建其他类型表达式
        /// </summary>
        /// <returns></returns>
        private DataTable CreateGridviewOtherTypeExpressiondtTable()
        {
            DataTable gridviewsdic = new DataTable();
            gridviewsdic.Columns.AddRange(new DataColumn[] { new DataColumn("GridcontrolName", typeof(string)), new DataColumn("GridviewName", typeof(string)), new DataColumn("columnName", typeof(string)), new DataColumn("ExpressionType", typeof(string)), new DataColumn("ExpressionString", typeof(string)), new DataColumn("IsRowOrColumnOrOther", typeof(string)), new DataColumn("Description") });
            return gridviewsdic;
        }

        /// <summary>
        /// 列默认值
        /// </summary>
        public DataTable GridviewOtherTypeExpressiondt { get; set; }

        /// <summary>
        /// 创建列默认值表
        /// </summary>
        /// <returns></returns>
        private DataTable CreateGridviewColumnDefaultValueTable()
        {
            DataTable gridviewsdic = new DataTable();
            gridviewsdic.Columns.AddRange(new DataColumn[] { new DataColumn("GridcontrolName", typeof(string)), new DataColumn("GridviewName", typeof(string)), new DataColumn("columnName", typeof(string)), new DataColumn("DefaultValue", typeof(string)) });
            return gridviewsdic;
        }

        /// <summary>
        /// 创建表达式表
        /// </summary>
        private DataTable CreateGridviewUnboundExpressionTable()
        {
            DataTable gridviewsdic = new DataTable();
            gridviewsdic.Columns.AddRange(new DataColumn[] { new DataColumn("GridcontrolName", typeof(string)), new DataColumn("GridviewName", typeof(string)), new DataColumn("columnName", typeof(string)), new DataColumn("ExpressionString", typeof(string)) });
            return gridviewsdic;
        }

        /// <summary>
        /// 创建跳转索引表
        /// </summary>
        private DataTable CreateGridviewTabIndexTable()
        {
            DataTable gridviewTabIndexdt = new DataTable();
            gridviewTabIndexdt.Columns.AddRange(new DataColumn[] { new DataColumn("GridviewName", typeof(string)), new DataColumn("ColumnName", typeof(string)), new DataColumn("TabIndex", typeof(int)) });
            return gridviewTabIndexdt;
        }

        /// <summary>
        /// 结束初始化
        /// </summary>
        public override void EndInit()
        {
            if (!SkinCat.Instance.IsDesignMode)
            {

                RegisterEvent();
                SetSerialNo();

                initialGridviewLayoutDel = InitialGridviewLayout;
            }
            base.EndInit();

            this.OptionsMenu.EnableColumnMenu = false;
            this.OptionsCustomization.AllowFilter = false;
            //this.OptionsCustomization.AllowSort = true;
            this.OptionsView.ShowGroupPanel = false;
        }

        /// <summary>
        /// 保存当前控件的默认布局
        /// </summary>
        private void SaveDefaultLayout()
        {
            if (this.GridControl.FindForm() == null) return;
            DataLayoutDefaultValue = new E_GY_DATALAYOUTDTO();
            DataLayoutDefaultValue.DataLayout1 = new E_GY_DATALAYOUT1();
            DataLayoutDefaultValue.DataLayout2 = new List<E_GY_DATALAYOUT2>();

            DataLayoutDefaultValue.DataLayout1.YINGYONGID = HISClientHelper.YINGYONGID;
            DataLayoutDefaultValue.DataLayout1.FORMNAME = this.GridControl.FindForm().Name;
            DataLayoutDefaultValue.DataLayout1.CONTROLNAME = this.Name;
            DataLayoutDefaultValue.DataLayout1.NAMESPACE = this.GridControl.FindForm().GetType().Namespace;
            //DataLayoutDefaultValue.DataLayout1.ALLOWFILTER = this.OptionsCustomization.AllowFilter == true ? 1 : 0;
            //DataLayoutDefaultValue.DataLayout1.ALLOWSORT = this.OptionsCustomization.AllowSort == true ? 1 : 0;
            //DataLayoutDefaultValue.DataLayout1.ENABLECOLUMNMENU = this.OptionsMenu.EnableColumnMenu == true ? 1 : 0;
            DataLayoutDefaultValue.DataLayout1.ORDERBYFIELD = "";
            DataLayoutDefaultValue.DataLayout1.SHOWGROUPPANEL = this.OptionsView.ShowGroupPanel == true ? 1 : 0;
            DataLayoutDefaultValue.DataLayout1.LINENUMBER = this.IsShowLineNumber == true ? 1 : 0;
            DataLayoutDefaultValue.DataLayout1.ROWBACKCOLOREXPRESSION = "";
            DataLayoutDefaultValue.DataLayout1.ROWBACKCOLORDESCRIBE = "";
            DataLayoutDefaultValue.DataLayout1.ROWFONTSIZE = Convert.ToInt32(this.Appearance.Row.Font.Size);
            for (int i = 0; i < this.Columns.Count; i++)
            {
                E_GY_DATALAYOUT2 e_GY_DATALAYOUT2 = new E_GY_DATALAYOUT2();

                //e_GY_DATALAYOUT2.DATALAYOUTID = observerMediGridview.Columns[i]
                e_GY_DATALAYOUT2.FIELDNAME = this.Columns[i].FieldName;
                e_GY_DATALAYOUT2.CAPTION = this.Columns[i].Caption;
                e_GY_DATALAYOUT2.VISIBLE = this.Columns[i].Visible == true ? 1 : 0;
                e_GY_DATALAYOUT2.WIDTH = this.Columns[i].Width;
                e_GY_DATALAYOUT2.FIXED = (int)this.Columns[i].Fixed;
                e_GY_DATALAYOUT2.HEADERFONTSIZE = Convert.ToInt32(this.Columns[i].AppearanceHeader.Font.Size);
                e_GY_DATALAYOUT2.HEADERHALIGNMENT = (int)this.Columns[i].AppearanceHeader.TextOptions.HAlignment;
                e_GY_DATALAYOUT2.TABINDEX = -1;//跳转顺序----待定
                e_GY_DATALAYOUT2.READONLY = this.Columns[i].ReadOnly == true ? 1 : 0;
                e_GY_DATALAYOUT2.DEFAULTVALUE = "";//默认值待定
                e_GY_DATALAYOUT2.CELLFONTSIZE = Convert.ToInt32(this.Columns[i].AppearanceCell.Font.Size);
                e_GY_DATALAYOUT2.CELLHALIGNMENT = (int)this.Columns[i].AppearanceCell.TextOptions.HAlignment;
                e_GY_DATALAYOUT2.IMEMODE = "";//输入法模式待定
                e_GY_DATALAYOUT2.VALIDATEEXPRISSION = "";//表达式待定
                e_GY_DATALAYOUT2.VALIDATEDESCRIBE = "";//有效性说明
                e_GY_DATALAYOUT2.FORMATSTRING = "";//显示格式
                e_GY_DATALAYOUT2.FORMATTYPE = 0;//显示格式类型
                e_GY_DATALAYOUT2.YOUXIANJI = 0;//优先级
                e_GY_DATALAYOUT2.SORTNO = this.Columns[i].VisibleIndex;
                DataLayoutDefaultValue.DataLayout2.Add(e_GY_DATALAYOUT2);
            }
        }

        /// <summary>
        /// 从数据库取页面布局
        /// </summary>
        public void GetDataLayoutForDB()
        {
            if (string.IsNullOrWhiteSpace(HISClientHelper.YINGYONGID))
                return;
            if (this.GridControl.FindForm() == null) return;
            E_GY_DATALAYOUTDTO datalayoutinfo = GYDataLayoutHelper.GetDataLayoutInfo(this.Name, this.GridControl.FindForm().Name.ToString(), this.GridControl.FindForm().GetType().Namespace, HISClientHelper.YINGYONGID);
            if (datalayoutinfo != null)
            {
                _EDataLayout1 = datalayoutinfo.DataLayout1;
                _EDataLayout2 = datalayoutinfo.DataLayout2;
                DataLayoutCustomValue = datalayoutinfo;
                SetRowStyle();
                RunViewColunmProcess();
            }
            this.OptionsCustomization.AllowColumnMoving = true;
        }

        /// <summary>
        /// 更新页面样式
        /// </summary>
        /// <param name="dataLayoutInfo">页面布局信息</param>
        protected void GridviewAttributeSet(E_GY_DATALAYOUTDTO dataLayoutInfo)
        {
            _EDataLayout1 = dataLayoutInfo.DataLayout1;
            _EDataLayout2 = dataLayoutInfo.DataLayout2;
            SetRowStyle();
            RunViewColunmProcess();
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
            //非数据源绑定
            this.CustomUnboundColumnData -= MediGridView_CustomUnboundColumnData;
            this.CustomUnboundColumnData += MediGridView_CustomUnboundColumnData;
            this.MouseMove -= MediGridView_MouseMove;
            this.MouseMove += MediGridView_MouseMove;
            this.MouseLeave -= MediGridView_MouseLeave;
            this.MouseLeave += MediGridView_MouseLeave;

            this.FocusedColumnChanged -= MediGridView_FocusedColumnChanged;
            this.FocusedColumnChanged += MediGridView_FocusedColumnChanged;
            this.MouseWheel -= MediGridView_MouseWheel;
            this.MouseWheel += MediGridView_MouseWheel;
            //打印预览
            this.PrintInitialize -= MediGridView_PrintInitialize;
            this.PrintInitialize += MediGridView_PrintInitialize;
            //单击单元格事件
            this.RowCellClick -= MediGridView_RowCellClick;
            this.RowCellClick += MediGridView_RowCellClick;

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
            //右键双击事件
            this.DoubleClick -= GridViewEx_DoubleClick;
            this.DoubleClick += GridViewEx_DoubleClick;
            this.CustomDrawCell -= MediGridView_CustomDrawCell;
            this.CustomDrawCell += MediGridView_CustomDrawCell;
            this.CustomRowCellEdit -= MediGridView_CustomRowCellEdit;
            this.CustomRowCellEdit += MediGridView_CustomRowCellEdit;
            //校验提示信息
            this.InvalidValueException -= MediGridView_InvalidValueException;
            this.InvalidValueException += MediGridView_InvalidValueException;

            //聚焦行改变事件
            this.FocusedRowChanged -= MediGridView_FocusedRowChanged;
            this.FocusedRowChanged += MediGridView_FocusedRowChanged;

            //单元格背景色设置
            this.RowCellStyle -= MediGridView_RowCellStyle;
            this.RowCellStyle += MediGridView_RowCellStyle;
            //计算行高
            this.CalcRowHeight -= MediGridView_CalcRowHeight;
            this.CalcRowHeight += MediGridView_CalcRowHeight;
            //设置默认值
            this.InitNewRow -= MediGridView_InitNewRow;
            this.InitNewRow += MediGridView_InitNewRow;
            //跳转事件
            this.KeyDown -= MediGridView_KeyDown;
            this.KeyDown += MediGridView_KeyDown;
            this.MouseDown -= MediGridView_MouseDown;
            this.MouseDown += MediGridView_MouseDown;
            //gridcontrol加载事件
            this.GridControl.Load -= GridControl_Load;
            this.GridControl.Load += GridControl_Load;
            //输入法模式设置
            this.ShownEditor -= MediGridView_ShownEditor;
            this.ShownEditor += MediGridView_ShownEditor;

            this.GotFocus -= MediGridView_GotFocus;
            this.GotFocus += MediGridView_GotFocus;
            this.LostFocus -= MediGridView_LostFocus;
            this.LostFocus += MediGridView_LostFocus;
            this.Disposed -= MediGridView_Disposed;
            this.Disposed += MediGridView_Disposed;

            //新增行事件
            if (this.GridControl.DataSource != null)
            {
                ((BindingSource)this.GridControl.DataSource).AddingNew -= MediGridView_AddingNew;
                ((BindingSource)this.GridControl.DataSource).AddingNew += MediGridView_AddingNew;
            }
        }

        private void MediGridView_Disposed(object sender, EventArgs e)
        {
            //释放定时器
            timer?.Dispose();
        }

        private void MediGridView_LostFocus(object sender, EventArgs e)
        {
            if (isNoneFocusedWhenLostFocus)
                this.ClearFocuseRow();

        }

        private void MediGridView_GotFocus(object sender, EventArgs e)
        {

            if (isNoneFocusedWhenLostFocus)
                this.EnabledFocusedRow();
        }

        private void MediGridView_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (colActionMarkColumn != null)
            {
                #region 鼠标悬浮显示对应行按钮

                if (e.Column.Name == colActionMarkColumn.Name)
                {
                    if (e.RowHandle == mouseHoverRowHandle)
                        e.RepositoryItem = rpiaddpicmark;
                    else
                        e.RepositoryItem = emptyItemButtonEdit;
                }

                #endregion
            }
        }

        private void MediGridView_MouseLeave(object sender, EventArgs e)
        {

        }

        private void MediGridView_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            //焦点离开后根据选中行行号设置背景色（离开后背景色不变）
            if (isNoneFocusedWhenLostFocus)
            {
                if (e.RowHandle == rowIndex)
                {
                    e.Appearance.BackColor = Color.FromArgb(154, 215, 255);
                }
            }

            if (e.RowHandle == this.FocusedRowHandle &&
                e.Column == this.FocusedColumn &&
                this.CustomBorderOnRowCellFocus &&
                String.Compare(e.Column.FieldName, "ActionMark", StringComparison.OrdinalIgnoreCase) != 0)
            {
                this.FocusRectStyle = DrawFocusRectStyle.CellFocus;
                this.OptionsSelection.EnableAppearanceFocusedCell = true;

                e.Cache.Paint = xPaint;
            }
        }

        private void MediGridView_MouseMove(object sender, MouseEventArgs e)
        {
            MediGridView view = sender as MediGridView;
            if (SkinCat.Instance.IsDesignMode)
                return;
            if (view != null && (!view.isShowAddMark && !view.isShowMinusMark && !view.isShowGengDuoMark && !view.isShowZanTingMark && !view.isShowBaoBiaoMark && !view.isShowBianJiMark && !view.isShowTiHuanMark && !view.isShowQuXiaoTHMark && !view.isShowUPMark && !view.isShowDOWNMark && !view.isShowZZDMark))
                return;
        }

        private void MediGridView_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            if ((GridControl.Parent is SplitGroupPanel))
            {
                if ((SplitGroupPanel)GridControl.Parent == null ||
                    ((SplitGroupPanel)GridControl.Parent).Owner == null ||
                    (((SplitGroupPanel)GridControl.Parent).Owner is Mediinfo.WinForm.HIS.Controls.MediGridSplitContainer))
                {
                    return;
                }
            }

            if (this.FocusedColumn == null) return;

            if (this.FocusedColumn.RealColumnEdit is RepositoryItemGridLookUpEdit)
            {
                this.ShowEditor();
                if (ActiveEditor is MediGridLookUpEdit upEdit)
                {
                    if (this.ActiveEditor is MediGridLookUpEdit edit && edit.Properties.TextEditStyle == TextEditStyles.DisableTextEditor)
                    {
                        edit.ShowPopup();
                    }
                }

            }
            else if (this.FocusedColumn.RealColumnEdit is RepositoryItemMediDateEdit)
            {
                this.ShowEditor();
                if (ActiveEditor is MediDateEdit)
                {
                    if (this.ActiveEditor is MediDateEdit edit && (!edit.ReadOnly || edit.Enabled))
                        edit.ShowPopup();

                }

            }

        }

        private void MediGridView_RowClick(object sender, RowClickEventArgs e)
        {
            if (this.OptionsSelection.MultiSelect && this.OptionsSelection.MultiSelectMode == GridMultiSelectMode.CheckBoxRowSelect)
            {
                if (!this.FocusedColumn.FieldName.Equals("DX$CheckboxSelectorColumn"))
                    this.InvertRowSelection(e.RowHandle);

            }
        }

        private void MediGridView_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
                this.ScrollInfo.VScroll.Value += 1;
            else
                this.ScrollInfo.VScroll.Value -= 1;
        }

        private void MediGridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (FocusedByRight)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    GridView view = sender as GridView;
                    GridHitInfo hi = view.CalcHitInfo(e.Location);
                    DataRow row = null;
                    if (hi.InRow)
                        row = view.GetDataRow(hi.RowHandle);
                    if (row != null)
                    {
                        this.FocusedRowHandle = hi.RowHandle;
                    }
                }
            }

            if ((GridControl.Parent is SplitGroupPanel panel))
            {
                if ((SplitGroupPanel)GridControl.Parent == null ||
                    ((SplitGroupPanel)GridControl.Parent).Owner == null ||
                    (((SplitGroupPanel)GridControl.Parent).Owner is MediGridSplitContainer))
                {
                    GridHitInfo hitInfo = this.CalcHitInfo(e.Location);
                    if (hitInfo.Column == null || (hitInfo.Column.FieldName.Equals("addPicMark") || hitInfo.Column.FieldName.Equals("minusPicMark")))
                        return;
                }
            }
        }
        /// <summary>
        /// 记录当前输入法状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnActiveEditor_LostFocus(object sender, EventArgs e)
        {
            if (!(sender is IInputIMEMode))
            {
                if (OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows8 || OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows10)
                {

                }
                else
                {
                    int globalconversion = 0;
                    int globalsentence = 0;
                    IntPtr prt = IMMModeHelper.ImmGetContext(((BaseEdit)sender).Handle);
                    IMMModeHelper.ImmGetConversionStatus(prt, ref globalconversion, ref globalsentence);
                    IMMModeHelper.globalconversion = globalconversion;
                    IMMModeHelper.globalsentence = globalsentence;
                }
            }
            else
            {
                IInputIMEMode editor = (IInputIMEMode)sender;
                if (editor.MediinfoIMEMode == MediInfoImeMode.CHS)
                {
                    if (OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows8 || OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows10)
                    {

                    }
                    else
                    {
                        int globalconversion = 0;
                        int globalsentence = 0;
                        IntPtr prt = IMMModeHelper.ImmGetContext(((BaseEdit)sender).Handle);
                        IMMModeHelper.ImmGetConversionStatus(prt, ref globalconversion, ref globalsentence);
                        IMMModeHelper.globalconversion = globalconversion;
                        IMMModeHelper.globalsentence = globalsentence;
                    }
                }
                else if (editor.MediinfoIMEMode == MediInfoImeMode.EN)
                {
                    if (OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows8 || OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows10)
                    {

                    }
                    else
                    {
                        int globalconversion = 0;
                        int globalsentence = 0;
                        IntPtr prt = IMMModeHelper.ImmGetContext(((BaseEdit)sender).Handle);
                        IMMModeHelper.ImmGetConversionStatus(prt, ref globalconversion, ref globalsentence);
                        IMMModeHelper.globalconversion = globalconversion;
                        IMMModeHelper.globalsentence = globalsentence;
                    }
                }
                else
                {
                    if (OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows8 || OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows10)
                    {

                    }
                    else
                    {
                        int globalconversion = 0;
                        int globalsentence = 0;
                        IntPtr prt = IMMModeHelper.ImmGetContext(((BaseEdit)sender).Handle);
                        IMMModeHelper.ImmGetConversionStatus(prt, ref globalconversion, ref globalsentence);
                        IMMModeHelper.globalconversion = globalconversion;
                        IMMModeHelper.globalsentence = globalsentence;
                        if (sender is GridLookUpEdit && inputMethodTimer != null) inputMethodTimer.Dispose();
                    }
                }
            }

        }

        /// <summary>
        /// 获取当前输入法状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnActiveEditor_GotFocus(object sender, EventArgs e)
        {
            if (!(sender is IInputIMEMode))
            {
                if (OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows8 || OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows10)
                {

                }
                else
                {
                    IntPtr prt = IMMModeHelper.ImmGetContext(((BaseEdit)sender).Handle);
                    IMMModeHelper.ImmSetConversionStatus(prt, IMMModeHelper.globalconversion, IMMModeHelper.globalsentence);
                }
            }
            else
            {
                IInputIMEMode editor = (IInputIMEMode)sender;
                if (editor.MediinfoIMEMode == MediInfoImeMode.CHS)
                {
                    if (OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows8 || OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows10)
                    {

                    }
                    else
                    {
                        IntPtr prt = IMMModeHelper.ImmGetContext(((BaseEdit)sender).Handle);
                        IMMModeHelper.ImmSetConversionStatus(prt, IMMModeHelper.globalconversion, IMMModeHelper.globalsentence);
                    }
                }
                else if (editor.MediinfoIMEMode == MediInfoImeMode.EN)
                {
                    if (OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows8 || OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows10)
                    {

                    }
                    else
                    {
                        IntPtr prt = IMMModeHelper.ImmGetContext(((BaseEdit)sender).Handle);
                        IMMModeHelper.ImmSetConversionStatus(prt, IMMModeHelper.globalconversion, IMMModeHelper.globalsentence);
                    }
                }
                else
                {
                    if (OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows8 || OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows10)
                    {

                    }
                    else
                    {
                        IntPtr prt = IMMModeHelper.ImmGetContext(((BaseEdit)sender).Handle);
                        IMMModeHelper.ImmSetConversionStatus(prt, IMMModeHelper.globalconversion, IMMModeHelper.globalsentence);
                    }
                }
            }

        }
        private System.Threading.Timer inputMethodTimer;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediGridView_ShownEditor(object sender, EventArgs e)
        {
            if (this.ActiveEditor == null) return;
            if (!(sender is IInputIMEMode))
            {
                if (OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows8 || OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows10)
                {

                }
                else
                {
                    IntPtr prt = IMMModeHelper.ImmGetContext(this.ActiveEditor.Handle);
                    IMMModeHelper.ImmSetConversionStatus(prt, IMMModeHelper.globalconversion, IMMModeHelper.globalsentence);
                }
            }
            else
            {
                IInputIMEMode editor = (IInputIMEMode)this.ActiveEditor;
                if (editor.MediinfoIMEMode == MediInfoImeMode.CHS)
                {
                    if (OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows8 || OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows10)
                    {

                    }
                    else
                    {
                        IntPtr prt = IMMModeHelper.ImmGetContext(((BaseEdit)editor).Handle);
                        IMMModeHelper.ImmSetConversionStatus(prt, IMMModeHelper.globalconversion, IMMModeHelper.globalsentence);
                    }
                }
                else if (editor.MediinfoIMEMode == MediInfoImeMode.EN)
                {
                    if (OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows8 || OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows10)
                    {

                    }
                    else
                    {
                        IntPtr prt = IMMModeHelper.ImmGetContext(((BaseEdit)editor).Handle);
                        IMMModeHelper.ImmSetConversionStatus(prt, IMMModeHelper.globalconversion, IMMModeHelper.globalsentence);
                    }
                }
                else
                {
                    if (OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows8 || OSHelper.GetOSType() == OSHelper.OSVersionNo.Windows10)
                    {

                    }
                    else
                    {
                        IntPtr prt = IMMModeHelper.ImmGetContext(((BaseEdit)editor).Handle);
                        IMMModeHelper.ImmSetConversionStatus(prt, IMMModeHelper.globalconversion, IMMModeHelper.globalsentence);
                        if (this.ActiveEditor is GridLookUpEdit) inputMethodTimer = new System.Threading.Timer(SetInputMethodofTimer, this.ActiveEditor, 200, Timeout.Infinite);
                    }
                }
            }

            //gxl 2019.11.06   添加当autoselect为true时聚焦单元格时自动全选数据
            if (AutoSelect && this.ActiveEditor is MediTextBox)
            {
                var oEdit = (TextEdit)this.ActiveEditor;
                if (null != oEdit)
                    this.GridControl.BeginInvoke(new MethodInvoker(() =>
                    {
                        oEdit.SelectionStart = 0;
                        oEdit.SelectionLength = oEdit.Text.Length;
                    }));
            }
        }

        /// <summary>
        /// 通过定时器设置输入法
        /// </summary>
        /// <param name="sender"></param>
        private void SetInputMethodofTimer(object sender)
        {
            if (OSHelper.GetOSType() != OSHelper.OSVersionNo.Windows8 && OSHelper.GetOSType() != OSHelper.OSVersionNo.Windows10)
            {
                int globalconversion = 0;
                int globalsentence = 0;
                IntPtr prt = IMMModeHelper.ImmGetContext(((BaseEdit)sender).Handle);
                IMMModeHelper.ImmGetConversionStatus(prt, ref globalconversion, ref globalsentence);
                IMMModeHelper.globalconversion = globalconversion;
                IMMModeHelper.globalsentence = globalsentence;
            }
        }
        private void GridControl_Validated(object sender, EventArgs e)
        {
            if (!SkinCat.Instance.IsDesignMode)
            {
                if (DataLayoutCustomValue != null)
                {
                    if (DataLayoutCustomValue.DataLayout2 != null)
                        foreach (var item in DataLayoutCustomValue.DataLayout2)
                        {
                            if (item.NONEMPTY != null && item.NONEMPTY == 1)
                            {
                                for (int i = 0; i < this.DataRowCount; i++)
                                {
                                    if (string.IsNullOrWhiteSpace(this.GetRowCellValue(i, this.Columns["item.FIELDNAME.ToUpper()"]).ToStringEx()))
                                    {
                                        MediMsgBox.Show("当前值不允许为空!");
                                        break;
                                    }
                                }
                            }
                        }
                }
            }
        }

        /// <summary>
        /// 单元格背景色设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //记录选中行行号
            if (e.RowHandle >= 0)
            {
                rowIndex = this.FocusedRowHandle;
            }

            if (DataLayoutCustomValue != null)
            {
                switch ((ExpressionType)Convert.ToInt32(3))
                {
                    case ExpressionType.BackColorType:
                        switch ((ExpressionApplyType)Convert.ToInt32(2))
                        {
                            case ExpressionApplyType.RowType:

                                break;

                            case ExpressionApplyType.ColumnType:

                                if (DataLayoutCustomValue.DataLayout2 != null)
                                {
                                    DataLayoutCustomValue.DataLayout2.ForEach(o =>
                                    {
                                        if (!string.IsNullOrWhiteSpace(o.BACKCOLOREXPRISSION) && o.FIELDNAME.ToUpper().Equals(e.Column.FieldName.ToUpper()))
                                        {
                                            string[] unboundExpressions = o.BACKCOLOREXPRISSION.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                                            foreach (var t in unboundExpressions)
                                            {
                                                //在查找component类控件
                                                System.Drawing.Color? expressionResult = ExpressionHelper.ColorExpressionFunc(string.Empty, t, this.GridControl, this, ExpressionApplyType.ColumnType, ExpressionType.BackColorType, e.RowHandle);
                                                if (expressionResult != null)
                                                    e.Appearance.BackColor = (System.Drawing.Color)expressionResult;
                                            }
                                        }

                                        if (!string.IsNullOrWhiteSpace(o.CELLFORECOLOREXPRISSION))
                                        {
                                            if (!o.FIELDNAME.ToUpper().Equals(e.Column.FieldName.ToUpper())) return;
                                            string[] unboundExpressions = o.CELLFORECOLOREXPRISSION.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                                            foreach (var t in unboundExpressions)
                                            {
                                                //在查找component类控件
                                                System.Drawing.Color? expressionResult = ExpressionHelper.ColorExpressionFunc(string.Empty, t, this.GridControl, this, ExpressionApplyType.ColumnType, ExpressionType.ForeColorType, e.RowHandle);
                                                if (expressionResult != null)
                                                    e.Appearance.ForeColor = (System.Drawing.Color)expressionResult;
                                            }
                                        }
                                    });
                                }
                                break;

                            default:
                                break;
                        }
                        break;
                }
            }

            if (CurrentCell != null)
                CurrentCell.Appearance.BackColor = this.Appearance.FocusedRow.BackColor;
        }

        /// <summary>
        /// 验证信息提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediGridView_InvalidValueException(object sender, InvalidValueExceptionEventArgs e)
        {
            // 如果输入信息用"{}"表示自定义信息
            if (!e.ErrorText.Contains("{") && !e.ErrorText.Contains("}"))
            {
                e.ErrorText = "输入的字符串不是正确的格式.";
            }
            else
            {
                e.ErrorText = e.ErrorText.Replace("{", "").Replace("}", "");
            }
        }

        /// <summary>
        /// 声明初始化布局委托
        /// </summary>
        public delegate void InitialGridviewLayoutDel();

        /// <summary>
        /// 定义委托变量
        /// </summary>
        public InitialGridviewLayoutDel initialGridviewLayoutDel;

        /// <summary>
        /// 初始化页面布局
        /// </summary>
        public void InitialGridviewLayout()
        {
        }

        private bool isShowLine = true;
        /// <summary>
        /// 是否显示网格线
        /// </summary>
        [DefaultValue(true), Description("是否显示表格水平和垂直分割线")]
        public bool IsShowLine
        {
            get => isShowLine;
            set
            {
                isShowLine = value;
                if (isShowLine)
                {
                    this.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.True;
                    this.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.True;
                }
                else
                {
                    this.OptionsView.ShowHorizontalLines = showMediHorizontalLines ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
                    this.OptionsView.ShowVerticalLines = showMediVerticalLines ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
                }

            }
        }
        private string path = AppDomain.CurrentDomain.BaseDirectory;//System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        /// <summary>
        /// gridcontrol加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridControl_Load(object sender, EventArgs e)
        {
            if (!SkinCat.Instance.IsDesignMode)
            {
                if (!string.IsNullOrWhiteSpace(HISClientHelper.XITONGID) && !HISClientHelper.LINCHUANGBZ.Equals(0))
                {
                    this.ColumnPanelRowHeight = 30;
                    if (IsShowLine)
                    {
                        this.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.True;
                        this.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.True;
                    }
                    else
                    {
                        this.OptionsView.ShowHorizontalLines = showMediHorizontalLines ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
                        this.OptionsView.ShowVerticalLines = showMediVerticalLines ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
                    }

                }
                SaveDefaultLayout();
                //获取表格信息地址
                if (!string.IsNullOrWhiteSpace(this.GridControl.FindForm().Name))
                {
                    var readPath = $@"{path}{this.GridControl.FindForm().Name}.xml";
                    if (File.Exists(readPath))
                    {
                        this.RestoreLayoutFromXml(readPath);
                        GetDesignConfig(sender);
                    }
                    else
                    {
                        GetDataLayoutForDB();
                    }
                }
            }
            this.OptionsCustomization.AllowColumnMoving = true;
        }

        /// <summary>
        /// 获取本地表风格设计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e_GY_DATALAYOUT2"></param>
        private void GetDesignConfig(object sender)
        {
            try
            {
                string pathconfig = $@"{path}Config.xml";
                if (File.Exists(pathconfig))
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(pathconfig);
                    GridView _GridView = ((sender as GridControl).Views[0] as GridView);
                    if (_GridView != null && _GridView.Columns.Count > 0 && DataLayoutDefaultValue.DataLayout2 != null && DataLayoutDefaultValue.DataLayout2.Count > 0)
                    {
                        string GetValue = "";
                        GridViewStyleSetFrm gridViewStyleSetFrm = new GridViewStyleSetFrm();
                        for (int a = 0; a < _GridView.Columns.Count; a++)
                        {
                            DataLayoutDefaultValue.DataLayout2.ForEach(r =>
                            {
                                if (r.FIELDNAME == _GridView.Columns[a].FieldName)
                                {
                                    r.SHENGJIANGXH = _GridView.Columns[a].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending ? 1 : _GridView.Columns[a].SortOrder == DevExpress.Data.ColumnSortOrder.Descending ? 2 : 0;//升降序
                                    if (File.Exists(pathconfig) && xmlDocument.SelectSingleNode($@"YOUXIANJIPZ/{this.GridControl.FindForm().Name}") != null)
                                    {
                                        gridViewStyleSetFrm.IsSelectNode(pathconfig, $@"YOUXIANJIPZ/{this.GridControl.FindForm().Name}/{r.FIELDNAME}/YOUXIANJI", "Get", ref GetValue);
                                        r.YOUXIANJI = string.IsNullOrWhiteSpace(GetValue) ? 0 : Convert.ToInt32(GetValue);
                                    }
                                }
                            });
                        }
                    }

                    //数据排序
                    this.SortInfo.Clear();
                    List<GridColumnSortInfo> gridColumnSortInfos = new List<GridColumnSortInfo>();
                    //var eDataLayoutOrder2 = DataLayoutDefaultValue.DataLayout2.Where(c => c.YOUXIANJI != 0 && c.YOUXIANJI != null).OrderBy(c => c.YOUXIANJI);
                    var eDataLayoutOrder2 = DataLayoutDefaultValue.DataLayout2.Where(c => c.YOUXIANJI != null && c.YOUXIANJI != 0).OrderBy(c => c.YOUXIANJI);
                
                    foreach (var e_GY_DATALAYOUT2 in eDataLayoutOrder2)
                    {
                        if (e_GY_DATALAYOUT2.YOUXIANJI != 0
                            && e_GY_DATALAYOUT2.YOUXIANJI != null
                            && e_GY_DATALAYOUT2.SHENGJIANGXH != 0
                            && e_GY_DATALAYOUT2.SHENGJIANGXH != null)
                        {
                            GridColumn columnName = this.Columns[e_GY_DATALAYOUT2.FIELDNAME];
                            var info = new GridColumnSortInfo(columnName, e_GY_DATALAYOUT2.SHENGJIANGXH == 1 ? ColumnSortOrder.Ascending : ColumnSortOrder.Descending);
                            gridColumnSortInfos.Add(info);
                        }
                       
                    }
                    eDataLayoutOrder2 = DataLayoutDefaultValue.DataLayout2.Where(c => c.YOUXIANJI != null && c.YOUXIANJI == 0).OrderBy(c => c.YOUXIANJI);
                    foreach (var e_GY_DATALAYOUT2 in eDataLayoutOrder2)
                    {
                        if (e_GY_DATALAYOUT2.YOUXIANJI == 0 && e_GY_DATALAYOUT2.YOUXIANJI != null && e_GY_DATALAYOUT2.SHENGJIANGXH != 0 && e_GY_DATALAYOUT2.SHENGJIANGXH != null)
                        {
                            GridColumn columnName = this.Columns[e_GY_DATALAYOUT2.FIELDNAME];
                            var info = new GridColumnSortInfo(columnName, e_GY_DATALAYOUT2.SHENGJIANGXH == 1 ? ColumnSortOrder.Ascending : ColumnSortOrder.Descending);
                            gridColumnSortInfos.Add(info);
                        }
                    }


                    var sortInfo = gridColumnSortInfos.ToArray();
                    this.SortInfo.AddRange(sortInfo);
                }
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// 打印初始化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediGridView_PrintInitialize(object sender, PrintInitializeEventArgs e)
        {
            PrintingSystemBase pb = e.PrintingSystem as PrintingSystemBase;
            pb.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
        }

        /// <summary>
        /// 是否显示复选框
        /// </summary>
        [DefaultValue(false), Description("是否显示复选框")]
        public bool IsShowCheckBoX
        {
            get { return _IsShowCheckBox; }
            set
            {
                _IsShowCheckBox = value;
                if (!SkinCat.Instance.IsDesignMode)
                    SetCheckBox();
            }
        }

        private int actionColumnWidth = 50;
        /// <summary>
        /// 操作列列宽
        /// </summary>
        [DefaultValue(50), Description("操作列列宽")]
        public int ActionColumnWidth
        {
            get { return actionColumnWidth; }
            set { actionColumnWidth = value; }

        }

        private bool isShowAddMark = false;
        /// <summary>
        /// 是否显示"+"号按钮
        /// </summary>
        [DefaultValue(false), Description("是否添加\"+\"按钮")]
        public bool IsShowAddMark
        {
            get { return isShowAddMark; }
            set
            {
                isShowAddMark = value;
                if (!SkinCat.Instance.IsDesignMode)
                    SetAddMark(CreateColumn());
            }
        }

        private bool isShowMinusMark = false;
        /// <summary>
        /// 是否显示"-"号按钮
        /// </summary>
        [DefaultValue(false), Description("是否添加\"-\"按钮")]
        public bool IsShowMinusMark
        {
            get { return isShowMinusMark; }
            set
            {
                isShowMinusMark = value;
                if (!SkinCat.Instance.IsDesignMode)
                    SetMinusMark(CreateColumn());
            }
        }
        private bool isShowGengDuoMark = false;
        /// <summary>
        /// 是否显示更多操作按钮
        /// </summary>
        [DefaultValue(false), Description("是否显示更多操作按钮")]
        public bool IsShowGengDuoMark
        {
            get { return isShowGengDuoMark; }
            set
            {
                isShowGengDuoMark = value;
                if (!SkinCat.Instance.IsDesignMode)
                    SetGengDuoMark(CreateColumn());
            }
        }
        private bool isShowZanTingMark = false;
        /// <summary>
        /// 是否显示停止按钮
        /// </summary>
        [DefaultValue(false), Description("是否显示停止按钮")]
        public bool IsShowZanTingMark
        {
            get { return isShowZanTingMark; }
            set
            {
                isShowZanTingMark = value;
                if (!SkinCat.Instance.IsDesignMode)
                    SetZanTingMark(CreateColumn());
            }
        }

        private bool isShowBaoBiaoMark = false;
        /// <summary>
        /// 是否显示报表按钮
        /// </summary>
        [DefaultValue(false), Description("是否显示报表按钮")]
        public bool IsShowBaoBiaoMark
        {
            get { return isShowBaoBiaoMark; }
            set
            {
                isShowBaoBiaoMark = value;
                if (!SkinCat.Instance.IsDesignMode)
                    SetBaoBiaoMark(CreateColumn());
            }
        }

        private bool isShowBianJiMark = false;
        /// <summary>
        /// 是否显示编辑按钮
        /// </summary>
        [DefaultValue(false), Description("是否显示编辑按钮")]
        public bool IsShowBianJiMark
        {
            get { return isShowBianJiMark; }
            set
            {
                isShowBianJiMark = value;
                if (!SkinCat.Instance.IsDesignMode)
                    SetBianJiMark(CreateColumn());
            }
        }

        private bool isShowTiHuanMark = false;
        /// <summary>
        /// 是否显示替换按钮
        /// </summary>
        [DefaultValue(false), Description("是否显示替换按钮")]
        public bool IsShowTiHuanMark
        {
            get { return isShowTiHuanMark; }
            set
            {
                isShowTiHuanMark = value;
                if (!SkinCat.Instance.IsDesignMode)
                    SetTiHuanMark(CreateColumn());
            }
        }

        private bool isShowQuXiaoTHMark = false;
        /// <summary>
        /// 是否显示取消替换按钮
        /// </summary>
        [DefaultValue(false), Description("是否显示取消替换按钮")]
        public bool IsShowQuXiaoTHMark
        {
            get { return isShowQuXiaoTHMark; }
            set
            {
                isShowQuXiaoTHMark = value;
                if (!SkinCat.Instance.IsDesignMode)
                    SetQuXiaoTHMark(CreateColumn());
            }
        }

        private bool isShowUPMark = false;
        /// <summary>
        /// 是否显示箭头向上按钮
        /// </summary>
        [DefaultValue(false), Description("是否显示箭头向上按钮")]
        public bool IsShowUPMark
        {
            get { return isShowUPMark; }
            set
            {
                isShowUPMark = value;
                if (!SkinCat.Instance.IsDesignMode)
                    SetUPMark(CreateColumn());
            }
        }


        private bool isShowDOWNMark = false;
        /// <summary>
        /// 是否显示箭头向下按钮
        /// </summary>
        [DefaultValue(false), Description("是否显示箭头向下按钮")]
        public bool IsShowDOWNMark
        {
            get { return isShowDOWNMark; }
            set
            {
                isShowDOWNMark = value;
                if (!SkinCat.Instance.IsDesignMode)
                    SetDOWNMark(CreateColumn());
            }
        }

        private bool isShowZZDMark = false;
        /// <summary>
        /// 是否显示子诊断按钮
        /// </summary>
        [DefaultValue(false), Description("是否显示子诊断按钮")]
        public bool IsShowZZDMark
        {
            get { return isShowZZDMark; }
            set
            {
                isShowZZDMark = value;
                if (!SkinCat.Instance.IsDesignMode)
                    SetZZDMark(CreateColumn());
            }
        }

        /// <summary>
        /// 是否显示复选框
        /// </summary>
        private bool _IsShowCheckBox = false;
        #region 设置增减符号
        /// <summary>
        /// 设置加号
        /// </summary>
        private void SetAddMark(GridColumn gridColumn)
        {
            if (rpiaddpicmark == null)
                return;
            EditorButton editorButton = new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), null, null, null, null, "", "0", null, DevExpress.Utils.ToolTipAnchor.Default);
            SetMarkButton(isShowAddMark, editorButton);
        }

        /// <summary>
        /// 创建按钮列
        /// </summary>
        private GridColumn CreateColumn()
        {
            GridColumnCollection gridColumnCollection = this.Columns;
            bool exist = false;
            for (int i = 0; i < gridColumnCollection.Count; i++)
            {
                GridColumn gridColumn = gridColumnCollection[i];

                if (gridColumn.FieldName.ToUpper() == "ACTIONMARK")
                {
                    exist = true;

                    gridColumn.Visible = false || (isShowMinusMark || isShowAddMark || isShowGengDuoMark || isShowZanTingMark || isShowBaoBiaoMark || isShowBianJiMark || isShowTiHuanMark || isShowQuXiaoTHMark || isShowDOWNMark || isShowUPMark || isShowZZDMark);
                    break;
                }
            }

            if (exist) return this.Columns["ActionMark"];

            return AddActionMarkColumn();
        }
        /// <summary>
        /// 设置减号
        /// </summary>

        private void SetMinusMark(GridColumn gridColumn)
        {
            if (rpiaddpicmark == null)
                return;
            EditorButton editorButton = new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), null, null, null, null, "", "1", null, DevExpress.Utils.ToolTipAnchor.Default);
            SetMarkButton(isShowMinusMark, editorButton);

        }

        private void SetGengDuoMark(GridColumn gridColumn)
        {
            if (rpiaddpicmark == null)
                return;
            EditorButton editorButton = new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), null, null, null, null, "", "2", null, DevExpress.Utils.ToolTipAnchor.Default);
            SetMarkButton(isShowGengDuoMark, editorButton);
        }

        private void SetZanTingMark(GridColumn gridColumn)
        {
            if (rpiaddpicmark == null)
                return;
            EditorButton editorButton = new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), null, null, null, null, "", "3", null, DevExpress.Utils.ToolTipAnchor.Default);
            SetMarkButton(isShowZanTingMark, editorButton);
        }

        private void SetBaoBiaoMark(GridColumn gridColumn)
        {
            if (rpiaddpicmark == null)
                return;
            EditorButton editorButton = new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions5, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), null, null, null, null, "", "4", null, DevExpress.Utils.ToolTipAnchor.Default);
            SetMarkButton(isShowBaoBiaoMark, editorButton);
        }

        private void SetBianJiMark(GridColumn gridColumn)
        {
            if (rpiaddpicmark == null)
                return;
            EditorButton editorButton = new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions6, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), null, null, null, null, "", "5", null, DevExpress.Utils.ToolTipAnchor.Default);
            SetMarkButton(isShowBianJiMark, editorButton);
        }

        private void SetTiHuanMark(GridColumn gridColumn)
        {
            if (rpiaddpicmark == null)
                return;
            EditorButton editorButton = new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions7, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), null, null, null, null, "", "6", null, DevExpress.Utils.ToolTipAnchor.Default);
            SetMarkButton(isShowTiHuanMark, editorButton);
        }

        private void SetQuXiaoTHMark(GridColumn gridColumn)
        {
            if (rpiaddpicmark == null)
                return;
            EditorButton editorButton = new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions8, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), null, null, null, null, "", "7", null, DevExpress.Utils.ToolTipAnchor.Default);
            SetMarkButton(isShowQuXiaoTHMark, editorButton);
        }


        private void SetUPMark(GridColumn gridColumn)
        {
            if (rpiaddpicmark == null)
                return;
            EditorButton editorButton = new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions9, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), null, null, null, null, "", "8", null, DevExpress.Utils.ToolTipAnchor.Default);
            SetMarkButton(isShowUPMark, editorButton);
        }



        private void SetDOWNMark(GridColumn gridColumn)
        {
            if (rpiaddpicmark == null)
                return;
            EditorButton editorButton = new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions10, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), null, null, null, null, "", "9", null, DevExpress.Utils.ToolTipAnchor.Default);
            SetMarkButton(isShowDOWNMark, editorButton);
        }

        private void SetZZDMark(GridColumn gridColumn)
        {
            if (rpiaddpicmark == null)
                return;
            EditorButton editorButton = new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions11, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), null, null, null, null, "", "10", null, DevExpress.Utils.ToolTipAnchor.Default);
            SetMarkButton(isShowZZDMark, editorButton);
        }

        /// <summary>
        /// 设置按钮
        /// </summary>
        /// <param name="showButton">是否显示</param>
        /// <param name="editorButton">按钮</param>
        private void SetMarkButton(bool showButton, EditorButton editorButton)
        {
            if (rpiaddpicmark.Buttons.FirstOrDefault(b => b.Tag.ToString() == editorButton.Tag.ToString()) is EditorButton btn)
                btn.Visible = showButton;
            else if (showButton)
                rpiaddpicmark.Buttons.Add(editorButton);
        }


        /// <summary>
        /// 改变显示的按钮
        /// </summary>
        /// <param name="showAdd">是否显示"+"号按钮</param>
        /// <param name="showMinus">是否显示"-"号按钮</param>
        /// <param name="showBaoBiao">是否显示报表按钮</param>
        /// <param name="showBianJi">是否显示编辑按钮</param>
        /// <param name="showDown">是否显示箭头向下按钮</param>
        /// <param name="showGengDuo">是否显示更多操作按钮</param>
        /// <param name="showQuXiao">是否显示取消替换按钮</param>
        /// <param name="showTiHuan">是否显示替换按钮</param>
        /// <param name="showUp">是否显示箭头向上按钮</param>
        /// <param name="showZanTing">是否显示停止按钮</param>
        /// <param name="showZZD">是否显示子诊断按钮</param>
        public void ChangeVisibleButtons(bool showAdd = false, bool showMinus = false, bool showBaoBiao = false, bool showBianJi = false, bool showDown = false,
            bool showGengDuo = false, bool showQuXiao = false, bool showTiHuan = false, bool showUp = false, bool showZanTing = false, bool showZZD = false)
        {
            if (rpiaddpicmark == null) return;
            IsShowAddMark = showAdd;
            IsShowMinusMark = showMinus;
            IsShowBaoBiaoMark = showBaoBiao;
            IsShowBianJiMark = showBianJi;
            IsShowDOWNMark = showDown;
            IsShowGengDuoMark = showGengDuo;
            IsShowQuXiaoTHMark = showQuXiao;
            IsShowTiHuanMark = showTiHuan;
            IsShowUPMark = showUp;
            IsShowZanTingMark = showZanTing;
            IsShowZZDMark = showZZD;
            rpiaddpicmark = (RepositoryItemMediButtonEdit)rpiaddpicmark.Clone();//通过克隆获得新对象，从而解决按钮显示异常问题
            SetActionColumnWidth();
        }

        DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1;
        DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2;
        DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3;
        DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions4;
        DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions5;
        DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions6;
        DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions7;
        DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions8;
        DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions9;
        DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions10;
        DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions11;
        RepositoryItemMediButtonEdit rpiaddpicmark;
        RepositoryItemMediButtonEdit emptyItemButtonEdit;
        GridColumn colActionMarkColumn;//按钮列
        System.Windows.Forms.Timer timer;

        /// <summary>
        /// 添加操作列
        /// </summary>
        public GridColumn AddActionMarkColumn()
        {

            if (!isShowAddMark && !isShowMinusMark && !isShowGengDuoMark && !isShowZanTingMark && !isShowBaoBiaoMark && !isShowBianJiMark && !isShowTiHuanMark && !isShowQuXiaoTHMark && !isShowUPMark && !isShowDOWNMark && !isShowZZDMark) return null;

            colActionMarkColumn = new GridColumn();
            colActionMarkColumn.FieldName = "ActionMark";
            colActionMarkColumn.Caption = " ";
            colActionMarkColumn.Name = "colActionMark";
            colActionMarkColumn.Visible = true;
            colActionMarkColumn.VisibleIndex = this.Columns.Count;

            colActionMarkColumn.AppearanceHeader.Options.UseTextOptions = true;
            colActionMarkColumn.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            colActionMarkColumn.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
            colActionMarkColumn.AppearanceCell.Options.UseTextOptions = true;
            colActionMarkColumn.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            colActionMarkColumn.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            //列宽
            colActionMarkColumn.Width = actionColumnWidth;
            colActionMarkColumn.MaxWidth = actionColumnWidth;
            colActionMarkColumn.MinWidth = actionColumnWidth;

            //是否可以拖动列标题
            colActionMarkColumn.OptionsColumn.AllowMove = false;
            //固定列头
            colActionMarkColumn.Fixed = FixedStyle.None;
            //排序
            colActionMarkColumn.OptionsColumn.AllowSort = DefaultBoolean.False;
            colActionMarkColumn.OptionsColumn.AllowEdit = true;
            colActionMarkColumn.OptionsColumn.AllowFocus = true;

            colActionMarkColumn.OptionsFilter.AllowFilter = false;
            colActionMarkColumn.OptionsColumn.AllowMerge = DefaultBoolean.False;
            this.Columns.Insert(this.Columns.Count, colActionMarkColumn);
            colActionMarkColumn.BestFit();
            rpiaddpicmark = new RepositoryItemMediButtonEdit();
            rpiaddpicmark.Appearance.Options.UseTextOptions = true;
            rpiaddpicmark.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            rpiaddpicmark.Buttons.RemoveAt(0);
            editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions() { Image = Resources.icon_xinzeng };
            editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions() { Image = Resources.icon_shanchu };
            editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions() { Image = Resources.icon_gengduocaozuo };
            editorButtonImageOptions4 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions() { Image = Resources.icon_tingzhu };
            editorButtonImageOptions5 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions() { Image = Resources.icon_chart };
            editorButtonImageOptions6 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions() { Image = Resources.编辑 };
            editorButtonImageOptions7 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions() { Image = Resources.replace };
            editorButtonImageOptions8 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions() { Image = Resources.quxiaotihuan };
            editorButtonImageOptions9 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions() { Image = Resources.arrow_up };
            editorButtonImageOptions10 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions() { Image = Resources.arrow_down };
            editorButtonImageOptions11 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions() { Image = Resources.zzd };

            rpiaddpicmark.AutoHeight = false;
            rpiaddpicmark.Name = "rpiaddpicmark";
            rpiaddpicmark.ButtonClick -= Rpiaddpicmark_ButtonClick;
            rpiaddpicmark.ButtonClick += Rpiaddpicmark_ButtonClick;
            rpiaddpicmark.Buttons.CollectionChanged -= Buttons_CollectionChanged;
            rpiaddpicmark.Buttons.CollectionChanged += Buttons_CollectionChanged;
            colActionMarkColumn.ColumnEdit = rpiaddpicmark;
            rpiaddpicmark.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            colActionMarkColumn.ShowButtonMode = ShowButtonModeEnum.ShowAlways;

            emptyItemButtonEdit = new RepositoryItemMediButtonEdit();
            emptyItemButtonEdit.Buttons.Clear();
            emptyItemButtonEdit.TextEditStyle = TextEditStyles.HideTextEditor;

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1;
            timer.Tick += Timer_Tick;
            timer.Start();

            return colActionMarkColumn;
        }

        int mouseHoverRowHandle = -1;//鼠标悬浮所在行索引
        int mouseHoverColumnIndex = -1;//鼠标悬浮所在列索引
        bool buttonVisible;//按钮是否可见
        bool inButtonAction;//是否处于按钮操作中

        /// <summary>
        /// 根据鼠标位置决定是否显示按钮(add by 余佳平)
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            #region 鼠标悬浮显示对应行按钮

            if (InButtonAction) return;
            Point pt = PointToClient(Control.MousePosition);
            GridHitInfo hi = CalcHitInfo(pt);
            if (mouseHoverRowHandle != hi.RowHandle)
            {
                //当按钮被点击之后，鼠标移出该行需手动关闭编辑器，否则会出现显示两行按钮的情况
                if (FocusedColumn?.Name == colActionMarkColumn.Name)
                    CloseEditor();

                //手动调用RefreshRowCell方法，从而触发CustomRowCellEdit事件，进行按钮的显示隐藏设置
                RefreshRowCell(mouseHoverRowHandle, colActionMarkColumn);
                RefreshRowCell(hi.RowHandle, colActionMarkColumn);
            }
            mouseHoverRowHandle = hi.RowHandle;
            mouseHoverColumnIndex = hi.Column?.VisibleIndex ?? -1;

            #endregion
        }

        /// <summary>
        /// 动态调整按钮列宽度(add by 余佳平)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Buttons_CollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            if (e.Action == CollectionChangeAction.Refresh) return;
            SetActionColumnWidth();
        }

        /// <summary>
        /// 设置按钮列宽度
        /// </summary>
        private void SetActionColumnWidth()
        {
            if (rpiaddpicmark.Buttons.VisibleCount > 0)
            {
                actionColumnWidth = 24 + (rpiaddpicmark.Buttons.VisibleCount - 1) * 20;
                colActionMarkColumn.Width = actionColumnWidth;
                colActionMarkColumn.MaxWidth = actionColumnWidth;
                colActionMarkColumn.MinWidth = actionColumnWidth;
            }
        }

        private void Rpiaddpicmark_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                inButtonAction = true;//按钮操作开始

                if (e.Button.Tag.ToString().Equals("0"))
                {
                    if (XinZengClick != null)
                    {
                        XinZengClick(sender, e);
                    }
                }
                else if (e.Button.Tag.ToString().Equals("1"))
                {
                    if (ShanChuClick != null)
                    {
                        ShanChuClick(sender, e);
                    }
                }
                else if (e.Button.Tag.ToString().Equals("2"))
                {
                    GengDuoCZClick?.Invoke(sender, e);
                }
                else if (e.Button.Tag.ToString().Equals("3"))
                {
                    ZanTingClick?.Invoke(sender, e);
                }
                else if (e.Button.Tag.ToString().Equals("4"))
                {
                    BaoBiaoClick?.Invoke(sender, e);
                }
                else if (e.Button.Tag.ToString().Equals("5"))
                {
                    BianJiClick?.Invoke(sender, e);
                }
                else if (e.Button.Tag.ToString().Equals("6"))
                {
                    TiHuanClick?.Invoke(sender, e);
                }
                else if (e.Button.Tag.ToString().Equals("7"))
                {
                    QuXiaoTHClick?.Invoke(sender, e);
                }
                else if (e.Button.Tag.ToString().Equals("8"))
                {
                    UpClick?.Invoke(sender, e);
                }
                else if (e.Button.Tag.ToString().Equals("9"))
                {
                    DownClick?.Invoke(sender, e);
                }
                else if (e.Button.Tag.ToString().Equals("10"))
                {
                    ZiZhenDuanClick?.Invoke(sender, e);
                }

            }
            finally
            {
                inButtonAction = false;//按钮操作结束
            }
        }





        #endregion
        #region 选择框

        /// <summary>
        /// 设置复选框
        /// </summary>
        private void SetCheckBox()
        {
            GridColumnCollection gridColumnCollection = this.Columns;
            bool exist = false;
            for (int i = 0; i < gridColumnCollection.Count; i++)
            {
                GridColumn gridColumn = gridColumnCollection[i];

                if (gridColumn.FieldName.ToUpper() == "IS_SELECT")
                {
                    exist = true;

                    if (_IsShowCheckBox)
                        gridColumn.Visible = true;
                    else
                        gridColumn.Visible = false;
                    break;
                }
            }

            if (exist) return;
            AddCheckBox();

        }

        /// <summary>
        /// 添加复选框
        /// </summary>
        private void AddCheckBox()
        {
            if (!_IsShowCheckBox) return;
            GridColumn colCheckBox = new GridColumn();
            colCheckBox.FieldName = "IS_SELECT";//列绑定字段
            colCheckBox.Caption = "选择";//列名称
            colCheckBox.Name = "colIS_SELECT";
            colCheckBox.Visible = true;
            colCheckBox.VisibleIndex = 0;

            colCheckBox.AppearanceHeader.Options.UseTextOptions = true;
            colCheckBox.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            colCheckBox.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
            colCheckBox.AppearanceCell.Options.UseTextOptions = true;
            colCheckBox.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            colCheckBox.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            colCheckBox.MaxWidth = mediCheckBoxFixSize;
            colCheckBox.MinWidth = mediCheckBoxFixSize;
            //列宽
            colCheckBox.Width = 50;
            //是否可以拖动列标题
            colCheckBox.OptionsColumn.AllowMove = false;
            //固定列头
            colCheckBox.Fixed = FixedStyle.None;
            //排序
            colCheckBox.OptionsColumn.AllowSort = DefaultBoolean.False;
            colCheckBox.OptionsColumn.AllowEdit = true;
            colCheckBox.OptionsColumn.AllowFocus = true;
            colCheckBox.UnboundType = UnboundColumnType.Boolean;
            colCheckBox.OptionsFilter.AllowFilter = false;
            colCheckBox.OptionsColumn.AllowMerge = DefaultBoolean.False;
            this.Columns.Insert(mediCheckBoxIndex, colCheckBox);

            RepositoryItemCheckEdit selectcheckbox = new RepositoryItemCheckEdit();
            selectcheckbox.AutoHeight = false;

            selectcheckbox.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Standard;
            selectcheckbox.ReadOnly = false;

            selectcheckbox.Name = "isselectcheckbox";
            colCheckBox.ColumnEdit = selectcheckbox;
            colCheckBox.BestFit();

        }

        #endregion 选择框

        /// <summary>
        /// 非绑定数据源字典
        /// </summary>
        private List<CheckEditValue> _UnboundCheckValues = new List<CheckEditValue>();

        /// <summary>
        /// 单击单元格一次选中复选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediGridView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (!this.Editable || !this.EditableState) return;
            if ((GridControl.Parent is SplitGroupPanel))
            {
                if ((SplitGroupPanel)GridControl.Parent == null ||
                    ((SplitGroupPanel)GridControl.Parent).Owner == null ||
                    (((SplitGroupPanel)GridControl.Parent).Owner is Mediinfo.WinForm.HIS.Controls.MediGridSplitContainer))
                {
                    return;
                }
            }


            GridHitInfo hitInfo = this.CalcHitInfo(e.Location);
            if (hitInfo.InRowCell)
            {
                if (hitInfo.Column.RealColumnEdit is RepositoryItemCheckEdit)
                {
                    this.FocusedColumn = hitInfo.Column;
                    this.FocusedRowHandle = hitInfo.RowHandle;
                    this.ShowEditor();
                    if (this.ActiveEditor is CheckEdit)
                    {
                        CheckEdit edit = this.ActiveEditor as CheckEdit;
                        if (edit == null) return;
                        edit.Toggle();
                    }

                    DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                }
                else if (hitInfo.Column.RealColumnEdit is RepositoryItemGridLookUpEdit)
                {
                    this.FocusedColumn = hitInfo.Column;
                    this.FocusedRowHandle = hitInfo.RowHandle;
                    this.ShowEditor();
                    if (this.ActiveEditor is GridLookUpEdit)
                    {
                        GridLookUpEdit edit = this.ActiveEditor as GridLookUpEdit;
                        if (edit == null || edit.Properties.TextEditStyle != TextEditStyles.DisableTextEditor) return;
                        if (!edit.IsPopupOpen)
                        {
                            edit.ShowPopup();
                        }
                        else
                        {
                            edit.ClosePopup();
                        }
                    }


                    DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                }
                else if (hitInfo.Column.RealColumnEdit is RepositoryItemComboBox)
                {
                    this.FocusedColumn = hitInfo.Column;
                    this.FocusedRowHandle = hitInfo.RowHandle;
                    this.ShowEditor();
                    if (this.ActiveEditor is ComboBoxEdit)
                    {
                        ComboBoxEdit edit = this.ActiveEditor as ComboBoxEdit;
                        if (edit == null || edit.Properties.TextEditStyle != TextEditStyles.DisableTextEditor) return;
                        if (!edit.IsPopupOpen)
                        {
                            edit.ShowPopup();
                        }
                        else
                        {
                            edit.ClosePopup();
                        }
                    }

                    DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                }


            }
        }

        /// <summary>
        /// 列头重绘事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediGridView_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            if (e.Column == null)
                return;
            e.Graphics.FillRectangle(new SolidBrush(System.Drawing.SystemColors.Control), e.Bounds);
            e.Appearance.DrawString(e.Cache, e.Info.Caption, e.Info.CaptionRect);
            //e.Graphics.DrawLine(new Pen(Color.Snow, 1.0f), e.Bounds.Right - 1, e.Bounds.Y, e.Bounds.Right - 1, e.Bounds.Bottom);
            if (e.Column.VisibleIndex != 0)
            {
                e.Graphics.DrawLine(new Pen(Color.Snow, 3.0f), e.Bounds.Right, e.Bounds.Y, e.Bounds.Right, e.Bounds.Bottom);
                e.Graphics.DrawLine(new Pen(Color.Snow, 3.0f), e.Bounds.Left, e.Bounds.Y, e.Bounds.Left, e.Bounds.Bottom);
            }
            e.Handled = true;
        }

        /// <summary>
        /// 非绑定数据源事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediGridView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {

            if (e.Column.Name.Equals("colSerialNo"))
            {
                if (e.IsGetData)
                    e.Value = e.ListSourceRowIndex + 1;
            }

            if (e.Column.FieldName == "IS_SELECT")
            {
                if (_UnboundCheckValues.Count == 0)
                    for (int i = 0; i < this.DataRowCount; i++)
                        _UnboundCheckValues.Add(new CheckEditValue() { Index = i, Value = false });
                var row = _UnboundCheckValues.Where(u => u.Index == e.ListSourceRowIndex).FirstOrDefault();
                if (e.IsGetData)
                {
                    if (row != null)
                    {
                        e.Value = row.Value;
                    }
                }
                if (e.IsSetData)
                {
                    if (row != null)
                    {
                        row.Value = e.Value;
                    }
                    else
                    {
                        _UnboundCheckValues.Add(new CheckEditValue() { Index = e.ListSourceRowIndex, Value = e.Value });
                    }
                }
            }




        }

        /// <summary>
        /// 聚焦行改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediGridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (SkinCat.Instance.IsDesignMode)
                return;
            if (this.IsFocusedView && enableFocuseStyle)
            {
                this.CustomDrawRowIndicator -= new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.MediGridView_CustomDrawRowIndicator);
                this.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
                this.OptionsSelection.EnableAppearanceFocusedCell = true;

                this.OptionsSelection.EnableAppearanceFocusedRow = true;

                this.OptionsSelection.EnableAppearanceHideSelection = true;
            }



        }

        /// <summary>
        /// 新增行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MediGridView_AddingNew(object sender, AddingNewEventArgs e)
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
            MediGridView mediGridView = sender as MediGridView;
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
            if (!enableNewRowHeight)
            {
                if (!customRowHeight)
                {
                    if (RowSpace == 8)
                        e.RowHeight += RowSpace;
                    if (!string.IsNullOrWhiteSpace(HISClientHelper.XITONGID) && !HISClientHelper.LINCHUANGBZ.Equals(0))
                    {
                        e.RowHeight = 30;

                    }
                    else
                    {
                        e.RowHeight = 28;
                    }

                    this.RowHeight = e.RowHeight;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(HISClientHelper.XITONGID) && !HISClientHelper.LINCHUANGBZ.Equals(0))
                    {
                        this.RowHeight = 30;
                    }
                    else
                    {
                        this.RowHeight = 28;
                    }
                }
            }


        }
        /// <summary>
        /// 右键双击弹出菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridViewEx_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs Mouse_e = (MouseEventArgs)e;

            //var result = jCJGBuJuQXService.IsBuJuQX(HISClientHelper.USERID);

            if (Mouse_e.Button == MouseButtons.Right && HISClientHelper.USERID == "DBA")
            {
                MediGridControl grid = this.GridControl as MediGridControl;
                string sFormName = grid.FindForm().Name.ToString();
                string sNameSpace = grid.FindForm().GetType().Namespace;
                GridViewStyleSetFrm frm = new GridViewStyleSetFrm(sFormName, this.Name, sNameSpace, this);

                //设置界面就不在取了
                if (sFormName == "GridViewStyleSetFrm" || sFormName == "FrmButtonSetting" || sFormName.Equals("DataLayoutStyleSetFrm"))
                {
                    return;
                }

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (frm.Tag != null)
                    {
                        if (frm.Tag.ToString().Equals("RESET"))
                        {
                            DataLayoutCustomValue = DataLayoutDefaultValue;
                            GridviewAttributeSet(DataLayoutDefaultValue);
                        }
                        else if (frm.Tag is List<E_GY_DATALAYOUT2>)
                        {
                            //DataLayoutCustomValue = new E_GY_DATALAYOUTDTO();
                            //DataLayoutCustomValue.DataLayout2 = frm.Tag as List<E_GY_DATALAYOUT2>;
                            _EDataLayout2 = frm.Tag as List<E_GY_DATALAYOUT2>;
                            //DataLayoutDefaultValue.DataLayout2 = _EDataLayout2;
                            RunViewColunmProcess(frm._GridView.Columns);
                        }
                        else
                        {
                            E_GY_DATALAYOUTDTO e_GY_DATALAYOUTDTO = frm.Tag as E_GY_DATALAYOUTDTO;
                            DataLayoutCustomValue = e_GY_DATALAYOUTDTO;
                            GridviewAttributeSet(e_GY_DATALAYOUTDTO);
                        }
                        this.RefreshData();
                    }
                }
                frm.Dispose();
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
                    save.Dispose();
                });
                e.Menu.Items.Add(dxMenuItem);

                #endregion 导出数据

                #region 打印数据

                dxMenuItem = new DXMenuItem("打印数据");
                dxMenuItem.Click += new EventHandler(delegate (object sd, EventArgs ce)
                {
                    //(sender as GridView).OptionsPrint.AutoWidth = false;

                    //(sender as GridView).PrintDialog();

                    DevExpress.XtraPrinting.PrintingSystem ps = new DevExpress.XtraPrinting.PrintingSystem();
                    DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink(ps);
                    DevExpress.XtraPrinting.PrinterSettingsUsing psu = new DevExpress.XtraPrinting.PrinterSettingsUsing();

                    psu.UsePaperKind = true;
                    psu.UseLandscape = true;

                    link.Component = (sender as GridView).GridControl;
                    link.Landscape = true;
                    link.PaperKind = System.Drawing.Printing.PaperKind.A4;

                    link.CreateDocument();
                    ps.PageSettings.AssignDefaultPrinterSettings(psu);

                    //ps.ExportOptions.Pdf.ShowPrintDialogOnOpen = false;
                    ps.PageSettings.Landscape = true;

                    // ps.ExportToPdf(Path);
                    link.ShowRibbonPreviewDialog(this.LookAndFeel);
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
            //if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            //{
            //    e.Info.DisplayText = (e.RowHandle + 1).ToString();
            //}
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
                    e.Column.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
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
            if (this.DesignMode)
                return;
            if (this.GridControl is MediGridControl grid)
            {
                Form form = grid.FindForm();
                if (form == null)
                    return;
            }

            if (DataLayoutCustomValue != null)
            {
                if ((ExpressionType)Convert.ToInt32(3) == ExpressionType.BackColorType)
                    if ((ExpressionApplyType)Convert.ToInt32(1) == ExpressionApplyType.RowType)
                        if (DataLayoutCustomValue.DataLayout1 != null &&
                            !string.IsNullOrWhiteSpace(DataLayoutCustomValue.DataLayout1.ROWBACKCOLOREXPRESSION))
                        {
                            string[] unboundExpressions =
                                DataLayoutCustomValue.DataLayout1.ROWBACKCOLOREXPRESSION.Split(new string[] { ";" },
                                    StringSplitOptions.RemoveEmptyEntries);
                            foreach (var t in unboundExpressions)
                            {
                                //在查找component类控件
                                Color? expressionResult =
                                    ExpressionHelper.ColorExpressionFunc(string.Empty, t, this.GridControl, this,
                                        ExpressionApplyType.RowType, ExpressionType.BackColorType, e.RowHandle);
                                if (expressionResult != null && e.RowHandle % 2 != 0)
                                {
                                    this.OptionsView.EnableAppearanceOddRow = true;
                                    this.OptionsView.EnableAppearanceEvenRow = false;
                                    e.Appearance.BackColor = (System.Drawing.Color)expressionResult;
                                }
                                else if (expressionResult != null && e.RowHandle % 2 == 0)
                                {
                                    this.OptionsView.EnableAppearanceOddRow = false;
                                    this.OptionsView.EnableAppearanceEvenRow = true;
                                    e.Appearance.BackColor = (System.Drawing.Color)expressionResult;
                                }
                            }
                        }
            }
        }

        /// <summary>
        /// 设置右键聚焦
        /// </summary>
        [Browsable(true), DefaultValue(false), Description("是否启用右键聚焦")]
        public bool FocusedByRight { get; set; }

        /// <summary>
        /// 是否处于按钮操作中
        /// </summary>
        public bool InButtonAction { get => inButtonAction; private set => inButtonAction = value; }

        /// <summary>
        /// 触发跳转顺序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                if (DataLayoutCustomValue != null && DataLayoutCustomValue.DataLayout2 != null)
                {//gxl   2019.9.18 加上 Where(o=>o.TABINDEX>=0)过滤，当tabindex不为-1的才是真正的跳转顺序
                    List<E_GY_DATALAYOUT2> datalayout2List = DataLayoutCustomValue.DataLayout2.Where(o => o.TABINDEX >= 0).OrderBy(o => o.TABINDEX).ToList();
                    if (datalayout2List == null || this.CurrentCell == null)
                        return;
                    for (int i = 0; i < datalayout2List.Count; i++)
                    {
                        if (string.IsNullOrWhiteSpace(datalayout2List[i].FIELDNAME))
                            continue;
                        if (this.CurrentCell.Column.FieldName.ToUpper().Equals(datalayout2List[i].FIELDNAME.ToUpper()))
                        {
                            if (i == datalayout2List.Count - 1)
                            {
                                foreach (var t in datalayout2List)
                                {
                                    if (string.IsNullOrWhiteSpace(t.FIELDNAME) || this.Columns[t.FIELDNAME] == null)
                                        continue;
                                    if (this.Columns[t.FIELDNAME].Visible && this.Columns[t.FIELDNAME].OptionsColumn.AllowFocus)
                                    {
                                        this.OptionsNavigation.EnterMoveNextColumn = false;
                                        this.FocusedColumn = this.Columns[t.FIELDNAME];
                                        this.ShowEditorByKey(e);
                                        e.Handled = true;
                                        break;
                                    }
                                }

                                break;
                            }

                            for (int k = i + 1; k < datalayout2List.Count; k++)
                            {
                                if (string.IsNullOrWhiteSpace(datalayout2List[i].FIELDNAME) || this.Columns[datalayout2List[k].FIELDNAME] == null)
                                    continue;
                                if (this.Columns[datalayout2List[k].FIELDNAME].Visible && this.Columns[datalayout2List[k].FIELDNAME].OptionsColumn.AllowFocus)
                                {
                                    if (GridControl.Parent != null && GridControl.Parent is SplitGroupPanel)
                                        if ((GridControl.Parent as SplitGroupPanel).Owner != null && (GridControl.Parent as SplitGroupPanel).Owner is MediGridSplitContainer)
                                            continue;

                                    this.OptionsNavigation.EnterMoveNextColumn = false;
                                    this.FocusedColumn = this.Columns[datalayout2List[k].FIELDNAME];
                                    this.ShowEditorByKey(e);
                                    e.Handled = true;
                                    break;
                                }
                            }

                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 列排序(升序A,降序D,不区分大小写,例如 "列名 A")
        /// </summary>
        /// <param name="param"></param>
        public void SetColumnsSort(params string[] param)
        {
            try
            {
                if (param.Length > 0)
                {
                    foreach (var t in param)
                    {
                        if (string.IsNullOrWhiteSpace((t)))
                            continue;
                        string[] filedNamestrs = t.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (filedNamestrs.Length == 1)
                        {
                            if (this.Columns[filedNamestrs[0]] != null)
                            {
                                this.Columns[filedNamestrs[0]].SortOrder = ColumnSortOrder.Ascending;
                            }
                        }
                        else if (filedNamestrs.Length == 2)
                        {
                            this.Columns[filedNamestrs[0]].OptionsColumn.AllowSort = DefaultBoolean.True;
                            if (this.Columns[filedNamestrs[0]] != null && filedNamestrs[1] != null)
                            {
                                if (filedNamestrs[1].ToUpper().Equals("A"))
                                {
                                    this.Columns[filedNamestrs[0]].SortOrder = ColumnSortOrder.Ascending;
                                }
                                else if (filedNamestrs[1].ToUpper().Equals("D"))
                                {
                                    this.Columns[filedNamestrs[0]].SortOrder = ColumnSortOrder.Descending;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
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

        #endregion 事件相关

        private void InitializeComponent()
        {
            GridviewImageCollection = new ImageCollection();
            this.GridviewImageCollection.Images.Add(Resources.icon_xinzeng);
            this.GridviewImageCollection.Images.Add(Resources.icon_shanchu);
        }
    }

    public class MediGridviewScrollInfo : ScrollInfo
    {
        public MediGridviewScrollInfo(BaseView view) : base(view)
        {

        }

        public override int VScrollSize { get { return SystemInformation.VerticalScrollBarWidth - 5; } }

        public override int HScrollSize { get { return SystemInformation.HorizontalScrollBarHeight; } }
        /// <summary>
        /// 创建垂直
        /// </summary>
        /// <returns></returns>
        protected override HCrkScrollBar CreateHScroll()
        {
            return new MediGridviewHCrkScrollBar(this);
        }
        /// <summary>
        /// 创建水平滚动条
        /// </summary>
        /// <returns></returns>
        protected override VCrkScrollBar CreateVScroll()
        {
            return new MediGridviewVCrkScrollBar(this);
        }

        protected override void CalcRects()
        {
            base.CalcRects();

            var prop = typeof(ScrollInfo).GetField("vscrollRect", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var prop1 = typeof(ScrollInfo).GetField("hscrollRect", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            prop.SetValue(this as ScrollInfo, new Rectangle(new Point(VScrollRect.Location.X, VScrollRect.Location.Y), new Size(12, VScrollRect.Height)));

            prop1.SetValue(this as ScrollInfo, new Rectangle(new Point(HScrollRect.Location.X, HScrollRect.Location.Y), new Size(HScrollRect.Width, 12)));
        }
    }

    public class MediGridViewInfo : GridViewInfo
    {
        MediGridviewScrollInfo scrollInfo = null;
        public MediGridViewInfo(GridView gridView) : base(gridView)
        {
            var prop = typeof(GridView).GetField("scrollInfo", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            scrollInfo = prop.GetValue(this.View) as MediGridviewScrollInfo;
        }

        protected override Rectangle CalcClientRect()
        {
            var clientRect = base.CalcClientRect();
            clientRect.Height = clientRect.Height + 5;
            return clientRect;
        }
    }

    public class MediGridviewHCrkScrollBar : HCrkScrollBar
    {
        public MediGridviewHCrkScrollBar(ScrollInfo scrollInfo) : base(scrollInfo)
        {

        }

        protected override ScrollBarViewInfo CreateScrollBarViewInfo()
        {
            return new MediGridviewScrollBarViewInfo(this);
        }
    }

    public class MediGridviewVCrkScrollBar : VCrkScrollBar
    {
        private MediGridView mediGridView;
        public MediGridviewVCrkScrollBar(ScrollInfo scrollInfo) : base(scrollInfo)
        {
            mediGridView = scrollInfo.View as MediGridView;
        }
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (!SkinCat.Instance.IsDesignMode)
            {
                if (specified == BoundsSpecified.None || specified == BoundsSpecified.All) return;
                if (mediGridView != null)
                {
                    if (mediGridView.VerSrollBarLocation == VerticalSrollLocation.Left)
                        base.SetBoundsCore(x - mediGridView.GridControl.Width + 15, y, width, height, specified);
                    else
                        base.SetBoundsCore(x - mediGridView.VScrollBarXValue, y, width, height, specified);
                }
            }
            else
            {
                base.SetBoundsCore(x, y, width, height, specified);
            }

        }
        protected override ScrollBarViewInfo CreateScrollBarViewInfo()
        {
            return new MediGridviewScrollBarViewInfo(this);
        }
    }

    public class MediGridviewScrollBarViewInfo : ScrollBarViewInfo
    {
        public MediGridviewScrollBarViewInfo(IScrollBar scrollBar) : base(scrollBar)
        {

        }
    }
    /// <summary>
    /// 垂直滚动条位置
    /// </summary>
    public enum VerticalSrollLocation
    {
        /// <summary>
        /// 默认靠右
        /// </summary>
        Default = 0,
        /// <summary>
        /// 靠左
        /// </summary>
        Left = 1,
        /// <summary>
        /// 靠右
        /// </summary>
        Right = 2
    }
    /// <summary>
    /// 边框重绘对象
    /// </summary>
    public class BorderXPaint : XPaint
    {
        #region 构造函数

        public BorderXPaint()
        {
            this.BorderWidth = 1;
            this.BorderColor = Color.FromArgb(0, 115, 195);
        }

        #endregion

        #region 属性

        /// <summary>
        /// 边框大小
        /// </summary>
        public int BorderWidth { get; set; }

        /// <summary>
        /// 边框颜色
        /// </summary>
        public Color BorderColor { get; set; }

        #endregion

        #region 重写方法

        /// <summary>
        /// 重绘当前聚焦单元格
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        /// <param name="foreColor"></param>
        /// <param name="backColor"></param>
        public override void DrawFocusRectangle(Graphics g, Rectangle r, Color foreColor, Color backColor)
        {
            if (!CanDraw(r))
                return;

            // 画边框
            using (Brush brush = new SolidBrush(BorderColor))
            {
                g.FillRectangle(brush, new Rectangle(r.X, r.Y, BorderWidth, r.Height - BorderWidth));       // 重绘左边框
                g.FillRectangle(brush, new Rectangle(r.X, r.Y, r.Width - BorderWidth, BorderWidth));        // 重绘上边框
                g.FillRectangle(brush, new Rectangle(r.Right - BorderWidth, r.Y, BorderWidth, r.Height - BorderWidth));     //重绘有边框
                g.FillRectangle(brush, new Rectangle(r.X, r.Bottom - BorderWidth, r.Width, BorderWidth));   // 重绘下边框
            }

            backColor = Color.White;
            // 画背景色
            using (Brush brush = new SolidBrush(backColor))
            {
                g.FillRectangle(brush, Rectangle.Inflate(r, -1, -1));
            }
        }

        #endregion
    }
    public class CheckEditHelper
    {
        protected GridView _GridView;
        protected ArrayList _Selection;
        private GridColumn _CheckColumn;
        private RepositoryItemCheckEdit _CheckEdit;
        private List<CheckEditValue> _UnboundCheckValues;
        public CheckEditHelper()
        {
            _Selection = new ArrayList();
        }

        public CheckEditHelper(GridView view, GridColumn gridColumn, RepositoryItemCheckEdit repositoryItemCheckEdit, List<CheckEditValue> unboundCheckValues)
        : this()
        {
            _CheckColumn = gridColumn;
            _CheckEdit = repositoryItemCheckEdit;
            _UnboundCheckValues = unboundCheckValues;
            this.View = view;

        }

        protected virtual void Attach(GridView view)
        {
            if (view == null)
            {
                return;
            }
            _Selection.Clear();
            this._GridView = view;
            _CheckEdit.EditValueChanged += Edit_EditValueChanged;
            _CheckColumn.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            _CheckColumn.OptionsColumn.ShowCaption = false;
            view.MouseDown += View_MouseDown;
            view.CustomDrawColumnHeader += View_CustomDrawColumnHeader;
            view.CustomDrawGroupRow += View_CustomDrawGroupRow;

        }

        private void View_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo info = View.CalcHitInfo(e.Location);
            if (info.InColumn & object.ReferenceEquals(info.Column, _CheckColumn))
            {
                if (SelectedCount == View.DataRowCount)
                {
                    ClearSelection();
                }
                else
                {
                    SelectAll();
                }
            }

        }
        protected virtual void Detach()
        {
            if (View == null)
            {
                return;
            }

            _CheckColumn?.Dispose();
            if ((_CheckEdit != null))
            {
                View.GridControl.RepositoryItems.Remove(_CheckEdit);
                _CheckEdit.Dispose();
            }

            if (_CheckEdit != null) _CheckEdit.EditValueChanged -= Edit_EditValueChanged;
            _GridView.MouseDown -= View_MouseDown;
            _GridView.CustomDrawColumnHeader -= View_CustomDrawColumnHeader;
            _GridView.CustomDrawGroupRow -= View_CustomDrawGroupRow;



            View = null;
        }
        protected void DrawCheckBox(Graphics g, Rectangle r, bool Checked, bool Grayed)
        {
            CheckEditViewInfo info = default(CheckEditViewInfo);
            CheckEditPainter painter = default(CheckEditPainter);
            ControlGraphicsInfoArgs args = default(ControlGraphicsInfoArgs);
            info = (CheckEditViewInfo)_CheckEdit.CreateViewInfo();
            painter = (CheckEditPainter)_CheckEdit.CreatePainter();
            if (Grayed)
            {
                info.EditValue = _CheckEdit.ValueGrayed;
            }
            else
            {
                info.EditValue = Checked;
            }
            info.Bounds = r;
            info.CalcViewInfo(g);
            args = new ControlGraphicsInfoArgs(info, new DevExpress.Utils.Drawing.GraphicsCache(g), r);
            painter.Draw(args);
            args.Cache.Dispose();
        }



        private void View_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            if (object.ReferenceEquals(e.Column, _CheckColumn))
            {
                e.Info.InnerElements.Clear();
                e.Painter.DrawObject(e.Info);
                bool gray = SelectedCount > 0 & SelectedCount < View.DataRowCount;
                if (SelectedCount == 0 && View.DataRowCount == 0)
                    DrawCheckBox(e.Graphics, e.Bounds, false, gray);
                else
                    DrawCheckBox(e.Graphics, e.Bounds, (SelectedCount == View.DataRowCount), gray);

                e.Handled = true;
            }
        }

        private void View_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo info = default(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo);
            info = (DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo)e.Info;

            info.GroupText = " " + info.GroupText.TrimStart();
            e.Info.Paint.FillRectangle(e.Graphics, e.Appearance.GetBackBrush(e.Cache), e.Bounds);
            e.Painter.DrawObject(e.Info);

            Rectangle r = info.ButtonBounds;
            r.Offset(r.Width * 2, 0);

            int g = GroupRowSelectionStatus(e.RowHandle);
            DrawCheckBox(e.Graphics, r, g > 0, g < 0);
            e.Handled = true;
        }



        public GridView View
        {
            get { return _GridView; }
            set
            {
                if ((!object.ReferenceEquals(_GridView, value)))
                {
                    Detach();
                    Attach(value);
                }
            }
        }

        public GridColumn CheckMarkColumn
        {
            get { return _CheckColumn; }
        }

        public int SelectedCount
        {
            get { return _Selection.Count; }
        }

        public object GetSelectedRow(int index)
        {
            return this._Selection[index];
        }

        public int GetSelectedIndex(object row)
        {
            return _Selection.IndexOf(row);
        }

        public void ClearSelection()
        {
            _UnboundCheckValues.ForEach(u =>
            {
                u.Value = false;
            });
            _Selection.Clear();
            Invalidate();
        }

        private void Invalidate()
        {
            View.BeginUpdate();
            View.EndUpdate();
        }

        public void SelectAll()
        {
            _Selection.Clear();
            _UnboundCheckValues.ForEach(u =>
            {
                u.Value = true;
                _Selection.Add(View.GetRow(u.Index));
            });
            Invalidate();
        }

        public void SelectGroup(int rowHandle, bool @select)
        {
            if (IsGroupRowSelected(rowHandle) & @select)
            {
                return;
            }
            int i = 0;
            for (i = 0; i <= (View.GetChildRowCount(rowHandle)) - 1; i++)
            {
                int childRowHandle = View.GetChildRowHandle(rowHandle, i);
                if (View.IsGroupRow(childRowHandle))
                {
                    SelectGroup(childRowHandle, @select);
                }
                else
                {
                    SelectRow(childRowHandle, @select, false);
                }
            }
            Invalidate();
        }

        public void SelectRow(int rowHandle, bool @select)
        {
            bool isSelectSate = Convert.ToBoolean(View.GetRowCellValue(rowHandle, "IS_SELECT"));
            SelectRow(rowHandle, isSelectSate, true);
        }

        private void SelectRow(int rowHandle, bool @select, bool invalidate)
        {
            object row = View.GetRow(rowHandle);
            if (@select)
            {
                if (!_Selection.Contains(row))
                    _Selection.Add(row);
            }
            else
            {
                if (_Selection.Contains(row))
                    _Selection.Remove(row);
            }
            if (invalidate)
            {
                this.Invalidate();
            }
        }

        public int GroupRowSelectionStatus(int rowHandle)
        {
            int count = 0;
            int i = 0;
            for (i = 0; i <= (View.GetChildRowCount(rowHandle)) - 1; i++)
            {
                int row = View.GetChildRowHandle(rowHandle, i);
                if (View.IsGroupRow(row))
                {
                    int g = GroupRowSelectionStatus(row);
                    if (g < 0)
                    {
                        return g;
                    }
                    if (g > 0)
                    {
                        count += 1;
                    }
                }
                else
                {
                    if (IsRowSelected(row))
                    {
                        count += 1;
                    }
                }
            }
            if (count == 0)
            {
                return 0;
            }
            if (count == View.GetChildRowCount(rowHandle))
            {
                return 1;
            }
            return -1;
        }

        public bool IsGroupRowSelected(int rowHandle)
        {
            int i = 0;
            for (i = 0; i <= (View.GetChildRowCount(rowHandle)) - 1; i++)
            {
                int row = View.GetChildRowHandle(rowHandle, i);
                if (View.IsGroupRow(row))
                {
                    if (!IsGroupRowSelected(row))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!IsRowSelected(row))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool IsRowSelected(int rowHandle)
        {
            if (View.IsGroupRow(rowHandle))
            {
                return IsGroupRowSelected(rowHandle);
            }

            object row = View.GetRow(rowHandle);
            return GetSelectedIndex(row) != -1;
        }
        private void Edit_EditValueChanged(object sender, EventArgs e)
        {
            View.PostEditor();
            SelectRow(this.View.FocusedRowHandle, IsRowSelected(this.View.FocusedRowHandle));
        }
    }
    /// <summary>
    /// 复选框选中值
    /// </summary>
    public class CheckEditValue
    {
        /// <summary>
        /// 索引
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }

    }
}