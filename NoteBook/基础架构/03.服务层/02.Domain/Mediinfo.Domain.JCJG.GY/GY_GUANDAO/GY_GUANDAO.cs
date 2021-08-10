using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_GUANDAO")]
	public partial class GY_GUANDAO : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 管道ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string GUANDAOID { get; set; }
		/// <summary>
		/// 管道类型 1输液 2引流
		/// </summary>
		[StringLength(10)]
		public string GUANDAOLX { get; set; }
		/// <summary>
		/// 管道名称
		/// </summary>
		[StringLength(100)]
		public string GUANDAOMC { get; set; }
		/// <summary>
		/// 等级 1低危 2中危 3高危
		/// </summary>
		[StringLength(10)]
		public string DENGJI { get; set; }
		/// <summary>
		/// 顺序号
		/// </summary>
		public int? SHUNXUHAO { get; set; }
		/// <summary>
		/// 管道评估模板
		/// </summary>
		[StringLength(50)]
		public string PINGGUMB { get; set; }
		/// <summary>
		/// 修改人
		/// </summary>
		[Required]
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		[Required]
		public DateTime? XIUGAISJ { get; set; }
		private int? _ZUOFEIBZ;
		/// <summary>
		/// 作废标志
		/// </summary>
		public int? ZUOFEIBZ { get; set; }
		private int? _DUOGENBZ;
		/// <summary>
		/// 多根标志
		/// </summary>
		public int? DUOGENBZ { get; set; }
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
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{
			if (ZUOFEIBZ.IsNullOrDBNull())
			ZUOFEIBZ = 0;
			if (DUOGENBZ.IsNullOrDBNull())
			DUOGENBZ = 0;
		}
	}
}
