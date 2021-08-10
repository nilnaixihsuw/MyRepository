using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_YILIAOZM
	{
        /// <summary>
        /// 更新医疗证明信息
        /// </summary>
        /// <param name="eYiLiaoZMDTO"></param>
        /// <returns></returns>
        public GY_YILIAOZM Update(E_GY_YILIAOZM eYiLiaoZMDTO)
        {
            this.MargeDTO<GY_YILIAOZM, E_GY_YILIAOZM>(eYiLiaoZMDTO);
            this.RIQI = GetSYSTime();
            return this.RegisterUpdate(this);
        }

        /// <summary>
        /// 删除医疗证明信息
        /// </summary>
        /// <returns></returns>
        public GY_YILIAOZM Delete()
        {
            this.RegisterDelete<GY_YILIAOZM>(this);

            return this;
        }

        public GY_YILIAOZM ShenHe(string shenHeZT,string ShengHeJGSM)
        {
            this.SHENHEZT = shenHeZT;
            this.SHENHEJGSM = ShengHeJGSM;
            this.SHENHERQ = GetSYSTime();
            this.SHENHEREN = ServiceContext.USERNAME;
            return this;
        }

        public GY_YILIAOZM Print()
        {
            this.DAYINBZ = "1"; 
            this.DAYINRQ = GetSYSTime();
            this.DAYINREN = ServiceContext.USERNAME;
            return this;
        }

    }
}
