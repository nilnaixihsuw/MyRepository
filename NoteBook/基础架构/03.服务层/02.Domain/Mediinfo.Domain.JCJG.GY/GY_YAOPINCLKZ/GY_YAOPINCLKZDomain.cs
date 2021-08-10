using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public partial class GY_YAOPINCLKZ
	{
        /// <summary>
        /// 更新 公用_药品存量控制
        /// </summary>
        /// <param name="eYaoPinCLKZ"></param>
        /// <returns></returns>
        //public GY_YAOPINCLKZ Update(E_GY_YAOPINCLKZ eYaoPinCLKZ)
        //{
        //    this.MargeDTO<GY_YAOPINCLKZ, E_GY_YAOPINCLKZ>(eYaoPinCLKZ);
        //    this.XIUGAIREN = ServiceContext.USERID;
        //    this.XIUGAISJ = this.GetSYSTime();
        //    return this;
        //}
        /// <summary>
        /// 删除 公用_药品存量控制
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_YAOPINCLKZ>(this);
        }
    }
}
