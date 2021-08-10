using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.ViewInfo;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 普通按钮类
    /// </summary>
    [ToolboxItem(true)]
    public partial class MediButton : SimpleButton, IExpressionInterface
    {
        #region constructor

        /// <summary>
        /// 构造函数
        /// </summary>
        public MediButton()
        {
            this.Appearance.Options.UseTextOptions = true;
            this.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.LookAndFeel.SkinName = "MediSkinDevExpressStyle";
            this.Size = new Size(70, 26);
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

        #endregion

        #region fields

        private ButtonType medibuttonStyle = ButtonType.Default;

        #endregion

        #region properties

        /// <summary>
        /// 开发者帮助接口
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IDeveloperHelper developerHelper { get; set; }

        /// <summary>
        /// 表达式属性
        /// </summary>
        [Browsable(true)]
        [Editor(typeof(UnboundExpressionEditor), typeof(UITypeEditor))]
        [Category("UnboundExpression"), Description("控件自定义表达式,返回值为当前控件的值或者属性或者其他控件的值或属性"), DefaultValue("")]
        public string UnboundExpression { get; set; }

        /// <summary>
        /// 按钮类型
        /// </summary>
        [DefaultValue(0), Browsable(true)]
        public ButtonType MediButtonStyle
        {
            get { return medibuttonStyle; }
            set
            {
                medibuttonStyle = value;
                if (ControlCommonHelper.IsDesignMode())
                    return;
                if (medibuttonStyle == ButtonType.TabButton)
                {
                    this.Appearance.ForeColor = Color.FromArgb(0, 115, 195);
                    this.AppearanceHovered.ForeColor = Color.White;
                    this.AppearanceDisabled.ForeColor = Color.LightGray;
                    this.AppearancePressed.ForeColor = Color.White;
                }
                this.Refresh();
            }
        }

        #endregion

        #region override

        /// <summary>
        /// 获取焦点
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            if (ControlCommonHelper.IsDesignMode())
                return;

            if (this.MediButtonStyle == ButtonType.TabButton)
                this.Appearance.ForeColor = Color.White;
        }

        /// <summary>
        /// 失去焦点
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            if (ControlCommonHelper.IsDesignMode())
                return;
        }

        /// <summary>
        /// 创建视图信息
        /// </summary>
        /// <returns></returns>
        protected override BaseStyleControlViewInfo CreateViewInfo()
        {
            return new MediCustomButtonViewInfo(this);
        }

        #endregion
    }

    /// <summary>
    /// 搜索蓝色按钮
    /// </summary>
    [ToolboxItem(true)]
    public class MediSearchBuleButton : MediButton
    {
        #region constructor

        /// <summary>
        /// 搜索蓝色按钮
        /// </summary>
        public MediSearchBuleButton()
        {
            this.AutoSize = true;
            this.ButtonStyle = BorderStyles.NoBorder;
            this.ImageOptions.Image = Properties.Resources.search_button;
            this.Text = "";

            this.MouseHover -= new EventHandler(MediSearchBuleButton_MouseHover);
            this.MouseHover += new EventHandler(MediSearchBuleButton_MouseHover);
            this.MouseLeave -= new EventHandler(MediSearchBuleButton_MouseLeave);
            this.MouseLeave += new EventHandler(MediSearchBuleButton_MouseLeave);
        }

        #endregion

        #region override

        /// <summary>
        /// 按钮文本
        /// </summary>
        public override string Text { get => ""; set => base.Text = ""; }

        #endregion

        #region private events

        /// <summary>
        /// 鼠标移入时触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediSearchBuleButton_MouseHover(object sender, EventArgs e)
        {
            this.ImageOptions.Image = Properties.Resources.search_button_hover;
        }

        /// <summary>
        /// 鼠标移出时触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediSearchBuleButton_MouseLeave(object sender, EventArgs e)
        {
            this.ImageOptions.Image = Properties.Resources.search_button;
        }

        #endregion
    }

    /// <summary>
    /// 自定义按钮视图信息
    /// </summary>
    public class MediCustomButtonViewInfo : SimpleButtonViewInfo
    {
        #region constructor

        public MediCustomButtonViewInfo(SimpleButton owner) : base(owner)
        {
            if (owner is MediButton)
                mediButton = (MediButton)owner;
        }

        #endregion

        #region fields

        private string defaultSkinName = "Button";
        private string skinName = "Button";
        public SkinElement element;
        public MediButton mediButton;

        #endregion

        #region override

        protected override Font GetDefaultFont()
        {
            if (!IsSkinLookAndFeel) return base.GetDefaultFont();
            return GetDefaultSkinFont(defaultSkinName);
        }

        protected override Color GetForeColor()
        {
            if (!IsSkinLookAndFeel)
            {
                return GetSystemColor(SystemColors.ControlText);
            }

            if (element == null)
                element = CommonSkins.GetSkin(LookAndFeel)[defaultSkinName];
            return element.GetForeColor(State);
        }

        protected override void UpdateButton(EditorButtonObjectInfoArgs buttonInfo)
        {
            base.UpdateButton(buttonInfo);
            if (buttonInfo.State != DevExpress.Utils.Drawing.ObjectState.Pressed && buttonInfo.State != DevExpress.Utils.Drawing.ObjectState.Disabled && buttonInfo.State != DevExpress.Utils.Drawing.ObjectState.Selected && mediButton.MediButtonStyle == ButtonType.TabButton)
            {
                buttonInfo.Appearance.ForeColor = Color.FromArgb(0, 115, 195);
            }
        }

        protected override EditorButtonPainter GetButtonPainter()
        {
            if (!ControlCommonHelper.IsDesignMode() && mediButton != null)
            {
                switch (mediButton.MediButtonStyle)
                {
                    case ButtonType.Default:
                        skinName = "DefaultButton";
                        element = CommonSkins.GetSkin(LookAndFeel)[skinName];
                        break;
                    case ButtonType.BlueButton:
                        skinName = "BlueButton";
                        element = CommonSkins.GetSkin(LookAndFeel)[skinName];
                        break;
                    case ButtonType.BlueBorderButton:
                        skinName = "MenuButton";
                        element = CommonSkins.GetSkin(LookAndFeel)[skinName];
                        break;
                    case ButtonType.TabButton:
                        skinName = "TabButton";
                        element = CommonSkins.GetSkin(LookAndFeel)[skinName];
                        break;
                    default:
                        element = CommonSkins.GetSkin(LookAndFeel)[skinName];
                        break;
                }
            }
            return new MediCustomSkinEditorButtonPainter(OwnerControl.LookAndFeel);
        }

        #endregion
    }

    public class MediCustomSkinEditorButtonPainter : SkinEditorButtonPainter
    {
        #region constructor

        public MediCustomSkinEditorButtonPainter(ISkinProvider provider) : base(provider)
        {
            if (!ControlCommonHelper.IsDesignMode())
            {
                if (((DevExpress.LookAndFeel.UserLookAndFeel)provider).OwnerControl is MediButton)
                {
                    var medibutton = ((DevExpress.LookAndFeel.UserLookAndFeel)provider).OwnerControl as MediButton;

                    switch (medibutton.MediButtonStyle)
                    {
                        case ButtonType.Default:
                            skinName = "DefaultButton";
                            SkinManager.GetSkinElement(SkinProductId.Common, DevExpress.LookAndFeel.UserLookAndFeel.Default, skinName);
                            break;
                        case ButtonType.BlueButton:
                            skinName = "BlueButton";
                            SkinManager.GetSkinElement(SkinProductId.Common, DevExpress.LookAndFeel.UserLookAndFeel.Default, skinName);
                            break;
                        case ButtonType.BlueBorderButton:
                            skinName = "MenuButton";
                            SkinManager.GetSkinElement(SkinProductId.Common, DevExpress.LookAndFeel.UserLookAndFeel.Default, skinName);
                            break;
                        case ButtonType.TabButton:
                            skinName = "TabButton";
                            SkinManager.GetSkinElement(SkinProductId.Common, DevExpress.LookAndFeel.UserLookAndFeel.Default, skinName);
                            break;
                        default:
                            SkinManager.GetSkinElement(SkinProductId.Common, DevExpress.LookAndFeel.UserLookAndFeel.Default, skinName);
                            break;
                    }
                }
            }
        }

        #endregion

        #region fields

        public string defaultSkinName = "Button";
        public string skinName = "Button";
        private SkinElement element;

        #endregion

        #region override

        protected override SkinElementInfo CreateSkinElementInfo()
        {
            if (element == null)
                element = SkinManager.GetSkinElement(SkinProductId.Common, DevExpress.LookAndFeel.UserLookAndFeel.Default, defaultSkinName);
            return new SkinElementInfo(element);
        }

        protected override SkinElement GetSkinElement(EditorButtonObjectInfoArgs e, ButtonPredefines kind)
        {
            return CommonSkins.GetSkin(Provider)[skinName] ?? CommonSkins.GetSkin(Provider)[defaultSkinName];
        }

        #endregion
    }

    /// <summary>
    /// 按钮类型
    /// </summary>
    public enum ButtonType
    {
        /// <summary>
        /// 默认按钮
        /// </summary>
        Default = 0,
        /// <summary>
        /// 蓝色按钮
        /// </summary>
        BlueButton = 1,
        /// <summary>
        /// 蓝色边框按钮
        /// </summary>
        BlueBorderButton = 2,
        /// <summary>
        /// 菜单tab按钮
        /// </summary>
        TabButton = 3
    }
}