using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_DANJUDXXX
	{

        /// <summary>
        /// 修改单据对象信息
        /// </summary>
        public GY_DANJUDXXX Update(E_GY_DANJUDXXX e_GY_DANJUDXXX)
        {          
            this.MargeDTO<GY_DANJUDXXX, E_GY_DANJUDXXX>(e_GY_DANJUDXXX);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.RegisterUpdate<GY_DANJUDXXX>(this);
            return this;
        }

        /// <summary>
        /// 删除单据对象信息
        /// </summary>
        public void Delete()
        {
            this.RegisterDelete<GY_DANJUDXXX>(this);
        }
    }
}
