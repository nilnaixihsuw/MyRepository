using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YAOPINJGDZRepository : RepositoryBase<GY_YAOPINJGDZ>, IGY_YAOPINJGDZRepository
	{
		public GY_YAOPINJGDZRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public void SetCache()
        {
            var yaoPinJGDZList = GetFromCache<List<GY_YAOPINJGDZ>>("YPJGDZ");
            if (yaoPinJGDZList == null)
            {
                var list = this.QuerySet<GY_YAOPINJGDZ>().ToList();
                AddToCache<List<GY_YAOPINJGDZ>>("YPJGDZ", list);
            }
        }

        public void UpdateCache(GY_YAOPINJGDZ yaoPinJGDZ)
        { 

            var yaoPinJGDZList = GetFromCache<List<GY_YAOPINJGDZ>>("YPJGDZ");
            if (yaoPinJGDZList != null)
            {
                yaoPinJGDZList.Add(yaoPinJGDZ);
                base.UpdateToCache<List<GY_YAOPINJGDZ>>("YPJGDZ", yaoPinJGDZList);
            }
        }

        public List<GY_YAOPINJGDZ> GetListByJiaGeID2(string jiaGeID2,decimal jiaGe2, int jiaGeLX)
        {
            var list = new List<GY_YAOPINJGDZ>();
            var yaoPinJGDZList = GetFromCache<List<GY_YAOPINJGDZ>>("YPJGDZ");
            if (yaoPinJGDZList == null)
            {
                list = (from ypjgdz in this.Set<GY_YAOPINJGDZ>()
                        where ypjgdz.JIAGEID2 == jiaGeID2 && ypjgdz.JIAGE2 == jiaGe2 && ypjgdz.JIAGELX == jiaGeLX
                        select ypjgdz).ToList();
            }
            else
            {
                list = yaoPinJGDZList.Where(ypjgdz => ypjgdz.JIAGEID2 == jiaGeID2 && ypjgdz.JIAGE2 == jiaGe2 && ypjgdz.JIAGELX == jiaGeLX).ToList();

            }
            return list;
        }

        public List<GY_YAOPINJGDZ> GetListByJiaGeID1FromCache(string jiaGeID1, decimal jiaGe1, int jiaGeLX)
        {

            var list = new List<GY_YAOPINJGDZ>();
            var yaoPinJGDZList = GetFromCache<List<GY_YAOPINJGDZ>>("YPJGDZ");
            if (yaoPinJGDZList == null)
            {
                list = (from ypjgdz in this.Set<GY_YAOPINJGDZ>()
                        where ypjgdz.JIAGEID1 == jiaGeID1 && ypjgdz.JIAGE1 == jiaGe1 && ypjgdz.JIAGELX == jiaGeLX
                        select ypjgdz).ToList();
            }
            else
            {
                list = yaoPinJGDZList.Where(ypjgdz => ypjgdz.JIAGEID1 == jiaGeID1 && ypjgdz.JIAGE1 == jiaGe1 && ypjgdz.JIAGELX == jiaGeLX).ToList();
            }
            return list;
        }
        public List<GY_YAOPINJGDZ> GetListByJiaGeID1(string jiaGeID1, decimal jiaGe1, int jiaGeLX)
        { 
           
             var   list = (from ypjgdz in this.Set<GY_YAOPINJGDZ>()
                            where ypjgdz.JIAGEID1 == jiaGeID1 && ypjgdz.JIAGE1 == jiaGe1 && ypjgdz.JIAGELX == jiaGeLX
                            select ypjgdz).ToList();
           
            return list; 
        }
        public List<GY_YAOPINJGDZ> GetList(string jiaGeID1,string jiaGeID2, decimal jiaGe2, int jiaGeLX)
        {
           
            var list = this.Set<GY_YAOPINJGDZ>().Where(p => p.JIAGEID1 == jiaGeID1 && p.JIAGEID2 == jiaGeID2 && p.JIAGE2 == jiaGe2 && p.JIAGELX == jiaGeLX).ToList();

            return list;
        }

        public List<GY_YAOPINJGDZ> GetListFromCache(string jiaGeID1, string jiaGeID2, decimal jiaGe2, int jiaGeLX)
        {
            var list = new List<GY_YAOPINJGDZ>();
            var yaoPinJGDZList = GetFromCache<List<GY_YAOPINJGDZ>>("YPJGDZ");
            if (yaoPinJGDZList == null)
            {
                 list = this.Set<GY_YAOPINJGDZ>().Where(p => p.JIAGEID1 == jiaGeID1 && p.JIAGEID2 == jiaGeID2 && p.JIAGE2 == jiaGe2 && p.JIAGELX == jiaGeLX).ToList();

            }
            else
            {
                list = yaoPinJGDZList.Where(p => p.JIAGEID1 == jiaGeID1 && p.JIAGEID2 == jiaGeID2 && p.JIAGE2 == jiaGe2 && p.JIAGELX == jiaGeLX).ToList();

            }

            return list;
        }

    }
}
