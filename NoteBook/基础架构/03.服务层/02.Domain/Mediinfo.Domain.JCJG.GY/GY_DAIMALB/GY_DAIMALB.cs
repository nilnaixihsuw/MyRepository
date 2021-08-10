using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_DAIMALB")]
	public partial class GY_DAIMALB : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 类别ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string LEIBIEID { get; set; }
		/// <summary>
		/// 类别名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string LEIBIEMC { get; set; }
		/// <summary>
		/// 编码规则
		/// </summary>
		[Required]
		[StringLength(10)]
		public string BIANMAGZ { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		[Required]
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 顺序号
		/// </summary>
		[Required]
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
		/// 级次
		/// </summary>
		public int? JICI { get; set; }
		/// <summary>
		/// 系统标志
		/// </summary>
		public int? XITONGBZ { get; set; }
		/// <summary>
		/// 视图SQL
		/// </summary>
		[StringLength(500)]
		public string SHITUSQL { get; set; }
		/// <summary>
		/// 视图SQL1
		/// </summary>
		[StringLength(500)]
		public string SHITUSQL1 { get; set; }
		/// <summary>
		/// 有效列
		/// </summary>
		[StringLength(200)]
		public string YOUXIAOLIE { get; set; }
		/// <summary>
		/// 有效列描述
		/// </summary>
		[StringLength(500)]
		public string YOUXIAOLMS { get; set; }
		/// <summary>
		/// 用户对象
		/// </summary>
		[StringLength(50)]
		public string YONGHUDX { get; set; }
		/// <summary>
		/// 下拉列的数据信息
		/// </summary>
		[StringLength(200)]
		public string LIESHUJU { get; set; }
		private int? _YGYBBZ;
		/// <summary>
		/// 阳光医保字典标志HR3-25130(270218)
		/// </summary>
		public int? YGYBBZ { get{ return _YGYBBZ; } set{ _YGYBBZ = value ?? 0; } }
	}
}
