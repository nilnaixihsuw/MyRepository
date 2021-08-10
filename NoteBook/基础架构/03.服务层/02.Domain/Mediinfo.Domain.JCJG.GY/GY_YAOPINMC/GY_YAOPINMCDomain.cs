using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise.Exceptions;
using Mediinfo.Infrastructure.Core.Domain;
using System;
using System.Collections.Generic;

namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_YAOPINMC
	{

        #region ҩƷ����


        /// <summary>
        /// ����ҩƷ����
        /// </summary>
        /// <param name="yaoPinMCDTO"></param>
        public void Update(E_GY_YAOPINMC_EX yaoPinMCDTO)
        {
            this.MargeDTO<GY_YAOPINMC, E_GY_YAOPINMC_EX>(yaoPinMCDTO);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
        }

        //����ҩƷ����

        public void ZuoFeiYaoPinMC(List<GY_YAOPINMCGG2> yaoPinMCGGentityList )
        {
            if (this.ZUOFEIBZ == 1)
            {
                throw new DomainException("[" + YAOPINMC + "]��ҩƷ�����ϲ����ٴ����ϣ�");
            }
            string yaoPinID = this.YAOPINID;

          //  var yaoPinMCGGentityList = DBContext.Set<GY_YAOPINMC>().Where(o => o.YAOPINID == yaoPinID && o.ZUOFEIBZ == 0).AsNoTracking().ToList();
            if (yaoPinMCGGentityList.Count > 0)
            {
                throw new DomainException("[" + this.YAOPINMC + "]��ҩƷ���й�񣬲���ɾ����");
            }
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.ZUOFEIBZ = 1;
        }

        /// <summary>
        /// ����ҩƷ��������
        /// </summary>
        /// <param name="yaoPinID"></param>
        /// <param name="yaoPinFl"></param>
        /// <param name="yaoPinGL"></param>
        /// <param name="biaoZhunBM"></param>
        public void UpdateYaoPinMCFL(string yaoPinID, string yaoPinFl, string yaoPinGL, string biaoZhunBM)
        {
            this.YAOPINFL = yaoPinFl;
            this.YAOPINGL = yaoPinGL;
            this.BIAOZHUNBM = biaoZhunBM;
        }

        public void Delete()
        {
            IRepositoyBase.RegisterDelete(this);
        }
        #endregion
    }
}
