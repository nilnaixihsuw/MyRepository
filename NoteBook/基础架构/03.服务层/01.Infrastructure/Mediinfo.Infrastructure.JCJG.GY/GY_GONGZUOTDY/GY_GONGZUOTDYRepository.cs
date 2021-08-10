using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_GONGZUOTDYRepository : RepositoryBase<GY_GONGZUOTDY>, IGY_GONGZUOTDYRepository
    {
        public GY_GONGZUOTDYRepository(IRepositoryContext context, ServiceContext sContext) : base(context, sContext) { }

        public List<GY_GONGZUOTDY> GetList()
        {

            var list = this.Set<GY_GONGZUOTDY>().Where(p => p.ZUOFEIBZ != 1 && p.XITONGID == ServiceContext.XITONGID).ToList().WithContext(this, ServiceContext);

            return list;
        }
    }
}
