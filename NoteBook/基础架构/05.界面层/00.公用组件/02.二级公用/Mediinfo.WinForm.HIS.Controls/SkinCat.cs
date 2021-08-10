﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>  
    /// SkinCat  
    /// </summary>  
    public class SkinCat : System.ComponentModel.Component
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
        public static SkinCat Instance
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
        public bool IsDesignMode
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
