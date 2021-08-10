using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_DANJUXX
	{

        /// <summary>
        /// �޸ĵ�����Ϣ
        /// </summary>
        public GY_DANJUXX Update(E_GY_DANJUXX e_GY_DANJUXX)
        {           
            this.MargeDTO<GY_DANJUXX, E_GY_DANJUXX>(e_GY_DANJUXX);
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();            
            return this.RegisterUpdate<GY_DANJUXX>(this);
        }

        /// <summary>
        /// ɾ��������Ϣ
        /// </summary>
        public void Delete()
        {
           this.RegisterDelete<GY_DANJUXX>(this);
        }
    }
}
