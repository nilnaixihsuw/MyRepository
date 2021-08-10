using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_FANGJIAN")]
	public partial class GY_FANGJIAN : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 房间ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string FANGJIANID { get; set; }
		/// <summary>
		/// 病人ID
		/// </summary>
		[StringLength(100)]
		public string BINGRENID { get; set; }
		/// <summary>
		/// 房间名称
		/// </summary>
		[StringLength(100)]
		public string FANGJIANMC { get; set; }
		/// <summary>
		/// 性别限制
		/// </summary>
		[Required]
		public int? XINGBIEXZ { get; set; }
		/// <summary>
		/// 当前性别属性
		/// </summary>
		[StringLength(4)]
		public string DANGQIANXBSX { get; set; }
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
		/// 病区ID
		/// </summary>
		[StringLength(10)]
		public string BINGQUID { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		public int? ZUOFEIBZ { get; set; }
	}
}
