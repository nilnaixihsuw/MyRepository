using Mediinfo.DTO.HIS.GY;
using Mediinfo.Enterprise.Exceptions;
using Mediinfo.HIS.Core;
using Mediinfo.Infrastructure.Core.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediinfo.Domain.JCJG.GY
{
    public partial class GY_YAOPINCDJG2
	{

        /// <summary>
        /// 获取药品产地价格2的JIAGEID
        /// </summary>
        /// <returns></returns>
        public GY_YAOPINCDJG2 GetOrderYaoPinCDJG2()
        {
            this.JIAGEID = this.GetOrder("GY_YAOPINCDJG2", ServiceContext.YUANQUID, 1)[0];
            return this;
        }
        /// <summary> 
        /// 删除 药品产地价格2 信息
        /// </summary>
        public void Delete()
        {
            this.IRepositoyBase.RegisterDelete(this);
        }

        
        /// <summary>
        /// 通用更新药品产地价格 结合GetYaoPinCDJGByKey一块使用
        /// </summary>
        public GY_YAOPINCDJG2 UpdateYaoPinCDJG(E_GY_YAOPINCDJG2_EX yaoPinCDJGDTO)
        {
            this.MargeDTO<GY_YAOPINCDJG2, E_GY_YAOPINCDJG2_EX>(yaoPinCDJGDTO);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            return this;
        }

        /// <summary>
        /// 通用更新药品产地价格 结合GetYaoPinCDJGByKey一块使用
        /// </summary>
        public GY_YAOPINCDJG2 UpdateYaoPinCDJG(E_GY_YAOPINCDJG2 yaoPinCDJGDTO)
        {
            this.MargeDTO<GY_YAOPINCDJG2, E_GY_YAOPINCDJG2>(yaoPinCDJGDTO);
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            return this;
        }

        /// <summary>
        /// 修改药品产地价格门诊住院使用
        /// </summary>
        public GY_YAOPINCDJG2 UpdateYaoPinCDJGMZSY(E_GY_YAOPINCDJG2 yaoPinCDJGDTO)
        {
            //this.MargeDTO<GY_YAOPINCDJG2, E_GY_YAOPINCDJG2>(yaoPinCDJGDTO);
            this.MENYAOSY = yaoPinCDJGDTO.MENYAOSY;
            this.MENYAOSY_ZY = yaoPinCDJGDTO.MENYAOSY_ZY;
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
           // this.RegisterUpdate(this);
            return this;
        }
        /// <summary>
        /// 通用更新药品产地价格 结合GetYaoPinCDJGByKey一块使用
        /// </summary>
        public GY_YAOPINCDJG2 UpdateZhiJIJX(string zhijijx)
        {
            this.ZHIJIJX = zhijijx;
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            return this;
        }

        /// <summary>
        /// 药品规格有变动时同步更新药品产地
        /// </summary>
        public GY_YAOPINCDJG2 UpdateYaoPinCDByYaoPinGG(GY_YAOPINMCGG2 yaoPinMCGGEntity, List<GY_DAIMA> daiMa_QiTaSXList, GY_YAOPINMCGG2 maxYaoPinGG, GY_YAOPINCDJG2 maxGGYaoPinCJJG)
        {
            SetYaoPinCDXXModifyYPGG(yaoPinMCGGEntity, daiMa_QiTaSXList, maxYaoPinGG, maxGGYaoPinCJJG);

            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            return this;
        }

        /// <summary>
        /// 更新产地小规格ID
        /// </summary>
        /// <param name="xiaoGuiGID"></param>
        public GY_YAOPINCDJG2 UpdateYaoPinCDXiaoGGID(string xiaoGuiGID)
        {

            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIAOGUIGID = xiaoGuiGID;
            this.RegisterUpdate(this);
            return this;
        }


        /// <summary>
        /// 更新中药配方颗粒ID
        /// </summary>
        /// <param name="xiaoGuiGID"></param>
        public GY_YAOPINCDJG2 UpdateYaoPinCDPFKLID(string zhongYaoPFKLID)
        {

            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.ZHONGYAOPFKLID = zhongYaoPFKLID;
            this.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 停用药品
        /// </summary>
        /// <param name="xiaoGuiGID"></param>
        public GY_YAOPINCDJG2 TingYongYP()
        {
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.TINGYONGBZ = 1;
            return this;
        }

        /// <summary>
        /// 恢复药品
        /// </summary>
        /// <param name="yiYuanSPQYBZ"></param>
        public GY_YAOPINCDJG2 HuiFuYaoPin(string yiYuanSPQYBZ)
        {

            if (this.CHAXUNBZ == 1)
            {
                throw new DomainException("请先恢复该药品的帐簿查询");
            }

            if (yiYuanSPQYBZ.Substring(3, 1) == "1")
            {
                //TODO   SELECT t.Zuofeibz
                // INTO Str_Shenhebz  --这里用zuofeibz来表示审核 1 表示审核  0  表示未审核
                // FROM Gy_Yiyuansp t
                //WHERE t.Daguigid = Prm_Jiageid;
            }
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.TINGYONGBZ = 0;
            return this;
        }

        /// <summary>
        /// 禁止账簿查询
        /// </summary>
        public GY_YAOPINCDJG2 JinZhiZBCX()
        {

            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.TINGYONGBZ = 1;
            this.CHAXUNBZ = 1;
            return this;
        }

        /// <summary>
        /// 恢复账簿查询
        /// </summary>
        public GY_YAOPINCDJG2 HuiFuZBCX()
        {

            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.CHAXUNBZ = 0;
            return this;
        }

        /// <summary>
        /// 门诊启用停用
        /// </summary>
        public GY_YAOPINCDJG2 MenZhenQYTY(int menZhenQYBZ)
        {

            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.MENZHENQYBZ = menZhenQYBZ;
            return this;
        }
        /// <summary>
        /// 住院启用停用
        /// </summary>
        public GY_YAOPINCDJG2 ZhuYuanQYTY(int zhuYuanQYBZ)
        {

            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.ZHUYUANQYBZ = zhuYuanQYBZ;
            return this;
        }
        /// <summary>
        /// 院区使用标志更新
        /// </summary>
        public GY_YAOPINCDJG2 UpdateYuanQuSY(string yuanQuSY)
        {

            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.YUANQUSY = yuanQuSY;
            return this;
        }

        /// <summary>
        /// 更新平均进价
        /// </summary>
        /// <param name="yuanQuSY"></param>
        public GY_YAOPINCDJG2 UpdatePingJuJJ(decimal PingJuJJ)
        {
            this.PINGJUNJJ = PingJuJJ;
            return this;
        }

        /// <summary>
        /// 更新批发价和零售价
        /// </summary>
        /// <param name="PingJuJJ"></param>
        public GY_YAOPINCDJG2 UpdatePingLingJia(decimal PiFaJia, decimal LingShouJia)
        {
            this.PIFAJIA = PiFaJia;
            this.DANJIA1 = LingShouJia;
            return this;
        }

        /// <summary>
        /// 更新批发价、零售价、最高零售价和批发价
        /// </summary>
        /// <param name="xiaoGuiGID"></param>
        public void UpdateTiaoJia(decimal? PiFaJia, decimal? LingShouJia,decimal? zuiGaoPFJ, decimal? zuiGaoLSJ)
        {
            this.PIFAJIA = PiFaJia;
            this.DANJIA1 = LingShouJia;
            this.ZUIGAOPFJ = zuiGaoPFJ;
            this.ZUIGAOLSJ = zuiGaoLSJ;
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();
        }

        public GY_YAOPINCDJG2 UpdateYaoPinTXM(string tiaoXingMa)
        {
            this.TIAOXINGMA = tiaoXingMa;
            return this;
        }

        public void DeleteYaoPinCDJG()
        { 
           // if (this.ZUOFEIBZ == 1) return;
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.ZUOFEIBZ = 1;
        }



        /// <summary>
        /// 根据药品规格信息同步药品产地
        /// </summary>
        /// <param name="yaoPinGGID"></param>
        public void SetYaoPinCDXXModifyYPGG(GY_YAOPINMCGG2 yaoPinMCGGEntity, List<GY_DAIMA> daiMa_QiTaSXList, GY_YAOPINMCGG2 maxYaoPinGG, GY_YAOPINCDJG2 maxGGYaoPinCJJG)
        {
            // var yaoPinMCGGEntity = DBContext.Set<GY_YAOPINMCGG2>().Where(o => o.GUIGEID == yaoPinGGID).AsNoTracking().ToList().FirstOrDefault();
            if (yaoPinMCGGEntity != null)
            {
                SetYaoPinCDXX(yaoPinMCGGEntity, daiMa_QiTaSXList);
                ComputeJiGe(  maxYaoPinGG, maxGGYaoPinCJJG);
            }
        }

        /// <summary>
        /// 设置药品信息
        /// </summary>
        /// <param name="yaoPinCDJG2"></param>
        public void SetYaoPinCDXX(GY_YAOPINMCGG2 yaoPinMCGG2,List<GY_DAIMA> daiMa_QiTaSXList)
        {
           // var daiMaList = DBContext.Set<GY_DAIMA>().Where(e => e.DAIMALB == HISGlobalHelper.DaiMaLB.QiTaSX).ToList();

            this.ZHANGBULB = yaoPinMCGG2.ZHANGBULB;
            this.SHOUFEIXM = yaoPinMCGG2.SHOUFEIXM;
            this.JIXING = yaoPinMCGG2.JIXING;
            this.DULIFL = yaoPinMCGG2.DULIFL;
            this.JIAZHIFL = yaoPinMCGG2.JIAZHIFL;
            this.GEIYAOFS = yaoPinMCGG2.GEIYAOFS;
            this.YONGYAOTC = yaoPinMCGG2.YONGYAOTC;
            this.YAOPINMC = yaoPinMCGG2.YAOPINMC;
            this.YAOPINGG = yaoPinMCGG2.YAOPINGG;
            this.ZUIXIAODW = yaoPinMCGG2.ZUIXIAODW;
            this.BAOZHUANGDW = yaoPinMCGG2.BAOZHUANGDW;
            this.PISHIBZ = yaoPinMCGG2.PISHIBZ;
            this.KANGJUNYXJ = yaoPinMCGG2.KANGJUNYXJ;
            this.DASHUYBZ = yaoPinMCGG2.DASHUYBZ;
            this.FUFANGBZ = yaoPinMCGG2.FUFANGBZ;
            this.YAOPINLX = yaoPinMCGG2.YAOPINLX;
            this.MENYAOSY = yaoPinMCGG2.MENYAOSY;
            this.BINGYAOSY = yaoPinMCGG2.BINGYAOSY;
            this.DUIYINGPSYP = yaoPinMCGG2.DUIYINGPSYP;
            this.FUJIASFBZ = yaoPinMCGG2.FUJIASFBZ;
            this.GUANLIMS = yaoPinMCGG2.GUANLIMS;
            this.LINCHUANGSYBZ = yaoPinMCGG2.LINCHUANGSYBZ;
            this.YAOKUSY = yaoPinMCGG2.YAOKUSY;
            this.BAOZHUANGLIANG = yaoPinMCGG2.BAOZHUANGLIANG;
            this.KANGSHENGSXJ = yaoPinMCGG2.KANGSHENGSXJ;
            this.JIZHENYYBZ = yaoPinMCGG2.JIZHENYYBZ;   //要统一根据YAOPINID更新cdjg表 todo.......
            this.XIANLIANG3 = yaoPinMCGG2.XIANLIANG3;
            this.XIANLIANG7 = yaoPinMCGG2.XIANLIANG7;
            this.XIANLIANG15 = yaoPinMCGG2.XIANLIANG15;
            this.XIANLIANG30 = yaoPinMCGG2.XIANLIANG30;
            this.JILIANG = yaoPinMCGG2.JILIANG;
            this.JILIANGDW = yaoPinMCGG2.JILIANGDW;
            this.NONGDU = yaoPinMCGG2.NONGDU;
            this.TIJI = yaoPinMCGG2.TIJI;
            this.TIJIDW = yaoPinMCGG2.TIJIDW;
            this.YIBAODJ = yaoPinMCGG2.YIBAODJ;
            this.YAOPINID = yaoPinMCGG2.YAOPINID;
            this.FUYAOSX = yaoPinMCGG2.FUYAOSX;
            this.YONGYAOJYSM = yaoPinMCGG2.YONGYAOJYSM;
            this.SHURUMA1 = yaoPinMCGG2.SHURUMA1;
            this.SHURUMA2 = yaoPinMCGG2.SHURUMA2;
            this.SHURUMA3 = yaoPinMCGG2.SHURUMA3;
            this.QITASX = GetQiTaSX(this.QITASX, yaoPinMCGG2.QITASX, daiMa_QiTaSXList);
            this.PISHIYXTS = yaoPinMCGG2.PISHIYXTS;
            this.DUIYINGPSYPSL = yaoPinMCGG2.DUIYINGPSYPSL;
            this.RICHANGYJL = yaoPinMCGG2.RICHANGYJL;
            this.TESHUJL = yaoPinMCGG2.TESHUJL;
            this.DANJILIANG = yaoPinMCGG2.DANJILIANG;
            this.YICIJL = yaoPinMCGG2.YICIJL;
            this.YICIJLDW = yaoPinMCGG2.YICIJLDW;
            this.PINCI = yaoPinMCGG2.PINCI;
            this.PISHISJ = yaoPinMCGG2.PISHISJ;
            this.MENYAOSY_ZY = yaoPinMCGG2.MENYAOSY_ZY;
            this.BINGYAOSY = yaoPinMCGG2.BINGYAOSY;
            this.AHFS = yaoPinMCGG2.AHFS;
            this.ATC7 = yaoPinMCGG2.ATC7;
            this.PEIZHISM = yaoPinMCGG2.PEIZHISM;
            this.HUAIYUNFJ = yaoPinMCGG2.HUAIYUNFJ;
            this.TIDAIYP = yaoPinMCGG2.TIDAIYP;
            this.YUSHERS = yaoPinMCGG2.YUSHERS;
            this.YINIANJTZJL = yaoPinMCGG2.YINIANJTZJL;
            this.YIQIGTZJL = yaoPinMCGG2.YIQIGTZJL;
            this.KAIFENGHYXSS = yaoPinMCGG2.KAIFENGHYXSS;
            this.SHUZHUSL = yaoPinMCGG2.SHUZHUSL;
            this.JIXINGFL = yaoPinMCGG2.JIXINGFL;
            this.CUNCHUWD = yaoPinMCGG2.CUNCHUWD;
            this.DANWEIZLJL = yaoPinMCGG2.DANWEIZLJL;
            this.YAOPINXZ = yaoPinMCGG2.YAOPINXZ;
            this.YAOPINYS = yaoPinMCGG2.YAOPINYS;
            this.YAOPINXSD = yaoPinMCGG2.YAOPINXSD;
            this.ZITITX = yaoPinMCGG2.ZITITX;
            this.ZITITXPZ = yaoPinMCGG2.ZITITXPZ;
            this.XIANDINGGYFS = yaoPinMCGG2.XIANDINGGYFS;
            this.DISU = yaoPinMCGG2.DISU;

        }


        /// <summary>
        /// 计算药品价格
        /// </summary>
        /// <param name="yaoPinMCGG2"></param>
        private void ComputeJiGe( GY_YAOPINMCGG2 maxYaoPinGG , GY_YAOPINCDJG2 maxGGYaoPinCJJG)
        {

            //修改价格
           // var maxGuiGeId = daGuiGID;

          // var maxYaoPinGG = DBContext.Set<GY_YAOPINMCGG2>().Where(o => o.GUIGEID == maxGuiGeId).AsNoTracking().FirstOrDefault();

            //药品产地标中规格相同，产地一样的数据只能有一条
           // var maxGGYaoPinCJJG = DBContext.Set<GY_YAOPINCDJG2>().Where(o => o.GUIGEID == maxGuiGeId && o.CHANDI == this.CHANDI).AsNoTracking().ToList().FirstOrDefault();
            if (this.BAOZHUANGLIANG != maxYaoPinGG.BAOZHUANGLIANG)
            {
                decimal zhaoBiaoJJ = Convert.ToDecimal(maxGGYaoPinCJJG.ZHAOBIAOJJ);
                decimal piFaJia = Convert.ToDecimal(maxGGYaoPinCJJG.PIFAJIA);
                decimal zuiGaoPFJ = Convert.ToDecimal(maxGGYaoPinCJJG.ZUIGAOPFJ);
                decimal zuiGaoLSJ = Convert.ToDecimal(maxGGYaoPinCJJG.ZUIGAOLSJ);
                decimal pingJunJJ = Convert.ToDecimal(maxGGYaoPinCJJG.PINGJUNJJ);
                decimal danJia1 = Convert.ToDecimal(maxGGYaoPinCJJG.DANJIA1);
                decimal danJia2 = Convert.ToDecimal(maxGGYaoPinCJJG.DANJIA2);
                decimal danJia3 = Convert.ToDecimal(maxGGYaoPinCJJG.DANJIA3);
                decimal danJia4 = Convert.ToDecimal(maxGGYaoPinCJJG.DANJIA4);
                decimal danJia5 = Convert.ToDecimal(maxGGYaoPinCJJG.DANJIA5);
                decimal danJia6 = Convert.ToDecimal(maxGGYaoPinCJJG.DANJIA6);
                decimal danJia7 = Convert.ToDecimal(maxGGYaoPinCJJG.DANJIA7);
                decimal danJia8 = Convert.ToDecimal(maxGGYaoPinCJJG.DANJIA8);
                decimal danJia9 = Convert.ToDecimal(maxGGYaoPinCJJG.DANJIA9);
                decimal danJia10 = Convert.ToDecimal(maxGGYaoPinCJJG.DANJIA10);
                decimal danJia11 = Convert.ToDecimal(maxGGYaoPinCJJG.DANJIA11);
                decimal danJia12 = Convert.ToDecimal(maxGGYaoPinCJJG.DANJIA12);
                decimal danJia13 = Convert.ToDecimal(maxGGYaoPinCJJG.DANJIA13);
                decimal danJia14 = Convert.ToDecimal(maxGGYaoPinCJJG.DANJIA14);
                decimal danJia15 = Convert.ToDecimal(maxGGYaoPinCJJG.DANJIA15);
                decimal danJia16 = Convert.ToDecimal(maxGGYaoPinCJJG.DANJIA16);
                decimal danJia17 = Convert.ToDecimal(maxGGYaoPinCJJG.DANJIA17);
                decimal danJia18 = Convert.ToDecimal(maxGGYaoPinCJJG.DANJIA18);
                decimal danJia19 = Convert.ToDecimal(maxGGYaoPinCJJG.DANJIA19);
                decimal danJia20 = Convert.ToDecimal(maxGGYaoPinCJJG.DANJIA20);

                decimal baoZhuangLiang = Convert.ToDecimal(maxYaoPinGG.BAOZHUANGLIANG);

                decimal baoZhuangLiang_DB = Convert.ToDecimal(this.BAOZHUANGLIANG);

                if (maxGGYaoPinCJJG.ZHAOBIAOJJ == null)
                {
                    this.ZHAOBIAOJJ = null;
                }
                else
                {
                    this.ZHAOBIAOJJ = Math.Round(zhaoBiaoJJ * baoZhuangLiang_DB / baoZhuangLiang, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);
                }

                if (maxGGYaoPinCJJG.PIFAJIA == null)
                {
                    this.PIFAJIA = null;
                }
                else
                {
                    this.PIFAJIA = Math.Round(piFaJia * baoZhuangLiang_DB / baoZhuangLiang, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);
                }
                if (maxGGYaoPinCJJG.ZUIGAOPFJ == null)
                {
                    this.ZUIGAOPFJ = null;
                }
                else
                {
                    this.ZUIGAOPFJ = Math.Round(zuiGaoPFJ * baoZhuangLiang_DB / baoZhuangLiang, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

                }
                if (maxGGYaoPinCJJG.ZUIGAOLSJ == null)
                {
                    this.ZUIGAOLSJ = null;
                }
                else
                {
                    this.ZUIGAOLSJ = Math.Round(zuiGaoLSJ * baoZhuangLiang_DB / baoZhuangLiang, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

                }

                if (maxGGYaoPinCJJG.PINGJUNJJ == null)
                {
                    this.PINGJUNJJ = null;
                }
                else
                {
                    this.PINGJUNJJ = Math.Round(pingJunJJ * baoZhuangLiang_DB / baoZhuangLiang, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);
                }
                if (maxGGYaoPinCJJG.DANJIA1 == null)
                {
                    this.DANJIA1 = null;
                }
                else
                {
                    this.DANJIA1 = Math.Round(danJia1 * baoZhuangLiang_DB / baoZhuangLiang, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

                }
                if (maxGGYaoPinCJJG.DANJIA2 == null)
                {
                    this.DANJIA2 = null;
                }
                else
                {
                    this.DANJIA2 = Math.Round(danJia2 * baoZhuangLiang_DB / baoZhuangLiang, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

                }
                if (maxGGYaoPinCJJG.DANJIA3 == null)
                {
                    this.DANJIA3 = null;
                }
                else
                {
                    this.DANJIA3 = Math.Round(danJia3 * baoZhuangLiang_DB / baoZhuangLiang, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

                }
                if (maxGGYaoPinCJJG.DANJIA4 == null)
                {
                    this.DANJIA4 = null;
                }
                else
                {
                    this.DANJIA4 = Math.Round(danJia4 * baoZhuangLiang_DB / baoZhuangLiang, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);
                }
                if (maxGGYaoPinCJJG.DANJIA5 == null)
                {
                    this.DANJIA5 = null;
                }
                else
                {
                    this.DANJIA5 = Math.Round(danJia5 * baoZhuangLiang_DB / baoZhuangLiang, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

                }
                if (maxGGYaoPinCJJG.DANJIA6 == null)
                {
                    this.DANJIA6 = null;
                }
                else
                {
                    this.DANJIA6 = Math.Round(danJia6 * baoZhuangLiang_DB / baoZhuangLiang, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

                }
                if (maxGGYaoPinCJJG.DANJIA7 == null)
                {
                    this.DANJIA7 = null;
                }
                else
                {
                    this.DANJIA7 = Math.Round(danJia7 * baoZhuangLiang_DB / baoZhuangLiang, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

                }
                if (maxGGYaoPinCJJG.DANJIA8 == null)
                {
                    this.DANJIA8 = null;
                }
                else
                {
                    this.DANJIA8 = Math.Round(danJia8 * baoZhuangLiang_DB / baoZhuangLiang, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

                }
                if (maxGGYaoPinCJJG.DANJIA9 == null)
                {
                    this.DANJIA9 = null;
                }
                else
                {
                    this.DANJIA9 = Math.Round(danJia9 * baoZhuangLiang_DB / baoZhuangLiang, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

                }
                if (maxGGYaoPinCJJG.DANJIA10 == null)
                {
                    this.DANJIA10 = null;
                }
                else
                {
                    this.DANJIA10 = Math.Round(danJia10 * baoZhuangLiang_DB / baoZhuangLiang, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

                }
                if (maxGGYaoPinCJJG.DANJIA11 == null)
                {
                    this.DANJIA11 = null;
                }
                else
                {
                    this.DANJIA11 = Math.Round(danJia11 * baoZhuangLiang_DB / baoZhuangLiang, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

                }
                if (maxGGYaoPinCJJG.DANJIA12 == null)
                {
                    this.DANJIA12 = null;
                }
                else
                {
                    this.DANJIA12 = Math.Round(danJia12 * baoZhuangLiang_DB / baoZhuangLiang, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

                }
                if (maxGGYaoPinCJJG.DANJIA13 == null)
                {
                    this.DANJIA13 = null;
                }
                else
                {
                    this.DANJIA13 = Math.Round(danJia13 * baoZhuangLiang_DB / baoZhuangLiang, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

                }
                if (maxGGYaoPinCJJG.DANJIA14 == null)
                {
                    this.DANJIA14 = null;
                }
                else
                {
                    this.DANJIA14 = Math.Round(danJia14 * baoZhuangLiang_DB / baoZhuangLiang, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

                }
                if (maxGGYaoPinCJJG.DANJIA15 == null)
                {
                    this.DANJIA15 = null;
                }
                else
                {
                    this.DANJIA15 = Math.Round(danJia15 * baoZhuangLiang_DB / baoZhuangLiang, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

                }
                if (maxGGYaoPinCJJG.DANJIA16 == null)
                {
                    this.DANJIA16 = null;
                }
                else
                {
                    this.DANJIA16 = Math.Round(danJia16 * baoZhuangLiang_DB / baoZhuangLiang, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

                }
                if (maxGGYaoPinCJJG.DANJIA17 == null)
                {
                    this.DANJIA17 = null;
                }
                else
                {
                    this.DANJIA17 = Math.Round(danJia17 * baoZhuangLiang_DB / baoZhuangLiang, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

                }
                if (maxGGYaoPinCJJG.DANJIA18 == null)
                {
                    this.DANJIA18 = null;
                }
                else
                {
                    this.DANJIA18 = Math.Round(danJia18 * baoZhuangLiang_DB / baoZhuangLiang, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

                }
                if (maxGGYaoPinCJJG.DANJIA19 == null)
                {
                    this.DANJIA19 = null;
                }
                else
                {
                    this.DANJIA19 = Math.Round(danJia19 * baoZhuangLiang_DB / baoZhuangLiang, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

                }
                if (maxGGYaoPinCJJG.DANJIA20 == null)
                {
                    this.DANJIA20 = null;
                }
                else
                {
                    this.DANJIA20 = Math.Round(danJia20 * baoZhuangLiang_DB / baoZhuangLiang, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);
                }
            }
        }


        /// <summary>
        /// 拼装其他属性
        /// </summary>
        /// <param name="qiTaSX"></param>
        /// <param name="qiTaSXNew"></param>
        /// <param name="daiMaList"></param>
        /// <returns></returns>
        private string GetQiTaSX(string qiTaSX, string qiTaSXNew, List<GY_DAIMA> daiMaList)
        {
            var chaoChuCDBF = "";
            var count = 0;
            qiTaSX = string.IsNullOrEmpty(qiTaSX) ? "" : qiTaSX;
            qiTaSXNew = string.IsNullOrEmpty(qiTaSXNew) ? "" : qiTaSXNew;
            StringBuilder qiTaSXBuilder = new StringBuilder(qiTaSX);
            StringBuilder qiTaSXNewBuilder = new StringBuilder(qiTaSXNew);

            if (qiTaSX.Length > qiTaSXNew.Length)
            {
                chaoChuCDBF = qiTaSX.Substring(qiTaSXNew.Length);
                count = qiTaSXNew.Length;
            }
            else
            {
                chaoChuCDBF = qiTaSXNew.Substring(qiTaSX.Length);
                count = qiTaSX.Length;
            }
            for (int i = 0; i < count; i++)
            {
                var daiMa = daiMaList.Where(o => o.SHUNXUHAO == (i + 1)).FirstOrDefault();
                if (daiMa != null && daiMa.ZIFU1 != "2")
                {
                    qiTaSXBuilder[i] = qiTaSXNewBuilder[i];
                }
            }
            qiTaSXBuilder.Append(chaoChuCDBF);

            return qiTaSXBuilder.ToString();
        }

        /// <summary>
        /// 新增时从药品规格同步药品信息
        /// </summary>
        /// <param name="yaoPinCDJG2"></param>
        public void SetYaoPinCDXXModifyCDJG(E_GY_YAOPINCDJG2_EX yaoPincdjg2DTO,GY_YAOPINMCGG2 maxYaoPinGG, GY_YAOPINCDJG2 maxGGYaoPinCJJG)
        {
            var yaoPinCDJG2Entity = yaoPincdjg2DTO;// yaoPincdjg2DTO.EToDB<E_GY_YAOPINCDJG2_EX,GY_YAOPINCDJG2>();
            this.SHOUFEIXM = yaoPinCDJG2Entity.SHOUFEIXM;
            this.YAOPINLX = yaoPinCDJG2Entity.YAOPINLX;
            //this.ZHAOBIAOJJ = yaoPinCDJG2Entity.ZHAOBIAOJJ;
            //this.PIFAJIA = yaoPinCDJG2Entity.PIFAJIA;
            //this.DANJIA1 = yaoPinCDJG2Entity.DANJIA1;
            //this.DANJIA2 = yaoPinCDJG2Entity.DANJIA2;
            //this.DANJIA3 = yaoPinCDJG2Entity.DANJIA3;
            //this.DANJIA4 = yaoPinCDJG2Entity.DANJIA4;
            //this.DANJIA5 = yaoPinCDJG2Entity.DANJIA5;
            //this.ZUIGAOPFJ = yaoPinCDJG2Entity.DANJIA6;
            //this.ZUIGAOLSJ = yaoPinCDJG2Entity.ZUIGAOLSJ; 
            //this.PINGJUNJJ = yaoPinCDJG2Entity.PINGJUNJJ;
            this.GOUHUOBZ = yaoPinCDJG2Entity.GOUHUOBZ;
            this.CHAXUNBZ = yaoPinCDJG2Entity.CHAXUNBZ;
            this.SHURUMA1 = yaoPinCDJG2Entity.SHURUMA1;
            this.SHURUMA2 = yaoPinCDJG2Entity.SHURUMA2;
            this.SHURUMA3 = yaoPinCDJG2Entity.SHURUMA3;
            this.ZHUCESB = yaoPinCDJG2Entity.ZHUCESB;
            this.PIZHUNWH = yaoPinCDJG2Entity.PIZHUNWH;
            this.JINKOUYPZH = yaoPinCDJG2Entity.JINKOUYPZH;
            this.ZHAOBIAOYPBZ = yaoPinCDJG2Entity.ZHAOBIAOYPBZ;
            this.GMPBZ = yaoPinCDJG2Entity.GMPBZ;
            this.XIUGAIREN = yaoPinCDJG2Entity.XIUGAIREN;
            this.XIUGAISJ = yaoPinCDJG2Entity.XIUGAISJ;
            this.DULIFL = yaoPinCDJG2Entity.DULIFL;
            this.JIAZHIFL = yaoPinCDJG2Entity.JIAZHIFL;
            this.GUANLIMS = yaoPinCDJG2Entity.GUANLIMS;
            this.GEIYAOFS = yaoPinCDJG2Entity.GEIYAOFS;
            this.YONGYAOTC = yaoPinCDJG2Entity.YONGYAOTC;
            this.ZUIXIAODW = yaoPinCDJG2Entity.ZUIXIAODW;
            this.PISHIBZ = yaoPinCDJG2Entity.PISHIBZ;
            this.KANGJUNYXJ = yaoPinCDJG2Entity.KANGJUNYXJ;
            this.DASHUYBZ = yaoPinCDJG2Entity.DASHUYBZ;
            this.FUFANGBZ = yaoPinCDJG2Entity.FUFANGBZ;
            this.DUIYINGPSYP = yaoPinCDJG2Entity.DUIYINGPSYP;
            this.FUJIASFBZ = yaoPinCDJG2Entity.FUJIASFBZ;
            this.ZHANGBULB = yaoPinCDJG2Entity.ZHANGBULB;
            this.YAOPINMC = yaoPinCDJG2Entity.YAOPINMC;
            this.KANGSHENGSXJ = yaoPinCDJG2Entity.KANGSHENGSXJ;
            this.JIZHENYYBZ = yaoPinCDJG2Entity.JIZHENYYBZ;
            this.JIXING = yaoPinCDJG2Entity.JIXING;
            this.XIANLIANG3 = yaoPinCDJG2Entity.XIANLIANG3;
            this.XIANLIANG7 = yaoPinCDJG2Entity.XIANLIANG7;
            this.XIANLIANG15 = yaoPinCDJG2Entity.XIANLIANG15;
            this.XIANLIANG30 = yaoPinCDJG2Entity.XIANLIANG30;
            this.JILIANG = yaoPinCDJG2Entity.JILIANG;
            this.JILIANGDW = yaoPinCDJG2Entity.JILIANGDW;
            this.NONGDU = yaoPinCDJG2Entity.NONGDU;
            this.TIJI = yaoPinCDJG2Entity.TIJI;
            this.TIJIDW = yaoPinCDJG2Entity.TIJIDW;
            this.YIBAODJ = yaoPinCDJG2Entity.YIBAODJ;
            this.YAOPINID = yaoPinCDJG2Entity.YAOPINID;
            this.FUYAOSX = yaoPinCDJG2Entity.FUYAOSX;
            this.YONGYAOJYSM = yaoPinCDJG2Entity.YONGYAOJYSM;
            this.QITASX = yaoPinCDJG2Entity.QITASX;
            this.ZHONGBIAOHAO = yaoPinCDJG2Entity.ZHONGBIAOHAO;
            this.JIAJIALV = yaoPinCDJG2Entity.JIAJIALV;
            this.OTCBZ = yaoPinCDJG2Entity.OTCBZ;
            this.LINGCHAJBZ = yaoPinCDJG2Entity.LINGCHAJBZ;
            this.PISHISJ = yaoPinCDJG2Entity.PISHISJ;
            this.JIYAOZS = yaoPinCDJG2Entity.JIYAOZS;
            this.MIANFEIYPBZ = yaoPinCDJG2Entity.MIANFEIYPBZ;
            this.ZENGPINBZ = yaoPinCDJG2Entity.ZENGPINBZ;
            this.WAIBUBM1 = yaoPinCDJG2Entity.WAIBUBM1;
            this.WAIBUBM2 = yaoPinCDJG2Entity.WAIBUBM2;
            this.WAIBUBM3 = yaoPinCDJG2Entity.WAIBUBM3;
            ComputeJiGe( maxYaoPinGG, maxGGYaoPinCJJG);
        }

        /// <summary>
        /// 拆分药品产地价格
        /// </summary>
        /// <param name="guiGID"></param>
        /// <param name="baoZhuangLiang"></param>
        /// <param name="baoZhuangDW"></param>
        /// <returns>success 拆分成功 ，exist 已存在 </returns>
        public string ChaiFenYaoPinCDJG(GY_YAOPINMCGG2 yaoPinGGEntity, decimal baoZhuangLiang, string baoZhuangDW)
        {
            string daGuiGeID = this.DAGUIGID;
            decimal zhaoBiaoJJ = Convert.ToDecimal(this.ZHAOBIAOJJ);
            decimal piFaJia = Convert.ToDecimal(this.PIFAJIA);
            decimal zuiGaoPFJ = Convert.ToDecimal(this.ZUIGAOPFJ);
            decimal zuiGaoLSJ = Convert.ToDecimal(this.ZUIGAOLSJ);
            decimal pingJunJJ = Convert.ToDecimal(this.PINGJUNJJ);
            decimal danJia1 = Convert.ToDecimal(this.DANJIA1);
            decimal danJia2 = Convert.ToDecimal(this.DANJIA2);
            decimal danJia3 = Convert.ToDecimal(this.DANJIA3);
            decimal danJia4 = Convert.ToDecimal(this.DANJIA4);
            decimal danJia5 = Convert.ToDecimal(this.DANJIA5);
            decimal danJia6 = Convert.ToDecimal(this.DANJIA6);
            decimal danJia7 = Convert.ToDecimal(this.DANJIA7);
            decimal danJia8 = Convert.ToDecimal(this.DANJIA8);
            decimal danJia9 = Convert.ToDecimal(this.DANJIA9);
            decimal danJia10 = Convert.ToDecimal(this.DANJIA10);
            decimal danJia11 = Convert.ToDecimal(this.DANJIA11);
            decimal danJia12 = Convert.ToDecimal(this.DANJIA12);
            decimal danJia13 = Convert.ToDecimal(this.DANJIA13);
            decimal danJia14 = Convert.ToDecimal(this.DANJIA14);
            decimal danJia15 = Convert.ToDecimal(this.DANJIA15);
            decimal danJia16 = Convert.ToDecimal(this.DANJIA16);
            decimal danJia17 = Convert.ToDecimal(this.DANJIA17);
            decimal danJia18 = Convert.ToDecimal(this.DANJIA18);
            decimal danJia19 = Convert.ToDecimal(this.DANJIA19);
            decimal danJia20 = Convert.ToDecimal(this.DANJIA20);

            decimal baoZhuangLiang_DB = Convert.ToDecimal(baoZhuangLiang);

            decimal baoZhuangLiang_daguige = Convert.ToDecimal(this.BAOZHUANGLIANG);

            //var yaoPinGGEntity = DBContext.Set<GY_YAOPINMCGG2>().Where(o => o.GUIGEID == guiGID).AsNoTracking().FirstOrDefault();

            //规格ID
            // var jiaGeID = this.GetOrder("GY_YAOPINCDJG2", ServiceContext.YUANQUID, 1)[0];

            //JiaGeID = jiaGeID;

            this.GUIGEID = yaoPinGGEntity.GUIGEID;
            this.XIAOGUIGID = yaoPinGGEntity.XIAOGUIGID;
            this.DAGUIGID = yaoPinGGEntity.DAGUIGID;

            this.MENYAOSY = yaoPinGGEntity.MENYAOSY;
            this.YAOKUSY = yaoPinGGEntity.YAOKUSY;
            this.BINGYAOSY = yaoPinGGEntity.BINGYAOSY;
            this.JINGMAIPSY = yaoPinGGEntity.JINGMAIPSY;
            this.ZHIJISY = yaoPinGGEntity.ZHIJISY;
            this.MENYAOSY_ZY = yaoPinGGEntity.MENYAOSY_ZY;
            this.BINGYAOSY_MZ = yaoPinGGEntity.BINGYAOSY_MZ;

            this.YAOPINGG = yaoPinGGEntity.YAOPINGG;
            this.BAOZHUANGDW = string.IsNullOrEmpty(baoZhuangDW) ? this.ZUIXIAODW : baoZhuangDW;
            this.BAOZHUANGLIANG = baoZhuangLiang;
            this.XIUGAISJ = GetSYSTime();
            this.XIUGAIREN = ServiceContext.USERID;
            this.LINCHUANGSYBZ = 1;

            if (this.ZHAOBIAOJJ == null)
            {
                this.ZHAOBIAOJJ = null;
            }
            else
            {
                this.ZHAOBIAOJJ = Math.Round(zhaoBiaoJJ * baoZhuangLiang_DB / baoZhuangLiang_daguige, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

            }
            if (this.PIFAJIA == null)
            {
                this.PIFAJIA = null;
            }
            else
            {
                this.PIFAJIA = Math.Round(piFaJia * baoZhuangLiang_DB / baoZhuangLiang_daguige, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

            }

            if (this.ZUIGAOPFJ == null)
            {
                this.ZUIGAOPFJ = null;
            }
            else
            {
                this.ZUIGAOPFJ = Math.Round(zuiGaoPFJ * baoZhuangLiang_DB / baoZhuangLiang_daguige, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

            }
            if (this.ZUIGAOLSJ == null)
            {
                this.ZUIGAOLSJ = null;
            }
            else
            {
                this.ZUIGAOLSJ = Math.Round(zuiGaoLSJ * baoZhuangLiang_DB / baoZhuangLiang_daguige, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

            }
            if (this.PINGJUNJJ == null)
            {
                this.PINGJUNJJ = null;
            }
            else
            {
                this.PINGJUNJJ = Math.Round(pingJunJJ * baoZhuangLiang_DB / baoZhuangLiang_daguige, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);
            }
            if (this.DANJIA1 == null)
            {
                this.DANJIA1 = null;
            }
            else
            {
                this.DANJIA1 = Math.Round(danJia1 * baoZhuangLiang_DB / baoZhuangLiang_daguige, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

            }
            if (this.DANJIA2 == null)
            {
                this.DANJIA2 = null;
            }
            else
            {
                this.DANJIA2 = Math.Round(danJia2 * baoZhuangLiang_DB / baoZhuangLiang_daguige, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

            }
            if (this.DANJIA3 == null)
            {
                this.DANJIA3 = null;
            }
            else
            {
                this.DANJIA3 = Math.Round(danJia3 * baoZhuangLiang_DB / baoZhuangLiang_daguige, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

            }
            if (this.DANJIA4 == null)
            {
                this.DANJIA4 = null;
            }
            else
            {
                this.DANJIA4 = Math.Round(danJia4 * baoZhuangLiang_DB / baoZhuangLiang_daguige, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

            }
            if (this.DANJIA5 == null)
            {
                this.DANJIA5 = null;
            }
            else
            {
                this.DANJIA5 = Math.Round(danJia5 * baoZhuangLiang_DB / baoZhuangLiang_daguige, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);
            }
            if (this.DANJIA6 == null)
            {
                this.DANJIA6 = null;
            }
            else
            {
                this.DANJIA6 = Math.Round(danJia6 * baoZhuangLiang_DB / baoZhuangLiang_daguige, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

            }
            if (this.DANJIA7 == null)
            {
                this.DANJIA7 = null;
            }
            else
            {
                this.DANJIA7 = Math.Round(danJia7 * baoZhuangLiang_DB / baoZhuangLiang_daguige, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

            }
            if (this.DANJIA8 == null)
            {
                this.DANJIA8 = null;
            }
            else
            {
                this.DANJIA8 = Math.Round(danJia8 * baoZhuangLiang_DB / baoZhuangLiang_daguige, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

            }
            if (this.DANJIA9 == null)
            {
                this.DANJIA9 = null;
            }
            else
            {
                this.DANJIA9 = Math.Round(danJia9 * baoZhuangLiang_DB / baoZhuangLiang_daguige, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

            }
            if (this.DANJIA10 == null)
            {
                this.DANJIA10 = null;
            }
            else
            {
                this.DANJIA10 = Math.Round(danJia10 * baoZhuangLiang_DB / baoZhuangLiang_daguige, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);
            }
            if (this.DANJIA11 == null)
            {
                this.DANJIA11 = null;
            }
            else
            {
                this.DANJIA11 = Math.Round(danJia11 * baoZhuangLiang_DB / baoZhuangLiang_daguige, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

            }
            if (this.DANJIA12 == null)
            {
                this.DANJIA12 = null;
            }
            else
            {
                this.DANJIA12 = Math.Round(danJia12 * baoZhuangLiang_DB / baoZhuangLiang_daguige, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

            }
            if (this.DANJIA13 == null)
            {
                this.DANJIA13 = null;
            }
            else
            {
                this.DANJIA13 = Math.Round(danJia13 * baoZhuangLiang_DB / baoZhuangLiang_daguige, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

            }
            if (this.DANJIA14 == null)
            {
                this.DANJIA14 = null;
            }
            else
            {
                this.DANJIA14 = Math.Round(danJia14 * baoZhuangLiang_DB / baoZhuangLiang_daguige, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

            }
            if (this.DANJIA15 == null)
            {
                this.DANJIA15 = null;
            }
            else
            {
                this.DANJIA15 = Math.Round(danJia15 * baoZhuangLiang_DB / baoZhuangLiang_daguige, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

            }
            if (this.DANJIA16 == null)
            {
                this.DANJIA16 = null;
            }
            else
            {
                this.DANJIA16 = Math.Round(danJia16 * baoZhuangLiang_DB / baoZhuangLiang_daguige, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

            }
            if (this.DANJIA17 == null)
            {
                this.DANJIA17 = null;
            }
            else
            {
                this.DANJIA17 = Math.Round(danJia17 * baoZhuangLiang_DB / baoZhuangLiang_daguige, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

            }
            if (this.DANJIA18 == null)
            {
                this.DANJIA18 = null;
            }
            else
            {
                this.DANJIA18 = Math.Round(danJia18 * baoZhuangLiang_DB / baoZhuangLiang_daguige, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

            }
            if (this.DANJIA19 == null)
            {
                this.DANJIA19 = null;
            }
            else
            {
                this.DANJIA19 = Math.Round(danJia19 * baoZhuangLiang_DB / baoZhuangLiang_daguige, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);

            }
            if (this.DANJIA20 == null)
            {
                this.DANJIA20 = null;
            }
            else
            {
                this.DANJIA20 = Math.Round(danJia20 * baoZhuangLiang_DB / baoZhuangLiang_daguige, HISGlobalHelper.GlobalConst.gs_ShuLiangXSW);
            }

            this.TIAOXINGMA = null;
            return "success";
        }
        /// <summary>
        /// 更新最高 批发 零售 价
        /// </summary>
        /// <param name="eYaoPinCDJG2"></param>
        public void UpdateZuiGaoPLJ(E_GY_YAOPINCDJG2 eYaoPinCDJG2)
        {
            this.ZUIGAOPFJ = eYaoPinCDJG2.ZUIGAOPFJ;
            this.ZUIGAOLSJ = eYaoPinCDJG2.ZUIGAOLSJ;
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();
        }
        /// <summary>
        /// 更新 担保类型 名称
        /// </summary>
        /// <param name="eYaoPinCDJG2"></param>
        public void UpdateDanBaoLX(E_GY_YAOPINCDJG2 eYaoPinCDJG2)
        {
            this.DANBAOLX = eYaoPinCDJG2.DANBAOLX;
            this.DANBAOLXMC = eYaoPinCDJG2.DANBAOLXMC;
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = GetSYSTime();
        }
    } 
}
