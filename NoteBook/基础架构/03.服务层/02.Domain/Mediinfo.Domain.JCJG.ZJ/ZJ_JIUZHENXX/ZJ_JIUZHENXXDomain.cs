using Mediinfo.DTO.HIS.ZJ;
using Mediinfo.Infrastructure.Core.Domain;
using System;
namespace Mediinfo.Domain.JCJG.ZJ
{
    public partial class ZJ_JIUZHENXX
    {
        public ZJ_JIUZHENXX GuaHao(string guaHaoXH)
        {
            this.GUAHAOXH = guaHaoXH;
            return this;
        }
        public ZJ_JIUZHENXX ZuoFei()
        {
            this.ZUOFEIBZ = 1;
            return this;
        }
        public void Delete()
        {
            this.RegisterDelete(this);
        }

        public ZJ_JIUZHENXX updateZH(int zhaoHuiBZ)
        {
            this.ZHAOHUIBZ = zhaoHuiBZ;
            this.RegisterUpdate<ZJ_JIUZHENXX>(this);
            return this;
        }

        public ZJ_JIUZHENXX updateDYBZ()
        {
            this.DIAOYUEBZ = 1;
            this.RegisterUpdate<ZJ_JIUZHENXX>(this);
            return this;
        }

        public ZJ_JIUZHENXX UpdateCZBZ(int chuZhenBZ)
        {
            this.CHUZHENBZ = chuZhenBZ;
            this.RegisterUpdate<ZJ_JIUZHENXX>(this);
            return this;
        }

        public ZJ_JIUZHENXX UpdateXY(string shuZhangYa, string shouSuoYa)
        {
            this.SHUZHANGYA = Convert.ToDecimal(shuZhangYa);
            this.SHOUSUOYA = Convert.ToDecimal(shouSuoYa);
            this.RegisterUpdate<ZJ_JIUZHENXX>(this);
            return this;
        }

        public ZJ_JIUZHENXX UpdateLCZD(string jiBingFL, string linChuangZD)
        {
            this.JIBINGFL = jiBingFL;
            this.LINCHUANGZD = linChuangZD;
            this.RegisterUpdate<ZJ_JIUZHENXX>(this);
            return this;
        }

        /// <summary>
        /// 清除就诊信息
        /// </summary>
        /// <returns></returns>
        public ZJ_JIUZHENXX UpdateJZXX()
        {
            this.JIUZHENZT = 0; //就诊状态 = 0 未接诊
            this.BINGRENQX = ""; //病人去向
            this.ZHAOHUIBZ = 0; //召回标志
            this.ZHUSU = ""; //主诉
            this.JIANYAOBS = ""; //现病史
            this.TIGEJC = ""; //体格检查
            this.LINCHUANGZD = ""; //临床诊断
            this.JIBINGFL = ""; //疾病分类
            this.SHOUSUOYA = null; //收缩压
            this.SHUZHANGYA = null; //舒张压
            this.CHUZHI = ""; //处置
            this.BINGSHIJWS = ""; //既往史
            this.BINGSHIQT = ""; //其他既往史
            this.TIWEN = null; //体温
            this.MAIBO = null; //脉搏
            this.HUXI = null; //呼吸
            this.SHENGAO = null; //身高
            this.TIZHONG = null; //体重
            this.JIUZHENYS = null; //就诊医生
            this.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 更新就诊信息病历
        /// </summary>
        /// <param name="eJiuZhenXX"></param>
        /// <returns></returns>
        public ZJ_JIUZHENXX UpdateZJBL(E_ZJ_JIUZHENXX eJiuZhenXX)
        {
            this.ZHUSU = eJiuZhenXX.ZHUSU;
            this.TIGEJC = eJiuZhenXX.TIGEJC;//体格检查
            this.CHUZHI = eJiuZhenXX.CHUZHI;//处置——
            this.JIANYAOBS = eJiuZhenXX.JIANYAOBS;//现病史——
            this.BINGSHIJWS = eJiuZhenXX.BINGSHIJWS;//既往史——
            this.BINGSHIQT = eJiuZhenXX.BINGSHIQT;//其他病史——
            this.ZHONGYISZ = eJiuZhenXX.ZHONGYISZ;//中医四诊——
            this.FUZHUJC = eJiuZhenXX.FUZHUJC;// 辅助检查——
            this.ZHUYISX = eJiuZhenXX.ZHUYISX;//注意事项——
            this.BINGSHI_JZ = eJiuZhenXX.BINGSHI_JZ;// 家族史—— 
            this.BINGSHI_SS = eJiuZhenXX.BINGSHI_SS;// 手术史——
            this.BINGSHI_HY = eJiuZhenXX.BINGSHI_HY;//婚育史——
            this.BINGSHI_WS = eJiuZhenXX.BINGSHI_WS;// 外伤史——
            this.BINGSHI_YJ = eJiuZhenXX.BINGSHI_YJ;//月经史—— 
            this.GUOMINSHI = eJiuZhenXX.GUOMINSHI;
            this.YUNZHOU = eJiuZhenXX.YUNZHOU;
            this.SHUZHANGYA = eJiuZhenXX.SHUZHANGYA;
            this.SHOUSUOYA = eJiuZhenXX.SHOUSUOYA;
            this.TIWEN = eJiuZhenXX.TIWEN;
            this.MAIBO = eJiuZhenXX.MAIBO;
            this.HUXI = eJiuZhenXX.HUXI;
            this.DABINGZHONG = eJiuZhenXX.DABINGZHONG;
            this.XIAOBINGZHONG = eJiuZhenXX.XIAOBINGZHONG;
            this.SHENGAO = eJiuZhenXX.SHENGAO;
            this.TIZHONG = eJiuZhenXX.TIZHONG;
            this.GONGGAO = eJiuZhenXX.GONGGAO;
            this.FUWEI = eJiuZhenXX.FUWEI;
            this.TAIXIN = eJiuZhenXX.TAIXIN;
            this.TAIWEI = eJiuZhenXX.TAIWEI;
            this.FUZHONG = eJiuZhenXX.FUZHONG;
            this.LINCHUANGZD = eJiuZhenXX.LINCHUANGZD;
            this.JIBINGFL = eJiuZhenXX.JIBINGFL;
            this.CHUZHENBZ = eJiuZhenXX.CHUZHENBZ;
            this.KOUFUKNYWSBZ = eJiuZhenXX.KOUFUKNYWSBZ;
            this.XUEYANGBHD = eJiuZhenXX.XUEYANGBHD;
            this.RegisterUpdate(this);
            return this;
        }


        /// <summary>
        /// 更新就诊信息病历
        /// </summary>
        /// <param name="eJiuZhenXX"></param>
        /// <returns></returns>
        public ZJ_JIUZHENXX UpdateJiuYiZFFS(E_ZJ_JIUZHENXX eJiuZhenXX)
        {
            this.JIUYIZFFS = eJiuZhenXX.JIUYIZFFS;
            this.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 更新就诊信息生命体征
        /// </summary>
        /// <param name="eJiuZhenXX"></param>
        /// <returns></returns>
        public ZJ_JIUZHENXX UpdateSMTZ(E_ZJ_JIUZHENXX eJiuZhenXX)
        {
            this.SHENGAO = eJiuZhenXX.SHENGAO;//身高——
            this.TIGEJC = eJiuZhenXX.TIGEJC;//体重——
            this.TIWEN = eJiuZhenXX.TIWEN;//体温——
            this.SHOUSUOYA = eJiuZhenXX.SHOUSUOYA; // 收缩压—— 
            this.SHUZHANGYA = eJiuZhenXX.SHUZHANGYA;// 舒张压——
            //this. = eJiuZhenXX.ZHUANTAi;// 生理状态——
            this.GUOMINSHI = eJiuZhenXX.GUOMINSHI; // 过敏史—— 


            this.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// 接诊更新就诊信息数据
        /// </summary>
        /// <param name="keShiID"></param>
        /// <param name="yiShengID"></param>
        /// <param name="yingYongID"></param>
        /// <returns></returns>
        public ZJ_JIUZHENXX JieZhen(string keShiID, string yiShengID, string yingYongID)
        {
            var dateTime = this.GetSYSTime();
            this.XIUGAIBZ = "1";
            this.XIUGAIREN = yiShengID;
            this.XIUGAISJ = dateTime;
            this.XIUGAIYYID = yingYongID;
            //if (this.JIEZHENSJ == null)
            //{
            //    this.JIEZHENSJ = dateTime;
            //    this.JIUZHENKS = keShiID;
            //    this.JIUZHENYS = yiShengID;
            //}    
            return this;
        }
        /// <summary>
        /// 取消就诊
        /// </summary>
        /// <param name="keShiID"></param>
        /// <param name="yiShengID"></param>
        /// <param name="yingYongID"></param>
        /// <returns></returns>
        public ZJ_JIUZHENXX QuXiaoJZ()
        {
            this.ZHAOHUIBZ = 0;
            this.XIUGAIBZ = "0";
            //this.XIUGAIREN = yiShengID;
            //this.XIUGAISJ = this.GetSYSTime();
            //this.XIUGAIYYID = yingYongID;

            //this.JIUZHENKS = keShiID;
            //this.JIUZHENYS = "";

            return this;
        }
        /// <summary>
        /// 召回
        /// </summary>
        /// <param name="zhaoHuiYYID"></param>
        /// <returns></returns>
        public ZJ_JIUZHENXX ZhaoHui(string zhaoHuiYYID)
        {
            this.ZHAOHUIBZ = 1;
            this.ZHAOHUIYYID = zhaoHuiYYID;
            return this;
        }
        /// <summary>
        /// 完成就诊
        /// </summary>
        /// <param name="jiuZhenKS"></param>
        /// <param name="jiuZhenYS"></param>
        /// <param name="bingRenQX"></param>
        /// <returns></returns>
        public ZJ_JIUZHENXX WanChengJZ(string jiuZhenKS, string jiuZhenYS, string bingRenQX)
        {
            this.ZHAOHUIBZ = 0;
            this.JIUZHENZT = 2;
            this.XIUGAIBZ = "0";
            this.JIUZHENKS = jiuZhenKS;
            this.JIUZHENYS = jiuZhenYS;
            var shiJian = this.GetSYSTime();
            this.JIUZHENRQ = shiJian;
            if (!string.IsNullOrWhiteSpace(bingRenQX))
                this.BINGRENQX = bingRenQX;
            if (this.SHOUZHENSJ == null )//第一次完成就诊,要更新首诊信息
            {
                this.SHOUZHENKS = jiuZhenKS;
                this.SHOUZHENYS = jiuZhenYS;
                this.SHOUZHENSJ = shiJian;
            }
            
            return this;
        }

        public ZJ_JIUZHENXX Update(E_ZJ_JIUZHENXX entity)
        {
            this.MargeDTO<ZJ_JIUZHENXX, E_ZJ_JIUZHENXX>(entity, false);
            return this;
        }

        public ZJ_JIUZHENXX UpdateGuiDingBZ(int guiDingBZBZ, string teShuSPBH)
        {
            this.GUIDINGBZBZ = guiDingBZBZ;
            this.TESHUSPBH = teShuSPBH;
            return this;
        }
    }
}
