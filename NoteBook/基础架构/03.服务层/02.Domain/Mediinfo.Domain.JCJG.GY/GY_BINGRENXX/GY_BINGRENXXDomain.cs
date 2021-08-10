using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_BINGRENXX
	{
        /// <summary>
        /// 资金账户启用，将资金账户启用标志设为1
        /// </summary>
        /// <param name="eGYBingRenXXDTO"></param>
        /// <returns></returns>
        public GY_BINGRENXX ZiJinZHQY()
        {
            ZIJINZHQYBZ = 1;
            //this.RegisterUpdate(this);
            return this;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="e_BingRenXX"></param>
        /// <returns></returns>
        public GY_BINGRENXX Update(E_GY_BINGRENXX e_BingRenXX)
        {
            this.MargeDTO<GY_BINGRENXX, E_GY_BINGRENXX>(e_BingRenXX);
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();
            return this.RegisterUpdate(this);
             
        }

        public GY_BINGRENXX Update()
        {
            return this.RegisterUpdate(this);
        }

        /// <summary>
        /// 更新病历状态
        /// </summary>
        /// <param name="e_BingRenXX"></param>
        /// <returns></returns>
        public GY_BINGRENXX UpdateBingLiZT(E_GY_BINGRENXX e_BingRenXX)
        {
            // this.MargeDTO<GY_BINGRENXX, E_GY_BINGRENXX>(e_BingRenXX);
            this.JIBINGFL = e_BingRenXX.JIBINGFL;
            this.BINGLISLZT = e_BingRenXX.BINGLISLZT;
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();
            return this;
        }


        /// <summary>
        /// 删除
        /// </summary>
        public GY_BINGRENXX Delete()
        {
            IRepositoyBase.RegisterDelete<GY_BINGRENXX>(this);
            return this;
        }

        public GY_BINGRENXX GengXinZYCS(int? zhuYuanCS)
        {
            this.ZHUYUANCS = zhuYuanCS;
            this.RegisterUpdate(this);
            return this;
        }
        /// <summary>
        /// 病例合并时身份确认
        /// </summary>
        /// <returns></returns>
        public GY_BINGRENXX ShengFenQR(int shenFenQRBZ)
        {

            this.SHENFENQRBZ = shenFenQRBZ;
            this.RegisterUpdate<GY_BINGRENXX>(this);
            return this;
        }
        public GY_BINGRENXX GengXinMZCS(int? menZhenCS)
        {
            this.MENZHENCS = menZhenCS;
            this.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 增加门诊次数
        /// </summary>
        /// <returns></returns>
        public GY_BINGRENXX ZengJiaMZZS()
        {
            this.MENZHENCS = this.MENZHENCS + 1;
            return this;
        }
        /// <summary>
        /// 减少门诊次数
        /// </summary>
        /// <returns></returns>
        public GY_BINGRENXX JianShaoMZZS()
        {
            if (this.MENZHENCS >= 1)
                this.MENZHENCS = this.MENZHENCS - 1;
            return this;
        }
    }
}
