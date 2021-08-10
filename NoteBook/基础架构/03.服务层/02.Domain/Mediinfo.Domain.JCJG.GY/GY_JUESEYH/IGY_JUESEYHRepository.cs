using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_JUESEYHRepository : IRepository<GY_JUESEYH>, IDependency
	{
        GY_JUESEYH GetByID(string jueseid, string yonghuid);
        List<GY_JUESEYH> GetJueSeID(string zhiGongID);


    }
}
