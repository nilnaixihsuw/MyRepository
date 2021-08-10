using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.ZY
{
	[Table("ZY_YINGERXX")]
	public partial class ZY_YINGERXX : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 婴儿ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string YINGERID { get; set; }
		/// <summary>
		/// 病人住院ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string BINGRENZYID { get; set; }
		/// <summary>
		/// 病人ID
		/// </summary>
		[StringLength(100)]
		public string BINGRENID { get; set; }
		/// <summary>
		/// 院区ID
		/// </summary>
		[Required]
		[StringLength(1)]
		public string YUANQUID { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[Required]
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 费用类别
		/// </summary>
		[Required]
		[StringLength(10)]
		public string FEIYONGLB { get; set; }
		/// <summary>
		/// 费用性质
		/// </summary>
		[Required]
		[StringLength(10)]
		public string FEIYONGXZ { get; set; }
		/// <summary>
		/// 姓名
		/// </summary>
		[Required]
		[StringLength(50)]
		public string XINGMING { get; set; }
		/// <summary>
		/// 性别
		/// </summary>
		[StringLength(4)]
		public string XINGBIE { get; set; }
		/// <summary>
		/// 出生日期
		/// </summary>
		[Required]
		public DateTime? CHUSHENGRQ { get; set; }
		/// <summary>
		/// 预产期
		/// </summary>
		public DateTime? YUCHANQI { get; set; }
		/// <summary>
		/// 孕周
		/// </summary>
		public int? YUNZHOU { get; set; }
		/// <summary>
		/// 胎次
		/// </summary>
		public int? TAICI { get; set; }
		/// <summary>
		/// 产次
		/// </summary>
		public int? CHANCI { get; set; }
		/// <summary>
		/// 身长
		/// </summary>
		public decimal? SHENCHANG { get; set; }
		/// <summary>
		/// 体重
		/// </summary>
		public decimal? TIZHONG { get; set; }
		/// <summary>
		/// 父亲姓名
		/// </summary>
		[StringLength(20)]
		public string FUQINXM { get; set; }
		/// <summary>
		/// 健康状况
		/// </summary>
		[StringLength(10)]
		public string JIANKANGZK { get; set; }
		/// <summary>
		/// 建档人
		/// </summary>
		[StringLength(10)]
		public string JIANDANGREN { get; set; }
		/// <summary>
		/// 建档日期
		/// </summary>
		public DateTime? JIANDANGRQ { get; set; }
		/// <summary>
		/// 登记人
		/// </summary>
		[StringLength(10)]
		public string DENGJIREN { get; set; }
		/// <summary>
		/// 登记日期
		/// </summary>
		public DateTime? DENGJIRQ { get; set; }
		/// <summary>
		/// 母亲住院ID
		/// </summary>
		[StringLength(10)]
		public string MUQINZYID { get; set; }
		/// <summary>
		/// 住院号
		/// </summary>
		[StringLength(10)]
		public string ZHUYUANHAO { get; set; }
		/// <summary>
		/// 婴儿入院住院ID(婴儿入院后的新纪录的住院ID)
		/// </summary>
		[StringLength(10)]
		public string YINGERRYZYID { get; set; }
		/// <summary>
		/// 孕周天数
		/// </summary>
		public int? TIANSHUANG { get; set; }
		/// <summary>
		/// 入院病区
		/// </summary>
		[StringLength(10)]
		public string RUYUANBQ { get; set; }
		/// <summary>
		/// 入院科室
		/// </summary>
		[StringLength(10)]
		public string RUYUANKS { get; set; }
		/// <summary>
		/// 入院病区名称
		/// </summary>
		[StringLength(100)]
		public string RUYUANBQMC { get; set; }
		/// <summary>
		/// 入院科室名称
		/// </summary>
		[StringLength(100)]
		public string RUYUANKSMC { get; set; }
		/// <summary>
		/// 优惠类别
		/// </summary>
		[StringLength(10)]
		public string YOUHUILB { get; set; }
		/// <summary>
		/// 分娩方式HR3-17330(207079)
		/// </summary>
		[StringLength(10)]
		public string FENMIANFS { get; set; }
		/// <summary>
		/// 接生医生 HR3-23959(262872) 
		/// </summary>
		[StringLength(10)]
		public string JIESHENGYS { get; set; }
		/// <summary>
		/// 接生护士 HR3-23959(262872) 
		/// </summary>
		[StringLength(10)]
		public string JIESHENGHS { get; set; }
		/// <summary>
		/// 胎盘处置方式 HR3-23959(262872) 
		/// </summary>
		[StringLength(100)]
		public string TAIPANCZFS { get; set; }
		/// <summary>
		/// 阿氏评分 HR3-23959(262872) 
		/// </summary>
		[StringLength(50)]
		public string ASHIPINGFEN { get; set; }
		/// <summary>
		/// 备注 HR3-23959(262872) 
		/// </summary>
		[StringLength(200)]
		public string BEIZHU { get; set; }
		/// <summary>
		/// 接生医生姓名 HR3-23959(262872) 
		/// </summary>
		[StringLength(50)]
		public string JIESHENGYSXM { get; set; }
		/// <summary>
		/// 接生护士姓名 HR3-23959(262872) 
		/// </summary>
		[StringLength(50)]
		public string JIESHENGHSXM { get; set; }
		/// <summary>
		/// 入院诊断代码
		/// </summary>
		[StringLength(10)]
		public string RUYUANZDDM { get; set; }
		/// <summary>
		/// 入院诊断名称
		/// </summary>
		[StringLength(100)]
		public string RUYUANZDMC { get; set; }
		/// <summary>
		/// 主治医师名称
		/// </summary>
		[StringLength(200)]
		public string ZHUZHIYSXM { get; set; }
		/// <summary>
		/// 主治医师
		/// </summary>
		[StringLength(200)]
		public string ZHUZHIYS { get; set; }
		/// <summary>
		/// 责任护士HR3-13246(171002)
		/// </summary>
		[StringLength(10)]
		public string ZERENHS { get; set; }
		/// <summary>
		/// 责任护士姓名HR3-13246(171002)
		/// </summary>
		[StringLength(100)]
		public string ZERENHSXM { get; set; }

	}
}
