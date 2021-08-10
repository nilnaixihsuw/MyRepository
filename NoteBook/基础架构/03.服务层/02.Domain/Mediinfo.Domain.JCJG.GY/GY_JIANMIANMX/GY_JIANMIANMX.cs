using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JIANMIANMX")]
	public partial class GY_JIANMIANMX : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 减免明细ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JIANMIANMXID { get; set; }
		/// <summary>
		/// 结算ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string JIESUANID { get; set; }
		/// <summary>
		/// 收费ID
		/// </summary>
		[StringLength(10)]
		public string SHOUFEIID { get; set; }
		/// <summary>
		/// 门诊住院标志
		/// </summary>
		[Required]
		public int? MENZHENZYBZ { get; set; }
		/// <summary>
		/// 病人住院ID
		/// </summary>
		[StringLength(10)]
		public string BINGRENZYID { get; set; }
		/// <summary>
		/// 病人ID
		/// </summary>
		[Required]
		[StringLength(100)]
		public string BINGRENID { get; set; }
		/// <summary>
		/// 婴儿ID
		/// </summary>
		[StringLength(10)]
		public string YINGERID { get; set; }
		/// <summary>
		/// 项目ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string XIANGMUID { get; set; }
		/// <summary>
		/// 项目类型
		/// </summary>
		[Required]
		[StringLength(4)]
		public string XIANGMULX { get; set; }
		/// <summary>
		/// 总金额
		/// </summary>
		[Required]
		public decimal? ZONGJINE { get; set; }
		/// <summary>
		/// 自理金额
		/// </summary>
		[Required]
		public decimal? ZILIJE { get; set; }
		/// <summary>
		/// 自费金额
		/// </summary>
		[Required]
		public decimal? ZIFEIJE { get; set; }
		/// <summary>
		/// 统筹金额
		/// </summary>
		[Required]
		public decimal? TONGCHOUJE { get; set; }
		/// <summary>
		/// 减免比例
		/// </summary>
		[Required]
		public decimal? JIANMIANBL { get; set; }
		/// <summary>
		/// 自理自费减免
		/// </summary>
		[Required]
		public decimal? ZILIZFJM { get; set; }
		/// <summary>
		/// 统筹减免
		/// </summary>
		[Required]
		public decimal? TONGCHOUJM { get; set; }
		/// <summary>
		/// 核算项目
		/// </summary>
		[StringLength(10)]
		public string HESUANXM { get; set; }
		/// <summary>
		/// 费用明细ID
		/// </summary>
		[StringLength(10)]
		public string FEIYONGMXID { get; set; }
		/// <summary>
		/// 费用来源
		/// </summary>
		[StringLength(4)]
		public string FEIYONGLY { get; set; }
		/// <summary>
		/// 应急收费标志
		/// </summary>
		public long? YINGJISFBZ { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 优惠价格HR3-10139
		/// </summary>
		public decimal? YOUHUIJG { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		public void SetDefaultValue () 
		{}
		}
	}
