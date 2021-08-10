using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JUESE")]
	public partial class GY_JUESE : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 角色ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JUESEID { get; set; }
		/// <summary>
		/// 角色名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string JUESEMC { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		[Required]
		public int? ZUOFEIBZ { get; set; }
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
