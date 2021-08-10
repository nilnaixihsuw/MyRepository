using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_CAOZUORZ")]
	public partial class GY_CAOZUORZ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 日志ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long? RIZHIID { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 功能ID
		/// </summary>
		[StringLength(20)]
		public string GONGNENGID { get; set; }
		/// <summary>
		/// 操作信息
		/// </summary>
		[StringLength(500)]
		public string CAOZUOXX { get; set; }
		/// <summary>
		/// 操作日期
		/// </summary>
		public DateTime? CAOZUORQ { get; set; }
		/// <summary>
		/// 用户ID
		/// </summary>
		[StringLength(10)]
		public string YONGHUID { get; set; }
		/// <summary>
		/// 用户姓名
		/// </summary>
		[StringLength(20)]
		public string YONGHUXM { get; set; }
		/// <summary>
		/// IP
		/// </summary>
		[StringLength(20)]
		public string IP { get; set; }
		/// <summary>
		/// 计算机名
		/// </summary>
		[StringLength(50)]
		public string JISUANJM { get; set; }
		/// <summary>
		/// 网卡地址
		/// </summary>
		[StringLength(20)]
		public string WANGKADZ { get; set; }
		/// <summary>
		/// 版本号
		/// </summary>
		[StringLength(20)]
		public string BANBENHAO { get; set; }
		/// <summary>
		/// 日志类型
		/// </summary>
		public int? RIZHILX { get; set; }
		/// <summary>
		/// 工作站ID
		/// </summary>
		[StringLength(20)]
		public string GONGZUOZID { get; set; }
		/// <summary>
		/// 操作日期2
		/// </summary>
		public DateTime? CAOZUORQ2 { get; set; }
		/// <summary>
		/// 异常退出标志
		/// </summary>
		public int? YICHANGTCBZ { get; set; }
	}
}
