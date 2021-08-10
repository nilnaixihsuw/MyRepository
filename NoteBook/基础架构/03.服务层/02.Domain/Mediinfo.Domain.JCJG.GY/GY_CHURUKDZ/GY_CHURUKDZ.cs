using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_CHURUKDZ")]
	public partial class GY_CHURUKDZ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 应用ID1
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(4)]
		public string YINGYONGID1 { get; set; }
		/// <summary>
		/// 入库方式ID1
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string RUKUFSID1 { get; set; }
		/// <summary>
		/// 应用ID2
		/// </summary>
		[Key]
		[Column(Order=3)]
		[StringLength(4)]
		public string YINGYONGID2 { get; set; }
		/// <summary>
		/// 出库方式ID2
		/// </summary>
		[Key]
		[Column(Order=4)]
		[StringLength(10)]
		public string CHUKUFSID2 { get; set; }
	}
}
