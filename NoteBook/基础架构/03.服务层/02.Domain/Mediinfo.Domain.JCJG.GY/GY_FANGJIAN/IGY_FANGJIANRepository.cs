using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_FANGJIANRepository : IRepository<GY_FANGJIAN>, IDependency
	{
        List<GY_FANGJIAN> GetFangJianList(string FangJianID);

    }
}
