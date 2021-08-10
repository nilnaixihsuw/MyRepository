using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_HUIZHENDAN")]
	public partial class GY_HUIZHENDAN : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 会诊单ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string HUIZHENDID { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[Required]
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 院区ID
		/// </summary>
		[Required]
		[StringLength(1)]
		public string YUANQUID { get; set; }
		/// <summary>
		/// 病人住院ID
		/// </summary>
		[StringLength(10)]
		public string BINGRENZYID { get; set; }
		/// <summary>
		/// 医护标志(1.护士 2.医生 3.诊间)
		/// </summary>
		[Required]
		public int? YIHUBZ { get; set; }
		/// <summary>
		/// 会诊类别(备用)
		/// </summary>
		public int? HUIZHENLB { get; set; }
		/// <summary>
		/// 就诊ID
		/// </summary>
		[StringLength(10)]
		public string JIUZHENID { get; set; }
		/// <summary>
		/// 病人ID
		/// </summary>
		[Required]
		[StringLength(100)]
		public string BINGRENID { get; set; }
		/// <summary>
		/// 病人姓名
		/// </summary>
		[StringLength(50)]
		public string BINGRENXM { get; set; }
		/// <summary>
		/// 开单日期
		/// </summary>
		[Required]
		public DateTime? KAIDANRQ { get; set; }
		/// <summary>
		/// 开单医生
		/// </summary>
		[Required]
		[StringLength(10)]
		public string KAIDANYS { get; set; }
		/// <summary>
		/// 开单医生姓名
		/// </summary>
		[StringLength(20)]
		public string KAIDANYSXM { get; set; }
		/// <summary>
		/// 开单科室
		/// </summary>
		[Required]
		[StringLength(10)]
		public string KAIDANKS { get; set; }
		/// <summary>
		/// 开单科室名称
		/// </summary>
		[StringLength(100)]
		public string KAIDANKSMC { get; set; }
		/// <summary>
		/// 会诊医生
		/// </summary>
		[StringLength(10)]
		public string HUIZHENYS { get; set; }
		/// <summary>
		/// 会诊医生姓名
		/// </summary>
		[StringLength(20)]
		public string HUIZHENYSXM { get; set; }
		/// <summary>
		/// 会诊科室
		/// </summary>
		[StringLength(10)]
		public string HUIZHENKS { get; set; }
		/// <summary>
		/// 会诊科室名称
		/// </summary>
		[StringLength(100)]
		public string HUIZHENKSMC { get; set; }
		/// <summary>
		/// 邀请医生
		/// </summary>
		[StringLength(100)]
		public string YAOQINGYS { get; set; }
		/// <summary>
		/// 邀请医生姓名
		/// </summary>
		[StringLength(200)]
		public string YAOQINGYSXM { get; set; }
		/// <summary>
		/// 会诊意见
		/// </summary>
		[StringLength(2000)]
		public string HUIZHENYJ { get; set; }
		private int? _HUIZHENZT;
		/// <summary>
		/// 会诊状态(0.新开单 1.审核通过2.审核不通过3.接收 4.完成5.打印6.关闭7.评价 9.作废 10.拒绝)
		/// </summary>
		public int? HUIZHENZT { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? XIUGAISJ { get; set; }
		/// <summary>
		/// 修改人
		/// </summary>
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 会诊日期
		/// </summary>
		public DateTime? HUIZHENRQ { get; set; }
		/// <summary>
		/// 会诊目的
		/// </summary>
		[StringLength(2000)]
		public string HUIZHENMD { get; set; }
		private int? _YUANWAIHZBZ;
		/// <summary>
		/// 院外会诊标志HR3-26367
		/// </summary>
		public int? YUANWAIHZBZ { get; set; }
		/// <summary>
		/// 医疗结构名称
		/// </summary>
		[StringLength(100)]
		public string YILIAOJGMC { get; set; }
		/// <summary>
		/// 模板类型
		/// </summary>
		[StringLength(20)]
		public string MOBANLX { get; set; }
		/// <summary>
		/// 病历记录序号
		/// </summary>
		public long? BINGLIJLXH { get; set; }
		/// <summary>
		/// 会诊类别IDHR3-27532(285892)
		/// </summary>
		[StringLength(10)]
		public string HUIZHENLBID { get; set; }
		/// <summary>
		/// 住院号
		/// </summary>
		[StringLength(10)]
		public string ZHUYUANHAO { get; set; }
		/// <summary>
		/// 病案号
		/// </summary>
		[StringLength(10)]
		public string BINGANHAO { get; set; }
		/// <summary>
		/// 性别
		/// </summary>
		[StringLength(4)]
		public string XINGBIE { get; set; }
		/// <summary>
		/// 病人病区
		/// </summary>
		[StringLength(10)]
		public string BINGRENBQ { get; set; }
		/// <summary>
		/// 病人科室
		/// </summary>
		[StringLength(10)]
		public string BINGRENKS { get; set; }
		/// <summary>
		/// 床号
		/// </summary>
		[StringLength(10)]
		public string BINGRENCW { get; set; }
		/// <summary>
		/// 门诊住院标志(0.门诊 1.住院)
		/// </summary>
		[Required]
		public int? MENZHENZYBZ { get; set; }
		/// <summary>
		/// 医疗组id
		/// </summary>
		[StringLength(10)]
		public string YILIAOZID { get; set; }
		/// <summary>
		/// 邀请科室名称
		/// </summary>
		[StringLength(500)]
		public string YAOQINGKSMC { get; set; }
		/// <summary>
		/// 邀请科室
		/// </summary>
		[StringLength(100)]
		public string YAOQINGKS { get; set; }
		/// <summary>
		/// 病情摘要 主诉+现病史+体格检查+辅助检查
		/// </summary>
		[StringLength(4000)]
		public string BINGQINGZY { get; set; }
		/// <summary>
		/// 临床诊断
		/// </summary>
		[StringLength(200)]
		public string LINCHUANGZD { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		[StringLength(500)]
		public string BEIZHU { get; set; }
		/// <summary>
		/// 接收人
		/// </summary>
		[StringLength(10)]
		public string JIESHOUREN { get; set; }
		/// <summary>
		/// 接收时间
		/// </summary>
		public DateTime? JIESHOUSJ { get; set; }
		/// <summary>
		/// 打印人
		/// </summary>
		[StringLength(10)]
		public string DAYINREN { get; set; }
		/// <summary>
		/// 打印时间
		/// </summary>
		public DateTime? DAYINSJ { get; set; }
		/// <summary>
		/// 医嘱ID
		/// </summary>
		[StringLength(10)]
		public string YIZHUID { get; set; }
		private int? _JIZHENBZ;
		/// <summary>
		/// 急诊标志0：普通1：急诊
		/// </summary>
		public int? JIZHENBZ { get; set; }
		/// <summary>
		/// 专家会诊标志HR3-26367  1专家
		/// </summary>
		public int? ZHUANJIAHZBZ { get; set; }
		/// <summary>
		/// 要求会诊时间
		/// </summary>
		public DateTime? YAOQIUHZSJ { get; set; }
		/// <summary>
		/// 审核人
		/// </summary>
		[StringLength(10)]
		public string SHENHEREN { get; set; }
		/// <summary>
		/// 审核时间
		/// </summary>
		public DateTime? SHENHESJ { get; set; }
		private int? _MDTHZBZ;
		/// <summary>
		/// MDT会诊标志HR3-31154 
		/// </summary>
		public int? MDTHZBZ { get; set; }
		private int? _MDTHZSPBZ;
		/// <summary>
		/// MDT会诊审批标志
		/// </summary>
		public int? MDTHZSPBZ { get; set; }
		/// <summary>
		/// 拒绝原因HR6-426(475591)
		/// </summary>
		[StringLength(200)]
		public string JUJUEYY { get; set; }
		/// <summary>
		/// 审核不通过原因HR6-426(475591)
		/// </summary>
		[StringLength(200)]
		public string SHENHEBTGYY { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{
			if (HUIZHENZT.IsNullOrDBNull())
			HUIZHENZT = 0;
			if (YUANWAIHZBZ.IsNullOrDBNull())
			YUANWAIHZBZ = 0;
			if (JIZHENBZ.IsNullOrDBNull())
			JIZHENBZ = 0;
			if (MDTHZBZ.IsNullOrDBNull())
			MDTHZBZ = 0;
			if (MDTHZSPBZ.IsNullOrDBNull())
			MDTHZSPBZ = 0;
		}
	}
}
