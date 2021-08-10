using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JIANCHAXMSB")]
	public partial class GY_JIANCHAXMSB : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 检查项目设备ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JIANCHAXMSBID { get; set; }
		/// <summary>
		/// 检查项目ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string JIANCHAXMID { get; set; }
		/// <summary>
		/// 检查设备ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string JIANCHASBID { get; set; }
		/// <summary>
		/// 检查设备名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string JIANCHASBMC { get; set; }
		/// <summary>
		/// 优先级
		/// </summary>
		[Required]
		public int? YOUXIANJI { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{}
		}
	}
