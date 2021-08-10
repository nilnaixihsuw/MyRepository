using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_YONGHUXXRepository : RepositoryBase<GY_YONGHUXX>, IGY_YONGHUXXRepository
    {
        public GY_YONGHUXXRepository(IRepositoryContext context, ServiceContext sContext) : base(context, sContext) { }


        public int ModifyShuRuMaByYongHuID(string id, string shuRuMa)
        {
            var srm = "SRM" + shuRuMa.Substring(7, 1);
            return this.ExecuteSqlCommand("update GY_YHXX_HIS1 set SRM = :P1 where yhdm = :P2", srm, id);
        }
    }
}
