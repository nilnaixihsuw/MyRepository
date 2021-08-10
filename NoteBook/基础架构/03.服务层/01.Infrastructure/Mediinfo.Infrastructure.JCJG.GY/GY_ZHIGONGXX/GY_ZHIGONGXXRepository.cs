using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_ZHIGONGXXRepository : RepositoryBase<GY_ZHIGONGXX>, IGY_ZHIGONGXXRepository
	{
		public GY_ZHIGONGXXRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_ZHIGONGXX> GetList(string zhiGongID)
        {
            var list = this.Set<GY_ZHIGONGXX>().Where(p => p.ZHIGONGID == zhiGongID).ToList().WithContext(this, ServiceContext);
            return list;
        }

        public List<GY_ZHIGONGXX> GetListByZhiGongGH(string zhiGongGH)
        {
            var list = this.Set<GY_ZHIGONGXX>().Where(p => p.ZHIGONGGH == zhiGongGH).ToList().WithContext(this, ServiceContext);
            return list;
        }
        /// <summary>
        /// 通过获取职工信息 xieyz 2019-06-20
        /// </summary>
        public List<GY_ZHIGONGXX> GetZhiGongXX(string new_ZhiGong, string old_ZhiGong)
        {
            string sql = string.Format(@"SELECT A.ZHIGONGLB,
           A.KESHIID,
           NVL(A.ZHIWU, S_ZHIWU_NULL),
           NVL(A.YISHENGDJ, '0'),
           B.ZHIGONGLB,
           B.KESHIID,
           NVL(B.ZHIWU, S_ZHIWU_NULL),
           NVL(B.YISHENGDJ, '0')
      INTO S_NEWZGLB,
           S_NEWZGKS,
           S_NEWZGZW,
           S_NEWZGDJ,
           S_OLDZGLB,
           S_OLDZGKS,
           S_OLDZGZW,
           S_OLDZGDJ
      FROM GY_ZHIGONGXX A, GY_ZHIGONGXX B
     WHERE A.ZHIGONGID = {0}
       AND B.ZHIGONGID = {1}", new_ZhiGong, old_ZhiGong);
            return this.SqlQuery<GY_ZHIGONGXX>(sql).ToList().WithContext(this, ServiceContext);
        }


        public string GetZhiGongxx(string zhigongid)
        {
            return Set<GY_ZHIGONGXX>().FirstOrDefault(o => o.ZHIGONGID == zhigongid)?.ZHIGONGXM;
        }

        public List<GY_ZHIGONGXX> GetZhiGongXXS(string[] zhiGongIDS)
        {
            return Set<GY_ZHIGONGXX>().Where(o => zhiGongIDS.Contains(o.ZHIGONGID)).ToList().WithContext(this, ServiceContext);
        }
    }
}
