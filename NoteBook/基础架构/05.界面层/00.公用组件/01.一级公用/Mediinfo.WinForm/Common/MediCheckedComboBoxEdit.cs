using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 下拉框中包含复选框控件
    /// </summary>
    [ToolboxItem(true)]
    public class MediCheckedComboBoxEdit : DevExpress.XtraEditors.CheckedComboBoxEdit
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public MediCheckedComboBoxEdit()
        {
            this.Properties.SelectAllItemCaption = "全部";

            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }
    }

    [UserRepositoryItem("RegisterMediCheckedComboBoxEdit")]
    public class RepositoryItemMediCheckedComboBoxEdit : DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit, IExpressionInterface
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        /// <summary>
        /// 表达式属性
        /// </summary>
        [Browsable(true)]
        [Editor(typeof(UnboundExpressionEditor), typeof(UITypeEditor))]
        [Category("UnboundExpression"), Description("控件自定义表达式,返回值为当前控件的值或者属性或者其他控件的值或属性"), DefaultValue("")]
        public string UnboundExpression { get; set; }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static RepositoryItemMediCheckedComboBoxEdit()
        {
            RegisterMediCheckedComboBoxEdit();
        }

        public RepositoryItemMediCheckedComboBoxEdit()
        {
            this.SelectAllItemCaption = "全选";
            this.PopupWidthMode = DevExpress.XtraEditors.PopupWidthMode.ContentWidth;
            this.PopupFormMinSize = new Size(this.PopupFormSize.Width, 100);
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        public override string EditorTypeName { get { return "MediCheckedComboBoxEdit"; } }

        public static void RegisterMediCheckedComboBoxEdit()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo("MediCheckedComboBoxEdit", typeof(MediCheckedComboBoxEdit), typeof(RepositoryItemMediCheckedComboBoxEdit), typeof(DevExpress.XtraEditors.ViewInfo.PopupContainerEditViewInfo), new ButtonEditPainter(), true, null, typeof(DevExpress.Accessibility.ComboBoxEditAccessible)));
        }
    }
}