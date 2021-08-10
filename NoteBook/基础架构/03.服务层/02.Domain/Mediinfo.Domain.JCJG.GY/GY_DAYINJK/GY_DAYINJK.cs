using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_DAYINJK")]
	public partial class GY_DAYINJK : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 记录ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JILUID { get; set; }
		/// <summary>
		/// 记录来源1：门诊病历，2：病假单
		/// </summary>
		public int? JILULY { get; set; }
		/// <summary>
		/// 来源ID
		/// </summary>
		[StringLength(10)]
		public string LAIYUANID { get; set; }
		/// <summary>
		/// 病人ID
		/// </summary>
		[StringLength(100)]
		public string BINGRENID { get; set; }
		/// <summary>
		/// 就诊ID
		/// </summary>
		[StringLength(10)]
		public string JIUZHENID { get; set; }
		/// <summary>
		/// 打印内容
		/// </summary>
		[StringLength(30000000)]
		public string DAYINNR { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{}
		}
	}
