namespace Mediinfo.Enterprise.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        #region constructor

        /// <summary>
        /// 未授权异常
        /// </summary>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="errorCode">错误代码(默认值: ReturnCode.SERVICEERROR)</param>
        public UnauthorizedException(string errorMessage, ReturnCode errorCode = ReturnCode.UNAUTHORIZED)
            : base(errorCode, errorMessage)
        {

        }

        #endregion
    }
}
