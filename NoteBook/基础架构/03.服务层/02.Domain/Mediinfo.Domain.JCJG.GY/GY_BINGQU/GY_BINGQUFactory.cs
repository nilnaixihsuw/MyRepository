using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_BINGQUFactory
	{
		/*
		 
		public static GY_BINGQU CreateIfNotExists(IGY_BINGQURepository irep, ServiceContext sContext, E_GY_BINGQU dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_BINGQU();
			}  
			return entity;
		}
		*/
		   
		public static GY_BINGQU Create(IGY_BINGQURepository irep,ServiceContext sContext,E_GY_BINGQU dto )
		{
            GY_BINGQU entity = dto.EToDB<E_GY_BINGQU, GY_BINGQU>();
            entity.Initialize(irep, sContext);
			return entity;
		}		        
	}
}
