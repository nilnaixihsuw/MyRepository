using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_BINGRENXXFactory
	{
		/*
		 
		public static GY_BINGRENXX CreateIfNotExists(IGY_BINGRENXXRepository irep, ServiceContext sContext, E_GY_BINGRENXX dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_BINGRENXX();
			}  
			return entity;
		}
		*/
		        
		 
		public static GY_BINGRENXX Create(IGY_BINGRENXXRepository irep,ServiceContext sContext,E_GY_BINGRENXX dto )
		{
			GY_BINGRENXX entity = dto.EToDB<E_GY_BINGRENXX, GY_BINGRENXX>();
            entity.Initialize(irep, sContext);
            if (string.IsNullOrWhiteSpace(entity.BINGRENID))
            {
                entity.BINGRENID = irep.GetOrder("GY_BINGRENXX", sContext.YUANQUID)[0].ToString();
            } 
            entity.JIANDANGREN = sContext.USERID;
            entity.JIANDANGRQ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();
            irep.RegisterAdd(entity);
            return entity;
		}
		 
		 
		        
	}
}
