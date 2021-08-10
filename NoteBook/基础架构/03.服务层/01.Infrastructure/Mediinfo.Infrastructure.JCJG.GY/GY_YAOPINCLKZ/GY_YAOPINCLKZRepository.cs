using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YAOPINCLKZRepository : RepositoryBase<GY_YAOPINCLKZ>, IGY_YAOPINCLKZRepository
	{
		public GY_YAOPINCLKZRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public void SetCache()
        {
            var yaoPinCLKZList = GetFromCache<List<GY_YAOPINCLKZ>>("YPCLKZLIST");
            if (yaoPinCLKZList == null)
            {
                yaoPinCLKZList = this.QuerySet<GY_YAOPINCLKZ>().ToList().WithContext(this, ServiceContext);
                AddToCache<List<GY_YAOPINCLKZ>>("YPCLKZLIST", yaoPinCLKZList);
            }
        }

        public List<GY_YAOPINCLKZ> GetByAll()
        {
            var list = this.Set<GY_YAOPINCLKZ>().ToList().WithContext(this, ServiceContext);
            return list;
        }

        public GY_YAOPINCLKZ GetByID(string yingYongID, string jiaGeID, string guiGeID)
        {
            var db = (from ypclkz in this.Set<GY_YAOPINCLKZ>()
                      where ypclkz.YINGYONGID == yingYongID && ypclkz.JIAGEID == jiaGeID && ypclkz.GUIGEID == guiGeID
                      select ypclkz).FirstOrDefault().WithContext(this, ServiceContext); 
            return db;
        }
        public List<GY_YAOPINCLKZ> GetListByJiaGeID(string jiaGeID,string yingYongID)
        {
            var list = this.Set<GY_YAOPINCLKZ>().Where(o => o.JIAGEID == jiaGeID && o.YINGYONGID == yingYongID).ToList().WithContext(this, ServiceContext);
            return list;
        }

        public List<GY_YAOPINCLKZ> GetListByGuiGeID(string guiGeID, string yingYongID)
        {
            var list = this.Set<GY_YAOPINCLKZ>().Where(o => o.GUIGEID == guiGeID && o.YINGYONGID == yingYongID).ToList().WithContext(this, ServiceContext);

            return list;
        }
        public List<GY_YAOPINCLKZ> GetListByYaoPinID(string yaoPinID, string yingYongID)
        {
            var list = this.Set<GY_YAOPINCLKZ>().Where(o => o.YAOPINID == yaoPinID && o.YINGYONGID == yingYongID).ToList().WithContext(this, ServiceContext);

            return list;
        }

    }
}
