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
    /// 下拉框控件(网格中使用)
    /// 主要用于绑定集合对象
    /// </summary>
    [UserRepositoryItem("RegisterMediComboBox")]
    public class RepositoryItemMediComboBox : RepositoryItemComboBox, IExpressionInterface, IInputIMEMode
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

        static RepositoryItemMediComboBox()
        {
            RegisterMediComboBox();
        }

        public const string CustomEditName = "MediComboBox";

        /// <summary>
        /// 通过皮肤获取控件信息
        /// </summary>
        protected virtual void Init()
        {
            AllowMouseWheel = false;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public RepositoryItemMediComboBox()
        {
            Init();
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        private bool readOnly = false;

        public override string EditorTypeName => CustomEditName;

        /// <summary>
        /// 输入法模式
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        public MediInfoImeMode MediinfoIMEMode { get; set; }

        public static void RegisterMediComboBox()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediComboBox), typeof(RepositoryItemMediComboBox), typeof(MediComboBoxViewInfo), new MediComboBoxPainter(), true, img));
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemMediComboBox source = item as RepositoryItemMediComboBox;
                if (source == null) return;
            }
            finally
            {
                EndUpdate();
            }
        }
    }

    /// <summary>
    /// 下拉框控件
    /// 主要用于绑定集合对象
    /// </summary>
    [ToolboxItem(true)]
    public class MediComboBox : ComboBoxEdit, IExpressionInterface, IInputIMEMode
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        [Browsable(true)]
        [Editor(typeof(UnboundExpressionEditor), typeof(UITypeEditor))]
        [Category("UnboundExpression"), Description("控件自定义表达式,返回值为当前控件的值或者属性或者其他控件的值或属性"), DefaultValue("")]
        public string UnboundExpression { get; set; } = string.Empty;

        static MediComboBox()
        {
            RepositoryItemMediComboBox.RegisterMediComboBox();
        }

        /// <summary>
        /// 是否全选文本
        /// </summary>
        [Description("是否全选文本"), DefaultValue(true), Browsable(true)]
        public bool FocusAllText { get; set; } = true;

        public MediComboBox()
        {
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GotFocus -= MediComboBox_GotFocus;
            this.GotFocus += MediComboBox_GotFocus;
            this.MouseUp -= MediComboBox_MouseUp;
            this.MouseUp += MediComboBox_MouseUp;
            this.Enter -= MediComboBox_Enter;
            this.Enter += MediComboBox_Enter;
            this.MouseDown -= MediComboBox_MouseDown;
            this.MouseDown += MediComboBox_MouseDown;
            this.SetEditorsCustomSkin();
            this.EnterMoveNextControl = true;
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

        private void MediComboBox_Enter(object sender, EventArgs e)
        {
            enter = true;
            BeginInvoke(new MethodInvoker(ResetEnterFlag));
        }

        private void MediComboBox_MouseDown(object sender, MouseEventArgs e)
        {
            needSelect = enter;
        }

        private bool enter = false, needSelect = false;
        private void ResetEnterFlag()
        {
            enter = false;
        }

        private void MediComboBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (needSelect)
            {
                if (this.Properties.TextEditStyle == DevExpress.XtraEditors.Controls.TextEditStyles.Standard)
                {
                    (sender as TextEdit).SelectAll();
                }
            }
        }

        private void MediComboBox_GotFocus(object sender, EventArgs e)
        {
            if (FocusAllText)
            {
                if (this.Properties.TextEditStyle == DevExpress.XtraEditors.Controls.TextEditStyles.Standard)
                {
                    (sender as TextEdit).SelectAll();
                }
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (!this.Enabled)
            {
                this.ForeColor = Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));

                this.BackColor = Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
                this.Enabled = false;
            }
            else
            {
                this.ForeColor = Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));

                this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                this.Enabled = true;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMediComboBox Properties => base.Properties as RepositoryItemMediComboBox;

        public override string EditorTypeName => RepositoryItemMediComboBox.CustomEditName;

        /// <summary>
        /// 输入法模式
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        public MediInfoImeMode MediinfoIMEMode { get; set; }

        protected override PopupBaseForm CreatePopupForm()
        {
            return new MediComboBoxPopupForm(this);
        }
    }

    public class MediComboBoxViewInfo : ComboBoxViewInfo
    {
        public MediComboBoxViewInfo(RepositoryItem item) : base(item)
        {
        }
    }

    public class MediComboBoxPainter : ButtonEditPainter
    {
        public MediComboBoxPainter()
        {
        }
    }

    public class MediComboBoxPopupForm : ComboBoxPopupListBoxForm
    {
        public MediComboBoxPopupForm(MediComboBox ownerEdit) : base(ownerEdit)
        {
        }
    }
}