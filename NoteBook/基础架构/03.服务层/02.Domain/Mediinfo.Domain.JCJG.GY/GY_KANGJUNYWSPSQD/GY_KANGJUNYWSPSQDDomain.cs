using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_KANGJUNYWSPSQD
	{
        /// <summary>
        /// ���¿���ҩ���������뵥��Ϣ
        /// </summary>
        /// <param name="eYuYueBR"></param>
        /// <returns></returns>
        public GY_KANGJUNYWSPSQD Update(E_GY_KANGJUNYWSPSQD ekangjun)
        {
            this.MargeDTO<GY_KANGJUNYWSPSQD, E_GY_KANGJUNYWSPSQD>(ekangjun);
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }
    }
}
