using Mediinfo.Infrastructure.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediinfo.Domain.JCJG.GY
{
    /// <summary>
    /// 库存缓存
    /// </summary>
    public class GY_KUCUNCACHE: EntityBase
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
        /// 规格IDS(GY_YAOPINMCGG2.GUIGEID)
        /// </summary>
        public string GUIGEIDS { get; set; }
        /// <summary>
        /// 分装父类(GY_YAOPINMCGG2.FZFL)
        /// </summary>
        public int FZFL { get; set; }
        /// <summary>
        /// 门药库存数量(MY_KUCUN2.KUCUNSL)
        /// </summary>
        public decimal? KUCUNSL { get; set; }
        /// <summary>
        /// 已收未发数量(MY_KUCUN2.YISHOUWFSL)
        /// </summary>
        public decimal? YISHOUWFSL { get; set; }

        public T2 DBToE<T1, T2>(T1 row)
        {
            throw new NotImplementedException();
        }
    }
}
