using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.DTO.HIS.GY;

namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_CHUANGWEIZU2Repository : RepositoryBase<GY_CHUANGWEIZU2>, IGY_CHUANGWEIZU2Repository
    {
        public GY_CHUANGWEIZU2Repository(IRepositoryContext context, ServiceContext sContext) : base(context, sContext) { }

        public int Update(string sqlString)
        {
            var num = this.SqlQuery<E_GY_CHUANGWEIZU2>(sqlString).ToList();
            return num.Count;
        }
    }
}
