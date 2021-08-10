namespace Mediinfo.Enterprise.Exceptions
{
    /// <summary>
    /// 运维平台异常
    /// </summary>
    public class CloudException : BaseException
    {
        #region constructor

        /// <summary>
        /// 运维平台异常
        /// </summary>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="errorCode">错误代码(默认值: ReturnCode.CLOUDERROR)</param>
        public CloudException(string errorMessage, ReturnCode errorCode = ReturnCode.CLOUDERROR)
            : base(errorCode, errorMessage)
        {
            
        }

        #endregion
    }
}
