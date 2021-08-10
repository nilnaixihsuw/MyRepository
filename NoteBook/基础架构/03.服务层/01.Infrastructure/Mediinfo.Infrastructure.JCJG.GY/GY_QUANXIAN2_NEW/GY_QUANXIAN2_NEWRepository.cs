using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_QUANXIAN2_NEWRepository : RepositoryBase<GY_QUANXIAN2_NEW>, IGY_QUANXIAN2_NEWRepository
	{
        public GY_QUANXIAN2_NEWRepository(IRepositoryContext context, ServiceContext sContext) : base(context, sContext) { }
        /// <summary>
        /// 根据主键获取权限2信息
        /// </summary>
        /// <param name="quanXianID"></param>
        /// <returns></returns>
        public GY_QUANXIAN2_NEW GetByID(string quanXianID)
        {
            var dto = (from qx in this.Set<GY_QUANXIAN2_NEW>()
                       where qx.QUANXIANID == quanXianID
                       select qx).FirstOrDefault().WithContext(this, ServiceContext);
            return dto;
        }
    }
}
