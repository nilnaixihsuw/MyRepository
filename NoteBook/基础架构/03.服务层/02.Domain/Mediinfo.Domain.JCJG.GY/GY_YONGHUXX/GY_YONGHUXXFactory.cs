using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_YONGHUXXFactory
	{
        /*
		 
		public static GY_YONGHUXX CreateIfNotExists(IGY_YONGHUXXRepository irep, ServiceContext sContext, E_GY_YONGHUXX dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YONGHUXX();
			}  
			return entity;
		}
		*/

        /// <summary>
        /// 新增 公用_用户信息
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static GY_YONGHUXX Create(IGY_YONGHUXXRepository irep, ServiceContext sContext, E_GY_YONGHUXX dto)
        {
            GY_YONGHUXX entity = dto.EToDB<E_GY_YONGHUXX, GY_YONGHUXX>();
            entity.Initialize(irep, sContext);
          
            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
