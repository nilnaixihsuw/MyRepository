using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JIXINGDZ")]
	public partial class GY_JIXINGDZ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 剂型对照ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JIXINGDZID { get; set; }
		/// <summary>
		/// 剂型ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string JIXINGID { get; set; }
		/// <summary>
		/// 给药方式ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string GEIYAOFSID { get; set; }
		/// <summary>
		/// 修改人
		/// </summary>
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? XIUGAISJ { get; set; }
	}
}
