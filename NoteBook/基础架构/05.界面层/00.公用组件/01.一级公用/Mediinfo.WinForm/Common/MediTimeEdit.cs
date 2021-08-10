using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace Mediinfo.WinForm.Common
{
    [ToolboxItem(true)]
    public class MediTimeEdit : DevExpress.XtraEditors.TimeEdit, IInputIMEMode
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        /// <summary>
        /// 输入法模式
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        public MediInfoImeMode MediinfoIMEMode { get; set; }

        public MediTimeEdit()
        {
            //if (ControlCommonHelper.IsDesignMode())
            //    this.Font = new Font("微软雅黑", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Properties.DisplayFormat.FormatString = "T";
            this.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.SetEditorsCustomSkin();
            this.MinimumSize = new Size(0, 26);
            // this.Size = new System.Drawing.Size(100, 26);
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
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

                this.BackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                this.Enabled = true;
            }
        }
    }

    [UserRepositoryItem("RegisterMediTimeEdit")]
    public class MediRepositoryItemTimeEdit : DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit, IInputIMEMode
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        static MediRepositoryItemTimeEdit()
        {
            RegisterMediTimeEdit();
        }

        public const string CustomEditName = "MediTimeEdit";

        public MediRepositoryItemTimeEdit()
        {
            this.DisplayFormat.FormatString = "T";
            this.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.Mask.MaskType = MaskType.DateTimeAdvancingCaret;
            //  this.SetCustomSkin();
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        private void MediRepositoryItemTimeEdit_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            this.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
        }

        private bool readOnly = false;

        protected override MaskProperties CreateMaskProperties()
        {
            return new MediTimeEditMaskProperties();
        }

        public override string EditorTypeName => CustomEditName;

        /// <summary>
        /// 输入法模式
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        public MediInfoImeMode MediinfoIMEMode { get; set; }

        public static void RegisterMediTimeEdit()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediTimeEdit), typeof(MediRepositoryItemTimeEdit), typeof(MediTimeEditViewInfo), new MediTimeEditPainter(), true, EditImageIndexes.TimeEdit, typeof(DevExpress.Accessibility.BaseSpinEditAccessible)));
        }
    }

    public class MediTimeEditPainter : ButtonEditPainter
    {
        public MediTimeEditPainter()
        {
        }
    }

    public class MediTimeEditViewInfo : TimeEditViewInfo
    {
        public MediTimeEditViewInfo(RepositoryItem item) : base(item)
        {
        }
    }

    public class MediTimeEditMaskProperties : TimeEditMaskProperties
    {
        public MediTimeEditMaskProperties() : base()
        {
            this.fEditMask = "T";
        }

        [CategoryAttribute("Mask"),
        DefaultValue("T"),
        Localizable(true),
        RefreshProperties(RefreshProperties.All),
        ]
        public override string EditMask
        {
            get { return base.EditMask; }
            set { base.EditMask = value; }
        }
    }
}