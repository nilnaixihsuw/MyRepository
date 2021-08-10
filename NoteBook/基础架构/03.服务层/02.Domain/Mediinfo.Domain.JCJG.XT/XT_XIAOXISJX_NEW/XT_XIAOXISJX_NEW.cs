using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.XT
{
	[Table("XT_XIAOXISJX_NEW")]
	public partial class XT_XIAOXISJX_NEW : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 消息ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long? XIAOXIID { get; set; }
		/// <summary>
		/// 收件人ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(20)]
		public string SHOUJIANRID { get; set; }
		/// <summary>
		/// 收件人姓名
		/// </summary>
		[StringLength(100)]
		public string SHOUJIANRXM { get; set; }
		/// <summary>
		/// 阅读标志
		/// </summary>
		public int? YUEDUBZ { get; set; }
		/// <summary>
		/// 阅读时间
		/// </summary>
		public DateTime? YUEDUSJ { get; set; }
		/// <summary>
		/// 删除标志
		/// </summary>
		public int? SHANCHUBZ { get; set; }
		/// <summary>
		/// 删除时间
		/// </summary>
		public DateTime? SHANCHUSJ { get; set; }
		/// <summary>
		/// 回复标志
		/// </summary>
		public int? HUIFUBZ { get; set; }
		/// <summary>
		/// 回复时间
		/// </summary>
		public DateTime? HUIFUSJ { get; set; }
		/// <summary>
		/// 后续标志
		/// </summary>
		public int? HOUXUBZ { get; set; }
		/// <summary>
		/// 消息状态
		/// </summary>
		public int? ZHUANGTAI { get; set; }
		/// <summary>
		/// 提醒效期(在效期内会提醒)
		/// </summary>
		//[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime? TIXINGXQ { get; set; }
        /// <summary>
		/// 接收标志
		/// </summary>
		public int? JIESHOUBZ { get; set; }
        /// <summary>
		/// 接收时间
		/// </summary>
		public DateTime? JIESHOUSJ { get; set; }
        /// <summary>
        ///默认值方法 
        /// </summary>
        public override void SetDefaultValue () 
		{
		}

        
    }
}
