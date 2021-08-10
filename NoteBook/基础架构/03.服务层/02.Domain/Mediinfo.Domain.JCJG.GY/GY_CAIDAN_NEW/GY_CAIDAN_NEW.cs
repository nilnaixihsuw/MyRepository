using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
    [Table("GY_CAIDAN_NEW")]
    public partial class GY_CAIDAN_NEW : EntityBase, IEntityMapper
    {
        /// <summary>
        /// 行ID
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string CAIDANROWID { get; set; }
        /// <summary>
        /// 应用ID
        /// </summary>
        [Required]
        [StringLength(4)]
        public string YINGYONGID { get; set; }
        /// <summary>
        /// 菜单ID
        /// </summary>
        [Required]
        [StringLength(20)]
        public string CAIDANID { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        [StringLength(100)]
        public string CAIDANMC { get; set; }
        /// <summary>
        /// 菜单描述
        /// </summary>
        [StringLength(100)]
        public string CAIDANMS { get; set; }
        /// <summary>
        /// 上级菜单ID
        /// </summary>
        [StringLength(20)]
        public string SHANGJICDID { get; set; }
        /// <summary>
        /// 顺序号
        /// </summary>
        [StringLength(10)]
        public string SHUNXUHAO { get; set; }
        /// <summary>
        /// 调用参数(第一位调用窗口，第二位功能ID，第三位参数，第四位打开方式)
        /// </summary>
        [StringLength(1000)]
        public string DIAOYONGCS { get; set; }
        /// <summary>
        /// 图片名称
        /// </summary>
        [StringLength(100)]
        public string TUPIANMC { get; set; }
        /// <summary>
        /// 控件名称
        /// </summary>
        [StringLength(100)]
        public string KONGJIANMC { get; set; }
        /// <summary>
        /// 快捷键
        /// </summary>
        [StringLength(10)]
        public string KUAIJIEJIAN { get; set; }
        /// <summary>
        /// 作废标志
        /// </summary>
        [Required]
        public int? ZUOFEIBZ { get; set; }
        /// <summary>
        /// 功能ID
        /// </summary>
        [StringLength(20)]
        public string GONGNENGID { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [Required]
        [StringLength(10)]
        public string XIUGAIREN { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? XIUGAISJ { get; set; }
     
        /// <summary>
        /// 0-未打开，1-打开
        /// </summary>
        [Required]
        public int? ISOPEN { get; set; }
        /// <summary>
		/// 启用标志
		/// </summary>
		public int? QIYONGBZ { get; set; }
        /// <summary>
        ///默认值方法 
        /// </summary>
        [Ignore]
        public override void SetDefaultValue()
        {
            if (ZUOFEIBZ.IsNullOrDBNull())
                ZUOFEIBZ = 0;
            if (ISOPEN.IsNullOrDBNull())
                ISOPEN = 0;
        }
    }
}
