using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_ZIJINZH
	{
        /// <summary>
        /// 更新资金账户信息，包括增加金额、减少金额和期末金额
        /// </summary>
        /// <param name="eGYZiJinZHDTO"></param>
        /// <returns></returns>
        public GY_ZIJINZH Update(E_GY_ZIJINZH eGYZiJinZHDTO)
        {
            this.MargeDTO<GY_ZIJINZH, E_GY_ZIJINZH>(eGYZiJinZHDTO);
            this.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 更新增加金额和期末金额
        /// </summary>
        /// <param name="zengJiaJE">增加金额</param>
        /// <param name="qiMoJE">期末金额</param>
        /// <returns></returns>
        public GY_ZIJINZH ZengJiaJE(decimal? zengJiaJE,decimal? qiMoJE)
        {
            this.ZENGJIAJE = zengJiaJE;
            this.QIMOJE = qiMoJE;
            this.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 更新减少金额和期末金额
        /// </summary>
        /// <param name="jianShaoJE">减少金额</param>
        /// <param name="qiMoJE">期末金额</param>
        /// <returns></returns>
        public GY_ZIJINZH JianShaoJE(decimal? jianShaoJE, decimal? qiMoJE)
        {
            this.JIANSHAOJE = jianShaoJE;
            this.QIMOJE = qiMoJE;
            this.RegisterUpdate(this);
            return this;
        }


        /// <summary>
        /// 更新账户名称、账户等级、账户类型
        /// </summary>
        /// <param name="zhangHuMC">账户名称</param>
        /// <param name="zhangHuDJ">账户等级</param>
        /// <param name="zhangHuLX">账户类型1：个人2：单位</param>
        /// <returns></returns>
        public GY_ZIJINZH UpdateZhuangHuMCDELX(string zhangHuMC,string zhangHuDJ,int? zhangHuLX)
        {
            this.ZHANGHUMC = zhangHuMC;
            this.ZHANGHUDJ = zhangHuDJ;
            this.GERENDWBZ = zhangHuLX;
            this.RegisterUpdate(this);
            return this;
        }


        /// <summary>
        /// 更新直接资金账户介质号
        /// </summary>
        /// <param name="jieZhiHao"></param>
        /// <returns></returns>
        public GY_ZIJINZH UpdateJieZhiHao(string jieZhiHao)
        {
            this.JIEZHIHAO = jieZhiHao; 
            return this.RegisterUpdate(this);
        }
    }
}
