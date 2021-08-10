using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_CHURUKFSRepository : IRepository<GY_CHURUKFS>, IDependency
	{
        void SetCache();

        List<GY_CHURUKFS> GetList(string yingYongID, string fangShiID);
        List<GY_CHURUKFS> GetListbydanweibm(string yingYongID, string churukbz, string danweibm);
        List<GY_CHURUKFS> GetListFromCache(string yingYongID, string fangShiID);

        /// <summary>
        /// 根据应用ID获取出入库方式
        /// </summary>
        /// <param name="yingYongID"></param>
        /// <returns></returns>
        List<GY_CHURUKFS> GetList(string yingYongID);
    }
}
