using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;
using Mediinfo.Utility.Extensions;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_KUAIJIQJRepository : RepositoryBase<GY_KUAIJIQJ>, IGY_KUAIJIQJRepository
	{
		public GY_KUAIJIQJRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public GY_KUAIJIQJ GetByID(string yingYongID, string kuaiJiRL)
        {
            var db = this.Set<GY_KUAIJIQJ>().Where(o => o.YINGYONGID == yingYongID && o.KUAIJIRL == kuaiJiRL).FirstOrDefault().WithContext(this, ServiceContext);
            return db;
        }
        public List<GY_KUAIJIQJ> GetList(string yingYongID,int dangQianBZ=0)
        {
            var list = this.Set<GY_KUAIJIQJ>().Where(o => o.YINGYONGID == yingYongID && o.DANGQIANBZ == dangQianBZ).ToList();
            //list.ForEach(o =>
            //{
            //    o.JIESHURQ = o.JIESHURQ.ToEndDateTime();
            //});
            return list;
        }
        public List<GY_KUAIJIQJ> GetList(string yingYongID, string nian)
        {
            var list = this.Set<GY_KUAIJIQJ>().Where(o => o.YINGYONGID == yingYongID && o.KUAIJIRL.Substring(0, 4) == nian).ToList();
            return list;
        }



    }
}
