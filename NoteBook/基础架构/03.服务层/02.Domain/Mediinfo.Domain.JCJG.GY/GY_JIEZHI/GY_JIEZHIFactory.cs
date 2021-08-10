using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public static class GY_JIEZHIFactory
	{
        /*
		 
		public static GY_JIEZHI CreateIfNotExists(IGY_JIEZHIRepository irep, ServiceContext sContext, E_GY_JIEZHI dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new GY_JIEZHI();
			}  
			return entity;
		}
		*/

        /// <summary>
        /// 根据dto创建介质
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static GY_JIEZHI Create(IGY_JIEZHIRepository irep, ServiceContext sContext, E_GY_JIEZHI dto)
        {
            GY_JIEZHI entity = dto.EToDB<E_GY_JIEZHI, GY_JIEZHI>();
            entity.Initialize(irep, sContext);
            if (string.IsNullOrWhiteSpace(entity.JIEZHIID))
            {
                entity.JIEZHIID = irep.GetOrder("GY_JIEZHI", sContext.YUANQUID)[0].ToString();
            }
            if (string.IsNullOrWhiteSpace(entity.ZHIKAREN))
            {
                entity.ZHIKAREN = sContext.USERID;
                entity.ZHIKARQ = irep.GetSYSTime();
            }

            if (string.IsNullOrWhiteSpace(entity.XIUGAIREN))
            {
                entity.XIUGAIREN = sContext.USERID;
                entity.XIUGAISJ = irep.GetSYSTime();
            }
            entity.ZUOFEIBZ = 0; 
            irep.RegisterAdd(entity);
            return entity;
             
        }
        /// <summary>
        /// 创建介质
        /// </summary>
        /// <param name="irep"></param>
        /// <param name="sContext"></param>
        /// <param name="jieZhiHao"></param>
        /// <param name="jieZhiLX"></param>
        /// <param name="bingRenID"></param>
        /// <param name="yeWuLX"></param>
        /// <returns></returns>
        public static GY_JIEZHI Create(IGY_JIEZHIRepository irep, ServiceContext sContext,string jieZhiHao,string jieZhiLX,
            string bingRenID,int yeWuLX)
        {
            GY_JIEZHI entity = new GY_JIEZHI();
            entity.JIEZHIHAO = jieZhiHao;
            entity.LEIXING = jieZhiLX;
            entity.BINGRENID = bingRenID;
            entity.YEWULX = yeWuLX;
            entity.JIEZHIID = irep.GetOrder("GY_JIEZHI", sContext.YUANQUID)[0].ToString(); 
            entity.ZHIKAREN = sContext.USERID;
            entity.ZHIKARQ = irep.GetSYSTime(); 
            entity.XIUGAIREN = sContext.USERID; 
            entity.ZUOFEIBZ = 0;
            entity.JIEZHIZT = "0";
            irep.RegisterAdd(entity);
            return entity;

        }

    }
}
