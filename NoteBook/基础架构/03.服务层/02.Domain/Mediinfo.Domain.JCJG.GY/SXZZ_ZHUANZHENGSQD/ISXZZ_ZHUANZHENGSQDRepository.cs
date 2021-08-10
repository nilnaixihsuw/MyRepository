using System;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Core.DBEntity;
namespace Mediinfo.Domain.JCJG.GY
{
    public interface ISXZZ_ZHUANZHENGSQDRepository : IRepository<SXZZ_ZHUANZHENGSQD>, IDependency
    {
        /// <summary>
        /// 根据转诊单号获取单号信息
        /// </summary>
        /// <param name="zhuanZhenDH"></param>
        /// <returns></returns>
        SXZZ_ZHUANZHENGSQD GetByZhuanZhenDH(string zhuanZhenDH);
        /// <summary>
        /// 更新sxzz_zzsqd就诊卡号
        /// </summary>
        /// <param name="jiuZhenKH"></param>
        /// <param name="zhuanZhenSQDH"></param>
        /// <returns></returns>
        int SaveSXZZ_ZZXXJZKH(string jiuZhenKH, string zhuanZhenSQDH);
        /// <summary>
        /// 更新SXZZ_ZZXXFSZT离院发送状态
        /// </summary>
        /// <param name="yiBaoID"></param>
        /// <returns></returns>
        int SaveSXZZ_ZZXXFSZT(string zhuZhenSQDH);

        /// <summary>
        /// 更新sxzz_zzsqd离院发送状态
        /// </summary>
        /// <param name="yiBaoID"></param>
        /// <returns></returns>
        int SaveSXZZ_ZZSQD(string zhuZhenSQDH);
    }
}
