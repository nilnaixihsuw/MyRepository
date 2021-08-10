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
        /// ����ת�ﵥ�Ż�ȡ������Ϣ
        /// </summary>
        /// <param name="zhuanZhenDH"></param>
        /// <returns></returns>
        SXZZ_ZHUANZHENGSQD GetByZhuanZhenDH(string zhuanZhenDH);
        /// <summary>
        /// ����sxzz_zzsqd���￨��
        /// </summary>
        /// <param name="jiuZhenKH"></param>
        /// <param name="zhuanZhenSQDH"></param>
        /// <returns></returns>
        int SaveSXZZ_ZZXXJZKH(string jiuZhenKH, string zhuanZhenSQDH);
        /// <summary>
        /// ����SXZZ_ZZXXFSZT��Ժ����״̬
        /// </summary>
        /// <param name="yiBaoID"></param>
        /// <returns></returns>
        int SaveSXZZ_ZZXXFSZT(string zhuZhenSQDH);

        /// <summary>
        /// ����sxzz_zzsqd��Ժ����״̬
        /// </summary>
        /// <param name="yiBaoID"></param>
        /// <returns></returns>
        int SaveSXZZ_ZZSQD(string zhuZhenSQDH);
    }
}
