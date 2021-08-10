using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_ZIFUBL")]
	public partial class GY_ZIFUBL : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 费用控制ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string FEIYONGKZID { get; set; }
		/// <summary>
		/// 项目ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string XIANGMUID { get; set; }
		/// <summary>
		/// 项目类型
		/// </summary>
		[Key]
		[Column(Order=3)]
		[StringLength(4)]
		public string XIANGMULX { get; set; }
		/// <summary>
		/// 自付比例
		/// </summary>
		[Required]
		public decimal? ZIFUBL { get; set; }
		/// <summary>
		/// 开始日期
		/// </summary>
		[Key]
		[Column(Order=4)]
		public DateTime? KAISHIRQ { get; set; }
		/// <summary>
		/// 结束日期
		/// </summary>
		public DateTime? JIESHURQ { get; set; }
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
		private string _FENDUANLX;
		/// <summary>
		/// 分段类型
		/// </summary>
		[StringLength(4)]
		public string FENDUANLX { get{ return _FENDUANLX; } set{ _FENDUANLX = value ?? "0"; } }
		/// <summary>
		/// 下限
		/// </summary>
		public decimal? XIAXIAN { get; set; }
		/// <summary>
		/// 上限
		/// </summary>
		public decimal? SHANGXIAN { get; set; }
	}
}
