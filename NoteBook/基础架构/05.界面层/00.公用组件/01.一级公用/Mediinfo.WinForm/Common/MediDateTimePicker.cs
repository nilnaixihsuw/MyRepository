using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace Mediinfo.WinForm.Common
{
    /// <summary>
    /// 显示年月日时分秒控件
    /// </summary>
    [UserRepositoryItem("RegisterMediDateTimePicker")]
    public class RepositoryItemMediDateTimePicker : RepositoryItemDateEdit, IExpressionInterface, IInputIMEMode
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

        static RepositoryItemMediDateTimePicker()
        {
            RegisterMediDateTimePicker();
        }

        public const string CustomEditName = "MediDateTimePicker";

        public RepositoryItemMediDateTimePicker()
        {
            this.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            EditMask = "yyyy-MM-dd HH:mm:ss";
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

        private bool readOnly = false;

        public override string EditorTypeName => CustomEditName;

        /// <summary>
        /// 输入法模式
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        public MediInfoImeMode MediinfoIMEMode { get; set; }

        public static void RegisterMediDateTimePicker()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediDateTimePicker), typeof(RepositoryItemMediDateTimePicker), typeof(MediDateTimePickerViewInfo), new MediDateTimePickerPainter(), true, img));
        }

        protected override FormatInfo CreateDisplayFormat()
        {
            return new MediDateTimePickerFormatInfo();
        }

        protected override FormatInfo CreateEditFormat()
        {
            return new MediDateTimePickerFormatInfo();
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemMediDateTimePicker source = item as RepositoryItemMediDateTimePicker;
                if (source == null) return;
            }
            finally
            {
                EndUpdate();
            }
        }
    }

    public class MediDateTimePickerFormatInfo : DateEditFormatInfo
    {
        public MediDateTimePickerFormatInfo(IComponentLoading componentLoading) : base(componentLoading)
        {
            FormatType = FormatType.DateTime;
            FormatString = "yyyy-MM-dd HH:mm:ss";
        }

        public MediDateTimePickerFormatInfo() : base()
        {
            FormatType = FormatType.DateTime;
            FormatString = "yyyy-MM-dd HH:mm:ss";
        }
    }

    [ToolboxItem(true)]
    public class MediDateTimePicker : DateEdit, IInputIMEMode
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        static MediDateTimePicker()
        {
            RepositoryItemMediDateTimePicker.RegisterMediDateTimePicker();
        }

        public MediDateTimePicker()
        {
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject9 = new DevExpress.Utils.SerializableAppearanceObject();
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SetEditorsCustomSkin();
            this.MinimumSize = new Size(0, 26);
            this.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false,
            DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(ResourceImage.ico_dateEdit)),
            new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject9, "", null, null, true)});
            if (!ControlCommonHelper.IsDesignMode())
            {
                RegisterEvents();
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        /// <summary>
        /// 注册事件爱
        /// </summary>
        private void RegisterEvents()
        {
            this.Properties.TodayClick -= RepositoryItemMediDateTimePicker_TodayClick;
            this.Properties.TodayClick += RepositoryItemMediDateTimePicker_TodayClick;

        }

        private void RepositoryItemMediDateTimePicker_TodayClick(object sender, EventArgs e)
        {
            this.ClosePopup();
            this.EditValue = DateTime.Now;
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (!this.Enabled)
            {
                this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));

                this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
                this.Enabled = false;
            }
            else
            {
                this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));

                this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                this.Enabled = true;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMediDateTimePicker Properties
        {
            get
            {
                return base.Properties as RepositoryItemMediDateTimePicker;
            }
        }

        public override string EditorTypeName
        {
            get
            {
                return RepositoryItemMediDateTimePicker.CustomEditName;
            }
        }

        /// <summary>
        /// 输入法模式
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        public MediInfoImeMode MediinfoIMEMode { get; set; }

        protected override PopupBaseForm CreatePopupForm()
        {
            return new MediDateTimePickerPopupForm(this);
        }
    }

    public class MediDateTimePickerViewInfo : DateEditViewInfo
    {
        public MediDateTimePickerViewInfo(RepositoryItem item) : base(item)
        {
        }
    }

    public class MediDateTimePickerPainter : ButtonEditPainter
    {
        public MediDateTimePickerPainter()
        {
        }
    }

    public class MediDateTimePickerPopupForm : PopupDateEditForm
    {
        public MediDateTimePickerPopupForm(MediDateTimePicker ownerEdit) : base(ownerEdit)
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