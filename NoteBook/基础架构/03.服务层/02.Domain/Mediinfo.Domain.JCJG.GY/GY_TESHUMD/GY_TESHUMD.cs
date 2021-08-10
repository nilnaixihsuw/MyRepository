using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_TESHUMD")]
	public partial class GY_TESHUMD : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 特殊名单ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(20)]
		public string TESHUMDID { get; set; }
		/// <summary>
		/// 名单类别
		/// </summary>
		public int? MINGDANLB { get; set; }
		/// <summary>
		/// 病人ID
		/// </summary>
		[StringLength(100)]
		public string BINGRENID { get; set; }
		/// <summary>
		/// 冻结人
		/// </summary>
		[Required]
		[StringLength(20)]
		public string DONGJIEREN { get; set; }
		/// <summary>
		/// 冻结日期
		/// </summary>
		[Required]
		public DateTime? DONGJIERQ { get; set; }
		/// <summary>
		/// 当前状态
		/// </summary>
		[Required]
		public int? DANGQIANZT { get; set; }
		/// <summary>
		/// 解冻人
		/// </summary>
		[StringLength(20)]
		public string JIEDONGREN { get; set; }
		/// <summary>
		/// 解冻日期
		/// </summary>
		public DateTime? JIEDONGRQ { get; set; }
		/// <summary>
		/// 解冻理由
		/// </summary>
		[StringLength(200)]
		public string JIEDONGLY { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{}
		}
	}
