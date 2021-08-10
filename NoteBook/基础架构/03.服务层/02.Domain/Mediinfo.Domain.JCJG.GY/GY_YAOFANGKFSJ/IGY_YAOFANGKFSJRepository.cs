using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_YAOFANGKFSJRepository : IRepository<GY_YAOFANGKFSJ>, IDependency
	{
        GY_YAOFANGKFSJ GetByID(string yingYongID, string kaiShiSJ, string jieShuSJ);

    }
}
