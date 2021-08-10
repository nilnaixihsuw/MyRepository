using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YAOPINLD")]
	public partial class GY_YAOPINLD : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 药品联动ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string YAOPINLDID { get; set; }
		/// <summary>
		/// 被联动规格ID
		/// </summary>
		[StringLength(10)]
		public string GUIGEID { get; set; }
		/// <summary>
		/// 被联动药品名称
		/// </summary>
		[StringLength(100)]
		public string YAOPINMC { get; set; }
		/// <summary>
		/// 联动类型
		/// </summary>
		[StringLength(4)]
		public string LIANDONGLX { get; set; }
		/// <summary>
		/// 联动规格ID
		/// </summary>
		[StringLength(10)]
		public string LIANDONGGGID { get; set; }
		/// <summary>
		/// 联动药品名称
		/// </summary>
		[StringLength(100)]
		public string LIANDONGYPMC { get; set; }
		/// <summary>
		/// 联动数量
		/// </summary>
		public decimal? LIANDONGSL { get; set; }
		/// <summary>
		/// 顺序号
		/// </summary>
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
		/// 作废标志
		/// </summary>
		public int? ZUOFEIBZ { get; set; }
		private string _LIANDONGXMLX;
		/// <summary>
		/// 联动项目类型
		/// </summary>
		[StringLength(4)]
		public string LIANDONGXMLX { get{ return _LIANDONGXMLX; } set{ _LIANDONGXMLX = value ?? "1"; } }
	}
}
