using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public partial class GY_YAOKUFYWFGDY
	{
        /// <summary>
        /// 更新 公用_药库房业务分工对应
        /// </summary>
        /// <param name="eYaoKuFYWFGDY"></param>
        //public GY_YAOKUFYWFGDY Update(E_GY_YAOKUFYWFGDY eYaoKuFYWFGDY)
        //{
        //    this.MargeDTO<GY_YAOKUFYWFGDY, E_GY_YAOKUFYWFGDY>(eYaoKuFYWFGDY);
        //    return this;

        //}
        /// <summary>
        /// 删除 公用_药库房业务分工对应
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_YAOKUFYWFGDY>(this);
        }
    }
}
