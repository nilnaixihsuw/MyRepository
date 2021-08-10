using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_LVSETDYPSZ")]
	public partial class GY_LVSETDYPSZ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 价格ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JIAGEID { get; set; }
		/// <summary>
		/// 规格ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string GUIGEID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[Required]
		[StringLength(10)]
		public string CHANDI { get; set; }
		/// <summary>
		/// 担保类型
		/// </summary>
		[Required]
		[StringLength(100)]
		public string DANBAOLX { get; set; }
		/// <summary>
		/// 担保类型名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string DANBAOLXMC { get; set; }
		/// <summary>
		/// 顺序号
		/// </summary>
		[Required]
		public int? SHUNXUHAO { get; set; }
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
