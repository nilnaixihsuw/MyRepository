using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
    public interface IGY_DAYINJKRepository : IRepository<GY_DAYINJK>, IDependency
    {
        /// <summary>
        /// 获取打印接口
        /// </summary>
        /// <param name="jiuZhenID"></param>
        /// <param name="laiYuanID"></param>
        /// <returns></returns>
        List<GY_DAYINJK> GetMZList(int laiYuanLX, string jiuZhenID, string laiYuanID);
    }
}
