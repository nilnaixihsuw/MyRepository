using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
using Mediinfo.Infrastructure.Core.Entity;

namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_CANSHU")]
	public partial class GY_CANSHU : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 应用ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 参数ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(50)]
		public string CANSHUID { get; set; }
		/// <summary>
		/// 参数描述
		/// </summary>
		[StringLength(500)]
		public string CANSHUMS { get; set; }
		/// <summary>
		/// 参数值
		/// </summary>
		[Required]
		[StringLength(500)]
		public string CANSHUZHI { get; set; }
		/// <summary>
		/// 缺省值
		/// </summary>
		[Required]
		[StringLength(500)]
		public string QUESHENGZHI { get; set; }
		/// <summary>
		/// 相关说明
		/// </summary>
		[StringLength(500)]
		public string XIANGGUANSM { get; set; }
		/// <summary>
		/// 加载方式
		/// </summary>
		[StringLength(4)]
		public string JIAZAIFS { get; set; }
		/// <summary>
		/// 系统标志
		/// </summary>
		[Required]
		public int? XITONGBZ { get; set; }
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
		/// 修改用户名
		/// </summary>
		[StringLength(30)]
		public string XIUGAIYHM { get; set; }
		/// <summary>
		/// 修改主机
		/// </summary>
		[StringLength(64)]
		public string XIUGAIZJ { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? XIUGAISJ1 { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(50)]
		public string SHIYONGXT { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(50)]
		public string CANSHUFZ { get; set; }
	}
}
