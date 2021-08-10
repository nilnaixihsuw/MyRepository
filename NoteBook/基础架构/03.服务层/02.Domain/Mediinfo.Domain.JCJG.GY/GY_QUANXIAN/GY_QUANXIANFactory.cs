using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_QUANXIANFactory
	{

        /// <summary>
        /// �ж� ������ �ٴ��� Ȩ����Ϣ
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static GY_QUANXIAN CreateIfNotExists(IGY_QUANXIANRepository irep, ServiceContext sContext, E_GY_QUANXIAN dto)
        {
            var entity = irep.GetByKey("1");
            if (entity == null)
            {
                entity = new GY_QUANXIAN();
                entity.Initialize(irep, sContext);
            }
            return entity;
        }
		/// <summary>
        /// ����Ȩ����Ϣ
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
		public static GY_QUANXIAN Create(IGY_QUANXIANRepository irep,ServiceContext sContext,E_GY_QUANXIAN dto )
		{
			GY_QUANXIAN entity = dto.EToDB<E_GY_QUANXIAN, GY_QUANXIAN>();
            entity.Initialize(irep, sContext);
            //entity.QUANXIANID = irep.GetOrder("GY_QUANXIAN")[0];
            entity.XITONGSJ = irep.GetSYSTime();
            entity.XIUGAIREN = sContext.USERID;
            entity.XIUGAISJ = irep.GetSYSTime();

            irep.RegisterAdd(entity);
            return entity;
		}
		        
	}
}
