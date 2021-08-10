namespace Mediinfo.ServiceProxy.Core
{
    /// <summary>
    /// 服务端内部错误
    /// </summary>
    public class InternalServerErrorMessage
    {
        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 详细消息
        /// </summary>
        public string ExceptionMessage { get; set; }
    }
}
