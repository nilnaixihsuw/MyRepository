using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_FEIYONGLB")]
	public partial class GY_FEIYONGLB : EntityBase, IEntityMapper
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
		/// 医保ID
		/// </summary>
		[StringLength(4)]
		public string YIBAOID { get; set; }
		/// <summary>
		/// 类别属性
		/// </summary>
		[Required]
		[StringLength(10)]
		public string LEIBIESX { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		[Required]
		public int? ZUOFEIBZ { get; set; }
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
		/// 输入码1
		/// </summary>
		[StringLength(10)]
		public string SHURUMA1 { get; set; }
		/// <summary>
		/// 输入码2
		/// </summary>
		[StringLength(10)]
		public string SHURUMA2 { get; set; }
		/// <summary>
		/// 输入码3
		/// </summary>
		[StringLength(10)]
		public string SHURUMA3 { get; set; }
		private int? _SHUNXUHAO;
		/// <summary>
		/// 顺序号
		/// </summary>
		[Required]
		public int? SHUNXUHAO { get{ return _SHUNXUHAO; } set{ _SHUNXUHAO = value ?? 0; } }
		/// <summary>
		/// 前缀
		/// </summary>
		[StringLength(10)]
		public string QIANZHUI { get; set; }
		/// <summary>
		/// 费用算法
		/// </summary>
		[StringLength(4)]
		public string FEIYONGSF { get; set; }
		/// <summary>
		/// 医保断开标志HR3-10460
		/// </summary>
		[StringLength(10)]
		public string YIBAODKBZ { get; set; }
		/// <summary>
		/// 类别属性2 HR3-14886
		/// </summary>
		[StringLength(10)]
		public string LEIBIESX2 { get; set; }
		/// <summary>
		/// 默认启用账户213198
		/// </summary>
		[StringLength(4)]
		public string MORENQYZH { get; set; }
		/// <summary>
		/// 住院医保断开标志HR3-21987(250014)
		/// </summary>
		[StringLength(10)]
		public string YIBAODKBZZY { get; set; }
		/// <summary>
		/// 住院医生站显示颜色（RGB）
		/// </summary>
		[StringLength(20)]
		public string YISHENGZXSYS { get; set; }
		/// <summary>
		/// 是否单独取诊断HR3-30117(303086))
		/// </summary>
		[StringLength(1)]
		public string SHIFOUDDQZD { get; set; }
	}
}
