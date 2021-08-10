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

namespace Mediinfo.WinForm.Common
{
    [UserRepositoryItem("RegisterMediColorEdit")]
    public class RepositoryItemMediColorEdit : RepositoryItemColorEdit, IExpressionInterface, IInputIMEMode
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        [Browsable(true)]
        [Editor(typeof(UnboundExpressionEditor), typeof(UITypeEditor))]
        [Category("UnboundExpression"), Description("控件自定义表达式,返回值为当前控件的值或者属性或者其他控件的值或属性"), DefaultValue("")]
        public string UnboundExpression { get; set; }

        static RepositoryItemMediColorEdit()
        {
            RegisterMediColorEdit();
        }

        public const string CustomEditName = "MediColorEdit";

        public RepositoryItemMediColorEdit()
        {
            //this.SetCustomSkin();
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

        public static void RegisterMediColorEdit()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediColorEdit), typeof(RepositoryItemMediColorEdit), typeof(MediColorEditViewInfo), new MediColorEditPainter(), true, img));
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemMediColorEdit source = item as RepositoryItemMediColorEdit;
                if (source == null) return;

                this.StoreColorAsInteger = true;
            }
            finally
            {
                EndUpdate();
            }
        }
    }

    [ToolboxItem(true)]
    public class MediColorEdit : ColorEdit, IInputIMEMode
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        /// <summary>
        /// 是否全选文本
        /// </summary>
        [Description("是否全选文本"), DefaultValue(true), Browsable(true)]
        public bool FocusAllText { get; set; } = true;

        static MediColorEdit()
        {
            RepositoryItemMediColorEdit.RegisterMediColorEdit();
        }

        public MediColorEdit()
        {
            this.GotFocus -= MediColorEdit_GotFocus;
            this.GotFocus += MediColorEdit_GotFocus;
            this.MouseUp -= MediColorEdit_MouseUp;
            this.MouseUp += MediColorEdit_MouseUp;
            this.Enter -= MediColorEdit_Enter;
            this.Enter += MediColorEdit_Enter;
            this.MouseDown -= MediColorEdit_MouseDown;
            this.MouseDown += MediColorEdit_MouseDown;
            this.SetEditorsCustomSkin();
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

        private void MediColorEdit_Enter(object sender, EventArgs e)
        {
            enter = true;
            BeginInvoke(new MethodInvoker(ResetEnterFlag));
        }

        private void MediColorEdit_MouseDown(object sender, MouseEventArgs e)
        {
            needSelect = enter;
        }

        private bool enter = false, needSelect = false;
        private void ResetEnterFlag()
        {
            enter = false;
        }

        private void MediColorEdit_MouseUp(object sender, MouseEventArgs e)
        {
            if (needSelect)
            {
                if (this.Properties.TextEditStyle == DevExpress.XtraEditors.Controls.TextEditStyles.Standard)
                    (sender as TextEdit).SelectAll();
            }
        }

        private void MediColorEdit_GotFocus(object sender, System.EventArgs e)
        {
            if (FocusAllText)
                if (this.Properties.TextEditStyle == DevExpress.XtraEditors.Controls.TextEditStyles.Standard)
                    (sender as TextEdit).SelectAll();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMediColorEdit Properties
        {
            get
            {
                return base.Properties as RepositoryItemMediColorEdit;
            }
        }

        public override string EditorTypeName => RepositoryItemMediColorEdit.CustomEditName;

        /// <summary>
        /// 输入法模式
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        public MediInfoImeMode MediinfoIMEMode { get; set; }

        protected override PopupBaseForm CreatePopupForm()
        {
            return new MediColorEditPopupForm(this);
        }
    }

    public class MediColorEditViewInfo : ColorEditViewInfo
    {
        public MediColorEditViewInfo(RepositoryItem item) : base(item)
        {
        }
    }

    public class MediColorEditPainter : ColorEditPainter
    {
        public MediColorEditPainter()
        {
        }
    }

    public class MediColorEditPopupForm : PopupColorEditForm
    {
        public MediColorEditPopupForm(MediColorEdit ownerEdit) : base(ownerEdit)
        {
        }
    }
}