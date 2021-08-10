using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_HUIZHENDANRepository : IRepository<GY_HUIZHENDAN>, IDependency
	{
        /// <summary>
        /// ∞¥’’“Ω÷ˆid»°y_huizhendan
        /// </summary>
        /// <param name="yiZhuID"></param>
        /// <returns></returns>
        List<GY_HUIZHENDAN> GetList(string yiZhuID);

    }
}
