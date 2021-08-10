using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YILIAOZMS")]
	public partial class GY_YILIAOZMS : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 病人ID
		/// </summary>
		[Key]
        [Column(Order = 1)]
        [StringLength(100)]
		public string BINGRENID { get; set; }
        /// <summary>
        /// 就诊ID(住院病人住院id)
        /// </summary>
        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
		public string JIUZHENID { get; set; }
		/// <summary>
		/// 姓名
		/// </summary>
		[StringLength(50)]
		public string XINGMING { get; set; }
		/// <summary>
		/// 年龄单位
		/// </summary>
		[StringLength(10)]
		public string NIANLING { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(4)]
		public string NIANLINGDW { get; set; }
		/// <summary>
		/// 性别
		/// </summary>
		[StringLength(4)]
		public string XINGBIE { get; set; }
		/// <summary>
		/// 职业
		/// </summary>
		[StringLength(50)]
		public string ZHIYE { get; set; }
		/// <summary>
		/// 门诊号(住院号)
		/// </summary>
		[StringLength(50)]
		public string MENZHENHAO { get; set; }
		/// <summary>
		/// 地址
		/// </summary>
		[StringLength(100)]
		public string DIZHI { get; set; }
		/// <summary>
		/// 病情诊断处理意见
		/// </summary>
		[StringLength(1000)]
		public string BINGQINGZDCLYJ { get; set; }
		/// <summary>
		/// 开单科室
		/// </summary>
		[Required]
		[StringLength(10)]
		public string KAIDANKS { get; set; }
		/// <summary>
		/// 开单科室名称
		/// </summary>
		[StringLength(50)]
		public string KAIDANKSMC { get; set; }
		/// <summary>
		/// 开单医生
		/// </summary>
		[Required]
		[StringLength(10)]
		public string KAIDANYS { get; set; }
		/// <summary>
		/// 开单医生姓名
		/// </summary>
		[StringLength(50)]
		public string KAIDANYSXM { get; set; }
		/// <summary>
		/// 操作时间
		/// </summary>
		[Required]
		public DateTime? CAOZUOSJ { get; set; }
		/// <summary>
		/// 打印标志HR3-19187
		/// </summary>
		[StringLength(1)]
		public string DAYINBZ { get; set; }
		private int? _MENZHENZYBZ;
		/// <summary>
		/// 门诊住院标志HR3-26196(277221)，1门诊，2住院
		/// </summary>
		public int? MENZHENZYBZ { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(100)]
		public string BINGQINGZD { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{
			if (MENZHENZYBZ.IsNullOrDBNull())
			MENZHENZYBZ = 0;
		}
	}
}
