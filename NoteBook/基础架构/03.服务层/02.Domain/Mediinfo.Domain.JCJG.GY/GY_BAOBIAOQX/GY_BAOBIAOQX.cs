using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_BAOBIAOQX")]
	public partial class GY_BAOBIAOQX : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 报表ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(20)]
		public string BAOBIAOID { get; set; }
		/// <summary>
		/// 报表名称
		/// </summary>
		[StringLength(100)]
		public string BAOBIAOMC { get; set; }
		/// <summary>
		/// 报表中心使用标志
		/// </summary>
		public int? BAOBIAOBZ { get; set; }
		/// <summary>
		/// 输入码1
		/// </summary>
		[StringLength(50)]
		public string SHURUMA1 { get; set; }
		/// <summary>
		/// 输入码2
		/// </summary>
		[StringLength(50)]
		public string SHURUMA2 { get; set; }
		/// <summary>
		/// 输入码3
		/// </summary>
		[StringLength(50)]
		public string SHURUMA3 { get; set; }
		private int? _SHIYONGFW;
		/// <summary>
		/// 使用范围 1-所有应用, 2-指定应用
		/// </summary>
		public int? SHIYONGFW { get; set; }
		private int? _CHANGSHIBZ;
		/// <summary>
		/// 是否长时查询的报表
		/// </summary>
		public int? CHANGSHIBZ { get; set; }
		/// <summary>
		/// 报表中心使用分类
		/// </summary>
		[StringLength(10)]
		public string BAOBIAOZXSYLX { get; set; }
		/// <summary>
		/// 报表说明HR3-23183(257848)
		/// </summary>
		[StringLength(2000)]
		public string BAOBIAOSM { get; set; }
		/// <summary>
		/// 报表来源类型HR3-45498
		/// </summary>
		public int? BAOBIAOLYLX { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{
			if (SHIYONGFW.IsNullOrDBNull())
			SHIYONGFW = 1;
			if (CHANGSHIBZ.IsNullOrDBNull())
			CHANGSHIBZ = 0;
		}
	}
}
