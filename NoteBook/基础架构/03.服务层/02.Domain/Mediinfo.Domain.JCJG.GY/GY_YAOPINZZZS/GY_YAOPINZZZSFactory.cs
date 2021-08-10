using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_YAOPINZZZSFactory
	{
        /*
		 
		public static GY_YAOPINZZZS CreateIfNotExists(IGY_YAOPINZZZSRepository irep, ServiceContext sContext, E_GY_YAOPINZZZS dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YAOPINZZZS();
			}  
			return entity;
		}
		*/

        /// <summary>
        /// 新增 公用_药品资质证书
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static GY_YAOPINZZZS Create(IGY_YAOPINZZZSRepository irep, ServiceContext sContext, E_GY_YAOPINZZZS dto)
        {
            GY_YAOPINZZZS entity = dto.EToDB<E_GY_YAOPINZZZS, GY_YAOPINZZZS>();
            entity.Initialize(irep, sContext);

            entity.ZHENGSHUMXID = irep.GetOrder("GY_YAOPINZZZS", sContext.YUANQUID)[0].ToString();

            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
