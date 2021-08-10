using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.DBEntity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class SXZZ_ZHUANZHENGSQDRepository : RepositoryBase<SXZZ_ZHUANZHENGSQD>, ISXZZ_ZHUANZHENGSQDRepository
	{
		public SXZZ_ZHUANZHENGSQDRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) { }

        public SXZZ_ZHUANZHENGSQD GetByZhuanZhenDH(string zhuanZhenDH)
        {
            return this.Set<SXZZ_ZHUANZHENGSQD>().Where(w => w.ZHUANZHENDH == zhuanZhenDH).FirstOrDefaultWithContext(this, ServiceContext);
        }

        /// <summary>
        /// 更新Sxzz_Zzsqd就诊卡号发送状态
        /// </summary>
        /// <param name="yiBaoID"></param>
        /// <returns></returns>
        public int SaveSXZZ_ZZXXJZKH(string jiuZhenKH,string zhuanZhenSQDH)
        {
            int result = 0;
            result = this.ExecuteSqlCommand("Update Sxzz_Zzsqd Set Jzkh = :jiuZhenKH Where ZzSqDh = :zhuZhenSQDH", jiuZhenKH,zhuanZhenSQDH);
            return result;
        }
        /// <summary>
        /// 更新SXZZ_ZZXXFSZT离院发送状态
        /// </summary>
        /// <param name="yiBaoID"></param>
        /// <returns></returns>
        public int SaveSXZZ_ZZXXFSZT(string zhuZhenSQDH)
        {
            int result = 0;
            if (!string.IsNullOrWhiteSpace(zhuZhenSQDH))
                result = this.ExecuteSqlCommand("Update SXZZ_ZZXXFSZT Set lyfszt = 1 ,lyfsrq = sysdate Where zzsqdh = :zhuZhenSQDH", zhuZhenSQDH);
            return result;
        }

        /// <summary>
        /// 更新sxzz_zzsqd离院发送状态
        /// </summary>
        /// <param name="yiBaoID"></param>
        /// <returns></returns>
        public int SaveSXZZ_ZZSQD(string zhuZhenSQDH)
        {
            int result = 0;
            if (!string.IsNullOrWhiteSpace(zhuZhenSQDH))
                result = this.ExecuteSqlCommand("update sxzz_zzsqd set brzt = 4 where zzsqdh = :zhuZhenSQDH", zhuZhenSQDH);
            return result;
        }
    }
}
