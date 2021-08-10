using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Enterprise.Exceptions;
using Mediinfo.Infrastructure.Core.Domain;
using System;
using System.Linq;

namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_YAOPINCDJG2Factory
	{
        /*
		 
		public static GY_YAOPINCDJG2 CreateIfNotExists(IGY_YAOPINCDJG2Repository irep, ServiceContext sContext, E_GY_YAOPINCDJG2 dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YAOPINCDJG2();
			}  
			return entity;
		}
		*/

        /*
		public static GY_YAOPINCDJG2 Create(IGY_YAOPINCDJG2Repository irep,ServiceContext sContext,E_GY_YAOPINCDJG2 dto )
		{
			GY_YAOPINCDJG2 entity = new GY_YAOPINCDJG2();
			return entity;
		}
		 
		*/


        public static GY_YAOPINCDJG2 Create(IGY_YAOPINCDJG2Repository irep, ServiceContext sContext, E_GY_YAOPINCDJG2_EX EYaoPinJG )
        {
            var yaoPinCDJG = irep.GetList( EYaoPinJG.GUIGEID , EYaoPinJG.CHANDI).FirstOrDefault();

            if (yaoPinCDJG != null)
            {
                throw new DomainException("[" + EYaoPinJG.CHANDIMC + "]药品产地记录已经存在不能新增！");
            } 
            
            var yaoPinJG = EYaoPinJG.EToDB<E_GY_YAOPINCDJG2_EX, GY_YAOPINCDJG2>();
            yaoPinJG.XIUGAISJ = irep.GetSYSTime();
            yaoPinJG.XIUGAIREN = sContext.USERID;
            yaoPinJG.JIAGEID = irep.GetOrder("GY_YAOPINCDJG2", sContext.YUANQUID, 1)[0];
            yaoPinJG.Initialize(irep, sContext);

            irep.RegisterAdd(yaoPinJG);
            return yaoPinJG;
        }


        /// <summary>
        /// 通过entity生成药品产地信息
        /// </summary>
        /// <param name="DBContext"></param>
        /// <param name="sContext"></param>
        /// <param name="yaoPinJGEntity"></param>
        /// <returns></returns>
        public static GY_YAOPINCDJG2 CreateYaoPinCDJG2(IGY_YAOPINCDJG2Repository irep , ServiceContext sContext, GY_YAOPINCDJG2 yaoPinJGEntity)
        {
            //拆分产地
            var yaoPinCDJGEntity = yaoPinJGEntity.DBToDB<GY_YAOPINCDJG2, GY_YAOPINCDJG2>(); //拆分前以大规格的产地价格为模板拆分小规格
            yaoPinCDJGEntity.Initialize(irep, sContext); 

            yaoPinCDJGEntity.JIAGEID = irep.GetOrder("GY_YAOPINCDJG2", sContext.YUANQUID, 1)[0];

            irep.RegisterAdd(yaoPinCDJGEntity);
            return yaoPinCDJGEntity;
        }


    }
}
