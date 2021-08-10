using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YONGHUXX")]
	public partial class GY_YONGHUXX : EntityBase, IEntityMapper
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
		/// 密码
		/// </summary>
		[StringLength(100)]
		public string MIMA { get; set; }
		/// <summary>
		/// 输入码
		/// </summary>
		[StringLength(10)]
		public string SHURUMA { get; set; }
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
		/// 外部用户ID
		/// </summary>
		[StringLength(50)]
		public string WAIBUYHID { get; set; }
		/// <summary>
		/// 外部用户域
		/// </summary>
		[StringLength(200)]
		public string WAIBUYHY { get; set; }
		/// <summary>
		/// 密码修改时间
		/// </summary>
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime? MIMAXGSJ { get; set; }
	}
}
