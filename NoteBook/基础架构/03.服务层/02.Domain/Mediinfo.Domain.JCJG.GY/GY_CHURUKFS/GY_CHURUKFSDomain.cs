using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public partial class GY_CHURUKFS
	{
        /// <summary>
        /// ɾ������ⷽʽ
        /// </summary>
        /// <returns></returns>
        public GY_CHURUKFS Delete()
        {
            return this.RegisterDelete<GY_CHURUKFS>(this);
        }
        /// <summary>
        /// ���³���ⷽʽ
        /// </summary>
        /// <param name="eChuRuKFS"></param>
        /// <returns></returns>
        //public GY_CHURUKFS Update(E_GY_CHURUKFS eChuRuKFS)
        //{
        //    this.MargeDTO<GY_CHURUKFS, E_GY_CHURUKFS>(eChuRuKFS);
        //    this.XIUGAISJ = this.GetSYSTime();
        //    this.XIUGAIREN = ServiceContext.USERID;
        //    this.RegisterUpdate(this);
        //    return this;
        //}
    }
}
