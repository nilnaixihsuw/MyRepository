using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using  Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	public interface IGY_FEIYONGKZRepository : IRepository<GY_FEIYONGKZ>, IDependency
	{
        /// <summary>
        /// 获取费用控制（含已作废）
        /// </summary>
        /// <param name="feiYongXZ"></param>
        /// <returns></returns>
        GY_FEIYONGKZ Get(string feiYongXZ);

        /// <summary>
        /// 获取费用控制（含已作废）
        /// </summary>
        /// <param name="kaiDanYYID"></param>
        /// <param name="feiYongXZ"></param>
        /// <returns></returns>
        GY_FEIYONGKZ Get(string yingYongId, string feiYongXZ);


        decimal GetHis1DanJia(string xiangMuXX_XML,ref int jiaGeTX);
	}
}
