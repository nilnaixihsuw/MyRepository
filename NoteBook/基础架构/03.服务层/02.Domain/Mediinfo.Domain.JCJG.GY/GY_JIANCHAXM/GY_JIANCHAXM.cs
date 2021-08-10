using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JIANCHAXM")]
	public partial class GY_JIANCHAXM : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 检查项目ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JIANCHAXMID { get; set; }
		/// <summary>
		/// 检查项目名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string JIANCHAXMMC { get; set; }
		/// <summary>
		/// 检查类型
		/// </summary>
		[Required]
		[StringLength(10)]
		public string JIANCHALX { get; set; }
		/// <summary>
		/// 执行科室
		/// </summary>
		[StringLength(10)]
		public string ZHIXINGKS { get; set; }
		/// <summary>
		/// 输入码1
		/// </summary>
		[StringLength(10)]
		public string SHURUMA1 { get; set; }
		/// <summary>
		/// 输入码2
		/// </summary>
		[StringLength(10)]
		public string SHURUMA2 { get; set; }
		/// <summary>
		/// 输入码3
		/// </summary>
		[StringLength(10)]
		public string SHURUMA3 { get; set; }
		/// <summary>
		/// 父类ID
		/// </summary>
		[StringLength(10)]
		public string FULEIID { get; set; }
		private int? _MOJIBZ;
		/// <summary>
		/// 末级标志
		/// </summary>
		[Required]
		public int? MOJIBZ { get; set; }
		/// <summary>
		/// 院区使用
		/// </summary>
		[StringLength(10)]
		public string YUANQUSY { get; set; }
		/// <summary>
		/// 门诊使用
		/// </summary>
		public int? MENZHENSY { get; set; }
		/// <summary>
		/// 住院使用
		/// </summary>
		public int? ZHUYUANSY { get; set; }
		private int? _ZUOFEIBZ;
		/// <summary>
		/// 作废标志
		/// </summary>
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 划价标志
		/// </summary>
		public int? HUAJIABZ { get; set; }
		/// <summary>
		/// 模版代码
		/// </summary>
		[StringLength(50)]
		public string MOBANDM { get; set; }
		/// <summary>
		/// 检查说明
		/// </summary>
		[StringLength(1000)]
		public string JIANCHASM { get; set; }
		/// <summary>
		/// 导医说明
		/// </summary>
		[StringLength(500)]
		public string DAOYISM { get; set; }
		private int? _YUYUEBZ;
		/// <summary>
		/// 预约标志
		/// </summary>
		[Required]
		public int? YUYUEBZ { get; set; }
		private int? _BUWEIJJBZ;
		/// <summary>
		/// 部位计价标志
		/// </summary>
		public int? BUWEIJJBZ { get; set; }
		private decimal? _YOUXIAOBWSL;
		/// <summary>
		/// 有效部位数量
		/// </summary>
		public decimal? YOUXIAOBWSL { get; set; }
		private int? _DANGTIANZDYYXZ;
		/// <summary>
		/// 当天自动预约限制
		/// </summary>
		public int? DANGTIANZDYYXZ { get; set; }
		/// <summary>
		/// 检查项目简称
		/// </summary>
		[StringLength(100)]
		public string JIANCHAXMJC { get; set; }
		/// <summary>
		/// 部位分割标志
		/// </summary>
		public int? BUWEIFGBZ { get; set; }
		/// <summary>
		/// 特殊标志 1.不控制数量
		/// </summary>
		public int? TESHUBZ { get; set; }
		private int? _TUWENJPFBZ;
		/// <summary>
		/// 图文胶片费标志HR3-19382(221511)
		/// </summary>
		public int? TUWENJPFBZ { get; set; }
		/// <summary>
		/// 顺序号HR3-21152(243707)
		/// </summary>
		public int? SHUNXUHAO { get; set; }
		private int? _YUYUETXBZ;
		/// <summary>
		/// 预约提醒标志HR3-26690(280267)
		/// </summary>
		public int? YUYUETXBZ { get; set; }
		/// <summary>
		/// 项目描述HR3-26922(281659)
		/// </summary>
		[StringLength(1000)]
		public string XIANGMUMS { get; set; }
		private int? _KESHIXZ;
		/// <summary>
		/// 科室性质HR3-30873(308214) 0普通,1急诊,2全部
		/// </summary>
		public int? KESHIXZ { get; set; }
		/// <summary>
		/// 增强扫描标志HR3-17066(205489)
		/// </summary>
		public int? ZENGQIANGSMBZ { get; set; }
		/// <summary>
		/// 是否打印知情同意书HR3-17066(205489)
		/// </summary>
		public int? DANYINZQTYSBZ { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{
			if (MOJIBZ.IsNullOrDBNull())
			MOJIBZ = 0 ;
			if (ZUOFEIBZ.IsNullOrDBNull())
			ZUOFEIBZ = 0;
			if (YUYUEBZ.IsNullOrDBNull())
			YUYUEBZ = 0 ;
			if (BUWEIJJBZ.IsNullOrDBNull())
			BUWEIJJBZ = 0;
			if (YOUXIAOBWSL.IsNullOrDBNull())
			YOUXIAOBWSL = 0;
			if (DANGTIANZDYYXZ.IsNullOrDBNull())
			DANGTIANZDYYXZ = 0;
			if (TUWENJPFBZ.IsNullOrDBNull())
			TUWENJPFBZ = 0;
			if (YUYUETXBZ.IsNullOrDBNull())
			YUYUETXBZ = 0;
			if (KESHIXZ.IsNullOrDBNull())
			KESHIXZ = 0;
		}
	}
}
