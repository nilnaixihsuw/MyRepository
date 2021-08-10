using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_ZHIGONGKSFactory
	{
        /*
		 
		public static GY_ZHIGONGKS CreateIfNotExists(IGY_ZHIGONGKSRepository irep, ServiceContext sContext, E_GY_ZHIGONGKS dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_ZHIGONGKS();
			}  
			return entity;
		}
		*/

        /// <summary>
        /// 新增 公用_职工科室
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static GY_ZHIGONGKS Create(IGY_ZHIGONGKSRepository irep, ServiceContext sContext, E_GY_ZHIGONGKS dto)
        {
            GY_ZHIGONGKS entity = dto.EToDB<E_GY_ZHIGONGKS, GY_ZHIGONGKS>();
            entity.Initialize(irep, sContext);

            
            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
