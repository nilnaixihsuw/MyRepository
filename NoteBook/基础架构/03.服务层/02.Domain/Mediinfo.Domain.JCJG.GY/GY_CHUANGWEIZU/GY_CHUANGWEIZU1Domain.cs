using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using Mediinfo.Utility;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_CHUANGWEIZU1
	{
        /// <summary>
        /// 作废床位组
        /// </summary>
        /// <returns></returns>
        public GY_CHUANGWEIZU1 ZuoFei()
        {
            this.ZUOFEIBZ = 1;
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = DateTime.Now;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 恢复床位组
        /// </summary>
        /// <returns></returns>
        public GY_CHUANGWEIZU1 HuiFu()
        {
            this.ZUOFEIBZ = 0;
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = DateTime.Now;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 更新床位组1表
        /// </summary>
        /// <param name="eChuangWeiZu1"></param>
        /// <returns></returns>
        public GY_CHUANGWEIZU1 Update(E_GY_CHUANGWEIZU1 eChuangWeiZu1)
        {
            this.MargeDTO<GY_CHUANGWEIZU1, E_GY_CHUANGWEIZU1>(eChuangWeiZu1);
            string shuRuMa1 = string.Empty;
            string shuRuMa2 = string.Empty;
            string shuRuMa3 = string.Empty;
            ShuRuMaHelper.GetShuRuMa(this.CHUANGWEIZUMC, out shuRuMa1, out shuRuMa2, out shuRuMa3);
            this.SHURUMA1 = shuRuMa1;
            this.SHURUMA2 = shuRuMa2;
            this.SHURUMA3 = shuRuMa3;
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            return this;
        }
    }
}
