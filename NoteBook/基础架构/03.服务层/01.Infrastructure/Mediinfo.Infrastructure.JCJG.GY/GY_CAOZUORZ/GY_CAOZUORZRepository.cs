using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Repository;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_CAOZUORZRepository : RepositoryBase<GY_CAOZUORZ>, IGY_CAOZUORZRepository
	{
		public GY_CAOZUORZRepository(IRepositoryContext context,ServiceContext sContext) :base(context,sContext) {}

        /// <summary>
        /// 获取登陆日志
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sContext"></param>
        /// <returns></returns>
        public GY_CAOZUORZ GetLoginLog()
        {
           var caoZuoRZ  = this.Set<GY_CAOZUORZ>().Where(c => c.YINGYONGID == ServiceContext.YINGYONGID &&
                                                        c.YONGHUID == ServiceContext.USERID &&
                                                        c.CAOZUOXX == "登录成功" &&
                                                        c.GONGZUOZID == ServiceContext.GONGZUOZID &&
                                                        c.RIZHILX == 1
                                                        ).OrderByDescending(c => c.CAOZUORQ)
                                                         .FirstOrDefault();
            return caoZuoRZ;
        }

        public GY_CAOZUORZ GetByID(string ip,string yingyongid)
        {
            var caoZuoRZ = this.Set<GY_CAOZUORZ>().Where(c => c.YINGYONGID == yingyongid &&
                                                      c.IP == ip &&
                                                      c.RIZHILX == 1
                                                       ).OrderByDescending(c => c.CAOZUORQ)
                                                        .FirstOrDefault();
            return caoZuoRZ;

        }
    }
}
