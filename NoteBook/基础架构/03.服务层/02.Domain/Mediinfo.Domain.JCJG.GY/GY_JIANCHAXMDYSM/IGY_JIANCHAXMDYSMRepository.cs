using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_JIANCHAXMDYSMRepository : IRepository<GY_JIANCHAXMDYSM>, IDependency
	{
        List<GY_JIANCHAXMDYSM> GetJianChaXMDYSM(string jianChaXMID, string yingYongID);

    }
}
