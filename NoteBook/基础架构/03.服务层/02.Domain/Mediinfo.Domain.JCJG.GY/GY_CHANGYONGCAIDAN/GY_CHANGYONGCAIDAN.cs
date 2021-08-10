using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_CHANGYONGCAIDAN")]
	public partial class GY_CHANGYONGCAIDAN : EntityBase, IEntityMapper
	{
        /// <summary>
        /// 常用菜单ID
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? CHANGYONGCAIDANID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        [StringLength(10)]
		public string YONGHUID { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 菜单ID
		/// </summary>
		[StringLength(20)]
		public string CAIDANID { get; set; }
		/// <summary>
		/// 菜单名称
		/// </summary>
		[StringLength(100)]
		public string CAIDANMC { get; set; }
		private int? _ISCHANGYONG;
		/// <summary>
		/// 是否是常用菜单
		/// </summary>
		public int? ISCHANGYONG { get; set; }
		/// <summary>
		/// 排序
		/// </summary>
		public int? PAIXU { get; set; }
        /// <summary>
		/// 是否是全局常用菜单
		/// </summary>
		public int? ISQUANJVCY { get; set; }
        /// <summary>
        ///默认值方法 
        /// </summary>
        [Ignore]
		public override void SetDefaultValue () 
		{
			if (ISCHANGYONG.IsNullOrDBNull())
			ISCHANGYONG = 0;
            if (ISQUANJVCY.IsNullOrDBNull())
                ISQUANJVCY = 0;
        }
	}
}
