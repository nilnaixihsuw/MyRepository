using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_XIANGMUSPJGFactory
	{
        /*
		 
		public static GY_XIANGMUSPJG CreateIfNotExists(IGY_XIANGMUSPJGRepository irep, ServiceContext sContext, E_GY_XIANGMUSPJG dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_XIANGMUSPJG();
			}  
			return entity;
		}
		*/


        public static GY_XIANGMUSPJG Create(IGY_XIANGMUSPJGRepository irep, ServiceContext sContext, E_GY_XIANGMUSPJG dto)
        {
            GY_XIANGMUSPJG entity = dto.EToDB<E_GY_XIANGMUSPJG, GY_XIANGMUSPJG>();
            entity.Initialize(irep, sContext);
            entity.SHENPIJGID = irep.GetOrder("GY_XIANGMUSPJG")[0];
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();
            irep.RegisterAdd(entity);
            return entity;
        }



    }
}
