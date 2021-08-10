using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("SXZZ_ZHUANZHENGSQD")]
	public partial class SXZZ_ZHUANZHENGSQD : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 申请单ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(20)]
		public string SHENQINGDID { get; set; }
		/// <summary>
		/// 就诊卡类型
		/// </summary>
		public long? JIUZHENKLX { get; set; }
		/// <summary>
		/// 就诊卡号
		/// </summary>
		[Required]
		[StringLength(20)]
		public string JIUZHENKH { get; set; }
		private long? _YEWULX;
		/// <summary>
		/// 业务类型 1 门诊 2 住院
		/// </summary>
		[Required]
		public long? YEWULX { get; set; }
		/// <summary>
		/// 病人姓名
		/// </summary>
		[Required]
		[StringLength(20)]
		public string XINGMING { get; set; }
		private long? _XINGBIE;
		/// <summary>
		/// 性别 1 男 2 女 9 其他
		/// </summary>
		[Required]
		public long? XINGBIE { get; set; }
		/// <summary>
		/// 出生日期
		/// </summary>
		public DateTime? CHUSHENRQ { get; set; }
		/// <summary>
		/// 年龄
		/// </summary>
		public long? NIANLIN { get; set; }
		/// <summary>
		/// 年龄单位
		/// </summary>
		[StringLength(10)]
		public string NIANLINDW { get; set; }
		/// <summary>
		/// 身份证号
		/// </summary>
		[Required]
		[StringLength(18)]
		public string SHENFENZH { get; set; }
		/// <summary>
		/// 联系电话
		/// </summary>
		[StringLength(20)]
		public string LIANXIDH { get; set; }
		/// <summary>
		/// 联系地址
		/// </summary>
		[StringLength(200)]
		public string LIANXIDZ { get; set; }
		/// <summary>
		/// 费用类别
		/// </summary>
		[StringLength(30)]
		public string FEIYONGLB { get; set; }
		/// <summary>
		/// 申请机构代码
		/// </summary>
		[StringLength(10)]
		public string SHENQINGJGDM { get; set; }
		/// <summary>
		/// 申请机构名称
		/// </summary>
		[StringLength(60)]
		public string SHENQINGJGMC { get; set; }
		/// <summary>
		/// 申请机构联系电话
		/// </summary>
		[StringLength(20)]
		public string JIGOULXDH { get; set; }
		/// <summary>
		/// 申请医生姓名
		/// </summary>
		[StringLength(20)]
		public string SHENQINGYS { get; set; }
		/// <summary>
		/// 申请医生电话
		/// </summary>
		[StringLength(20)]
		public string SHENQINGYSDH { get; set; }
		/// <summary>
		/// 申请日期
		/// </summary>
		public DateTime? SHENQINGRQ { get; set; }
		/// <summary>
		/// 转诊原因
		/// </summary>
		[StringLength(400)]
		public string ZHUANZHENYY { get; set; }
		/// <summary>
		/// 病情描述
		/// </summary>
		[StringLength(400)]
		public string BINGQINGMS { get; set; }
		/// <summary>
		/// 注意事项
		/// </summary>
		[StringLength(400)]
		public string ZHUYISX { get; set; }
		/// <summary>
		/// 转诊单号
		/// </summary>
		[StringLength(20)]
		public string ZHUANZHENDH { get; set; }
		/// <summary>
		/// 上转接收联系人
		/// </summary>
		[StringLength(20)]
		public string JIESHOULXR { get; set; }
		/// <summary>
		/// 上转接收联系人电话
		/// </summary>
		[StringLength(20)]
		public string JIESHOULXDH { get; set; }
		/// <summary>
		/// 转入科室代码
		/// </summary>
		[StringLength(20)]
		public string ZHUANRUKSDM { get; set; }
		/// <summary>
		/// 转入科室名称
		/// </summary>
		[StringLength(40)]
		public string ZHUANRUKSMC { get; set; }
		/// <summary>
		/// 转诊状态
		/// </summary>
		public long? ZHUANZHENZT { get; set; }
        /// <summary>
        /// 病人状态
        /// </summary>
        public long? BINGRENZT { get; set; }
        /// <summary>
        /// 操作日期
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime? CAOZUORQ { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{
			if (YEWULX.IsNullOrDBNull())
			YEWULX = 2 ;
			if (XINGBIE.IsNullOrDBNull())
			XINGBIE = 9 ;
		}
	}
}
