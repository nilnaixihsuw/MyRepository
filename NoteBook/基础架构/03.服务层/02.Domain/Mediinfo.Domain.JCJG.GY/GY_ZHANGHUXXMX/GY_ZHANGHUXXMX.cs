using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_ZHANGHUXXMX")]
	public partial class GY_ZHANGHUXXMX : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 账户信息明细ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string ZHANGHUXXMXID { get; set; }
		/// <summary>
		/// 院区ID
		/// </summary>
		[StringLength(1)]
		public string YUANQUID { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 账户ID
		/// </summary>
		[StringLength(4)]
		public string ZHANGHUID { get; set; }
		/// <summary>
		/// 病人ID
		/// </summary>
		[StringLength(100)]
		public string BINGRENID { get; set; }
		/// <summary>
		/// 状态1:正常;2:停用;3:挂失
		/// </summary>
		[StringLength(4)]
		public string ZHUANGTAI { get; set; }
		/// <summary>
		/// 医院流水号
		/// </summary>
		[StringLength(100)]
		public string YIYUANLSH { get; set; }
		/// <summary>
		/// 账户流水号
		/// </summary>
		[StringLength(100)]
		public string ZHANGHULSH { get; set; }
		/// <summary>
		/// 操作时间
		/// </summary>
		public DateTime? CAOZUOSJ { get; set; }
		/// <summary>
		/// 操作员
		/// </summary>
		[StringLength(10)]
		public string CAOZUOYUAN { get; set; }
		/// <summary>
		/// 字符1
		/// </summary>
		[StringLength(100)]
		public string ZIFU1 { get; set; }
		/// <summary>
		/// 字符2
		/// </summary>
		[StringLength(100)]
		public string ZIFU2 { get; set; }
		/// <summary>
		/// 字符3
		/// </summary>
		[StringLength(100)]
		public string ZIFU3 { get; set; }
		/// <summary>
		/// 字符4
		/// </summary>
		[StringLength(100)]
		public string ZIFU4 { get; set; }
		/// <summary>
		/// 字符5
		/// </summary>
		[StringLength(100)]
		public string ZIFU5 { get; set; }
		/// <summary>
		/// 字符6
		/// </summary>
		[StringLength(100)]
		public string ZIFU6 { get; set; }
		/// <summary>
		/// 字符7
		/// </summary>
		[StringLength(100)]
		public string ZIFU7 { get; set; }
		/// <summary>
		/// 字符8
		/// </summary>
		[StringLength(100)]
		public string ZIFU8 { get; set; }
		/// <summary>
		/// 字符9
		/// </summary>
		[StringLength(100)]
		public string ZIFU9 { get; set; }
		/// <summary>
		/// 交易入参
		/// </summary>
		[StringLength(2000)]
		public string JIAOYIRC { get; set; }
		/// <summary>
		/// 交易出参
		/// </summary>
		[StringLength(2000)]
		public string JIAOYICC { get; set; }
	}
}
