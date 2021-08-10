using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_SHOUFEIXMWZDZ")]
	public partial class GY_SHOUFEIXMWZDZ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 对照ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string DUIZHAOID { get; set; }
		/// <summary>
		/// 价格ID
		/// </summary>
		[StringLength(10)]
		public string JIAGEID { get; set; }
		/// <summary>
		/// 物品ID
		/// </summary>
		[StringLength(10)]
		public string WUPINID { get; set; }
		/// <summary>
		/// 收费项目ID
		/// </summary>
		[StringLength(10)]
		public string SHOUFEIXMID { get; set; }
		/// <summary>
		/// 修改人
		/// </summary>
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? XIUGAISJ { get; set; }
		private int? _ZUOFEIBZ;
		/// <summary>
		/// 作废标志
		/// </summary>
		public int? ZUOFEIBZ { get{ return _ZUOFEIBZ; } set{ _ZUOFEIBZ = value ?? 0; } }
	}
}
