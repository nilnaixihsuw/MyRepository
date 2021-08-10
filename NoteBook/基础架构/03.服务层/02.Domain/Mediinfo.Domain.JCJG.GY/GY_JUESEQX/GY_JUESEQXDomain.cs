using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_JUESEQX
	{
        /// <summary>
        /// ɾ����ɫȨ����Ϣ
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_JUESEQX>(this);
        }
        /// <summary>
        /// ���½�ɫȨ����Ϣ
        /// </summary>
        /// <param name="eJueSeQX"></param>
        /// <returns></returns>
        public GY_JUESEQX Update(E_GY_JUESEQX eJueSeQX)
        {
            this.MargeDTO<GY_JUESEQX, E_GY_JUESEQX>(eJueSeQX);
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = this.GetSYSTime();
            return this;
        }
    }
}
