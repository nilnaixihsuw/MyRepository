using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YOUHUILB")]
	public partial class GY_YOUHUILB : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 优惠类别ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(4)]
		public string YOUHUILBID { get; set; }
		/// <summary>
		/// 优惠名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string YOUHUIMC { get; set; }
		/// <summary>
		/// 优惠方式
		/// </summary>
		[Required]
		public int? YOUHUIFS { get; set; }
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
		/// 挂号使用
		/// </summary>
		[Required]
		public int? GUAHAOSY { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		[Required]
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 打印标志
		/// </summary>
		[Required]
		public int? DAYINBZ { get; set; }
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
		/// 顺序号
		/// </summary>
		[Required]
		public int? SHUNXUHAO { get; set; }
		/// <summary>
		/// 输入码3
		/// </summary>
		[StringLength(10)]
		public string SHURUMA3 { get; set; }
		/// <summary>
		/// 输入码2
		/// </summary>
		[StringLength(10)]
		public string SHURUMA2 { get; set; }
		/// <summary>
		/// 输入码1
		/// </summary>
		[StringLength(10)]
		public string SHURUMA1 { get; set; }
		/// <summary>
		/// 优惠金额--定额值
		/// </summary>
		public decimal? YOUHUIJE { get; set; }
		/// <summary>
		/// 急诊挂号优惠金额HR3-13092
		/// </summary>
		public decimal? JIZHENGHYHJE { get; set; }
		/// <summary>
		/// 性质属性HR3-13146)
		/// </summary>
		[StringLength(10)]
		public string XINGZHISX { get; set; }
		/// <summary>
		/// 结算使用HR3-15184(191654)
		/// </summary>
		public int? JIESUANSY { get; set; }
		/// <summary>
		/// 全院减免标志HR3-17884(210397)
		/// </summary>
		public int? QUANYUANJMBZ { get; set; }
		/// <summary>
		/// 有效时间HR3-16946(204408)
		/// </summary>
		public DateTime? YOUXIAOSJ { get; set; }
		/// <summary>
		/// 是否显示HR3-19700(225231)
		/// </summary>
		public int? SHIFOUXS { get; set; }
		/// <summary>
		/// 剔除丙类标志HR3-32920(320615)
		/// </summary>
		public int? TICHUBINGLEI { get; set; }
	}
}
