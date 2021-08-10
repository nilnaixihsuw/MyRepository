using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_FEIYONGLBRepository : RepositoryBase<GY_FEIYONGLB>, IGY_FEIYONGLBRepository
	{
		public GY_FEIYONGLBRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_FEIYONGLB> GetFeiYongLBList(string feiYongLB)
        {
            var list = this.Set<GY_FEIYONGLB>().Where(o => o.LEIBIEID == feiYongLB ).ToList().WithContext(this, ServiceContext);
            return list;
        }

        /// <summary>
        /// 取费用列表list关联费用性质
        /// </summary>
        /// <param name="feiYongXZ"></param>
        /// <returns></returns>
        public List<GY_FEIYONGLB> GetFeiYongLBGLFYXZ(string feiYongXZ)
        {
            var list = (from gyFeiYongXZ in this.Set<GY_FEIYONGXZ>()
                        join gyfeiyonglb in this.Set<GY_FEIYONGLB>()
                        on gyFeiYongXZ.FEIYONGLB equals gyfeiyonglb.LEIBIEID
                        where gyFeiYongXZ.XINGZHIID == feiYongXZ 
                        select gyfeiyonglb).ToList();
            return list;


        }


    }
}
