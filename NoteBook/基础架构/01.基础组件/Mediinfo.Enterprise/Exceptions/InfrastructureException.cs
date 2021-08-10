namespace Mediinfo.Enterprise.Exceptions
{
    /// <summary>
    /// Infrastructure层异常
    /// </summary>
    public class InfrastructureException : BaseException
    {
        #region constructor

        /// <summary>
        /// Infrastructure层异常
        /// </summary>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="errorCode">错误代码(默认值: ReturnCode.INFRASTRUCTUREERROR)</param>
        public InfrastructureException(string errorMessage, ReturnCode errorCode = ReturnCode.INFRASTRUCTUREERROR)
            : base(errorCode, errorMessage)
        {
            
        }

        #endregion
    }
}
