namespace Mediinfo.Enterprise.Exceptions
{
    /// <summary>
    /// 插件异常
    /// </summary>
    public class PluginsException : BaseException
    {
        #region constructor

        /// <summary>
        /// 插件异常
        /// </summary>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="errorCode">错误代码(默认值: ReturnCode.PLUGINSERROR)</param>
        public PluginsException(string errorMessage, ReturnCode errorCode = ReturnCode.PLUGINSERROR)
            : base(errorCode, errorMessage)
        {
            
        }

        #endregion
    }
}
