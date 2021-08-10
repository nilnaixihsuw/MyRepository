using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_JIANYANXMFactory
	{
		/*
		 
		public static GY_JIANYANXM CreateIfNotExists(IGY_JIANYANXMRepository irep, ServiceContext sContext, E_GY_JIANYANXM dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_JIANYANXM();
			}  
			return entity;
		}
		*/
		        
		
		public static GY_JIANYANXM Create(IGY_JIANYANXMRepository irep,ServiceContext sContext,E_GY_JIANYANXM dto )
		{
            GY_JIANYANXM entity = dto.EToDB<E_GY_JIANYANXM, GY_JIANYANXM>();
            entity.Initialize(irep, sContext);
            if (string.IsNullOrWhiteSpace(entity.JIANYANXMID))
            {
                entity.JIANYANXMID = irep.GetOrder("GY_JIANYANXM", sContext.YUANQUID)[0].ToString();
            }
            irep.RegisterAdd(entity);
            return entity;
        }
		 
		
		        
	}
}
