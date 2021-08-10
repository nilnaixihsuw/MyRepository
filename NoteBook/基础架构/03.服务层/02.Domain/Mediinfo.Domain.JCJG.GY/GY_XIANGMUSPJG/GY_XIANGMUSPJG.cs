using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_XIANGMUSPJG")]
	public partial class GY_XIANGMUSPJG : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 审批结果ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string SHENPIJGID { get; set; }
		/// <summary>
		/// 审批ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string SHENPIID { get; set; }
		/// <summary>
		/// 病人ID
		/// </summary>
		[Required]
		[StringLength(100)]
		public string BINGRENID { get; set; }
		/// <summary>
		/// 项目ID
		/// </summary>
		[Required]
		[StringLength(50)]
		public string XIANGMUID { get; set; }
		/// <summary>
		/// 项目类型
		/// </summary>
		[StringLength(4)]
		public string XIANGMULX { get; set; }
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
		/// 审批人
		/// </summary>
		[StringLength(10)]
		public string SHENPIREN { get; set; }
		/// <summary>
		/// 审批日期
		/// </summary>
		public DateTime? SHENPIRQ { get; set; }
		/// <summary>
		/// 审批数量
		/// </summary>
		public decimal? SHENPISL { get; set; }
		/// <summary>
		/// 开始日期
		/// </summary>
		public DateTime? KAISHIRQ { get; set; }
		/// <summary>
		/// 结束日期
		/// </summary>
		public DateTime? JIESHURQ { get; set; }
		/// <summary>
		/// 审批编号
		/// </summary>
		[StringLength(20)]
		public string SHENPIBH { get; set; }
		/// <summary>
		/// 审批结果0未审核，1审核通过，2审核未通过，3审核通过后报销完成
		/// </summary>
		public int? SHENPIJG { get; set; }
		/// <summary>
		/// 疾病编码
		/// </summary>
		[StringLength(50)]
		public string JIBINGDM { get; set; }
		/// <summary>
		/// 疾病名称
		/// </summary>
		[StringLength(100)]
		public string JIBINGMC { get; set; }
		/// <summary>
		/// 项目名称HR3-12058(158865)
		/// </summary>
		[StringLength(1000)]
		public string XIANGMUMC { get; set; }
		/// <summary>
		/// 检查治疗理由
		/// </summary>
		[StringLength(2000)]
		public string JIANCHAZLLY { get; set; }
		/// <summary>
		/// 经治医师
		/// </summary>
		[StringLength(10)]
		public string JINGZHIYS { get; set; }
		/// <summary>
		/// 签名日期
		/// </summary>
		public DateTime? QIANMINGRQ { get; set; }
		/// <summary>
		/// 医院审核意见
		/// </summary>
		[StringLength(2000)]
		public string YIYUANSHYJ { get; set; }
		/// <summary>
		/// 放化疗情况
		/// </summary>
		[StringLength(1000)]
		public string FANGHUALQK { get; set; }
		/// <summary>
		/// 商品名
		/// </summary>
		[StringLength(1000)]
		public string SHANGPINMING { get; set; }
		/// <summary>
		/// 进口
		/// </summary>
		public int? JINKOU { get; set; }
		/// <summary>
		/// 剂量
		/// </summary>
		[StringLength(100)]
		public string JILIANG { get; set; }
		/// <summary>
		/// 用量
		/// </summary>
		[StringLength(100)]
		public string YONGLIANG { get; set; }
		/// <summary>
		/// 一个疗程总量
		/// </summary>
		[StringLength(100)]
		public string ZONGLIANG { get; set; }
		/// <summary>
		/// 机构审批意见
		/// </summary>
		[StringLength(2000)]
		public string JIGOUSPYJ { get; set; }
		/// <summary>
		/// 机构审批日期
		/// </summary>
		public DateTime? JIGOUSPRQ { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		[StringLength(2000)]
		public string BEIZHU { get; set; }
		/// <summary>
		/// 1.口服2.注射
		/// </summary>
		public int? YONGYAOTJ { get; set; }
		/// <summary>
		/// 修改人
		/// </summary>
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? XIUGAISJ { get; set; }
		/// <summary>
		/// 申请日期
		/// </summary>
		public DateTime? SHENQINGRQ { get; set; }
		/// <summary>
		/// 申请人
		/// </summary>
		[StringLength(10)]
		public string SHENQINGREN { get; set; }
		private int? _ZUOFEIBZ;
		/// <summary>
		/// 作废标志
		/// </summary>
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 药品诊疗标志
		/// </summary>
		public int? YAOPINZLBZ { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[StringLength(10)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 院区ID
		/// </summary>
		[StringLength(4)]
		public string YUANQUID { get; set; }
		/// <summary>
		/// 住院号
		/// </summary>
		[StringLength(10)]
		public string ZHUYUANHAO { get; set; }
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
		public int? NIANLING { get; set; }
		/// <summary>
		/// 年龄单位
		/// </summary>
		[StringLength(4)]
		public string NIANLINGDW { get; set; }
		/// <summary>
		/// 参保单位名称
		/// </summary>
		[StringLength(500)]
		public string CANBAODWMC { get; set; }
		/// <summary>
		/// 单位编码
		/// </summary>
		[StringLength(100)]
		public string DANWEIBM { get; set; }
		/// <summary>
		/// 身份证号
		/// </summary>
		[StringLength(20)]
		public string SHENFENZH { get; set; }
		/// <summary>
		/// 医保卡号
		/// </summary>
		[StringLength(50)]
		public string YIBAOKH { get; set; }
		/// <summary>
		/// 诊断名称
		/// </summary>
		[StringLength(100)]
		public string ZHENDUANMC { get; set; }
		/// <summary>
		/// 就诊ID
		/// </summary>
		[StringLength(10)]
		public string JIUZHENID { get; set; }
		/// <summary>
		/// 费用类别
		/// </summary>
		[StringLength(10)]
		public string FEIYONGLB { get; set; }
		/// <summary>
		/// 费用性质
		/// </summary>
		[StringLength(10)]
		public string FEIYONGXZ { get; set; }
		/// <summary>
		/// 可用天数
		/// </summary>
		public int? KEYONGTS { get; set; }
		/// <summary>
		/// 申请科室HR3-16737(203120)
		/// </summary>
		[StringLength(10)]
		public string SHENQINGKS { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{
			if (ZUOFEIBZ.IsNullOrDBNull())
			ZUOFEIBZ = 0;
		}
	}
}
