using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public partial class GY_YAOPINBFWZ
	{
        /// <summary>
        /// ���� ҩƷ�ڷ�λ��
        /// </summary>
        /// <param name="eYaoPinBFWZ"></param>
        /// <returns></returns>
        //public GY_YAOPINBFWZ Update(E_GY_YAOPINBFWZ eYaoPinBFWZ)
        //{
        //    this.MargeDTO<GY_YAOPINBFWZ, E_GY_YAOPINBFWZ>(eYaoPinBFWZ);
        //    this.XIUGAIREN = ServiceContext.USERID;
        //    this.XIUGAISJ = this.GetSYSTime();
        //    return this;
        //}
        /// <summary>
        /// ɾ�� ҩƷ�ڷ�λ��
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_YAOPINBFWZ>(this);
        }
    }
}
