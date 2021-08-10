using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_XIANGMUSP
	{

	    public GY_XIANGMUSP Update(E_GY_XIANGMUSP entity)
	    {
	        this.MargeDTO(entity);
	        return this;
	    }

	    public GY_XIANGMUSP Delete()
	    {
	        IRepositoyBase.RegisterDelete(this);
	        return this;
	    }

	    public GY_XIANGMUSP ZuoFei()
	    {
	        this.ZUOFEIBZ = 1;
	        IRepositoyBase.RegisterUpdate(this);
            return this;
        }
	}
}
