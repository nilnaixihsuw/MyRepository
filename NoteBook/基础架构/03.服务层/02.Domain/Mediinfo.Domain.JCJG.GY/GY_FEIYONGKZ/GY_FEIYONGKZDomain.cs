using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_FEIYONGKZ
	{
        /// <summary>
        /// ÊÇ·ñÒÑ×÷·Ï
        /// </summary>
        /// <returns></returns>
        public bool IsZuoFei()
        {
            return (this.ZUOFEIBZ.HasValue ? (this.ZUOFEIBZ != 0) : false);
        }

	    public GY_FEIYONGKZ Update(E_GY_FEIYONGKZ entity)
	    {
	        this.MargeDTO(entity);
	        this.XIUGAISJ = GetSYSTime();
	        this.XIUGAIREN = ServiceContext.USERID;
            IRepositoyBase.RegisterUpdate(this);
	        return this;
	    }

	    public GY_FEIYONGKZ ZuoFei()
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
