using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_YAOPINTJFLDZRepository : IRepository<GY_YAOPINTJFLDZ>, IDependency
	{
        GY_YAOPINTJFLDZ GetByID(string yingyongid, string tongjifl, string guigeid);
        List<GY_YAOPINTJFLDZ> GetYaoPinTJFLDZListByYingYongIDAndTongJiFl(string yingyongid, string tongjifl);
    }
}
