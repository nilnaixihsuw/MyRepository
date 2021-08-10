using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JIANCHAMBDYKS")]
	public partial class GY_JIANCHAMBDYKS : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 科室ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string KESHIID { get; set; }
		/// <summary>
		/// 模板代码
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(50)]
		public string MOBANDM { get; set; }
		/// <summary>
		/// 记录人
		/// </summary>
		[StringLength(10)]
		public string JILUREN { get; set; }
		/// <summary>
		/// 记录时间
		/// </summary>
		public DateTime? JILUSJ { get; set; }
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
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{}
		}
	}
