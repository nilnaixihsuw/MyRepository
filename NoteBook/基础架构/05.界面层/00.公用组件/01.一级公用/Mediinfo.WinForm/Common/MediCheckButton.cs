using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 选择按钮
    /// </summary>
    [ToolboxItem(true)]
    public class MediCheckButton: DevExpress.XtraEditors.CheckButton
    {
        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false)]
        public IDeveloperHelper developerHelper { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MediCheckButton()
        {
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
            return new MediCheckButtonViewInfo(this);
        }
    }
    public class MediCheckButtonViewInfo : CheckButtonViewInfo
    {
        public MediCheckButtonViewInfo(CheckButton owner) : base(owner)
        {

        }

        protected override Font GetDefaultFont()
        {
            if (!IsSkinLookAndFeel) return base.GetDefaultFont();
            return GetDefaultSkinFont("CheckButton");
        }

        protected override Color GetForeColor()
        {
            if (!IsSkinLookAndFeel)
            {
                return GetSystemColor(SystemColors.ControlText);
            }
            SkinElement element = CommonSkins.GetSkin(LookAndFeel)["CheckButton"];
            if (element == null)
                element = CommonSkins.GetSkin(LookAndFeel)["Button"];
            return element.GetForeColor(State);
        }

        protected override EditorButtonPainter GetButtonPainter()
        {
            return new MediCheckSkinEditorButtonPainter(OwnerControl.LookAndFeel);
        }
    }

    public class MediCheckSkinEditorButtonPainter : SkinEditorButtonPainter
    {
        public MediCheckSkinEditorButtonPainter(ISkinProvider provider) : base(provider)
        {

        }

        protected override SkinElementInfo CreateSkinElementInfo()
        {
            SkinElement element = SkinManager.GetSkinElement(SkinProductId.Common, DevExpress.LookAndFeel.UserLookAndFeel.Default, "CheckButton");
            if (element == null)
                element = SkinManager.GetSkinElement(SkinProductId.Common, DevExpress.LookAndFeel.UserLookAndFeel.Default, "Button");
            return new SkinElementInfo(element);
        }

        protected override SkinElement GetSkinElement(EditorButtonObjectInfoArgs e, ButtonPredefines kind)
        {
            return CommonSkins.GetSkin(Provider)["CheckButton"] ?? CommonSkins.GetSkin(Provider)["Button"];
        }
    }
}
