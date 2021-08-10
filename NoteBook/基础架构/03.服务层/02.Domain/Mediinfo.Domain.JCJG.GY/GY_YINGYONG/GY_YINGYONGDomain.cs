using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_YINGYONG
	{

        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_YINGYONG>(this);
        }
        /// <summary>
        /// ¸üÐÂ
        /// </summary>
        /// <param name="eYingYong"></param>
        public void Update(E_GY_YINGYONG eYingYong)
        {
            this.MargeDTO<GY_YINGYONG, E_GY_YINGYONG>(eYingYong);

        }
    }
}
