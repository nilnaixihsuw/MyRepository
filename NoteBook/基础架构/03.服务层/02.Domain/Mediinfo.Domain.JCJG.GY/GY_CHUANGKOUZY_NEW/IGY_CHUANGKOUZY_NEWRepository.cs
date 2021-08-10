using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_CHUANGKOUZY_NEWRepository : IRepository<GY_CHUANGKOUZY_NEW>, IDependency
	{
        List<GY_CHUANGKOUZY_NEW> GetList(string nameSpace, string formName, string controlName);  
        List<GY_CHUANGKOUZY_NEW> GetList(string nameSpace, string formName);
    }

    
}
