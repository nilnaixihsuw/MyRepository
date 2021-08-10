using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_CHUANGWEITCMXFactory
	{
        /*
		 
		public static GY_CHUANGWEITCMX CreateIfNotExists(IGY_CHUANGWEITCMXRepository irep, ServiceContext sContext, E_GY_CHUANGWEITCMX dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_CHUANGWEITCMX();
			}  
			return entity;
		}
		*/

        /*
		public static GY_CHUANGWEITCMX Create(IGY_CHUANGWEITCMXRepository irep,ServiceContext sContext,E_GY_CHUANGWEITCMX dto )
		{
			GY_CHUANGWEITCMX entity = new GY_CHUANGWEITCMX();
			return entity;
		}
		 
		*/

        public static GY_CHUANGWEITCMX Create(IGY_CHUANGWEITCMXRepository irep, ServiceContext sContext, E_GY_CHUANGWEITCMX_EX dto)
        {
            var dto1 = dto.EToE<E_GY_CHUANGWEITCMX_EX, E_GY_CHUANGWEITCMX>();
            GY_CHUANGWEITCMX entity = dto1.EToDB<E_GY_CHUANGWEITCMX, GY_CHUANGWEITCMX>();
            entity.Initialize(irep, sContext);

            entity.CHUANGWEITCMXID = irep.GetOrder("GY_CHUANGWEITCMX", sContext.YUANQUID)[0].ToString();

            entity.XINGZHISX= string.Format("{0}{1}{2}{3}{4}", dto.PUTONG, dto.BAOCHUANG, dto.BAOFANG, dto.YINGER, "000000"); 
            entity.XIUGAISJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;

            irep.RegisterAdd(entity);
            return entity;
        }

    }
}
