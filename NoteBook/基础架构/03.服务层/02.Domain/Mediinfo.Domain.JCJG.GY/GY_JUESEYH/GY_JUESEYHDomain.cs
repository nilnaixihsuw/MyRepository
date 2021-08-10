using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_JUESEYH
	{
        /// <summary>
        /// ɾ����ɫ�û�
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_JUESEYH>(this);
        }
        /// <summary>
        /// ���½�ɫ�û�
        /// </summary>
        /// <param name="eJueSeYH"></param>
        /// <returns></returns>
        public GY_JUESEYH Update(E_GY_JUESEYH eJueSeYH)
        {
            this.MargeDTO<GY_JUESEYH, E_GY_JUESEYH>(eJueSeYH);
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = this.GetSYSTime();
            return this;
        }
    }
}
