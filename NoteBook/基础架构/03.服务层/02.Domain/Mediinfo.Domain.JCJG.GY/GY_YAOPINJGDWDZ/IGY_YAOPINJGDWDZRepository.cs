using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_YAOPINJGDWDZRepository : IRepository<GY_YAOPINJGDWDZ>, IDependency
	{
        List<GY_YAOPINJGDWDZ> GetList(string jiaGeID);

    }
}
