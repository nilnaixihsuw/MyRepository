using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_XIANGMUSP")]
	public partial class GY_XIANGMUSP : EntityBase, IEntityMapper
	{
        /// <summary>
        /// 审批ID
        /// </summary>
        [Key]
        [Column(Order = 1)] 
        [StringLength(10)]
		public string SHENPIID { get; set; }
		/// <summary>
		/// 费用控制ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string FEIYONGKZID { get; set; }
		/// <summary>
		/// 项目ID
		/// </summary>
		[Required]
		[StringLength(50)]
		public string XIANGMUID { get; set; }
		/// <summary>
		/// 审批类型
		/// </summary>
		[Required]
		[StringLength(4)]
		public string SHENPILX { get; set; }
		/// <summary>
		/// 项目类型
		/// </summary>
		[StringLength(4)]
		public string XIANGMULX { get; set; }
		/// <summary>
		/// 审批结果期限
		/// </summary>
		[StringLength(4)]
		public string SHENPIJGQX { get; set; }
		/// <summary>
		/// 门诊使用
		/// </summary>
		[Required]
		public int? MENZHENSY { get; set; }
		/// <summary>
		/// 住院使用
		/// </summary>
		[Required]
		public int? ZHUYUANSY { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		[Required]
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 开始日期
		/// </summary>
		[Required]
		public DateTime? KAISHIRQ { get; set; }
		/// <summary>
		/// 结束日期
		/// </summary>
		public DateTime? JIESHURQ { get; set; }
		/// <summary>
		/// 修改人
		/// </summary>
		[Required]
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		[Required]
		public DateTime? XIUGAISJ { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		[StringLength(500)]
		public string BEIZHU { get; set; }
		/// <summary>
		/// 项目名称
		/// </summary>
		[StringLength(100)]
		public string XIANGMUMC { get; set; }
		/// <summary>
		/// 医保IDHR3-12058(158865)
		/// </summary>
		[StringLength(4)]
		public string YIBAOID { get; set; }
		private int? _SHENPIBZ;
		/// <summary>
		/// 审批标志1.需要审批,2不须要审批HR3-12058(158865)
		/// </summary>
		public int? SHENPIBZ { get{ return _SHENPIBZ; } set{ _SHENPIBZ = value; } }
		/// <summary>
		/// 药品诊疗标志(0诊疗1.药品)HR3-12058(158865)
		/// </summary>
		public int? YAOPINZLBZ { get; set; }
		/// <summary>
		/// 输入码1HR3-12058(158865)
		/// </summary>
		[StringLength(10)]
		public string SHURUMA1 { get; set; }
		/// <summary>
		/// 输入码2HR3-12058(158865)
		/// </summary>
		[StringLength(10)]
		public string SHURUMA2 { get; set; }
		/// <summary>
		/// 输入码3HR3-12058(158865)
		/// </summary>
		[StringLength(10)]
		public string SHURUMA3 { get; set; }
		/// <summary>
		/// 限制审批范围HR3-12452(162549)
		/// </summary>
		[StringLength(1000)]
		public string XIANZHISPFW { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		public void SetDefaultValue () 
		{
			int? SHENPIBZ = 1;
		}
	}
}
