using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YAOPINCLKZ")]
	public partial class GY_YAOPINCLKZ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 应用ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 价格ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string JIAGEID { get; set; }
		/// <summary>
		/// 规格ID
		/// </summary>
		[Key]
		[Column(Order=3)]
		[StringLength(10)]
		public string GUIGEID { get; set; }
		/// <summary>
		/// 药品ID
		/// </summary>
		[StringLength(10)]
		public string YAOPINID { get; set; }
		/// <summary>
		/// 库存上限
		/// </summary>
		public decimal? KUCUNSX { get; set; }
		/// <summary>
		/// 库存下限
		/// </summary>
		public decimal? KUCUNXX { get; set; }
		/// <summary>
		/// 管理模式
		/// </summary>
		public int? GUANLIMS { get; set; }
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
		/// 请领固定数量
		/// </summary>
		public decimal? QINGLINGGDSL { get; set; }
		/// <summary>
		/// 采购固定数量
		/// </summary>
		public decimal? CAIGOUGDSL { get; set; }
		/// <summary>
		/// 月采购数量
		/// </summary>
		public decimal? YUECAIGSL { get; set; }
		/// <summary>
		/// 库存警戒线
		/// </summary>
		public decimal? KUCUNJJX { get; set; }
		/// <summary>
		/// 批量生成库存上下限方案366667
		/// </summary>
		[StringLength(10)]
		public string PILIANGSCFA { get; set; }
		/// <summary>
		/// 批量生成库存上下限系数366667
		/// </summary>
		public decimal? KUCUNSCXS { get; set; }
		/// <summary>
		/// 批量生成库存上下限-上限368069
		/// </summary>
		public decimal? PILIANGSCSX { get; set; }
		/// <summary>
		/// 批量生成库存上下限-下限368069
		/// </summary>
		public decimal? PILIANGSCXX { get; set; }
		/// <summary>
		/// 批量生成库存上下限-标志368069
		/// </summary>
		[StringLength(10)]
		public string PILIANGSCBZ { get; set; }
	}
}
