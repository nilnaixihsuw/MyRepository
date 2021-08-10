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

        //新增_抗菌药物审批申请单信息
        public static GY_KANGJUNYWSPSQD Create(IGY_KANGJUNYWSPSQDRepository irep, ServiceContext sContext, E_GY_KANGJUNYWSPSQD dto)
        {
            GY_KANGJUNYWSPSQD entity = dto.EToDB<E_GY_KANGJUNYWSPSQD, GY_KANGJUNYWSPSQD>();

            entity.Initialize(irep, sContext);
            //创建申请单id主键
            if (string.IsNullOrWhiteSpace(entity.SHENQINDANID))
            {
                entity.SHENQINDANID = irep.GetOrder("GY_KANGJUNYWSPSQD", sContext.YUANQUID)[0].ToString();
            }
           
            irep.RegisterAdd(entity);
            return entity;
        }
    }
}
