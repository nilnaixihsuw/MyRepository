using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_JIANYANDMB1Factory
	{
		/*
		 
		public static GY_JIANYANDMB1 CreateIfNotExists(IGY_JIANYANDMB1Repository irep, ServiceContext sContext, E_GY_JIANYANDMB1 dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_JIANYANDMB1();
			}  
			return entity;
		}
		*/
		   
		public static GY_JIANYANDMB1 Create(IGY_JIANYANDMB1Repository irep,ServiceContext sContext,E_GY_JIANYANDMB1 dto )
		{
            //GY_JIANYANDMB1 entity = new GY_JIANYANDMB1();
            GY_JIANYANDMB1 entity = dto.EToDB<E_GY_JIANYANDMB1, GY_JIANYANDMB1>();
            entity.Initialize(irep, sContext);
            if (string.IsNullOrWhiteSpace(entity.JIANYANDGSID))
            {
                entity.JIANYANDGSID = irep.GetOrder("GY_JIANYANDMB1", sContext.YUANQUID)[0].ToString();
            }
            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;
            irep.RegisterAdd(entity);
            return entity;
		}
		 
		
		        
	}
}
