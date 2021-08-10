using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_YAOPINZBM
	{

        /// <summary>
        /// 同步主别名
        /// </summary>
        /// <param name="yaoPinMC"></param>
        /// <param name="shuRuMa1"></param>
        /// <param name="shuRuMa2"></param>
        /// <param name="shuRuMa3"></param>
        public void UpdateYaoPinZBMModifyYPMC(string yaoPinMC, string shuRuMa1, string shuRuMa2, string shuRuMa3)
        {
            this. YAOPINMC = yaoPinMC;
            this.SHURUMA1 = shuRuMa1;
            this.SHURUMA2 = shuRuMa2;
            this.SHURUMA3 = shuRuMa3;

            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
        }


        public void SetYaoPinZBM(string yaoPinID, string guiGeID, string jiaGeID)
        {
            this.YAOPINID = yaoPinID;
            this.GUIGEID = guiGeID;
            this.JIAGEID = jiaGeID;
        }
        #region 药品药品别名
        //public string InsertYaoPinBM()
        //{
        //    if (this.DBYaoPinzbm == null)
        //    {
        //        throw new DomainException("入参：药品别名不能为空！");
        //    }
        //    this.XIUGAISJ = GetSYSTime();
        //    this.XIUGAIREN = ServiceContext.USERID; 
        //    this.ZUOFEIBZ = 0;
        //    this.BIEMINGID = this.GetOrder("GY_YAOPINCDJG2", ServiceContext.YUANQUID, 1)[0];
        //    this.ZHUMINGID = ZhuBieMing;
        //    base.Insert<GY_YAOPINZBM>(DBYaoPinzbm);
        //    return this.BIEMINGID;
        //}

        public void UpdateYaoPinBM(E_GY_YAOPINZBM yaoPinZBMDTO)
        {

            //this.MargeDTO<GY_YAOPINZBM, E_GY_YAOPINZBM>(yaoPinZBMDTO);
            this.YAOPINMC = yaoPinZBMDTO.YAOPINMC;
            this.SHURUMA1 = yaoPinZBMDTO.SHURUMA1;
            this.SHURUMA2 = yaoPinZBMDTO.SHURUMA2;
            this.SHURUMA3 = yaoPinZBMDTO.SHURUMA3;
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
        }

        public void DeleteYaoPinBM()
        {
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.ZUOFEIBZ = 1;
        }
        #endregion
    }
}
