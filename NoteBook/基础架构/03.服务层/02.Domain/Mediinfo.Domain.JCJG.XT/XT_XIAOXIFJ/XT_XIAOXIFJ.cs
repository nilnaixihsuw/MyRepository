using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.XT
{
	[Table("XT_XIAOXIFJ")]
	public partial class XT_XIAOXIFJ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 消息ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long XIAOXIID { get; set; }
		/// <summary>
		/// 附件ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long FUJIANID { get; set; }
		/// <summary>
		/// 附件类型
		/// </summary>
		[Required]
		[StringLength(4)]
		public string FUJIANLX { get; set; }
		/// <summary>
		/// 附件名称
		/// </summary>
		[StringLength(50)]
		public string FUJIANMC { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		public void SetDefaultValue () 
		{}
		}
	}
