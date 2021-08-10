using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_YUANQURepository : IRepository<GY_YUANQU>, IDependency
	{
        /// <summary>
        /// 取院区
        /// </summary>
        /// <returns></returns>
        List<GY_YUANQU> GetList();

        /// <summary>
        /// 生成条码前缀
        /// </summary>
        /// <param name="yuanQuID"></param>
        /// <param name="menZhenZYBZ"></param>
        /// <returns></returns>
        string TiaoMaQZ(string yuanQuID, int menZhenZYBZ);

        List<GY_YUANQU> GetList(string yuanQuID);
    }
}
