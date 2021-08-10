using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_DUOJICTLXXMFLDZ")]
	public partial class GY_DUOJICTLXXMFLDZ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 分类对照ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string FENLEIDZID { get; set; }
		/// <summary>
		/// 分类ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string FENLEIID { get; set; }
		/// <summary>
		/// 项目ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string XIANGMUID { get; set; }
		/// <summary>
		/// 项目类型
		/// </summary>
		[Required]
		[StringLength(10)]
		public string XIANGMULX { get; set; }
		/// <summary>
		/// 收费级次
		/// </summary>
		public int? SHOUFEIJC { get; set; }
		/// <summary>
		/// 开始天数
		/// </summary>
		public int? KAISHITS { get; set; }
		/// <summary>
		/// 结束天数
		/// </summary>
		public int? JIESHUTS { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		public int? ZUOFEIBZ { get; set; }
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
