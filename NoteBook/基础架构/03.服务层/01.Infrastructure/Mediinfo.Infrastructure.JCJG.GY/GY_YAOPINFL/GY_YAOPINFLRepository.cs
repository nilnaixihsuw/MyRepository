using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.UnitOfWork;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_YAOPINFLRepository : RepositoryBase<GY_YAOPINFL>, IGY_YAOPINFLRepository
	{
		public GY_YAOPINFLRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        public List<GY_YAOPINFL> GetList(string yaoPinID)
        { 
            var list = (from yaoPinFL in this.Set<GY_YAOPINFL>()
                        join yaoPinMC in this.Set<GY_YAOPINMC>()
                        on yaoPinFL.YAOPINFLID equals yaoPinMC.YAOPINFL
                           where yaoPinMC.YAOPINID == yaoPinID
                        select yaoPinFL).ToList();  

            return list;
        }

        public List<GY_YAOPINFL> GetListByShangJiFL(string yaoPinID)
        {
            var list = this.Set<GY_YAOPINFL>().Where(o => o.SHANGJIFL == yaoPinID).ToList();

            return list;
        }

    }
}
