using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_BILIYHRepository : RepositoryBase<GY_BILIYH>, IGY_BILIYHRepository
	{
		public GY_BILIYHRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// 通过主键获取
        /// </summary>
        /// <param name="youHuiLB"></param>
        /// <param name="xiangMuId"></param>
        /// <param name="xiangMuLX"></param>
        /// <returns></returns>
        public GY_BILIYH GetByKey(string youHuiLB, string xiangMuId, string xiangMuLX)
        {
         
            return this.Set<GY_BILIYH>()
                        .Where(c => c.YOUHUILB == youHuiLB && c.XIANGMUID == xiangMuId && c.XIANGMULX == xiangMuLX)
                        .FirstOrDefault()
                        .WithContext(this, this.ServiceContext);
        }

        public List<GY_BILIYH> GetList()
        {
            return this.Set<GY_BILIYH>().ToList().WithContext(this, this.ServiceContext);
        }
    }
}
