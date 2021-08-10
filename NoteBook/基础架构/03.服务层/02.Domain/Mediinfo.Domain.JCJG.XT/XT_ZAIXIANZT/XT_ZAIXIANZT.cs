using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.XT
{
	[Table("XT_ZAIXIANZT")]
	public partial class XT_ZAIXIANZT : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 状态ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string ZHUANGTAIID { get; set; }
		/// <summary>
		/// 职工ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string ZHIGONGID { get; set; }
		/// <summary>
		/// 职工工号
		/// </summary>
		[Required]
		[StringLength(10)]
		public string ZHIGONGGH { get; set; }
		/// <summary>
		/// IP地址
		/// </summary>
		[StringLength(20)]
		public string IP { get; set; }
		/// <summary>
		/// MAC地址
		/// </summary>
		[StringLength(20)]
		public string MAC { get; set; }
		/// <summary>
		/// 开始时间
		/// </summary>
		[Required]
		public DateTime? KAISHISJ { get; set; }
		/// <summary>
		/// 结束时间
		/// </summary>
		[Required]
		public DateTime? JIESHUSJ { get; set; }
		/// <summary>
		/// 系统ID
		/// </summary>
		[StringLength(10)]
		public string XITONGID { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[StringLength(10)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 科室ID
		/// </summary>
		[StringLength(10)]
		public string KESHIID { get; set; }
		/// <summary>
		/// 病区ID
		/// </summary>
		[StringLength(10)]
		public string BINGQUID { get; set; }
		/// <summary>
		/// 医疗组ID
		/// </summary>
		[StringLength(10)]
		public string YILIAOZID { get; set; }
		/// <summary>
		/// 角色权限（以|分隔）
		/// </summary>
		[StringLength(500)]
		public string JUESEQX { get; set; }
		/// <summary>
		/// 院区ID
		/// </summary>
		[StringLength(10)]
		public string YUANQUID { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		public void SetDefaultValue () 
		{}
		}
	}
