using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_ZHONGYAOPFKLZD
    {
        /// <summary>
        /// ���� ����_��ҩ�䷽�����ֵ�
        /// </summary>
        /// <param name="eYaoPinZZZS"></param>
        /// <returns></returns>
        public GY_ZHONGYAOPFKLZD Update(E_GY_ZHONGYAOPFKLZD eYaoPinZZZS)
        {
            this.MargeDTO<GY_ZHONGYAOPFKLZD, E_GY_ZHONGYAOPFKLZD>(eYaoPinZZZS);
            return this;
        }

        /// <summary>
        ///ɾ�� ����_��ҩ�䷽�����ֵ�
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_ZHONGYAOPFKLZD>(this);
        }
    }
}
