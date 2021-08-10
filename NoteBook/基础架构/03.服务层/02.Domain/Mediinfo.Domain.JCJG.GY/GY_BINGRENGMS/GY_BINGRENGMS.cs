using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_BINGRENGMS")]
	public partial class GY_BINGRENGMS : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 过敏史ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string GUOMINSID { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[Required]
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 院区ID
		/// </summary>
		[Required]
		[StringLength(1)]
		public string YUANQUID { get; set; }
		/// <summary>
		/// 病人ID
		/// </summary>
		[Required]
		[StringLength(100)]
		public string BINGRENID { get; set; }
		/// <summary>
		/// 来源ID
		/// </summary>
		[StringLength(10)]
		public string LAIYUANID { get; set; }
		/// <summary>
		/// 记录来源
		/// </summary>
		[StringLength(4)]
		public string JILULY { get; set; }
		/// <summary>
		/// 价格ID
		/// </summary>
		[StringLength(10)]
		public string JIAGEID { get; set; }
		/// <summary>
		/// 药品名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string YAOPINMC { get; set; }
		/// <summary>
		/// 处理意见
		/// </summary>
		[StringLength(10)]
		public string CHULIYJ { get; set; }
		/// <summary>
		/// 皮试结果
		/// </summary>
		[StringLength(10)]
		public string PISHIJG { get; set; }
		/// <summary>
		/// 执行时间
		/// </summary>
		[StringLength(20)]
		public string ZHIXINGSJ { get; set; }
		/// <summary>
		/// 执行人
		/// </summary>
		[StringLength(10)]
		public string ZHIXINGREN { get; set; }
		/// <summary>
		/// 执行人姓名
		/// </summary>
		[StringLength(20)]
		public string ZHIXINGRXM { get; set; }
		/// <summary>
		/// 过敏类型
		/// </summary>
		[StringLength(4)]
		public string GUOMINLX { get; set; }
		/// <summary>
		/// 药品ID
		/// </summary>
		[StringLength(10)]
		public string YAOPINID { get; set; }
		/// <summary>
		/// 身份证号
		/// </summary>
		[StringLength(50)]
		public string SHENFENZH { get; set; }
		/// <summary>
		/// 药物反应
		/// </summary>
		[StringLength(10)]
		public string YAOWUFY { get; set; }
        /// <summary>
		/// 不良反应详情
		/// </summary>
		[StringLength(250)]
        public string BULIANGFYXQ { get; set; }
        /// <summary>
        /// 严禁标识
        /// </summary>
        public int? YANJINFLAG { get; set; }
    }
}
