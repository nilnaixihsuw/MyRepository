using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_HUIZHENDANFactory
	{
		/*
		 
		public static GY_HUIZHENDAN CreateIfNotExists(IGY_HUIZHENDANRepository irep, ServiceContext sContext, E_GY_HUIZHENDAN dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_HUIZHENDAN();
			}  
			return entity;
		}
		*/
		        
		
		public static GY_HUIZHENDAN Create(IGY_HUIZHENDANRepository irep,ServiceContext sContext,E_GY_HUIZHENDAN_EX dto )
		{
			GY_HUIZHENDAN entity = dto.EToDB<E_GY_HUIZHENDAN_EX, GY_HUIZHENDAN>();
            entity.Initialize(irep, sContext);
            if (string.IsNullOrWhiteSpace(entity.HUIZHENDID))
            {
                entity.HUIZHENDID = irep.GetOrder("GY_HUIZHENDAN", sContext.YUANQUID)[0].ToString();
            }
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();
            irep.RegisterAdd(entity);
            return entity;
		}
		 
		
		        
	}
}
