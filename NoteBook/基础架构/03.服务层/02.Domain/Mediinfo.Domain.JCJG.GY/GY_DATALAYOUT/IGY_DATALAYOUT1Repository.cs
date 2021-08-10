using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_DATALAYOUT1Repository : IRepository<GY_DATALAYOUT1>, IDependency
	{
        GY_DATALAYOUT1 GetByName(string nameSpace, string formName, string yingYongId, string controlName);

    }
}
