using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YAOPINJGDZ")]
	public partial class GY_YAOPINJGDZ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 大规格价格ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JIAGEID1 { get; set; }
		/// <summary>
		/// 大规格价格
		/// </summary>
		[Key]
		[Column(Order=2)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public decimal? JIAGE1 { get; set; }
		/// <summary>
		/// 小规格或者中间规格价格ID
		/// </summary>
		[Key]
		[Column(Order=3)]
		[StringLength(10)]
		public string JIAGEID2 { get; set; }
		/// <summary>
		/// 小规格或者中间规格价格
		/// </summary>
		[Key]
		[Column(Order=4)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public decimal? JIAGE2 { get; set; }
		/// <summary>
		/// 价格类型(1进价2批发3零售)
		/// </summary>
		[Key]
		[Column(Order=5)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int? JIAGELX { get; set; }
	}
}
