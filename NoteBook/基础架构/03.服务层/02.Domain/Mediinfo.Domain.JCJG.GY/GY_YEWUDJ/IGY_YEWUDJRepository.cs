using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_YEWUDJRepository : IRepository<GY_YEWUDJ>, IDependency
	{
        List<GY_YEWUDJ> GetList(string yeWuID, List<string> yeWuPCLXList);

        List<GY_YEWUDJ> GetList(string yeWuID);

        List<GY_YEWUDJ> GetList(string yeWuID, string yeWuLX);

    }
}
