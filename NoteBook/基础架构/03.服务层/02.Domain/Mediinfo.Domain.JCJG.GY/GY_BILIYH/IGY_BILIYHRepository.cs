using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	public interface IGY_BILIYHRepository : IRepository<GY_BILIYH>, IDependency
	{
        /// <summary>
        /// 通过主键获取（对于复合主键需要重载）
        /// </summary>
        /// <param name="youHuiLB">优惠类别</param>
        /// <param name="xiangMuId">项目Id</param>
        /// <param name="xiangMuLX">项目类型</param>
        /// <returns></returns>
        GY_BILIYH GetByKey(string youHuiLB, string xiangMuId, string xiangMuLX);
        List<GY_BILIYH> GetList();
	}
}
