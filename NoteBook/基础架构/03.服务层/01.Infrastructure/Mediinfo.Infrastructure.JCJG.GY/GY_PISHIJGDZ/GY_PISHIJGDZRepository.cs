using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_PISHIJGDZRepository : RepositoryBase<GY_PISHIJGDZ>, IGY_PISHIJGDZRepository
	{
		public GY_PISHIJGDZRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
        /// <summary>
        /// 根据皮试结果ID取处理意见ID
        /// </summary>
        /// <param name="prmPiShiJGID"></param>
        /// <returns></returns>
        public string GetChuLiYJID(string prmPiShiJGID)
        {
            return this.Set<GY_PISHIJGDZ>().Where(w => w.PISHIJGID == prmPiShiJGID).FirstOrDefault().CHULIYJID;
        }
    }
}
