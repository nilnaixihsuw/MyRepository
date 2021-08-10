using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YILIAOZM")]
	public partial class GY_YILIAOZM : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 医疗证明ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string YILIAOZMID { get; set; }
		/// <summary>
		/// 就诊ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string JIUZHENID { get; set; }
		/// <summary>
		/// 姓名
		/// </summary>
		[StringLength(50)]
		public string XINGMING { get; set; }
		/// <summary>
		/// 性别
		/// </summary>
		[StringLength(4)]
		public string XINGBIE { get; set; }
		/// <summary>
		/// 年龄
		/// </summary>
		[StringLength(4)]
		public string NIANLING { get; set; }
		/// <summary>
		/// 就诊日期
		/// </summary>
		public DateTime? JIUZHENRQ { get; set; }
		/// <summary>
		/// 就诊科室
		/// </summary>
		[StringLength(100)]
		public string JIUZHENKS { get; set; }
		/// <summary>
		/// 诊断
		/// </summary>
		[StringLength(100)]
		public string ZHENDUAN { get; set; }
        /// <summary>
        /// 诊断1
        /// </summary>
        [StringLength(100)]
        public string ZHENDUAN1 { get; set; }
        /// <summary>
        /// 诊断2
        /// </summary>
        [StringLength(100)]
        public string ZHENDUAN2 { get; set; }
        /// <summary>
        /// 处理意见
        /// </summary>
        [StringLength(500)]
		public string CHULIYJ { get; set; }
		/// <summary>
		/// 就诊医生
		/// </summary>
		[StringLength(50)]
		public string JIUZHENYS { get; set; }
		/// <summary>
		/// 打印日期
		/// </summary>
		public DateTime? RIQI { get; set; }
		/// <summary>
		/// 打印人
		/// </summary>
		[StringLength(100)]
		public string DAYINREN { get; set; }
		/// <summary>
		/// 证明类型
		/// </summary>
		[StringLength(1)]
		public string ZHENGMINGLX { get; set; }
		/// <summary>
		/// 工作单位
		/// </summary>
		[StringLength(50)]
		public string GONGZUODW { get; set; }
		/// <summary>
		/// 家庭地址
		/// </summary>
		[StringLength(200)]
		public string JIATINGDZ { get; set; }
		/// <summary>
		/// 审核状态
		/// </summary>
		[StringLength(1)]
		public string SHENHEZT { get; set; }
		/// <summary>
		/// 审核结果说明
		/// </summary>
		[StringLength(50)]
		public string SHENHEJGSM { get; set; }
		/// <summary>
		/// 诊断意见
		/// </summary>
		[StringLength(500)]
		public string ZHENDUANYJ { get; set; }
		/// <summary>
		/// 住院号
		/// </summary>
		[StringLength(10)]
		public string ZHUYUANHAO { get; set; }
		/// <summary>
		/// 入院日期
		/// </summary>
		public DateTime? RUYUANRQ { get; set; }
		/// <summary>
		/// 出院日期
		/// </summary>
		public DateTime? CHUYUANRQ { get; set; }
		/// <summary>
		/// 门诊住院标志0门诊1住院
		/// </summary>
		public int? MENZHENZYBZ { get; set; }
		/// <summary>
		/// 就诊卡号
		/// </summary>
		[StringLength(50)]
		public string JIUZHENKH { get; set; }
		/// <summary>
		/// 审核人
		/// </summary>
		[StringLength(20)]
		public string SHENHEREN { get; set; }
		/// <summary>
		/// 审核日期
		/// </summary>
		public DateTime? SHENHERQ { get; set; }
		/// <summary>
		/// 病人ID
		/// </summary>
		[StringLength(100)]
		public string BINGRENID { get; set; }
		/// <summary>
		/// 身份证号
		/// </summary>
		[StringLength(20)]
		public string SHENFENZH { get; set; }
		/// <summary>
		/// 职业
		/// </summary>
		[StringLength(100)]
		public string ZHIYE { get; set; }
		/// <summary>
		/// 电话-35161(333905)
		/// </summary>
		[StringLength(20)]
		public string DIANHUA { get; set; }
		/// <summary>
		/// 打印标志
		/// </summary>
		[StringLength(2)]
		public string DAYINBZ { get; set; }
		/// <summary>
		/// 打印日期
		/// </summary>
		public DateTime? DAYINRQ { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		public void SetDefaultValue () 
		{}
		}
	}
