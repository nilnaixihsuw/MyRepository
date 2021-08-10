using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_KESHIFactory
	{
		/*
		 
		public static GY_KESHI CreateIfNotExists(IGY_KESHIRepository irep, ServiceContext sContext, E_GY_KESHI dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_KESHI();
			}  
			return entity;
		}
		*/
		        
		public static GY_KESHI Create(IGY_KESHIRepository irep,ServiceContext sContext,E_GY_KESHI dto )
		{
            GY_KESHI entity = dto.EToDB<E_GY_KESHI, GY_KESHI>();
            entity.Initialize(irep, sContext);
			return entity;
		}		        
	}
}
