using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JIBINGBK")]
	public partial class GY_JIBINGBK : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 就诊ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string JIUZHENID { get; set; }
		/// <summary>
		/// 疾病ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string JIBINGID { get; set; }
		/// <summary>
		/// 报卡标志
		/// </summary>
		[Required]
		public int? BAOKABZ { get; set; }
		/// <summary>
		/// 报卡卡号
		/// </summary>
		[StringLength(50)]
		public string BAOKAKH { get; set; }
		/// <summary>
		/// 病人ID
		/// </summary>
		[Required]
		[StringLength(100)]
		public string BINGRENID { get; set; }
		/// <summary>
		/// 报卡次数
		/// </summary>
		public int? BAOKACS { get; set; }
		/// <summary>
		/// 报卡类型
		/// </summary>
		[StringLength(500)]
		public string BAOKALX { get; set; }
		/// <summary>
		/// 疾病名称
		/// </summary>
		[StringLength(100)]
		public string JIBINGMC { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[Required]
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 院区ID
		/// </summary>
		[Required]
		[StringLength(1)]
		public string YUANQUID { get; set; }
		/// <summary>
		/// 报卡ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string BAOKAID { get; set; }
		/// <summary>
		/// 备注HR3-16407
		/// </summary>
		[StringLength(500)]
		public string BEIZHU { get; set; }
		/// <summary>
		/// 系统时间HR3-22929
		/// </summary>
		public DateTime? XITONGSJ { get; set; }
		/// <summary>
		/// 门诊住院标志0门诊1住院
		/// </summary>
		[StringLength(1)]
		public string MENZHENZYBZ { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{}
		}
	}
