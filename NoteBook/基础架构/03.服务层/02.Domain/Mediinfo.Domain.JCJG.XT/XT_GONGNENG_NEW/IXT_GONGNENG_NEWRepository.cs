using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.XT
{
	 public interface IXT_GONGNENG_NEWRepository : IRepository<XT_GONGNENG_NEW>, IDependency
	{
        string GetGongNengID(int jiBie, string shangjignid, string xitongid);
        XT_GONGNENG_NEW GetByID(string id);
        XT_GONGNENG_NEW GetByGongNengID(string gongNengID);
    }
}
