using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_ZHONGYAOPFKLZD
    {
        /// <summary>
        /// 更新 公用_中药配方颗粒字典
        /// </summary>
        /// <param name="eYaoPinZZZS"></param>
        /// <returns></returns>
        public GY_ZHONGYAOPFKLZD Update(E_GY_ZHONGYAOPFKLZD eYaoPinZZZS)
        {
            this.MargeDTO<GY_ZHONGYAOPFKLZD, E_GY_ZHONGYAOPFKLZD>(eYaoPinZZZS);
            return this;
        }

        /// <summary>
        ///删除 公用_中药配方颗粒字典
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_ZHONGYAOPFKLZD>(this);
        }
    }
}
