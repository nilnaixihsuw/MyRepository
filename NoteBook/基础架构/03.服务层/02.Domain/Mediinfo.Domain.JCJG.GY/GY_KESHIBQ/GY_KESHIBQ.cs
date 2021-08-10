using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_KESHIBQ")]
	public partial class GY_KESHIBQ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 科室ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string KESHIID { get; set; }
		/// <summary>
		/// 病区ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string BINGQUID { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{}
		}
	}
