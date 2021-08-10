using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.XT
{
	[Table("XT_GONGNENG")]
	public partial class XT_GONGNENG : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 功能ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(20)]
		public string GONGNENGID { get; set; }
		/// <summary>
		/// 功能名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string GONGNENGMC { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		[Required]
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 入口窗口
		/// </summary>
		[StringLength(50)]
		public string RUKOUCK { get; set; }
		/// <summary>
		/// 大图片
		/// </summary>
		[StringLength(50)]
		public string DATUPIAN { get; set; }
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
		/// 打开方式
		/// </summary>
		[StringLength(50)]
		public string DAKAIFS { get; set; }
		/// <summary>
		/// 功能描述
		/// </summary>
		[StringLength(100)]
		public string GONGNENGMS { get; set; }
		/// <summary>
		/// 导航栏显示
		/// </summary>
		[Required]
		public int? DAOHANGLXS { get; set; }
		/// <summary>
		/// 工具栏显示
		/// </summary>
		[Required]
		public int? GONGJULXS { get; set; }
		private int? _XITONGBZ;
		/// <summary>
		/// 系统标志
		/// </summary>
		public int? XITONGBZ { get{ return _XITONGBZ; } set{ _XITONGBZ = value ?? 1; } }
		/// <summary>
		/// 调用参数
		/// </summary>
		[StringLength(1000)]
		public string DIAOYONGCS { get; set; }
		/// <summary>
		/// 分类ID
		/// </summary>
		[StringLength(10)]
		public string FENLEIID { get; set; }
		/// <summary>
		/// 系统时间
		/// </summary>
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime? XITONGSJ { get; set; }
		/// <summary>
		/// 系统ID
		/// </summary>
		[StringLength(2)]
		public string XITONGID { get; set; }
		private int? _GONGXIANGGL;
		/// <summary>
		/// 共享管理
		/// </summary>
		public int? GONGXIANGGL { get{ return _GONGXIANGGL; } set{ _GONGXIANGGL = value ?? 0; } }
		/// <summary>
		/// 快捷键
		/// </summary>
		public int? KUAIJIEJIAN { get; set; }
		/// <summary>
		/// 上级功能ID
		/// </summary>
		[StringLength(20)]
		public string SHANGJIGNID { get; set; }
		/// <summary>
		/// 顺序号
		/// </summary>
		public int? SHUNXUHAO { get; set; }
	}
}
