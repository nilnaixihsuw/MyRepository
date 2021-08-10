using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;

namespace Mediinfo.Domain.JCJG.GY
{
    public partial class GY_YAOPINZZZS
	{
        /// <summary>
        ///删除 公用_药品资质证书
        /// </summary>
        public void Delete()
        {
            IRepositoyBase.RegisterDelete<GY_YAOPINZZZS>(this);
        }
        /// <summary>
        /// 更新 公用_药品资质证书
        /// </summary>
        /// <param name="eYaoPinZZZS"></param>
        /// <returns></returns>
        public GY_YAOPINZZZS Update(E_GY_YAOPINZZZS eYaoPinZZZS)
        {
            this.MargeDTO<GY_YAOPINZZZS, E_GY_YAOPINZZZS>(eYaoPinZZZS);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            return this;
        }
        //public GY_YAOPINZZZS UpdateYaoPinZZZS()
        //{
        //    this.XIUGAISJ = GetSYSTime();
        //    this.XIUGAIREN = ServiceContext.USERID;
        //    return this;
        //}
        public GY_YAOPINZZZS ZuoFei()
        {
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();
            this.ZUOFEIBZ = 1;
            return this;
        }
    }
}
