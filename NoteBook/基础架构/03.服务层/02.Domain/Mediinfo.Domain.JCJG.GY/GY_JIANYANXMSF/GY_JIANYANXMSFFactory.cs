using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_JIANYANXMSFFactory
	{
		/*
		 
		public static GY_JIANYANXMSF CreateIfNotExists(IGY_JIANYANXMSFRepository irep, ServiceContext sContext, E_GY_JIANYANXMSF dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_JIANYANXMSF();
			}  
			return entity;
		}
		*/
		        
		
		public static GY_JIANYANXMSF Create(IGY_JIANYANXMSFRepository irep,ServiceContext sContext, E_GY_JIANYANXMSFXX dto )
		{

            GY_JIANYANXMSF entity = dto.EToDB<E_GY_JIANYANXMSFXX, GY_JIANYANXMSF>();
            entity.Initialize(irep, sContext);
            irep.RegisterAdd(entity);
            return entity;
        }
		 
		
		        
	}
}
