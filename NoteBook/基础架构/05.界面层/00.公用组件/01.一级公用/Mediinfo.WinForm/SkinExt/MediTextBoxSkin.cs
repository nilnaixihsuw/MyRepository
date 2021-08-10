using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

using System;
using System.Drawing;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// TextBox自定义皮肤扩展类
    /// </summary>
    public static class MediTextBoxExt
    {
        /// <summary>
        /// 设置TextBox的自定义皮肤信息
        /// </summary>
        /// <param name="edit"></param>
        public static void SetCustomSkin(this RepositoryItemTextEdit edit)
        {
            if (ControlCommonHelper.IsDesignMode())
                return;

            try
            {
                string skinElementName = "TextBox";

                // 从皮肤中颜色信息
                Skin currentSkin = ControlCommonHelper.GetSkinInstance();
                SkinElement ele = currentSkin[skinElementName];

                if (null != ele)
                {
                    bool setBorderStyle = false;
                    Color skinColor = Color.Empty;

                    // 默认边框颜色
                    skinColor = ele.Properties.GetColor("BorderColor");
                    if (skinColor != Color.Empty)
                    {
                        edit.Appearance.BorderColor = skinColor;
                        setBorderStyle = true;
                    }

                    // 得到焦点时边框的颜色
                    skinColor = ele.Properties.GetColor("FocusedBorderColor");
                    if (skinColor != Color.Empty)
                    {
                        edit.AppearanceFocused.BorderColor = skinColor;
                        setBorderStyle = true;
                    }

                    // 只读时边框颜色
                    skinColor = ele.Properties.GetColor("ReadOnlyBorderColor");
                    if (skinColor != Color.Empty)
                    {
                        edit.AppearanceReadOnly.BorderColor = skinColor;
                        edit.AppearanceReadOnly.BackColor = ele.Properties.GetColor("ReadOnlyBackColor");
                        edit.AppearanceReadOnly.ForeColor = ele.Properties.GetColor("ReadOnlyForeColor");
                        setBorderStyle = true;
                    }

                    // Disable时边框颜色
                    skinColor = ele.Properties.GetColor("DisabledBorderColor");
                    if (skinColor != Color.Empty)
                    {
                        edit.AppearanceDisabled.BorderColor = skinColor;
                        setBorderStyle = true;
                    }

                    if (setBorderStyle)
                        edit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
                }
            }
            catch
            {
                throw new Exception("皮肤获取异常");
            }
        }

        /// <summary>
        /// 设置TextBox的自定义皮肤信息
        /// </summary>
        /// <param name="editor"></param>
        public static void SetEditorsCustomSkin(this BaseEdit editor)
        {
            if (ControlCommonHelper.IsDesignMode())
                return;

            try
            {
                string skinElementName = "TextBox";
                bool setBorderStyle = false;
                if (ControlCommonHelper.IsExistColorKey(skinElementName, "BorderColor"))
                {
                    editor.Properties.Appearance.BorderColor = ControlCommonHelper.GetColorValue(skinElementName, "BorderColor");
                    setBorderStyle = true;
                }
                else
                {
                    // 从皮肤中颜色信息
                    Skin currentSkin = ControlCommonHelper.GetSkinInstance();
                    SkinElement ele = currentSkin[skinElementName];
                    if (null != ele)
                    {
                        Color skinColor = Color.Empty;

                        // 默认边框颜色
                        skinColor = ele.Properties.GetColor("BorderColor");
                        if (skinColor != Color.Empty)
                        {
                            editor.Properties.Appearance.BorderColor = skinColor;
                            setBorderStyle = true;
                            ControlCommonHelper.SetColor("TextBox", "BorderColor", skinColor);
                        }
                    }
                }

                if (ControlCommonHelper.IsExistColorKey(skinElementName, "FocusedBorderColor"))
                {
                    editor.Properties.AppearanceFocused.BorderColor = ControlCommonHelper.GetColorValue(skinElementName, "FocusedBorderColor");
                    setBorderStyle = true;
                }
                else
                {
                    // 从皮肤中颜色信息
                    Skin currentSkin = ControlCommonHelper.GetSkinInstance();
                    SkinElement ele = currentSkin[skinElementName];
                    if (null != ele)
                    {
                        Color skinColor = Color.Empty;

                        // 默认边框颜色
                        skinColor = ele.Properties.GetColor("FocusedBorderColor");
                        if (skinColor != Color.Empty)
                        {
                            editor.Properties.AppearanceFocused.BorderColor = skinColor;
                            setBorderStyle = true;
                            ControlCommonHelper.SetColor("TextBox", "FocusedBorderColor", skinColor);
                        }
                    }
                }

                if (ControlCommonHelper.IsExistColorKey(skinElementName, "ReadOnlyBorderColor"))
                {
                    editor.Properties.AppearanceReadOnly.BorderColor = ControlCommonHelper.GetColorValue(skinElementName, "ReadOnlyBorderColor");
                    editor.Properties.AppearanceReadOnly.BackColor = ControlCommonHelper.GetColorValue(skinElementName, "ReadOnlyBackColor");
                    editor.Properties.AppearanceReadOnly.ForeColor = ControlCommonHelper.GetColorValue(skinElementName, "ReadOnlyForeColor");
                    setBorderStyle = true;
                }
                else
                {
                    // 从皮肤中颜色信息
                    Skin currentSkin = ControlCommonHelper.GetSkinInstance();
                    SkinElement ele = currentSkin[skinElementName];
                    if (null != ele)
                    {
                        Color skinReadOnlyBorderColor = Color.Empty;
                        Color skinReadOnlyBackColor = Color.Empty;
                        Color skinReadOnlyForeColor = Color.Empty;
                        // 默认边框颜色
                        skinReadOnlyBorderColor = ele.Properties.GetColor("ReadOnlyBorderColor");
                        skinReadOnlyBackColor = ele.Properties.GetColor("ReadOnlyBackColor");
                        skinReadOnlyForeColor = ele.Properties.GetColor("ReadOnlyForeColor");

                        if (skinReadOnlyBorderColor != Color.Empty)
                        {
                            editor.Properties.AppearanceReadOnly.BorderColor = skinReadOnlyBorderColor;
                            setBorderStyle = true;
                            ControlCommonHelper.SetColor("TextBox", "ReadOnlyBorderColor", skinReadOnlyBorderColor);
                        }

                        if (skinReadOnlyBackColor != Color.Empty)
                        {
                            editor.Properties.AppearanceReadOnly.BackColor = skinReadOnlyBackColor;
                            ControlCommonHelper.SetColor("TextBox", "ReadOnlyBackColor", skinReadOnlyBackColor);
                        }

                        if (skinReadOnlyForeColor != Color.Empty)
                        {
                            editor.Properties.AppearanceReadOnly.ForeColor = skinReadOnlyForeColor;
                            ControlCommonHelper.SetColor("TextBox", "ReadOnlyForeColor", skinReadOnlyForeColor);
                        }
                    }
                }

                //if (ControlCommonHelper.IsExistColorKey(skinElementName, "DisabledBorderColor"))
                //{
                //    editor.Properties.AppearanceDisabled.BorderColor = ControlCommonHelper.GetColorValue(skinElementName, "DisabledBorderColor");
                //    editor.Properties.AppearanceDisabled.BackColor = ControlCommonHelper.GetColorValue(skinElementName, "DisabledBackColor");
                //    editor.Properties.AppearanceDisabled.ForeColor = ControlCommonHelper.GetColorValue(skinElementName, "DisabledForeColor");
                //    setBorderStyle = true;
                //}
                //else
                //{
                //    //从皮肤中颜色信息


                //    DevExpress.Skins.Skin currentSkin = ControlCommonHelper.GetSkinInstance();
                //    SkinElement ele = currentSkin[skinElementName];
                //    if (null != ele)
                //    {
                //        Color skinDisabledBorderColor = Color.Empty;
                //        Color skinDisabledBackColor = Color.Empty;
                //        Color skinDisabledForeColor = Color.Empty;

                //        //默认边框颜色
                //        skinDisabledBorderColor = ele.Properties.GetColor("DisabledBorderColor");

                //        skinDisabledBackColor = ele.Properties.GetColor("DisabledBackColor");
                //        skinDisabledForeColor = ele.Properties.GetColor("DisabledForeColor");

                //        if (skinDisabledBorderColor != Color.Empty)
                //        {
                //            editor.Properties.AppearanceDisabled.BorderColor = skinDisabledBorderColor;
                //            setBorderStyle = true;
                //            ControlCommonHelper.SetColor("TextBox", "DisabledBorderColor", skinDisabledBorderColor);


                //        }

                //        if (skinDisabledBackColor != Color.Empty)
                //        {
                //            editor.Properties.AppearanceDisabled.BackColor = skinDisabledBackColor;
                //            ControlCommonHelper.SetColor("TextBox", "DisabledBackColor", skinDisabledBackColor);
                //        }
                //        if (skinDisabledForeColor != Color.Empty)
                //        {
                //            editor.Properties.AppearanceDisabled.ForeColor = skinDisabledForeColor;
                //            ControlCommonHelper.SetColor("TextBox", "DisabledForeColor", skinDisabledForeColor);
                //        }
                //    }
                //}

                if (setBorderStyle)
                    editor.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
                editor.Size = new Size(editor.Size.Width, 26);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}