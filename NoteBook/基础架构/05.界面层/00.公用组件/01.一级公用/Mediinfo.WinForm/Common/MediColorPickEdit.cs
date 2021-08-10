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
    [UserRepositoryItem("RegisterMediColorPickEdit")]
    public class RepositoryItemMediColorPickEdit : RepositoryItemColorPickEdit, IExpressionInterface, IInputIMEMode
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

        static RepositoryItemMediColorPickEdit()
        {
            RegisterMediColorPickEdit();
        }

        public const string CustomEditName = "MediColorPickEdit";

        public RepositoryItemMediColorPickEdit()
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

        public static void RegisterMediColorPickEdit()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediColorPickEdit), typeof(RepositoryItemMediColorPickEdit), typeof(MediColorPickEditViewInfo), new MediColorPickEditPainter(), true, img));
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemMediColorPickEdit source = item as RepositoryItemMediColorPickEdit;
                if (source == null) return;
                //
                this.StoreColorAsInteger = true;
            }
            finally
            {
                EndUpdate();
            }
        }
    }

    [ToolboxItem(true)]
    public class MediColorPickEdit : ColorPickEdit, IInputIMEMode
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        static MediColorPickEdit()
        {
            RepositoryItemMediColorPickEdit.RegisterMediColorPickEdit();
        }

        /// <summary>
        /// 是否全选文本
        /// </summary>
        [Description("是否全选文本"), DefaultValue(true), Browsable(true)]
        public bool FocusAllText { get; set; } = true;

        public MediColorPickEdit()
        {
            this.GotFocus -= MediColorPickEdit_GotFocus;
            this.GotFocus += MediColorPickEdit_GotFocus;
            this.MouseUp -= MediColorPickEdit_MouseUp;
            this.MouseUp += MediColorPickEdit_MouseUp;
            this.Enter -= MediColorPickEdit_Enter;
            this.Enter += MediColorPickEdit_Enter;
            this.MouseDown -= MediColorPickEdit_MouseDown;
            this.MouseDown += MediColorPickEdit_MouseDown;
            this.MinimumSize = new Size(0, 26);

            this.SetEditorsCustomSkin();
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        private void MediColorPickEdit_Enter(object sender, EventArgs e)
        {
            enter = true;
            BeginInvoke(new MethodInvoker(ResetEnterFlag));
        }

        private void MediColorPickEdit_MouseDown(object sender, MouseEventArgs e)
        {
            needSelect = enter;
        }

        private bool enter = false, needSelect = false;
        private void ResetEnterFlag()
        {
            enter = false;
        }

        private void MediColorPickEdit_MouseUp(object sender, MouseEventArgs e)
        {
            if (needSelect)
            {
                if (this.Properties.TextEditStyle == DevExpress.XtraEditors.Controls.TextEditStyles.Standard)
                {
                    (sender as TextEdit).SelectAll();
                }
            }
        }

        private void MediColorPickEdit_GotFocus(object sender, System.EventArgs e)
        {
            if (FocusAllText)
            {
                if (this.Properties.TextEditStyle == DevExpress.XtraEditors.Controls.TextEditStyles.Standard)
                {
                    (sender as TextEdit).SelectAll();
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]

        public new RepositoryItemMediColorPickEdit Properties => base.Properties as RepositoryItemMediColorPickEdit;

        public override string EditorTypeName => RepositoryItemMediColorPickEdit.CustomEditName;

        /// <summary>
        /// 输入法模式
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        public MediInfoImeMode MediinfoIMEMode { get; set; }

        protected override PopupBaseForm CreatePopupForm()
        {
            return new MediColorPickEditPopupForm(this);
        }
    }

    public class MediColorPickEditViewInfo : ColorEditViewInfo
    {
        public MediColorPickEditViewInfo(RepositoryItem item) : base(item)
        {
        }
    }

    public class MediColorPickEditPainter : ColorEditPainter
    {
        public MediColorPickEditPainter()
        {
        }
    }

    public class MediColorPickEditPopupForm : PopupColorPickEditForm
    {
        public MediColorPickEditPopupForm(MediColorPickEdit ownerEdit) : base(ownerEdit)
        {
        }
    }
}