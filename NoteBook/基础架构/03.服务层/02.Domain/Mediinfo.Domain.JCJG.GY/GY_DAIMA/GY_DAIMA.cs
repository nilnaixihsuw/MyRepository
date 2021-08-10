using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_DAIMA")]
	public partial class GY_DAIMA : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 代码ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string DAIMAID { get; set; }
		/// <summary>
		/// 代码类别
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string DAIMALB { get; set; }
		/// <summary>
		/// 代码名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string DAIMAMC { get; set; }
		/// <summary>
		/// 级次
		/// </summary>
		public int? JICI { get; set; }
		/// <summary>
		/// 父类代码
		/// </summary>
		[StringLength(10)]
		public string FULEIDM { get; set; }
		/// <summary>
		/// 末级标志
		/// </summary>
		[Required]
		public int? MOJIBZ { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		[Required]
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 顺序号
		/// </summary>
		[Required]
		public int? SHUNXUHAO { get; set; }
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
		/// 系统标志
		/// </summary>
		public int? XITONGBZ { get; set; }
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
		/// 字符1
		/// </summary>
		[StringLength(50)]
		public string ZIFU1 { get; set; }
		/// <summary>
		/// 字符2
		/// </summary>
		[StringLength(20)]
		public string ZIFU2 { get; set; }
		/// <summary>
		/// 字符3
		/// </summary>
		[StringLength(50)]
		public string ZIFU3 { get; set; }
		/// <summary>
		/// 字符4
		/// </summary>
		[StringLength(100)]
		public string ZIFU4 { get; set; }
		/// <summary>
		/// 字符5
		/// </summary>
		[StringLength(1000)]
		public string ZIFU5 { get; set; }
		/// <summary>
		/// 数字1
		/// </summary>
		public decimal? SHUZI1 { get; set; }
		/// <summary>
		/// 数字2
		/// </summary>
		public decimal? SHUZI2 { get; set; }
		/// <summary>
		/// 数字3
		/// </summary>
		public decimal? SHUZI3 { get; set; }
		/// <summary>
		/// 日期1
		/// </summary>
		public DateTime? RIQI1 { get; set; }
		/// <summary>
		/// 日期2
		/// </summary>
		public DateTime? RIQI2 { get; set; }
		/// <summary>
		/// 日期3
		/// </summary>
		public DateTime? RIQI3 { get; set; }
		/// <summary>
		/// 标志位1
		/// </summary>
		public int? BIAOZHIWEI1 { get; set; }
		/// <summary>
		/// 标志位2
		/// </summary>
		public int? BIAOZHIWEI2 { get; set; }
		/// <summary>
		/// 标志位3
		/// </summary>
		public int? BIAOZHIWEI3 { get; set; }
		/// <summary>
		/// 代码简称
		/// </summary>
		[StringLength(20)]
		public string DAIMAJC { get; set; }
		/// <summary>
		/// 字符6
		/// </summary>
		[StringLength(500)]
		public string ZIFU6 { get; set; }
		/// <summary>
		/// 字符7
		/// </summary>
		[StringLength(100)]
		public string ZIFU7 { get; set; }
		/// <summary>
		/// 字符8
		/// </summary>
		[StringLength(100)]
		public string ZIFU8 { get; set; }
		/// <summary>
		/// 字符9
		/// </summary>
		[StringLength(100)]
		public string ZIFU9 { get; set; }
		/// <summary>
		/// 字符10
		/// </summary>
		[StringLength(100)]
		public string ZIFU10 { get; set; }
		/// <summary>
		/// 数值4
		/// </summary>
		public decimal? SHUZI4 { get; set; }
		/// <summary>
		/// 数值5
		/// </summary>
		public decimal? SHUZI5 { get; set; }
		/// <summary>
		/// 适用范围
		/// </summary>
		[StringLength(4)]
		public string SHIYONGFW { get; set; }
		/// <summary>
		/// 医生科室
		/// </summary>
		[StringLength(10)]
		public string YISHENGKS { get; set; }
		/// <summary>
		/// 英文名
		/// </summary>
		[StringLength(500)]
		public string YINGWENMING { get; set; }
		/// <summary>
		/// 字符11
		/// </summary>
		[StringLength(500)]
		public string ZIFU11 { get; set; }
		/// <summary>
		/// 字符12
		/// </summary>
		[StringLength(500)]
		public string ZIFU12 { get; set; }
		/// <summary>
		/// 字符13
		/// </summary>
		[StringLength(500)]
		public string ZIFU13 { get; set; }
		/// <summary>
		/// 字符14
		/// </summary>
		[StringLength(500)]
		public string ZIFU14 { get; set; }
		/// <summary>
		/// 字符15
		/// </summary>
		[StringLength(500)]
		public string ZIFU15 { get; set; }
		/// <summary>
		/// 字符16
		/// </summary>
		[StringLength(100)]
		public string ZIFU16 { get; set; }
		/// <summary>
		/// 字符17
		/// </summary>
		[StringLength(100)]
		public string ZIFU17 { get; set; }
		/// <summary>
		/// 字符18
		/// </summary>
		[StringLength(100)]
		public string ZIFU18 { get; set; }
		/// <summary>
		/// 字符19
		/// </summary>
		[StringLength(100)]
		public string ZIFU19 { get; set; }
		/// <summary>
		/// 字符20
		/// </summary>
		[StringLength(100)]
		public string ZIFU20 { get; set; }
		/// <summary>
		/// 默认频次 HR3-32959(320817)
		/// </summary>
		public int? MORENPC { get; set; }
	}
}
