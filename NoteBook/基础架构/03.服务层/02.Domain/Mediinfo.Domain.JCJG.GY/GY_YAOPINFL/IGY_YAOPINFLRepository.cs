using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_YAOPINFLRepository : IRepository<GY_YAOPINFL>, IDependency
	{
        List<GY_YAOPINFL> GetList(string yaoPinID);

        List<GY_YAOPINFL> GetListByShangJiFL(string yaoPinID);

    }
}
