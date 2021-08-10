using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_KUAIJIQJRepository : IRepository<GY_KUAIJIQJ>, IDependency
	{       
        GY_KUAIJIQJ GetByID(string yingYongID, string kuaiJiRL);
        List<GY_KUAIJIQJ> GetList(string yingYongID, int dangQianBZ = 0);
        List<GY_KUAIJIQJ> GetList(string yingYongID, string nian);
    }
}
