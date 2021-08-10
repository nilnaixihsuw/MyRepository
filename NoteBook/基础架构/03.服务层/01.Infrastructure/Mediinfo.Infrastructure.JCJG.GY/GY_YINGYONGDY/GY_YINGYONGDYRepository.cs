using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_YINGYONGDYRepository : RepositoryBase<GY_YINGYONGDY>, IGY_YINGYONGDYRepository
    {
        public GY_YINGYONGDYRepository(IRepositoryContext context, ServiceContext sContext) : base(context, sContext) { }

        // PKG_MZ_ZHENJIAN.PRC_ZJ_CREATEGUAHAOXX 7654-7664
        public string GetYingYongID2(string YingYongID)
        {
            var yingYongID2 = (from o in Set<GY_YINGYONGDY>()
                               where o.YINGYONGID1 == YingYongID &&
                                     o.DUIYINGLX == "10"   //门诊诊间应用ID  对应 挂号收费应用ID
                               select o.YINGYONGID2).FirstOrDefault();
            return yingYongID2 ?? "%";  // 如果为空就返回“%”
        }
    }
}
