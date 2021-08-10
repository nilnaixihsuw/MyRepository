using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_FEIYONGKZ")]
	public partial class GY_FEIYONGKZ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 费用控制ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string FEIYONGKZID { get; set; }
		/// <summary>
		/// 控制说明
		/// </summary>
		[Required]
		[StringLength(100)]
		public string KONGZHISM { get; set; }
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
		/// 限价标志
		/// </summary>
		[Required]
		public int? XIANJIABZ { get; set; }
		/// <summary>
		/// 限量标志
		/// </summary>
		[Required]
		public int? XIANLIANGBZ { get; set; }
		/// <summary>
		/// 扩大标志
		/// </summary>
		[Required]
		public int? KUODABZ { get; set; }
		/// <summary>
		/// 控制标志
		/// </summary>
		[Required]
		public int? KONGZHIBZ { get; set; }
		/// <summary>
		/// 审批标志
		/// </summary>
		[Required]
		public int? SHENPIBZ { get; set; }
		/// <summary>
		/// 上传标志
		/// </summary>
		[Required]
		public int? SHANGCHUANBZ { get; set; }
		/// <summary>
		/// 自费提示
		/// </summary>
		[Required]
		public int? ZIFEITS { get; set; }
		/// <summary>
		/// 价格体系
		/// </summary>
		[Required]
		public int? JIAGETX { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		[Required]
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 医保ID
		/// </summary>
		[StringLength(4)]
		public string YIBAOID { get; set; }
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
		/// 备注
		/// </summary>
		[StringLength(500)]
		public string BEIZHU { get; set; }
		/// <summary>
		/// 自负比例控制
		/// </summary>
		[StringLength(4)]
		public string ZIFUBLKZ { get; set; }
		/// <summary>
		/// 自负比例取法
		/// </summary>
		public int? ZIFUBLQF { get; set; }
		/// <summary>
		/// 参照费用控制
		/// </summary>
		[StringLength(10)]
		public string CANZHAOFYKZ { get; set; }
		private int? _SHENHEBZ;
		/// <summary>
		/// 审核标志
		/// </summary>
		public int? SHENHEBZ { get{ return _SHENHEBZ; } set{ _SHENHEBZ = value ?? 0; } }
		private int? _NIANLINGKZBZ;
		/// <summary>
		/// 年龄控制标志
		/// </summary>
		public int? NIANLINGKZBZ { get{ return _NIANLINGKZBZ; } set{ _NIANLINGKZBZ = value ?? 0; } }
	}
}
