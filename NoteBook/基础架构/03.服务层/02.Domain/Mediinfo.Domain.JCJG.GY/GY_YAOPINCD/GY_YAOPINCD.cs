using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YAOPINCD")]
	public partial class GY_YAOPINCD : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 产地ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string CHANDIID { get; set; }
		/// <summary>
		/// 产地名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string CHANDIMC { get; set; }
		/// <summary>
		/// 省份
		/// </summary>
		[StringLength(10)]
		public string SHENGFEN { get; set; }
		/// <summary>
		/// 产地类别
		/// </summary>
		[Required]
		[StringLength(4)]
		public string CHANDILB { get; set; }
		/// <summary>
		/// 标准编码
		/// </summary>
		[StringLength(20)]
		public string BIAOZHUNBM { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		[Required]
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 药品类型
		/// </summary>
		[Required]
		[StringLength(4)]
		public string YAOPINLX { get; set; }
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
		private int? _WAIPEIBZ;
		/// <summary>
		/// 外配标志(0院内 1外配 2院外)
		/// </summary>
		public int? WAIPEIBZ { get{ return _WAIPEIBZ; } set{ _WAIPEIBZ = value ?? 0; } }
	}
}
