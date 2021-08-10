using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_CHUANGWEIZU1")]
	public partial class GY_CHUANGWEIZU1 : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 床位组ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string CHUANGWEIZUID { get; set; }
		/// <summary>
		/// 床位组名称
		/// </summary>
		[StringLength(50)]
		public string CHUANGWEIZUMC { get; set; }
		/// <summary>
		/// 病区ID
		/// </summary>
		[StringLength(10)]
		public string BINGQUID { get; set; }
		private int? _ZUOFEIBZ;
		/// <summary>
		/// 作废标志
		/// </summary>
		[Required]
		public int? ZUOFEIBZ { get{ return _ZUOFEIBZ; } set{ _ZUOFEIBZ = value ?? 0; } }
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
		/// 顺序号
		/// </summary>
		public int? SHUNXUHAO { get; set; }
	}
}
