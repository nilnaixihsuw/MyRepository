using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YAOPINTJFLDZ")]
	public partial class GY_YAOPINTJFLDZ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 应用ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 统计分类
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string TONGJIFL { get; set; }
		/// <summary>
		/// 规格ID
		/// </summary>
		[Key]
		[Column(Order=3)]
		[StringLength(10)]
		public string GUIGEID { get; set; }
	}
}
