using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public partial class GY_YAOPINTJFLDZ
	{

        /// <summary>
        /// ���� ����_ҩƷͳ�Ʒ������
        /// </summary>
        /// <param name="eYaoPinTJFLDZ"></param>
        /// <returns></returns>
        //public GY_YAOPINTJFLDZ Update(E_GY_YAOPINTJFLDZ eYaoPinTJFLDZ)
        //{
        //    this.MargeDTO<GY_YAOPINTJFLDZ, E_GY_YAOPINTJFLDZ>(eYaoPinTJFLDZ);
        //    return this;
        //}
        /// <summary>
        /// ɾ�� ����_ҩƷͳ�Ʒ������
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_YAOPINTJFLDZ>(this);
        }
    }
}
