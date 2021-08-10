using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_WEIZHI")]
	public partial class GY_WEIZHI : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 位置ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string WEIZHIID { get; set; }
		/// <summary>
		/// 位置名称
		/// </summary>
		[StringLength(100)]
		public string WEIZHIMC { get; set; }
		/// <summary>
		/// 位置类别
		/// </summary>
		[StringLength(4)]
		public string WEIZHILB { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		public int? ZUOFEIBZ { get; set; }
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
		/// 输入码1
		/// </summary>
		[StringLength(10)]
		public string SHURUMA1 { get; set; }
		/// <summary>
		/// 输入码2
		/// </summary>
		[StringLength(10)]
		public string SHURUMA2 { get; set; }
		/// <summary>
		/// 输入码3
		/// </summary>
		[StringLength(10)]
		public string SHURUMA3 { get; set; }
		/// <summary>
		/// 别名
		/// </summary>
		[StringLength(20)]
		public string BIEMING { get; set; }
		/// <summary>
		/// 科室ID
		/// </summary>
		[StringLength(100)]
		public string KESHIID { get; set; }
		/// <summary>
		/// 英文名称
		/// </summary>
		[StringLength(100)]
		public string YINGWENMC { get; set; }
		/// <summary>
		/// 分诊台
		/// </summary>
		[StringLength(10)]
		public string FENZHENTAI { get; set; }
	}
}
