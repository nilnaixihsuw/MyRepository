using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_RIBAOSJY")]
	public partial class GY_RIBAOSJY : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 模板ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string MOBANID { get; set; }
		/// <summary>
		/// 数据源类型
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(4)]
		public string SHUJUYLX { get; set; }
		/// <summary>
		/// 数据源名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string SHUJUYMC { get; set; }
		/// <summary>
		/// 模板项目ID
		/// </summary>
		[Key]
		[Column(Order=3)]
		[StringLength(10)]
		public string MOBANXMID { get; set; }
		/// <summary>
		/// 数据源项目ID
		/// </summary>
		[Key]
		[Column(Order=4)]
		[StringLength(10)]
		public string SHUJUYXMID { get; set; }
	}
}
