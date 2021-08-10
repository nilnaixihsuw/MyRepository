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
        /// ���������Ϣ
        /// </summary>
        /// <returns></returns>
        public ZJ_JIUZHENXX UpdateJZXX()
        {
            this.JIUZHENZT = 0; //����״̬ = 0 δ����
            this.BINGRENQX = ""; //����ȥ��
            this.ZHAOHUIBZ = 0; //�ٻر�־
            this.ZHUSU = ""; //����
            this.JIANYAOBS = ""; //�ֲ�ʷ
            this.TIGEJC = ""; //�����
            this.LINCHUANGZD = ""; //�ٴ����
            this.JIBINGFL = ""; //��������
            this.SHOUSUOYA = null; //����ѹ
            this.SHUZHANGYA = null; //����ѹ
            this.CHUZHI = ""; //����
            this.BINGSHIJWS = ""; //����ʷ
            this.BINGSHIQT = ""; //��������ʷ
            this.TIWEN = null; //����
            this.MAIBO = null; //����
            this.HUXI = null; //����
            this.SHENGAO = null; //���
            this.TIZHONG = null; //����
            this.JIUZHENYS = null; //����ҽ��
            this.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// ���¾�����Ϣ����
        /// </summary>
        /// <param name="eJiuZhenXX"></param>
        /// <returns></returns>
        public ZJ_JIUZHENXX UpdateZJBL(E_ZJ_JIUZHENXX eJiuZhenXX)
        {
            this.ZHUSU = eJiuZhenXX.ZHUSU;
            this.TIGEJC = eJiuZhenXX.TIGEJC;//�����
            this.CHUZHI = eJiuZhenXX.CHUZHI;//���á���
            this.JIANYAOBS = eJiuZhenXX.JIANYAOBS;//�ֲ�ʷ����
            this.BINGSHIJWS = eJiuZhenXX.BINGSHIJWS;//����ʷ����
            this.BINGSHIQT = eJiuZhenXX.BINGSHIQT;//������ʷ����
            this.ZHONGYISZ = eJiuZhenXX.ZHONGYISZ;//��ҽ�����
            this.FUZHUJC = eJiuZhenXX.FUZHUJC;// ������顪��
            this.ZHUYISX = eJiuZhenXX.ZHUYISX;//ע�������
            this.BINGSHI_JZ = eJiuZhenXX.BINGSHI_JZ;// ����ʷ���� 
            this.BINGSHI_SS = eJiuZhenXX.BINGSHI_SS;// ����ʷ����
            this.BINGSHI_HY = eJiuZhenXX.BINGSHI_HY;//����ʷ����
            this.BINGSHI_WS = eJiuZhenXX.BINGSHI_WS;// ����ʷ����
            this.BINGSHI_YJ = eJiuZhenXX.BINGSHI_YJ;//�¾�ʷ���� 
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
        /// ���¾�����Ϣ����
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
        /// ���¾�����Ϣ��������
        /// </summary>
        /// <param name="eJiuZhenXX"></param>
        /// <returns></returns>
        public ZJ_JIUZHENXX UpdateSMTZ(E_ZJ_JIUZHENXX eJiuZhenXX)
        {
            this.SHENGAO = eJiuZhenXX.SHENGAO;//��ߡ���
            this.TIGEJC = eJiuZhenXX.TIGEJC;//���ء���
            this.TIWEN = eJiuZhenXX.TIWEN;//���¡���
            this.SHOUSUOYA = eJiuZhenXX.SHOUSUOYA; // ����ѹ���� 
            this.SHUZHANGYA = eJiuZhenXX.SHUZHANGYA;// ����ѹ����
            //this. = eJiuZhenXX.ZHUANTAi;// ����״̬����
            this.GUOMINSHI = eJiuZhenXX.GUOMINSHI; // ����ʷ���� 


            this.RegisterUpdate(this);
            return this;
        }

        /// <summary>
        /// ������¾�����Ϣ����
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
        /// ȡ������
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
        /// �ٻ�
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
        /// ��ɾ���
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
            if (this.SHOUZHENSJ == null )//��һ����ɾ���,Ҫ����������Ϣ
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
