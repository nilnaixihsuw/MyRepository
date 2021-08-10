using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public partial class GY_KUAIJIQJ
	{
        /// <summary>
        /// 删除会计期间
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_KUAIJIQJ>(this);
        }
        /// <summary>
        /// 更新会计期间
        /// </summary>
        /// <param name="eKuaiJiQJ"></param>
        //public GY_KUAIJIQJ Update(E_GY_KUAIJIQJ eKuaiJiQJ)
        //{
        //    this.MargeDTO<GY_KUAIJIQJ, E_GY_KUAIJIQJ>(eKuaiJiQJ);
        //    return this;
        //}
    }
}
