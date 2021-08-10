using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_ZHIGONGKS")]
	public partial class GY_ZHIGONGKS : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 职工ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string ZHIGONGID { get; set; }
		/// <summary>
		/// 科室病区ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string KESHIBQID { get; set; }
		/// <summary>
		/// 科室病区标志
		/// </summary>
		[Key]
		[Column(Order=3)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int? KESHIBQBZ { get; set; }
		/// <summary>
		/// 顺序号
		/// </summary>
		public int? SHUNXUHAO { get; set; }
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
		/// <summary>
		/// 科室病区名称
		/// </summary>
		[StringLength(100)]
		public string KESHIBQMC { get; set; }
	}
}
