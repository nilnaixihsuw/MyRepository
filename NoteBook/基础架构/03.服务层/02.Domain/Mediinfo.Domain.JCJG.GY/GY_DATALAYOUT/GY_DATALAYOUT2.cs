using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_DATALAYOUT2")]
	public partial class GY_DATALAYOUT2 : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 主键
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(20)]
		public string DATALAYOUTMXID { get; set; }
		/// <summary>
		/// 外键
		/// </summary>
		[Required]
		[StringLength(20)]
		public string DATALAYOUTID { get; set; }
		/// <summary>
		/// 排序号
		/// </summary>
		public int? SORTNO { get; set; }
		/// <summary>
		/// 保护标志
		/// </summary>
		public int? READONLY { get; set; }
		/// <summary>
		/// 背景颜色表达式
		/// </summary>
		[StringLength(500)]
		public string BACKCOLOREXPRISSION { get; set; }
		/// <summary>
		/// 背景颜色说明
		/// </summary>
		[StringLength(500)]
		public string BACKCOLORDESCRIBE { get; set; }
		/// <summary>
		/// 初始值
		/// </summary>
		[StringLength(100)]
		public string DEFAULTVALUE { get; set; }
		/// <summary>
		/// 单元格文本对齐方式
		/// </summary>
		public int? CELLHALIGNMENT { get; set; }
		/// <summary>
		/// 单元格字体大小
		/// </summary>
		public int? CELLFONTSIZE { get; set; }
		private int? _NONEMPTY;
		/// <summary>
		/// 非空项
		/// </summary>
		public int? NONEMPTY { get; set; }
		/// <summary>
		/// 非空表示式说明
		/// </summary>
		[StringLength(500)]
		public string NONEMPTYEDESCRIBE { get; set; }
		/// <summary>
		/// 输入法模式
		/// </summary>
		[StringLength(5)]
		public string IMEMODE { get; set; }
		/// <summary>
		/// 跳转顺序
		/// </summary>
		public int? TABINDEX { get; set; }
		/// <summary>
		/// 显示格式
		/// </summary>
		[StringLength(500)]
		public string FORMATSTRING { get; set; }
		/// <summary>
		/// 显示格式类型
		/// </summary>
		public int? FORMATTYPE { get; set; }
		/// <summary>
		/// 有效性检查
		/// </summary>
		[StringLength(500)]
		public string VALIDATEEXPRISSION { get; set; }
		/// <summary>
		/// 有效性说明
		/// </summary>
		[StringLength(500)]
		public string VALIDATEDESCRIBE { get; set; }
		/// <summary>
		/// 字体颜色表达式
		/// </summary>
		[StringLength(500)]
		public string CELLFORECOLOREXPRISSION { get; set; }
		/// <summary>
		/// 字体颜色说明
		/// </summary>
		[StringLength(500)]
		public string CELLFORECOLORDESCRIBE { get; set; }
		/// <summary>
		/// 列宽度
		/// </summary>
		public int? WIDTH { get; set; }
		/// <summary>
		/// 列头固定
		/// </summary>
		public int? FIXED { get; set; }
		/// <summary>
		/// 头标题对齐方式
		/// </summary>
		public int? HEADERHALIGNMENT { get; set; }
		/// <summary>
		/// 头字体大小
		/// </summary>
		public int? HEADERFONTSIZE { get; set; }
		/// <summary>
		/// 显示标志
		/// </summary>
		public int? VISIBLE { get; set; }
		/// <summary>
		/// 中文名称
		/// </summary>
		[StringLength(100)]
		public string CAPTION { get; set; }
		/// <summary>
		/// 字段名
		/// </summary>
		[StringLength(100)]
		public string FIELDNAME { get; set; }
		/// <summary>
		/// 修改人
		/// </summary>
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? XIUGAISJ { get; set; }
		private int? _HEADERFONTBOLD;
		/// <summary>
		/// 头字体加粗
		/// </summary>
		public int? HEADERFONTBOLD { get; set; }
		/// <summary>
		/// 优先级
		/// </summary>
		public long? YOUXIANJI { get; set; }
		/// <summary>
		/// 升降序号
		/// </summary>
		public int? SHENGJIANGXH { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{
			if (NONEMPTY.IsNullOrDBNull())
			NONEMPTY = 0;
			if (HEADERFONTBOLD.IsNullOrDBNull())
			HEADERFONTBOLD = 0;
		}
	}
}
