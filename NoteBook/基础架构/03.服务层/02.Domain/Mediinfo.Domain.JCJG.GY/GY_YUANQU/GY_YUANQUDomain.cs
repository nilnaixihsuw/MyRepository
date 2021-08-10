using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_YUANQU
	{
        public void UpdateYuanQu(E_GY_YUANQU EYuanQu)
        {
            this.MargeDTO<GY_YUANQU, E_GY_YUANQU>(EYuanQu);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
        }

	}
}
