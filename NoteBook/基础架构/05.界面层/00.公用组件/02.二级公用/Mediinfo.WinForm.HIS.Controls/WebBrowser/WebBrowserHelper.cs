using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 浏览器帮助类
    /// </summary>
    public class WebBrowserHelper
    {
        /// <summary>
        /// 获取浏览器名称
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultWebBrowserName()
        {
            string defaultbrowserpath = string.Empty;
            string defaultbrowsername = string.Empty;
            switch (OSHelper.GetOSType())
            {
                case OSHelper.OSVersionNo.WindowsXP:
                case OSHelper.OSVersionNo.Windows7:
                    defaultbrowserpath = GetSystemDefaultBrowser();
                    defaultbrowsername = string.Empty;
                    if (!string.IsNullOrWhiteSpace(defaultbrowserpath))
                    {
                        defaultbrowsername = defaultbrowserpath.Substring(defaultbrowserpath.LastIndexOf("\\") + 1);
                    }
                    return defaultbrowsername;
                case OSHelper.OSVersionNo.Windows8:
                case OSHelper.OSVersionNo.Windows10:
                    defaultbrowserpath = GetSystemDefaultBrowserForWin10();
                    defaultbrowsername = string.Empty;
                    if (!string.IsNullOrWhiteSpace(defaultbrowserpath))
                    {
                        defaultbrowsername = defaultbrowserpath.Substring(defaultbrowserpath.LastIndexOf("\\") + 1);
                    }
                    return defaultbrowsername;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取xp、win7默认浏览器名称
        /// </summary>
        /// <returns></returns>
        private static string GetSystemDefaultBrowser()
        {
            string name = string.Empty;
            RegistryKey regKey = null;

            try
            {
                regKey = Registry.ClassesRoot.OpenSubKey("HTTP\\shell\\open\\command", false);
                name = regKey.GetValue(null).ToString().ToLower().Replace("" + (char)34, "");
                if (!name.EndsWith("exe"))
                    name = name.Substring(0, name.LastIndexOf(".exe") + 4);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (regKey != null)
                    regKey.Close();
            }
            return name;

        }

        /// <summary>
        /// 获取win10默认浏览器
        /// </summary>
        /// <returns></returns>
        private static string GetSystemDefaultBrowserForWin10()
        {
            string name = string.Empty;
            RegistryKey regKey = null;
            try
            {
                var regDefault = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\.htm\\UserChoice", false);
                var stringDefault = regDefault.GetValue("ProgId");
                regKey = Registry.ClassesRoot.OpenSubKey(stringDefault + "\\shell\\open\\command", false);
                name = regKey.GetValue(null).ToString().ToLower().Replace("" + (char)34, "");
                if (!name.EndsWith("exe"))
                    name = name.Substring(0, name.LastIndexOf(".exe") + 4);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (regKey != null)
                    regKey.Close();
            }
            return name;
        }
    }
}
