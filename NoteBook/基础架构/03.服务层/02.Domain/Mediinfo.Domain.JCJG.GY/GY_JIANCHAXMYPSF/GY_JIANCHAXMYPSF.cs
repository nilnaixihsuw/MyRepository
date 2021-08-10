using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JIANCHAXMYPSF")]
	public partial class GY_JIANCHAXMYPSF : EntityBase, IEntityMapper
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
		public string JIAGEID { get; set; }
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
		private int? _ZIBEIBZ;
		/// <summary>
		/// 自备标志
		/// </summary>
		public int? ZIBEIBZ { get; set; }
		/// <summary>
		/// 收费对应代码
		/// </summary>
		[StringLength(50)]
		public string SHOUFEIDYDM { get; set; }
		/// <summary>
		/// 检查项目药品收费ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JIANCHAXMYPSFID { get; set; }
		/// <summary>
		/// 床边标志
		/// </summary>
		public int? CHUANGBIANBZ { get; set; }
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
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{
			if (ZIBEIBZ.IsNullOrDBNull())
			ZIBEIBZ = 0;
			if (MENZHENSY.IsNullOrDBNull())
			MENZHENSY = 1;
			if (ZHUYUANSY.IsNullOrDBNull())
			ZHUYUANSY = 1;
			if (TESHUBZ.IsNullOrDBNull())
			TESHUBZ = "0";
		}
	}
}
