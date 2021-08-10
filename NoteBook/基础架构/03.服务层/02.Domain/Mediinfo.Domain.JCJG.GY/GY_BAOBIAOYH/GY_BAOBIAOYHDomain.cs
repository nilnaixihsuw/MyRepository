using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_BAOBIAOYH
	{

        public GY_BAOBIAOYH Update(E_GY_BAOBIAOYH quanXianDTO)
        {
            quanXianDTO.XIUGAIREN = ServiceContext.USERID;
            quanXianDTO.XIUGAISJ = GetSYSTime();
            this.MargeDTO<GY_BAOBIAOYH, E_GY_BAOBIAOYH>(quanXianDTO);
            return this;
        }
    }
}
