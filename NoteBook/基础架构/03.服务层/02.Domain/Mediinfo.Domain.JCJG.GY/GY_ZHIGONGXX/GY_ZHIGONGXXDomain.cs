using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_ZHIGONGXX
	{
        /// <summary>
        /// 更新 职工信息
        /// </summary>
        /// <param name="eZhiGongXX"></param>
        /// <returns></returns>
        public GY_ZHIGONGXX Update(E_GY_ZHIGONGXX eZhiGongXX)
        {
            this.MargeDTO<GY_ZHIGONGXX, E_GY_ZHIGONGXX>(eZhiGongXX);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            return this;
        }
        /// <summary>
        /// 删除 职工信息
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_ZHIGONGXX>(this);
        }
    }
}
