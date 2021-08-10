using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_YONGHUYYFactory
	{
        /*
		 
		public static GY_YONGHUYY CreateIfNotExists(IGY_YONGHUYYRepository irep, ServiceContext sContext, E_GY_YONGHUYY dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YONGHUYY();
			}  
			return entity;
		}
		*/

        /// <summary>
        ///  新增 公用_用户应用
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static GY_YONGHUYY Create(IGY_YONGHUYYRepository irep, ServiceContext sContext, E_GY_YONGHUYY dto)
        {
            
            GY_YONGHUYY entity = dto.EToDB<E_GY_YONGHUYY, GY_YONGHUYY>();
            entity.Initialize(irep, sContext);

            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
