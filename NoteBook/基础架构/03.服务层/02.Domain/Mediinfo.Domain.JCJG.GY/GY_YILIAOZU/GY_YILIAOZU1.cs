using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YILIAOZU1")]
	public partial class GY_YILIAOZU1 : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 医疗组ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string YILIAOZID { get; set; }
		/// <summary>
		/// 医疗组名
		/// </summary>
		[StringLength(20)]
		public string YILIAOZM { get; set; }
		/// <summary>
		/// 科室ID
		/// </summary>
		[StringLength(10)]
		public string KESHIID { get; set; }
		/// <summary>
		/// 病区ID
		/// </summary>
		[StringLength(10)]
		public string BINGQUID { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		[Required]
		public int? ZUOFEIBZ { get; set; }
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
		/// <summary>
		/// 医疗组类别
		/// </summary>
		[StringLength(4)]
		public string YILIAOZLB { get; set; }
		/// <summary>
		/// 科室病区标志
		/// </summary>
		[Required]
		public int? KESHIBQBZ { get; set; }
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
		/// 顺序号HR3-10665(147996)
		/// </summary>
		public int? SHUNXUHAO { get; set; }
	}
}
