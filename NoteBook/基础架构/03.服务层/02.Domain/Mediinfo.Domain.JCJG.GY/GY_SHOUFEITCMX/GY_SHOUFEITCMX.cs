using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Mediinfo.Infrastructure.Core.Entity;

namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_SHOUFEITCMX")]
	public partial class GY_SHOUFEITCMX : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 收费套餐ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string SHOUFEITCID { get; set; }
		/// <summary>
		/// 项目ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string XIANGMUID { get; set; }
		/// <summary>
		/// 项目类型
		/// </summary>
		[StringLength(4)]
		public string XIANGMULX { get; set; }
		/// <summary>
		/// 项目名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string XIANGMUMC { get; set; }
		/// <summary>
		/// 产地名称
		/// </summary>
		[StringLength(100)]
		public string CHANDIMC { get; set; }
		/// <summary>
		/// 数量
		/// </summary>
		[Required]
		public decimal? SHULIANG { get; set; }
		/// <summary>
		/// 医疗组ID
		/// </summary>
		[StringLength(10)]
		public string YILIAOZID { get; set; }
	}
}
