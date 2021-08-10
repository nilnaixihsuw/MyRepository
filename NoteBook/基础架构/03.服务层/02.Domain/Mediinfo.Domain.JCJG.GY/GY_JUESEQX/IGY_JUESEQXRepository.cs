using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_JUESEQXRepository : IRepository<GY_JUESEQX>, IDependency
	{
        GY_JUESEQX GetByID(string jueseid, string quanxianid);
        /// <summary>
        ///added by xyz for   HR3-45056(390395) 是否具有管理员权限
        /// </summary>
        /// <param name="zhiGongId">职工id</param>
        /// <param name="quanXianID">权限id</param>
        /// <returns></returns>
        int GetByZhiGongID(string zhiGongId, string quanXianID);
    }
}
