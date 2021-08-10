using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YINGYONG")]
	public partial class GY_YINGYONG : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 应用ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 系统ID
		/// </summary>
		[Required]
		[StringLength(2)]
		public string XITONGID { get; set; }
		/// <summary>
		/// 院区ID
		/// </summary>
		[Required]
		[StringLength(1)]
		public string YUANQUID { get; set; }
		/// <summary>
		/// 应用名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string YINGYONGMC { get; set; }
		/// <summary>
		/// 应用简称
		/// </summary>
		[StringLength(20)]
		public string YINGYONGJC { get; set; }
		/// <summary>
		/// 说明
		/// </summary>
		[StringLength(500)]
		public string SHUOMING { get; set; }
		/// <summary>
		/// 版本号
		/// </summary>
		[Required]
		[StringLength(20)]
		public string BANBENHAO { get; set; }
		/// <summary>
		/// 发布日期
		/// </summary>
		[Required]
		public DateTime? FABURQ { get; set; }
		/// <summary>
		/// 停用日期
		/// </summary>
		public DateTime? TINGYONGRQ { get; set; }
		/// <summary>
		/// 停用标志
		/// </summary>
		[Required]
		public int? TINGYONGBZ { get; set; }
		/// <summary>
		/// 急诊标志
		/// </summary>
		[Required]
		public int? JIZHENBZ { get; set; }
		/// <summary>
		/// 需要更新标志
		/// </summary>
		[Required]
		public int? XUYAOGXBZ { get; set; }
		/// <summary>
		/// 库存管理类型
		/// </summary>
		[StringLength(10)]
		public string KUCUNGLLX { get; set; }
		/// <summary>
		/// 科室ID
		/// </summary>
		[StringLength(10)]
		public string KESHIID { get; set; }
		/// <summary>
		/// 修改人
		/// </summary>
		[Required]
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		[Required]
		public DateTime? XIUGAISJ { get; set; }
		private int? _MENZHENJGTX;
		/// <summary>
		/// 门诊价格体系
		/// </summary>
		public int? MENZHENJGTX { get{ return _MENZHENJGTX; } set{ _MENZHENJGTX = value ?? 1; } }
		private int? _ZHUYUANJGTX;
		/// <summary>
		/// 住院价格体系
		/// </summary>
		public int? ZHUYUANJGTX { get{ return _ZHUYUANJGTX; } set{ _ZHUYUANJGTX = value ?? 1; } }
		private int? _MENZHENSHBZ;
		/// <summary>
		/// 门诊审核标志
		/// </summary>
		public int? MENZHENSHBZ { get{ return _MENZHENSHBZ; } set{ _MENZHENSHBZ = value ?? 0; } }
		private int? _ZHUYUANSHBZ;
		/// <summary>
		/// 住院审核标志
		/// </summary>
		public int? ZHUYUANSHBZ { get{ return _ZHUYUANSHBZ; } set{ _ZHUYUANSHBZ = value ?? 0; } }
		private int? _KESHIJSBZ;
		/// <summary>
		/// 是否接收科室消息
		/// </summary>
		[Required]
		public int? KESHIJSBZ { get{ return _KESHIJSBZ; } set{ _KESHIJSBZ = value ?? 0; } }
		private int? _BINGQUJSBZ;
		/// <summary>
		/// 是否接收病区消息
		/// </summary>
		[Required]
		public int? BINGQUJSBZ { get{ return _BINGQUJSBZ; } set{ _BINGQUJSBZ = value ?? 0; } }
		private int? _YUANQUJSBZ;
		/// <summary>
		/// 是否接收院区消息
		/// </summary>
		[Required]
		public int? YUANQUJSBZ { get{ return _YUANQUJSBZ; } set{ _YUANQUJSBZ = value ?? 0; } }
		private int? _GONGZUOZJSBZ;
		/// <summary>
		/// 是否接收工作站消息
		/// </summary>
		[Required]
		public int? GONGZUOZJSBZ { get{ return _GONGZUOZJSBZ; } set{ _GONGZUOZJSBZ = value ?? 0; } }
		private int? _YILIAOZJSBZ;
		/// <summary>
		/// 是否接收医疗组消息
		/// </summary>
		[Required]
		public int? YILIAOZJSBZ { get{ return _YILIAOZJSBZ; } set{ _YILIAOZJSBZ = value ?? 0; } }
		private int? _YAOFANGTSKFBZ;
		/// <summary>
		/// 药房特殊开放标志
		/// </summary>
		public int? YAOFANGTSKFBZ { get{ return _YAOFANGTSKFBZ; } set{ _YAOFANGTSKFBZ = value ?? 0; } }
		/// <summary>
		/// 门诊收费标志HR3-16353
		/// </summary>
		public int? MENZHENSFBZ { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(3)]
		public string YAOFANGSX { get; set; }
		/// <summary>
		/// 院区ID2
		/// </summary>
		[StringLength(1)]
		public string YUANQUID2 { get; set; }
		/// <summary>
		/// 对应原来的门诊病区静配05,13,33
		/// </summary>
		[StringLength(2)]
		public string XITONGID2 { get; set; }
        /// <summary>
        /// 应用对应的图片路径
        /// </summary>
        [StringLength(64)]
        public string TUPIANLJ { get; set; }
        private int? _LINCHUANGKJQYBZ;
        /// <summary>
        /// 临床框架启用标志HR6-2448(550805)
        /// </summary>
        public int? LINCHUANGKJQYBZ { get; set; }
        /// <summary>
        /// 应用英文简称：bqyz、bqys、zysf等HR6-2448(550805)
        /// </summary>
        [StringLength(50)]
        public string YINGWENJC { get; set; }
    }
}
