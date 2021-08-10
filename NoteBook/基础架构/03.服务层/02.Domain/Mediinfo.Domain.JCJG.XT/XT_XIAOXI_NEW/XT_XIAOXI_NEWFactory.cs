using System;
using System.Collections.Generic;
using System.Linq;
using Mediinfo.Enterprise;
using Mediinfo.Utility.Util;

namespace Mediinfo.Domain.JCJG.XT
{
	public static class XT_XIAOXI_NEWFactory
	{
        /*
		 
		public static XT_XIAOXI_NEW CreateIfNotExists(IXT_XIAOXI_NEWRepository irep, ServiceContext sContext, E_XT_XIAOXI_NEW dto)
		{
			var entity =  irep.GetByKey("1");
			if(entity == null)
			{
				    entity = new XT_XIAOXI_NEW();
			}  
			return entity;
		}
		*/


        public static XT_XIAOXI_NEW Create(IXT_XIAOXI_NEWRepository irep, ServiceContext sContext,
            string xiaoXiBM, string xiaoXiMc, string xiaoXiJc, string bingRenID, int menZhenZyBz,
            List<XT_XIAOXIDY_NEW> dingYueList,
            string xiaoXiBT, string xiaoXiZY, object xiaoXiNR,
            string tiXingLx = "", double? youXiaoSj = 0, int? yiCiXBz = 0,
            int? baoMiXxBz = 0, int? youXianJi = 2, int? huiZhiBz = 0, int fuJianBz = 0, string xiaoXiLx = "2", string xiaoXiLY = "")
        {
            DateTime now = irep.GetSYSTime();
            XT_XIAOXI_NEW entity = new XT_XIAOXI_NEW();
            entity.XIAOXIID = long.Parse(irep.GetOrder("XT_XIAOXI_NEW")[0]);
            entity.XIAOXIBM = xiaoXiBM;
            entity.XIAOXIZT = xiaoXiBT;
            entity.BINGRENID = bingRenID;
            entity.MENZHENZYBZ = menZhenZyBz;
            entity.SHOUJIANRID = string.Join("|", dingYueList.Select(m => m.SHOUJIANRID));
            entity.SHOUJIANRXM = string.Join("|", dingYueList.Select(m => m.SHOUJIANRXM));
            entity.XIAOXIZT = xiaoXiBT;
            entity.XIAOXINR = JsonUtil.SerializeObject(xiaoXiNR);
            entity.XIAOXIZY = xiaoXiZY;
            entity.XIAOXITXLX = tiXingLx;
            entity.SHOUJIANREN = JsonUtil.SerializeObject(dingYueList);
            if (youXiaoSj == null || youXiaoSj == 0)
            {
                entity.YOUXIAOQI = DateTime.MaxValue;
            }
            else
            {
                entity.YOUXIAOQI = now.AddMinutes((double)youXiaoSj);
            }
            entity.YICIXBZ = yiCiXBz;
            entity.BAOMIXXBZ = baoMiXxBz;
            entity.YOUXIANJB = youXianJi;
            entity.HUIZHIBZ = huiZhiBz;
            entity.FAJIANRID = sContext.USERID;
            entity.FAJIANRXM = sContext.USERNAME;
            entity.YUEDUBZ = 0;

            entity.FUJIANBZ = fuJianBz;
            entity.XIAOXILX = xiaoXiLx;
            entity.FASONGSJ = now;
            entity.SHANCHUBZ = 0;
            entity.XIAOXIMC = xiaoXiMc;
            entity.XIAOXIJC = xiaoXiJc;

            entity.XIAOXILY = xiaoXiLY;

            return entity;
        }



    }
}
