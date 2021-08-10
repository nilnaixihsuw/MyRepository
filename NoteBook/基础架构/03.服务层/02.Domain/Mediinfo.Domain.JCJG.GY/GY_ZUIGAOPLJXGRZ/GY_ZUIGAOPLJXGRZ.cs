using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_ZUIGAOPLJXGRZ")]
	public partial class GY_ZUIGAOPLJXGRZ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 应用ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 价格ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string JIAGEID { get; set; }
		/// <summary>
		/// 调价文号
		/// </summary>
		[StringLength(50)]
		public string TIAOJIAWH { get; set; }
		/// <summary>
		/// 原最高批发价
		/// </summary>
		public decimal? YUANZUIGPFJ { get; set; }
		/// <summary>
		/// 原最高零售价
		/// </summary>
		public decimal? YUANZUIGLSJ { get; set; }
		/// <summary>
		/// 现最高批发价
		/// </summary>
		public decimal? XIANZUIGPFJ { get; set; }
		/// <summary>
		/// 现最高零售价
		/// </summary>
		public decimal? XIANZUIGLSJ { get; set; }
		/// <summary>
		/// 修改人
		/// </summary>
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		[Key]
		[Column(Order=3)]
		public DateTime? XIUGAISJ { get; set; }
	}
}
