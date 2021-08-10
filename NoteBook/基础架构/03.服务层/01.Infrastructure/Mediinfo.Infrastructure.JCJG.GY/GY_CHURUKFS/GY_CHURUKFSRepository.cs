using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_CHURUKFSRepository : RepositoryBase<GY_CHURUKFS>, IGY_CHURUKFSRepository
	{
		public GY_CHURUKFSRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public void SetCache()
        {
            var chuRuKFSList = GetFromCache<List<GY_CHURUKFS>>("ChuRuKFS");
            if (chuRuKFSList == null)
            {
                chuRuKFSList = this.QuerySet<GY_CHURUKFS>().ToList();
                AddToCache<List<GY_CHURUKFS>>("ChuRuKFS", chuRuKFSList);
            }
        }

        public List<GY_CHURUKFS> GetList(string yingYongID,string fangShiID)
        { 
            var list = this.Set<GY_CHURUKFS>().Where(o=>o.YINGYONGID== yingYongID && o.FANGSHIID == fangShiID).ToList().WithContext(this,ServiceContext);
            return list;
        }

        public List<GY_CHURUKFS> GetListbydanweibm(string yingYongID, string churukbz, string danweibm)
        {
            List<GY_CHURUKFS> list = new List<GY_CHURUKFS>();
            if (string.IsNullOrEmpty(yingYongID))
            {
                list = this.Set<GY_CHURUKFS>().Where(o => o.CHURUKBZ == churukbz && o.DANWEIBM == danweibm).ToList().WithContext(this, ServiceContext);
            }
            else
            {
                list = this.Set<GY_CHURUKFS>().Where(o => o.YINGYONGID == yingYongID && o.CHURUKBZ == churukbz && o.DANWEIBM == danweibm).ToList().WithContext(this, ServiceContext);
            }

            return list;
        }

        public List<GY_CHURUKFS> GetListFromCache(string yingYongID, string fangShiID)
        { 
            var chuRuKFS = new List<GY_CHURUKFS>();

            var chuRuKFSList = GetFromCache<List<GY_CHURUKFS>>("ChuRuKFS");

            if (chuRuKFSList == null)
            {
                chuRuKFS = this.Set<GY_CHURUKFS>().Where(o => o.YINGYONGID == yingYongID && o.FANGSHIID == fangShiID).ToList().WithContext(this, ServiceContext);
            }
            else
            {
                chuRuKFS = chuRuKFSList.Where(o => o.YINGYONGID == yingYongID && o.FANGSHIID == fangShiID).ToList().WithContext(this, ServiceContext);
             }  
            return chuRuKFS; 
        }

        /// <summary>
        /// 根据应用ID获取出入库方式
        /// </summary>
        /// <param name="yingYongID"></param>
        /// <returns></returns>
        public List<GY_CHURUKFS> GetList(string yingYongID)
        {
            var list = this.Set<GY_CHURUKFS>().Where(o => o.YINGYONGID == yingYongID).ToList().WithContext(this, ServiceContext);
            return list;
        }
    }
}
