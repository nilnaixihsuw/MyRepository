using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YAOPINZZZS")]
	public partial class GY_YAOPINZZZS : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 证书明细ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string ZHENGSHUMXID { get; set; }
		/// <summary>
		/// 证书ID
		/// </summary>
		[StringLength(10)]
		public string ZHENGSHUID { get; set; }
		/// <summary>
		/// 证书名称
		/// </summary>
		[StringLength(100)]
		public string ZHENGSHUMC { get; set; }
		/// <summary>
		/// 证书类别
		/// </summary>
		[StringLength(10)]
		public string ZHENGSHULB { get; set; }
		/// <summary>
		/// 证书效期
		/// </summary>
		public DateTime? ZHENGSHUXQ { get; set; }
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
		/// 应用ID
		/// </summary>
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 供货单位
		/// </summary>
		[StringLength(10)]
		public string DANWEI { get; set; }
		/// <summary>
		/// 价格ID
		/// </summary>
		[StringLength(10)]
		public string JIAGEID { get; set; }
		private int? _ZUOFEIBZ;
		/// <summary>
		/// 作废标志
		/// </summary>
		public int? ZUOFEIBZ { get{ return _ZUOFEIBZ; } set{ _ZUOFEIBZ = value ?? 0; } }
		/// <summary>
		/// 备注
		/// </summary>
		[StringLength(500)]
		public string BEIZHU { get; set; }
	}
}
