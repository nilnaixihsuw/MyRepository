using DevExpress.LookAndFeel;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Drawing;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 控件公共帮助类
    /// </summary>
    public static class ControlCommonHelper
    {
        private static DevExpress.Skins.Skin currentSkin = DevExpress.Skins.EditorsSkins.GetSkin(UserLookAndFeel.Default);

        /// <summary>
        /// 获取皮肤对象实例
        /// </summary>
        /// <returns></returns>
        public static DevExpress.Skins.Skin GetSkinInstance()
        {
            if (currentSkin != null)
            {
                return currentSkin;
            }
            else
            {
                return DevExpress.Skins.EditorsSkins.GetSkin(UserLookAndFeel.Default);
            }
        }

        /// <summary>
        /// 是否处于设计时(用于在扩展控件中使用)
        /// </summary>
        public static bool IsDesignMode()
        {
            return SkinCat.Instance.IsDesignMode;
        }

        private static ConcurrentDictionary<string, Color> textBoxDic = new ConcurrentDictionary<string, Color>();

        /// <summary>
        /// 设置控件样式缓存
        /// </summary>
        /// <param name="controlStyleName"></param>
        /// <param name="appreance"></param>
        /// <param name="color"></param>
        public static void SetColor(string controlStyleName, string appreance, Color color)
        {
            switch (controlStyleName)
            {
                case "TextBox":
                    if (!textBoxDic.ContainsKey(appreance))
                    {
                        textBoxDic.TryAdd(appreance, color);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        ///判断字典集合是否包含该样式
        /// </summary>
        /// <param name="controlStyleName"></param>
        /// <param name="appreance"></param>
        /// <returns></returns>
        public static bool IsExistColorKey(string controlStyleName, string appreance)
        {
            switch (controlStyleName)
            {
                case "TextBox":
                    if (textBoxDic.ContainsKey(appreance))
                        return true;
                    return false;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 获取颜色值
        /// </summary>
        /// <param name="controlStyleName"></param>
        /// <param name="appreance"></param>
        /// <returns></returns>
        public static Color GetColorValue(string controlStyleName, string appreance)
        {
            switch (controlStyleName)
            {
                case "TextBox":
                    if (textBoxDic.ContainsKey(appreance))
                    {
                        return textBoxDic[appreance];
                    }
                    return SystemColors.Control;

                default:
                    return SystemColors.Control;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controlStyleName"></param>
        /// <returns></returns>
        public static int GetCustomStyleCount(string controlStyleName)
        {
            switch (controlStyleName)
            {
                case "TextBox":
                    return textBoxDic.Count;
                default:
                    return 0;
            }
        }
    }

    internal class SkinCat : System.ComponentModel.Component
    {
        /// <summary>  
        /// 是否处于设计器模式  
        /// </summary>  
        private bool isDesignMode = false;

        /// <summary>  
        /// 唯一实例  
        /// </summary>  
        private static SkinCat instance = null;

        /// <summary>  
        /// 创建一个新的SkinCat对象  
        /// </summary>  
        private SkinCat()
        {
        }

        /// <summary>  
        /// 获取SkinCat唯一对象  
        /// </summary>  
        internal static SkinCat Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SkinCat();
                    instance.isDesignMode = instance.GetIsDesignMode();
                }
                return instance;
            }
        }

        /// <summary>  
        /// 获取是否处于设计器模式  
        /// </summary>  
        internal bool IsDesignMode
        {
            get
            {
                return isDesignMode;
            }
        }

        /// <summary>  
        /// 获取当前是否处于设计器模式  
        /// </summary>  
        /// <remarks>  
        /// 在程序初始化时获取一次比较准确，若需要时获取可能由于布局嵌套导致获取不正确，如GridControl-GridView组合。  
        /// </remarks>  
        /// <returns>是否为设计器模式</returns>  
        private bool GetIsDesignMode()
        {
            return (this.GetService(typeof(System.ComponentModel.Design.IDesignerHost)) != null
                || LicenseManager.UsageMode == LicenseUsageMode.Designtime || System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper().Equals("DEVENV"));
        }
    }
}
