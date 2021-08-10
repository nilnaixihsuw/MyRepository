using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using System.Linq;
namespace Mediinfo.Infrastructure.JCJG.GY
{
    public class GY_BUJUQXRepository : RepositoryBase<GY_BUJUQX>, IGY_BUJUQXRepository
    {
        public GY_BUJUQXRepository(IRepositoryContext context, ServiceContext sContext) : base(context, sContext) { }

        public int GetBuJuQX(string yonghuID)
        {
            var data = (from s in this.Set<GY_BUJUQX>()
                       join c in this.Set<GY_JUESEYH>() on s.JUESEID equals c.JUESEID
                       where c.YONGHUID == yonghuID
                       select s).ToList();
            return data.Count();
        }
    }
}
