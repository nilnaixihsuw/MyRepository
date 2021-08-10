using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.ViewInfo;

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 蓝色按钮类(主要用于查询按钮)
    /// </summary>
    [ToolboxItem(true)]
    public class MediBlueButton : SimpleButton
    {
        private ImageLocation imageLocation;
        private Image image;
        private int imageIndex;
        private object imageList;
        private bool allowFocus;
        private DefaultBoolean allowHtmlDraw, showFocusRectangle;

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
        /// 构造函数
        /// </summary>
        public MediBlueButton()
        {
            this.allowFocus = true;
            this.allowHtmlDraw = DefaultBoolean.Default;
            this.imageLocation = ImageLocation.Default;
            this.image = null;
            this.imageIndex = -1;
            this.imageList = null;
            this.AutoSizeInLayoutControl = false;
            this.showFocusRectangle = DefaultBoolean.Default;
            this.Cursor = Cursors.Hand;

            this.Size = new System.Drawing.Size(70, 26);
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (developerHelper == null)
                {
                    developerHelper = new SystemInfoHelper();
                }
                developerHelper.DealRelativeControl(this);
            }
            this.LookAndFeel.SkinName = "MediSkinDevExpressStyle";

        }

        /// <summary>
        /// 创建视图信息
        /// </summary>
        /// <returns></returns>
        protected override BaseStyleControlViewInfo CreateViewInfo()
        {
            return new MediButtonViewInfo(this);
        }
    }

    public class MediButtonViewInfo : SimpleButtonViewInfo
    {
        public MediButtonViewInfo(SimpleButton owner) : base(owner)
        {

        }

        protected override Font GetDefaultFont()
        {
            if (!IsSkinLookAndFeel) return base.GetDefaultFont();
            return GetDefaultSkinFont("BlueButton");
        }

        protected override Color GetForeColor()
        {
            if (!IsSkinLookAndFeel)
            {
                return GetSystemColor(SystemColors.ControlText);
            }
            SkinElement element = CommonSkins.GetSkin(LookAndFeel)["BlueButton"];
            if (element == null)
                element = CommonSkins.GetSkin(LookAndFeel)["Button"];
            return element.GetForeColor(State);
        }

        protected override EditorButtonPainter GetButtonPainter()
        {
            return new MediSkinEditorButtonPainter(OwnerControl.LookAndFeel);
        }
    }

    public class MediSkinEditorButtonPainter : SkinEditorButtonPainter
    {
        public MediSkinEditorButtonPainter(ISkinProvider provider) : base(provider)
        {

        }

        protected override SkinElementInfo CreateSkinElementInfo()
        {
            SkinElement element = SkinManager.GetSkinElement(SkinProductId.Common, DevExpress.LookAndFeel.UserLookAndFeel.Default, "BlueButton");
            if (element == null)
                element = SkinManager.GetSkinElement(SkinProductId.Common, DevExpress.LookAndFeel.UserLookAndFeel.Default, "Button");
            return new SkinElementInfo(element);
        }

        protected override SkinElement GetSkinElement(EditorButtonObjectInfoArgs e, ButtonPredefines kind)
        {
            return CommonSkins.GetSkin(Provider)["BlueButton"] ?? CommonSkins.GetSkin(Provider)["Button"];
        }
    }
}

