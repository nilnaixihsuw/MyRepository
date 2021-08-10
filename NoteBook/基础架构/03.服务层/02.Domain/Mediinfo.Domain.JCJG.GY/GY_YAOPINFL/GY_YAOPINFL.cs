using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YAOPINFL")]
	public partial class GY_YAOPINFL : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 药品分类ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string YAOPINFLID { get; set; }
		/// <summary>
		/// 上级分类
		/// </summary>
		[StringLength(10)]
		public string SHANGJIFL { get; set; }
		/// <summary>
		/// 分类名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string FENLEIMC { get; set; }
		/// <summary>
		/// 标准编码
		/// </summary>
		[Required]
		[StringLength(20)]
		public string BIAOZHUNBM { get; set; }
		/// <summary>
		/// 末级标志
		/// </summary>
		[Required]
		public int? MOJIBZ { get; set; }
		/// <summary>
		/// 药品类型
		/// </summary>
		[Required]
		[StringLength(4)]
		public string YAOPINLX { get; set; }
		/// <summary>
		/// 顺序号
		/// </summary>
		[Required]
		public int? SHUNXUHAO { get; set; }
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
		/// 分类类别
		/// </summary>
		[Required]
		[StringLength(10)]
		public string FENLEILB { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		[Required]
		public int? ZUOFEIBZ { get; set; }
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
		/// 药品根类ID
		/// </summary>
		[StringLength(1110)]
		public string YAOPINGLID { get; set; }
	}
}
