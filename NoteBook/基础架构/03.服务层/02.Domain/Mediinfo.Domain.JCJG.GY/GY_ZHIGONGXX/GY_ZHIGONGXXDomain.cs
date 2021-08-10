using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_ZHIGONGXX
	{
        /// <summary>
        /// ���� ְ����Ϣ
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
        /// ɾ�� ְ����Ϣ
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_ZHIGONGXX>(this);
        }
    }
}
