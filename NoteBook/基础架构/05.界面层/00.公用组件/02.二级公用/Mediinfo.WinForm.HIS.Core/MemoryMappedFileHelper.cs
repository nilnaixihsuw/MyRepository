using Mediinfo.Utility;

using System;
using System.Collections.Generic;
using System.IO;

namespace Mediinfo.WinForm.HIS.Core
{
    public static class MemoryMappedFileHelper
    {
        /// <summary>
        /// 创建共享数据
        /// </summary>
        /// <param name="yingYongId"></param>
        /// <param name="processId"></param>
        public static void CreateClipBoard(string yingYongId, string processId)
        {
            try
            {
                object yingYongIdObjects = ReadLoginInfoFromTemp();
                if (yingYongIdObjects != null)
                {
                    if (!string.IsNullOrWhiteSpace(yingYongIdObjects.ToString()))
                    {
                        if (!yingYongIdObjects.ToString().Contains(yingYongId))
                        {
                            WriteLoginInfoToTemp(yingYongIdObjects + yingYongId + ":" + processId + '|');
                        }
                    }
                    else
                    {
                        WriteLoginInfoToTemp(yingYongId + ":" + processId + '|');
                    }
                }
                else
                {
                    WriteLoginInfoToTemp(yingYongId + ":" + processId + '|');
                }
            }
            catch (Exception ex)
            {
                Enterprise.Log.LogHelper.Intance.Error("系统日志", ex.Message, ex.InnerException + ex.Message);
                // throw new Exception(ex.Message + ex.InnerException);
            }
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        public static void ClearClipBoardData()
        {
            WriteLoginInfoToTemp("");
        }

        /// <summary>
        /// 获取剪贴板数据
        /// </summary>
        public static List<string> GetClipBoardData()
        {
            try
            {
                object yingYongIdObjects = ReadLoginInfoFromTemp();
                if (yingYongIdObjects != null)
                {
                    if (!string.IsNullOrWhiteSpace(yingYongIdObjects.ToString()))
                    {
                        string[] yingyongids = yingYongIdObjects.ToString().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        List<string> list = new List<string>();

                        foreach (var item in yingyongids)
                            list.Add(item);
                        return list;
                    }
                    else
                    {
                        return new List<string>();
                    }
                }
                else
                {
                    return new List<string>();
                }
            }
            catch (Exception ex)
            {
                Enterprise.Log.LogHelper.Intance.Error("系统日志", ex.Message, ex.InnerException + ex.Message);
                // throw new Exception(ex.Message + ex.InnerException);
                return new List<string>();
            }
        }

        /// <summary>
        /// 移除共享数据
        /// </summary>
        /// <param name="yingYongId"></param>
        /// <param name="processId"></param>
        /// <returns></returns>
        public static bool RemoveClipBoardData(string yingYongId, string processId)
        {
            try
            {
                object yingYongIdObjects = ReadLoginInfoFromTemp();
                if (yingYongIdObjects != null)
                {
                    if (!string.IsNullOrWhiteSpace(yingYongIdObjects.ToString()))
                    {
                        string[] yingyongids = yingYongIdObjects.ToString().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        List<string> list = new List<string>();
                        string newYingYongIds = string.Empty;
                        foreach (string item in yingyongids)
                        {
                            if (!item.Equals(yingYongId + ":" + processId))
                            {
                                newYingYongIds += item + '|';
                            }
                        }

                        WriteLoginInfoToTemp(newYingYongIds);
                        return true;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Enterprise.Log.LogHelper.Intance.Error("系统日志", ex.Message, ex.InnerException + ex.Message);
                //throw new Exception(ex.Message + ex.InnerException);
                return false;
            }
        }

        /// <summary>
        /// 把应用信息存到临时文件中
        /// </summary>
        /// <param name="yingYongId"></param>
        public static bool WriteLoginInfoToTemp(string yingYongIdsAndProcessId)
        {
            try
            {
                string tempPath = Path.GetTempPath();
                string loginconfigInfo = tempPath + "YingYong.ini";
                if (File.Exists(loginconfigInfo))
                {
                    OperateIniFile operateFile = new OperateIniFile(loginconfigInfo);
                    operateFile.WriteString("YINGYONGDB", "MediInfoYingYongIds", yingYongIdsAndProcessId);
                    return true;
                }
                else
                {
                    File.Create(loginconfigInfo).Close();
                    OperateIniFile operateFile = new OperateIniFile(loginconfigInfo);
                    operateFile.WriteString("YINGYONGDB", "MediInfoYingYongIds", yingYongIdsAndProcessId);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Enterprise.Log.LogHelper.Intance.Error("系统日志", ex.Message, ex.InnerException + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 读取应用信息
        /// </summary>
        /// <returns></returns>
        public static string ReadLoginInfoFromTemp()
        {
            try
            {
                string tempPath = Path.GetTempPath();
                string loginconfigInfo = tempPath + "YingYong.ini";
                if (File.Exists(loginconfigInfo))
                {
                    OperateIniFile operateFile = new OperateIniFile(loginconfigInfo);
                    return operateFile.ReadString("YINGYONGDB", "MediInfoYingYongIds", "");
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                Enterprise.Log.LogHelper.Intance.Error("系统日志", ex.Message, ex.InnerException + ex.Message);
                return string.Empty;
            }
        }
    }
}