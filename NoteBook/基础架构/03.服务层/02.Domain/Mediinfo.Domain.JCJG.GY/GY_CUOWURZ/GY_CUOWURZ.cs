using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_CUOWURZ")]
	public partial class GY_CUOWURZ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 日志ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long? RIZHIID { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 功能ID
		/// </summary>
		[StringLength(20)]
		public string GONGNENGID { get; set; }
		/// <summary>
		/// 发生日期
		/// </summary>
		public DateTime? FASHENGRQ { get; set; }
		/// <summary>
		/// 错误编号
		/// </summary>
		public int? CUOWUBH { get; set; }
		/// <summary>
		/// 信息
		/// </summary>
		[StringLength(500)]
		public string XINXI { get; set; }
		/// <summary>
		/// 窗口菜单
		/// </summary>
		[StringLength(50)]
		public string CHUANGKOUCD { get; set; }
		/// <summary>
		/// 对象
		/// </summary>
		[StringLength(50)]
		public string DUIXIANG { get; set; }
		/// <summary>
		/// 事件函数
		/// </summary>
		[StringLength(50)]
		public string SHIJIANHS { get; set; }
		/// <summary>
		/// 脚本行号
		/// </summary>
		public int? JIAOBENHH { get; set; }
		/// <summary>
		/// 用户ID
		/// </summary>
		[StringLength(10)]
		public string YONGHUID { get; set; }
		/// <summary>
		/// 用户姓名
		/// </summary>
		[StringLength(20)]
		public string YONGHUXM { get; set; }
		/// <summary>
		/// IP
		/// </summary>
		[StringLength(20)]
		public string IP { get; set; }
		/// <summary>
		/// 计算机名
		/// </summary>
		[StringLength(50)]
		public string JISUANJM { get; set; }
		/// <summary>
		/// 网卡地址
		/// </summary>
		[StringLength(20)]
		public string WANGKADZ { get; set; }
		/// <summary>
		/// 版本号
		/// </summary>
		[StringLength(20)]
		public string BANBENHAO { get; set; }
		/// <summary>
		/// 工作站ID
		/// </summary>
		[StringLength(20)]
		public string GONGZUOZID { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{}
		}
	}
