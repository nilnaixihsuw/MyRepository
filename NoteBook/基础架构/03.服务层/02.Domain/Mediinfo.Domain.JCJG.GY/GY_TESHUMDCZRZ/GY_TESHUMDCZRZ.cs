using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_TESHUMDCZRZ")]
	public partial class GY_TESHUMDCZRZ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 特殊名单日志ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(20)]
		public string TESHUMDRZID { get; set; }
		/// <summary>
		/// 特殊名单ID
		/// </summary>
		[Required]
		[StringLength(20)]
		public string TESHUMDID { get; set; }
		/// <summary>
		/// 操作类型
		/// </summary>
		[Required]
		public int? CAOZUOLX { get; set; }
		/// <summary>
		/// 操作人
		/// </summary>
		[Required]
		[StringLength(20)]
		public string CAOZUOREN { get; set; }
		/// <summary>
		/// 操作日期
		/// </summary>
		[Required]
		public DateTime? CAOZUORQ { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		[StringLength(200)]
		public string BEIZHU { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{}
		}
	}
