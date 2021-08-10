using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JIEZHI")]
	public partial class GY_JIEZHI : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 介质ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JIEZHIID { get; set; }
		/// <summary>
		/// 介质号
		/// </summary>
		[Required]
		[StringLength(100)]
		public string JIEZHIHAO { get; set; }
		/// <summary>
		/// 类型
		/// </summary>
		[Required]
		[StringLength(4)]
		public string LEIXING { get; set; }
		/// <summary>
		/// 病人ID
		/// </summary>
		[Required]
		[StringLength(100)]
		public string BINGRENID { get; set; }
		/// <summary>
		/// 制卡日期
		/// </summary>
		public DateTime? ZHIKARQ { get; set; }
		/// <summary>
		/// 有效日期
		/// </summary>
		public DateTime? YOUXIAORQ { get; set; }
		private int? _ZUOFEIBZ;
		/// <summary>
		/// 作废标志(磁卡模式)
		/// </summary>
		public int? ZUOFEIBZ { get{ return _ZUOFEIBZ; } set{ _ZUOFEIBZ = value ?? 0; } }
		private string _JIEZHIZT;
		/// <summary>
		/// 介质状态(IC卡模式)
		/// </summary>
		[StringLength(4)]
		public string JIEZHIZT { get{ return _JIEZHIZT; } set{ _JIEZHIZT = value ?? "0"; } }
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? XIUGAISJ { get; set; }
		/// <summary>
		/// 修改人
		/// </summary>
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 业务类型 1,新增;2,换卡
		/// </summary>
		public int? YEWULX { get; set; }
		/// <summary>
		/// 制卡人
		/// </summary>
		[StringLength(50)]
		public string ZHIKAREN { get; set; }
		/// <summary>
		/// 合并标志HR3-31743
		/// </summary>
		[StringLength(4)]
		public string HEBINGBZ { get; set; }
		/// <summary>
		/// 费用类别HR3-31743
		/// </summary>
		[StringLength(10)]
		public string FEIYONGLB { get; set; }
		/// <summary>
		/// 费用性质HR3-31743
		/// </summary>
		[StringLength(10)]
		public string FEIYONGXZ { get; set; }
		/// <summary>
		/// 原介质IDR3-31743
		/// </summary>
		[StringLength(10)]
		public string YUANJIEZID { get; set; }
		/// <summary>
		/// 作废时间IDR3-31743
		/// </summary>
		public DateTime? ZUOFEISJ { get; set; }
		/// <summary>
		/// 作废人IDR3-31743
		/// </summary>
		[StringLength(10)]
		public string ZUOFEIREN { get; set; }
		/// <summary>
		/// 健康卡号HB3-21969
		/// </summary>
		[StringLength(100)]
		public string JIANKANGKH { get; set; }
	}
}
