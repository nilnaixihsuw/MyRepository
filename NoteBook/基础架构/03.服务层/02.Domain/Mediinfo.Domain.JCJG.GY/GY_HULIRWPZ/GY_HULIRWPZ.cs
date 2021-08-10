using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_HULIRWPZ")]
	public partial class GY_HULIRWPZ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 护理任务ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string HULIRWID { get; set; }
		/// <summary>
		/// 护理任务名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string HULIRWMC { get; set; }
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
		private int? _ZUOFEIBZ;
		/// <summary>
		/// 作废标志
		/// </summary>
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 病区ID
		/// </summary>
		[StringLength(10)]
		public string BINGQUID { get; set; }
		/// <summary>
		/// 顺序号
		/// </summary>
		public int? SHUNXUHAO { get; set; }
		/// <summary>
		/// 护理任务分类
		/// </summary>
		[Required]
		[StringLength(100)]
		public string HULIRWFL { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{
			if (ZUOFEIBZ.IsNullOrDBNull())
			ZUOFEIBZ = 0;
		}
	}
}
