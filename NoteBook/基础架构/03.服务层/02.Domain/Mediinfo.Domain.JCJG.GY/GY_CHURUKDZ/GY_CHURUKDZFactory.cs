using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_CHURUKDZFactory
	{
		/*
		 
		public static GY_CHURUKDZ CreateIfNotExists(IGY_CHURUKDZRepository irep, ServiceContext sContext, E_GY_CHURUKDZ dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_CHURUKDZ();
			}  
			return entity;
		}
		*/
		        
		
		public static GY_CHURUKDZ Create(IGY_CHURUKDZRepository irep,ServiceContext sContext,E_GY_CHURUKDZ dto)
		{
            GY_CHURUKDZ entity = dto.EToDB<E_GY_CHURUKDZ, GY_CHURUKDZ>();
            entity.Initialize(irep, sContext);
            irep.RegisterAdd<GY_CHURUKDZ>(entity);
			return entity;
		}		        
	}
}
