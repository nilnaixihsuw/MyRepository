using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YINGYONGDY")]
	public partial class GY_YINGYONGDY : EntityBase, IEntityMapper
	{
        /// <summary>
        /// 应用ID1
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [StringLength(4)]
		public string YINGYONGID1 { get; set; }
        /// <summary>
        /// 应用ID2
        /// </summary>
        [Key]
        [Column(Order = 2)]
        [StringLength(4)]
		public string YINGYONGID2 { get; set; }
		/// <summary>
		/// 对应类型
		/// </summary>
		[Required]
		[StringLength(4)]
		public string DUIYINGLX { get; set; }
		/// <summary>
		/// 项目类型
		/// </summary>
		[StringLength(4)]
		public string XIANGMULX { get; set; }
		/// <summary>
		/// 默认标志
		/// </summary>
		[Required]
		public int? MORENBZ { get; set; }
		/// <summary>
		/// 门诊住院标志
		/// </summary>
		public int? MENZHENZYBZ { get; set; }
		/// <summary>
		/// 顺序号
		/// </summary>
		public int? SHUNXUHAO { get; set; }
		/// <summary>
		/// 使用标志
		/// </summary>
		public int? SHIYONGBZ { get; set; }
		/// <summary>
		/// 院区ID HR3-14704
		/// </summary>
		[StringLength(1)]
		public string YUANQUID { get; set; }
	}
}
