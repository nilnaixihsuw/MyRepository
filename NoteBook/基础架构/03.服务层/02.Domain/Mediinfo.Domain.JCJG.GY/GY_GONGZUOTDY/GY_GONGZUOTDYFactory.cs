using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_GONGZUOTDYFactory
	{
		/*
		 
		public static GY_GONGZUOTDY CreateIfNotExists(IGY_GONGZUOTDYRepository irep, ServiceContext sContext, E_GY_GONGZUOTDY dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_GONGZUOTDY();
			}  
			return entity;
		}
		*/
		        
		public static GY_GONGZUOTDY Create(IGY_GONGZUOTDYRepository irep,ServiceContext sContext,E_GY_GONGZUOTDY dto )
		{
			GY_GONGZUOTDY entity = new GY_GONGZUOTDY();
            entity = dto.EToDB<E_GY_GONGZUOTDY, GY_GONGZUOTDY>();
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();
            entity.ZUOFEIBZ = 0;
            irep.RegisterAdd(entity);
            return entity;
		}
		        
	}
}
