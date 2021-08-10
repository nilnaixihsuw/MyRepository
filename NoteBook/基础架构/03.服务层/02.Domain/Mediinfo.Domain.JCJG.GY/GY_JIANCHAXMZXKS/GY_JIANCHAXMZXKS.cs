using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JIANCHAXMZXKS")]
	public partial class GY_JIANCHAXMZXKS : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 检查项目
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JIANCHAXM { get; set; }
		/// <summary>
		/// 科室ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string KESHIID { get; set; }
		/// <summary>
		/// 默认标志床位
		/// </summary>
		[Required]
		public int? MORENBZ { get; set; }
		/// <summary>
		/// 科室名称
		/// </summary>
		[StringLength(100)]
		public string KESHIMC { get; set; }
		/// <summary>
		/// 院区ID
		/// </summary>
		[StringLength(10)]
		public string YUANQUID { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{}
		}
	}
