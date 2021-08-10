using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_YONGHUQX2
	{


        public void Update(E_GY_ZHIGONGYHQX eyonghuqx)
        {
            this.MargeDTO<GY_YONGHUQX2, E_GY_ZHIGONGYHQX>(eyonghuqx);
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();
        }

        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_YONGHUQX2>(this);
        }
    }
}
