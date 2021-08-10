using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_CHURUKFS")]
	public partial class GY_CHURUKFS : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 应用ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 方式ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(4)]
		public string FANGSHIID { get; set; }
		/// <summary>
		/// 方式名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string FANGSHIMC { get; set; }
		/// <summary>
		/// 出入库标志
		/// </summary>
		[Required]
		[StringLength(4)]
		public string CHURUKBZ { get; set; }
		/// <summary>
		/// 库存增减标志
		/// </summary>
		[StringLength(4)]
		public string KUCUNZJBZ { get; set; }
		/// <summary>
		/// 单位部门
		/// </summary>
		[StringLength(4)]
		public string DANWEIBM { get; set; }
		/// <summary>
		/// 单价核算方式
		/// </summary>
		[StringLength(4)]
		public string DANJIAHSFS { get; set; }
		/// <summary>
		/// 显示标志
		/// </summary>
		[Required]
		public int? XIANSHIBZ { get; set; }
		/// <summary>
		/// 打印行数
		/// </summary>
		public int? DAYINHS { get; set; }
		/// <summary>
		/// 出库策略
		/// </summary>
		[StringLength(4)]
		public string CHUKUCL { get; set; }
		/// <summary>
		/// 批次包含进价
		/// </summary>
		public int? PICIBHJJ { get; set; }
		/// <summary>
		/// 发票号码标志
		/// </summary>
		[Required]
		public int? FAPIAOHMBZ { get; set; }
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
		/// 方式分类
		/// </summary>
		[Required]
		[StringLength(20)]
		public string FANGSHIFL { get; set; }
		/// <summary>
		/// 凭证类别
		/// </summary>
		[StringLength(4)]
		public string PINGZHENGLB { get; set; }
		/// <summary>
		/// 凭证打印方式
		/// </summary>
		public int? PINGZHENGDYFS { get; set; }
		/// <summary>
		/// 摘要打印方式
		/// </summary>
		public int? ZHAIYAODYFS { get; set; }
		/// <summary>
		/// 摘要打印格式
		/// </summary>
		[StringLength(100)]
		public string ZHAIYAODYGS { get; set; }
		private int? _BUCHABZ;
		/// <summary>
		/// 补差标志
		/// </summary>
		public int? BUCHABZ { get{ return _BUCHABZ; } set{ _BUCHABZ = value ?? 0; } }
		private string _YAOPINGGSYFS;
		/// <summary>
		/// 药品规格使用方式
		/// </summary>
		[StringLength(4)]
		public string YAOPINGGSYFS { get{ return _YAOPINGGSYFS; } set{ _YAOPINGGSYFS = value ?? "1"; } }
		private int? _KAIDANBZ;
		/// <summary>
		/// 开单标志
		/// </summary>
		public int? KAIDANBZ { get{ return _KAIDANBZ; } set{ _KAIDANBZ = value ?? 1; } }
	}
}
