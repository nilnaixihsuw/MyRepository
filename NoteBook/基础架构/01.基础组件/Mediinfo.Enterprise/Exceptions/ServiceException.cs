namespace Mediinfo.Enterprise.Exceptions
{
    /// <summary>
    /// 服务异常
    /// </summary>
    public class ServiceException : BaseException
    {
        #region constructor

        /// <summary>
        /// 服务异常
        /// </summary>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="errorCode">错误代码(默认值: ReturnCode.SERVICEERROR)</param>
        public ServiceException(string errorMessage, ReturnCode errorCode = ReturnCode.SERVICEERROR)
            : base(errorCode, errorMessage)
        {
            
        }

        #endregion
    }
}
