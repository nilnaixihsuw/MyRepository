using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_KESHI")]
	public partial class GY_KESHI : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 科室ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string KESHIID { get; set; }
		/// <summary>
		/// 院区ID
		/// </summary>
		[StringLength(1)]
		public string YUANQUID { get; set; }
		/// <summary>
		/// 科室名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string KESHIMC { get; set; }
		/// <summary>
		/// 科室别名
		/// </summary>
		[StringLength(100)]
		public string KESHIBM { get; set; }
		/// <summary>
		/// 核算科室
		/// </summary>
		[StringLength(10)]
		public string HESUANKS { get; set; }
		/// <summary>
		/// 成本科室
		/// </summary>
		[StringLength(10)]
		public string CHENGBENKS { get; set; }
		/// <summary>
		/// 位置说明
		/// </summary>
		[StringLength(100)]
		public string WEIZHISM { get; set; }
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
		/// 门诊标志
		/// </summary>
		public int? MENZHENBZ { get; set; }
		/// <summary>
		/// 住院标志
		/// </summary>
		public int? ZHUYUANBZ { get; set; }
		/// <summary>
		/// 急诊标志
		/// </summary>
		[Required]
		public int? JIZHENBZ { get; set; }
		/// <summary>
		/// 科室性质
		/// </summary>
		[Required]
		[StringLength(4)]
		public string KESHIXZ { get; set; }
		/// <summary>
		/// 性质属性
		/// </summary>
		[Required]
		[StringLength(50)]
		public string XINGZHISX { get; set; }
		/// <summary>
		/// 药品使用范围
		/// </summary>
		public int? YAOPINSYFW { get; set; }
		/// <summary>
		/// 医疗使用范围
		/// </summary>
		public int? YILIAOSYFW { get; set; }
		/// <summary>
		/// 挂号费项目
		/// </summary>
		[StringLength(10)]
		public string GUAHAOFXM { get; set; }
		/// <summary>
		/// 诊疗费项目
		/// </summary>
		[StringLength(10)]
		public string ZHENLIAOFXM { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		[Required]
		public int? ZUOFEIBZ { get; set; }
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
		/// 人事科室
		/// </summary>
		[StringLength(10)]
		public string RENSHIKS { get; set; }
		/// <summary>
		/// 平均处方限额
		/// </summary>
		public decimal? PINGJUNCFXE { get; set; }
		/// <summary>
		/// 限额控制频率
		/// </summary>
		[StringLength(4)]
		public string XIANEKZPL { get; set; }
		/// <summary>
		/// 输液处方限用量
		/// </summary>
		public int? SHUYECFXYL { get; set; }
		/// <summary>
		/// 输液处方限天数
		/// </summary>
		public int? SHUYECFXTS { get; set; }
		/// <summary>
		/// 输液限量控制频度
		/// </summary>
		[StringLength(4)]
		public string SHUYEXLKZPD { get; set; }
		/// <summary>
		/// 英文名
		/// </summary>
		[StringLength(50)]
		public string YINGWENMING { get; set; }
		/// <summary>
		/// 联系电话
		/// </summary>
		[StringLength(20)]
		public string LIANXIDH { get; set; }
		/// <summary>
		/// 上级业务科室
		/// </summary>
		[StringLength(10)]
		public string SHANGJIYWKS { get; set; }
		/// <summary>
		/// 针剂开始时间
		/// </summary>
		public DateTime? ZHENJIKSSJ { get; set; }
		/// <summary>
		/// 诊疗开始时间
		/// </summary>
		public DateTime? ZHENLIAOKSSJ { get; set; }
		/// <summary>
		/// 口服开始时间
		/// </summary>
		public DateTime? KOUFUKSSJ { get; set; }
		/// <summary>
		/// 针剂取第二天量
		/// </summary>
		[StringLength(4)]
		public string ZHENJIQDETL { get; set; }
		/// <summary>
		/// 默认处方类型
		/// </summary>
		[StringLength(4)]
		public string MORENCFLX { get; set; }
		/// <summary>
		/// 炮弹位置
		/// </summary>
		[StringLength(20)]
		public string PAODANWZ { get; set; }
		/// <summary>
		/// 科室资料
		/// </summary>
		[StringLength(500)]
		public string KESHIZL { get; set; }
		/// <summary>
		/// 排队叫号模式 1,按序号模式;2,指定医生优先
		/// </summary>
		public int? PAIDUIJHMS { get; set; }
		/// <summary>
		/// 普通挂号医保传入医生代码
		/// </summary>
		[StringLength(12)]
		public string YIBAOYSDM { get; set; }
		/// <summary>
		/// 顺序号
		/// </summary>
		public long? SHUNXUHAO { get; set; }
		/// <summary>
		/// 院前准备启用标志
		/// </summary>
		public int? YUANQIANZBQYBZ { get; set; }
		/// <summary>
		/// 挂号科室分类，即科室大类，与gy_daima daimalb=9901对应
		/// </summary>
		[StringLength(10)]
		public string GUAHAOKSFL { get; set; }
		/// <summary>
		/// 温州卫生局标准v1.9.4 HR3-28528(292076) 
		/// </summary>
		[StringLength(10)]
		public string WSJJKDZ { get; set; }
		/// <summary>
		/// 温州卫生局标准v1.9.4 HR3-28528(292076) 
		/// </summary>
		[StringLength(100)]
		public string WSJJKDZKSMC { get; set; }
	}
}
