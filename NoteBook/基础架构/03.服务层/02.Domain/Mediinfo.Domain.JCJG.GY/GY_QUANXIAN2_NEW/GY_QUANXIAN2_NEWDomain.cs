using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_QUANXIAN2_NEW
	{
        public void Update(E_GY_ERJIYHQX eyonghuqx)
        {
            this.MargeDTO<GY_QUANXIAN2_NEW, E_GY_ERJIYHQX>(eyonghuqx);
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();
        }

        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_QUANXIAN2_NEW>(this);
        }

    }
}
