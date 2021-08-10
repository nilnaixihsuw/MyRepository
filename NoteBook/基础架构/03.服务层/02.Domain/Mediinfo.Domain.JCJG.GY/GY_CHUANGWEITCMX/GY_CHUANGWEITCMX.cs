using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_CHUANGWEITCMX")]
	public partial class GY_CHUANGWEITCMX : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 套餐明细ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string CHUANGWEITCMXID { get; set; }
		/// <summary>
		/// 项目类型
		/// </summary>
		[StringLength(4)]
		public string XIANGMULX { get; set; }
		/// <summary>
		/// 项目ID
		/// </summary>
		[StringLength(10)]
		public string XIANGMUID { get; set; }
		/// <summary>
		/// 数量
		/// </summary>
		public decimal? SHULIANG { get; set; }
		/// <summary>
		/// 级别
		/// </summary>
		public int? JIBIE { get; set; }
		/// <summary>
		/// 开始时间
		/// </summary>
		public DateTime? KAISHISJ { get; set; }
		/// <summary>
		/// 结束时间
		/// </summary>
		public DateTime? JIESHUSJ { get; set; }
		/// <summary>
		/// 项目名称
		/// </summary>
		[StringLength(100)]
		public string XIANGMUMC { get; set; }
		/// <summary>
		/// 单价
		/// </summary>
		public decimal? DANJIA { get; set; }
		/// <summary>
		/// 取数方式
		/// </summary>
		[StringLength(1)]
		public string QUSHUFS { get; set; }
		/// <summary>
		/// 性质属性
		/// </summary>
		[StringLength(10)]
		public string XINGZHISX { get; set; }
		/// <summary>
		/// 床位套餐ID
		/// </summary>
		[StringLength(10)]
		public string CHUANGWEITCID { get; set; }
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
		/// 婴儿计费比例
		/// </summary>
		public decimal? YINGERJFBL{ get; set; }
	}
}
