using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.DTO.HIS.GY;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_CHUANGKOUZY_NEW")]
	public partial class GY_CHUANGKOUZY_NEW : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 主键
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string CHUANGKOUZYID { get; set; }
		/// <summary>
		/// 命名空间
		/// </summary>
		[Required]
		[StringLength(50)]
		public string NAMESPACE { get; set; }
		/// <summary>
		/// 窗口名
		/// </summary>
		[Required]
		[StringLength(50)]
		public string FORMNAME { get; set; }
		/// <summary>
		/// 控件名称
		/// </summary>
		[Required]
		[StringLength(50)]
		public string CONTROLNAME { get; set; }
		/// <summary>
		/// 文字
		/// </summary>
		[StringLength(50)]
		public string WENZI { get; set; }
		/// <summary>
		/// 图片
		/// </summary>
		[StringLength(100)]
		public string TUPIAN { get; set; }
		/// <summary>
		/// 颜色
		/// </summary>
		[StringLength(100)]
		public string YANSE { get; set; }
		private int? _XIANSHIBZ;
		/// <summary>
		/// 显示标志
		/// </summary>
		public int? XIANSHIBZ { get{ return _XIANSHIBZ; } set{ _XIANSHIBZ = value ?? 1; } }
		/// <summary>
		/// 字体大小
		/// </summary>
		public int? ZITIDX { get; set; }
		private int? _QUANXIANKZ;
		/// <summary>
		/// 启用权限控制
		/// </summary>
		public int? QUANXIANKZ { get{ return _QUANXIANKZ; } set{ _QUANXIANKZ = value ?? 0; } }
		private int? _XIANSHIKZ;
		/// <summary>
		/// 启用显示控制
		/// </summary>
		public int? XIANSHIKZ { get{ return _XIANSHIKZ; } set{ _XIANSHIKZ = value ?? 0; } }
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
		/// 控件文字（含窗体名）
		/// </summary>
		[StringLength(200)]
		public string FULLTEXT { get; set; }
		private int? _CONTROLTYPE;
		/// <summary>
		/// 控件类型（0：按钮)
		/// </summary>
		[Required]
		public int? CONTROLTYPE { get{ return _CONTROLTYPE; } set{ _CONTROLTYPE = value ?? 0; } }

      
    }
}
