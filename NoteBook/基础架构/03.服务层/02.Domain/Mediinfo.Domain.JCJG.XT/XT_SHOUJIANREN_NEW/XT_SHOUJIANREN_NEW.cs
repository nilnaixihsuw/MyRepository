using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.XT
{
    [Table("XT_SHOUJIANREN_NEW")]
    public partial class XT_SHOUJIANREN_NEW : EntityBase, IEntityMapper
    {
        /// <summary>
        /// 收件人ID
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [StringLength(12)]
        public string SHOUJIANRID { get; set; }
        /// <summary>
        /// 职工工号
        /// </summary>
        [Required]
        [StringLength(10)]
        public string ZHIGONGGH { get; set; }
        /// <summary>
        /// 收件人姓名
        /// </summary>
        [Required]
        [StringLength(100)]
        public string SHOUJIANRXM { get; set; }
        /// <summary>
        /// 输入码1
        /// </summary>
        [StringLength(10)]
        public string SHURUMA1 { get; set; }
        /// <summary>
        /// 输入码2
        /// </summary>
        [StringLength(10)]
        public string SHURUMA2 { get; set; }
        /// <summary>
        /// 输入码3
        /// </summary>
        [StringLength(10)]
        public string SHURUMA3 { get; set; }
        /// <summary>
        /// 作废标识
        /// </summary>
        public int ZUOFEIBZ { get; set; }
        /// <summary>
        ///默认值方法 
        /// </summary>
        public void SetDefaultValue()
        { }
    }
}
