using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace Mediinfo.WinForm.Common
{
    [UserRepositoryItem("RegisterMediRadioGroup")]
    public class RepositoryItemMediRadioGroup : RepositoryItemRadioGroup, IExpressionInterface
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

        static RepositoryItemMediRadioGroup()
        {
            RegisterMediRadioGroup();
        }

        public const string CustomEditName = "MediRadioGroup";

        /// <summary>
        /// 构造函数
        /// </summary>
        public RepositoryItemMediRadioGroup()
        {
            InitializeComponent();

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

        [Browsable(true)]
        public override bool ReadOnly
        {
            get
            {
                return readOnly;
            }
            set
            {
                readOnly = value;
                if (!readOnly)
                {
                }
                else
                {
                    if (ControlCommonHelper.IsDesignMode())
                    {
                        this.AppearanceReadOnly.ForeColor = Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
                        this.AppearanceReadOnly.Options.UseForeColor = true;
                        this.AppearanceReadOnly.BackColor = Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
                        this.AppearanceReadOnly.Options.UseBackColor = true;
                    }
                }
            }
        }

        public override string EditorTypeName => CustomEditName;

        public static void RegisterMediRadioGroup()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediRadioGroup), typeof(RepositoryItemMediRadioGroup), typeof(MediRadioGroupViewInfo), new MediRadioGroupPainter(), true, img));
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemMediRadioGroup source = item as RepositoryItemMediRadioGroup;
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
            //
            // RepositoryItemMediRadioGroup
            //
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        }
    }

    [ToolboxItem(true)]
    public class MediRadioGroup : RadioGroup
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        static MediRadioGroup()
        {
            RepositoryItemMediRadioGroup.RegisterMediRadioGroup();
        }

        public MediRadioGroup()
        {
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMediRadioGroup Properties => base.Properties as RepositoryItemMediRadioGroup;

        public override string EditorTypeName => RepositoryItemMediRadioGroup.CustomEditName;

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (ControlCommonHelper.IsDesignMode())
            {
                if (!this.Enabled)
                {
                    this.Properties.AppearanceDisabled.ForeColor = Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
                    this.Properties.AppearanceDisabled.Options.UseForeColor = true;
                    this.Properties.AppearanceDisabled.BackColor = Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
                    this.Properties.AppearanceDisabled.Options.UseBackColor = true;
                    this.Enabled = false;
                }
            }
        }
    }

    public class MediRadioGroupViewInfo : RadioGroupViewInfo
    {
        public MediRadioGroupViewInfo(RepositoryItem item) : base(item)
        {
        }
    }

    public class MediRadioGroupPainter : RadioGroupPainter
    {
        public MediRadioGroupPainter()
        {
        }
    }
}