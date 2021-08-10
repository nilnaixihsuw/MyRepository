using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YAOPINTJFLRepository : RepositoryBase<GY_YAOPINTJFL>, IGY_YAOPINTJFLRepository
	{
		public GY_YAOPINTJFLRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public GY_YAOPINTJFL GetByID(string yingyongid, string tongjiflid)
        {
            var db = (from yptjfl in this.Set<GY_YAOPINTJFL>()
                       where yptjfl.YINGYONGID == yingyongid && yptjfl.TONGJIFLID == tongjiflid    
                       select yptjfl).FirstOrDefault().WithContext(this, ServiceContext); ;
            return db;
        }
    }
}
