using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_BAOBIAOYHFactory
	{
        /*
		 
		public static GY_BAOBIAOYH CreateIfNotExists(IGY_BAOBIAOYHRepository irep, ServiceContext sContext, E_GY_BAOBIAOYH dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_BAOBIAOYH();
			}  
			return entity;
		}
		*/


        public static GY_BAOBIAOYH Create(IGY_BAOBIAOYHRepository irep, ServiceContext sContext, E_GY_BAOBIAOYH dto)
        {
            GY_BAOBIAOYH entity = dto.EToDB<E_GY_BAOBIAOYH, GY_BAOBIAOYH>();
            entity.Initialize(irep, sContext);
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();
            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
