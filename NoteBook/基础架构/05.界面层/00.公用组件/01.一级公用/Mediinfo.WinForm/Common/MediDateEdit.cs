using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 显示年月日的控件
    /// </summary>
    [UserRepositoryItem("RegisterMediDateEdit")]
    public class RepositoryItemMediDateEdit : RepositoryItemDateEdit, IExpressionInterface, IInputIMEMode
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
        
        private bool onlyYearMonth = false;

        /// <summary>
        /// 仅显示年月控件
        /// </summary>
        [Browsable(true), Category(""), Description("仅显示年月"), DefaultValue(false)]
        public bool OnlyYearMonth
        {
            get
            {
                return onlyYearMonth;
            }
            set
            {
                onlyYearMonth = value;
            }
        }

        static RepositoryItemMediDateEdit()
        {
            RegisterMediDateEdit();
        }

        public const string CustomEditName = "MediDateEdit";

        public RepositoryItemMediDateEdit()
        {
            this.Mask.MaskType = MaskType.DateTimeAdvancingCaret;
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        protected override MaskProperties CreateMaskProperties()
        {
            DateEditMaskProperties maskProperties = new DateEditMaskProperties();
            maskProperties.MaskType = MaskType.DateTimeAdvancingCaret;
            return maskProperties;
        }

        private bool readOnly = false;

        public override string EditorTypeName => CustomEditName;

        /// <summary>
        /// 输入法模式
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        public MediInfoImeMode MediinfoIMEMode { get; set; }

        public static void RegisterMediDateEdit()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediDateEdit), typeof(RepositoryItemMediDateEdit), typeof(MediDateEditViewInfo), new MediDateEditPainter(), true, img));
        }

        protected override FormatInfo CreateDisplayFormat()
        {
            return new MediDateEditFormatInfo();
        }

        protected override FormatInfo CreateEditFormat()
        {
            return new MediDateEditFormatInfo();
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemMediDateEdit source = item as RepositoryItemMediDateEdit;
                if (source == null) return;
            }
            finally
            {
                EndUpdate();
            }
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CalendarTimeProperties)).BeginInit();
            this.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.RepositoryItemMediDateEdit_EditValueChanging);
            ((System.ComponentModel.ISupportInitialize)(this.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        }

        private void RepositoryItemMediDateEdit_EditValueChanging(object sender, ChangingEventArgs e)
        {
        }
    }

    public class MediDateEditFormatInfo : DateEditFormatInfo
    {
        public MediDateEditFormatInfo(IComponentLoading componentLoading) : base(componentLoading)
        {
            FormatType = FormatType.DateTime;
            FormatString = "yyyy-MM-dd";
        }

        public MediDateEditFormatInfo() : base()
        {
            FormatType = FormatType.DateTime;
            FormatString = "yyyy-MM-dd";
        }
    }

    /// <summary>
    /// 显示年月日的控件
    /// </summary>
    [ToolboxItem(true)]
    public class MediDateEdit : DateEdit, IInputIMEMode
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        static MediDateEdit()
        {
            RepositoryItemMediDateEdit.RegisterMediDateEdit();
        }

        public MediDateEdit()
        {
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            SerializableAppearanceObject serializableAppearanceObject9 = new SerializableAppearanceObject();
            this.SetEditorsCustomSkin();
            this.Properties.Mask.MaskType = MaskType.DateTimeAdvancingCaret;
            this.MinimumSize = new Size(0, 26);
            this.SetEditorsCustomSkin();
            EditorButton editorButton = new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false,
            ImageLocation.MiddleCenter, ((Image)(ResourceImage.ico_dateEdit)),
            new KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject9, "", null, null, true);
            this.Properties.Buttons.AddRange(new EditorButton[] { editorButton });
            editorButton.Click += EditorButton_Click;
            
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        private void EditorButton_Click(object sender, EventArgs e)
        {
            if (this.IsPopupOpen)
                this.ClosePopup();
            else
                this.ShowPopup();
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

                this.BackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                this.Enabled = true;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMediDateEdit Properties => base.Properties as RepositoryItemMediDateEdit;

        protected override void OnEditValueChanged()
        {
            if (!this.Properties.OnlyYearMonth)
            {
                this.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";

                this.Properties.DisplayFormat.FormatType = FormatType.DateTime;
                this.Properties.Mask.EditMask = "yyyy-MM-dd";
            }
            base.OnEditValueChanged();
        }

        public override string EditorTypeName => RepositoryItemMediDateEdit.CustomEditName;

        /// <summary>
        /// 输入法模式
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        public MediInfoImeMode MediinfoIMEMode { get; set; }

        protected override PopupBaseForm CreatePopupForm()
        {
            return new MediDateEditPopupForm(this);
        }
    }

    public class MediDateEditViewInfo : DateEditViewInfo
    {
        public MediDateEditViewInfo(RepositoryItem item) : base(item)
        {
        }
    }

    public class MediDateEditPainter : ButtonEditPainter
    {
        public MediDateEditPainter()
        {
        }
    }

    public class MediDateEditPopupForm : PopupDateEditForm
    {
        public MediDateEditPopupForm(MediDateEdit ownerEdit) : base(ownerEdit)
        {
        }

        protected override CalendarControl CreateCalendar()
        {
            return new MediDateEditCalendar();
        }
    }

    public class MediDateEditCalendar : PopupCalendarControl
    {
        public MediDateEditCalendar()
        {
        }
    }
}