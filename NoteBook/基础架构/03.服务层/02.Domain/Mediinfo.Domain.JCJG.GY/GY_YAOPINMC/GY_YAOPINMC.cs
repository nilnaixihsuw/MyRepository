using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YAOPINMC")]
	public partial class GY_YAOPINMC : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 药品ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string YAOPINID { get; set; }
		/// <summary>
		/// 收费项目
		/// </summary>
		[StringLength(10)]
		public string SHOUFEIXM { get; set; }
		/// <summary>
		/// 药品分类
		/// </summary>
		[Required]
		[StringLength(10)]
		public string YAOPINFL { get; set; }
		/// <summary>
		/// 标准编码
		/// </summary>
		[Required]
		[StringLength(20)]
		public string BIAOZHUNBM { get; set; }
		/// <summary>
		/// 药品名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string YAOPINMC { get; set; }
		/// <summary>
		/// 药品类型
		/// </summary>
		[Required]
		[StringLength(4)]
		public string YAOPINLX { get; set; }
		/// <summary>
		/// 库房类型
		/// </summary>
		[Required]
		[StringLength(4)]
		public string KUFANGLX { get; set; }
		/// <summary>
		/// 急诊用药标志
		/// </summary>
		[Required]
		public int? JIZHENYYBZ { get; set; }
		/// <summary>
		/// 控制药量标志
		/// </summary>
		[StringLength(10)]
		public string KONGZHIYLBZ { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		[Required]
		public int? ZUOFEIBZ { get; set; }
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
		/// 修改人
		/// </summary>
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? XIUGAISJ { get; set; }
		/// <summary>
		/// 毒理分类
		/// </summary>
		[Required]
		[StringLength(4)]
		public string DULIFL { get; set; }
		/// <summary>
		/// 价值分类
		/// </summary>
		[Required]
		[StringLength(4)]
		public string JIAZHIFL { get; set; }
		/// <summary>
		/// 制剂剂型
		/// </summary>
		[StringLength(10)]
		public string ZHIJIJX { get; set; }
		/// <summary>
		/// OTC标志
		/// </summary>
		public int? OTCBZ { get; set; }
		private int? _FEIYONGLLBZ;
		/// <summary>
		/// 费用录入标志
		/// </summary>
		public int? FEIYONGLLBZ { get{ return _FEIYONGLLBZ; } set{ _FEIYONGLLBZ = value ?? 1; } }
		/// <summary>
		/// 药品根类
		/// </summary>
		[StringLength(10)]
		public string YAOPINGL { get; set; }
		/// <summary>
		/// 顺序号
		/// </summary>
		public int? SHUNXUHAO { get; set; }
		/// <summary>
		/// 物品ID--来源总务设备物资HR3-14830(187366)
		/// </summary>
		[StringLength(10)]
		public string WUPINID { get; set; }
		/// <summary>
		/// 院区使用
		/// </summary>
		[StringLength(10)]
		public string YUANQUSY { get; set; }
		private int? _WAIPEIBZ;
		/// <summary>
		/// 外配标志(0院内 1外配 2院外)
		/// </summary>
		public int? WAIPEIBZ { get{ return _WAIPEIBZ; } set{ _WAIPEIBZ = value ?? 0; } }
		/// <summary>
		/// 查询标志
		/// </summary>
		public int? CHAXUNBZ { get; set; }
	}
}
