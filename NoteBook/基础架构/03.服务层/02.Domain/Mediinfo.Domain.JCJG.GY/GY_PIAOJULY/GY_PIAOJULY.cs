using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_PIAOJULY")]
	public partial class GY_PIAOJULY : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 票据领用ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string PIAOJULYID { get; set; }
		/// <summary>
		/// 票据ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string PIAOJUID { get; set; }
		/// <summary>
		/// 票据类型
		/// </summary>
		[StringLength(4)]
		public string PIAOJULX { get; set; }
		/// <summary>
		/// 领用人
		/// </summary>
		[Required]
		[StringLength(10)]
		public string LINGYONGREN { get; set; }
		/// <summary>
		/// 领用日期
		/// </summary>
		[Required]
		public DateTime? LINGYONGRQ { get; set; }
		/// <summary>
		/// 使用人
		/// </summary>
		[Required]
		[StringLength(50)]
		public string SHIYONGREN { get; set; }
		/// <summary>
		/// 票据状态
		/// </summary>
		[Required]
		[StringLength(4)]
		public string PIAOJUZT { get; set; }
		/// <summary>
		/// 启用日期
		/// </summary>
		public DateTime? QIYONGRQ { get; set; }
		/// <summary>
		/// 停用日期
		/// </summary>
		public DateTime? TINGYONGRQ { get; set; }
		/// <summary>
		/// 开始号码
		/// </summary>
		[Required]
		[StringLength(20)]
		public string KAISHIHM { get; set; }
		/// <summary>
		/// 结束号码
		/// </summary>
		[Required]
		[StringLength(20)]
		public string JIESHUHM { get; set; }
		/// <summary>
		/// 当前号码
		/// </summary>
		[Required]
		[StringLength(20)]
		public string DANGQIANHM { get; set; }
		/// <summary>
		/// 门诊使用
		/// </summary>
		public int? MENZHENSY { get; set; }
		/// <summary>
		/// 住院使用
		/// </summary>
		public int? ZHUYUANSY { get; set; }
		/// <summary>
		/// 领用人姓名
		/// </summary>
		[Required]
		[StringLength(20)]
		public string LINGYONGRXM { get; set; }
		/// <summary>
		/// 使用人姓名
		/// </summary>
		[Required]
		[StringLength(100)]
		public string SHIYONGRXM { get; set; }
		private string _SHIYONGFW;
		/// <summary>
		/// 使用范围
		/// </summary>
		[StringLength(4)]
		public string SHIYONGFW { get{ return _SHIYONGFW; } set{ _SHIYONGFW = value ?? "1"; } }
		/// <summary>
		/// 
		/// </summary>
		public long? YINGJISFBZ { get; set; }
		/// <summary>
		/// 使用组
		/// </summary>
		[StringLength(10)]
		public string SHIYONGZU { get; set; }
		/// <summary>
		/// 使用组名称
		/// </summary>
		[StringLength(50)]
		public string SHIYONGZMC { get; set; }
		/// <summary>
		/// 票据入库ID ：HR3-11391(153045)
		/// </summary>
		[StringLength(20)]
		public string PIAOJURKID { get; set; }
		/// <summary>
		/// 原开始号码
		/// </summary>
		[StringLength(20)]
		public string YUANKSHM { get; set; }
		/// <summary>
		/// 原结束号码
		/// </summary>
		[StringLength(20)]
		public string YUANJSHM { get; set; }
		/// <summary>
		/// 原当前号码
		/// </summary>
		[StringLength(20)]
		public string YUANDANGQHM { get; set; }
		/// <summary>
		/// 数量
		/// </summary>
		public long? SHULIANG { get; set; }
		/// <summary>
		/// 工作站ID HR3-28445
		/// </summary>
		[StringLength(10)]
		public string GONGZUOZID { get; set; }
	}
}
