using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_YOUHUILB
	{
        /// <summary>
        /// ÊÇ·ñ×÷·Ï
        /// </summary>
        /// <returns></returns>
        public bool IsZuoFei()
        {
            return ((this.ZUOFEIBZ ?? 0) != 0);
        }

	    public void Update(E_GY_YOUHUILB entity)
	    {
	        this.MargeDTO(entity);
	        this.XIUGAISJ = GetSYSTime();
	        this.XIUGAIREN = ServiceContext.USERID;
        }

	    public void ZuoFei()
	    {
	        this.ZUOFEIBZ = 1;
	        IRepositoyBase.RegisterUpdate(this);
        }

	    public void Delete()
	    {
	        IRepositoyBase.RegisterDelete(this);
	    }

    }
}
