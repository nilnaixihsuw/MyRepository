using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_QUANXIAN2_NEW")]
	public partial class GY_QUANXIAN2_NEW : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 权限ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(200)]
		public string QUANXIANID { get; set; }
		/// <summary>
		/// 权限名称
		/// </summary>
		[Required]
		[StringLength(200)]
		public string QUANXIANMC { get; set; }
		/// <summary>
		/// 命名空间
		/// </summary>
		[Required]
		[StringLength(100)]
		public string MINGMINGKJ { get; set; }
		/// <summary>
		/// 窗口名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string CHUANGKOUMC { get; set; }
		/// <summary>
		/// 控件名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string KONGJIANMC { get; set; }
		private string _YINGYONGID;
		/// <summary>
		/// 应用ID
		/// </summary>
		[Required]
        [StringLength(20)]
        public string YINGYONGID { get { return _YINGYONGID; } set { _YINGYONGID = value; } }
		/// <summary>
		/// 功能ID
		/// </summary>
		[Required]
		[StringLength(20)]
		public string GONGNENGID { get; set; }
		/// <summary>
		/// 权限描述
		/// </summary>
		[StringLength(100)]
		public string QUANXIANMS { get; set; }
		/// <summary>
		/// 系统时间
		/// </summary>
		public DateTime? XITONGSJ { get; set; }
		/// <summary>
		/// 修改人
		/// </summary>
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? XIUGAISJ { get; set; }
	}
}
