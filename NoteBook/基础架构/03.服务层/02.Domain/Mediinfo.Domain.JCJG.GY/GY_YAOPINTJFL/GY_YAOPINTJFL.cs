using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YAOPINTJFL")]
	public partial class GY_YAOPINTJFL : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 统计分类ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string TONGJIFLID { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 院区ID
		/// </summary>
		[StringLength(1)]
		public string YUANQUID { get; set; }
		/// <summary>
		/// 分类名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string FENLEIMC { get; set; }
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
