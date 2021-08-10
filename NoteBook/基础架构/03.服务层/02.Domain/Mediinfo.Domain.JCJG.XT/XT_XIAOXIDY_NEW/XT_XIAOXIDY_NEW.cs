using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.XT
{
	[Table("XT_XIAOXIDY_NEW")]
	public partial class XT_XIAOXIDY_NEW : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 消息编码
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string XIAOXIBM { get; set; }
		/// <summary>
		/// 收件人ID ,根据不同的前缀来区分收件人类型.其中PU,PG,PW,PD等只有前缀,不需要指定对象
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
		/// 角色权限(限制能够查阅该消息的角色范围,每个角色用'|'分割,为空标识全部)
		/// </summary>
		[StringLength(100)]
		public string JUESEQX { get; set; }
		/// <summary>
		/// 应用权限(限制能够查阅该消息的应用范围,每个应用代码用'|'分割,为空标识全部)
		/// </summary>
		[StringLength(100)]
		public string YINGYONGQX { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		public void SetDefaultValue () 
		{}
		}
	}
