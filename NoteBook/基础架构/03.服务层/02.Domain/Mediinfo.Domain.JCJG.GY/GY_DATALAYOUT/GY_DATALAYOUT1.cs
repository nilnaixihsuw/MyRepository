using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;

namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_DATALAYOUT1")]
	public partial class GY_DATALAYOUT1 : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 主键
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(20)]
		public string DATALAYOUTID { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[Required]
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 窗体名
		/// </summary>
		[Required]
		[StringLength(50)]
		public string FORMNAME { get; set; }
		/// <summary>
		/// 控件名
		/// </summary>
		[Required]
		[StringLength(50)]
		public string CONTROLNAME { get; set; }
		/// <summary>
		/// 命名空间
		/// </summary>
		[Required]
		[StringLength(50)]
		public string NAMESPACE { get; set; }
		private int? _LINENUMBER;
		/// <summary>
		/// 行号
		/// </summary>
		public int? LINENUMBER { get{ return _LINENUMBER; } set{ _LINENUMBER = value; } }
		private int? _ROWFONTSIZE;
		/// <summary>
		/// 行字体
		/// </summary>
		public int? ROWFONTSIZE { get{ return _ROWFONTSIZE; } set{ _ROWFONTSIZE = value; } }
		/// <summary>
		/// 行背景色
		/// </summary>
		[StringLength(500)]
		public string ROWBACKCOLOREXPRESSION { get; set; }
		/// <summary>
		/// 行背景色描述
		/// </summary>
		[StringLength(500)]
		public string ROWBACKCOLORDESCRIBE { get; set; }
		/// <summary>
		/// 排序列设置
		/// </summary>
		[StringLength(500)]
		public string ORDERBYFIELD { get; set; }
		private int? _SHOWGROUPPANEL;
		/// <summary>
		/// 是否显示分组面板
		/// </summary>
		public int? SHOWGROUPPANEL { get{ return _SHOWGROUPPANEL; } set{ _SHOWGROUPPANEL = value; } }
		private int? _ALLOWFILTER;
		/// <summary>
		/// 是否允许过滤
		/// </summary>
		public int? ALLOWFILTER { get{ return _ALLOWFILTER; } set{ _ALLOWFILTER = value; } }
		private int? _ALLOWSORT;
		/// <summary>
		/// 是否允许排序
		/// </summary>
		public int? ALLOWSORT { get{ return _ALLOWSORT; } set{ _ALLOWSORT = value; } }
		private int? _ENABLECOLUMNMENU;
		/// <summary>
		/// 是显示列菜单
		/// </summary>
		public int? ENABLECOLUMNMENU { get{ return _ENABLECOLUMNMENU; } set{ _ENABLECOLUMNMENU = value; } }
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
        ///默认值方法 
        /// </summary>
        [Ignore]
        public override void SetDefaultValue () 
		{
			if (LINENUMBER.IsNullOrDBNull())
			LINENUMBER = 1;
			if (ROWFONTSIZE.IsNullOrDBNull())
			ROWFONTSIZE = 9;
			if (SHOWGROUPPANEL.IsNullOrDBNull())
			SHOWGROUPPANEL = 0;
			if (ALLOWFILTER.IsNullOrDBNull())
			ALLOWFILTER = 1;
			if (ALLOWSORT.IsNullOrDBNull())
			ALLOWSORT = 1;
			if (ENABLECOLUMNMENU.IsNullOrDBNull())
			ENABLECOLUMNMENU = 1;
		}
	}
}
