using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_GEIYAOFSJFXM")]
	public partial class GY_GEIYAOFSJFXM : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 给药方式收费ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string GEIYAOFSSFID { get; set; }
		/// <summary>
		/// 给药方式ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string GEIYAOFSID { get; set; }
		/// <summary>
		/// 收费项目
		/// </summary>
		[Required]
		[StringLength(10)]
		public string SHOUFEIXM { get; set; }
		/// <summary>
		/// 数量
		/// </summary>
		public decimal? SHULIANG { get; set; }
		/// <summary>
		/// 执行科室
		/// </summary>
		[StringLength(10)]
		public string ZHIXINGKS { get; set; }
		/// <summary>
		/// 接瓶使用
		/// </summary>
		public int? JIEPINGSY { get; set; }
		/// <summary>
		/// 适用诊疗医保代码
		/// </summary>
		[StringLength(10)]
		public string SHIYONGZLYBDM { get; set; }
		/// <summary>
		/// 适用诊疗项目名称
		/// </summary>
		[StringLength(100)]
		public string SHIYONGZLXMMC { get; set; }
		private string _MENZHENJFFS;
		/// <summary>
		/// 门诊计费方式
		/// </summary>
		[StringLength(1)]
		public string MENZHENJFFS { get{ return _MENZHENJFFS; } set{ _MENZHENJFFS = value ?? "0"; } }
		private string _ZHUYUANJFFS;
		/// <summary>
		/// 住院计费方式
		/// </summary>
		[StringLength(1)]
		public string ZHUYUANJFFS { get{ return _ZHUYUANJFFS; } set{ _ZHUYUANJFFS = value ?? "0"; } }
		/// <summary>
		/// 年龄条件HR3-12976
		/// </summary>
		[StringLength(100)]
		public string NIANLINGTJ { get; set; }
		/// <summary>
		/// 输液器类型HR3-16326(200482)
		/// </summary>
		public int? SHUYEQLX { get; set; }
		/// <summary>
		/// 接瓶计费排斥收费项目HR3-20686(239579)
		/// </summary>
		[StringLength(10)]
		public string PAICHISFXM { get; set; }
	}
}
