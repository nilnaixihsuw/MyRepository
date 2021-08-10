using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_ZIJINZH")]
	public partial class GY_ZIJINZH : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 账户ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string ZHANGHUID { get; set; }
		/// <summary>
		/// 期初金额
		/// </summary>
		public decimal? QICHUJE { get; set; }
		/// <summary>
		/// 增加金额
		/// </summary>
		public decimal? ZENGJIAJE { get; set; }
		/// <summary>
		/// 减少金额
		/// </summary>
		public decimal? JIANSHAOJE { get; set; }
		/// <summary>
		/// 期末金额
		/// </summary>
		public decimal? QIMOJE { get; set; }
		/// <summary>
		/// 个人单位标志
		/// </summary>
		public int? GERENDWBZ { get; set; }
		/// <summary>
		/// 现金券标志
		/// </summary>
		public int? XIANJINQBZ { get; set; }
		/// <summary>
		/// 账户等级
		/// </summary>
		[StringLength(4)]
		public string ZHANGHUDJ { get; set; }
		/// <summary>
		/// 介质号
		/// </summary>
		[StringLength(100)]
		public string JIEZHIHAO { get; set; }
		/// <summary>
		/// 查询密码
		/// </summary>
		[StringLength(50)]
		public string CHAXUNMM { get; set; }
		/// <summary>
		/// 交易密码
		/// </summary>
		[StringLength(50)]
		public string JIAOYIMM { get; set; }
		/// <summary>
		/// 单位ID
		/// </summary>
		[StringLength(10)]
		public string DANWEIID { get; set; }
		/// <summary>
		/// 账户名称
		/// </summary>
		[StringLength(100)]
		public string ZHANGHUMC { get; set; }
		/// <summary>
		/// 账户状态
		/// </summary>
		public int? ZHANGHUZT { get; set; }
		/// <summary>
		/// 病人ID
		/// </summary>
		[StringLength(100)]
		public string BINGRENID { get; set; }
		/// <summary>
		/// 职工ID
		/// </summary>
		[StringLength(10)]
		public string ZHIGONGID { get; set; }
		/// <summary>
		/// 职工标志
		/// </summary>
		public int? ZHIGONGBZ { get; set; }
		/// <summary>
		/// 未确认补贴
		/// </summary>
		public decimal? WEIQUERBT { get; set; }
		/// <summary>
		/// 未结算消费
		/// </summary>
		public decimal? WEIJIESXF { get; set; }
		private int? _QIANFEIZT;
		/// <summary>
		/// 欠费标志
		/// </summary>
		public int? QIANFEIZT { get{ return _QIANFEIZT; } set{ _QIANFEIZT = value ?? 0; } }
		/// <summary>
		/// 欠费帐户计划偿还日期
		/// </summary>
		public DateTime? CHANGHUANRQ { get; set; }
		/// <summary>
		/// 密码初始化人HR3-34000(327308)
		/// </summary>
		[StringLength(10)]
		public string MIMACSHR { get; set; }
		/// <summary>
		/// 密码初始化时间HR3-34000(327308)
		/// </summary>
		public DateTime? MIMACSHSJ { get; set; }
		/// <summary>
		/// 不可取现标志HR3-36823(342138)
		/// </summary>
		public int? BUKEQXBZ { get; set; }
	}
}
