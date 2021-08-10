using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_TAOCANMX
	{
        /// <summary>
        /// ¸üÐÂÌ×²ÍÃ÷Ï¸
        /// </summary>
        /// <param name="eGongYongTCMX"></param>
        /// <returns></returns>
        public GY_TAOCANMX Update(E_GY_TAOCANMX eGongYongTCMX)
        {
            this.MargeDTO<GY_TAOCANMX, E_GY_TAOCANMX>(eGongYongTCMX);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            return this;
        }

        /// <summary>
        /// É¾³ýÌ×²ÍÃ÷Ï¸
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_TAOCANMX>(this);
        }
    }
}
