using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public partial class GY_YAOKUFYWFGDY
	{
        /// <summary>
        /// ���� ����_ҩ�ⷿҵ��ֹ���Ӧ
        /// </summary>
        /// <param name="eYaoKuFYWFGDY"></param>
        //public GY_YAOKUFYWFGDY Update(E_GY_YAOKUFYWFGDY eYaoKuFYWFGDY)
        //{
        //    this.MargeDTO<GY_YAOKUFYWFGDY, E_GY_YAOKUFYWFGDY>(eYaoKuFYWFGDY);
        //    return this;

        //}
        /// <summary>
        /// ɾ�� ����_ҩ�ⷿҵ��ֹ���Ӧ
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_YAOKUFYWFGDY>(this);
        }
    }
}
