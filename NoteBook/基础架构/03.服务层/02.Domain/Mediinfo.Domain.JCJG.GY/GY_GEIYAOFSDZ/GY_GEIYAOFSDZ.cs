using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_GEIYAOFSDZ")]
	public partial class GY_GEIYAOFSDZ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 给药方式ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string GEIYAOFSID { get; set; }
		/// <summary>
		/// 适用范围(1-医生，2-科室，3-应用，4-院区，5-全院)
		/// </summary>
		[Required]
		[StringLength(4)]
		public string SHIYONGFW { get; set; }
		/// <summary>
		/// 医生科室ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string YISHENGKS { get; set; }
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
		/// 给药方式对照ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string GEIYAOFSDZID { get; set; }
		/// <summary>
		/// 医生科室名称
		/// </summary>
		[StringLength(50)]
		public string YISHENGKSMC { get; set; }
	}
}
