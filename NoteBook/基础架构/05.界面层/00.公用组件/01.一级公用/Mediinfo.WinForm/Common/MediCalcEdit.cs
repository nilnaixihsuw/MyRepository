using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// MediCalcEdit控件允许您编辑数值。使用下拉计算器，您可以执行基本计算，例如加法，乘法，获取倒数和使用存储器寄存器等。
    ///使用编辑器的Properties属性自定义编辑器。这提供了控制编辑器样式，文本编辑和值精度等的设置。
    ///Value和EditValue属性可用于指定calc编辑器的值。当EditValue接受任何对象时，Value仅接受十进制值。 Value只返回将edit值转换为System.Decimal类型的结果。
    ///当焦点移动到另一个控件或在编辑框中按下Enter键时（如果RepositoryItemTextEdit.ValidateOnEnterKey属性设置为true），编辑器将编辑值转换为Decimal类型并将转换结果分配回编辑值。
    ///calc编辑器允许您在编辑框中输入任何文本（包括alpha符号）。这会更改控件的编辑值（编辑框中显示的文本只是编辑值的文本表示）。在打开下拉列表之前，编辑值将转换为十进制值，下拉计算器将使用此转换的结果。
    ///如果接受使用下拉计算器评估的值，则将其分配给EditValue属性。否则，保留旧值。 RepositoryItemPopupBase.CloseUp主题描述了在接受或丢弃所选值时如何关闭下拉列表。
    ///要在编辑框中禁用文本编辑，可以将RepositoryItemButtonEdit.TextEditStyle属性设置为TextEditStyles.DisableTextEditor。(在网格中使用)
    /// </summary>
    [UserRepositoryItem("RegisterMediCalcEdit")]
    public class RepositoryItemMediCalcEdit : RepositoryItemCalcEdit, IExpressionInterface, IInputIMEMode
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        /// <summary>
        ///表达式属性
        /// </summary>
        [Browsable(true)]
        [Editor(typeof(UnboundExpressionEditor), typeof(UITypeEditor))]
        [Category("UnboundExpression"), Description("控件自定义表达式,返回值为当前控件的值或者属性或者其他控件的值或属性"), DefaultValue("")]
        public string UnboundExpression
        {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        static RepositoryItemMediCalcEdit()
        {
            RegisterMediCalcEdit();
        }

        public const string CustomEditName = "MediCalcEdit";

        public RepositoryItemMediCalcEdit()
        {
            // this.SetCustomSkin();
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        public override string EditorTypeName => CustomEditName;

        /// <summary>
        /// 输入法模式
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        public MediInfoImeMode MediinfoIMEMode { get; set; }

        public static void RegisterMediCalcEdit()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediCalcEdit), typeof(RepositoryItemMediCalcEdit), typeof(MediCalcEditViewInfo), new MediCalcEditPainter(), true, img));
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemMediCalcEdit source = item as RepositoryItemMediCalcEdit;
                if (source == null) return;
            }
            finally
            {
                EndUpdate();
            }
        }
    }

    /// <summary>
    /// MediCalcEdit控件允许您编辑数值。使用下拉计算器，您可以执行基本计算，例如加法，乘法，获取倒数和使用存储器寄存器等。
    ///使用编辑器的Properties属性自定义编辑器。这提供了控制编辑器样式，文本编辑和值精度等的设置。
    ///Value和EditValue属性可用于指定calc编辑器的值。当EditValue接受任何对象时，Value仅接受十进制值。 Value只返回将edit值转换为System.Decimal类型的结果。
    ///当焦点移动到另一个控件或在编辑框中按下Enter键时（如果RepositoryItemTextEdit.ValidateOnEnterKey属性设置为true），编辑器将编辑值转换为Decimal类型并将转换结果分配回编辑值。
    ///calc编辑器允许您在编辑框中输入任何文本（包括alpha符号）。这会更改控件的编辑值（编辑框中显示的文本只是编辑值的文本表示）。在打开下拉列表之前，编辑值将转换为十进制值，下拉计算器将使用此转换的结果。
    ///如果接受使用下拉计算器评估的值，则将其分配给EditValue属性。否则，保留旧值。 RepositoryItemPopupBase.CloseUp主题描述了在接受或丢弃所选值时如何关闭下拉列表。
    ///要在编辑框中禁用文本编辑，可以将RepositoryItemButtonEdit.TextEditStyle属性设置为TextEditStyles.DisableTextEditor。
    /// </summary>
    [ToolboxItem(true)]
    public class MediCalcEdit : CalcEdit, IInputIMEMode
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        static MediCalcEdit()
        {
            RepositoryItemMediCalcEdit.RegisterMediCalcEdit();
        }

        /// <summary>
        /// 是否全选文本
        /// </summary>
        [Description("是否全选文本"), DefaultValue(true), Browsable(true)]
        public bool FocusAllText { get; set; } = true;

        public MediCalcEdit()
        {
            this.SetEditorsCustomSkin();

            this.GotFocus -= MediCalcEdit_GotFocus;
            this.GotFocus += MediCalcEdit_GotFocus;
            this.MouseUp -= MediCalcEdit_MouseUp;
            this.MouseUp += MediCalcEdit_MouseUp;
            this.Enter -= MediCalcEdit_Enter;
            this.Enter += MediCalcEdit_Enter;
            this.MouseDown -= MediCalcEdit_MouseDown;
            this.MouseDown += MediCalcEdit_MouseDown;
            this.MinimumSize = new Size(0, 26);

            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        private void MediCalcEdit_Enter(object sender, EventArgs e)
        {
            enter = true;
            BeginInvoke(new MethodInvoker(ResetEnterFlag));
        }

        private void MediCalcEdit_MouseDown(object sender, MouseEventArgs e)
        {
            needSelect = enter;
        }

        private bool enter = false, needSelect = false;
        private void ResetEnterFlag()
        {
            enter = false;
        }

        private void MediCalcEdit_MouseUp(object sender, MouseEventArgs e)
        {
            if (needSelect)
            {
                if (this.Properties.TextEditStyle == DevExpress.XtraEditors.Controls.TextEditStyles.Standard)
                    (sender as TextEdit).SelectAll();

            }

        }

        private void MediCalcEdit_GotFocus(object sender, EventArgs e)
        {
            if (FocusAllText)
                if (this.Properties.TextEditStyle == DevExpress.XtraEditors.Controls.TextEditStyles.Standard)
                    (sender as TextEdit).SelectAll();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]

        public new RepositoryItemMediCalcEdit Properties => base.Properties as RepositoryItemMediCalcEdit;

        public override string EditorTypeName => RepositoryItemMediCalcEdit.CustomEditName;

        /// <summary>
        /// 输入法模式
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        public MediInfoImeMode MediinfoIMEMode { get; set; }

        protected override PopupBaseForm CreatePopupForm()
        {
            return new MediCalcEditPopupForm(this);
        }
    }

    public class MediCalcEditViewInfo : CalcEditViewInfo
    {
        public MediCalcEditViewInfo(RepositoryItem item) : base(item)
        {
        }
    }

    public class MediCalcEditPainter : ButtonEditPainter
    {
        public MediCalcEditPainter()
        {
        }
    }

    public class MediCalcEditPopupForm : PopupCalcEditForm
    {
        public MediCalcEditPopupForm(MediCalcEdit ownerEdit) : base(ownerEdit)
        {
        }
    }
}