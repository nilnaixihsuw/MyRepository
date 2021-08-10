using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.XT
{
	[Table("XT_XIAOXIBM")]
	public partial class XT_XIAOXIBM : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 消息编码
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string XIAOXIBM { get; set; }
		/// <summary>
		/// 顺序号
		/// </summary>
		public int? SHUNXUHAO { get; set; }
		/// <summary>
		/// 消息名称
		/// </summary>
		[StringLength(100)]
		public string XIAOXIMC { get; set; }
		private long? _YOUXIAOSJ;
		/// <summary>
		/// 有效时间(分钟数),为0则标识无限期
		/// </summary>
		public long? YOUXIAOSJ { get{ return _YOUXIAOSJ; } set{ _YOUXIAOSJ = value; } }
		private int? _BAOMIXXBZ;
		/// <summary>
		/// 保密消息标志(需要用户校验才能查看)
		/// </summary>
		public int? BAOMIXXBZ { get{ return _BAOMIXXBZ; } set{ _BAOMIXXBZ = value; } }
		private int? _YICIXBZ;
		/// <summary>
		/// 一次性标志(只要一人查看,其他收件人人就不提示了)
		/// </summary>
		public int? YICIXBZ { get{ return _YICIXBZ; } set{ _YICIXBZ = value; } }
		/// <summary>
		/// 角色权限(限制能够查阅该消息的角色范围,每个角色用'|'分割,为空标识全部)
		/// </summary>
		[StringLength(100)]
		public string JUESEQX { get; set; }
		/// <summary>
		/// 应用权限(限制能够查阅该消息的应用范围,每个应用代码用'|'分割,为空标识全部)
		/// </summary>
		[StringLength(100)]
		public string YINGYONGQX { get; set; }
		private int? _ZUOFEIBZ;
		/// <summary>
		/// 作废标志
		/// </summary>
		public int? ZUOFEIBZ { get{ return _ZUOFEIBZ; } set{ _ZUOFEIBZ = value; } }
		/// <summary>
		/// 修改人
		/// </summary>
		[Required]
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		[Required]
		public DateTime? XIUGAISJ { get; set; }
		private int? _YOUXIANJB;
		/// <summary>
		/// 优先级别(1.低  2.中 3.高
		/// </summary>
		public int? YOUXIANJB { get{ return _YOUXIANJB; } set{ _YOUXIANJB = value; } }
		private int? _HUIZHIBZ;
		/// <summary>
		/// 回执标志,标志是否在月度邮件后,发送一个回执给发件人
		/// </summary>
		public int? HUIZHIBZ { get{ return _HUIZHIBZ; } set{ _HUIZHIBZ = value; } }
		/// <summary>
		/// 消息提醒类型(1.右下角浮动框 2.弹出消息 0.不提示 )
		/// </summary>
		[Required]
		[StringLength(2)]
		public string XIAOXITXLX { get; set; }
		/// <summary>
		/// 自定义消息标志HR3-21898
		/// </summary>
		public int? ZIDINGYXXBZ { get; set; }
		/// <summary>
		/// 自定义消息对应应用 ,应用ID用 | 分割HR3-21898
		/// </summary>
		[StringLength(500)]
		public string ZIDINGYXXDYYY { get; set; }
		/// <summary>
		/// 处理条件sql(一段sql,当该sql返回值>0自动将消息记录的chulibz=1,)
		/// </summary>
		[StringLength(500)]
		public string CHULITJSQL { get; set; }
		/// <summary>
		/// 处理消息时调用的功能ID
		/// </summary>
		[StringLength(20)]
		public string CHULIGNID { get; set; }
		/// <summary>
		/// 消息简称
		/// </summary>
		[StringLength(10)]
		public string XIAOXIJC { get; set; }
        /// <summary>
		/// 消息处理窗口
		/// </summary>
		[StringLength(100)]
        public string XIAOXICLCK { get; set; }
        /// <summary>
		/// 是否独占
		/// </summary>
        public int? ISDUZHAN { get; set; }
        /// <summary>
		/// 消息简称
		/// </summary>
		[StringLength(100)]
        public string SHENGYINTXWJLJ { get; set; }
        /// <summary>
        ///默认值方法 
        /// </summary>
        public void SetDefaultValue () 
		{
			long? YOUXIAOSJ = 0;
			int? BAOMIXXBZ = 0;
			int? YICIXBZ = 0;
			int? ZUOFEIBZ = 0;
			int? YOUXIANJB = 2;
			int? HUIZHIBZ = 0;
		}
	}
}
