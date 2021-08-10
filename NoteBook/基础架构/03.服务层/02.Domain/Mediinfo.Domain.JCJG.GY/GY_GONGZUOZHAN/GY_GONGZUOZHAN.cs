using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_GONGZUOZHAN")]
	public partial class GY_GONGZUOZHAN : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 工作站ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(20)]
		public string GONGZUOZID { get; set; }
		/// <summary>
		/// 工作站名
		/// </summary>
		[StringLength(100)]
		public string GONGZUOZM { get; set; }
		/// <summary>
		/// 计算机名
		/// </summary>
		[StringLength(50)]
		public string JISUANJM { get; set; }
		/// <summary>
		/// IP
		/// </summary>
		[Required]
		[StringLength(20)]
		public string IP { get; set; }
		/// <summary>
		/// 网卡地址
		/// </summary>
		[StringLength(20)]
		public string WANGKADZ { get; set; }
		/// <summary>
		/// 位置说明
		/// </summary>
		[StringLength(100)]
		public string WEIZHISM { get; set; }
		/// <summary>
		/// 所属科室
		/// </summary>
		[StringLength(10)]
		public string SUOSHUKS { get; set; }
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
		/// 位置ID
		/// </summary>
		[StringLength(10)]
		public string WEIZHIID { get; set; }
		/// <summary>
		/// 输入码1
		/// </summary>
		[StringLength(20)]
		public string SHURUMA1 { get; set; }
		/// <summary>
		/// 输入码2
		/// </summary>
		[StringLength(20)]
		public string SHURUMA2 { get; set; }
		/// <summary>
		/// 输入码3
		/// </summary>
		[StringLength(20)]
		public string SHURUMA3 { get; set; }
		/// <summary>
		/// 上级工作站ID
		/// </summary>
		[StringLength(20)]
		public string SHANGJIID { get; set; }
		private int? _MOJIBZ;
		/// <summary>
		/// 末级标志
		/// </summary>
		public int? MOJIBZ { get{ return _MOJIBZ; } set{ _MOJIBZ = value ?? 1; } }
		private int? _GENGXINBZ;
		/// <summary>
		/// 更新标志
		/// </summary>
		public int? GENGXINBZ { get{ return _GENGXINBZ; } set{ _GENGXINBZ = value ?? 0; } }
		/// <summary>
		/// 打印机信息
		/// </summary>
		[StringLength(500)]
		public string DAYINJXX { get; set; }
	}
}
