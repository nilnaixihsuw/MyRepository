using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_ZIFUBL
	{
	    public GY_ZIFUBL Update(E_GY_ZIFUBL entity)
	    {
	        this.MargeDTO(entity);
	        this.XIUGAISJ = GetSYSTime();
	        this.XIUGAIREN = ServiceContext.USERID;
            IRepositoyBase.RegisterUpdate(this);
	        return this;
	    }

	    public void Delete()
	    {
	        IRepositoyBase.RegisterDelete(this);
	    }
    }
}
