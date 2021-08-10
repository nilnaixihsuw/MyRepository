using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
	 public interface IGY_ZIFUBLRepository : IRepository<GY_ZIFUBL>, IDependency
	{
        /// <summary>
        /// 获取自负比例
        /// </summary>
        /// <param name="feiYongKZID">费用控制ID</param>
        /// <param name="xiangMuID">项目ID</param>
        /// <param name="xiangMuLX">项目类型</param>
        /// <param name="faShengRQ">发生日期</param>
        /// <param name="menZhenZYBZ">门诊住院标志</param>
        /// <returns>返回当前项目的自负比例是否存在</returns>
        bool GetZiFuBL(string feiYongKZID, string xiangMuID, string xiangMuLX,
                       DateTime faShengRQ, int menZhenZYBZ,out Decimal ziFuBL);
        /// <summary>
        /// 获取his1的自付比例
        /// </summary>
        /// <param name="xiangMuID">项目ID</param>
        /// <param name="feiYongXZ">费用性质</param>
        /// <returns></returns>
        bool GetZiFuBL(string xiangMuID, string feiYongXZ,out decimal ziFuBL);

        List<GY_ZIFUBL> GetList(List<string> xiangMuIDS, string feiYongXZ);
         
	}
}
