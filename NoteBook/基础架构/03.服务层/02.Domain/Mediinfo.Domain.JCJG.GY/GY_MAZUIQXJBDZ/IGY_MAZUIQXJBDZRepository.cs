using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_MAZUIQXJBDZRepository : IRepository<GY_MAZUIQXJBDZ>, IDependency
	{
        List<GY_MAZUIQXJBDZ> GetMaZuiQX(string prmZhiChengID, string prmKeShiID);

    }
}
