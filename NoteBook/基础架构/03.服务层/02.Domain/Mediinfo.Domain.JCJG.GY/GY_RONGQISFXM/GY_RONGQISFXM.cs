using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_RONGQISFXM")]
	public partial class GY_RONGQISFXM : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 容器
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string RONGQI { get; set; }
		/// <summary>
		/// 项目类型
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(4)]
		public string XIANGMULX { get; set; }
		/// <summary>
		/// 项目ID
		/// </summary>
		[Key]
		[Column(Order=3)]
		[StringLength(10)]
		public string XIANGMUID { get; set; }
		/// <summary>
		/// 数量
		/// </summary>
		[Required]
		public decimal? SHULIANG { get; set; }
		private int? _YICISFBZ;
		/// <summary>
		/// 一次收费标志
		/// </summary>
		[Required]
		public int? YICISFBZ { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		private int? _MENZHENSY;
		/// <summary>
		/// 门诊使用HR3-16342(200628)
		/// </summary>
		public int? MENZHENSY { get; set; }
		private int? _ZHUYUANSY;
		/// <summary>
		/// 住院使用HR3-16342(200628)
		/// </summary>
		public int? ZHUYUANSY { get; set; }
		/// <summary>
		/// 执行科室HR3-16693(202928)
		/// </summary>
		[StringLength(10)]
		public string ZHIXINGKS { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{
			if (YICISFBZ.IsNullOrDBNull())
			YICISFBZ = 0 ;
			if (MENZHENSY.IsNullOrDBNull())
			MENZHENSY = 1;
			if (ZHUYUANSY.IsNullOrDBNull())
			ZHUYUANSY = 1;
		}
	}
}
