namespace Mediinfo.Domain.JCJG.GY
{
    public partial class GY_QINGLINGDAN2
	{
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="qld"></param>
        /// <returns></returns>
        public GY_QINGLINGDAN2 ShouLiQL()
        {
            this.QINGLINGZT = "3";
            this.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="qld"></param>
        /// <returns></returns>
        //public GY_QINGLINGDAN2 ShouLiQL(E_YK_CHUKUDAN2 chukudan2)
        //{
        //    this.QINGLINGZT = "3";
        //    this.SHIFASL = chukudan2.CHUKUSL;
        //    this.SHOULIRQ = this.GetSYSTime();
        //    this.RegisterUpdate(this);
        //    return this;
        //}
    }
}
