using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_YILIAOZU2Repository : RepositoryBase<GY_YILIAOZU2>, IGY_YILIAOZU2Repository
    {
        public GY_YILIAOZU2Repository(IRepositoryContext context, ServiceContext sContext) : base(context, sContext) { }

        public GY_YILIAOZU2 GetByID(string yiliaozid, string zhigongid)
        {
            var dto = (from ylz2 in this.Set<GY_YILIAOZU2>()
                       where ylz2.YILIAOZID == yiliaozid && ylz2.ZHIGONGID == zhigongid
                       select ylz2).FirstOrDefault().WithContext(this, ServiceContext);
            return dto;
        }

        public List<GY_YILIAOZU2> GetList(string yiliaozid)
        {
            var list = (from ylz2 in this.Set<GY_YILIAOZU2>()
                        where ylz2.YILIAOZID == yiliaozid
                        select ylz2).ToList().WithContext(this, ServiceContext);

            return list;
        }
        /// <summary>
        /// 根据职工id获取医疗组信息 xieyz 2019-06-22
        /// </summary>
        /// <param name="zhiGongID"></param>
        /// <returns></returns>
        public List<GY_YILIAOZU2> GetListByZhiGongID(string zhiGongID)
        {
            return this.Set<GY_YILIAOZU2>().Where(x => x.ZHIGONGID == zhiGongID).ToList().WithContext(this,ServiceContext);
        }

        public List<GY_YILIAOZU2> GetListByYiLiaoZu(List<string> yiliaozu)
        {
            return this.Set<GY_YILIAOZU2>().Where(x => yiliaozu.Contains(x.YILIAOZID)).ToList().WithContext(this, ServiceContext);
        }
    }
}
