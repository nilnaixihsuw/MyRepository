using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_BINGRENGMS
	{
        public GY_BINGRENGMS Delete()
        {
            IRepositoyBase.RegisterDelete<GY_BINGRENGMS>(this);
            return this;
        }
        /// <summary>
        /// ÐÞ¸ÄÕï¶Ï
        /// </summary>
        public GY_BINGRENGMS Update(E_GY_BINGRENGMS dto)
        {
            this.MargeDTO<GY_BINGRENGMS, E_GY_BINGRENGMS>(dto);
            IRepositoyBase.RegisterUpdate<GY_BINGRENGMS>(this);
            return this;
        }
    }
}
