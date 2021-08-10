using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_XUNIYFDY")]
	public partial class GY_XUNIYFDY : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 虚拟药房应用ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string YINGYONGID1 { get; set; }
		/// <summary>
		/// 药房应用ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string YINGYONGID2 { get; set; }
	}
}
