using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise.Exceptions;
using Mediinfo.Infrastructure.Core.Domain;
using Mediinfo.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mediinfo.Domain.JCJG.GY
{
	public partial class GY_YAOPINMCGG2
	{

        /// <summary>
        /// �޸�ҩƷ����ʱͬʱ�޸�ҩƷ����е���Ϣ
        /// </summary>
        /// <param name="yaoPinID"></param>
        /// <param name="yaoPinZDBMGZBM"></param>

        public void SetYaoPinGGodifyYPMC(string yaoPinID, int yaoPinZDBMGZBM, string piShiYPMCBZ, GY_YAOPINMC yaoPinMCDTO)
        {
            // var yaoPinMCDTO = DBContext.Set<GY_YAOPINMC>().Where(o => o.YAOPINID == yaoPinID).AsNoTracking().ToList().FirstOrDefault();
            if (yaoPinMCDTO != null)
            {
                if (yaoPinZDBMGZBM == 1)
                {
                    if (piShiYPMCBZ == "1")
                    {
                        //ҩƷ�ֵ�_Ƥ��ҩƷ����ǰ�Ƿ���Ǻ�  
                        if (this.PISHIBZ.IndexOf("1") >= 0 && this.PISHIBZ.IndexOf("1") < 2)
                        {
                            this.YAOPINMC = "��" + yaoPinMCDTO.YAOPINMC;
                        }
                        else
                        {
                            this.YAOPINMC = yaoPinMCDTO.YAOPINMC;
                        }
                    }
                    else
                    {

                        this.YAOPINMC = yaoPinMCDTO.YAOPINMC;
                    }
                    this.SHURUMA1 = yaoPinMCDTO.SHURUMA1;
                    this.SHURUMA2 = yaoPinMCDTO.SHURUMA2;
                    this.SHURUMA3 = yaoPinMCDTO.SHURUMA3;
                }


                this.SHOUFEIXM = yaoPinMCDTO.SHOUFEIXM;
                this.YAOPINLX = yaoPinMCDTO.YAOPINLX;
                this.JIZHENYYBZ = yaoPinMCDTO.JIZHENYYBZ;
                this.DULIFL = yaoPinMCDTO.DULIFL;
                this.JIAZHIFL = yaoPinMCDTO.JIAZHIFL;
            }
        }
        /// <summary>
        /// �޸�ҩƷ�����Ϣʱͬʱ�޸�ҩƷ��ͬ����Ķ��й������
        /// </summary>
        /// <param name="yaoPinMCGG"></param>
        public void SetYaoPinGGModifyYPCGG(E_GY_YAOPINMCGG2_EX yaoPinMCGG)
        {

            this.YAOPINID = yaoPinMCGG.YAOPINID;
            this.ZHANGBULB = yaoPinMCGG.ZHANGBULB;
            this.SHOUFEIXM = yaoPinMCGG.SHOUFEIXM;
            this.JIXING = yaoPinMCGG.JIXING;
            this.DULIFL = yaoPinMCGG.DULIFL;
            this.JIAZHIFL = yaoPinMCGG.JIAZHIFL;
            this.GEIYAOFS = yaoPinMCGG.GEIYAOFS;
            this.YONGYAOTC = yaoPinMCGG.YONGYAOTC;
            this.YAOPINMC = yaoPinMCGG.YAOPINMC;
            this.NONGDU = yaoPinMCGG.NONGDU;
            this.TIJI = yaoPinMCGG.TIJI;
            this.TIJIDW = yaoPinMCGG.TIJIDW;
            this.ZUIXIAODW = yaoPinMCGG.ZUIXIAODW;
            this.PISHIBZ = yaoPinMCGG.PISHIBZ;
            this.KANGJUNYXJ = yaoPinMCGG.KANGJUNYXJ;
            this.DASHUYBZ = yaoPinMCGG.DASHUYBZ;
            this.FUFANGBZ = yaoPinMCGG.FUFANGBZ;
            this.YIBAODJ = yaoPinMCGG.YIBAODJ;
            this.YAOPINLX = yaoPinMCGG.YAOPINLX;
            this.DUIYINGPSYP = yaoPinMCGG.DUIYINGPSYP;
            this.FUJIASFBZ = yaoPinMCGG.FUJIASFBZ;
            this.GUANLIMS = yaoPinMCGG.GUANLIMS;
            this.SHURUMA1 = yaoPinMCGG.SHURUMA1;
            this.SHURUMA2 = yaoPinMCGG.SHURUMA2;
            this.SHURUMA3 = yaoPinMCGG.SHURUMA3;
            this.XIANLIANG3 = yaoPinMCGG.XIANLIANG3;
            this.XIANLIANG7 = yaoPinMCGG.XIANLIANG7;
            this.XIANLIANG15 = yaoPinMCGG.XIANLIANG15;
            this.XIANLIANG30 = yaoPinMCGG.XIANLIANG30;
            this.JILIANG = yaoPinMCGG.JILIANG;
            this.JILIANGDW = yaoPinMCGG.JILIANGDW;
            this.YAOKUSY = yaoPinMCGG.YAOKUSY;
            this.KANGSHENGSXJ = yaoPinMCGG.KANGSHENGSXJ;
            this.JIZHENYYBZ = yaoPinMCGG.JIZHENYYBZ;   //Ҫͳһ����YAOPINID����cdjg�� todo.......           
            this.FUYAOSX = yaoPinMCGG.FUYAOSX;
            this.YONGYAOJYSM = yaoPinMCGG.YONGYAOJYSM;
            this.CUNCHUTJ = yaoPinMCGG.CUNCHUTJ;
            this.QITASX = yaoPinMCGG.QITASX;
            this.PISHIYXTS = yaoPinMCGG.PISHIYXTS;
            this.DUIYINGPSYPSL = yaoPinMCGG.DUIYINGPSYPSL;
            this.RICHANGYJL = yaoPinMCGG.RICHANGYJL;
            this.TESHUJL = yaoPinMCGG.TESHUJL;
            this.DANJILIANG = yaoPinMCGG.DANJILIANG;
            this.YICIJL = yaoPinMCGG.YICIJL;
            this.YICIJLDW = yaoPinMCGG.YICIJLDW;
            this.PINCI = yaoPinMCGG.PINCI;
            this.PISHISJ = yaoPinMCGG.PISHISJ;
            this.AHFS = yaoPinMCGG.AHFS;
            this.ATC7 = yaoPinMCGG.ATC7;
            this.PEIZHISM = yaoPinMCGG.PEIZHISM;
            this.HUAIYUNFJ = yaoPinMCGG.HUAIYUNFJ;
            this.TIDAIYP = yaoPinMCGG.TIDAIYP;
            this.YUSHERS = yaoPinMCGG.YUSHERS;
            this.YINIANJTZJL = yaoPinMCGG.YINIANJTZJL;
            this.YIQIGTZJL = yaoPinMCGG.YIQIGTZJL;
            this.KAIFENGHYXSS = yaoPinMCGG.KAIFENGHYXSS;
            this.SHUZHUSL = yaoPinMCGG.SHUZHUSL;
            this.JIXINGFL = yaoPinMCGG.JIXINGFL;
            this.CUNCHUWD = yaoPinMCGG.CUNCHUWD;
            this.DANWEIZLJL = yaoPinMCGG.DANWEIZLJL;
            this.YAOPINXZ = yaoPinMCGG.YAOPINXZ;
            this.YAOPINYS = yaoPinMCGG.YAOPINYS;
            this.YAOPINXSD = yaoPinMCGG.YAOPINXSD;
            this.ZITITX = yaoPinMCGG.ZITITX;
            this.ZITITXPZ = yaoPinMCGG.ZITITXPZ;
            this.XIANDINGGYFS = yaoPinMCGG.XIANDINGGYFS;
            this.DISU = yaoPinMCGG.DISU;
            this.NEIBUBM = yaoPinMCGG.NEIBUBM;
            this.MEIXIANGSL = yaoPinMCGG.MEIXIANGSL;
            this.YONGYAOSM = yaoPinMCGG.YONGYAOJYSM;
            this.GUIGESM = yaoPinMCGG.GUIGESM;
            this.ZHUSHESM = yaoPinMCGG.ZHUSHESM;
            this.XIUGAIREN = yaoPinMCGG.XIUGAIREN;
            this.XIUGAISJ = yaoPinMCGG.XIUGAISJ;
            this.TIAOXINGMA = yaoPinMCGG.TIAOXINGMA;
            this.JINGSHIXX = yaoPinMCGG.JINGSHIXX;
            this.JINGSHIYS = yaoPinMCGG.JINGSHIYS;
            if (this.GUIGEID == yaoPinMCGG.XIAOGUIGID)
            {
                this.BAOZHUANGDW = yaoPinMCGG.ZUIXIAODW;
            }
            this.YAOPINGG = GetYaoPinGG(this);
        }

        //public static GYYaoPinMCGGDomain GetYaoPinMCGG(DBContextBase DBContext, ServiceContext sContext,string guiGeID)
        //{
        //    var yaoPinMCGGDomain = new GYYaoPinMCGGDomain(DBContext, sContext);
        //    yaoPinMCGGDomain.DBYaoPinGG = DBContext.Set<GY_YAOPINMCGG2>().Where(c => c.GUIGEID == guiGeID).FirstOrDefault();
        //    return yaoPinMCGGDomain;
        //}
        #region ҩƷ��� ��ɾ��


        /// <summary>
        /// ����ʱ��ʹ��
        /// </summary>
        /// <returns></returns>
        public void InsertYaoPinGG_CGG()
        {
            IRepositoyBase.RegisterAdd(this,true);
        }

        /// <summary>
        /// �޸�ҩƷ���ƺ�ͬ��ҩƷ���
        /// </summary>
        public void UpdateYaoPinGGCaiFenSJ(string yaoPinID, int yaoPinZDBMGZBM, string piShiYPMCBZ, GY_YAOPINMC yaoPinMCEntity)
        {

            SetYaoPinGGodifyYPMC(yaoPinID, yaoPinZDBMGZBM, piShiYPMCBZ, yaoPinMCEntity);

            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
        }

        //�޸Ĵ����ͬ��ҩƷС����й��
        public void UpdateXiaoYaoPinGG(E_GY_YAOPINMCGG2_EX yaoPinMCGG2DTO)
        {
            SetYaoPinGGModifyYPCGG(yaoPinMCGG2DTO);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
        }
        /// <summary>
        /// ͨ�õĹ����·��� ��GetYaoPinMCGGByKEY ��������ʹ��
        /// </summary>
        /// <param name="yaoPinMCGG2DTO"></param>
        public void UpdateYaoPinGG(E_GY_YAOPINMCGG2_EX yaoPinMCGG2DTO)
        {

            this.MargeDTO<GY_YAOPINMCGG2, E_GY_YAOPINMCGG2_EX>(yaoPinMCGG2DTO);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
        }

        /// <summary>
        /// ����ҩƷС���
        /// </summary>
        /// <param name="xiaoGuiGID"></param>
        public void UpdateYaoPinXiaoGG(string xiaoGuiGID)
        {
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIAOGUIGID = xiaoGuiGID;
        }
        /// <summary>
        /// ɾ�����
        /// </summary>
        public void ZuoFeiYaoPinXiaoGG(List<GY_YAOPINCDJG2> yaoPinCDJG2List)
        {

            //var entity = DBContext.Set<GY_YAOPINMCGG2>().Where(o => o.GUIGEID == GUIGEID).FirstOrDefault();
            // if (entity == null)
            // {
            //     throw new DomainException("[" + YAOPINMC + "]��ҩƷ��������ȷ�ϣ�");
            // }
            //if (ZUOFEIBZ == 1)
            //{
            //    throw new DomainException("[" + YAOPINMC + "]��ҩƷ�����ϲ����ٴ����ϣ�");
            //}

            //var count = this.Set<GY_YAOPINCDJG2>().Where(o => o.ZUOFEIBZ == 0 && o.GUIGEID == this.GUIGEID).Count();
            if (yaoPinCDJG2List.Count > 0)
            {
                throw new DomainException("[" + this.YAOPINMC + "]�˸�ҩƷ����»��в��ؼ۸񣬲���ɾ����");
            }
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.ZUOFEIBZ = 1;
        }

        public void Delete()
        {
            IRepositoyBase.RegisterDelete(this);
        } 

        #endregion


        #region ���ҩƷ���
        private string GetYaoPinGG(GY_YAOPINMCGG2 yaoPingMCGG, decimal baoZhuangLiang = 0)
        {
            string yaoPinGG = "";
            string nongDu;
            string jiLiang;
            string jiLiangDW;
            string tiJi;
            string tiJiDW;
            string zuiXiaoDW;
            string teShuJL;

            int fuFangBZ = yaoPingMCGG.FUFANGBZ == null ? 0 : Convert.ToInt32(yaoPingMCGG.FUFANGBZ);

            nongDu = yaoPingMCGG.NONGDU.ToStringEx();
            nongDu = yaoPingMCGG.NONGDU == 0 ? "" : Convert.ToString(yaoPingMCGG.NONGDU);


            jiLiang = yaoPingMCGG.JILIANG.ToStringEx();
            jiLiangDW = yaoPingMCGG.JILIANGDW.ToStringEx();

            tiJi = yaoPingMCGG.TIJI.ToStringEx();
            tiJiDW = yaoPingMCGG.TIJIDW.ToStringEx();

            zuiXiaoDW = yaoPingMCGG.ZUIXIAODW.ToStringEx();

            if (baoZhuangLiang <= 0)
            {
                baoZhuangLiang = Convert.ToDecimal(yaoPingMCGG.BAOZHUANGLIANG);
            }

            teShuJL = yaoPingMCGG.TESHUJL.ToStringEx();



            if (!nongDu.IsNullOrEmpty())
            {
                yaoPinGG = nongDu + "%";
            }

            if (fuFangBZ == 1)
            {
                yaoPinGG = "CO " + yaoPinGG;
            }

            if (!teShuJL.IsNullOrEmpty())
            {
                yaoPinGG = yaoPinGG + teShuJL + " ";
            }

            if (!jiLiang.IsNullOrEmpty())
            {
                yaoPinGG = yaoPinGG + jiLiang + jiLiangDW + " ";
            }

            if (!tiJi.IsNullOrEmpty())
            {
                if (!jiLiang.IsNullOrEmpty())
                {
                    yaoPinGG = yaoPinGG + "/" + tiJi + tiJiDW + " ";
                }
                else
                {
                    yaoPinGG = yaoPinGG + tiJi + tiJiDW + " ";
                }
            }

            if (!yaoPinGG.IsNullOrEmpty())
            {
                yaoPinGG = yaoPinGG + "*" + baoZhuangLiang.ToString() + zuiXiaoDW;
            }
            else
            {
                yaoPinGG = baoZhuangLiang.ToString() + zuiXiaoDW;
            }
            return yaoPinGG;
        }



        #endregion


        #region ���ҩƷ���


        /// <summary>
        /// ���ǰУ�� 
        /// </summary>
        /// <param name="baoZhuangLiang"></param>
        /// <returns> 1 ������� 2 �Ȳ��С��� 0 �Ѳ�� </returns>
        public int ChaiFenQianCheck(decimal baoZhuangLiang, List<GY_YAOPINMCGG2> YaoPinSuoYouGGEntityList)
        {
            List<string> error = new List<string>();
            string daGuiGeID = this.DAGUIGID;
            if (this.ZUOFEIBZ == 1)
            {
                throw new DomainException("�ù���Ѿ����ϣ������ٲ�ֹ��");
            }

            var minYaoPinMCGGList = YaoPinSuoYouGGEntityList.Where(o => o.BAOZHUANGLIANG == 1 && o.ZUOFEIBZ == 0).ToList();
            if (minYaoPinMCGGList.Count > 0 && baoZhuangLiang == 1)
            {
                throw new DomainException("��С����Ѵ��ڣ������ظ����ɣ�");
                //error.Add("��С����Ѵ��ڣ������ظ����ɣ�");
                //this.SendMessage<OptionLogEventArgs>(new OptionLogEventArgs() { SQLLog = error });
                //return 0;
            }
            //��װ������1 ��ʾ�ŷ���С��� ����1ʱ���ʾ�м���
            if (baoZhuangLiang == 1)
            {
                return 1;
            }
            else
            {
                // var middleYaoPinMCGGList = DBContext.Set<GY_YAOPINMCGG2>().Where(o => o.BAOZHUANGLIANG == baoZhuangLiang && o.ZUOFEIBZ == 0 && o.DAGUIGID == daGuiGeID).AsNoTracking().ToList();
                var middleYaoPinMCGGList = YaoPinSuoYouGGEntityList.Where(o => o.BAOZHUANGLIANG == baoZhuangLiang && o.ZUOFEIBZ == 0).ToList();
                if (middleYaoPinMCGGList.Count > 0 && baoZhuangLiang > 1)
                {
                    throw new DomainException("�м����Ѵ��ڣ������ظ����ɣ�");
                    //error.Add("�м����Ѵ��ڣ������ظ����ɣ�");
                    //this.SendMessage<OptionLogEventArgs>(new OptionLogEventArgs() { SQLLog = error });
                    //return 0;
                }

                if (minYaoPinMCGGList.Count == 0)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }

            }

        }

        /// <summary>
        /// ���ǰУ�飨������2018-11-6��
        /// </summary>
        /// <param name="baoZhuangLiang"></param>
        /// <param name="YaoPinSuoYouGGEntityList"></param>
        /// <returns>0��ʾ���ɲ��,1��ʾ�ɼ������</returns>
        public int ChaiFenQianCheck1(decimal baoZhuangLiang, List<GY_YAOPINMCGG2> YaoPinSuoYouGGEntityList)
        {
            //��װ������1 ��ʾ�Ų��ɲ��
            if (baoZhuangLiang == 1 || this.ZUOFEIBZ == 1)
            {
                return 0;
            }
            var minYaoPinMCGGList = YaoPinSuoYouGGEntityList.Where(o => o.BAOZHUANGLIANG == 1 && o.ZUOFEIBZ == 0).ToList();
            if (minYaoPinMCGGList.Count > 0) return 0;
            return 1;
        }


        public void ChaiFenYaoPinGG(decimal baoZhuangLiang, string baoZhuangDW, Dictionary<string, string> canShuDictionary)
        {
            string daGuiGeID = this.DAGUIGID;
            string yaoKuSY = "".PadLeft(50, '0');
            string menYaoSY = "".PadLeft(50, '0');
            string bingYaoSY = "".PadLeft(50, '1');
            string jingMaiPSY = "".PadLeft(50, '1');
            string zhiJiSY = "".PadLeft(50, '0');
            string menYaoSY_ZY = "".PadLeft(50, '1');
            string bingYaoSY_MZ = "".PadLeft(50, '0');
            string yaoPinLX = "1";
            string shiYong = "";
            string guiGeID = "";


            string yaoPinGG = GetYaoPinGG(this, baoZhuangLiang);
            //���ID
            guiGeID = this.GetOrder("GY_YAOPINMCGG2", ServiceContext.YUANQUID, 1)[0];

            if (baoZhuangLiang == 1)
            {
                this.XIAOGUIGID = guiGeID; // �����ID��ֵ��DOMAIN�Ĺ��ID
            }

            yaoPinLX = this.YAOPINLX;
            if (yaoPinLX == "1")
            {
                shiYong = canShuDictionary["Ĭ��ʹ�ù������"];
            }
            else if (yaoPinLX == "2")
            {
                shiYong = canShuDictionary["Ĭ��ʹ�ù������2"];
            }
            else if (yaoPinLX == "3")
            {
                shiYong = canShuDictionary["Ĭ��ʹ�ù������3"];
            }
            //  menyaosy | 1,0 ^ menyaosy_zy | 0,1 ^ bingyaosy | 0,1 ^ bingyaosy_mz | 1,0

            List<string> shiYongList = shiYong.Split('^').ToList();

            if (shiYongList.Count >= 4)
            {
                var menYaoSYBZ = shiYongList[0].Substring(shiYongList[0].IndexOf('|') + 3, 1);
                var menYaoSY_ZYBZ = shiYongList[1].Substring(shiYongList[1].IndexOf('|') + 3, 1);
                var bingYaoSYBZ = shiYongList[2].Substring(shiYongList[2].IndexOf('|') + 3, 1);
                var bingYaoSY_MZBZ = shiYongList[3].Substring(shiYongList[3].IndexOf('|') + 3, 1);


                if (!menYaoSYBZ.IsNullOrWhiteSpace())
                {
                    menYaoSY = "".PadLeft(50, menYaoSYBZ.ToCharArray()[0]);
                }
                if (!menYaoSY_ZYBZ.IsNullOrWhiteSpace())
                {
                    menYaoSY_ZY = "".PadLeft(50, menYaoSY_ZYBZ.ToCharArray()[0]);
                }
                if (!bingYaoSYBZ.IsNullOrWhiteSpace())
                {
                    bingYaoSY = "".PadLeft(50, bingYaoSYBZ.ToCharArray()[0]);
                }
                if (!bingYaoSY_MZBZ.IsNullOrWhiteSpace())
                {
                    bingYaoSY_MZ = "".PadLeft(50, bingYaoSY_MZBZ.ToCharArray()[0]);
                }



            }


            this.MENYAOSY = menYaoSY;
            this.YAOKUSY = yaoKuSY;
            this.BINGYAOSY = bingYaoSY;
            this.JINGMAIPSY = jingMaiPSY;
            this.ZHIJISY = zhiJiSY;
            this.MENYAOSY_ZY = menYaoSY_ZY;
            this.BINGYAOSY_MZ = bingYaoSY_MZ;
            this.GUIGEID = guiGeID;

            this.YAOPINGG = yaoPinGG;
            this.BAOZHUANGDW = string.IsNullOrEmpty(baoZhuangDW) ? this.ZUIXIAODW : baoZhuangDW;
            this.BAOZHUANGLIANG = baoZhuangLiang;
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.LINCHUANGSYBZ = 1;
            this.TIAOXINGMA = null;

        }

        /// <summary>
        /// Ժ��ʹ�ñ�־����
        /// </summary>
        public GY_YAOPINMCGG2 UpdateYuanQuSY(string yuanQuSY)
        {

            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.YUANQUSY = yuanQuSY;
            return this;
        }
        #endregion


    }
}
