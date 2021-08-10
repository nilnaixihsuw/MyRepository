using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_QINGLINGDAN1")]
	public partial class GY_QINGLINGDAN1 : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 请领单ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string QINGLINGDID { get; set; }
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
		/// 被请领应用ID
		/// </summary>
		[Required]
		[StringLength(4)]
		public string BEIQINGLYYID { get; set; }
		/// <summary>
		/// 请领科室
		/// </summary>
		[Required]
		[StringLength(10)]
		public string QINGLINGKS { get; set; }
		/// <summary>
		/// 被请领科室
		/// </summary>
		[Required]
		[StringLength(10)]
		public string BEIQINGLKS { get; set; }
		/// <summary>
		/// 请领单号
		/// </summary>
		[StringLength(20)]
		public string QINGLINGDH { get; set; }
		/// <summary>
		/// 制单日期
		/// </summary>
		[Required]
		public DateTime? ZHIDANRQ { get; set; }
		/// <summary>
		/// 审核日期
		/// </summary>
		public DateTime? SHENHERQ { get; set; }
		/// <summary>
		/// 提交日期
		/// </summary>
		public DateTime? TIJIAORQ { get; set; }
		/// <summary>
		/// 受理日期
		/// </summary>
		public DateTime? SHOULIRQ { get; set; }
		/// <summary>
		/// 单据状态
		/// </summary>
		[Required]
		[StringLength(4)]
		public string DANJUZT { get; set; }
		/// <summary>
		/// 对应出库类型
		/// </summary>
		public int? DUIYINGCKLX { get; set; }
		/// <summary>
		/// 对应出库单ID
		/// </summary>
		[StringLength(10)]
		public string DUIYINGCKDID { get; set; }
		/// <summary>
		/// 制单人
		/// </summary>
		[Required]
		[StringLength(10)]
		public string ZHIDANREN { get; set; }
		/// <summary>
		/// 审核人
		/// </summary>
		[StringLength(10)]
		public string SHENHEREN { get; set; }
		/// <summary>
		/// 提交人
		/// </summary>
		[StringLength(10)]
		public string TIJIAOREN { get; set; }
		/// <summary>
		/// 受理人
		/// </summary>
		[StringLength(10)]
		public string SHOULIREN { get; set; }
		/// <summary>
		/// 修改人
		/// </summary>
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? XIUGAISJ { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		[StringLength(500)]
		public string BEIZHU { get; set; }
		/// <summary>
		/// 请领类型
		/// </summary>
		[StringLength(4)]
		public string QINGLINGLX { get; set; }
	}
}
