using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YAOPINZKZYSZ")]
	public partial class GY_YAOPINZKZYSZ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 价格ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string JIAGEID { get; set; }
		/// <summary>
		/// 科室ID
		/// </summary>
		[StringLength(10)]
		public string KESHIID { get; set; }
		/// <summary>
		/// 职工ID
		/// </summary>
		[StringLength(10)]
		public string ZHIGONGID { get; set; }
		/// <summary>
		/// 门诊住院标志
		/// </summary>
		public int? MENZHENZYBZ { get; set; }
		/// <summary>
		/// 修改人
		/// </summary>
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? XIUGAISJ { get; set; }
		/// <summary>
		/// 药品专科专用ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string YAOPINZKZYID { get; set; }
	}
}
