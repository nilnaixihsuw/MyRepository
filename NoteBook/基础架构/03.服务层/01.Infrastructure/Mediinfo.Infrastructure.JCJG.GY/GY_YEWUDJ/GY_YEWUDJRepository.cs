using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YEWUDJRepository : RepositoryBase<GY_YEWUDJ>, IGY_YEWUDJRepository
	{
		public GY_YEWUDJRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_YEWUDJ> GetList(string yeWuID ,List<string> yeWuPCLXList)
        { 
            var list = this.Set<GY_YEWUDJ>().Where(o=> o.YEWUID == yeWuID && yeWuPCLXList.Contains(o.YEWULX)).ToList().WithContext(this,ServiceContext);
            return list;
        } 
        public List<GY_YEWUDJ> GetList(string yeWuID)
        {
            var list = this.Set<GY_YEWUDJ>().Where(o =>  o.YEWUID == yeWuID).ToList().WithContext(this, ServiceContext);
            return list;
        }

        public List<GY_YEWUDJ> GetList(string yeWuID,string yeWuLX)
        {
            var list = this.Set<GY_YEWUDJ>().Where(o => o.YEWUID == yeWuID && o.YEWULX == yeWuLX).ToList().WithContext(this, ServiceContext);
            return list;
        }
    }
}
