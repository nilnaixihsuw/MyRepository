using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_CAIDANGJL_NEW")]
	public partial class GY_CAIDANGJL_NEW : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 应用ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 功能ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(20)]
		public string GONGNENGID { get; set; }
		/// <summary>
		/// 菜单ID
		/// </summary>
		[Key]
		[Column(Order=3)]
		[StringLength(20)]
		public string CAIDANID { get; set; }
		/// <summary>
		/// 顺序号
		/// </summary>
		[Required]
		public long? SHUNXUHAO { get; set; }
		/// <summary>
		/// 小图片
		/// </summary>
		[StringLength(50)]
		public string XIAOTUPIAN { get; set; }
		/// <summary>
		/// 工具栏文字
		/// </summary>
		[StringLength(50)]
		public string GONGJULWZ { get; set; }
		/// <summary>
		/// 快捷键
		/// </summary>
		[StringLength(10)]
		public string KUAIJIEJIAN { get; set; }
		/// <summary>
		/// 调用参数
		/// </summary>
		[StringLength(1000)]
		public string DIAOYONGCS { get; set; }
	}
}
