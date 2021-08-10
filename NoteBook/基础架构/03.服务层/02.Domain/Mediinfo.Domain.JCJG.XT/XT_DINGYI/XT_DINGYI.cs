using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.XT
{
	[Table("XT_DINGYI")]
	public partial class XT_DINGYI : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 系统ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(2)]
		public string XITONGID { get; set; }
		/// <summary>
		/// 系统名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string XITONGMC { get; set; }
		/// <summary>
		/// 系统简称
		/// </summary>
		[Required]
		[StringLength(20)]
		public string XITONGJC { get; set; }
		/// <summary>
		/// 说明
		/// </summary>
		[StringLength(100)]
		public string SHUOMING { get; set; }
		/// <summary>
		/// 执行文件
		/// </summary>
		[StringLength(50)]
		public string ZHIXINGWJ { get; set; }
		/// <summary>
		/// 启用标志
		/// </summary>
		[Required]
		public int? QIYONGBZ { get; set; }
		/// <summary>
		/// 启用日期
		/// </summary>
		public DateTime? QIYONGRQ { get; set; }
		/// <summary>
		/// 实例数量
		/// </summary>
		[Required]
		public int? SHILISL { get; set; }
		/// <summary>
		/// 急诊标志
		/// </summary>
		public int? JIZHENBZ { get; set; }
		/// <summary>
		/// 需要更新标志
		/// </summary>
		[Required]
		public int? XUYAOGXBZ { get; set; }
		/// <summary>
		/// 库存管理类型
		/// </summary>
		[StringLength(10)]
		public string KUCUNGLLX { get; set; }
	}
}
