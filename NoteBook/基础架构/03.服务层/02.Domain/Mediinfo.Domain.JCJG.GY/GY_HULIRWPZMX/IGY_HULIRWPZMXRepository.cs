using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_HULIRWPZMXRepository : IRepository<GY_HULIRWPZMX>, IDependency
	{
        GY_HULIRWPZMX GetRWPZMX(string hulirwid, string shujuly, string xiangmuid);
    }
}
