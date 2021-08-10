using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_DANJUDXXX
	{

        /// <summary>
        /// �޸ĵ��ݶ�����Ϣ
        /// </summary>
        public GY_DANJUDXXX Update(E_GY_DANJUDXXX e_GY_DANJUDXXX)
        {          
            this.MargeDTO<GY_DANJUDXXX, E_GY_DANJUDXXX>(e_GY_DANJUDXXX);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.RegisterUpdate<GY_DANJUDXXX>(this);
            return this;
        }

        /// <summary>
        /// ɾ�����ݶ�����Ϣ
        /// </summary>
        public void Delete()
        {
            this.RegisterDelete<GY_DANJUDXXX>(this);
        }
    }
}
