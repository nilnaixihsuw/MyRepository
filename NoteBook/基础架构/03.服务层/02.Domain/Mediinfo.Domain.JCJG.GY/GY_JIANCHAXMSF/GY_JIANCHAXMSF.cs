using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JIANCHAXMSF")]
	public partial class GY_JIANCHAXMSF : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 检查项目ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string JIANCHAXMID { get; set; }
		/// <summary>
		/// 项目ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string XIANGMUID { get; set; }
		/// <summary>
		/// 项目类型
		/// </summary>
		[Required]
		[StringLength(4)]
		public string XIANGMULX { get; set; }
		/// <summary>
		/// 数量
		/// </summary>
		[Required]
		public decimal? SHULIANG { get; set; }
		private decimal? _SHOUFEIBWSL;
		/// <summary>
		/// 收费部位数量
		/// </summary>
		public decimal? SHOUFEIBWSL { get; set; }
		private int? _JIAOPIANFBZ;
		/// <summary>
		/// 胶片费标志
		/// </summary>
		public int? JIAOPIANFBZ { get; set; }
		/// <summary>
		/// 适用诊疗医保代码
		/// </summary>
		[StringLength(10)]
		public string SHIYONGZLYBDM { get; set; }
		/// <summary>
		/// 适用诊疗项目名称
		/// </summary>
		[StringLength(100)]
		public string SHIYONGZLXMMC { get; set; }
		/// <summary>
		/// 收费对应代码
		/// </summary>
		[StringLength(50)]
		public string SHOUFEIDYDM { get; set; }
		/// <summary>
		/// 检查项目收费ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JIANCHAXMSFID { get; set; }
		/// <summary>
		/// 床边标志
		/// </summary>
		public int? CHUANGBIANBZ { get; set; }
		/// <summary>
		/// 胶片费对应部位HR3-9918
		/// </summary>
		[StringLength(10)]
		public string JIAOPIANFDYBW { get; set; }
		private int? _MENZHENSY;
		/// <summary>
		/// 门诊使用
		/// </summary>
		public int? MENZHENSY { get; set; }
		private int? _ZHUYUANSY;
		/// <summary>
		/// 住院使用
		/// </summary>
		public int? ZHUYUANSY { get; set; }
		private int? _TUWENBGFBZ;
		/// <summary>
		/// 图文报告费标志HR3-17216(206361)
		/// </summary>
		public int? TUWENBGFBZ { get; set; }
		private string _TESHUBZ;
		/// <summary>
		/// 特殊标志HR3-18712(215730)
		/// </summary>
		[StringLength(4)]
		public string TESHUBZ { get; set; }
		/// <summary>
		/// 收费对应部位HR3-19761(226149)
		/// </summary>
		[StringLength(10)]
		public string SHOUFEIDYBW { get; set; }
		/// <summary>
		/// 执行科室HR3-29397(297605)
		/// </summary>
		[StringLength(10)]
		public string ZHIXINGKS { get; set; }
		/// <summary>
		/// 优先标志HR3-30915(308413)
		/// </summary>
		public int? YOUXIANBZ { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{
			if (SHOUFEIBWSL.IsNullOrDBNull())
			SHOUFEIBWSL = 0;
			if (JIAOPIANFBZ.IsNullOrDBNull())
			JIAOPIANFBZ = 0;
			if (MENZHENSY.IsNullOrDBNull())
			MENZHENSY = 1;
			if (ZHUYUANSY.IsNullOrDBNull())
			ZHUYUANSY = 1;
			if (TUWENBGFBZ.IsNullOrDBNull())
			TUWENBGFBZ = 0;
			if (TESHUBZ.IsNullOrDBNull())
			TESHUBZ = "0";
		}
	}
}
