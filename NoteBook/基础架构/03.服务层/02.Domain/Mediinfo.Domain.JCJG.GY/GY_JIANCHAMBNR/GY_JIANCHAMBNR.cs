using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JIANCHAMBNR")]
	public partial class GY_JIANCHAMBNR : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 检查模版内容ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JIANCHAMBNRID { get; set; }
		/// <summary>
		/// 检查项目ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string JIANCHAXMID { get; set; }
		/// <summary>
		/// 模版代码
		/// </summary>
		[StringLength(10)]
		public string MOBANDM { get; set; }
		/// <summary>
		/// 模版内容
		/// </summary>
		[StringLength(4000)]
		public string MOBANNR { get; set; }
		/// <summary>
		/// 输入人
		/// </summary>
		[StringLength(10)]
		public string SHURUREN { get; set; }
		/// <summary>
		/// 输入时间
		/// </summary>
		public DateTime? SHURUSJ { get; set; }
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
		/// 作废人
		/// </summary>
		[StringLength(10)]
		public string ZUOFEIREN { get; set; }
		/// <summary>
		/// 作废日期
		/// </summary>
		public DateTime? ZUOFEIRQ { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		[Required]
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{}
		}
	}
