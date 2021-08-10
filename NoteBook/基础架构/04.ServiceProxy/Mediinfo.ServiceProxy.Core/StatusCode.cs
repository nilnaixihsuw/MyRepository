namespace Mediinfo.ServiceProxy.Core
{
    /// <summary>
    /// 请求状态
    /// </summary>
    public enum StatusCode
    {
        Success = 200,
        RequestError = 400,
        AuthorizationFailed = 401,
        InternalServerError = 500,
    }
}
