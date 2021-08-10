using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YINGYONGMZLB")]
	public partial class GY_YINGYONGMZLB : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 应用门诊类别ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string YINGYONGMZLBID { get; set; }
		/// <summary>
		/// 门诊类别
		/// </summary>
		[Required]
		[StringLength(10)]
		public string MENZHENLB { get; set; }
		/// <summary>
		/// 上午挂号费项目
		/// </summary>
		[StringLength(10)]
		public string SHANGWUGHFXM { get; set; }
		/// <summary>
		/// 上午诊疗费项目
		/// </summary>
		[StringLength(10)]
		public string SHANGWUZLFXM { get; set; }
		/// <summary>
		/// 下午挂号费项目
		/// </summary>
		[StringLength(10)]
		public string XIAWUGHFXM { get; set; }
		/// <summary>
		/// 下午诊疗费项目
		/// </summary>
		[StringLength(10)]
		public string XIAWUZLFXM { get; set; }
		/// <summary>
		/// 晚上挂号费项目
		/// </summary>
		[StringLength(10)]
		public string WANSHANGGHFXM { get; set; }
		/// <summary>
		/// 晚上诊疗费项目
		/// </summary>
		[StringLength(10)]
		public string WANSHANGZLFXM { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string YINGYONGID { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{}
		}
	}
