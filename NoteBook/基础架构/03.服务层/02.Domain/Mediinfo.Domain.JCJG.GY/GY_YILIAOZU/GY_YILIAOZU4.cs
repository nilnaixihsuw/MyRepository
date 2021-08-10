using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YILIAOZU4")]
	public partial class GY_YILIAOZU4 : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 医疗组ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string YILIAOZID { get; set; }
		/// <summary>
		/// 科室ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string KESHIID { get; set; }
		/// <summary>
		/// 修改人
		/// </summary>
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? XIUGAISJ { get; set; }
		/// <summary>
		/// 默认标志
		/// </summary>
		public int? MORENBZ { get; set; }
		/// <summary>
		/// 排序号HR3-23827(262066)
		/// </summary>
		[StringLength(10)]
		public string PAIXUHAO { get; set; }
	}
}
