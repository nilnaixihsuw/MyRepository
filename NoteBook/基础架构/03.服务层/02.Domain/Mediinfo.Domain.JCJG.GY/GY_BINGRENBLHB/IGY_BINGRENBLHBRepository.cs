using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_BINGRENBLHBRepository : IRepository<GY_BINGRENBLHB>, IDependency
	{

        List<GY_BINGRENBLHB> GetList(string YuanbingAH);
	}
}
