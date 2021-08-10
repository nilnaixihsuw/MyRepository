using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using Mediinfo.Utility.Extensions;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_YILIAOZMFactory
	{
        /*
		 
		public static GY_YILIAOZM CreateIfNotExists(IGY_YILIAOZMRepository irep, ServiceContext sContext, E_GY_YILIAOZM dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_YILIAOZM();
			}  
			return entity;
		}
		*/

        /*
		public static GY_YILIAOZM Create(IGY_YILIAOZMRepository irep,ServiceContext sContext,E_GY_YILIAOZM dto )
		{
			GY_YILIAOZM entity = new GY_YILIAOZM();
			return entity;
		}
		 
		*/
        /// <summary>
        /// 新增医疗证明数据
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>

        public static GY_YILIAOZM Create(IGY_YILIAOZMRepository irep, ServiceContext sContext, E_GY_YILIAOZM dto)
        {
            GY_YILIAOZM entity = dto.EToDB<E_GY_YILIAOZM, GY_YILIAOZM>();
            if (entity.YILIAOZMID.IsNullOrWhiteSpace())
            {
                entity.YILIAOZMID = GetYiLiaoZMID(entity.YILIAOZMID ?? "1",
                    irep.GetOrder( "ZJ_YILIAOZM", sContext.YUANQUID)[0].ToString().Substring(1, 9));
            }
            entity.RIQI = irep.GetSYSTime();
            entity.Initialize(irep, sContext);
            irep.RegisterAdd(entity);
            return entity;
        }

        public static string GetYiLiaoZMID(string zhengMingLX,string yiLiaoZMID)
        { 
            switch (zhengMingLX)
            {
                case "1":
                    zhengMingLX = "B";
                    break;
                case "2":
                    zhengMingLX = "Z";
                    break;
                case "3":
                    zhengMingLX = "H";
                    break;
                case "4":
                    zhengMingLX = "Y";
                    break;
                case "5":
                    zhengMingLX = "F";
                    break;
            }
            return zhengMingLX+ yiLiaoZMID;
        }
    }
}
