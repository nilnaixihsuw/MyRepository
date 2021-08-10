using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.XT
{
	[Table("XT_SHUJUZDHC")]
	public partial class XT_SHUJUZDHC : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 缓存ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(50)]
		public string HUANCUNID { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[Required]
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// SQLID（xt_selectsql3）
		/// </summary>
		[Required]
		[StringLength(50)]
		public string SQLID { get; set; }
		/// <summary>
		/// 修改人
		/// </summary>
		[StringLength(50)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? XIUGAISJ { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		public void SetDefaultValue () 
		{}
		}
	}
