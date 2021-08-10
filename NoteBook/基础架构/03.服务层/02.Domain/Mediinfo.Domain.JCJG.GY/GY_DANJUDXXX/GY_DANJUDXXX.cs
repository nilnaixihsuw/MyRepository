using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_DANJUDXXX")]
	public partial class GY_DANJUDXXX : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 单据对象ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(20)]
		public string DANJUDXID { get; set; }
		/// <summary>
		/// 单据对象名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string DANJUDXMC { get; set; }
		/// <summary>
		/// 单据对象描述
		/// </summary>
		[StringLength(300)]
		public string DANJUDXMS { get; set; }
		/// <summary>
		/// 单据对象类名
		/// </summary>
		[Required]
		[StringLength(100)]
		public string DANJUDXLM { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[Required]
		[StringLength(100)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 拼音码
		/// </summary>
		[StringLength(50)]
		public string SHURUMA1 { get; set; }
		/// <summary>
		/// 五笔码
		/// </summary>
		[StringLength(50)]
		public string SHURUMA2 { get; set; }
		/// <summary>
		/// 自定义码
		/// </summary>
		[StringLength(50)]
		public string SHURUMA3 { get; set; }
		/// <summary>
		/// 宽度
		/// </summary>
		public decimal? KUANDU { get; set; }
		/// <summary>
		/// 高度
		/// </summary>
		public decimal? GAODU { get; set; }
		/// <summary>
		/// 方向
		/// </summary>
		public int? FANGXIANG { get; set; }
		/// <summary>
		/// 左边距
		/// </summary>
		public decimal? ZUOBIANJU { get; set; }
		/// <summary>
		/// 右边距
		/// </summary>
		public decimal? YOUBIANJU { get; set; }
		/// <summary>
		/// 上边距
		/// </summary>
		public decimal? SHANGBIANJU { get; set; }
		/// <summary>
		/// 下边距
		/// </summary>
		public decimal? XIABIANJU { get; set; }
		/// <summary>
		/// 打印机
		/// </summary>
		[StringLength(50)]
		public string DAYINJI { get; set; }
		/// <summary>
		/// 重打限制次数
		/// </summary>
		public decimal? CHONGDAXZCS { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal? DAYINCDBZCS { get; set; }
		/// <summary>
		/// 当天重打标志
		/// </summary>
		public int? DANGTIANCDBZ { get; set; }
		/// <summary>
		/// 单位
		/// </summary>
		public int? DANWEI { get; set; }
		/// <summary>
		/// 预览 0、不预览 1、预览
		/// </summary>
		public int? YULAN { get; set; }
		/// <summary>
		/// 纸张分类1系统自定义2即打即停3打印机自定义
		/// </summary>
		public long? ZHIZHANGFL { get; set; }
		/// <summary>
		/// 行间距
		/// </summary>
		public long? HANGJIANJU { get; set; }
		/// <summary>
		/// 每行页数
		/// </summary>
		public long? MEIHANGYS { get; set; }
		/// <summary>
		/// 页缝空白
		/// </summary>
		public long? YEFENGKB { get; set; }
		/// <summary>
		/// 打印范围
		/// </summary>
		[StringLength(50)]
		public string FANWEI { get; set; }
		/// <summary>
		/// 设置变量范围
		/// </summary>
		public int? BIANLIANGFW { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 本地设置标志
		/// </summary>
		public int? BENDIFW { get; set; }
		/// <summary>
		/// 修改人
		/// </summary>
		[StringLength(50)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? XIUGAISJ { get; set; }
		/// <summary>
		/// 单据ID
		/// </summary>
		[StringLength(20)]
		public string DANJUID { get; set; }
	}
}
