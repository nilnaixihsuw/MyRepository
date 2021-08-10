using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YILIAOZU3")]
	public partial class GY_YILIAOZU3 : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 医疗组ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string YILIAOZID { get; set; }
		/// <summary>
		/// 职工ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string ZHIGONGID { get; set; }
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
		/// 组内角色
		/// </summary>
		[StringLength(4)]
		public string ZUNEIJS { get; set; }
		/// <summary>
		/// 开始时间
		/// </summary>
		[Required]
		public DateTime? KAISHISJ { get; set; }
		/// <summary>
		/// 结束时间
		/// </summary>
		[Required]
		public DateTime? JIESHUSJ { get; set; }
		/// <summary>
		/// 记录ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JILUID { get; set; }
	}
}
