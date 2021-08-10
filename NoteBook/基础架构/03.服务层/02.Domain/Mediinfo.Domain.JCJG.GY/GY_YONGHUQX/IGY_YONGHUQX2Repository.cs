using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_YONGHUQX2Repository : IRepository<GY_YONGHUQX2>, IDependency
	{
        GY_YONGHUQX2 GetByID(string quanxianid,string yonghuid);
    }
}
