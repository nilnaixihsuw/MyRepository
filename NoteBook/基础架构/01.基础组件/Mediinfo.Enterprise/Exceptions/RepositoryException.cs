namespace Mediinfo.Enterprise.Exceptions
{
    /// <summary>
    /// Repository异常
    /// </summary>
    public class RepositoryException : BaseException
    {
        #region constructor

        /// <summary>
        /// Repository异常
        /// </summary>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="errorCode">错误代码(默认值: ReturnCode.REPOSITORYERROR)</param>
        public RepositoryException(string errorMessage, ReturnCode errorCode = ReturnCode.REPOSITORYERROR)
            : base(errorCode, errorMessage)
        {
            
        }

        #endregion
    }
}
