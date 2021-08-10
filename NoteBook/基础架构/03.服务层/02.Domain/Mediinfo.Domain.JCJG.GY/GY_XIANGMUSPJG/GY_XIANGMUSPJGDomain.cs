using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_XIANGMUSPJG
	{
	    public GY_XIANGMUSPJG Update(E_GY_XIANGMUSPJG entity)
	    {
	        this.MargeDTO(entity);
	        
            return this;
	    }
    }
}
