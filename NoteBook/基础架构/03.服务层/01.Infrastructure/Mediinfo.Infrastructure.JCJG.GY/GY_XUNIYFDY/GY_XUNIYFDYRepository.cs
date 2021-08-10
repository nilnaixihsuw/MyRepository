using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;

using System.Collections.Generic;
using System.Linq;

namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_XUNIYFDYRepository : RepositoryBase<GY_XUNIYFDY>, IGY_XUNIYFDYRepository
	{
		public GY_XUNIYFDYRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_XUNIYFDY> GetList(string yingYongID)
        {
            var list = this.Set<GY_XUNIYFDY>().Where(p => p.YINGYONGID2 == yingYongID).ToList();
            return list;
        }

        public List<GY_YINGYONG> GetYingYongMC(string jiaGeID, int kuCunSL, string yingYongID, string yingYongID1)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 取虚拟药房对应的其它药房名称
        /// </summary>
        /// <param name="jiaGeID"></param>
        /// <param name="kuCunSL"></param>
        /// <param name="yingYongID"></param>
        /// <param name="yingYongID1"></param>
        /// <returns></returns>
        //public List<GY_YINGYONG> GetYingYongMC(string jiaGeID, int kuCunSL, string yingYongID, string yingYongID1)
        //{
        //    var list = (from yingYong in this.QuerySet<GY_YINGYONG>()
        //                join kuCun in this.QuerySet<MY_KUCUN1>()
        //                on yingYong.YINGYONGID equals kuCun.YINGYONGID
        //                join xuNiYFDY in this.QuerySet<GY_XUNIYFDY>()
        //                on yingYong.YINGYONGID equals xuNiYFDY.YINGYONGID2
        //                where xuNiYFDY.YINGYONGID1 == yingYongID1
        //                && xuNiYFDY.YINGYONGID2 != yingYongID
        //                  && kuCun.JIAGEID == jiaGeID
        //                  && kuCun.KUCUNSL > kuCunSL
        //                select yingYong).ToList();
        //    return list;
        //}
    }
}
