using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_CHUANGKOUCSDYJLRepository : RepositoryBase<GY_CHUANGKOUCSDYJL>, IGY_CHUANGKOUCSDYJLRepository
	{
		public GY_CHUANGKOUCSDYJLRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// 插入或者更新窗口参数调用记录
        /// </summary>
        /// <param name="chuangKouMc"></param>
        /// <param name="jlList"></param>
        /// <returns></returns>
        public void InsertOrUpdate(List<GY_CHUANGKOUCSDYJL> jlList)
        {
            foreach (var item in jlList)
            {
                var exists = this.Set<GY_CHUANGKOUCSDYJL>().Where(m => m.CHUANGKOUMC == item.CHUANGKOUMC && m.YINGYONGID == item.YINGYONGID).Any();
                if(exists)
                {
                    this.RegisterUpdate(item);
                    
                }
                else
                {
                    this.RegisterAdd(item);
                }
            }
        }
    }
}
