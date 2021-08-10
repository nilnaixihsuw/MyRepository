using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_SHOUFEIXMZXKSRepository : RepositoryBase<GY_SHOUFEIXMZXKS>, IGY_SHOUFEIXMZXKSRepository
	{
		public GY_SHOUFEIXMZXKSRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
         

        public List<GY_SHOUFEIXMZXKS> GetList(string shouFeiXMID)
        {
            var list = this.Set<GY_SHOUFEIXMZXKS>().Where(w=>w.SHOUFEIXM ==shouFeiXMID).ToList().WithContext(this, ServiceContext);
            return list;
        }
        public GY_SHOUFEIXMZXKS Get(string shouFeiXMID)
        {
            var domain = this.Set<GY_SHOUFEIXMZXKS>().Where(w => w.SHOUFEIXM == shouFeiXMID).FirstOrDefault().WithContext(this, ServiceContext);
            return domain;
        }

    }
}
