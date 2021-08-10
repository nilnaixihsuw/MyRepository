using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_CHUANGWEIZU2")]
	public partial class GY_CHUANGWEIZU2 : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 床位组ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string CHUANGWEIZUID { get; set; }
		/// <summary>
		/// 床位ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string CHUANGWEIID { get; set; }
		/// <summary>
		/// 修改人
		/// </summary>
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? XIUGAISJ { get; set; }
	}
}
