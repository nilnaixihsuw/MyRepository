using DevExpress.Skins;
using DevExpress.XtraEditors.Repository;

using System.Drawing;

namespace Mediinfo.WinForm
{
    public static class MediSearchControlSkin
    {
        /// <summary>
        /// 设置SearchControl的自定义皮肤信息
        /// </summary>
        /// <param name="edit"></param>
        public static void SetCustomSkin(this RepositoryItemSearchControl edit)
        {
            if (ControlCommonHelper.IsDesignMode())
                return;

            try
            {
                string skinElementName = "TextBox";

                //从皮肤中颜色信息
                Skin currentSkin = ControlCommonHelper.GetSkinInstance();
                SkinElement ele = currentSkin[skinElementName];

                if (null != ele)
                {
                    bool setBorderStyle = false;
                    Color skinColor = Color.Empty;

                    //默认边框颜色
                    skinColor = ele.Properties.GetColor("BorderColor");
                    if (skinColor != Color.Empty)
                    {
                        edit.Appearance.BorderColor = skinColor;
                        setBorderStyle = true;
                    }

                    //得到焦点时边框的颜色
                    skinColor = ele.Properties.GetColor("FocusedBorderColor");
                    if (skinColor != Color.Empty)
                    {
                        edit.AppearanceFocused.BorderColor = skinColor;
                        setBorderStyle = true;
                    }

                    //只读时边框颜色
                    skinColor = ele.Properties.GetColor("ReadOnlyBorderColor");
                    if (skinColor != Color.Empty)
                    {
                        edit.AppearanceReadOnly.BorderColor = skinColor;
                        edit.AppearanceReadOnly.BackColor = ele.Properties.GetColor("ReadOnlyBackColor");
                        edit.AppearanceReadOnly.ForeColor = ele.Properties.GetColor("ReadOnlyForeColor");
                        setBorderStyle = true;
                    }

                    //Disable时边框颜色
                    skinColor = ele.Properties.GetColor("DisabledBorderColor");
                    if (skinColor != Color.Empty)
                    {
                        edit.AppearanceDisabled.BorderColor = skinColor;
                        edit.AppearanceDisabled.BackColor = ele.Properties.GetColor("DisabledBackColor");
                        edit.AppearanceDisabled.ForeColor = ele.Properties.GetColor("DisabledForeColor");
                        setBorderStyle = true;
                    }

                    if (setBorderStyle)
                        edit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
                }
            }
            catch
            {
                throw new System.Exception("皮肤获取异常");
            }
        }
    }
}