using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_YINGYONGMZLBRepository : RepositoryBase<GY_YINGYONGMZLB>, IGY_YINGYONGMZLBRepository
    {
        public GY_YINGYONGMZLBRepository(IRepositoryContext context, ServiceContext sContext) : base(context, sContext) { }

        // PKG_MZ_ZHENJIAN.PRC_ZJ_CREATEGUAHAOXX 7688-7692
        public int GetShiFouWSDDPB(string S_YINGYONGMZLBID)
        {
            var leiBieGL = (from o in Set<GY_YINGYONGMZLB>()
                            join p in Set<GY_DAIMA>() on o.MENZHENLB equals p.DAIMAID
                            where p.DAIMALB == "0025" &&
                                  p.ZUOFEIBZ == 0 &&
                                  o.MENZHENLB == p.DAIMAID
                            select p.ZIFU1).FirstOrDefault();
            return leiBieGL == null ? -1 : Convert.ToInt32(leiBieGL);
        }

        // PKG_MZ_ZHENJIAN.PRC_ZJ_CREATEGUAHAOXX 7711-7726
        public void Get(string S_YINGYONGMZLBID,int shangXiaWBZ, out string S_GUAHAOLB, out string S_GUAHAOSFXM,out string S_ZHENLIAOSFXM)
        {
            var e = (from o in Set<GY_YINGYONGMZLB>()
                where o.YINGYONGMZLBID == S_YINGYONGMZLBID
                select new
                {
                    o.MENZHENLB,
                    o.SHANGWUGHFXM,
                    o.SHANGWUZLFXM,
                    o.XIAWUGHFXM,
                    o.XIAWUZLFXM,
                    o.WANSHANGGHFXM,
                    o.WANSHANGZLFXM
                }).FirstOrDefault();
            S_GUAHAOLB = e.MENZHENLB;
            if (shangXiaWBZ == 0)
            {
                S_GUAHAOSFXM = e.SHANGWUGHFXM;
                S_ZHENLIAOSFXM = e.SHANGWUZLFXM;
            }
            else if(shangXiaWBZ == 1)
            {
                S_GUAHAOSFXM = e.XIAWUGHFXM;
                S_ZHENLIAOSFXM = e.XIAWUZLFXM;
            }
            else
            {
                S_GUAHAOSFXM = e.WANSHANGGHFXM;
                S_ZHENLIAOSFXM = e.WANSHANGZLFXM;
            }
           
        }
    }
}
