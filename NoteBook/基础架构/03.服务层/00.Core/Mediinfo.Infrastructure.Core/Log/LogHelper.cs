﻿using System;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Mediinfo.Infrastructure.Core.Log
{
    /// <summary>
    /// 日志
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        public static void WriteLog(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error("Error", ex);
        }
        
        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        public static void WriteLog(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error(msg);
        }
    }
}