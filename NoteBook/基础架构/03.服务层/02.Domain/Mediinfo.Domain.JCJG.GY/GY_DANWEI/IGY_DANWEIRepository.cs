using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_DANWEIRepository : IRepository<GY_DANWEI>, IDependency
	{
        /// <summary>
        /// 根据单位ID，应用ID获取单位数据
        /// </summary>
        /// <param name="yyid"></param>
        /// <param name="dwid"></param>
        /// <returns></returns>
        List<GY_DANWEI> GetList(string yingYongID, string danWeiID);
    }
}
