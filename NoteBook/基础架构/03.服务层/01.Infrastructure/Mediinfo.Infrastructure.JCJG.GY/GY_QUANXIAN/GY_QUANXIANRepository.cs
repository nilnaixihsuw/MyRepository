using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_QUANXIANRepository : RepositoryBase<GY_QUANXIAN>, IGY_QUANXIANRepository
	{
		public GY_QUANXIANRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
        /// <summary>
        /// 根据主键获取权限信息
        /// </summary>
        /// <param name="quanXianID"></param>
        /// <returns></returns>
        public GY_QUANXIAN GetByID(string quanXianID)
        {
            var dto = (from qx in this.Set<GY_QUANXIAN>()
                       where qx.QUANXIANID == quanXianID
                       select qx).FirstOrDefault()?.WithContext(this, ServiceContext);
            return dto;
        }
	}
}
