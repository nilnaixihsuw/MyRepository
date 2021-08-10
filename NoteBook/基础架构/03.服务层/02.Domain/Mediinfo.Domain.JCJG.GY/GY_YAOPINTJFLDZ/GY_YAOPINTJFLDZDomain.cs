using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public partial class GY_YAOPINTJFLDZ
	{

        /// <summary>
        /// 更新 公用_药品统计分类对照
        /// </summary>
        /// <param name="eYaoPinTJFLDZ"></param>
        /// <returns></returns>
        //public GY_YAOPINTJFLDZ Update(E_GY_YAOPINTJFLDZ eYaoPinTJFLDZ)
        //{
        //    this.MargeDTO<GY_YAOPINTJFLDZ, E_GY_YAOPINTJFLDZ>(eYaoPinTJFLDZ);
        //    return this;
        //}
        /// <summary>
        /// 删除 公用_药品统计分类对照
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_YAOPINTJFLDZ>(this);
        }
    }
}
