namespace Mediinfo.Enterprise.Exceptions
{
    /// <summary>
    /// 客户端异常
    /// </summary>
    public class ClientException : BaseException
    {
        #region constructor

        /// <summary>
        /// 客户端异常
        /// </summary>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="errorCode">错误代码(默认值: ReturnCode.CLIENTERROR)</param>
        public ClientException(string errorMessage, ReturnCode errorCode = ReturnCode.CLIENTERROR)
            : base(errorCode, errorMessage)
        {

        }

        #endregion
    }
}