using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_BINGQU")]
	public partial class GY_BINGQU : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 病区ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string BINGQUID { get; set; }
		/// <summary>
		/// 院区ID
		/// </summary>
		[StringLength(1)]
		public string YUANQUID { get; set; }
		/// <summary>
		/// 病区名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string BINGQUMC { get; set; }
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
		/// 病区前缀
		/// </summary>
		[StringLength(10)]
		public string BINGQUQZ { get; set; }
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
		/// 病区类型
		/// </summary>
		[StringLength(4)]
		public string BINGQULX { get; set; }
		/// <summary>
		/// 英文名
		/// </summary>
		[StringLength(50)]
		public string YINGWENMING { get; set; }
		/// <summary>
		/// 炮弹位置
		/// </summary>
		[StringLength(20)]
		public string PAODANWZ { get; set; }
		/// <summary>
		/// 最大床位数
		/// </summary>
		[StringLength(10)]
		public string CHUANGWEIZS { get; set; }
		/// <summary>
		/// 字符1HR3-21454(245404)目前基线个性化使用
		/// </summary>
		[StringLength(10)]
		public string ZIFU1 { get; set; }
		/// <summary>
		/// 字符2HR3-21454(245404)目前基线个性化使用
		/// </summary>
		[StringLength(10)]
		public string ZIFU2 { get; set; }
		private int? _JIECHUANGBZ;
		/// <summary>
		/// 允许借床标志，1为允许借床，0为不允许
		/// </summary>
		public int? JIECHUANGBZ { get{ return _JIECHUANGBZ; } set{ _JIECHUANGBZ = value ?? 0; } }
		/// <summary>
		/// 1.男2.女HR3-29012(294850)
		/// </summary>
		[StringLength(4)]
		public string XINGBIESX { get; set; }
		/// <summary>
		/// 顺序号HR3-32863(320327)
		/// </summary>
		public int? SHUNXUHAO { get; set; }
	}
}
