using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_YONGHUXXRepository : IRepository<GY_YONGHUXX>, IDependency
     {
         int ModifyShuRuMaByYongHuID(string id, string shuRuMa);
     }
}
