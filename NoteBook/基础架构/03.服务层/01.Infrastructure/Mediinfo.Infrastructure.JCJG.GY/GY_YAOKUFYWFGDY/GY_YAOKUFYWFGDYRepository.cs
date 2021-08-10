using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YAOKUFYWFGDYRepository : RepositoryBase<GY_YAOKUFYWFGDY>, IGY_YAOKUFYWFGDYRepository
	{
		public GY_YAOKUFYWFGDYRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_YAOKUFYWFGDY> GetList(string yingYongID,string muBiaoZhi)
        {
            var list = this.Set<GY_YAOKUFYWFGDY>().Where(o => o.YINGYONGID == yingYongID && o.MUBIAOZHI == muBiaoZhi).ToList().WithContext(this,ServiceContext);
            return list;
        }        
    }
}
