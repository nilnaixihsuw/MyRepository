using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JIANDANGBR")]
	public partial class GY_JIANDANGBR : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 建档病人ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(20)]
		public string JIANDANGBRID { get; set; }
		/// <summary>
		/// 费用类别
		/// </summary>
		[StringLength(10)]
		public string FEIYONGLB { get; set; }
		/// <summary>
		/// 费用性质
		/// </summary>
		[Required]
		[StringLength(10)]
		public string FEIYONGXZ { get; set; }
		/// <summary>
		/// 姓名
		/// </summary>
		[StringLength(50)]
		public string XINGMING { get; set; }
		/// <summary>
		/// 性别
		/// </summary>
		[StringLength(4)]
		public string XINGBIE { get; set; }
		/// <summary>
		/// 出生日期
		/// </summary>
		public DateTime? CHUSHENGRQ { get; set; }
		/// <summary>
		/// 电话
		/// </summary>
		[StringLength(20)]
		public string DIANHUA { get; set; }
		/// <summary>
		/// 单位ID
		/// </summary>
		[StringLength(10)]
		public string DANWEIID { get; set; }
		/// <summary>
		/// 单位名称
		/// </summary>
		[StringLength(100)]
		public string DANWEIMC { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		[Required]
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 家庭电话
		/// </summary>
		[StringLength(50)]
		public string JIATINGDH { get; set; }
		/// <summary>
		/// 建档人
		/// </summary>
		[Required]
		[StringLength(10)]
		public string JIANDANGREN { get; set; }
		/// <summary>
		/// 建档日期
		/// </summary>
		public DateTime? JIANDANGRQ { get; set; }
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
		/// 自付比例
		/// </summary>
		public decimal? ZIFUBL { get; set; }
		/// <summary>
		/// 住院费用类别
		/// </summary>
		[StringLength(10)]
		public string ZHUYUANFYLB { get; set; }
		/// <summary>
		/// 住院费用性质
		/// </summary>
		[StringLength(10)]
		public string ZHUYUANFYXZ { get; set; }
		/// <summary>
		/// 就诊卡号HR3-18745
		/// </summary>
		[StringLength(100)]
		public string JIUZHENKH { get; set; }
		/// <summary>
		/// 公费证号HR3-18745
		/// </summary>
		[StringLength(20)]
		public string GONGFEIZH { get; set; }
	}
}
