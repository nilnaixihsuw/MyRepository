using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_PIAOJULYRepository : IRepository<GY_PIAOJULY>, IDependency
	{
        List<GY_PIAOJULY> GetPiaoJuLY(string zhuangTai);

        List<GY_PIAOJULY> GetPiaoJuLY(string zhuangTai, string leiXing);


    }
}
