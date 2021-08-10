using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_SHOUFEIXMZXKS")]
	public partial class GY_SHOUFEIXMZXKS : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 收费项目
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string SHOUFEIXM { get; set; }
		/// <summary>
		/// 科室ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string KESHIID { get; set; }
		/// <summary>
		/// 默认标志床位
		/// </summary>
		[Required]
		public int? MORENBZ { get; set; }
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
		/// <summary>
		/// 科室名称
		/// </summary>
		[StringLength(100)]
		public string KESHIMC { get; set; }
		/// <summary>
		/// 院区IDHR3-12040
		/// </summary>
		[StringLength(10)]
		public string YUANQUID { get; set; }
	}
}
