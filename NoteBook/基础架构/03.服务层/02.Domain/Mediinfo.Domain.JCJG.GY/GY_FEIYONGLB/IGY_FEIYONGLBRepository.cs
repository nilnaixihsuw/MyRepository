using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_FEIYONGLBRepository : IRepository<GY_FEIYONGLB>, IDependency
	{
        List<GY_FEIYONGLB> GetFeiYongLBList(string feiYongLB);

        /// <summary>
        /// 关联gy_feiyongxz 按照费用性质取gy_feiyonglb
        /// </summary>
        /// <param name="feiYongXZ"></param>
        /// <returns></returns>
        List<GY_FEIYONGLB> GetFeiYongLBGLFYXZ(string feiYongXZ);

    }
}
