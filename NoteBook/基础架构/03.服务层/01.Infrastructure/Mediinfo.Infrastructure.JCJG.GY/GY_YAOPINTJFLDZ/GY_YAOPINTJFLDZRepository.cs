using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YAOPINTJFLDZRepository : RepositoryBase<GY_YAOPINTJFLDZ>, IGY_YAOPINTJFLDZRepository
	{
		public GY_YAOPINTJFLDZRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}
        public GY_YAOPINTJFLDZ GetByID(string yingyongid, string tongjifl,string guigeid)
        {
            var db = (from yptjfldz in this.Set<GY_YAOPINTJFLDZ>()
                      where yptjfldz.YINGYONGID == yingyongid && yptjfldz.TONGJIFL == tongjifl && yptjfldz.GUIGEID == guigeid
                      select yptjfldz).FirstOrDefault().WithContext(this, ServiceContext); ;
            return db;
        }

        public List<GY_YAOPINTJFLDZ> GetYaoPinTJFLDZListByYingYongIDAndTongJiFl(string yingyongid, string tongjifl)
        {
            var list = (from yptjfldz in this.Set<GY_YAOPINTJFLDZ>()
                      where yptjfldz.YINGYONGID == yingyongid && yptjfldz.TONGJIFL == tongjifl 
                      select yptjfldz).ToList().WithContext(this, ServiceContext); ;
            return list;
        }
    }
}
