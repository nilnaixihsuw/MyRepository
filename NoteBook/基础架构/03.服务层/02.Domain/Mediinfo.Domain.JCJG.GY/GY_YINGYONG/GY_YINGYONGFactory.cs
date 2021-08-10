using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_YINGYONGFactory
	{
        /*
		 
		public static GY_YINGYONG CreateIfNotExists(IGY_YINGYONGRepository irep, ServiceContext sContext, E_GY_YINGYONG dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YINGYONG();
			}  
			return entity;
		}
		*/


        public static GY_YINGYONG Create(IGY_YINGYONGRepository irep, ServiceContext sContext, E_GY_YINGYONG dto)
        {
            GY_YINGYONG entity = dto.EToDB<E_GY_YINGYONG, GY_YINGYONG>();
            entity.Initialize(irep, sContext);

            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
