using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.XT
{
	[Table("XT_XIAOXI_NEW")]
	public partial class XT_XIAOXI_NEW : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 消息ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long? XIAOXIID { get; set; }
		/// <summary>
		/// 消息主题
		/// </summary>
		[Required]
		[StringLength(500)]
		public string XIAOXIZT { get; set; }
		/// <summary>
		/// 消息内容
		/// </summary>
		[StringLength(4000)]
		public string XIAOXINR { get; set; }
		/// <summary>
		/// 消息类型
		/// </summary>
		[Required]
		[StringLength(4)]
		public string XIAOXILX { get; set; }
		/// <summary>
		/// 消息编码
		/// </summary>
		[StringLength(10)]
		public string XIAOXIBM { get; set; }
		/// <summary>
		/// 发送时间
		/// </summary>
		[Required]
		public DateTime? FASONGSJ { get; set; }
		/// <summary>
		/// 保密消息标志(需要用户校验才能查看)
		/// </summary>
		[Required]
		public int? BAOMIXXBZ { get; set; }
		/// <summary>
		/// 一次性标志(只要一人查看,其他收件人人就不提示了)
		/// </summary>
		[Required]
		public int? YICIXBZ { get; set; }
		/// <summary>
		/// 收件人ID,为列表,各个收件人ID用|分割
		/// </summary>
		[Required]
		[StringLength(4000)]
		public string SHOUJIANRID { get; set; }
		/// <summary>
		/// 收件人姓名
		/// </summary>
		[Required]
		[StringLength(4000)]
		public string SHOUJIANRXM { get; set; }
		/// <summary>
		/// 抄送人ID,为列表,各个收件人ID用|分割
		/// </summary>
		[StringLength(4000)]
		public string CHAOSONGRXM { get; set; }
		/// <summary>
		/// 抄送人姓名
		/// </summary>
		[StringLength(4000)]
		public string CHAOSONGRID { get; set; }
		/// <summary>
		/// 附件标志
		/// </summary>
		[Required]
		public int? FUJIANBZ { get; set; }
		/// <summary>
		/// 优先级别(1.低  2.中 3.高)
		/// </summary>
		[Required]
		public int? YOUXIANJB { get; set; }
		/// <summary>
		/// 回执标志,标志是否在月度邮件后,发送一个回执给发件人
		/// </summary>
		[Required]
		public int? HUIZHIBZ { get; set; }
		/// <summary>
		/// 发件人ID
		/// </summary>
		[StringLength(20)]
		public string FAJIANRID { get; set; }
		/// <summary>
		/// 发件人姓名
		/// </summary>
		[StringLength(100)]
		public string FAJIANRXM { get; set; }
		/// <summary>
		/// 阅读标志
		/// </summary>
		[Required]
		public int? YUEDUBZ { get; set; }
		/// <summary>
		/// 删除标志
		/// </summary>
		[Required]
		public int? SHANCHUBZ { get; set; }
		/// <summary>
		/// 删除时间
		/// </summary>
		public DateTime? SHANCHUSJ { get; set; }
		/// <summary>
		/// 有效期
		/// </summary>
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime? YOUXIAOQI { get; set; }
		/// <summary>
		/// 消息提醒类型(1.右下角浮动框 2.弹出消息 )
		/// </summary>
		[Required]
		[StringLength(2)]
		public string XIAOXITXLX { get; set; }
		/// <summary>
		/// 角色权限(限制能够查阅该消息的角色范围,每个角色用|分割,为空标识全部)
		/// </summary>
		[StringLength(100)]
		public string JUESEQX { get; set; }
		/// <summary>
		/// 应用权限(限制能够查阅该消息的应用范围,每个应用代码用|分割,为空标识全部)
		/// </summary>
		[StringLength(100)]
		public string YINGYONGQX { get; set; }
		/// <summary>
		/// 自定义参数(用于存放自定义消息调用显示时的入参)
		/// </summary>
		[StringLength(500)]
		public string ZIDINGYCS { get; set; }
		private int? _CHULIBZ;
		/// <summary>
		/// 处理标志(0,未处理 1.已处理)
		/// </summary>
		public int? CHULIBZ { get{ return _CHULIBZ; } set{ _CHULIBZ = value; } }
		/// <summary>
		/// 处理条件(配合xt_xiaoxbm.chulitjsq,用于保存检索sql参数)
		/// </summary>
		[StringLength(500)]
		public string CHULITJ { get; set; }
		/// <summary>
		/// 门诊住院标志(0,门诊 1住院)
		/// </summary>
		public int? MENZHENZYBZ { get; set; }
		/// <summary>
		/// 病人标志(门诊时为jiuzhenid,住院时为bingrenzyid)
		/// </summary>
		[StringLength(100)]
		public string BINGRENID { get; set; }
		/// <summary>
		/// 消息摘要
		/// </summary>
		[StringLength(2000)]
		public string XIAOXIZY { get; set; }
		/// <summary>
		/// 收件人
		/// </summary>
		[StringLength(4000)]
		public string SHOUJIANREN { get; set; }
		/// <summary>
		/// 消息名称
		/// </summary>
		[StringLength(100)]
		public string XIAOXIMC { get; set; }
		/// <summary>
		/// 消息简称
		/// </summary>
		[StringLength(10)]
		public string XIAOXIJC { get; set; }
        /// <summary>
		/// 消息来源
		/// </summary>
		[StringLength(50)]
        public string XIAOXILY { get; set; }
        /// <summary>
        ///默认值方法 
        /// </summary>
        public void SetDefaultValue () 
		{
			DateTime? YOUXIAOQI;
			int? CHULIBZ = 0;
		}
	}
}
