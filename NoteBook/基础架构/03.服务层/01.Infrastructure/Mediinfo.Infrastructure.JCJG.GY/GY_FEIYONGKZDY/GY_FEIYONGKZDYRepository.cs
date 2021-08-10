using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_FEIYONGKZDYRepository : RepositoryBase<GY_FEIYONGKZDY>, IGY_FEIYONGKZDYRepository
	{
		public GY_FEIYONGKZDYRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// ȡδ���ϵķ��ÿ��ƶ�Ӧ��Ϣ����ѯ��
        /// </summary>
        /// <param name="yingYongID">Ӧ��ID</param>
        /// <param name="feiYongXZ">��������</param>
        /// <returns></returns>
        public GY_FEIYONGKZDY GetWeiZuoFei(string yingYongID, string feiYongXZ)
        {
            return this.Set()
                       .Where(c => c.FEIYONGXZ == feiYongXZ && c.YINGYONGID == yingYongID && c.ZUOFEIBZ != 1)
                       .FirstOrDefault()
                       .WithContext(this,this.ServiceContext);
        }
    }
}
