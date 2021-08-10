namespace Mediinfo.Enterprise.Exceptions
{
    /// <summary>
    /// DTO异常
    /// </summary>
    public class DTOException : BaseException
    {
        #region constructor

        /// <summary>
        /// DTO异常
        /// </summary>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="errorCode">错误代码(默认值: ReturnCode.DTOERROR)</param>
        public DTOException(string errorMessage, ReturnCode errorCode = ReturnCode.DTOERROR)
            : base(errorCode, errorMessage)
        {
            
        }

        #endregion
    }
}