using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.XT
{
	[Table("XT_XIAOXIFJNR")]
	public partial class XT_XIAOXIFJNR : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 附件ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long? FUJIANID { get; set; }
		/// <summary>
		/// 顺序号
		/// </summary>
		[Key]
		[Column(Order=2)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int? SHUNXUHAO { get; set; }
		/// <summary>
		/// 附件内容
		/// </summary>
		[StringLength(4000)]
		public string FUJIANNR { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		public void SetDefaultValue () 
		{}
		}
	}
