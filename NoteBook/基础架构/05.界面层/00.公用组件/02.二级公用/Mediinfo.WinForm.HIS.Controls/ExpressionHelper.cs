using DevExpress.Data.Filtering;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.XtraGrid.Views.Grid;

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 表达式帮助类
    /// </summary>
    public static class ExpressionHelper
    {
        /// <summary>
        ///
        /// </summary>
        public static string StringCriteria { get; set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        public static OperandValue[] CriteriaParametersList { get; set; }

        /// <summary>
        /// 外部控件
        /// </summary>
        public static Control DevForm { get; set; }

        /// <summary>
        /// 外部调用表达式接口
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="expressionString"></param>
        /// <param name="mediForm"></param>
        /// <param name="gridView"></param>
        /// <param name="expressionApplyType"></param>
        /// <param name="expressionType"></param>
        public static void ExpressionFunc(string controlName, string expressionString, Control mediForm, DevExpress.XtraGrid.Views.Grid.GridView gridView, ExpressionApplyType expressionApplyType, ExpressionType expressionType)
        {
            DevForm = mediForm;
            StringCriteria = expressionString;
            OperandValue[] criteriaParametersList = null;//返回参数列表
            CriteriaOperator criteriaOperator = CriteriaParser.Parse(expressionString, out criteriaParametersList);
            CriteriaParametersList = criteriaParametersList;
            object result;
            if (gridView != null)
            {
                switch (expressionApplyType)
                {
                    case ExpressionApplyType.RowType:
                        for (int i = 0; i < gridView.DataRowCount; i++)
                        {
                            switch (expressionType)
                            {
                                case ExpressionType.BackColorType:

                                    break;
                            }
                        }
                        break;

                    case ExpressionApplyType.ColumnType:
                        for (int i = 0; i < gridView.DataRowCount; i++)
                        {
                            RecursionExpressionTree(criteriaOperator, out result, i);
                            currentExpressionGridviewControlColumnValue(controlName, gridView, result, i);
                        }
                        break;

                    case ExpressionApplyType.OtherType:
                        break;

                    default:
                        break;
                }
            }
            else
            {
                RecursionExpressionTree(criteriaOperator, out result, 0);
                //currentExpressionControlValue(controlName, mediForm, result);
            }
        }

        /// <summary>
        ///  颜色外部调用表达式接口
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="expressionString"></param>
        /// <param name="mediForm"></param>
        /// <param name="gridView"></param>
        /// <param name="expressionApplyType"></param>
        /// <param name="expressionType"></param>
        /// <param name="rowHandle"></param>
        /// <returns></returns>
        public static System.Drawing.Color? ColorExpressionFunc(string controlName, string expressionString, Control mediForm, DevExpress.XtraGrid.Views.Grid.GridView gridView, ExpressionApplyType expressionApplyType, ExpressionType expressionType, int rowHandle)
        {
            try
            {
                DevForm = mediForm;
                StringCriteria = expressionString;
                OperandValue[] criteriaParametersList = null;//返回参数列表
                CriteriaOperator criteriaOperator = CriteriaParser.Parse(expressionString, out criteriaParametersList);
                CriteriaParametersList = criteriaParametersList;
                object result;
                System.Drawing.Color? color = null;
                if (gridView != null)
                {
                    switch (expressionApplyType)
                    {
                        case ExpressionApplyType.RowType:
                            switch (expressionType)
                            {
                                case ExpressionType.BackColorType:
                                    RecursionExpressionTree(criteriaOperator, out result, rowHandle);
                                    color = currentExpressionGridviewControlRowValue(controlName, gridView, result, rowHandle, ExpressionType.BackColorType);
                                    break;
                            }

                            return color;

                        case ExpressionApplyType.ColumnType:
                            RecursionExpressionTree(criteriaOperator, out result, rowHandle);
                            color = currentExpressionGridviewControlRowValue(controlName, gridView, result, rowHandle, ExpressionType.BackColorType);
                            break;

                        case ExpressionApplyType.OtherType:
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    RecursionExpressionTree(criteriaOperator, out result, 0);
                    //currentExpressionControlValue(controlName, mediForm, result);
                }
                return color;
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>
        /// gridview控件表达式结果返回值(列类型)
        /// </summary>
        private static void currentExpressionGridviewControlColumnValue(string controlName, GridView gridView, object expressionReturnValue, int componentIndex)
        {
            gridView.SetRowCellValue(componentIndex, gridView.Columns[controlName], expressionReturnValue);
        }

        /// <summary>
        /// gridview控件表达式结果返回值(行类型)
        /// </summary>
        private static System.Drawing.Color? currentExpressionGridviewControlRowValue(string controlName, GridView gridView, object expressionReturnValue, int componentIndex, ExpressionType expressionType)
        {
            switch (expressionType)
            {
                case ExpressionType.BackColorType:
                    if (expressionReturnValue != null && Convert.ToString(expressionReturnValue) != "")
                    {
                        return (System.Drawing.Color)expressionReturnValue;
                    }

                    break;
            }
            return null;
            //gridView.SetRowCellValue(componentIndex, gridView.Columns[controlName], expressionReturnValue);
        }

        private static void RecursionExpressionTree(CriteriaOperator criteriaOperator, out object result, int componentIndex)
        {
            string criteriaOperatorType = criteriaOperator.GetType().FullName;

            result = string.Empty;
            switch (criteriaOperatorType)
            {
                case "DevExpress.Data.Filtering.ContainsOperator":

                    break;

                case "DevExpress.Data.Filtering.BetweenOperator":

                    break;

                case "DevExpress.Data.Filtering.BinaryOperator":
                    var binarycriteriaOperatorType = ((DevExpress.Data.Filtering.BinaryOperator)criteriaOperator).OperatorType;
                    var binarycriteriaLeftOperator = ((DevExpress.Data.Filtering.BinaryOperator)criteriaOperator).LeftOperand;
                    object leftresult = 0;
                    RecursionExpressionTree(binarycriteriaLeftOperator, out leftresult, componentIndex);

                    var binarycriteriaRightOperator = ((DevExpress.Data.Filtering.BinaryOperator)criteriaOperator).RightOperand;
                    object rightresult;
                    RecursionExpressionTree(binarycriteriaRightOperator, out rightresult, componentIndex);

                    if (binarycriteriaOperatorType == BinaryOperatorType.LessOrEqual)
                    {
                        if (Convert.ToInt32(leftresult) <= Convert.ToInt32(rightresult))
                        {
                            result = "true";
                        }
                        else
                        {
                            result = "false";
                        }
                    }
                    else if (binarycriteriaOperatorType == BinaryOperatorType.Greater)
                    {
                        if (Convert.ToDouble(leftresult) > Convert.ToDouble(rightresult))
                        {
                            result = "true";
                        }
                        else
                        {
                            result = "false";
                        }
                    }
                    else if (binarycriteriaOperatorType == BinaryOperatorType.Equal)
                    {
                        var lfn = leftresult.GetType().FullName;
                        var rfn = rightresult.GetType().FullName;
                        if (rfn != null && lfn != null && lfn.Equals("System.String") && rfn.Equals("System.String"))
                        {
                            result = Convert.ToString(leftresult) == Convert.ToString(rightresult) ? "true" : "false";
                        }
                        else
                        {
                            result = Math.Abs(Convert.ToDouble(leftresult) - Convert.ToDouble(rightresult)) > 0 ? "true" : "false";
                        }
                    }
                    else if (binarycriteriaOperatorType == BinaryOperatorType.Plus)
                    {
                        result = (Convert.ToDouble(leftresult) + Convert.ToDouble(rightresult)).ToString(CultureInfo.InvariantCulture);
                    }
                    else if (binarycriteriaOperatorType == BinaryOperatorType.Divide)
                    {
                        result = (Convert.ToDouble(leftresult) / Convert.ToDouble(rightresult)).ToString(CultureInfo.InvariantCulture);
                    }
                    else if (binarycriteriaOperatorType == BinaryOperatorType.GreaterOrEqual)
                    {
                        result = Convert.ToDouble(leftresult) >= Convert.ToDouble(rightresult) ? "true" : "false";
                    }
                    else if (binarycriteriaOperatorType == BinaryOperatorType.Less)
                    {
                        result = Convert.ToInt32(leftresult) < Convert.ToInt32(rightresult) ? "true" : "false";
                    }
                    else if (binarycriteriaOperatorType == BinaryOperatorType.NotEqual)
                    {
                        result = Convert.ToInt32(leftresult) != Convert.ToInt32(rightresult) ? "true" : "false";
                    }
                    else if (binarycriteriaOperatorType == BinaryOperatorType.Modulo)
                    {
                        if (Math.Abs(Convert.ToDouble(leftresult) % Convert.ToDouble(rightresult)) > 0)
                        {
                            result = 0;
                        }
                        else
                        {
                            result = Convert.ToDouble(leftresult) % Convert.ToDouble(rightresult);
                        }
                    }
                    else if (binarycriteriaOperatorType == BinaryOperatorType.Multiply)
                    {
                        result = (Convert.ToInt32(leftresult) * Convert.ToInt32(rightresult)).ToString();
                    }
                    break;
                case "DevExpress.Data.Filtering.GroupOperator":
                    CriteriaOperatorCollection groupOperatorcriteriaOperatorCollection = ((GroupOperator)criteriaOperator).Operands;
                    var groupOperatorType = ((GroupOperator)criteriaOperator).OperatorType;
                    string[] results = new string[(groupOperatorcriteriaOperatorCollection.Count)];
                    for (int i = 0; i < groupOperatorcriteriaOperatorCollection.Count; i++)
                    {
                        RecursionExpressionTree(groupOperatorcriteriaOperatorCollection[i], out result, componentIndex);
                        results[i] = (string)result;
                    }
                    if (groupOperatorType == GroupOperatorType.And)
                    {
                        int count = 0;
                        foreach (var t in results)
                            if (t == "false")
                                count++;
                        if (count > 0)
                            result = "false";
                        result = "true";
                    }
                    else if (groupOperatorType == GroupOperatorType.Or)
                    {
                        int count = 0;
                        foreach (var t in results)
                            if (t == "true")
                                count++;
                        if (count > 0)
                            result = "true";
                        result = "false";
                    }
                    break;
                case "DevExpress.Data.Filtering.ConstantValue":
                    object constantValue = ((ConstantValue)criteriaOperator).Value;
                    result = constantValue.ToString();
                    break;
                case "DevExpress.Data.Filtering.OperandProperty":
                    var OperandPropertyName = ((OperandProperty)criteriaOperator).PropertyName;
                    object componentValue = null;
                    RecursionMediValueComponent(DevForm, OperandPropertyName, ref componentValue, componentIndex);
                    if (componentValue != null)
                        result = componentValue.ToString();
                    else
                        result = 0;
                    break;

                case "DevExpress.Data.Filtering.AggregateOperand":
                    var aggregateOperandType = ((AggregateOperand)criteriaOperator).AggregateType;

                    var aggregateOperandExpress = ((AggregateOperand)criteriaOperator).AggregatedExpression;

                    if (aggregateOperandExpress is OperandProperty property)
                    {
                        var aggregateOperandExpressPropertyName = property.PropertyName;

                        if (aggregateOperandType == Aggregate.Sum)
                        {
                            object componentSumValue = null;
                            RecursionMediValueComponent(DevForm, aggregateOperandExpressPropertyName, ref componentSumValue, componentIndex);
                            result = componentSumValue.ToString();
                        }
                    }
                    else
                    {
                        RecursionExpressionTree(aggregateOperandExpress, out result, componentIndex);

                        if (aggregateOperandType == Aggregate.Avg)
                        {
                            string[] variables = aggregateOperandExpress.ToString().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                            int operandCount = variables.Length + 1;
                            result = Convert.ToString(Convert.ToDouble(result) / operandCount);
                        }
                    }
                    break;
                case "DevExpress.Data.Filtering.FunctionOperator":
                    CriteriaOperatorCollection FunctionOperatorcriteriaOperatorCollection = ((FunctionOperator)criteriaOperator).Operands;
                    FunctionOperatorType functionOperatorType = ((FunctionOperator) criteriaOperator).OperatorType;
                    if (functionOperatorType != FunctionOperatorType.Custom)
                    {
                        if (functionOperatorType == FunctionOperatorType.Max)
                        {
                            string leftoperator = ((OperandProperty)FunctionOperatorcriteriaOperatorCollection[0]).PropertyName;
                            string rightoperator = ((OperandProperty)FunctionOperatorcriteriaOperatorCollection[1]).PropertyName;
                            int compareResult = CultureInfo.CurrentCulture.CompareInfo.Compare(leftoperator, rightoperator, CompareOptions.IgnoreCase);
                            result = compareResult > 0 ? leftoperator : rightoperator;
                        }
                        else if (functionOperatorType == FunctionOperatorType.Min)
                        {
                            string leftoperator = ((OperandProperty)FunctionOperatorcriteriaOperatorCollection[0]).PropertyName;
                            string rightoperator = ((OperandProperty)FunctionOperatorcriteriaOperatorCollection[1]).PropertyName;
                            int compareResult = CultureInfo.CurrentCulture.CompareInfo.Compare(leftoperator, rightoperator, CompareOptions.IgnoreCase);
                            result = compareResult > 0 ? rightoperator : leftoperator;
                        }
                        else
                        {
                            object[] functionresults = new object[(FunctionOperatorcriteriaOperatorCollection.Count)];
                            for (int i = 0; i < FunctionOperatorcriteriaOperatorCollection.Count; i++)
                            {
                                RecursionExpressionTree(FunctionOperatorcriteriaOperatorCollection[i], out result, componentIndex);
                                functionresults[i] = result;
                            }
                            if (functionOperatorType == FunctionOperatorType.Iif)
                            {
                                result = Convert.ToBoolean(functionresults[0]) ? functionresults[1] : functionresults[2];
                            }
                        }
                    }
                    else
                    {
                        OperandValue customfunctionresults = ((OperandValue)FunctionOperatorcriteriaOperatorCollection[0]);
                        OperandValue rValue = ((OperandValue)FunctionOperatorcriteriaOperatorCollection[1]);
                        OperandValue gValue = ((OperandValue)FunctionOperatorcriteriaOperatorCollection[2]);
                        OperandValue bValue = ((OperandValue)FunctionOperatorcriteriaOperatorCollection[3]);
                        if (customfunctionresults.Value.ToString().ToLower().Trim() == "bcrgb")
                        {
                            result = System.Drawing.Color.FromArgb(Convert.ToInt32(rValue.Value), Convert.ToInt32(gValue.Value), Convert.ToInt32(bValue.Value));
                        }
                        else if (customfunctionresults.Value.ToString().ToLower().Trim() == "rgb")
                        {
                            result = System.Drawing.Color.FromArgb(Convert.ToInt32(rValue.Value), Convert.ToInt32(gValue.Value), Convert.ToInt32(bValue.Value));
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// 获取控件值
        /// </summary>
        /// <param name="component"></param>
        /// <param name="propertyName"></param>
        /// <param name="componentValue"></param>
        /// <param name="componentIndex"></param>
        private static void RecursionMediValueComponent(Component component, string propertyName, ref object componentValue, int componentIndex)
        {
            if (component is Control)
            {
                if (((Control)component).Name == propertyName)
                {
                    if (component is MediButton)
                    {
                        componentValue = ((MediButton)component).Text;
                    }
                    else if (component is MediTextBox)
                    {
                        componentValue = ((MediTextBox)component).EditValue;
                    }
                    else if (component is MediGridLookUpEdit)
                    {
                        componentValue = ((MediGridLookUpEdit)component).EditValue;
                    }
                    else if (component is MediComboBox)
                    {
                        componentValue = ((MediComboBox)component).EditValue;
                    }
                }
                else
                {
                    if (component is MediGridControl)
                    {
                        foreach (var medigridview in ((MediGridControl)component).ViewCollection)
                        {
                            if (!(medigridview is MediGridView))
                                continue;

                            if (((MediGridView)medigridview).GetRowCellDisplayText(componentIndex, propertyName) == null)
                                continue;
                            componentValue = ((MediGridView)medigridview).GetRowCellDisplayText(componentIndex, propertyName);


                        }
                    }
                    else
                    {
                        foreach (Component item in ((Control)component).Controls)
                        {
                            if (item is MediGridControl)
                            {
                                foreach (var medigridview in ((MediGridControl)item).ViewCollection)
                                {
                                    if (!(medigridview is MediGridView))
                                        continue;

                                    if (((MediGridView)medigridview).GetDataRow(componentIndex) == null)
                                        continue;
                                    object columnSum = ((MediGridView)medigridview).GetDataRow(componentIndex)[propertyName];

                                    componentValue = columnSum.ToString();
                                }
                            }
                            else
                            {
                                if (item is Control)
                                {
                                    RecursionMediValueComponent(item, propertyName, ref componentValue, componentIndex);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 表达式作用于类型
    /// </summary>
    public enum ExpressionApplyType
    {
        /// <summary>
        /// 针对行的表达式
        /// </summary>
        RowType = 1,

        /// <summary>
        /// 针对列的表达式
        /// </summary>
        ColumnType = 2,

        /// <summary>
        /// 针对按钮或者未来要扩展的表达式
        /// </summary>
        OtherType = 3,
    }

    /// <summary>
    /// 表达式类型
    /// </summary>
    public enum ExpressionType
    {
        /// <summary>
        /// 非空类型
        /// </summary>
        NoneEmptyType = 1,

        /// <summary>
        /// 验证类型
        /// </summary>
        ValidateType = 2,

        /// <summary>
        /// 背景色类型
        /// </summary>
        BackColorType = 3,

        /// <summary>
        /// 前景色类型
        /// </summary>
        ForeColorType = 4,

        /// <summary>
        /// 值类型
        /// </summary>
        ValueType = 5,

        /// <summary>
        /// 控件状态启用类型
        /// </summary>
        EnableStateType = 6
    }

    public struct ColorRGB
    {
        public int R;
        public int G;
        public int B;
    }
}