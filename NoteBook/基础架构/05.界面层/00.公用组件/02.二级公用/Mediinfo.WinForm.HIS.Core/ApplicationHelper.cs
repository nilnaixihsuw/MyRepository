using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Core
{
    /// <summary>
    /// 应用程序帮助类
    /// </summary>
    public static class ApplicationHelper
    {
        /// <summary>
        /// 获取当前应用启动路径
        /// </summary>
        public static string GetCurrentStartPath()
        {
            return Application.StartupPath;
        }

        /// <summary>
        /// 获取上一级文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetParentPath()
        {
            DirectoryInfo startPathInfo = new DirectoryInfo(GetCurrentStartPath());
            return startPathInfo.Parent.FullName;
        }

        /// <summary>
        /// 相对路径获取
        /// </summary>
        /// <param name="absolutePath">文件路径</param>
        /// <returns></returns>
        public static string RelativePath(string absolutePath)
        {
            string[] absoluteDirectories = absolutePath.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            string[] relativeDirectories = GetCurrentStartPath().Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            int length = absoluteDirectories.Length < relativeDirectories.Length ? absoluteDirectories.Length : relativeDirectories.Length;
            int lastCommonRoot = -1;
            int index;
            for (index = 0; index < length; index++)
                if (absoluteDirectories[index] == relativeDirectories[index])
                    lastCommonRoot = index;
                else
                    break;
            if (lastCommonRoot == -1)
                throw new ArgumentException("文件路径和启动程序文件路径不匹配，无法生成虚拟路径!");
            StringBuilder relativePath = new StringBuilder();
            for (index = lastCommonRoot + 1; index < relativeDirectories.Length; index++)
                if (relativeDirectories[index].Length > 0)
                    relativePath.Append("..\\");
            for (index = lastCommonRoot + 1; index < absoluteDirectories.Length - 1; index++)
                relativePath.Append(absoluteDirectories[index] + "\\");
            relativePath.Append(absoluteDirectories[absoluteDirectories.Length - 1]);

            return relativePath.ToString();
        }

        /// <summary>
        /// 存储应用程序中的某个操作的状态，例如读卡初始化
        /// </summary>
        private static Dictionary<string, string> ApplicationStaticDic = new Dictionary<string, string>();

        /// <summary>
        /// 设置应用程序的对应对象状态值
        /// </summary>
        /// <param name="staticName">状态对象名</param>
        /// <param name="staticValue">状态值</param>
        public static void SetStatic(string staticName, string staticValue)
        {
            if (ApplicationStaticDic.ContainsKey(staticName))
            {
                ApplicationStaticDic["staticName"] = staticValue;
            }
            else
            {

            }
        }

        /// <summary>
        /// 获取应用程序的对应对象状态值
        /// </summary>
        /// <param name="staticName">状态对象名</param>
        /// <param name="staticValue">状态值</param>
        /// <returns></returns>
        public static bool GetStatic(string staticName, ref string staticValue)
        {
            if (ApplicationStaticDic.ContainsKey(staticName))
            {
                staticValue = ApplicationStaticDic["staticName"];
                return true;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// 是否处于设计时(用于在扩展控件中使用)
        /// </summary>
        public static bool IsDesignMode()
        {
            bool returnFlag = false;

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                returnFlag = true;
            }
            else if (Process.GetCurrentProcess().ProcessName == "devenv")
            {
                returnFlag = true;
            }

            return returnFlag;
        }
        
        /// <summary>
        /// 事件集合
        /// </summary>
        public static List<PropertyChangedEventHandler> PropertyChangedEventHandlers = new List<PropertyChangedEventHandler>();
    }
}
