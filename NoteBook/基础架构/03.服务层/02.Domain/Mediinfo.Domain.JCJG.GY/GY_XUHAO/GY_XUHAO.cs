using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_XUHAO")]
	public partial class GY_XUHAO : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 序号名称
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(20)]
		public string XUHAOMC { get; set; }
		/// <summary>
		/// 表名
		/// </summary>
		[StringLength(100)]
		public string BIAOMING { get; set; }
		/// <summary>
		/// 列名
		/// </summary>
		[StringLength(100)]
		public string LIEMING { get; set; }
		/// <summary>
		/// 当前值
		/// </summary>
		[Required]
		public long? DANGQIANZHI { get; set; }
		/// <summary>
		/// 最小值
		/// </summary>
		public long? ZUIXIAOZHI { get; set; }
		/// <summary>
		/// 最大值
		/// </summary>
		public long? ZUIDAZHI { get; set; }
		/// <summary>
		/// 长度
		/// </summary>
		public int? CHANGDU { get; set; }
		/// <summary>
		/// 组合方式
		/// </summary>
		[StringLength(4)]
		public string ZUHEFS { get; set; }
		/// <summary>
		/// 序列名称
		/// </summary>
		[StringLength(50)]
		public string XULIEMC { get; set; }
	}
}
