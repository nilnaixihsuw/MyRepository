using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_CUOWURZFactory
	{
        /*
		 
		public static GY_CUOWURZ CreateIfNotExists(IGY_CUOWURZRepository irep, ServiceContext sContext, E_GY_CUOWURZ dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_CUOWURZ();
			}  
			return entity;
		}
		*/

        /*
		public static GY_CUOWURZ Create(IGY_CUOWURZRepository irep,ServiceContext sContext,E_GY_CUOWURZ dto )
		{
			GY_CUOWURZ entity = new GY_CUOWURZ();
			return entity;
		}
		 
		*/


        public static GY_CUOWURZ Create(IGY_CUOWURZRepository irep, ServiceContext sContext, E_GY_CUOWURZ dto)
        {

            //��ߵ���־id����������domainservice���渳ֵ��addby chenchao 
            dto.FASHENGRQ = irep.GetSYSTime();
      
            var zyBingRenXX = dto.EToDB<E_GY_CUOWURZ, GY_CUOWURZ>();
            zyBingRenXX.Initialize(irep, sContext);
            irep.RegisterAdd(zyBingRenXX);
            return zyBingRenXX;
        }
    }
}
