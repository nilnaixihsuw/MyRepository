using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_KANGJUNYWSPSQDFactory
	{
        /*
		 
		public static GY_KANGJUNYWSPSQD CreateIfNotExists(IGY_KANGJUNYWSPSQDRepository irep, ServiceContext sContext, E_GY_KANGJUNYWSPSQD dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_KANGJUNYWSPSQD();
			}  
			return entity;
		}
		*/

        //����_����ҩ���������뵥��Ϣ
        public static GY_KANGJUNYWSPSQD Create(IGY_KANGJUNYWSPSQDRepository irep, ServiceContext sContext, E_GY_KANGJUNYWSPSQD dto)
        {
            GY_KANGJUNYWSPSQD entity = dto.EToDB<E_GY_KANGJUNYWSPSQD, GY_KANGJUNYWSPSQD>();

            entity.Initialize(irep, sContext);
            //�������뵥id����
            if (string.IsNullOrWhiteSpace(entity.SHENQINDANID))
            {
                entity.SHENQINDANID = irep.GetOrder("GY_KANGJUNYWSPSQD", sContext.YUANQUID)[0].ToString();
            }
           
            irep.RegisterAdd(entity);
            return entity;
        }
    }
}
