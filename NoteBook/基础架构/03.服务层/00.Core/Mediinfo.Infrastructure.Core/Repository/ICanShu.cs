namespace Mediinfo.Infrastructure.Core.Repository
{
    /// <summary>
    /// 参数接口
    /// </summary>
    public interface ICanShu
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="yingyongId">应用Id</param>
        /// <param name="canShuId">参数Id</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回：参数值</returns>
        string GetCanShu(string yingyongId, string canShuId, string defaultValue);

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="yingyongId">应用ID</param>
        /// <param name="canShuId">参数ID</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="realtime">是否实时取数据库</param>
        /// <returns>返回：参数值</returns>
        string GetCanShu(string yingyongId, string canShuId, string defaultValue, bool realtime);
    }
}
