using Mediinfo.DTO.HIS.XT;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.XT
{
	public partial class XT_GONGNENG_NEW
	{
        public void UpdateXiTongGN(E_XT_GONGNENG_NEW dto)
        {
            this.MargeDTO<XT_GONGNENG_NEW, E_XT_GONGNENG_NEW>(dto);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;

        }
    }
}
