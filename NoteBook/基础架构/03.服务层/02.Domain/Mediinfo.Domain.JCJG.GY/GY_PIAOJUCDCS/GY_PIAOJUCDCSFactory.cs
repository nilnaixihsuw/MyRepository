using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_PIAOJUCDCSFactory
	{
        /*
		 
		public static GY_PIAOJUCDCS CreateIfNotExists(IGY_PIAOJUCDCSRepository irep, ServiceContext sContext, E_GY_PIAOJUCDCS dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_PIAOJUCDCS();
			}  
			return entity;
		}
		*/


        public static GY_PIAOJUCDCS Create(IGY_PIAOJUCDCSRepository irep, ServiceContext sContext, E_GY_PIAOJUCDCS dto)
        {
            GY_PIAOJUCDCS entity = dto.EToDB<E_GY_PIAOJUCDCS,GY_PIAOJUCDCS>();
            entity.Initialize(irep, sContext);
            entity.PIAOJUCDCSID = irep.GetOrder("GY_PIAOJUCDCS", sContext.YUANQUID)[0].ToString();
            entity.DAYINRQ = irep.GetSYSTime();
            entity.DAYINREN = sContext.USERID;
            entity.PIAOJUYWXT = sContext.YINGYONGID;
            entity.PIAOJUCDCS = "1";
            irep.RegisterAdd(entity);
            return entity;
        }

     

    }
}
