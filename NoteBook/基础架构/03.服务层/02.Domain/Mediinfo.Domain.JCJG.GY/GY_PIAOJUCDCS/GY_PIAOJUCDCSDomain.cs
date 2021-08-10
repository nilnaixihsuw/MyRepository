using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_PIAOJUCDCS
	{

        public void Update(E_GY_PIAOJUCDCS e_GY_PIAOJUCDCS)
        {
            this.MargeDTO<GY_PIAOJUCDCS, E_GY_PIAOJUCDCS>(e_GY_PIAOJUCDCS);
            this.DAYINRQ = GetSYSTime();
            this.DAYINREN = ServiceContext.USERID;
            this.PIAOJUYWXT = ServiceContext.YINGYONGID;
        }

        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_PIAOJUCDCS>(this);
        }
    }
}
