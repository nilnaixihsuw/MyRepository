using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public partial class GY_KUAIJIQJ
	{
        /// <summary>
        /// ɾ������ڼ�
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_KUAIJIQJ>(this);
        }
        /// <summary>
        /// ���»���ڼ�
        /// </summary>
        /// <param name="eKuaiJiQJ"></param>
        //public GY_KUAIJIQJ Update(E_GY_KUAIJIQJ eKuaiJiQJ)
        //{
        //    this.MargeDTO<GY_KUAIJIQJ, E_GY_KUAIJIQJ>(eKuaiJiQJ);
        //    return this;
        //}
    }
}
