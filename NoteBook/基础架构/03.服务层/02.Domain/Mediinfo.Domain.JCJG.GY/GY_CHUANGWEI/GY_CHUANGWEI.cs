using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_CHUANGWEI")]
	public partial class GY_CHUANGWEI : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 床位IDHR3-32668(319040)
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string CHUANGWEIID { get; set; }
		/// <summary>
		/// 病区ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string BINGQUID { get; set; }
		/// <summary>
		/// 院区ID
		/// </summary>
		[Required]
		[StringLength(1)]
		public string YUANQUID { get; set; }
		/// <summary>
		/// 科室ID
		/// </summary>
		[StringLength(10)]
		public string KESHIID { get; set; }
		/// <summary>
		/// 房间ID
		/// </summary>
		[StringLength(10)]
		public string FANGJIANID { get; set; }
		/// <summary>
		/// 病人住院ID
		/// </summary>
		[StringLength(10)]
		public string BINGRENZYID { get; set; }
		/// <summary>
		/// 床位状态
		/// </summary>
		[StringLength(4)]
		public string CHUANGWEIZT { get; set; }
		/// <summary>
		/// 收费项目
		/// </summary>
		[StringLength(10)]
		public string SHOUFEIXM { get; set; }
		/// <summary>
		/// 分组代码
		/// </summary>
		[StringLength(10)]
		public string FENZUDM { get; set; }
		/// <summary>
		/// 床位类型
		/// </summary>
		[StringLength(4)]
		public string CHUANGWEILX { get; set; }
		/// <summary>
		/// 编制分类
		/// </summary>
		[StringLength(4)]
		public string BIANZHIFL { get; set; }
		/// <summary>
		/// 病人标志
		/// </summary>
		[StringLength(4)]
		public string BINGRENBZ { get; set; }
		/// <summary>
		/// 主治医师
		/// </summary>
		[StringLength(10)]
		public string ZHUZHIYS { get; set; }
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
		/// 作废标志
		/// </summary>
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 床位等级
		/// </summary>
		[StringLength(10)]
		public string CHUANGWEIDJ { get; set; }
		/// <summary>
		/// 占床标志
		/// </summary>
		public int? ZHANCHUANGBZ { get; set; }
		/// <summary>
		/// 套餐ID
		/// </summary>
		[StringLength(10)]
		public string CHUANGWEITCID { get; set; }
		/// <summary>
		/// 预约状态HR3-14491(183963)
		/// </summary>
		[StringLength(4)]
		public string YUYUEZT { get; set; }
		/// <summary>
		/// 预约病人IDHR3-14491(183963)
		/// </summary>
		[StringLength(10)]
		public string YUYUEBRID { get; set; }
		/// <summary>
		/// 顺序号HR3-15205(191889) 
		/// </summary>
		public int? SHUNXUHAO { get; set; }
		/// <summary>
		/// 陪轮椅标志 默认1：有 0：无
		/// </summary>
		public int? PEILUNYBZ { get; set; }
		/// <summary>
		/// 床位分区HR3-39344(359082)
		/// </summary>
		[StringLength(10)]
		public string CHUANGWEIFQ { get; set; }
	}
}
