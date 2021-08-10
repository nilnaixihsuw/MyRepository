using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_GUANDAOYZXMDYFactory
	{
        /*
		 
		public static GY_GUANDAOYZXMDY CreateIfNotExists(IGY_GUANDAOYZXMDYRepository irep, ServiceContext sContext, E_GY_GUANDAOYZXMDY dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_GUANDAOYZXMDY();
			}  
			return entity;
		}
		*/


        public static GY_GUANDAOYZXMDY Create(IGY_GUANDAOYZXMDYRepository irep, ServiceContext sContext, E_GY_GUANDAOYZXMDY_EX dto)
        {
            GY_GUANDAOYZXMDY entity = new GY_GUANDAOYZXMDY();
            entity = dto.EToDB<E_GY_GUANDAOYZXMDY_EX, GY_GUANDAOYZXMDY>();
            entity.Initialize(irep, sContext);
            irep.RegisterAdd(entity);
            return entity;
        }




    }
}
