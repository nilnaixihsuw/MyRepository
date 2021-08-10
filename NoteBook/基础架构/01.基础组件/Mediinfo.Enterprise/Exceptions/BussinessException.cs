namespace Mediinfo.Enterprise.Exceptions
{
    /// <summary>
    /// 业务异常
    /// </summary>
    public class BussinessException : BaseException
    {
        #region constructor

        /// <summary>
        /// 业务异常
        /// </summary>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="errorCode">错误代码(默认值: ReturnCode.BUSSINESS)</param>
        /// <param name="level">异常级别(默认: 1(轻量级))</param>
        public BussinessException(string errorMessage, ReturnCode errorCode = ReturnCode.BUSSINESS, int level = 1)
            : base(errorCode, errorMessage)
        {
            this.Level = level;
        }

        #endregion
    }
}
