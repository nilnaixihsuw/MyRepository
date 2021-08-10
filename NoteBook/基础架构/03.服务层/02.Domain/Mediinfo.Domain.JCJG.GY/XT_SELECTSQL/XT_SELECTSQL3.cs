using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("XT_SELECTSQL3")]
	public partial class XT_SELECTSQL3 : EntityBase, IEntityMapper
	{
		/// <summary>
		/// SQLID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(50)]
		public string SQLID { get; set; }
		/// <summary>
		/// 系统ID
		/// </summary>
		[StringLength(2)]
		public string XITONGID { get; set; }
		/// <summary>
		/// 类别
		/// </summary>
		[StringLength(4)]
		public string LEIBIE { get; set; }
		/// <summary>
		/// SQL
		/// </summary>
		[Required]
		[StringLength(1000)]
		public string SQL { get; set; }
		/// <summary>
		/// SQL2
		/// </summary>
		[StringLength(1000)]
		public string SQL2 { get; set; }
	}
}
