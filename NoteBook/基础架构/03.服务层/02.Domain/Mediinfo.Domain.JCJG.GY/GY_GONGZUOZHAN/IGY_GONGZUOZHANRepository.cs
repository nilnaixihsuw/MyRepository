using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.Repository;

using System.Collections.Generic;

namespace Mediinfo.Domain.JCJG.GY
{
    public interface IGY_GONGZUOZHANRepository : IRepository<GY_GONGZUOZHAN>, IDependency
    {
        /// <summary>
        /// 获取工作站列表
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="moJiBz">末级标志</param>
        /// <returns></returns>
        List<GY_GONGZUOZHAN> GetList(string ip, int moJiBz);
    }
}
