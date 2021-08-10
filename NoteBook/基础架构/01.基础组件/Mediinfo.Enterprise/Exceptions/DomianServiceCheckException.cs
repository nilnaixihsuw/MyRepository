namespace Mediinfo.Enterprise.Exceptions
{
    /// <summary>
    /// DomianService校验异常
    /// </summary>
    public class DomianServiceCheckException : DomainServiceException
    {
        #region constructor

        /// <summary>
        /// DomianService校验异常
        /// </summary>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="errorCode">错误代码(默认值: ReturnCode.DOMAINSERVICEERROR)</param>
        public DomianServiceCheckException(string errorMessage, ReturnCode errorCode = ReturnCode.DOMAINSERVICEERROR)
            : base(errorMessage, errorCode)
        {
            
        }

        #endregion
    }
}
