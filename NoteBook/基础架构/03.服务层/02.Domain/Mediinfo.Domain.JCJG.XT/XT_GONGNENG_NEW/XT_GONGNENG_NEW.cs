using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.XT
{
	[Table("XT_GONGNENG_NEW")]
	public partial class XT_GONGNENG_NEW : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 行ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(20)]
		public string GONGNENGROWID { get; set; }
		/// <summary>
		/// 系统ID
		/// </summary>
		[StringLength(2)]
		public string XITONGID { get; set; }
		/// <summary>
		/// 功能ID
		/// </summary>
		[Required]
		[StringLength(20)]
		public string GONGNENGID { get; set; }
		/// <summary>
		/// 功能名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string GONGNENGMC { get; set; }
		/// <summary>
		/// 功能描述
		/// </summary>
		[StringLength(500)]
		public string GONGNENGMS { get; set; }
		/// <summary>
		/// 上级功能ID
		/// </summary>
		[StringLength(20)]
		public string SHANGJIGNID { get; set; }
		/// <summary>
		/// 顺序号
		/// </summary>
		public int? SHUNXUHAO { get; set; }
		/// <summary>
		/// 入口窗口
		/// </summary>
		[StringLength(50)]
		public string RUKOUCK { get; set; }
		/// <summary>
		/// 调用参数
		/// </summary>
		[StringLength(1000)]
		public string DIAOYONGCS { get; set; }
		/// <summary>
		/// 图片
		/// </summary>
		[StringLength(100)]
		public string TUPIAN { get; set; }
		private int? _DAOHANGLXS;
		/// <summary>
		/// 导航栏显示
		/// </summary>
		public int? DAOHANGLXS { get{ return _DAOHANGLXS; } set{ _DAOHANGLXS = value ?? 0; } }
		private int? _GONGJULXS;
		/// <summary>
		/// 工具栏显示
		/// </summary>
		public int? GONGJULXS { get{ return _GONGJULXS; } set{ _GONGJULXS = value ?? 0; } }
		/// <summary>
		/// 快捷键
		/// </summary>
		[StringLength(10)]
		public string KUAIJIEJIAN { get; set; }
		private int? _ZUOFEIBZ;
		/// <summary>
		/// 作废标志
		/// </summary>
		[Required]
		public int? ZUOFEIBZ { get{ return _ZUOFEIBZ; } set{ _ZUOFEIBZ = value ?? 0; } }
		/// <summary>
		/// 修改时间
		/// </summary>
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime? XIUGAISJ { get; set; }
		/// <summary>
		/// 修改人
		/// </summary>
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 打开方式
		/// </summary>
		[StringLength(50)]
		public string DAKAIFS { get; set; }
	}
}
