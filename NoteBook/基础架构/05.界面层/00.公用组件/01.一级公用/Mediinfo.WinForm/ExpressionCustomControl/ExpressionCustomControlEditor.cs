using DevExpress.Data.Filtering;
using DevExpress.Data.Filtering.Helpers;

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 表达式
    /// </summary>
    public class ExpressionCustomControlEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null && context.Instance != null)
            {
                return UITypeEditorEditStyle.Modal;
            }

            return base.GetEditStyle(context);
        }

        /// <summary>
        /// 重写值
        /// </summary>
        /// <param name="context"></param>
        /// <param name="provider"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorService = null;

            if (context != null && context.Instance != null && provider != null)
            {
                editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (editorService != null)
                {
                    string sourceValue = string.Empty;
                    if (value != null)
                        sourceValue = value.ToString();
                    CellExpressionShowFrm exeditor = new CellExpressionShowFrm(((System.Windows.Forms.Control)editorService).TopLevelControl, sourceValue);
                    if (editorService.ShowDialog(exeditor) == DialogResult.OK)
                    {
                        value = exeditor.UnboundExpression;
                        return value;
                    }
                }
            }

            return value;
        }
    }


    /// <summary>
    /// 表达式公共帮助类
    /// </summary>
    public static class ExpressionCommonHelper
    {
        /// <summary>
        /// 表达式验证
        /// </summary>
        public static bool ExpressionValidate(string expressionStr, out string errorMsg)
        {
            if (string.IsNullOrWhiteSpace(expressionStr))
            {
                errorMsg = string.Empty;
                return true;
            }
            string[] multiExpressionString = expressionStr.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            if (multiExpressionString.Length < 1)
            {
                errorMsg = string.Empty;
                return true;
            }
            foreach (var item in multiExpressionString)
            {
                try
                {
                    OperandValue[] criteriaParametersList = null;
                    CriteriaOperator criteriaOperator = CriteriaParser.Parse(item, out criteriaParametersList);
                    errorMsg = string.Empty;
                    return true;
                }
                catch (Exception ex)
                {
                    errorMsg = ex.Message;

                    return false;
                }
            }
            errorMsg = string.Empty;
            return true;
        }
    }
}