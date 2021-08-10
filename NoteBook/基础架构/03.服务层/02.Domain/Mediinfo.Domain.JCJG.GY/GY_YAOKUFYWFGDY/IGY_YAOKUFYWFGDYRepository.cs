using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_YAOKUFYWFGDYRepository : IRepository<GY_YAOKUFYWFGDY>, IDependency
	{
        List<GY_YAOKUFYWFGDY> GetList(string yingYongID, string muBiaoZhi);

    }
}
