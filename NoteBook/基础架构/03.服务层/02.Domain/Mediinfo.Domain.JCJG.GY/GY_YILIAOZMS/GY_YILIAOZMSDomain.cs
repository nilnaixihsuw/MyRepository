using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_YILIAOZMS
	{
        /// <summary>
        /// ����ҽ��֤����Ϣ
        /// </summary>
        /// <param name="eYiLiaoZMDTO"></param>
        /// <returns></returns>
        public GY_YILIAOZMS Update(E_GY_YILIAOZMS eYiLiaoZMSDTO)
        {
            this.MargeDTO<GY_YILIAOZMS, E_GY_YILIAOZMS>(eYiLiaoZMSDTO);
            return this.RegisterUpdate(this);
        }

        /// <summary>
        /// ɾ��ҽ��֤����Ϣ
        /// </summary>
        /// <returns></returns>
        public GY_YILIAOZMS Delete()
        {
            this.RegisterDelete<GY_YILIAOZMS>(this);
            return this;
        }
    }
}
