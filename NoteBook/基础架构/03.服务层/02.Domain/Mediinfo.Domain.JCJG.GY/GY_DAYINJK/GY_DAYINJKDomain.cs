using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_DAYINJK
	{

        /// <summary>
        /// 更新打印接口
        /// </summary>
        /// <param name="dtoDaYinJK"></param>
        /// <returns></returns>
        public GY_DAYINJK Update(E_GY_DAYINJK dtoDaYinJK)
        {
            this.MargeDTO<GY_DAYINJK, E_GY_DAYINJK>(dtoDaYinJK);
            return this.RegisterUpdate(this);
        }

      
    }
}
