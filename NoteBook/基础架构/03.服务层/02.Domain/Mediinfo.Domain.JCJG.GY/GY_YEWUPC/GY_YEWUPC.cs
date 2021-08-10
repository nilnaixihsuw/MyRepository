using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YEWUPC")]
	public partial class GY_YEWUPC : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 院区ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(1)]
		public string YUANQUID { get; set; }
		/// <summary>
		/// 业务类型
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(20)]
		public string YEWULX { get; set; }
		/// <summary>
		/// 排斥业务
		/// </summary>
		[Required]
		[StringLength(200)]
		public string PAICHIYW { get; set; }
	}
}
