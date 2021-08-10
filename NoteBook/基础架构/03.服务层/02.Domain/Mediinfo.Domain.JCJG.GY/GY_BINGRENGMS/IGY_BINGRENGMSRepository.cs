using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_BINGRENGMSRepository : IRepository<GY_BINGRENGMS>, IDependency
	{
        List<GY_BINGRENGMS> GetList(string laiYuanID);

        /// <summary>
        /// 获取病人过敏史
        /// </summary>
        /// <param name="bingrenID"></param>
        /// <param name="jiageID"></param>
        /// <returns></returns>
        List<GY_BINGRENGMS> GetBingRenGMSList(string bingrenID, string jiageID);

        /// <summary>
        /// 获取病人过敏史
        /// </summary>
        /// <param name="bingrenID"></param>
        /// <returns></returns>
        List<GY_BINGRENGMS> GetBingRenGMSList(string bingrenID);
    }
}
