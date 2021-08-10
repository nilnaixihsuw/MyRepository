using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public static class GY_CHUANGWEITCFactory
    {
        /*
		 
		public static GY_CHUANGWEITC CreateIfNotExists(IGY_CHUANGWEITCRepository irep, ServiceContext sContext, E_GY_CHUANGWEITC dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_CHUANGWEITC();
			}  
			return entity;
		}
		*/

        /*
		public static GY_CHUANGWEITC Create(IGY_CHUANGWEITCRepository irep,ServiceContext sContext,E_GY_CHUANGWEITC dto )
		{
			GY_CHUANGWEITC entity = new GY_CHUANGWEITC();
			return entity;
		}
		 
		*/
        public static GY_CHUANGWEITC Create(IGY_CHUANGWEITCRepository irep, ServiceContext sContext, E_GY_CHUANGWEITC dto)
        {
            GY_CHUANGWEITC entity = dto.EToDB<E_GY_CHUANGWEITC, GY_CHUANGWEITC>();
            entity.Initialize(irep, sContext);

            entity.CHUANGWEITCID = irep.GetOrder("GY_CHUANGWEITC", sContext.YUANQUID)[0].ToString();
            //jieSuan2Entity.JIESUANMXID = irep.GetOrder("ZY_JIESUAN2", sContext.YUANQUID)[0].ToString();

            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            irep.RegisterAdd(entity);
            return entity;
        }
    }
}
