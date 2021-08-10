using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_FEIYONGXZ")]
	public partial class GY_FEIYONGXZ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 性质ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string XINGZHIID { get; set; }
		/// <summary>
		/// 性质名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string XINGZHIMC { get; set; }
		/// <summary>
		/// 费用控制ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string FEIYONGKZID { get; set; }
		/// <summary>
		/// 结算类型
		/// </summary>
		[Required]
		[StringLength(4)]
		public string JIESUANLX { get; set; }
		/// <summary>
		/// 费用类别
		/// </summary>
		[Required]
		[StringLength(10)]
		public string FEIYONGLB { get; set; }
		/// <summary>
		/// 末级标志
		/// </summary>
		public int? MOJIBZ { get; set; }
		/// <summary>
		/// 父类性质
		/// </summary>
		[StringLength(10)]
		public string FULEIXZ { get; set; }
		/// <summary>
		/// 级次
		/// </summary>
		public int? JICI { get; set; }
		/// <summary>
		/// 门诊使用
		/// </summary>
		[Required]
		public int? MENZHENSY { get; set; }
		/// <summary>
		/// 住院使用
		/// </summary>
		[Required]
		public int? ZHUYUANSY { get; set; }
		/// <summary>
		/// 性质属性
		/// </summary>
		[StringLength(10)]
		public string XINGZHISX { get; set; }
		/// <summary>
		/// 建档类型
		/// </summary>
		[Required]
		[StringLength(4)]
		public string JIANDANGLX { get; set; }
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
		/// 优惠类别
		/// </summary>
		[StringLength(4)]
		public string YOUHUILB { get; set; }
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
		/// 医保病人性质
		/// </summary>
		[StringLength(10)]
		public string YIBAOBRXZ { get; set; }
		/// <summary>
		/// 顺序号
		/// </summary>
		[Required]
		public int? SHUNXUHAO { get; set; }
		/// <summary>
		/// 发票打印范围
		/// </summary>
		[StringLength(4)]
		public string FAPIAODYFW { get; set; }
		/// <summary>
		/// 病案首页_医疗付款方式
		/// </summary>
		[StringLength(10)]
		public string BINGANSY_YILIAOFKFS { get; set; }
		/// <summary>
		/// HR3-25353(272001)预交款倍率
		/// </summary>
		public decimal? YUJIAOKBL { get; set; }
	}
}
