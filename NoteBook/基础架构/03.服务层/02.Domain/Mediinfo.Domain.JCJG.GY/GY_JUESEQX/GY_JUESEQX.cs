using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JUESEQX")]
	public partial class GY_JUESEQX : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 角色ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JUESEID { get; set; }
		/// <summary>
		/// 权限ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(200)]
		public string QUANXIANID { get; set; }
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
	}
}
