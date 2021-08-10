using System;

namespace Mediinfo.Utility
{
    /// <summary>
    /// 日志信息分级别操作
    /// </summary>
    public class LogHelper
    {
        NLog.Logger logger;
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="logger"></param>
        private LogHelper(NLog.Logger logger)
        {
            this.logger = logger;
        }

        public LogHelper(string name)
            : this(NLog.LogManager.GetLogger(name))
        {

        }

        public static LogHelper Default { get; private set; }

        static LogHelper()
        {
            Default = new LogHelper(NLog.LogManager.GetCurrentClassLogger());
        }

        public void Debug(string msg, params object[] args)
        {
            logger.Debug(msg, args);
        }

        public void Debug(string msg, Exception err)
        {
            logger.Debug(err, msg);
        }

        public void Info(string msg, params object[] args)
        {
            logger.Info(msg, args);
        }

        public void Info(string msg, Exception err)
        {
            logger.Info(err, msg);
        }

        public void Trace(string msg, params object[] args)
        {
            logger.Trace(msg, args);
        }

        public void Trace(string msg, Exception err)
        {
            logger.Trace(err, msg);
        }

        public void Error(string msg, params object[] args)
        {
            logger.Error(msg, args);
        }

        public void Error(string msg, Exception err)
        {
            logger.Error(err, msg);
        }

        public void Fatal(string msg, params object[] args)
        {
            logger.Fatal(msg, args);
        }

        public void Fatal(string msg, Exception err)
        {
            logger.Fatal(err, msg);
        }
    }
}
