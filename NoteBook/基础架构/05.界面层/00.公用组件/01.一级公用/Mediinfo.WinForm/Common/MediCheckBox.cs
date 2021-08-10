using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;

using Mediinfo.HIS.Core;

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 允许最终用户在两个（已选择和未未选择）或三个（已选择，未选择和不确定）状态之间进行选择。(网格中使用)
    /// </summary>
    [UserRepositoryItem("RegisterMediCheckBox")]
    public class RepositoryItemMediCheckBox : RepositoryItemCheckEdit, IExpressionInterface
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

        static RepositoryItemMediCheckBox()
        {
            RegisterMediCheckBox();
        }

        public const string CustomEditName = "MediCheckBox";

        public RepositoryItemMediCheckBox()
        {
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
        }

        private Size checkBoxSize = new Size(15, 15);

        /// <summary>
        /// 设置复选框大小属性
        /// </summary>
        [Description("改变复选框大小")]
        public Size CheckBoxSize
        {
            get
            {
                return checkBoxSize;
            }
            set
            {
                if (checkBoxSize != value)
                {
                    if (!string.IsNullOrWhiteSpace(HISClientHelper.XITONGID) && !HISClientHelper.LINCHUANGBZ.Equals(0))
                    {

                    }
                    else
                    {
                        checkBoxSize = value;
                        SkinElement element = SkinManager.GetSkinElement(SkinProductId.Editors, DevExpress.LookAndFeel.UserLookAndFeel.Default, EditorsSkins.SkinCheckBox);
                        element.Size.MinSize = checkBoxSize;
                        element.Image.Stretch = SkinImageStretch.Stretch;
                        LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
                    }
                }
            }
        }

        public override string EditorTypeName => CustomEditName;

        public static void RegisterMediCheckBox()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(MediCheckBox), typeof(RepositoryItemMediCheckBox), typeof(MediCheckBoxViewInfo), new MediCheckBoxPainter(), true, img));
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemMediCheckBox source = item as RepositoryItemMediCheckBox;
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
            // RepositoryItemMediCheckBox
            // 
            this.LookAndFeel.SkinName = "MediSkinDevExpressStyle";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
    }

    /// <summary>
    /// 允许最终用户在两个（已选择和未未选择）或三个（已选择，未选择和不确定）状态之间进行选择。
    /// </summary>
    [ToolboxItem(true)]
    public class MediCheckBox : CheckEdit
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        static MediCheckBox()
        {
            RepositoryItemMediCheckBox.RegisterMediCheckBox();
        }

        private Size checkBoxSize = new Size(15, 15);

        /// <summary>
        /// 设置复选框大小属性
        /// </summary>
        [Description("改变复选框大小")]
        public Size CheckBoxSize
        {
            get
            {
                return checkBoxSize;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(HISClientHelper.XITONGID) &&! HISClientHelper.LINCHUANGBZ.Equals(0))
                {

                }
                else
                {
                    checkBoxSize = value;
                    SkinElement element = SkinManager.GetSkinElement(SkinProductId.Editors, DevExpress.LookAndFeel.UserLookAndFeel.Default, EditorsSkins.SkinCheckBox);
                    element.Size.MinSize = checkBoxSize;
                    element.Image.Stretch = SkinImageStretch.Stretch;
                    LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
                }
            }
        }

        public MediCheckBox()
        {
            this.Cursor = System.Windows.Forms.Cursors.Hand;
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
        public new RepositoryItemMediCheckBox Properties
        {
            get
            {
                return base.Properties as RepositoryItemMediCheckBox;
            }
        }

        public override string EditorTypeName
        {
            get
            {
                return RepositoryItemMediCheckBox.CustomEditName;
            }
        }
    }

    public class MediCheckBoxViewInfo : CheckEditViewInfo
    {
        public MediCheckBoxViewInfo(RepositoryItem item) : base(item)
        {
        }
    }

    public class MediCheckBoxPainter : CheckEditPainter
    {
        public MediCheckBoxPainter()
        {
        }

        protected virtual void DrawCheck(ControlGraphicsInfoArgs info)
        {
            base.DrawCheck(info);
        }
    }
}