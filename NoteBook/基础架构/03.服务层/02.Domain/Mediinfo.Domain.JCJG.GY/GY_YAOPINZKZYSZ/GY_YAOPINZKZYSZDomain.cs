namespace Mediinfo.Domain.JCJG.GY
{
    public partial class GY_YAOPINZKZYSZ
	{
        /// <summary>
        /// ���� ����_ҩƷר��ר������
        /// </summary>
        /// <param name="eYaoPinZKZYSZ"></param>
        /// <returns></returns>
        //public GY_YAOPINZKZYSZ Update(E_GY_YAOPINZKZYSZ eYaoPinZKZYSZ)
        //{
        //    this.MargeDTO<GY_YAOPINZKZYSZ, E_GY_YAOPINZKZYSZ>(eYaoPinZKZYSZ);
        //    this.XIUGAISJ = GetSYSTime();
        //    this.XIUGAIREN = ServiceContext.USERID;
        //    return this;
        //}
        /// <summary>
        /// ɾ�� ����_ҩƷר��ר������
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_YAOPINZKZYSZ>(this);
        }
    }
}
