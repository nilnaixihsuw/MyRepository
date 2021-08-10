using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_YUANQUFactory
	{
		/*
		 
		public static GY_YUANQU CreateIfNotExists(IGY_YUANQURepository irep, ServiceContext sContext, E_GY_YUANQU dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YUANQU();
			}  
			return entity;
		}
		*/
		        
		
		public static GY_YUANQU Create(IGY_YUANQURepository irep,ServiceContext sContext,E_GY_YUANQU dto )
		{
            var entity = dto.EToDB<E_GY_YUANQU, GY_YUANQU>();

            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;
            irep.RegisterAdd(entity);
            return entity;
        }
		 
		
		        
	}
}
