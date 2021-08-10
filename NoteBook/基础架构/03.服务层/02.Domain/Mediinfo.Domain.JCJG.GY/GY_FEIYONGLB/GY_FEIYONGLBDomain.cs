using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_FEIYONGLB
	{
	    public GY_FEIYONGLB Update(E_GY_FEIYONGLB entity)
	    {
	        this.MargeDTO(entity);
	        this.XIUGAISJ = GetSYSTime();
	        this.XIUGAIREN = ServiceContext.USERID;
	        IRepositoyBase.RegisterUpdate(this);
            return this;
        }

	    public GY_FEIYONGLB ZuoFei()
	    {
	        this.ZUOFEIBZ = 1;
	        IRepositoyBase.RegisterUpdate(this);
	        return this;
	    }

	    public void Delete()
	    {
	        IRepositoyBase.RegisterDelete(this);
        }


    }
}
