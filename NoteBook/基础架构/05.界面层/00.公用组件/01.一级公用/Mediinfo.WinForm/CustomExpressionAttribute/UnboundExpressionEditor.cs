using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 表达式窗体
    /// </summary>
    public class UnboundExpressionEditor : UITypeEditor
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
                    IExpressionInterface control = (IExpressionInterface)context.Instance;
                    ExpressionEditor exeditor = new ExpressionEditor(control);
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
}