using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_ZIJINMXZ")]
	public partial class GY_ZIJINMXZ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 资金明细帐ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string ZIJINMXZID { get; set; }
		/// <summary>
		/// 病人ID
		/// </summary>
		[StringLength(100)]
		public string BINGRENID { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[Required]
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 院区ID
		/// </summary>
		[StringLength(1)]
		public string YUANQUID { get; set; }
		/// <summary>
		/// 业务类型
		/// </summary>
		[Required]
		[StringLength(4)]
		public string YEWULX { get; set; }
		/// <summary>
		/// 交易类型
		/// </summary>
		[Required]
		public int? JIAOYILX { get; set; }
		/// <summary>
		/// 交易ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string JIAOYIID { get; set; }
		/// <summary>
		/// 交易方式
		/// </summary>
		[StringLength(4)]
		public string JIAOYIFS { get; set; }
		/// <summary>
		/// 发生金额
		/// </summary>
		[Required]
		public decimal? FASHENGJE { get; set; }
		/// <summary>
		/// 本次金额
		/// </summary>
		[Required]
		public decimal? BENCIJE { get; set; }
		/// <summary>
		/// 结算ID
		/// </summary>
		[StringLength(10)]
		public string JIESUANID { get; set; }
		/// <summary>
		/// 结算日期
		/// </summary>
		public DateTime? JIESUANRQ { get; set; }
		/// <summary>
		/// 原资金明细帐ID
		/// </summary>
		[StringLength(10)]
		public string YUANZIJMXZID { get; set; }
		/// <summary>
		/// 支付ID
		/// </summary>
		[StringLength(10)]
		public string ZHIFUID { get; set; }
		/// <summary>
		/// 操作日期
		/// </summary>
		[Required]
		public DateTime? CAOZUORQ { get; set; }
		/// <summary>
		/// 操作员
		/// </summary>
		[Required]
		[StringLength(10)]
		public string CAOZUOYUAN { get; set; }
		/// <summary>
		/// 摘要
		/// </summary>
		[StringLength(100)]
		public string ZHAIYAO { get; set; }
		/// <summary>
		/// 结转ID
		/// </summary>
		[StringLength(10)]
		public string JIEZHUANID { get; set; }
		/// <summary>
		/// 结转日期
		/// </summary>
		public DateTime? JIEZHUANRQ { get; set; }
		/// <summary>
		/// 账户ID
		/// </summary>
		[StringLength(10)]
		public string ZHANGHUID { get; set; }
		private int? _CHONGXIAOBZ;
		/// <summary>
		/// 偿还日期
		/// </summary>
		public int? CHONGXIAOBZ { get{ return _CHONGXIAOBZ; } set{ _CHONGXIAOBZ = value ?? 0; } }
		/// <summary>
		/// 支付方式
		/// </summary>
		[StringLength(10)]
		public string ZHIFUFS { get; set; }
		/// <summary>
		/// 顺序号
		/// </summary>
		public int? SHUNXUHAO { get; set; }
		/// <summary>
		/// 使用金额
		/// </summary>
		public decimal? SHIYONGJE { get; set; }
		/// <summary>
		/// 工作站名称
		/// </summary>
		[StringLength(100)]
		public string GONGZUOZM { get; set; }
	}
}
