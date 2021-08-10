using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_QUANXIAN")]
	public partial class GY_QUANXIAN : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 权限ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(50)]
		public string QUANXIANID { get; set; }
		/// <summary>
		/// 权限名称
		/// </summary>
		[Required]
		[StringLength(200)]
		public string QUANXIANMC { get; set; }
		/// <summary>
		/// 功能ID
		/// </summary>
		[Required]
		[StringLength(20)]
		public string GONGNENGID { get; set; }
		/// <summary>
		/// 权限级别
		/// </summary>
		[Required]
		public int? QUANXIANJB { get; set; }
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
		/// 权限描述
		/// </summary>
		[StringLength(100)]
		public string QUANXIANMS { get; set; }
		/// <summary>
		/// 系统时间
		/// </summary>
		public DateTime? XITONGSJ { get; set; }
		private int? _QIYONGBZ;
		/// <summary>
		/// 起用标志,1启用,0 未用,仅对三级权限有用
		/// </summary>
		public int? QIYONGBZ { get{ return _QIYONGBZ; } set{ _QIYONGBZ = value ?? 1; } }
	}
}
