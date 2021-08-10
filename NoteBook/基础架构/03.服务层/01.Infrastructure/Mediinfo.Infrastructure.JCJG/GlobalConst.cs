using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediinfo.Infrastructure.HIS
{
    public partial class HISGlobalHelper
    {
        public static class GlobalConst
        {
            public const int gs_ShuLiangXSW = 4;
            //药品分类类别
            public const string GY_YaoPinFL_FenLeiLB = "1";
            ////'111'库存管理类型   HR3-16365(200827)
            public const string KF_KuCunGLLX = "1110";

            //库房使用标志
            public const int GY_KuFangSY_YaoKu = 1; //药库使用
            public const int GY_KuFangSY_MenYao = 2; //门药使用
            public const int GY_KuFangSY_BingYao = 3; //病药使用
            public const int GY_KuFangSY_MenYao_ZY = 4; //门药使用_住院
            public const int GY_KuFangSY_BingYao_MZ = 5; //病药使用_门诊
            public const int GY_KuFangSY_JingMaiP = 6; //静脉配
            public const int GY_KuFangSY_ZhiJi = 7; //制剂(全部)
            public const int GY_KuFangSY_ZhiJiCP = 8; //制剂(成品)
            public const int GY_KuFangSY_ZhiJiFCP = 9; //制剂(非成品)
            public const int GY_KuFangSY_ZhongYaoKLSY = 10; //中药散配颗粒
            public const int GY_KuFangSY_MenYao_BingYao = 11; //根据应用ID的前两位，自动取门药使用或病药使用
            public const int GY_KuFangSY_ZhongYaoKLZBSY = 12;   //中药颗粒整包使用  HR3-13306(171599)
            public const int GY_KuFangSY_ZhongYaoKLSY_MenYao_ZY = 13; //住院用的散配颗粒
            public const int GY_KuFangSY_ZhongYaoKLZBSY_MenYao_ZY = 14; //住院用的整包颗粒
            public const int GY_KuFangSY_YinPianXBZ = 15;//门诊饮片小包装 HR3-20532(238252)
            public const int GY_KuFangSY_YinPianXBZ_ZY = 16;//住院饮片小包装 HR3-20532(238252)
        }
    }
}
