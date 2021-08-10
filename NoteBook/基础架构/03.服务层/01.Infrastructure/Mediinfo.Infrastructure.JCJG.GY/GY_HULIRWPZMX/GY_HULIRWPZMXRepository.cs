using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_HULIRWPZMXRepository : RepositoryBase<GY_HULIRWPZMX>, IGY_HULIRWPZMXRepository
	{
		public GY_HULIRWPZMXRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}


        public GY_HULIRWPZMX GetRWPZMX(string hulirwid, string shujuly, string xiangmuid)
        {
            //hulirwid, shujuly, xiangmuid

            return this.Set<GY_HULIRWPZMX>().Where(item => item.HULIRWID == hulirwid && item.SHUJULY == shujuly && item.XIANGMUID == xiangmuid).FirstOrDefault();

        }
    }
}
