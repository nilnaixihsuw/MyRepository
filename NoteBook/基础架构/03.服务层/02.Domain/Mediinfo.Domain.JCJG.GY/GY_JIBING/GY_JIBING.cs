using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JIBING")]
	public partial class GY_JIBING : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 疾病ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JIBINGID { get; set; }
		/// <summary>
		/// 疾病名称
		/// </summary>
		[StringLength(100)]
		public string JIBINGMC { get; set; }
		/// <summary>
		/// 别名
		/// </summary>
		[StringLength(100)]
		public string BIEMING { get; set; }
		/// <summary>
		/// 级效
		/// </summary>
		public int? JICI { get; set; }
		/// <summary>
		/// 末级标志
		/// </summary>
		public int? MOJIBZ { get; set; }
		/// <summary>
		/// 父类疾病
		/// </summary>
		[StringLength(10)]
		public string FULEIJB { get; set; }
		/// <summary>
		/// ICD9
		/// </summary>
		[StringLength(20)]
		public string ICD9 { get; set; }
		/// <summary>
		/// ICD10
		/// </summary>
		[StringLength(20)]
		public string ICD10 { get; set; }
		/// <summary>
		/// 疾病分类
		/// </summary>
		[StringLength(50)]
		public string JIBINGFL { get; set; }
		/// <summary>
		/// 统计码
		/// </summary>
		[StringLength(100)]
		public string TONGJIMA { get; set; }
		/// <summary>
		/// 报卡类型
		/// </summary>
		[StringLength(500)]
		public string BAOKALX { get; set; }
		/// <summary>
		/// 提示文字
		/// </summary>
		[StringLength(500)]
		public string TISHIWZ { get; set; }
		/// <summary>
		/// 中医诊断分类
		/// </summary>
		[StringLength(100)]
		public string ZHONGYIZDFL { get; set; }
		/// <summary>
		/// 适用范围
		/// </summary>
		[StringLength(4)]
		public string SHIYONGFW { get; set; }
		/// <summary>
		/// 医生科室名称
		/// </summary>
		[StringLength(10)]
		public string YISHENGKS { get; set; }
		/// <summary>
		/// 主名
		/// </summary>
		public long? ZHUMING { get; set; }
		/// <summary>
		/// 中医诊断标志
		/// </summary>
		public int? ZHONGYIZDBZ { get; set; }
		/// <summary>
		/// 报卡方式
		/// </summary>
		public int? BAOKAFS { get; set; }
		/// <summary>
		/// 拼音码
		/// </summary>
		[StringLength(10)]
		public string SHURUMA1 { get; set; }
		/// <summary>
		/// 五笔码
		/// </summary>
		[StringLength(10)]
		public string SHURUMA2 { get; set; }
		/// <summary>
		/// 自定义码
		/// </summary>
		[StringLength(10)]
		public string SHURUMA3 { get; set; }
		/// <summary>
		/// 门诊使用
		/// </summary>
		public int? MENZHENSY { get; set; }
		/// <summary>
		/// 住院使用
		/// </summary>
		public int? ZHUYUANSY { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 顺序号
		/// </summary>
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
		/// 报卡时间
		/// </summary>
		[StringLength(10)]
		public string BAOKASJ { get; set; }
		/// <summary>
		/// 报卡病人条件
		/// </summary>
		[StringLength(500)]
		public string BAOKABRTJ { get; set; }
		/// <summary>
		/// 健康宣教(189839)
		/// </summary>
		[StringLength(1000)]
		public string JIANKANGXJ { get; set; }
		/// <summary>
		/// 报卡类型2 HR3-15868(196925)
		/// </summary>
		[StringLength(10)]
		public string BAOKALX2 { get; set; }
		/// <summary>
		/// 上传时间HR3-29612(299133)
		/// </summary>
		public DateTime? SHANGCHUANSJ { get; set; }
		private string _FEIYONGLB;
		/// <summary>
		/// 费用类别HR3-30117(303086))
		/// </summary>
		[Required]
		[StringLength(10)]
		public string FEIYONGLB { get{ return _FEIYONGLB; } set{ _FEIYONGLB = value ?? "0"; } }
		/// <summary>
		/// 肿瘤形态学编码HR3-32005(315090)
		/// </summary>
		[StringLength(50)]
		public string ZHONGLIUXTXBM { get; set; }
		/// <summary>
		/// 临床路径疾病标志
		/// </summary>
		public int? LINCHUANGLJJBBZ { get; set; }
	}
}
