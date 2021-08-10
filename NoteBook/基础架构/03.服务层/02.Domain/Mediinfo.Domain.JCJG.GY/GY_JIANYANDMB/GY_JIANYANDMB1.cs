using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JIANYANDMB1")]
	public partial class GY_JIANYANDMB1 : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 检验单格式ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JIANYANDGSID { get; set; }
		/// <summary>
		/// 格式名
		/// </summary>
		[StringLength(100)]
		public string GESHIMING { get; set; }
		/// <summary>
		/// 数据窗口
		/// </summary>
		[StringLength(20)]
		public string SHUJUCK { get; set; }
		/// <summary>
		/// 顺序号
		/// </summary>
		[Required]
		public int? SHUNXUHAO { get; set; }
		/// <summary>
		/// 导入标志
		/// </summary>
		public int? DAORUBZ { get; set; }
		/// <summary>
		/// 门诊使用
		/// </summary>
		public int? MENZHENSY { get; set; }
		/// <summary>
		/// 住院使用
		/// </summary>
		public int? ZHUYUANSY { get; set; }
		/// <summary>
		/// 急诊使用
		/// </summary>
		public int? JIZHENSY { get; set; }
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
		/// 作废标志
		/// </summary>
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 模板类型
		/// </summary>
		[StringLength(4)]
		public string MOBANLX { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		private int? _ZHUANTAOCBZ;
		/// <summary>
		/// 转套餐标志
		/// </summary>
		public int? ZHUANTAOCBZ { get; set; }
		private int? _MOJIBZ;
		/// <summary>
		/// 未级标志
		/// </summary>
		public int? MOJIBZ { get; set; }
		/// <summary>
		/// 父类ID
		/// </summary>
		[StringLength(10)]
		public string FULEIID { get; set; }
		/// <summary>
		/// 急诊标志HR3-18591
		/// </summary>
		public int? JIZHENBZ { get; set; }
		/// <summary>
		/// 院区使用HR3-28945
		/// </summary>
		[StringLength(10)]
		public string YUANQUSY { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{
			if (ZHUANTAOCBZ.IsNullOrDBNull())
			ZHUANTAOCBZ = 0;
			if (MOJIBZ.IsNullOrDBNull())
			MOJIBZ = 0;
		}
	}
}
