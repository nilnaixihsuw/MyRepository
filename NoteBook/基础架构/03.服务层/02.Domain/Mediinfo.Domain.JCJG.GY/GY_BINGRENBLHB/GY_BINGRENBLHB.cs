using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_BINGRENBLHB")]
	public partial class GY_BINGRENBLHB : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 病人ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(100)]
		public string BINGRENID { get; set; }
		/// <summary>
		/// 病历合并ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string BINGLIHBID { get; set; }
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
		/// 原病案号
		/// </summary>
		[StringLength(10)]
		public string YUANBINGAH { get; set; }
		private long? _ZHUBINGRBZ;
		/// <summary>
		/// 主病人标志
		/// </summary>
		public long? ZHUBINGRBZ { get{ return _ZHUBINGRBZ; } set{ _ZHUBINGRBZ = value ?? 0; } }
	}
}
