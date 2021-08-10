using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_ZHIGONGKS
	{
        /// <summary>
        /// 更新 公用_职工科室
        /// </summary>
        /// <param name="eZhiGongKS"></param>
        /// <returns></returns>
        public GY_ZHIGONGKS Update(E_GY_ZHIGONGKS eZhiGongKS)
        {
            this.MargeDTO<GY_ZHIGONGKS, E_GY_ZHIGONGKS>(eZhiGongKS);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            return this;
        }
        /// <summary>
        /// 删除 公用_职工科室
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_ZHIGONGKS>(this);
        }
    }
}
