using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_JIANMIANMX
	{
        /// <summary>
        /// É¾³ý¼õÃâ¼ÇÂ¼
        /// </summary>
        /// <returns></returns>
        public GY_JIANMIANMX Delete()
        {
            this.RegisterDelete<GY_JIANMIANMX>(this);
            return this;
        }
    }
}
