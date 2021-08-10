using DevExpress.Data.ExpressionEditor;
using DevExpress.Data.Filtering;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.Utils.Controls;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.Native;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 表达式窗体(主要用于网格弹出窗体)
    /// </summary>
    public partial class CellExpressionShowFrm : MediForm
    {
        /// <summary>
        /// 目标自定义布局窗体
        /// </summary>
        public Control DataLayoutFrm { get; set; }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        /// <param name="dataLayoutFrm">包含网格的目标窗体</param>
        public CellExpressionShowFrm(Control dataLayoutFrm)
        {
            InitializeComponent();
            this.DataLayoutFrm = dataLayoutFrm;
            this.listOfInputTypes.SelectedValueChanged += new EventHandler(this.listOfInputTypes_SelectedValueChanged);
            this.plusItemButton.Click += new EventHandler(this.plusItemButton_Click);
            this.layoutItemButton2.Click += new EventHandler(this.layoutItemButton2_Click);
            this.layoutItemButton3.Click += new EventHandler(this.layoutItemButton3_Click);
            this.layoutItemButton4.Click += new EventHandler(this.layoutItemButton4_Click);

            this.layoutItemButton5.Click += new EventHandler(this.layoutItemButton5_Click);
            this.layoutItemButton6.Click += new EventHandler(this.layoutItemButton6_Click);
            this.layoutItemButton7.Click += new EventHandler(this.layoutItemButton7_Click);
            this.layoutItemButton8.Click += new EventHandler(this.layoutItemButton8_Click);
            this.layoutItemButton9.Click += new EventHandler(this.layoutItemButton9_Click);
            this.layoutItemButton10.Click += new EventHandler(this.layoutItemButton10_Click);
            this.layoutItemButton11.Click += new EventHandler(this.layoutItemButton11_Click);
            this.layoutItemButton12.Click += new EventHandler(this.layoutItemButton12_Click);
            this.layoutItemButton13.Click += new EventHandler(this.layoutItemButton13_Click);
            this.layoutItemButton14.Click += new EventHandler(this.layoutItemButton14_Click);
            this.layoutItemButton15.Click += new EventHandler(this.layoutItemButton15_Click);
            this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
            this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
            this.listOfInputParameters.SelectedValueChanged += new EventHandler(this.listOfInputParameters_SelectedValueChanged);
            this.functionsTypes.EditValueChanged += new EventHandler(this.functionsTypes_EditValueChanged);
            this.listOfInputParameters.DoubleClick += new EventHandler(this.listOfInputParameters_DoubleClick);
        }

        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="dataLayoutFrm">包含网格的目标窗体</param>
        /// <param name="value">单元格原始值</param>
        public CellExpressionShowFrm(Control dataLayoutFrm, string value)
        {
            InitializeComponent();
            this.DataLayoutFrm = dataLayoutFrm;
            this.listOfInputTypes.SelectedValueChanged += new EventHandler(this.listOfInputTypes_SelectedValueChanged);
            this.plusItemButton.Click += new EventHandler(this.plusItemButton_Click);
            this.layoutItemButton2.Click += new EventHandler(this.layoutItemButton2_Click);
            this.layoutItemButton3.Click += new EventHandler(this.layoutItemButton3_Click);
            this.layoutItemButton4.Click += new EventHandler(this.layoutItemButton4_Click);

            this.layoutItemButton5.Click += new EventHandler(this.layoutItemButton5_Click);
            this.layoutItemButton6.Click += new EventHandler(this.layoutItemButton6_Click);
            this.layoutItemButton7.Click += new EventHandler(this.layoutItemButton7_Click);
            this.layoutItemButton8.Click += new EventHandler(this.layoutItemButton8_Click);
            this.layoutItemButton9.Click += new EventHandler(this.layoutItemButton9_Click);
            this.layoutItemButton10.Click += new EventHandler(this.layoutItemButton10_Click);
            this.layoutItemButton11.Click += new EventHandler(this.layoutItemButton11_Click);
            this.layoutItemButton12.Click += new EventHandler(this.layoutItemButton12_Click);
            this.layoutItemButton13.Click += new EventHandler(this.layoutItemButton13_Click);
            this.layoutItemButton14.Click += new EventHandler(this.layoutItemButton14_Click);
            this.layoutItemButton15.Click += new EventHandler(this.layoutItemButton15_Click);
            this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
            this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
            this.listOfInputParameters.SelectedValueChanged += new EventHandler(this.listOfInputParameters_SelectedValueChanged);
            this.functionsTypes.EditValueChanged += new EventHandler(this.functionsTypes_EditValueChanged);
            this.listOfInputParameters.DoubleClick += new EventHandler(this.listOfInputParameters_DoubleClick);
            expressionMemoEdit.Text = value;
        }

        private ImageCollection icons;

        /// <summary>
        /// 操作数运算符
        /// </summary>
        private Dictionary<string, string> operatorsitemsTable;

        /// <summary>
        /// 逻辑表达式
        /// </summary>
        private Dictionary<string, string> constantsitemTable;

        /// <summary>
        /// 函数表达式
        /// </summary>
        private Dictionary<string, string> functionitemTable;

        /// <summary>
        /// 列名称表达式
        /// </summary>
        private Dictionary<string, string> columnitemTable;

        private Hashtable buttonStrings;

        /// <summary>
        /// 表达式属性
        /// </summary>
        public string UnboundExpression { get; set; }

        /// <summary>
        /// 控件图标更新方法
        /// </summary>
        protected virtual void UpdateIcons()
        {
            Image iconsImage = ResourceImageHelper.CreateImageFromResources("DevExpress.XtraEditors.Images.FormulaWizardIcons.png", typeof(BaseEdit).Assembly);
            iconsImage = ImageColorizer.GetColoredImage(iconsImage, CommonSkins.GetSkin(this.LookAndFeel)[CommonSkins.SkinForm].Color.GetForeColor());
            icons = ImageHelper.CreateImageCollectionCore(iconsImage, new Size(24, 24), Color.Empty);
        }

        /// <summary>
        /// 初始化运算符按钮
        /// </summary>
        private void InitializeButtons()
        {
            buttonStrings = new Hashtable();
            buttonStrings[plusItemButton] = StandardOperations.Plus;
            buttonStrings[layoutItemButton2] = StandardOperations.Minus;
            buttonStrings[layoutItemButton3] = StandardOperations.Multiply;
            buttonStrings[layoutItemButton4] = StandardOperations.Divide;
            buttonStrings[layoutItemButton5] = StandardOperations.Modulo;
            buttonStrings[layoutItemButton6] = StandardOperations.Equal;
            buttonStrings[layoutItemButton7] = StandardOperations.NotEqual;
            buttonStrings[layoutItemButton8] = StandardOperations.Less;
            buttonStrings[layoutItemButton9] = StandardOperations.LessOrEqual;
            buttonStrings[layoutItemButton10] = StandardOperations.LargerOrEqual;
            buttonStrings[layoutItemButton11] = StandardOperations.Larger;
            buttonStrings[layoutItemButton12] = StandardOperations.And;
            buttonStrings[layoutItemButton13] = StandardOperations.Or;
            buttonStrings[layoutItemButton14] = StandardOperations.Not;
            foreach (Control control in this.Controls)
            {
                if (control is LayoutItemButton && control.Name == "plusItemButton")
                    this.plusItemButton.Text = buttonStrings[plusItemButton].ToString();
                if (control is LayoutItemButton && control.Name == "layoutItemButton2")
                    this.layoutItemButton2.Text = buttonStrings[layoutItemButton2].ToString();
                if (control is LayoutItemButton && control.Name == "layoutItemButton3")
                    this.layoutItemButton3.Text = buttonStrings[layoutItemButton3].ToString();
                if (control is LayoutItemButton && control.Name == "layoutItemButton4")
                    this.layoutItemButton4.Text = buttonStrings[layoutItemButton4].ToString();
                if (control is LayoutItemButton && control.Name == "layoutItemButton5")
                    this.layoutItemButton5.Text = buttonStrings[layoutItemButton5].ToString();
                if (control is LayoutItemButton && control.Name == "layoutItemButton6")
                    this.layoutItemButton6.Text = buttonStrings[layoutItemButton6].ToString();
                if (control is LayoutItemButton && control.Name == "layoutItemButton7")
                    this.layoutItemButton7.Text = buttonStrings[layoutItemButton7].ToString();
                if (control is LayoutItemButton && control.Name == "layoutItemButton8")
                    this.layoutItemButton8.Text = buttonStrings[layoutItemButton8].ToString();
                if (control is LayoutItemButton && control.Name == "layoutItemButton9")
                    this.layoutItemButton9.Text = buttonStrings[layoutItemButton9].ToString();
                if (control is LayoutItemButton && control.Name == "layoutItemButton10")
                    this.layoutItemButton10.Text = buttonStrings[layoutItemButton10].ToString();
                if (control is LayoutItemButton && control.Name == "layoutItemButton11")
                    this.layoutItemButton11.Text = buttonStrings[layoutItemButton11].ToString();
                if (control is LayoutItemButton && control.Name == "layoutItemButton12")
                    this.layoutItemButton12.Text = buttonStrings[layoutItemButton12].ToString();
                if (control is LayoutItemButton && control.Name == "layoutItemButton13")
                    this.layoutItemButton13.Text = buttonStrings[layoutItemButton13].ToString();
                if (control is LayoutItemButton && control.Name == "layoutItemButton14")
                    this.layoutItemButton14.Text = buttonStrings[layoutItemButton14].ToString();
            }
        }

        private Dictionary<string, string> unBoundExpressionDic = new Dictionary<string, string>();

        /// <summary>
        /// 初始化操作数
        /// </summary>
        private void InitializeOperators()
        {
            operatorsitemsTable = new Dictionary<string, string>();
            operatorsitemsTable.Add(" + ", ExpressionResource.PlusDescription);
            operatorsitemsTable.Add(" - ", ExpressionResource.MinusDescription);
            operatorsitemsTable.Add(" * ", ExpressionResource.MultiplyDescription);
            operatorsitemsTable.Add(" / ", ExpressionResource.DivideDescription);
            operatorsitemsTable.Add(" % ", ExpressionResource.ModuloDescription);
            operatorsitemsTable.Add(" | ", ExpressionResource.BitwiseOrDescription);
            operatorsitemsTable.Add(" & ", ExpressionResource.BitwiseAndDescription);
            operatorsitemsTable.Add(" ^ ", ExpressionResource.BitwiseXorDescription);
            operatorsitemsTable.Add(" == ", ExpressionResource.EqualDescription);
            operatorsitemsTable.Add(" != ", ExpressionResource.NotEqualDescription);
            operatorsitemsTable.Add(" < ", ExpressionResource.LessDescription);
            operatorsitemsTable.Add(" <= ", ExpressionResource.LessOrEqualDescription);
            operatorsitemsTable.Add(" >= ", ExpressionResource.GreaterOrEqualDescription);
            operatorsitemsTable.Add(" > ", ExpressionResource.GreaterDescription);
            operatorsitemsTable.Add(" In ", ExpressionResource.InDescription);
            operatorsitemsTable.Add(" Like ", ExpressionResource.LikeDescription);
            operatorsitemsTable.Add(" Between ", ExpressionResource.BetweenDescription);
            operatorsitemsTable.Add(" And ", ExpressionResource.AndDescription);
            operatorsitemsTable.Add(" Or ", ExpressionResource.OrDescription);
            operatorsitemsTable.Add(" Not ", ExpressionResource.NotDescription);
        }

        /// <summary>
        /// 类型切换
        /// </summary>
        /// <param name="sender">输入参数控件触发对象</param>
        /// <param name="e">输入参数控件参数</param>
        private void listOfInputTypes_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listOfInputTypes != null)
            {
                string selectedValue = this.listOfInputTypes.SelectedValue.ToString();

                switch (selectedValue)
                {
                    case "函数":
                        functionsTypes.Enabled = true;
                        this.functionsTypes.SelectedIndex = 0;
                        AllFunctions();
                        this.listOfInputParameters.SelectedIndex = 0;
                        break;

                    case "运算操作符":
                        functionsTypes.Enabled = false;
                        functionsTypes.Text = "运算操作符";
                        this.listOfInputParameters.Items.Clear();
                        foreach (var item in operatorsitemsTable.Keys)
                            this.listOfInputParameters.Items.Add(item);

                        this.listOfInputParameters.SelectedIndex = 0;
                        break;

                    case "常量":
                        functionsTypes.Enabled = false;
                        functionsTypes.Text = "常量";
                        this.listOfInputParameters.Items.Clear();
                        this.listOfInputParameters.Items.AddRange(new object[] { "False", "True", "?" });
                        this.listOfInputParameters.SelectedIndex = 0;
                        break;

                    case "列名称":
                        functionsTypes.Enabled = false;
                        functionsTypes.Text = "列名称";
                        Control[] controls = DataLayoutFrm.Controls.Find("columnAttributeGridControl", true);
                        GridView gridview = (GridView)((DevExpress.XtraGrid.GridControl)controls[0]).ViewCollection[0];
                        this.listOfInputParameters.Items.Clear();

                        for (int i = 0; i < gridview.DataRowCount; i++)
                        {
                            object filedName = gridview.GetRowCellValue(i, "FIELDNAME");
                            this.listOfInputParameters.Items.Add(filedName);
                        }

                        this.listOfInputParameters.SelectedIndex = 0;
                        break;

                    case "参数":
                        functionsTypes.Enabled = false;
                        this.listOfInputParameters.Items.Clear();
                        foreach (var item in unBoundExpressionDic.Keys)
                            this.listOfInputParameters.Items.Add(item);

                        this.listOfInputParameters.SelectedIndex = 0;
                        break;

                    case "自定义":
                        functionsTypes.Enabled = false;
                        this.listOfInputParameters.Items.Clear();
                        this.listOfInputParameters.Items.AddRange(new object[] { "Null(,,)", "Valid(,,)", "Enable(,,)", "Font(,,,)", "BCRGB(,,)", "RGB(,,)" });
                        this.listOfInputParameters.SelectedIndex = 0;
                        break;
                }
            }
        }

        /// <summary>
        /// 切换输入参数事件
        /// </summary>
        /// <param name="sender">事件触发对象</param>
        /// <param name="e">事件参数</param>
        private void listOfInputParameters_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listOfInputParameters == null || this.listOfInputParameters.SelectedItem == null)
                return;
            string selectValue = this.listOfInputParameters.SelectedItem.ToString();
            foreach (var item in operatorsitemsTable.Keys)
            {
                if (item != selectValue)
                    continue;
                this.descriptionControl.Text = operatorsitemsTable[item];
            }

            foreach (var item in constantsitemTable.Keys)
            {
                if (item != selectValue)
                    continue;
                this.descriptionControl.Text = constantsitemTable[item];
            }

            foreach (var item in functionitemTable.Keys)
            {
                if (item != selectValue)
                    continue;

                this.descriptionControl.Text = functionitemTable[item];
            }

            foreach (var item in columnitemTable.Keys)
            {
                if (item != selectValue)
                    continue;

                this.descriptionControl.Text = columnitemTable[item];
            }
        }

        /// <summary>
        /// 加法
        /// </summary>
        /// <param name="sender">事件触发对象</param>
        /// <param name="e">事件参数</param>
        private void plusItemButton_Click(object sender, EventArgs e)
        {
            this.expressionMemoEdit.Focus();

            this.expressionMemoEdit.SelectedText = this.expressionMemoEdit.SelectedText + "+ ";

            int selectIndex = this.expressionMemoEdit.SelectionStart;
            this.expressionMemoEdit.Select(selectIndex, 0);
            //this.expressionMemoEdit.Select(, 0);
        }
        /// <summary>
        /// 所有函数
        /// </summary>
        private void AllFunctions()
        {
            this.listOfInputParameters.Items.Clear();
            this.listOfInputParameters.Items.AddRange(new object[] { "AddDays(,)", "AddHours(,)", "AddMilliSeconds(,)", "AddMinutes(,)", "AddMonths(,)", "AddSeconds(,)", "AddTicks(,)", "AddTimeSpan(,)", "AddYears(,)", "DateDiffDay(,)", "DateDiffHour(,)", "DateDiffMilliSecond(,)", "DateDiffMinute(,)", "DateDiffMonth(,)", "DateDiffSecond(,)", "DateDiffTick(,)", "DateDiffYear(,)", "GetDate()", "GetDay()", "GetDayOfWeek()", "GetDayOfYear()", "GetHour()", "GetMilliSecond()", "GetMinute()", " GetMonth()", "GetSecond()", "GetTimeOfDay()", "GetYear()", "IsThisMonth()", "IsThisWeek()", "IsThisYaer()", "LocalDateTimeDayAfterTomorrow()", "LocalDateTimeLastWeek()", "LocalDateTimeNextMonth()", "LocalDateTimeNextWeek()", "LocalDateTimeNextYear()", "LocalDateTimeNow()", "LocalDateTimeThisMonth()", "LocalDateTimeThisWeek()", "LocalDateTimeThisYear()", "LocalDateTimeToday()", "LocalDateTimeTomorrow()", "LocalDateTimeTwoWeeksAway()", "LocalDateTimeYesterday()", "Now()", "Today()", "UtcNow()", "Iif(,,)", "IsNull()", "IsNullOrEmpty()", "Abs()", "Acos()", "Asin()", "Atn()", "Atn2(,)", "BigMul(,)", "Ceiling()", "Cos()", "Cosh()", "Exp()", "Floor()", "Log()", "Log(,)", "Log10()", "Max(,)", "Power(,)", "Rnd()", "Round()", "Round(,)", "Sign()", "Sin()", "Sinh()", "Sqr()", "Tan()", "Tanh()", "ToDecimal()", "ToDouble()", "ToFloat()", "ToInt()", "ToLong()", "Ascii('')", "Char()", "CharIndex('','')", "CharIndex('','',)", "Concat(,)", "Contains('','')", "EndsWith('','')", "Insert('','')", "Len()", "Lower()", "PadLeft(,)", "PadLeft(,,'')", "PadRight(,)", "PadRight(,,'')", "Remove('',)", "Remove('',,)", "Replace('','','')", "Reverse('')", "StartsWith('','')", "Substring('',,)", "Substring('',)", "ToStr()", "Trim()", "Upper()", "Avg()", "Count()", "Exists()", "Max()", "Min()", "Single()", "Sum()", "Null(,,)", "Valid(,,)", "Enable(,,)", "Font(,,,)", "BCRGB(,,)", "RGB(,,)" });
        }

        /// <summary>
        /// 函数类型改变时触发的事件
        /// </summary>
        /// <param name="sender">事件触发对象</param>
        /// <param name="e">事件参数</param>
        private void functionsTypes_EditValueChanged(object sender, EventArgs e)
        {
            string FunctionType = this.functionsTypes.EditValue.ToString();
            switch (FunctionType)
            {
                case "(所有)":
                    AllFunctions();
                    this.listOfInputParameters.SelectedIndex = 0;
                    break;
                case "日期类型":
                    this.listOfInputParameters.Items.Clear();
                    this.listOfInputParameters.Items.AddRange(new object[] { "AddDays(,)", "AddHours(,)", "AddMilliSeconds(,)", "AddMinutes(,)", "AddMonths(,)", "AddSeconds(,)", "AddTicks(,)", "AddTimeSpan(,)", "AddYears(,)", "DateDiffDay(,)", "DateDiffHour(,)", "DateDiffMilliSecond(,)", "DateDiffMinute(,)", "DateDiffMonth(,)", "DateDiffSecond(,)", "DateDiffTick(,)", "DateDiffYear(,)", "GetDate()", "GetDay()", "GetDayOfWeek()", "GetDayOfYear()", "GetHour()", "GetMilliSecond()", "GetMinute()", "GetMonth()", "GetSecond()", "GetTimeOfDay()", "GetYear()", "IsThisMonth()", "IsThisWeek()", "IsThisYaer()", "LocalDateTimeDayAfterTomorrow()", "LocalDateTimeLastWeek()", "LocalDateTimeNextMonth()", "LocalDateTimeNextWeek()", "LocalDateTimeNextYear()", "LocalDateTimeNow()", "LocalDateTimeThisMonth()", "LocalDateTimeThisWeek()", "LocalDateTimeThisYear()", "LocalDateTimeToday()", "LocalDateTimeTomorrow()", "LocalDateTimeTwoWeeksAway()", "LocalDateTimeYesterday()", "Now()", "Today()", "UtcNow()" });
                    this.listOfInputParameters.SelectedIndex = 0;
                    break;
                case "逻辑类型":
                    this.listOfInputParameters.Items.Clear();
                    this.listOfInputParameters.Items.AddRange(new object[] { "Iif(,,)", "IsNull()", "IsNullOrEmpty()" });
                    this.listOfInputParameters.SelectedIndex = 0;
                    break;
                case "数学类型":
                    this.listOfInputParameters.Items.Clear();
                    this.listOfInputParameters.Items.AddRange(new object[] { "Abs()", "Acos()", "Asin()", "Atn()", "Atn2(,)", "BigMul(,)", "Ceiling()", "Cos()", "Cosh()", "Exp()", "Floor()", "Log()", "Log(,)", "Log10()", "Max(,)", "Power(,)", "Rnd()", "Round()", "Round(,)", "Sign()", "Sin()", "Sinh()", "Sqr()", "Tan()", "Tanh()", "ToDecimal()", "ToDouble()", "ToFloat()", "ToInt()", "ToLong()" });
                    this.listOfInputParameters.SelectedIndex = 0;
                    break;
                case "字符串类型":
                    this.listOfInputParameters.Items.Clear();
                    this.listOfInputParameters.Items.AddRange(new object[] { "Ascii('')", "Char()", "CharIndex('','')", "CharIndex('','',)", "Concat(,)", "Contains('','')", "EndsWith('','')", "Insert('','')", "Len()", "Lower()", "PadLeft(,)", "PadLeft(,,'')", "PadRight(,)", "PadRight(,,'')", "Remove('',)", "Remove('',,)", "Replace('','','')", "Reverse('')", "StartsWith('','')", "Substring('',,)", "Substring('',)", "ToStr()", "Trim()", "Upper()" });
                    this.listOfInputParameters.SelectedIndex = 0;
                    break;
                case "聚合函数类型":
                    this.listOfInputParameters.Items.Clear();
                    this.listOfInputParameters.Items.AddRange(new object[] { "Avg()", "Count()", "Exists()", "Max()", "Min()", "Single()", "Sum()" });
                    this.listOfInputParameters.SelectedIndex = 0;
                    break;
                case "自定义类型":
                    this.listOfInputParameters.Items.Clear();
                    this.listOfInputParameters.Items.AddRange(new object[] { "Null(,,)", "Valid(,,)", "Enable(,,)", "Font(,,,)", "BCRGB(,,)", "RGB(,,)" });
                    this.listOfInputParameters.SelectedIndex = 0;
                    break;
            }
        }

        /// <summary>
        /// 减法
        /// </summary>
        /// <param name="sender">事件触发对象</param>
        /// <param name="e">事件参数</param>
        private void layoutItemButton2_Click(object sender, EventArgs e)
        {
            this.expressionMemoEdit.Focus();

            this.expressionMemoEdit.SelectedText = this.expressionMemoEdit.SelectedText + "- ";

            int selectIndex = this.expressionMemoEdit.SelectionStart;
            this.expressionMemoEdit.Select(selectIndex, 0);
        }

        /// <summary>
        /// 乘法
        /// </summary>
        /// <param name="sender">事件触发对象</param>
        /// <param name="e">事件参数</param>
        private void layoutItemButton3_Click(object sender, EventArgs e)
        {
            this.expressionMemoEdit.Focus();

            this.expressionMemoEdit.SelectedText = this.expressionMemoEdit.SelectedText + "* ";

            int selectIndex = this.expressionMemoEdit.SelectionStart;
            this.expressionMemoEdit.Select(selectIndex, 0);
        }

        /// <summary>
        /// 除法
        /// </summary>
        /// <param name="sender">事件触发对象</param>
        /// <param name="e">事件参数</param>
        private void layoutItemButton4_Click(object sender, EventArgs e)
        {
            this.expressionMemoEdit.Focus();

            this.expressionMemoEdit.SelectedText = this.expressionMemoEdit.SelectedText + "/ ";

            int selectIndex = this.expressionMemoEdit.SelectionStart;
            this.expressionMemoEdit.Select(selectIndex, 0);
        }

        /// <summary>
        /// 取余
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layoutItemButton5_Click(object sender, EventArgs e)
        {
            this.expressionMemoEdit.Focus();

            this.expressionMemoEdit.SelectedText = this.expressionMemoEdit.SelectedText + "% ";

            int selectIndex = this.expressionMemoEdit.SelectionStart;
            this.expressionMemoEdit.Select(selectIndex, 0);
        }

        /// <summary>
        /// 小括号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layoutItemButton15_Click(object sender, EventArgs e)
        {
            this.expressionMemoEdit.Focus();

            this.expressionMemoEdit.SelectedText = this.expressionMemoEdit.SelectedText + "()";

            int selectIndex = this.expressionMemoEdit.SelectionStart;
            this.expressionMemoEdit.Select(selectIndex - 1, 0);
        }

        /// <summary>
        /// 等于号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layoutItemButton6_Click(object sender, EventArgs e)
        {
            this.expressionMemoEdit.Focus();

            this.expressionMemoEdit.SelectedText = this.expressionMemoEdit.SelectedText + "= ";

            int selectIndex = this.expressionMemoEdit.SelectionStart;
            this.expressionMemoEdit.Select(selectIndex, 0);
        }

        /// <summary>
        /// 不等于号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layoutItemButton7_Click(object sender, EventArgs e)
        {
            this.expressionMemoEdit.Focus();

            this.expressionMemoEdit.SelectedText = this.expressionMemoEdit.SelectedText + "!= ";

            int selectIndex = this.expressionMemoEdit.SelectionStart;
            this.expressionMemoEdit.Select(selectIndex, 0);
        }

        /// <summary>
        /// 小于号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layoutItemButton8_Click(object sender, EventArgs e)
        {
            this.expressionMemoEdit.Focus();

            this.expressionMemoEdit.SelectedText = this.expressionMemoEdit.SelectedText + "< ";

            int selectIndex = this.expressionMemoEdit.SelectionStart;
            this.expressionMemoEdit.Select(selectIndex, 0);
        }

        /// <summary>
        /// 小于等于
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layoutItemButton9_Click(object sender, EventArgs e)
        {
            this.expressionMemoEdit.Focus();

            this.expressionMemoEdit.SelectedText = this.expressionMemoEdit.SelectedText + "<= ";

            int selectIndex = this.expressionMemoEdit.SelectionStart;
            this.expressionMemoEdit.Select(selectIndex, 0);
        }

        /// <summary>
        /// 大于等于
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layoutItemButton10_Click(object sender, EventArgs e)
        {
            this.expressionMemoEdit.Focus();

            this.expressionMemoEdit.SelectedText = this.expressionMemoEdit.SelectedText + ">= ";

            int selectIndex = this.expressionMemoEdit.SelectionStart;
            this.expressionMemoEdit.Select(selectIndex, 0);
        }

        /// <summary>
        /// 大于号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layoutItemButton11_Click(object sender, EventArgs e)
        {
            this.expressionMemoEdit.Focus();

            this.expressionMemoEdit.SelectedText = this.expressionMemoEdit.SelectedText + "> ";

            int selectIndex = this.expressionMemoEdit.SelectionStart;
            this.expressionMemoEdit.Select(selectIndex, 0);
        }

        /// <summary>
        /// and
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layoutItemButton12_Click(object sender, EventArgs e)
        {
            this.expressionMemoEdit.Focus();

            this.expressionMemoEdit.SelectedText = this.expressionMemoEdit.SelectedText + "And ";

            int selectIndex = this.expressionMemoEdit.SelectionStart;
            this.expressionMemoEdit.Select(selectIndex, 0);
        }

        /// <summary>
        /// or
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layoutItemButton13_Click(object sender, EventArgs e)
        {
            this.expressionMemoEdit.Focus();

            this.expressionMemoEdit.SelectedText = this.expressionMemoEdit.SelectedText + "Or ";

            int selectIndex = this.expressionMemoEdit.SelectionStart;
            this.expressionMemoEdit.Select(selectIndex, 0);
        }

        /// <summary>
        /// not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layoutItemButton14_Click(object sender, EventArgs e)
        {
            this.expressionMemoEdit.Focus();

            this.expressionMemoEdit.SelectedText = this.expressionMemoEdit.SelectedText + "Not ";

            int selectIndex = this.expressionMemoEdit.SelectionStart;
            this.expressionMemoEdit.Select(selectIndex, 0);
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.UnboundExpression = this.expressionMemoEdit.Text;
            string[] multiExpressionString = UnboundExpression.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in multiExpressionString)
            {
                try
                {
                    OperandValue[] criteriaParametersList = null;
                    CriteriaOperator criteriaOperator = CriteriaParser.Parse(item, out criteriaParametersList);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            this.Tag = UnboundExpression;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
            this.Dispose();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 双击在文本中输入函数表达式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listOfInputParameters_DoubleClick(object sender, EventArgs e)
        {
            this.expressionMemoEdit.Focus();
            if (this.listOfInputTypes.SelectedItem != null)
            {
                if (this.listOfInputTypes.SelectedItem.ToString() == "参数")
                {
                    this.expressionMemoEdit.SelectedText = this.expressionMemoEdit.SelectedText + "[" + this.listOfInputParameters.SelectedItem + "]";
                }
                else if (this.listOfInputTypes.SelectedItem.ToString() == "列名称")
                {
                    this.expressionMemoEdit.SelectedText = this.expressionMemoEdit.SelectedText + "[" + this.listOfInputParameters.SelectedItem + "]";
                }
                else
                    this.expressionMemoEdit.SelectedText = this.expressionMemoEdit.SelectedText + this.listOfInputParameters.SelectedItem;
            }
            else
            {
                this.expressionMemoEdit.SelectedText = this.expressionMemoEdit.SelectedText + this.listOfInputParameters.SelectedItem;
            }

            if (this.listOfInputParameters.SelectedItem.ToString().Contains("'"))
            {
                if (this.listOfInputParameters.SelectedItem.ToString().Substring(this.listOfInputParameters.SelectedItem.ToString().IndexOf('(') + 1).IndexOf("'") > 0)
                {
                    int selectIndex = this.expressionMemoEdit.SelectionStart;
                    this.expressionMemoEdit.Select(selectIndex - this.listOfInputParameters.SelectedItem.ToString().IndexOf(')') + this.listOfInputParameters.SelectedItem.ToString().IndexOf('('), 0);
                }
                else
                {
                    int selectIndex = this.expressionMemoEdit.SelectionStart;
                    this.expressionMemoEdit.Select(selectIndex - this.listOfInputParameters.SelectedItem.ToString().IndexOf(')') + this.listOfInputParameters.SelectedItem.ToString().IndexOf('(') + 1, 0);
                }
            }
            else
            {
                int selectIndex = this.expressionMemoEdit.SelectionStart;
                this.expressionMemoEdit.Select(selectIndex - this.listOfInputParameters.SelectedItem.ToString().IndexOf(')') + this.listOfInputParameters.SelectedItem.ToString().IndexOf('('), 0);
            }
        }

        /// <summary>
        /// 初始化逻辑运算符
        /// </summary>
        private void InitialConstants()
        {
            constantsitemTable = new Dictionary<string, string>();
            constantsitemTable.Add("True", ExpressionResource.TrueDescription);
            constantsitemTable.Add("False", ExpressionResource.FalseDescription);
            constantsitemTable.Add("?", ExpressionResource.NullDescription);
        }

        protected virtual void AssingButtonImages()
        {
            plusItemButton.Image = icons.Images[0];
            layoutItemButton2.Image = icons.Images[1];
            layoutItemButton3.Image = icons.Images[2];
            layoutItemButton4.Image = icons.Images[3];
            layoutItemButton5.Image = icons.Images[4];
            layoutItemButton15.Image = icons.Images[5];
            layoutItemButton6.Image = icons.Images[6];
            layoutItemButton7.Image = icons.Images[7];
            layoutItemButton8.Image = icons.Images[8];
            layoutItemButton9.Image = icons.Images[9];
            layoutItemButton11.Image = icons.Images[10];
            layoutItemButton10.Image = icons.Images[11];
            layoutItemButton12.Image = icons.Images[12];
            layoutItemButton13.Image = icons.Images[13];
            layoutItemButton14.Image = icons.Images[14];
        }

        /// <summary>
        /// 初始化函数表达式
        /// </summary>
        private void InitialFunction()
        {
            functionitemTable = new Dictionary<string, string>();
            functionitemTable.Add("LocalDateTimeDayAfterTomorrow()", ExpressionResource.LocalDateTimeDayAfterTomorrowDescription);
            functionitemTable.Add("LocalDateTimeLastWeek()", ExpressionResource.LocalDateTimeLastWeekDescription);
            functionitemTable.Add("LocalDateTimeNextMonth()", ExpressionResource.LocalDateTimeNextMonthDescription);
            functionitemTable.Add("LocalDateTimeNextWeek()", ExpressionResource.LocalDateTimeNextWeekDescription);
            functionitemTable.Add("LocalDateTimeNextYear()", ExpressionResource.LocalDateTimeNextYearDescription);
            functionitemTable.Add("LocalDateTimeNow()", ExpressionResource.LocalDateTimeNowDescription);
            functionitemTable.Add("LocalDateTimeThisMonth()", ExpressionResource.LocalDateTimeThisMonthDescription);
            functionitemTable.Add("LocalDateTimeThisWeek()", ExpressionResource.LocalDateTimeThisWeekDescription);
            functionitemTable.Add("LocalDateTimeThisYear()", ExpressionResource.LocalDateTimeThisYearDescription);
            functionitemTable.Add("LocalDateTimeToday()", ExpressionResource.LocalDateTimeTodayDescription);
            functionitemTable.Add("LocalDateTimeTomorrow()", ExpressionResource.LocalDateTimeTomorrowDescription);
            functionitemTable.Add("LocalDateTimeTwoWeeksAway()", ExpressionResource.LocalDateTimeTwoWeeksAwayDescription);
            functionitemTable.Add("LocalDateTimeYesterday()", ExpressionResource.LocalDateTimeYesterdayDescription);
            functionitemTable.Add("Abs()", ExpressionResource.AbsDescription);
            functionitemTable.Add("Acos()", ExpressionResource.AcosDescription);
            functionitemTable.Add("AddDays(,)", ExpressionResource.AddDaysDescription);
            functionitemTable.Add("AddHours(,)", ExpressionResource.AddHoursDescription);
            functionitemTable.Add("AddMilliSeconds(,)", ExpressionResource.AddMilliSecondsDescription);
            functionitemTable.Add("AddMinutes(,)", ExpressionResource.AddMinutesDescription);
            functionitemTable.Add("AddMonths(,)", ExpressionResource.AddMonthsDescription);
            functionitemTable.Add("AddSeconds(,)", ExpressionResource.AddSecondsDescription);
            functionitemTable.Add("AddTicks(,)", ExpressionResource.AddTicksDescription);
            functionitemTable.Add("AddTimeSpan(,)", ExpressionResource.AddTimeSpanDescription);
            functionitemTable.Add("IsThisWeek()", ExpressionResource.IsThisWeekDescription);
            functionitemTable.Add("IsThisMonth()", ExpressionResource.IsThisMonthDescription);
            functionitemTable.Add("IsThisYear()", ExpressionResource.IsThisYearDescription);
            functionitemTable.Add("AddYears(,)", ExpressionResource.AddYearsDescription);
            functionitemTable.Add("Ascii('')", ExpressionResource.AsciiDescription);
            functionitemTable.Add("Asin()", ExpressionResource.AsinDescription);
            functionitemTable.Add("Atn()", ExpressionResource.AtnDescription);
            functionitemTable.Add("Atn2(,)", ExpressionResource.Atn2Description);
            functionitemTable.Add("BigMul(,)", ExpressionResource.BigMulDescription);
            functionitemTable.Add("Ceiling()", ExpressionResource.CeilingDescription);
            functionitemTable.Add("Char()", ExpressionResource.CharDescription);
            functionitemTable.Add("CharIndex('','')", ExpressionResource.CharIndexDescription);
            functionitemTable.Add("CharIndex('','',)", ExpressionResource.CharIndex3ParamDescription);
            functionitemTable.Add("Concat(,)", ExpressionResource.ConcatDescription);
            functionitemTable.Add("Cos()", ExpressionResource.CosDescription);
            functionitemTable.Add("Cosh()", ExpressionResource.CoshDescription);
            functionitemTable.Add("DateDiffDay(,)", ExpressionResource.DateDiffDayDescription);
            functionitemTable.Add("DateDiffHour(,)", ExpressionResource.DateDiffHourDescription);
            functionitemTable.Add("DateDiffMilliSecond(,)", ExpressionResource.DateDiffMilliSecondDescription);
            functionitemTable.Add("DateDiffMinute(,)", ExpressionResource.DateDiffMinuteDescription);
            functionitemTable.Add("DateDiffMonth(,)", ExpressionResource.DateDiffMonthDescription);
            functionitemTable.Add("DateDiffSecond(,)", ExpressionResource.DateDiffSecondDescription);
            functionitemTable.Add("DateDiffTick(,)", ExpressionResource.DateDiffTickDescription);
            functionitemTable.Add("DateDiffYear(,)", ExpressionResource.DateDiffYearDescription);
            functionitemTable.Add("Exp()", ExpressionResource.ExpDescription);
            functionitemTable.Add("Floor()", ExpressionResource.FloorDescription);
            functionitemTable.Add("GetDate()", ExpressionResource.GetDateDescription);
            functionitemTable.Add("GetDay()", ExpressionResource.GetDayDescription);
            functionitemTable.Add("GetDayOfWeek()", ExpressionResource.GetDayOfWeekDescription);
            functionitemTable.Add("GetDayOfYear()", ExpressionResource.GetDayOfYearDescription);
            functionitemTable.Add("GetHour()", ExpressionResource.GetHourDescription);
            functionitemTable.Add("GetMilliSecond()", ExpressionResource.GetMilliSecondDescription);
            functionitemTable.Add("GetMinute()", ExpressionResource.GetMinuteDescription);
            functionitemTable.Add("GetMonth()", ExpressionResource.GetMonthDescription);
            functionitemTable.Add("GetSecond()", ExpressionResource.GetSecondDescription);
            functionitemTable.Add("GetTimeOfDay()", ExpressionResource.GetTimeOfDayDescription);
            functionitemTable.Add("GetYear()", ExpressionResource.GetYearDescription);
            functionitemTable.Add("Iif(,,)", ExpressionResource.IifDescription);
            functionitemTable.Add("Insert('',,'')", ExpressionResource.InsertDescription);
            functionitemTable.Add("IsNull()", ExpressionResource.IsNullDescription);
            functionitemTable.Add("IsNullOrEmpty()", ExpressionResource.IsNullOrEmptyDescription);
            functionitemTable.Add("Len()", ExpressionResource.LenDescription);
            functionitemTable.Add("Lower()", ExpressionResource.LowerDescription);
            functionitemTable.Add("Log()", ExpressionResource.LogDescription);
            functionitemTable.Add("Log(,)", ExpressionResource.Log2ParamDescription);
            functionitemTable.Add("Log10()", ExpressionResource.Log10Description);
            functionitemTable.Add("Max(,)", ExpressionResource.MaxDescription);
            functionitemTable.Add("Min(,)", ExpressionResource.MinDescription);
            functionitemTable.Add("Now()", ExpressionResource.NowDescription);
            functionitemTable.Add("PadLeft(,)", ExpressionResource.PadLeftDescription);
            functionitemTable.Add("PadLeft(,,'')", ExpressionResource.PadLeft3ParamDescription);
            functionitemTable.Add("PadRight(,)", ExpressionResource.PadRightDescription);
            functionitemTable.Add("PadRight(,,'')", ExpressionResource.PadRight3ParamDescription);
            functionitemTable.Add("Power(,)", ExpressionResource.PowerDescription);
            functionitemTable.Add("Remove('',)", ExpressionResource.Remove2ParamDescription);
            functionitemTable.Add("Remove('',,)", ExpressionResource.Remove3ParamDescription);
            functionitemTable.Add("Replace('','','')", ExpressionResource.ReplaceDescription);
            functionitemTable.Add("Reverse('')", ExpressionResource.ReverseDescription);
            functionitemTable.Add("Rnd()", ExpressionResource.RndDescription);
            functionitemTable.Add("Round()", ExpressionResource.RoundDescription);
            functionitemTable.Add("Round(,)", ExpressionResource.Round2ParamDescription);
            functionitemTable.Add("Sign()", ExpressionResource.SignDescription);
            functionitemTable.Add("Sin()", ExpressionResource.SinDescription);
            functionitemTable.Add("Sinh()", ExpressionResource.SinhDescription);
            functionitemTable.Add("Sqr()", ExpressionResource.SqrDescription);
            functionitemTable.Add("Substring('',,)", ExpressionResource.Substring3paramDescription);
            functionitemTable.Add("Substring('',)", ExpressionResource.Substring2paramDescription);
            functionitemTable.Add("Tan()", ExpressionResource.TanDescription);
            functionitemTable.Add("Tanh()", ExpressionResource.TanhDescription);
            functionitemTable.Add("Today()", ExpressionResource.TodayDescription);
            functionitemTable.Add("ToInt()", ExpressionResource.ToIntDescription);
            functionitemTable.Add("ToLong()", ExpressionResource.ToLongDescription);
            functionitemTable.Add("ToFloat()", ExpressionResource.ToFloatDescription);
            functionitemTable.Add("ToDouble()", ExpressionResource.ToDoubleDescription);
            functionitemTable.Add("ToDecimal()", ExpressionResource.ToDecimalDescription);
            functionitemTable.Add("ToStr()", ExpressionResource.ToStrDescription);
            functionitemTable.Add("Trim()", ExpressionResource.TrimDescription);
            functionitemTable.Add("Upper()", ExpressionResource.UpperDescription);
            functionitemTable.Add("UtcNow()", ExpressionResource.UtcNowDescription);
            functionitemTable.Add("StartsWith('','')", ExpressionResource.StartsWithDescription);
            functionitemTable.Add("EndsWith('','')", ExpressionResource.EndsWithDescription);
            functionitemTable.Add("Contains('','')", ExpressionResource.ContainsDescription);
            functionitemTable.Add("Avg()", ExpressionResource.AvgAggregateDescription);
            functionitemTable.Add("Count()", ExpressionResource.CountAggregateDescription);
            functionitemTable.Add("Exists()", ExpressionResource.ExistsAggregateDescription);
            functionitemTable.Add("Max()", ExpressionResource.MaxAggregateDescription);
            functionitemTable.Add("Min()", ExpressionResource.MinAggregateDescription);
            functionitemTable.Add("Sum()", ExpressionResource.SumAggregateDescription);
            functionitemTable.Add("Single()", ExpressionResource.SingleAggregateDescription);
            functionitemTable.Add("Null(,,)", ExpressionResource.NoneEmptyDescription);
            functionitemTable.Add("Valid(,,)", ExpressionResource.ValidateDescription);
            functionitemTable.Add("Enable(,,)", ExpressionResource.EnableDescription);
            functionitemTable.Add("Font(,,,)", ExpressionResource.FontDescription);
            functionitemTable.Add("BCRGB(,,)", ExpressionResource.BCRGBDescription);
            functionitemTable.Add("RGB(,,)", ExpressionResource.RGBDescription);
        }

        private void CellExpressionShowFrm_Load(object sender, EventArgs e)
        {
            this.listOfInputTypes.Items.AddRange(new object[] { ExpressionResource.FunctionsText, ExpressionResource.OperatorsText, ExpressionResource.ConstantsText, ExpressionResource.ColumnText });

            //RecursionUnboundExpressionControls(ParentForm);
            //this.listOfInputParameters.Items.AddRange(new object[] { new ComboBoxEdit(),"plus" });
            // InitializeButtons();
            UpdateIcons();
            AssingButtonImages();
            InitializeOperators();
            InitialConstants();
            InitialFunction();
            InitialColumnInfo();
        }
        
        void InitialColumnInfo()
        {
            columnitemTable = new Dictionary<string, string>();
            Control[] controls = DataLayoutFrm.Controls.Find("columnAttributeGridControl", true);
            GridView gridview = (GridView)((DevExpress.XtraGrid.GridControl)controls[0]).ViewCollection[0];

            for (int i = 0; i < gridview.DataRowCount; i++)
            {
                object filedName = gridview.GetRowCellValue(i, "FIELDNAME");

                object captionName = gridview.GetRowCellValue(i, "CAPTION");

                if (filedName != null)
                {
                    columnitemTable.Add(filedName.ToString(), captionName == null ? "" : captionName.ToString());
                }
            }
        }

        private void layoutItemButton2_Click_1(object sender, EventArgs e)
        {

        }

        private void functionsTypes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonOK_Click_1(object sender, EventArgs e)
        {
        }

        private void layoutItemButton12_Click_1(object sender, EventArgs e)
        {

        }
    }
}