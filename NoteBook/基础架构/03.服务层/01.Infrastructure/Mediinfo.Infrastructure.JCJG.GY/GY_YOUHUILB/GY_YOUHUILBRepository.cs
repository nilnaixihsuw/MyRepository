using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YOUHUILBRepository : RepositoryBase<GY_YOUHUILB>, IGY_YOUHUILBRepository
	{
		public GY_YOUHUILBRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// ��ȡȫԺ������Ż����
        /// </summary>
        /// <param name="menZhenZYBZ"></param>
        /// <returns></returns>
        public GY_YOUHUILB GetQuanYuanJM(int menZhenZYBZ)
        {
            var query = this.Set<GY_YOUHUILB>().Where(c => c.QUANYUANJMBZ == 1);

            if (menZhenZYBZ == 0)
                query.Where(c => c.MENZHENSY == 1);
            else
                query.Where(c => c.ZHUYUANSY == 1);

            return query.FirstOrDefault();
        }

        /// <summary>
        /// ȡ�Ż����
        /// </summary>
        /// <returns></returns>
        public List<GY_YOUHUILB> GetList()
        {
            var list = this.Set<GY_YOUHUILB>().ToList().WithContext(this, ServiceContext);
            return list;
        }

        /// <summary>
        /// ȡ�Ż����
        /// </summary>
        /// <returns></returns>
        public GY_YOUHUILB GetGuaHaoSY(string youHuiLB,string danDuYH)
        {
            if (danDuYH=="0")
            {
                return this.Set<GY_YOUHUILB>().Where(w => w.YOUHUILBID == youHuiLB && (w.MENZHENSY == 1)).FirstOrDefaultWithContext(this, ServiceContext);
            }
            else
            {
                return this.Set<GY_YOUHUILB>().Where(w => w.YOUHUILBID == youHuiLB && (w.GUAHAOSY == 1)).FirstOrDefaultWithContext(this, ServiceContext);
            }
             
        }

        /// <summary>
        /// �ӻ�����ȡ
        /// add by songxl on 2019-7-4
        /// </summary>
        /// <param name="youHuiLBID"></param>
        /// <returns></returns>
        public List<GY_YOUHUILB> GetByKeyFromCache(string youHuiLBID)
        {
            var youHuiLBList = GetFromCache<List<GY_YOUHUILB>>("YOUHUILB");
            if (youHuiLBList == null)
            {
                youHuiLBList = this.Set<GY_YOUHUILB>().ToList();
                AddToCache<List<GY_YOUHUILB>>("YOUHUILB", youHuiLBList);
            }
            var list = youHuiLBList.Where(o => o.YOUHUILBID == youHuiLBID).ToList().WithContext(this, ServiceContext);

            return list;
        }

     
    }
}
