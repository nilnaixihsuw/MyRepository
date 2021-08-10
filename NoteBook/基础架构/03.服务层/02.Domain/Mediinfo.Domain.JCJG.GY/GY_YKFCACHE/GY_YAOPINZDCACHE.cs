using Mediinfo.Infrastructure.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediinfo.Domain.JCJG.GY
{
    /// <summary>
    /// 药品字典缓存
    /// </summary>
    public class GY_YAOPINZDCACHE : EntityBase
    {
        /// <summary>
        /// 缓存主键
        /// </summary>
        public string CACHEID { get; set; }
        /// <summary>
        /// 应用ID(MY_KUCUN2.YINGYONGID)
        /// </summary>
        public string YINGYONGID { get; set; }
        /// <summary>
        /// 价格ID(GY_YAOPINCDJG2.JIAGEID)
        /// </summary>
        public string JIAGEID { get; set; }
        /// <summary>
        /// 小规格价格ID(MY_KUCUN2.JIAGEID)
        /// </summary>
        public string MINJIAGEID { get; set; }
        /// <summary>
        /// 规格ID(GY_YAOPINMCGG2.GUIGEID)
        /// </summary>
        public string GUIGEID { get; set; }
        /// <summary>
        /// 摆放位置(GY_YAOPINBFWZ.WEIZHISM)
        /// </summary>
        public string BAIFANGWZ { get; set; }
        /// <summary>
        /// 药品别名(GY_YAOPINZBM.YAOPINMC)
        /// </summary>
        public string YAOPINBM { get; set; }
        /// <summary>
        /// 其它属性(GY_YAOPINCDJG2.QITASX)
        /// </summary>
        public string QITASX { get; set; }
        /// <summary>
        /// 毒理分类(GY_YAOPINCDJG2.DULIFL)
        /// </summary>
        public string DULIFL { get; set; }
        /// <summary>
        /// 账簿类别(GY_YAOPINCDJG2.ZHANGBULB)
        /// </summary>
        public string ZHANGBULB { get; set; }
        /// <summary>
        /// 价值分类(GY_YAOPINCDJG2.JIAZHIFL)
        /// </summary>
        public string JIAZHIFL { get; set; }
        /// <summary>
        /// 用药简要说明(GY_YAOPINMCGG2.YONGYAOJYSM)
        /// </summary>
        public string YONGYAOJYSM { get; set; }
        /// <summary>
        /// 警示颜色(GY_YAOPINMCGG2.JINGSHIYS)
        /// </summary>
        public long? JINGSHIYS { get; set; }
        /// <summary>
        /// 作废标志(GY_YAOPINCDJG2.ZUOFEIBZ)
        /// </summary>
        public int? ZUOFEIBZ { get; set; }
        /// <summary>
        /// 停用标志(GY_YAOPINCDJG2.TINGYONGBZ)
        /// </summary>
        public int? TINGYONGBZ { get; set; }
        /// <summary>
        /// 使用频率(MY_KUCUN2.SHIYONGPL)
        /// </summary>
        public string SHIYONGPL { get; set; }
        /// <summary>
        /// 门药库存数量(MY_KUCUN2.KUCUNSL)
        /// </summary>
        public decimal? MYKUCUNSL { get; set; }
        /// <summary>
        /// 药库库存数量(YK_KUCUN2.KUCUNSL)
        /// </summary>
        public decimal? YKKUCUNSL { get; set; }
        /// <summary>
        /// 已收未发数量(MY_KUCUN2.YISHOUWFSL)
        /// </summary>
        public decimal? YISHOUWFSL { get; set; }
        /// <summary>
        /// 医嘱已收未发数量(MY_KUCUN2.YISHOUWFSLYZ)
        /// </summary>
        public decimal? YISHOUWFSLYZ { get; set; }
        /// <summary>
        /// 预扣库存数量(MY_KUCUN2.YUKOUCKSL)
        /// </summary>
        public decimal? YUKOUKCSL { get; set; }
        /// <summary>
		/// 进价(MY_KUCUN2.JINJIA)
		/// </summary>
		public decimal? JINJIA { get; set; }
        /// <summary>
		/// 零售价(MY_KUCUN2.LINGSHOUJIA)
		/// </summary>
		public decimal? LINGSHOUJIA { get; set; }
        /// <summary>
        /// 库存上限(GY_YAOPINCLKZ.KUCUNSX)
        /// </summary>
        public decimal? KUCUNSX { get; set; }
        /// <summary>
        /// 库存下限(GY_YAOPINCLKZ.KUCUNXX)
        /// </summary>
        public decimal? KUCUNXX { get; set; }
        /// <summary>
        /// 库存警戒线(GY_YAOPINCLKZ.KUCUNJJX)
        /// </summary>
        public decimal? KUCUNJJX { get; set; }
        /// <summary>
        /// 请领固定数量(GY_YAOPINCLKZ.QINGLINGGDSL)
        /// </summary>
        public decimal? QINGLINGGDSL { get; set; }
        /// <summary>
        /// 采购固定数量(GY_YAOPINCLKZ.CAIGOUGDSL)
        /// </summary>
        public decimal? CAIGOUGDSL { get; set; }
        /// <summary>
        /// 中药配方颗粒转换数量(GY_ZhongYaoPFKLZD.ZhuanHuanSL)
        /// </summary>
        public decimal? ZHONGYAOPFKLZHSL { get; set; }
    }
}
