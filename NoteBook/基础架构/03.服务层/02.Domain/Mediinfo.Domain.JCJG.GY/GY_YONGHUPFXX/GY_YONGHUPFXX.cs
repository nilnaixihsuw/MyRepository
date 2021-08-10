using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YONGHUPFXX")]
	public partial class GY_YONGHUPFXX : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 用户ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string YONGHUID { get; set; }
		/// <summary>
		/// 用户姓名
		/// </summary>
		[StringLength(20)]
		public string YONGHUXM { get; set; }
		/// <summary>
		/// 停用标志
		/// </summary>
		[Required]
		public int? TINGYONGBZ { get; set; }
		/// <summary>
		/// 皮肤名称
		/// </summary>
		[StringLength(100)]
		public string PIFUMC { get; set; }
		/// <summary>
		/// 字体名称
		/// </summary>
		[StringLength(100)]
		public string ZITIMC { get; set; }
		/// <summary>
		/// 字体大小
		/// </summary>
		public decimal? ZITIDX { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? XIUGAISJ { get; set; }
		/// <summary>
		/// 修改人
		/// </summary>
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
	}
}
