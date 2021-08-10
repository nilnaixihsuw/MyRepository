using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_ZHANGHULB")]
	public partial class GY_ZHANGHULB : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 账户ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(4)]
		public string ZHANGHUID { get; set; }
		/// <summary>
		/// 账户名称
		/// </summary>
		[StringLength(100)]
		public string ZHANGHUMC { get; set; }
		/// <summary>
		/// 机构编号
		/// </summary>
		[StringLength(20)]
		public string JIGOUBH { get; set; }
		/// <summary>
		/// 机构名称
		/// </summary>
		[StringLength(100)]
		public string JIGOUMC { get; set; }
		/// <summary>
		/// 顺序号
		/// </summary>
		public int? SHUNXUHAO { get; set; }
		/// <summary>
		/// 对象名称
		/// </summary>
		[StringLength(50)]
		public string DUIXIANGMC { get; set; }
		/// <summary>
		/// 性质属性
		/// </summary>
		[StringLength(100)]
		public string XINGZHISX { get; set; }
		/// <summary>
		/// 启用标志
		/// </summary>
		public int? QIYONGBZ { get; set; }
		/// <summary>
		/// 健康卡启用标志
		/// </summary>
		public int? JIANKANGKQYBZ { get; set; }
		/// <summary>
		/// 健康卡名称
		/// </summary>
		[StringLength(100)]
		public string JIANKANGKMC { get; set; }
        /// <summary>
		/// 图片名称
		/// </summary>
		[StringLength(100)]
        public string TUPIANMC { get; set; }
        /// <summary>
		/// 支付方式(该账户对应的支付方式)
		/// </summary>
		[StringLength(10)]
        public string ZHIFUFS { get; set; }
        /// <summary>
		/// 费用类别串(支持结算的费用类别串，空则全部支持)
		/// </summary>
		[StringLength(100)]
        public string FEIYONGLBS { get; set; }
    }
}
