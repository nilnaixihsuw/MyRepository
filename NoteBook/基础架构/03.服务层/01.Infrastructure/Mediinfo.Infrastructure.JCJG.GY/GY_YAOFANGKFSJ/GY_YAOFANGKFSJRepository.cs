using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.DBEntity;
using Mediinfo.Infrastructure.Core.Repository;

using System.Linq;

namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_YAOFANGKFSJRepository : RepositoryBase<GY_YAOFANGKFSJ>, IGY_YAOFANGKFSJRepository
	{
		public GY_YAOFANGKFSJRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public GY_YAOFANGKFSJ GetByID(string yingYongID, string kaiShiSJ,string jieShuSJ)
        {
            var db = this.Set<GY_YAOFANGKFSJ>().Where(o => o.YINGYONGID == yingYongID && o.KAISHISJ == kaiShiSJ && o.JIESHUSJ == jieShuSJ).FirstOrDefault().WithContext(this, ServiceContext);
            return db;
        }
    }
}
