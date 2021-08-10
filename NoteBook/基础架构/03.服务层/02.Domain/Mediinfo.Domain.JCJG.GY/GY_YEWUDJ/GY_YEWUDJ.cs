using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YEWUDJ")]
	public partial class GY_YEWUDJ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 业务登记ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long? YEWUDJID { get; set; }
		/// <summary>
		/// 院区ID
		/// </summary>
		[StringLength(1)]
		public string YUANQUID { get; set; }
		/// <summary>
		/// 业务ID
		/// </summary>
		[StringLength(20)]
		public string YEWUID { get; set; }
		/// <summary>
		/// 业务类型
		/// </summary>
		[StringLength(20)]
		public string YEWULX { get; set; }
		/// <summary>
		/// 网卡地址
		/// </summary>
		[StringLength(20)]
		public string WANGKADZ { get; set; }
		/// <summary>
		/// 计算机名
		/// </summary>
		[StringLength(50)]
		public string JISUANJM { get; set; }
		/// <summary>
		/// 操作员
		/// </summary>
		[StringLength(10)]
		public string CAOZUOYUAN { get; set; }
		/// <summary>
		/// 操作日期
		/// </summary>
		public DateTime? CAOZUORQ { get; set; }
		/// <summary>
		/// 扩展信息
		/// </summary>
		[StringLength(500)]
		public string KUOZHANXX { get; set; }
	}
}
