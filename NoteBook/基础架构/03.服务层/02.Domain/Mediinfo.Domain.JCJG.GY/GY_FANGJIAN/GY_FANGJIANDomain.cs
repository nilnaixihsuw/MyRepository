using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
    public partial class GY_FANGJIAN
    {

        //更新房间的性别属性
        public GY_FANGJIAN Update(string xingbiesx)
        {

            this.DANGQIANXBSX = xingbiesx;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        //更新房间信息
        public GY_FANGJIAN Update(E_GY_FANGJIAN eFangJian)
        {
            this.MargeDTO<GY_FANGJIAN, E_GY_FANGJIAN>(eFangJian);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            return this;
        }

        /// <summary>
        /// 作废房间
        /// </summary>
        /// <returns></returns>
        public GY_FANGJIAN ZuoFei()
        {
            this.ZUOFEIBZ = 1;
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = DateTime.Now;
            this.RegisterUpdate<GY_FANGJIAN>(this);
            return this;
        }

        /// <summary>
        /// 恢复房间
        /// </summary>
        /// <returns></returns>
        public GY_FANGJIAN HuiFu()
        {
            this.ZUOFEIBZ = 0;
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = DateTime.Now;
            this.RegisterUpdate<GY_FANGJIAN>(this);
            return this;
        }
    }
}
