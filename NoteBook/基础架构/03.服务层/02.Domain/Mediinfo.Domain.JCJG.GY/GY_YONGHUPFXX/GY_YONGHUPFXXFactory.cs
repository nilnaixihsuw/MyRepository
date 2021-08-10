using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_YONGHUPFXXFactory
	{
        /*
		 
		public static GY_YONGHUPFXX CreateIfNotExists(IGY_YONGHUPFXXRepository irep, ServiceContext sContext, E_GY_YONGHUPFXX dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YONGHUPFXX();
			}  
			return entity;
		}
		*/

        
        public static GY_YONGHUPFXX Create(IGY_YONGHUPFXXRepository irep, ServiceContext sContext, E_GY_YONGHUPFXX dto)
        {
            GY_YONGHUPFXX yongHuGRXX = dto.EToDB<E_GY_YONGHUPFXX, GY_YONGHUPFXX>();
            yongHuGRXX.Initialize(irep, sContext);

            irep.RegisterAdd(yongHuGRXX);
            return yongHuGRXX;
        }

    }
}
