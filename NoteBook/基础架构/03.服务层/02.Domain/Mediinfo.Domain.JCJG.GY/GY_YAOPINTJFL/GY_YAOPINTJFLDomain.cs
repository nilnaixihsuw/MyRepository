using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public partial class GY_YAOPINTJFL
	{
        /// <summary>
        /// ���� ����_ҩƷͳ�Ʒ���
        /// </summary>
        /// <param name="eYaoPinTJFL"></param>
        /// <returns></returns>
        //public GY_YAOPINTJFL Update(E_GY_YAOPINTJFL eYaoPinTJFL)
        //{
        //    this.MargeDTO<GY_YAOPINTJFL, E_GY_YAOPINTJFL>(eYaoPinTJFL);
        //    this.XIUGAISJ = GetSYSTime();
        //    this.XIUGAIREN = ServiceContext.USERID;
        //    return this;
        //}
        /// <summary>
        /// ɾ�� ����_ҩƷͳ�Ʒ���
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_YAOPINTJFL>(this);
        }
    }
}
