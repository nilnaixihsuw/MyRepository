using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_PISHIJGDZ")]
	public partial class GY_PISHIJGDZ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string PISHIJGDZID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(10)]
		public string PISHIJGID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(10)]
		public string CHULIYJID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? XIUGAISJ { get; set; }
	}
}
