using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_JUESEBBFactory
	{
        /*
		 
		public static GY_JUESEBB CreateIfNotExists(IGY_JUESEBBRepository irep, ServiceContext sContext, E_GY_JUESEBB dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_JUESEBB();
			}  
			return entity;
		}
		*/


        public static GY_JUESEBB Create(IGY_JUESEBBRepository irep, ServiceContext sContext, E_GY_JUESEBB dto)
        {           
            GY_JUESEBB entity = dto.EToDB<E_GY_JUESEBB, GY_JUESEBB>();
            entity.Initialize(irep, sContext);
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();
            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
