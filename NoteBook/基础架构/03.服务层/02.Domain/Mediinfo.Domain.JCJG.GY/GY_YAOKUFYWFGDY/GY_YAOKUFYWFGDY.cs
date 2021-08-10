using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YAOKUFYWFGDY")]
	public partial class GY_YAOKUFYWFGDY : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 应用ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 组号
		/// </summary>
		public int? ZUHAO { get; set; }
		/// <summary>
		/// 目标值
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string MUBIAOZHI { get; set; }
		/// <summary>
		/// 对应值
		/// </summary>
		[Key]
		[Column(Order=3)]
		[StringLength(10)]
		public string DUIYINGZHI { get; set; }
		private string _LEIXING;
		/// <summary>
		/// 类型
		/// </summary>
		[StringLength(4)]
		public string LEIXING { get{ return _LEIXING; } set{ _LEIXING = value ?? "1"; } }
	}
}
