using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
    public static class GY_YAOPINLDFactory
    {
        /*
		 
		public static GY_YAOPINLD CreateIfNotExists(IGY_YAOPINLDRepository irep, ServiceContext sContext, E_GY_YAOPINLD dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YAOPINLD();
			}  
			return entity;
		}
		*/

        public static GY_YAOPINLD Create(IGY_YAOPINLDRepository irep, ServiceContext sContext, E_GY_YAOPINLD dto)
        {
            GY_YAOPINLD entity = dto.EToDB<E_GY_YAOPINLD, GY_YAOPINLD>();
            entity.YAOPINLDID = irep.GetOrder("GY_YAOPINLD", sContext.YUANQUID)[0].ToString();            
            entity.Initialize(irep, sContext);
            irep.RegisterAdd(entity);
            return entity;
        }
    }
}
