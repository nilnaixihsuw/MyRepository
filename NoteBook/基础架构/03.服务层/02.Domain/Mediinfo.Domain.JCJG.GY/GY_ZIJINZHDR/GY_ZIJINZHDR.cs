using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_ZIJINZHDR")]
	public partial class GY_ZIJINZHDR : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 介质号
		/// </summary>
		[Required]
		[StringLength(100)]
		public string JIEZHIHAO { get; set; }
		/// <summary>
		/// 充值金额
		/// </summary>
		public decimal? CHONGZHIJE { get; set; }
		/// <summary>
		/// 操作员
		/// </summary>
		[StringLength(10)]
		public string CAOZUOYUAN { get; set; }
		/// <summary>
		/// 操作时间
		/// </summary>
		public DateTime? CAOZUOSJ { get; set; }
		private int? _ZHUANGTAI;
		/// <summary>
		/// 状态0：未导入；1：已导入
		/// </summary>
		public int? ZHUANGTAI { get{ return _ZHUANGTAI; } set{ _ZHUANGTAI = value; } }
		/// <summary>
		/// 资金账户导入ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string ZIJINZHDRID { get; set; }
		/// <summary>
		/// 预交款ID
		/// </summary>
		[StringLength(10)]
		public string YUJIAOKID { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		public override void SetDefaultValue () 
		{
			ZHUANGTAI = 0;
		}
	}
}
