using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YAOPINJGDWDZ")]
	public partial class GY_YAOPINJGDWDZ : EntityBase, IEntityMapper
	{
        /// <summary>
        /// 价格ID
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
		public string JIAGEID { get; set; }
        /// <summary>
        /// 单位ID
        /// </summary>
        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
		public string DANWEIID { get; set; }
        /// <summary>
        /// 应用ID
        /// </summary>
        [Key]
        [Column(Order = 3)]
        [StringLength(4)]
		public string YINGYONGID { get; set; }
	}
}
