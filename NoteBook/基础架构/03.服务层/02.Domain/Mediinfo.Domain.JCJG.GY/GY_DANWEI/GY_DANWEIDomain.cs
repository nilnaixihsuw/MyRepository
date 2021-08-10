using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_DANWEI
	{
        /// <summary>
        /// 删除公用单位
        /// </summary>
        public GY_DANWEI Delete()
        {
            return IRepositoyBase.RegisterDelete<GY_DANWEI>(this);
        }
        /// <summary>
        /// 更新公用单位
        /// </summary>
        /// <param name="eDanWei"></param>
        public GY_DANWEI Update(E_GY_DANWEI eDanWei)
        {
            this.MargeDTO<GY_DANWEI, E_GY_DANWEI>(eDanWei);
            this.DANWEIMC = eDanWei.DANWEIMC;
            this.XIUGAISJ = this.GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }
        public GY_DANWEI Update(E_GY_GONGHUODW eDanWei)
        {
            this.MargeDTO<GY_DANWEI, E_GY_GONGHUODW>(eDanWei);
            //this.DANWEIMC = eDanWei.DANWEIMC;
            this.XIUGAISJ = this.GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }
    }
}
