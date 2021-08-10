using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YAOFANGKFSJ")]
	public partial class GY_YAOFANGKFSJ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 应用
		/// </summary>
		[StringLength(2)]
		public string YINGYONG { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 开始时间
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string KAISHISJ { get; set; }
		/// <summary>
		/// 结束时间
		/// </summary>
		[Key]
		[Column(Order=3)]
		[StringLength(10)]
		public string JIESHUSJ { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? XIUGAISJ { get; set; }
		/// <summary>
		/// 修改人
		/// </summary>
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
	}
}
