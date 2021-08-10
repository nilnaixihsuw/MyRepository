using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_PIAOJULYRepository : RepositoryBase<GY_PIAOJULY>, IGY_PIAOJULYRepository
	{
		public GY_PIAOJULYRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_PIAOJULY> GetPiaoJuLY(string zhuangTai)
        {
            var list = this.Set<GY_PIAOJULY>().Where(o => o.PIAOJUZT == zhuangTai).ToList().WithContext(this, ServiceContext);
            return list;
        }

        public List<GY_PIAOJULY> GetPiaoJuLY(string zhuangTai,string leiXing)
        {
            var list = this.Set<GY_PIAOJULY>().Where(o => o.PIAOJUZT == zhuangTai && o.PIAOJULX == leiXing).ToList();
            return list;
        }
    }
}
