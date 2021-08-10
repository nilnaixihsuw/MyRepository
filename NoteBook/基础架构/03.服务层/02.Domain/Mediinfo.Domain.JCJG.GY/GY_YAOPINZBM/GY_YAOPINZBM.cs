using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YAOPINZBM")]
	public partial class GY_YAOPINZBM : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 别名ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string BIEMINGID { get; set; }
		/// <summary>
		/// 药品ID
		/// </summary>
		[StringLength(10)]
		public string YAOPINID { get; set; }
		/// <summary>
		/// 规格ID
		/// </summary>
		[StringLength(10)]
		public string GUIGEID { get; set; }
		/// <summary>
		/// 主名ID
		/// </summary>
		[StringLength(10)]
		public string ZHUMINGID { get; set; }
		/// <summary>
		/// 价格ID
		/// </summary>
		[StringLength(10)]
		public string JIAGEID { get; set; }
		/// <summary>
		/// 药品名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string YAOPINMC { get; set; }
		/// <summary>
		/// 输入码1
		/// </summary>
		[StringLength(10)]
		public string SHURUMA1 { get; set; }
		/// <summary>
		/// 输入码2
		/// </summary>
		[StringLength(10)]
		public string SHURUMA2 { get; set; }
		/// <summary>
		/// 输入码3
		/// </summary>
		[StringLength(10)]
		public string SHURUMA3 { get; set; }
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
		/// 作废标志
		/// </summary>
		[Required]
		public int? ZUOFEIBZ { get; set; }
		private int? _WAIPEIBZ;
		/// <summary>
		/// 外配标志(0院内 1外配 2院外)
		/// </summary>
		public int? WAIPEIBZ { get{ return _WAIPEIBZ; } set{ _WAIPEIBZ = value ?? 0; } }
	}
}
