using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_DANJUDXXXFactory
	{
        /*
		 
		public static GY_DANJUDXXX CreateIfNotExists(IGY_DANJUDXXXRepository irep, ServiceContext sContext, E_GY_DANJUDXXX dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_DANJUDXXX();
			}  
			return entity;
		}
		*/


        public static GY_DANJUDXXX Create(IGY_DANJUDXXXRepository irep, ServiceContext sContext, E_GY_DANJUDXXX dto)
        {
            var danJuDXXX = dto.EToDB<E_GY_DANJUDXXX, GY_DANJUDXXX>();
            if (string.IsNullOrEmpty(danJuDXXX.DANJUDXID))
            {
                //�����п������ֳ������ϴ������Բ�ʹ�����У�����id�� Ӧ��idǰ��λ+yyyyMMddHHmmss
                danJuDXXX.DANJUDXID = danJuDXXX.YINGYONGID.Substring(0,2)+irep.GetSYSTime().ToString("yyyyMMddHHmmss");
            }
            danJuDXXX.XIUGAIREN = sContext.USERID;
            danJuDXXX.XIUGAISJ = irep.GetSYSTime();
            danJuDXXX.Initialize(irep, sContext);
            irep.RegisterAdd(danJuDXXX);
            return danJuDXXX;
        }



    }
}
