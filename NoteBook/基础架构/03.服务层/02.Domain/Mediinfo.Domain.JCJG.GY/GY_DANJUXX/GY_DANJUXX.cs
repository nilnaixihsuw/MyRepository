using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_DANJUXX")]
	public partial class GY_DANJUXX : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 单据ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(20)]
		public string DANJUID { get; set; }
		/// <summary>
		/// 单据名称
		/// </summary>
		[StringLength(100)]
		public string DANJUMC { get; set; }
		/// <summary>
		/// 单据描述
		/// </summary>
		[StringLength(300)]
		public string DANJUMS { get; set; }
		/// <summary>
		/// 单据内容
		/// </summary>
		[StringLength(30000000)]
		public string DANJUNR { get; set; }
		/// <summary>
		/// 应用系统
		/// </summary>
		[StringLength(100)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 单据分类
		/// </summary>
		[StringLength(100)]
		public string DANJUFL { get; set; }
		/// <summary>
		/// 拼音码
		/// </summary>
		[StringLength(50)]
		public string SHURUMA1 { get; set; }
		/// <summary>
		/// 五笔码
		/// </summary>
		[StringLength(50)]
		public string SHURUMA2 { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(50)]
		public string SHURUMA3 { get; set; }
		private int? _SHIFOUYL;
		/// <summary>
		/// 是否预览
		/// </summary>
		public int? SHIFOUYL { get{ return _SHIFOUYL; } set{ _SHIFOUYL = value ?? 1; } }
		/// <summary>
		/// 修改人
		/// </summary>
		[StringLength(50)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? XIUGAISJ { get; set; }
		/// <summary>
		/// 单据状态 0、正常在用   -1 、作废
		/// </summary>
		public int? DANJUZT { get; set; }
	}
}
