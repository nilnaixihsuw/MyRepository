using Mediinfo.DTO.HIS.ZY;
using Mediinfo.Infrastructure.Core.Domain;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mediinfo.Domain.JCJG.ZY
{
    public partial class ZY_BINGRENXX
    {
        public ZY_BINGRENXX UpdateZhuYuanBRXX(E_ZY_BINGRENXX_EX ebingrenxx)
        {
            this.MargeDTO<ZY_BINGRENXX, E_ZY_BINGRENXX_EX>(ebingrenxx);
            return this;
        }

        public ZY_BINGRENXX UpdateZhuYuanBRXX(E_ZY_BINGRENXX ebingrenxx)
        {
            this.MargeDTO<ZY_BINGRENXX, E_ZY_BINGRENXX>(ebingrenxx);
            return this;
        }

        /// <summary>
        /// 更新病人信息
        /// </summary>
        /// <returns></returns>
        public ZY_BINGRENXX Update()
        {
            this.RegisterUpdate(this);
            return this;
        }
        /// <summary>
        /// 更新绿色通道信息
        /// </summary>
        /// <returns></returns>
        public ZY_BINGRENXX UpdateLvSeTD(int lvSeTDBZ,DateTime? dateTime)
        {
            this.LVSETDBZ = lvSeTDBZ;
            this.LVSETDKQRQ = dateTime;
            return this;
        }
        /// <summary>
        /// 更新离院去向
        /// </summary>
        /// <returns></returns>
        public ZY_BINGRENXX UpdateLiYuanQX(string liYuanQX)
        {
            this.LIYUANQX = liYuanQX;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 更新住院病人信息
        /// </summary>
        /// <param name="ZhuZhiYS"></param>
        /// <param name="ChuangWeiID"></param>
        /// <returns></returns>
        public ZY_BINGRENXX UpDateZhuYuanBRXX(string ZhuZhiYS, string ChuangWeiID)
        {
            //如果入院登记时没有选床位，则首次分配的床位作为入院床位，
            if (string.IsNullOrEmpty(this.RUYUANCW))
            {
                this.RUYUANCW = ChuangWeiID;
            }
            this.DANGQIANCW = ChuangWeiID;
            this.ZHANCHUANGBZ = 1;
            this.ZHUZHIYS = ZhuZhiYS;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 更新占床标志
        /// </summary>
        /// <param name="ZhanChuangBZ"></param>
        /// <returns></returns>
        public ZY_BINGRENXX UpDateBingRenZCBZ(int ZhanChuangBZ)
        {

            this.CHUANGWEIZYBZ = ZhanChuangBZ;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 更新婴儿数量
        /// </summary>
        /// <param name="yingErSL"></param>
        /// <returns></returns>
        public ZY_BINGRENXX UpdateYingErSL(int yingErSL)
        {

            this.YINGERSL = yingErSL;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 更新婴儿信息，预出院日期
        /// </summary>
        /// <param name="yuChuYuanRQ"></param>
        /// <returns></returns>
        public ZY_BINGRENXX UpdateYingErXX(DateTime? yuChuYuanRQ)
        {
            this.YUCHUYRQ = yuChuYuanRQ;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 更新 YaoPinBZYBZ （药品不转移标志）
        /// </summary>
        /// <param name="YaoPinBZYBZ"></param>
        /// <returns></returns>
        public ZY_BINGRENXX UpdateYaoPinBZYBZ(int YaoPinBZYBZ)
        {

            this.YAOPINBZYBZ = YaoPinBZYBZ;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }
        /// <summary>
        /// 更新主治医生
        /// </summary>
        /// <param name="ZhuZhiYS"></param>
        /// <param name="ZhuZhiYSXM"></param>
        /// <returns></returns>
        public ZY_BINGRENXX UpdateZhuZhiYS(string ZhuZhiYS, string ZhuZhiYSXM)
        {

            this.ZHUZHIYS = ZhuZhiYS;
            this.ZHUZHIYSXM = ZhuZhiYSXM;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }


        /// <summary>
        ///更新当前床位
        /// </summary>
        /// <param name="dangQianCW"></param>
        /// <returns></returns>
        public ZY_BINGRENXX UpdateDangQianCW(string dangQianCW)
        {

            this.DANGQIANCW = dangQianCW;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }
        /// <summary>
        /// 更新产妇标志
        /// </summary>
        /// <param name="chanFuBZ"></param>
        /// <returns></returns>
        public ZY_BINGRENXX UpdateChanFuBZ(int chanFuBZ)
        {

            this.CHANFUBZ = chanFuBZ;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 更新病人iD
        /// </summary>
        /// <param name="bingRenID"></param>
        /// <returns></returns>
        public ZY_BINGRENXX GengXinBingRenID(string bingRenID)
        {

            this.BINGRENID = bingRenID;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }


        /// <summary>
        /// 更新费用性质转换内容！
        /// </summary>
        /// <param name="chanFuBZ"></param>
        /// <returns></returns>
        public ZY_BINGRENXX GengxinFeiYongXZZHNR()
        {

            FEIYONGXZZHBZ = 1;
            FEIYONGXZZHR = ServiceContext.USERID;
            FEIYONGXZZHRQ = GetSYSTime();
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 删除病人信息
        /// </summary>
        /// <returns></returns>
        public ZY_BINGRENXX Delete()
        {
            IRepositoyBase.RegisterDelete<ZY_BINGRENXX>(this); //将本身在仓储中登记为删除
            return this;
        }

        /// <summary>
        /// 更新最后出院日期
        /// </summary>
        /// <param name="riQi"></param>
        /// <returns></returns>
        public ZY_BINGRENXX UpdateLastCYRQ(DateTime riQi)
        {

            this.LASTCHUYUANRQ = riQi;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        public ZY_BINGRENXX UpdateChuYRQ(DateTime? riQi)
        {

            this.CHUYUANRQ = riQi;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 出院前审核
        /// </summary>
        /// <param name="shenHeRen"></param>
        /// <returns></returns>
        public ZY_BINGRENXX ChuYuanSH(string shenHeRen)
        {
            this.SHENHEBZ = 1;
            this.SHENHEREN = shenHeRen;
            this.SHENHERQ = DateTime.Now;
            return this;
        }

        /// <summary>
        /// 取消出院前审核
        /// </summary>
        /// <returns></returns>
        public ZY_BINGRENXX QuXiaoSH()
        {
            this.SHENHEBZ = 0;
            this.SHENHEREN = "";
            this.SHENHERQ = null;
            return this;
        }
        /// <summary>
        /// 将该病人的病人信息转婴儿
        /// </summary>
        /// <returns></returns>
        public ZY_BINGRENXX SetMuQZYIDAndYingErBZ(string muQinZYID)
        {
            this.MUQINZYID = muQinZYID;
            this.YINGERBZ = 1;
            return this;
        }

        public ZY_BINGRENXX UpdateQuXCYYY(string quXiaoCYYY)
        {

            this.QUXIAOYCYYY = quXiaoCYYY;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        public ZY_BINGRENXX UpdateLCLJBZ(int lingChuangLJBZ)
        {
            this.LINCHUANGLJBZ = lingChuangLJBZ;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 病人信息进行出院操作
        /// </summary>
        /// <param name="chuYuanRQ">指定出院日期</param>
        /// <param name="jieSuanXH">出院的结算序号</param>
        /// <returns></returns>
        public ZY_BINGRENXX UpdateChuYuan(DateTime chuYuanRQ, int jieSuanXH)
        {

            this.ZAIYUANZT = "2";
            this.CHUYUANRQ = chuYuanRQ;
            this.JIESUANXH = jieSuanXH;
            this.XIUGAIREN = ServiceContext.USERID;
            this.XIUGAISJ = IRepositoyBase.GetSYSTime();
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }
        public ZY_BINGRENXX RuKeCL(int RUKEBZ, int ZHANCHUANGBZ)
        {
            this.RUKEBZ = RUKEBZ;
            this.ZHANCHUANGBZ = ZHANCHUANGBZ;
            this.XIUGAISJ = IRepositoyBase.GetSYSTime();
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }
        /// <summary>
        /// 更新预出院日期,回滚数据
        /// </summary>
        /// <param name="riQi"></param>
        /// <returns></returns>
        public ZY_BINGRENXX HuiGunYuChuYRQ(DateTime? riQi)
        {
            this.YUCHUYRQ = riQi;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        ///住院天数
        ///ADD BY XYQ@20190423 HR6-355(468160)
        /// </summary>
        [NotMapped]
        public int ZhuYuanTS
        {
            get
            {
                DateTime chuYuanRQ;
                DateTime ruYuanRQ = this.RUYUANRQ ?? IRepositoyBase.GetSYSTime();
                if (this.ZAIYUANZT == "0")
                {
                    chuYuanRQ = IRepositoyBase.GetSYSTime();
                }
                else  //(this.ZAIYUANZT == "1")
                {
                    chuYuanRQ = this.YUCHUYRQ ?? IRepositoyBase.GetSYSTime();
                }
                //else
                //{
                //    chuYuanRQ = this.CHUYUANRQ ?? IRepositoyBase.GetSYSTime();
                //}

                var chaZhi = Convert.ToDateTime(chuYuanRQ.ToShortDateString()) - Convert.ToDateTime(ruYuanRQ.ToShortDateString());

                if (chaZhi.Days == 0)
                    return 1;
                else
                    return chaZhi.Days;


            }
            private set { }
        }

        /// <summary>
        /// 病人入科：通过PDA扫腕带完成病人从收费处到病区的入科操作，只记录入科时间
        /// ADD BY XYQ@20190423 HR6-355(468160)
        /// </summary>
        /// <param name="caoZuoRen"></param>
        /// <param name="caoZuoRXM"></param>
        /// <param name="caoZuoSJ"></param>
        /// <returns></returns>
        public ZY_BINGRENXX BingRenRK(string caoZuoRen, string caoZuoRXM, DateTime caoZuoSJ, string zhuZhiYS, string zhuZhiYSXM, string zeRenHS, string zeRenHSXM, string yiLiaoZID, string yiLiaoZM, string chuangWeiID, string keShiID, string keShiMC)
        {
            this.YUANRUKERQ = caoZuoSJ;
            this.SHOUCIRKR = caoZuoRen;
            this.SHOUCIRKRXM = caoZuoRXM;
            this.ZHUZHIYS = zhuZhiYS;
            this.ZHUZHIYSXM = zhuZhiYSXM;
            this.ZERENHS = zeRenHS;
            this.ZERENHSXM = zeRenHSXM;
            this.YILIAOZID = yiLiaoZID;
            this.YILIAOZM = yiLiaoZM;
            this.DANGQIANCW = chuangWeiID;
            this.DANGQIANKS = keShiID;
            this.DANGQIANKSMC = keShiMC;
            return this;
        }

        /// <summary>
        /// 跟新病人住院信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ZY_BINGRENXX GengXinBingRenZYXX(ZY_BINGRENXX model)
        {
            this.ZAIYUANZT = "0";
            this.SHENHEBZ = 0;
            this.RUKEBZ = 0;
            this.ZHANCHUANGBZ = model.ZHANCHUANGBZ;
            this.DANGQIANCW = model.DANGQIANCW;
            this.LIYUANQX = string.Empty;
            this.DANGQIANKS = model.DANGQIANKS;
            this.DANGQIANKSMC = model.DANGQIANKSMC;
            this.DANGQIANBQ = model.DANGQIANBQ;
            this.DANGQIANBQMC = model.DANGQIANBQMC;
            this.ZHUZHIYS = model.ZHUZHIYS;
            this.ZHUZHIYSXM = model.ZHUZHIYSXM;
            this.GUANLIKS = model.GUANLIKS;
            this.GUANLIKSMC = model.GUANLIKSMC;
            this.YILIAOZID = model.YILIAOZID;
            this.YILIAOZM = model.YILIAOZM;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 更新病人信息
        /// </summary>
        /// <returns></returns>
        public ZY_BINGRENXX GengXinBRXX()
        {
            this.ZAIYUANZT = "0";
            this.SHENHEBZ = 0;
            this.RUKEBZ = 0;
            this.ZHANCHUANGBZ = 1;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 更新病人的床位信息，主治医生，当前病区, 科室ID
        /// </summary>
        /// <param name="dangQianCW"></param>
        /// <param name="dangQianBQ"></param>
        /// <param name="dangQianKS"></param>
        /// <param name="guanLiKS"></param>
        /// <param name="zhuZhiYS"></param>
        /// <returns></returns>
        public ZY_BINGRENXX GengXinBRCWXX(string dangQianCW, string dangQianBQ, string dangQianKS, string guanLiKS, string zhuZhiYS)
        {
            this.DANGQIANCW = dangQianCW;
            this.DANGQIANBQ = dangQianBQ;
            this.DANGQIANKS = dangQianKS;
            this.GUANLIKS = guanLiKS;
            this.ZHANCHUANGBZ = 1;
            this.RUKEBZ = 1;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }
        /// <summary>
        /// 更新病人状态和结算序号 add by ql 
        /// </summary>
        /// <returns></returns>
        public ZY_BINGRENXX GengXinBRZTAndJSXH()
        {
            this.ZAIYUANZT = "1";
            this.JIESUANXH = 0;
            IRepositoyBase.RegisterUpdate(this);
            return this;
        }
        /// <summary>
        /// 入观
        /// </summary>
        /// <param name="jiuZhenID"></param>
        /// <param name="liuGuanSQDID"></param>
        /// <returns></returns>
        public ZY_BINGRENXX RuGuan(string jiuZhenID,string liuGuanSQDID)
        {
            this.LIUGUANBZ = 1;
            this.JIUZHENID = JIUZHENID;
            this.LIUGUANSQDID = liuGuanSQDID; 
            return this;
        }
    }
}
