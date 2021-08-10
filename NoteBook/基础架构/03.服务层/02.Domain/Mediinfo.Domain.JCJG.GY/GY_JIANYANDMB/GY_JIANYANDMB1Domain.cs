using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_JIANYANDMB1
	{
        public void Update(E_GY_JIANYANDMB1 jianYanDTO)
        {
            this.MargeDTO<GY_JIANYANDMB1, E_GY_JIANYANDMB1>(jianYanDTO);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
        }
    }
}
