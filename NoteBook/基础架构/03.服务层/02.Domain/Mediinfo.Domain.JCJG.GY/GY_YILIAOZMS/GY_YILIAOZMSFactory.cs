using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_YILIAOZMSFactory
	{
        /*
		 
		public static GY_YILIAOZMS CreateIfNotExists(IGY_YILIAOZMSRepository irep, ServiceContext sContext, E_GY_YILIAOZMS dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YILIAOZMS();
			}  
			return entity;
		}
		*/

        /*
		public static GY_YILIAOZMS Create(IGY_YILIAOZMSRepository irep,ServiceContext sContext,E_GY_YILIAOZMS dto )
		{
			GY_YILIAOZMS entity = new GY_YILIAOZMS();
			return entity;
		}
		 
		*/

        /// <summary>
        /// 新增医疗证明数据
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>

        public static GY_YILIAOZMS Create(IGY_YILIAOZMSRepository irep, ServiceContext sContext, E_GY_YILIAOZMS dto)
        {
            GY_YILIAOZMS entity = dto.EToDB<E_GY_YILIAOZMS, GY_YILIAOZMS>();

            entity.Initialize(irep, sContext);
            
            irep.RegisterAdd(entity);
            return entity;
        }

    }
}
