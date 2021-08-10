using System;
using Mediinfo.DTO.HIS.GY;
using System.Collections.Generic;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_YAOPINLD
	{
        public GY_YAOPINLD Update(E_GY_YAOPINLD ypld)
        {
            this.MargeDTO<GY_YAOPINLD, E_GY_YAOPINLD>(ypld);
            return this;
        }

        public void Delete()
        {
            this.RegisterDelete<GY_YAOPINLD>(this);
        }

        public GY_YAOPINLD Add()
        {
            this.RegisterAdd<GY_YAOPINLD>(this);
            return this;
        }
	}
}
