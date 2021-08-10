using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_KUAIJIQJ")]
	public partial class GY_KUAIJIQJ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 应用ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 会计日历
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(6)]
		public string KUAIJIRL { get; set; }
		/// <summary>
		/// 开始日期
		/// </summary>
		[Required]
		public DateTime? KAISHIRQ { get; set; }
		/// <summary>
		/// 结束日期
		/// </summary>
		[Required]
		public DateTime? JIESHURQ { get; set; }
		/// <summary>
		/// 当前标志
		/// </summary>
		[Required]
		public int? DANGQIANBZ { get; set; }
		/// <summary>
		/// 结转日期
		/// </summary>
		public DateTime? JIEZHUANRQ { get; set; }
	}
}
