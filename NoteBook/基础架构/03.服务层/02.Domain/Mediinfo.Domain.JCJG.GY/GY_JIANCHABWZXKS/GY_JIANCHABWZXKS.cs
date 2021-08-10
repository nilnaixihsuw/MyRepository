using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JIANCHABWZXKS")]
	public partial class GY_JIANCHABWZXKS : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 检查部位
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JIANCHABW { get; set; }
		/// <summary>
		/// 科室ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string KESHIID { get; set; }
		/// <summary>
		/// 默认标志
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
