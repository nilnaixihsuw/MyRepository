using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.XT
{
	[Table("XT_HANZIKU2")]
	public partial class XT_HANZIKU2 : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string HANZI { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(20)]
		public string CIZU { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(10)]
		public string SHURUMA1 { get; set; }
	}
}
