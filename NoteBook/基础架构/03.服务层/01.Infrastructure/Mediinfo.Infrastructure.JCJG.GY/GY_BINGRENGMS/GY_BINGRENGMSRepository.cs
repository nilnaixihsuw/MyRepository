using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_BINGRENGMSRepository : RepositoryBase<GY_BINGRENGMS>, IGY_BINGRENGMSRepository
	{
		public GY_BINGRENGMSRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_BINGRENGMS> GetList(string laiYuanID)
        {
            var list = this.Set<GY_BINGRENGMS>().Where(p => p.LAIYUANID == laiYuanID).ToList().WithContext(this, ServiceContext);
            return list;
        }
        /// <summary>
        /// 获取病人过敏史
        /// </summary>
        /// <param name="laiYuanID"></param>
        /// <returns></returns>
        public List<GY_BINGRENGMS> GetBingRenGMSList(string bingrenID, string jiageID)
        {
            var list = this.Set<GY_BINGRENGMS>().Where(p => p.BINGRENID == bingrenID && p.JIAGEID==jiageID && p.JILULY=="2").ToList().WithContext(this, ServiceContext);
            return list;
        }

        /// <summary>
        /// 获取病人过敏史
        /// </summary>
        /// <param name="bingrenID"></param>
        /// <returns></returns>
        public List<GY_BINGRENGMS> GetBingRenGMSList(string bingrenID)
        {
            var list = this.Set<GY_BINGRENGMS>().Where(p => p.BINGRENID == bingrenID).ToList().WithContext(this, ServiceContext);
            return list;
        }
    }
}
