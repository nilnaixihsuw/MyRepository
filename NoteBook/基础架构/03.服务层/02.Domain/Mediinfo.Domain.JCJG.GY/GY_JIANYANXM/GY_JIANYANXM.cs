using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JIANYANXM")]
	public partial class GY_JIANYANXM : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 检验项目ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JIANYANXMID { get; set; }
		/// <summary>
		/// 检验目的
		/// </summary>
		[Required]
		[StringLength(100)]
		public string JIANYANMD { get; set; }
		/// <summary>
		/// 打印名称
		/// </summary>
		[StringLength(50)]
		public string DAYINMC { get; set; }
		/// <summary>
		/// 样本类型
		/// </summary>
		[StringLength(10)]
		public string YANGBENLX { get; set; }
		/// <summary>
		/// 采集部位
		/// </summary>
		[StringLength(10)]
		public string CAIJIBW { get; set; }
		/// <summary>
		/// 执行科室
		/// </summary>
		[StringLength(10)]
		public string ZHIXINGKS { get; set; }
		/// <summary>
		/// 门诊使用
		/// </summary>
		public int? MENZHENSY { get; set; }
		/// <summary>
		/// 住院使用
		/// </summary>
		public int? ZHUYUANSY { get; set; }
		/// <summary>
		/// 急诊使用
		/// </summary>
		public int? JIZHENSY { get; set; }
		/// <summary>
		/// 套餐标志
		/// </summary>
		public int? TAOCANBZ { get; set; }
		/// <summary>
		/// 容器
		/// </summary>
		[StringLength(10)]
		public string RONGQI { get; set; }
		/// <summary>
		/// 采集说明
		/// </summary>
		[StringLength(200)]
		public string CAIJISM { get; set; }
		/// <summary>
		/// 化验分类
		/// </summary>
		[StringLength(10)]
		public string HUAYANFL { get; set; }
		/// <summary>
		/// 采血量
		/// </summary>
		[StringLength(20)]
		public string CAIXUELIANG { get; set; }
		/// <summary>
		/// 采血时间
		/// </summary>
		[StringLength(100)]
		public string CAIXUESJ { get; set; }
		/// <summary>
		/// 采血地点
		/// </summary>
		[StringLength(100)]
		public string CAIXUEDD { get; set; }
		/// <summary>
		/// 取报告地点
		/// </summary>
		[StringLength(500)]
		public string QUBAOGDD { get; set; }
		/// <summary>
		/// 取报告时间
		/// </summary>
		[StringLength(500)]
		public string QUBAOGSJ { get; set; }
		/// <summary>
		/// 检验说明
		/// </summary>
		[StringLength(500)]
		public string JIANYANSM { get; set; }
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
		/// 作废标志
		/// </summary>
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 特殊标志
		/// </summary>
		public int? TESHUBZ { get; set; }
		/// <summary>
		/// 组合编号
		/// </summary>
		[StringLength(10)]
		public string ZUHEBH { get; set; }
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
		/// 导医说明
		/// </summary>
		[StringLength(500)]
		public string DAOYISM { get; set; }
		/// <summary>
		/// 检验分类
		/// </summary>
		[StringLength(10)]
		public string JIANYANFL { get; set; }
		/// <summary>
		/// 变应原类型
		/// </summary>
		[StringLength(2)]
		public string BIANYINGYLX { get; set; }
		/// <summary>
		/// 关联ID
		/// </summary>
		[StringLength(10)]
		public string GUANLIANID { get; set; }
		/// <summary>
		/// 外送标志
		/// </summary>
		public int? WAISONGBZ { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? SHUNXUH { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? JIANYANXMFL { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(10)]
		public string YANGBENYXSJ { get; set; }
		/// <summary>
		/// 住院分类
		/// </summary>
		[StringLength(10)]
		public string ZHUYUANFL { get; set; }
		private int? _XIANSHIZXXX;
		/// <summary>
		/// 医嘱单是否显示执行信息
		/// </summary>
		public int? XIANSHIZXXX { get; set; }
		/// <summary>
		/// 院区使用HR3-17296(206891)
		/// </summary>
		[StringLength(10)]
		public string YUANQUSY { get; set; }
		private int? _SONGJIANDYBZ;
		/// <summary>
		/// 送检打印标志 HR3-23942(262791)
		/// </summary>
		public int? SONGJIANDYBZ { get; set; }
		/// <summary>
		/// .打印对象名称 HR3-23942(262791)
		/// </summary>
		[StringLength(100)]
		public string DAYINDX { get; set; }
		/// <summary>
		/// 项目描述HR3-26922(281659)
		/// </summary>
		[StringLength(1000)]
		public string XIANGMUMS { get; set; }
		/// <summary>
		/// 条码打印上限HR3-40298(364859)
		/// </summary>
		public int? TIAOMADYSX { get; set; }
		/// <summary>
		/// 双签名标志HR3-42121(375017)
		/// </summary>
		public int? SHUANGQIANMBZ { get; set; }
		/// <summary>
		/// 性别HR3-45776(394527)
		/// </summary>
		[StringLength(4)]
		public string XINGBIE { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{
			if (XIANSHIZXXX.IsNullOrDBNull())
			XIANSHIZXXX = 1;
			if (SONGJIANDYBZ.IsNullOrDBNull())
			SONGJIANDYBZ = 0;
		}
	}
}
