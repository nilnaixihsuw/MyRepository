using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_JIANMIANMXRepository : RepositoryBase<GY_JIANMIANMX>, IGY_JIANMIANMXRepository
	{
		public GY_JIANMIANMXRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// »°ºı√‚√˜œ∏
        /// </summary>
        /// <returns></returns>
        public List<GY_JIANMIANMX> GetList(string jieSuanID)
        {
            var list = this.Set<GY_JIANMIANMX>().Where(o => o.JIESUANID == jieSuanID ).ToList().WithContext(this, ServiceContext);
            return list;
        }
    }

  
}
