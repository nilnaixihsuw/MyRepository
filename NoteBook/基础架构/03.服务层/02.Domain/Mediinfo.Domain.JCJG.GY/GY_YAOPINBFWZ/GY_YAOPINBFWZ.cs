using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YAOPINBFWZ")]
	public partial class GY_YAOPINBFWZ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string WEIZHIID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(100)]
		public string WEIZHISM { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[Required]
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(10)]
		public string YAOPINID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(10)]
		public string GUIGEID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(10)]
		public string JIAGEID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[Required]
		[StringLength(4)]
		public string GUANLILB { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(4)]
		public string CHUANGKOULB { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(10)]
		public string CHUANGKOUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? XIUGAISJ { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(100)]
		public string WEIZHIFL { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? BAOYAOBZ { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? MENZHENZYBZ { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? XIANSHIBZ { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(50)]
		public string BAOYAOSY { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(50)]
		public string BAIYAOSY { get; set; }
	}
}
