using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_ZHANGHUJYMX")]
	public partial class GY_ZHANGHUJYMX : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 账户信息明细ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string ZHANGHUJYMXID { get; set; }
		/// <summary>
		/// 院区ID
		/// </summary>
		[StringLength(1)]
		public string YUANQUID { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 账户ID
		/// </summary>
		[StringLength(4)]
		public string ZHANGHUID { get; set; }
		/// <summary>
		/// 病人ID
		/// </summary>
		[StringLength(100)]
		public string BINGRENID { get; set; }
		/// <summary>
		/// 业务类型,存中文充值，取现，消费，退费
		/// </summary>
		[StringLength(50)]
		public string YEWULX { get; set; }
		/// <summary>
		/// 交易金额
		/// </summary>
		public decimal? JIAOYIJE { get; set; }
		/// <summary>
		/// 账户余额
		/// </summary>
		public decimal? ZHANGHUYE { get; set; }
		/// <summary>
		/// 医院流水号
		/// </summary>
		[StringLength(100)]
		public string YIYUANLSH { get; set; }
		/// <summary>
		/// 账户流水号
		/// </summary>
		[StringLength(100)]
		public string ZHANGHULSH { get; set; }
		/// <summary>
		/// 操作时间
		/// </summary>
		public DateTime? CAOZUOSJ { get; set; }
		/// <summary>
		/// 操作员
		/// </summary>
		[StringLength(10)]
		public string CAOZUOYUAN { get; set; }
		/// <summary>
		/// 字符1
		/// </summary>
		[StringLength(100)]
		public string ZIFU1 { get; set; }
		/// <summary>
		/// 字符2
		/// </summary>
		[StringLength(100)]
		public string ZIFU2 { get; set; }
		/// <summary>
		/// 字符3
		/// </summary>
		[StringLength(100)]
		public string ZIFU3 { get; set; }
		/// <summary>
		/// 字符4
		/// </summary>
		[StringLength(100)]
		public string ZIFU4 { get; set; }
		/// <summary>
		/// 字符5
		/// </summary>
		[StringLength(100)]
		public string ZIFU5 { get; set; }
		/// <summary>
		/// 字符6
		/// </summary>
		[StringLength(100)]
		public string ZIFU6 { get; set; }
		/// <summary>
		/// 字符7
		/// </summary>
		[StringLength(100)]
		public string ZIFU7 { get; set; }
		/// <summary>
		/// 字符8
		/// </summary>
		[StringLength(100)]
		public string ZIFU8 { get; set; }
		/// <summary>
		/// 字符9
		/// </summary>
		[StringLength(100)]
		public string ZIFU9 { get; set; }
		/// <summary>
		/// 交易入参
		/// </summary>
		[StringLength(2000)]
		public string JIAOYIRC { get; set; }
		/// <summary>
		/// 交易出参
		/// </summary>
		[StringLength(2000)]
		public string JIAOYICC { get; set; }
		/// <summary>
		/// 门诊ID
		/// </summary>
		[StringLength(10)]
		public string MENZHENID { get; set; }
		/// <summary>
		/// 挂号标志
		/// </summary>
		public int? GUAHAOBZ { get; set; }
		/// <summary>
		/// MAC地址
		/// </summary>
		[StringLength(20)]
		public string MAC { get; set; }
		/// <summary>
		/// 计算机名
		/// </summary>
		[StringLength(50)]
		public string COMPUTENAME { get; set; }
		/// <summary>
		/// 退费标志
		/// </summary>
		public int? TUIFEIBZ { get; set; }
		private int? _ZUOFEIBZ;
		/// <summary>
		/// 作废标志
		/// </summary>
		public int? ZUOFEIBZ { get{ return _ZUOFEIBZ; } set{ _ZUOFEIBZ = value; } }
		private int? _QUERENBZ;
		/// <summary>
		/// 确认标志
		/// </summary>
		public int? QUERENBZ { get{ return _QUERENBZ; } set{ _QUERENBZ = value; } }
		/// <summary>
		/// 原账户交易明细ID
		/// </summary>
		[StringLength(10)]
		public string YUANZHANGHJYMXID { get; set; }
		/// <summary>
		/// 对方账户交易日期
		/// </summary>
		public DateTime? DUIFANGZHJYRQ { get; set; }
		private string _RIBAOID;
		/// <summary>
		/// 日报ID
		/// </summary>
		[StringLength(10)]
		public string RIBAOID { get{ return _RIBAOID; } set{ _RIBAOID = value; } }
		/// <summary>
		/// 日报日期
		/// </summary>
		public DateTime? RIBAORQ { get; set; }
		/// <summary>
		/// 支付方式
		/// </summary>
		[StringLength(10)]
		public string ZHIFUFS { get; set; }
		/// <summary>
		/// 收费人HR3-12834
		/// </summary>
		[StringLength(10)]
		public string SHOUFEIREN { get; set; }
		/// <summary>
		/// 对账日期 HR3-16951
		/// </summary>
		public DateTime? DUIZHANGRQ { get; set; }
		/// <summary>
		/// 病人住院IDHR3-17594(208765)
		/// </summary>
		[StringLength(10)]
		public string BINGRENZYID { get; set; }
		/// <summary>
		/// 预交款IDHR3-17594(208765)
		/// </summary>
		[StringLength(10)]
		public string YUJIAOKID { get; set; }
		/// <summary>
		/// 结算IDHR3-17594(208765)
		/// </summary>
		[StringLength(10)]
		public string JIESUANID { get; set; }
		private int? _MENZHENZYBZ;
		/// <summary>
		/// 门诊住院标志HR3-17594(208765)
		/// </summary>
		public int? MENZHENZYBZ { get{ return _MENZHENZYBZ; } set{ _MENZHENZYBZ = value; } }
		/// <summary>
		/// 诊间代扣标志
		/// </summary>
		public int? ZHENJIANDKBZ { get; set; }
		/// <summary>
		/// 原发票id
		/// </summary>
		[StringLength(10)]
		public string YUANFAPIAOID { get; set; }
		/// <summary>
		/// 原交易流水号
		/// </summary>
		[StringLength(100)]
		public string YUANJIAOYLSH { get; set; }
		/// <summary>
		/// 收费IDHR3-42628
		/// </summary>
		[StringLength(10)]
		public string SHOUFEIID { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		public void SetDefaultValue () 
		{
			int? ZUOFEIBZ = 0;
			int? QUERENBZ = 0;
			string RIBAOID = "NULL";
			int? MENZHENZYBZ = 0;
		}
	}
}
