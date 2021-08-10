using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
using System.Collections.Generic;

namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_CHUANGWEI
	{

        public GY_CHUANGWEI UpdateChuangWeiXX(E_GY_CHUANGWEI eGYChuangWei)
        {
            this.MargeDTO<GY_CHUANGWEI, E_GY_CHUANGWEI>(eGYChuangWei);
            return this;
        }

        /// <summary>
        /// 更新床位信息
        /// </summary>
        /// <param name="bingrenzyid"></param>
        /// <param name="chuangweizt"></param>
        /// <param name="ZhangChuangBZ"></param>
        /// <returns></returns>
        public GY_CHUANGWEI UpdateChuangWeiXX(string bingrenzyid,string chuangweizt,int? ZhangChuangBZ )
        {

            this.BINGRENZYID = bingrenzyid;
            this.CHUANGWEIZT = chuangweizt;
            this.ZHANCHUANGBZ = ZhangChuangBZ;
            this.XIUGAISJ = IRepositoyBase.GetSYSTime();
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 床位登记
        /// </summary>
        /// <param name="eChuangWei"></param>
        /// <returns></returns>
        public GY_CHUANGWEI DengJiCW(string chuangWeiZT,string bingRenZYID,string yuYueZT,string yuYueBRID)
        {
            this.CHUANGWEIZT = chuangWeiZT;
            this.BINGRENZYID = bingRenZYID;
            this.YUYUEZT = yuYueZT;
            this.YUYUEBRID = yuYueBRID;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 作废床位
        /// </summary>
        /// <returns></returns>
        public GY_CHUANGWEI ZuoFei()
        {
            this.ZUOFEIBZ = 1;
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = DateTime.Now;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 恢复床位
        /// </summary>
        /// <returns></returns>
        public GY_CHUANGWEI HuiFu()
        {
            this.ZUOFEIBZ = 0;
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = DateTime.Now;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 更新床位信息
        /// </summary>
        /// <param name="eChuangWei"></param>
        /// <returns></returns>
        public GY_CHUANGWEI UpdateChuangWei(E_GY_CHUANGWEI eChuangWei)
        {
            this.MargeDTO<GY_CHUANGWEI, E_GY_CHUANGWEI>(eChuangWei);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            return this;
        }

        /// <summary>
        /// 取消登记
        /// </summary>
        /// <param name="eChuangWei"></param>
        /// <returns></returns>
        public GY_CHUANGWEI QuXiaoDJ(GY_CHUANGWEI eChuangWei)
        {
            if(this.CHUANGWEIZT=="7")
            {
                this.CHUANGWEIZT = "0";
                this.BINGRENZYID = null;
                this.XIUGAIREN = ServiceContext.USERID;
                this.XIUGAISJ = DateTime.Now;
            }

            if(this.YUYUEZT=="1")
            {
                this.YUYUEZT = "0";
                this.YUYUEBRID = null;
                this.XIUGAIREN = ServiceContext.USERID;
                this.XIUGAISJ = DateTime.Now;
            }

            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 更新床位信息
        /// </summary>
        /// <param name="bingRenZYID"></param>
        /// <returns></returns>
	    public GY_CHUANGWEI GengXinChuangWeiXX(string bingRenZYID)
        {
            this.CHUANGWEIZT = "1";
            this.BINGRENZYID = bingRenZYID;
            this.ZHANCHUANGBZ = 1;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }
	}
}
