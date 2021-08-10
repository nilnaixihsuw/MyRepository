using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_KANGJUNYWSPSQD")]
	public partial class GY_KANGJUNYWSPSQD : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 申请单ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string SHENQINDANID { get; set; }
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
		/// 医嘱ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string YIZHUID { get; set; }
		/// <summary>
		/// 项目ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string XIANGMUID { get; set; }
		/// <summary>
		/// 项目名称
		/// </summary>
		[StringLength(500)]
		public string XIANGMUMC { get; set; }
		/// <summary>
		/// 姓名
		/// </summary>
		[Required]
		[StringLength(50)]
		public string XINGMING { get; set; }
		/// <summary>
		/// 性别
		/// </summary>
		[StringLength(10)]
		public string XINGBIE { get; set; }
		/// <summary>
		/// 年龄
		/// </summary>
		public int? NIANLING { get; set; }
		/// <summary>
		/// 年龄单位
		/// </summary>
		[StringLength(40)]
		public string NIANLINGDW { get; set; }
		/// <summary>
		/// 出生日期
		/// </summary>
		public DateTime? CHUSHENGRQ { get; set; }
		/// <summary>
		/// 住院号
		/// </summary>
		[StringLength(10)]
		public string ZHUYUANHAO { get; set; }
		private string _SHENQINGLX;
		/// <summary>
		/// 申请类型0.越级1.会诊
		/// </summary>
		[StringLength(4)]
		public string SHENQINGLX { get; set; }
		/// <summary>
		/// 病情介绍
		/// </summary>
		[StringLength(2000)]
		public string BINGQINGJS { get; set; }
		/// <summary>
		/// 临床诊断
		/// </summary>
		[StringLength(500)]
		public string LINCHUANGZD { get; set; }
		/// <summary>
		/// 申请理由
		/// </summary>
		[StringLength(1000)]
		public string SHENQINGLY { get; set; }
		/// <summary>
		/// 剂量
		/// </summary>
		[StringLength(100)]
		public string JILIANG { get; set; }
		/// <summary>
		/// 用法
		/// </summary>
		[StringLength(100)]
		public string YONGFA { get; set; }
		/// <summary>
		/// 疗程
		/// </summary>
		[StringLength(100)]
		public string LIAOCHENG { get; set; }
		/// <summary>
		/// 申请人
		/// </summary>
		[StringLength(10)]
		public string SHENQINGREN { get; set; }
		/// <summary>
		/// 申请日期
		/// </summary>
		public DateTime? SHENQINGRQ { get; set; }
		/// <summary>
		/// 会诊建议
		/// </summary>
		[StringLength(1000)]
		public string HUIZHENJY { get; set; }
		private string _SHENQINGTZ;
		/// <summary>
		/// 申请状态0.未处理1.同意2拒绝
		/// </summary>
		[StringLength(10)]
		public string SHENQINGTZ { get; set; }
		/// <summary>
		/// 会诊人
		/// </summary>
		[StringLength(10)]
		public string HUIZHENREN { get; set; }
		/// <summary>
		/// 会诊日期
		/// </summary>
		public DateTime? HUIZHENRQ { get; set; }
		/// <summary>
		/// 申请科室
		/// </summary>
		[StringLength(10)]
		public string SHENQINGKS { get; set; }
		/// <summary>
		/// 用药阶段0.普通1.术前2.术中3.术后
		/// </summary>
		[StringLength(10)]
		public string YONGYAOJD { get; set; }
		/// <summary>
		/// 用药原因
		/// </summary>
		[StringLength(100)]
		public string YONGYAOYY { get; set; }
		/// <summary>
		/// 用药实效,小时数
		/// </summary>
		public decimal? YONGYAOSX { get; set; }
		/// <summary>
		/// 邀请医生
		/// </summary>
		[StringLength(10)]
		public string YAOQINGYS { get; set; }
		/// <summary>
		/// HRHR3-52995(436714)
		/// </summary>
		[StringLength(10)]
		public string HR { get; set; }
		/// <summary>
		/// BPHR3-52995(436714)
		/// </summary>
		[StringLength(10)]
		public string BP { get; set; }
		/// <summary>
		/// RRHR3-52995(436714)
		/// </summary>
		[StringLength(10)]
		public string RR { get; set; }
		/// <summary>
		/// 体温HR3-52995(436714)
		/// </summary>
		[StringLength(10)]
		public string TIWEN { get; set; }
		/// <summary>
		/// 血氧HR3-52995(436714)
		/// </summary>
		[StringLength(10)]
		public string XUEYANG { get; set; }
		/// <summary>
		/// 畏寒HR3-52995(436714)
		/// </summary>
		public int? WEIHAN { get; set; }
		/// <summary>
		/// 黄脓痰HR3-52995(436714)
		/// </summary>
		public int? HUANNONGTAN { get; set; }
		/// <summary>
		/// 胸闷气急HR3-52995(436714)
		/// </summary>
		public int? XIONGMENQJ { get; set; }
		/// <summary>
		/// 局部红肿热疼HR3-52995(436714)
		/// </summary>
		public int? JUBUHZRT { get; set; }
		/// <summary>
		/// 导管脓性引流物HR3-52995(436714)
		/// </summary>
		public int? DAOGUANNXYLW { get; set; }
		/// <summary>
		/// 神志改变HR3-52995(436714)
		/// </summary>
		public int? SHENZHIGB { get; set; }
		/// <summary>
		/// WCBHR3-52995(436714)
		/// </summary>
		[StringLength(10)]
		public string WCB { get; set; }
		/// <summary>
		/// BLPHR3-52995(436714)
		/// </summary>
		[StringLength(10)]
		public string BLP { get; set; }
		/// <summary>
		/// NAHR3-52995(436714)
		/// </summary>
		[StringLength(10)]
		public string NA { get; set; }
		/// <summary>
		/// NBHR3-52995(436714)
		/// </summary>
		[StringLength(10)]
		public string NB { get; set; }
		/// <summary>
		/// PCTHR3-52995(436714)
		/// </summary>
		[StringLength(10)]
		public string PCT { get; set; }
		/// <summary>
		/// HCRPHR3-52995(436714)
		/// </summary>
		[StringLength(10)]
		public string HCRP { get; set; }
		/// <summary>
		/// GGMHR3-52995(436714)
		/// </summary>
		[StringLength(10)]
		public string GGM { get; set; }
		/// <summary>
		/// 淋巴细胞亚群分类HR3-52995(436714)
		/// </summary>
		[StringLength(100)]
		public string LINBAXBYQFL { get; set; }
		/// <summary>
		/// 影像学检查结果HR3-52995(436714)
		/// </summary>
		[StringLength(1000)]
		public string YINGXIANXJCJG { get; set; }
		/// <summary>
		/// 标本送检HR3-52995(436714)
		/// </summary>
		public int? YANGBENSJ { get; set; }
		/// <summary>
		/// 何种责任菌HR3-52995(436714)
		/// </summary>
		[StringLength(100)]
		public string HEZHONGZRJ { get; set; }
		/// <summary>
		/// 排除污染与定植HR3-52995(436714)
		/// </summary>
		public int? PAICHUWR { get; set; }
		/// <summary>
		/// 放疗情况HR3-52995(436714)
		/// </summary>
		[StringLength(1000)]
		public string FANGLIAOQK { get; set; }
		/// <summary>
		/// 化疗情况HR3-52995(436714)
		/// </summary>
		[StringLength(1000)]
		public string HUALIAOQK { get; set; }
		/// <summary>
		/// 目前正在使用的抗感染方案HR3-52995(436714)
		/// </summary>
		[StringLength(1000)]
		public string KANGGANRFA { get; set; }
		/// <summary>
		/// 三级医师HR3-52995(436714)
		/// </summary>
		[StringLength(10)]
		public string SANJIYS { get; set; }
		/// <summary>
		/// 备注HR3-52995(436714)
		/// </summary>
		[StringLength(1000)]
		public string BEIZHU { get; set; }
		/// <summary>
		/// 审核日期HR3-52995(436714)
		/// </summary>
		public DateTime? SHENHERQ { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{
			if (SHENQINGLX.IsNullOrDBNull())
			SHENQINGLX = "0";
			if (SHENQINGTZ.IsNullOrDBNull())
			SHENQINGTZ = "0";
		}
	}
}
