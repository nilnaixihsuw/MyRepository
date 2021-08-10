using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_CANSHUFactory
	{
        /*
		 
		public static GY_CANSHU CreateIfNotExists(IGY_CANSHURepository irep, ServiceContext sContext, E_GY_CANSHU dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_CANSHU();
			}  
			return entity;
		}
		*/

        
		public static GY_CANSHU Create(IGY_CANSHURepository irep,ServiceContext sContext,E_GY_CANSHU dto )
		{
			GY_CANSHU entity = dto.EToDB<E_GY_CANSHU, GY_CANSHU>();
            entity.Initialize(irep, sContext);
            irep.RegisterAdd(entity);
            return entity;
		} 
    }
}
