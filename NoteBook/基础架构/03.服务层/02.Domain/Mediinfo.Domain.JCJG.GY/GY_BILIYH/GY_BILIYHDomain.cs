using System;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public partial class GY_BILIYH
    {
        public void Update(E_GY_BILIYH entity)
        {
            this.MargeDTO(entity);
            IRepositoyBase.RegisterUpdate(this);
        }

        public void Delete()
        {
            IRepositoyBase.RegisterDelete(this);
        }
    }
}
