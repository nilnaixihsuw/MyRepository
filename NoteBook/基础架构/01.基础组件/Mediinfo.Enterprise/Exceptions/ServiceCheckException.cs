namespace Mediinfo.Enterprise.Exceptions
{
    /// <summary>
    /// 服务校验异常
    /// </summary>
    public class ServiceCheckException : ServiceException
    {
        #region constructor

        /// <summary>
        /// 服务校验异常
        /// </summary>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="errorCode">错误代码(默认值: ReturnCode.REPOSITORYERROR)</param>
        public ServiceCheckException(string errorMessage, ReturnCode errorCode = ReturnCode.SERVICEERROR)
            : base(errorMessage, errorCode)
        {
            
        }

        #endregion
    }
}
