using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_HUIZHENDAN
	{
        public GY_HUIZHENDAN Update(E_GY_HUIZHENDAN_EX entity)
        {
            this.MargeDTO(entity);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// ���ͨ��
        /// </summary>
        /// <returns></returns>
        public GY_HUIZHENDAN ShenHeTG()
        {
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.SHENHEREN = ServiceContext.USERID;
            this.SHENHESJ = GetSYSTime();
            this.HUIZHENZT = 1;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// ���δͨ��
        /// </summary>
        /// <returns></returns>
        public GY_HUIZHENDAN ShenHeWTG(string shenHeBTGYY)
        {
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.SHENHEREN = ServiceContext.USERID;
            this.SHENHESJ = GetSYSTime();
            this.HUIZHENZT = 2;
            this.SHENHEBTGYY = shenHeBTGYY;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }


        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public GY_HUIZHENDAN JieShou()
        {
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.JIESHOUREN = ServiceContext.USERID;
            this.JIESHOUSJ = GetSYSTime();
            this.HUIZHENZT = 3;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public GY_HUIZHENDAN WanCheng(string huiZhenYJ)
        {
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.HUIZHENRQ = GetSYSTime();
            this.HUIZHENZT = 4;
            this.HUIZHENKS = ServiceContext.KESHIID;
            this.HUIZHENKSMC = ServiceContext.KESHIMC;
            this.HUIZHENYS = ServiceContext.USERID;
            this.HUIZHENYSXM = ServiceContext.USERNAME;
            this.HUIZHENYJ = huiZhenYJ;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public GY_HUIZHENDAN WanCheng(string huiZhenYJ, string huiZhenKS, string huiZhenKSMC,string huiZhenYS,string huiZhenYSXM)
        {
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.HUIZHENRQ = GetSYSTime();
            this.HUIZHENZT = 4;
            this.HUIZHENKS = huiZhenKS;
            this.HUIZHENKSMC = huiZhenKSMC;
            this.HUIZHENYS = huiZhenYS;
            this.HUIZHENYSXM = huiZhenYSXM;
            this.HUIZHENYJ = huiZhenYJ;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }
        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <returns></returns>
        public GY_HUIZHENDAN DaYin()
        {
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.DAYINREN = ServiceContext.USERID;
            this.DAYINSJ = GetSYSTime();
            this.HUIZHENZT = 5;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }
        /// <summary>
        /// ���ϻ��ﵥ������״̬Ϊ9��
        /// </summary>
        /// <returns></returns>
        public GY_HUIZHENDAN ZuoFei()
        {
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.HUIZHENZT = 9;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// ���أ�����״̬Ϊ0��
        /// </summary>
        /// <returns></returns>
        public GY_HUIZHENDAN CheHui()
        {
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.HUIZHENZT = 0;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// �ܾ� 
        /// </summary>
        /// <param name="juJueYY">�ܾ�ԭ��</param>
        /// <returns></returns>
        public GY_HUIZHENDAN JuJue(string juJueYY)
        {
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.JUJUEYY = juJueYY;
            this.HUIZHENZT =10;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }
      
    }
}
