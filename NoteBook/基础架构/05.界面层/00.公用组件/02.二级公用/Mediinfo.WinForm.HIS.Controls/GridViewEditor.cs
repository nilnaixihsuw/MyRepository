using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Design;
using DevExpress.XtraGrid.Columns;

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// Gridview
    /// </summary>
    public class GridViewEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            // 编辑属性值时，在右侧显示...更多按钮
            return UITypeEditorEditStyle.Modal;//DropDown;//.Modal;
        }

        protected virtual ExpressionEditorForm CreateForm(object instance, IDesignerHost designerHost, object value)
        {
            if (instance is StyleFormatConditionBase) return new ConditionExpressionEditorForm(instance, designerHost);
            if (instance is FormatConditionRuleBase) return new FormatRuleExpressionEditorForm(instance, designerHost, value);
            if (value != null)
            {
                if (instance is GridColumn)
                {
                    (instance as GridColumn).UnboundExpression = value.ToString();
                }
                else
                {
                    if (instance is GridColumnInfo)
                    {
                        (instance as GridColumnInfo).UnboundExpression = value.ToString();
                    }
                }
            }
            else
            {
                if (instance is GridColumn)
                {
                    (instance as GridColumn).UnboundExpression = "";
                }
                else
                {
                    if (instance is GridColumnInfo)
                    {
                        (instance as GridColumnInfo).UnboundExpression = "";
                    }
                }
            }
            return new UnboundColumnExpressionEditorForm(instance, designerHost);

            //return null;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            var edSvc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            if (edSvc != null)
            {
                string sValue = "";
                if (value != null)
                    sValue = value.ToString();

                var popedControl = new EditorControl(sValue);
                // 还有ShowDialog这种方式，可以弹出一个窗体来进行编辑
                edSvc.DropDownControl(popedControl);

                value = popedControl.result;
            }
            return base.EditValue(context, provider, value);
        }
    }

    /// <summary>
    /// 存储列信息
    /// </summary>
    public class GridColumnInfo : IDataColumnInfo
    {
        private GridColumnCollection columns;
        private GridColumn column;

        public GridColumnInfo(GridColumnCollection columns)
        {
            this.columns = columns;
        }

        private GridColumnInfo(GridColumn column)
        {
            this.column = column;
        }

        #region IDataColumnInfo Members

        public string Caption
        {
            get
            {
                if (column == null) return string.Empty;
                return string.IsNullOrEmpty(column.Caption) ? column.FieldName : column.Caption;
            }
        }

        public List<IDataColumnInfo> Columns { get { return GetColumns(); } }

        private List<IDataColumnInfo> GetColumns()
        {
            if (column != null) return null;
            List<IDataColumnInfo> result = new List<IDataColumnInfo>();
            foreach (GridColumn col in columns)
                result.Add(new GridColumnInfo(col));
            return result;
        }

        public DataControllerBase Controller { get { return null; } }

        public string ExpressionValue { get; set; }

        public string FieldName { get { return column == null ? string.Empty : column.FieldName; } }

        public Type FieldType { get { return column == null ? null : column.ColumnType; } }

        public string Name { get { return Caption; } }

        public string UnboundExpression { get; set; }

        #endregion IDataColumnInfo Members
    }

    /// <summary>
    /// 文本控件
    /// </summary>
    public class EditorControl : MediUserControl
    {
        public EditorControl(string sValue)
        {
            this.textBox1 = new TextBox();
            this.textBox1.Name = "textBox1";
            this.textBox1.Dock = DockStyle.Fill;
            this.textBox1.Multiline = true;
            this.textBox1.LostFocus += TextBox1_LostFocus;
            this.textBox1.Text = sValue;

            this.SuspendLayout();
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox1);
            this.Name = "EditorControl";
            this.Size = new System.Drawing.Size(210, 64);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void TextBox1_LostFocus(object sender, EventArgs e)
        {
            result = this.textBox1.Text;
        }

        public string result = "";
        private TextBox textBox1;
    }
}