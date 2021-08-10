using Mediinfo.Enterprise;
using Mediinfo.DTO.HIS.GY;
using System;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_ZHIGONGXXFactory
	{
        /*
		 
		public static GY_ZHIGONGXX CreateIfNotExists(IGY_ZHIGONGXXRepository irep, ServiceContext sContext, E_GY_ZHIGONGXX dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_ZHIGONGXX();
			}  
			return entity;
		}
		*/

        /// <summary>
        /// 新增 职工信息
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static GY_ZHIGONGXX Create(IGY_ZHIGONGXXRepository irep, ServiceContext sContext, E_GY_ZHIGONGXX dto)
        {
            GY_ZHIGONGXX entity = dto.EToDB<E_GY_ZHIGONGXX, GY_ZHIGONGXX>();
            entity.Initialize(irep, sContext);

            entity.ZHIGONGID = dto.ZHIGONGGH;
            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
