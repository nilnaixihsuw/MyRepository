using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_CHUANGWEIZU2Repository : IRepository<GY_CHUANGWEIZU2>, IDependency
	{
        int Update(string sqlString);
	}
}
