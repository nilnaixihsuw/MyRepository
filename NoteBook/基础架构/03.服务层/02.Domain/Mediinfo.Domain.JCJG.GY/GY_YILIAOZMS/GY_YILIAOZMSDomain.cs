using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_YILIAOZMS
	{
        /// <summary>
        /// 更新医疗证明信息
        /// </summary>
        /// <param name="eYiLiaoZMDTO"></param>
        /// <returns></returns>
        public GY_YILIAOZMS Update(E_GY_YILIAOZMS eYiLiaoZMSDTO)
        {
            this.MargeDTO<GY_YILIAOZMS, E_GY_YILIAOZMS>(eYiLiaoZMSDTO);
            return this.RegisterUpdate(this);
        }

        /// <summary>
        /// 删除医疗证明信息
        /// </summary>
        /// <returns></returns>
        public GY_YILIAOZMS Delete()
        {
            this.RegisterDelete<GY_YILIAOZMS>(this);
            return this;
        }
    }
}
