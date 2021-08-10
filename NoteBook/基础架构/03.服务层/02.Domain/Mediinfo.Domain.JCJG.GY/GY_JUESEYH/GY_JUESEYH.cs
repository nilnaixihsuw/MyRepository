using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JUESEYH")]
	public partial class GY_JUESEYH : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 角色ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JUESEID { get; set; }
		/// <summary>
		/// 用户ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string YONGHUID { get; set; }
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
