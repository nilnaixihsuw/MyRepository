namespace Mediinfo.Enterprise.Exceptions
{
    /// <summary>
    /// Domain Service层异常基类（业务异常）
    /// </summary>
    public class DomainServiceException : BaseException
    {
        #region constructor

        /// <summary>
        /// Domain Service层异常基类(业务异常)
        /// </summary>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="errorCode">错误代码(默认值: ReturnCode.DOMAINSERVICEERROR)</param>
        public DomainServiceException(string errorMessage, ReturnCode errorCode = ReturnCode.DOMAINSERVICEERROR)
            : base(errorCode, errorMessage)
        {
            
        }

        #endregion
    }
}