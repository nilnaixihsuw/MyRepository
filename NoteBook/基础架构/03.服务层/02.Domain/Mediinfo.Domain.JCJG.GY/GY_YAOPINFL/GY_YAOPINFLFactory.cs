using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_YAOPINFLFactory
	{
        /*
		 
		public static GY_YAOPINFL CreateIfNotExists(IGY_YAOPINFLRepository irep, ServiceContext sContext, E_GY_YAOPINFL dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YAOPINFL();
			}  
			return entity;
		}
		*/

        /// <summary>
        /// 新增 公用_药品分类
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="EYaoPinFL"></param>
        /// <returns></returns>
        public static GY_YAOPINFL Create(IGY_YAOPINFLRepository irep, ServiceContext sContext, E_GY_YAOPINFL EYaoPinFL)
        {
          
            var yaoPinFL = EYaoPinFL.EToDB<E_GY_YAOPINFL, GY_YAOPINFL>();

            yaoPinFL.YAOPINFLID = irep.GetOrder("GY_YAOPINFL", sContext.YUANQUID, 1)[0];
            yaoPinFL.XIUGAISJ = irep.GetSYSTime();
            yaoPinFL.XIUGAIREN = sContext.USERID;
            if (yaoPinFL.YAOPINGLID == "*")
            {
                yaoPinFL.YAOPINGLID = yaoPinFL.YAOPINFLID;
            }
            irep.RegisterAdd(yaoPinFL);
            return yaoPinFL;
        }

    }
}
