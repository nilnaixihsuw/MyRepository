using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_ZHONGYAOPFKLZD")]
	public partial class GY_ZHONGYAOPFKLZD : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 中药配方颗粒ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string ZHONGYAOPFKLID { get; set; }
		/// <summary>
		/// 代号
		/// </summary>
		[Required]
		[StringLength(20)]
		public string DAIHAO { get; set; }
		/// <summary>
		/// 功能
		/// </summary>
		[StringLength(100)]
		public string GONGNENG { get; set; }
		/// <summary>
		/// 条码
		/// </summary>
		[Required]
		[StringLength(20)]
		public string TIAOMA { get; set; }
		/// <summary>
		/// 品名
		/// </summary>
		[Required]
		[StringLength(50)]
		public string PINMING { get; set; }
		/// <summary>
		/// 规格
		/// </summary>
		[StringLength(100)]
		public string GUIGE { get; set; }
		/// <summary>
		/// 饮片量
		/// </summary>
		[StringLength(100)]
		public string YINPIANLIANG { get; set; }
		/// <summary>
		/// 转换数量
		/// </summary>
		[Required]
		public decimal? ZHUANHUANSL { get; set; }
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
	}
}
