using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_BILIYH")]
	public partial class GY_BILIYH : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 优惠类别
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(4)]
		public string YOUHUILB { get; set; }
		/// <summary>
		/// 项目类型
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(4)]
		public string XIANGMULX { get; set; }
		/// <summary>
		/// 项目ID
		/// </summary>
		[Key]
		[Column(Order=3)]
		[StringLength(10)]
		public string XIANGMUID { get; set; }
		/// <summary>
		/// 优惠比例
		/// </summary>
		[Required]
		public decimal? YOUHUIBL { get; set; }
		/// <summary>
		/// 优惠金额HR3-13462(173204)
		/// </summary>
		public decimal? YOUHUIJE { get; set; }
	}
}
