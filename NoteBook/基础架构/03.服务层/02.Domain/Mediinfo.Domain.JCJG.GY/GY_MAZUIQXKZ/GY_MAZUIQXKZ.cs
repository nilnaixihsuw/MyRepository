using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_MAZUIQXKZ")]
	public partial class GY_MAZUIQXKZ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 麻醉权限控制ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string MAZUIQXKZID { get; set; }
		/// <summary>
		/// 职工ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string ZHIGONGID { get; set; }
		/// <summary>
		/// 手术级别
		/// </summary>
		[StringLength(10)]
		public string SHOUSHUJB { get; set; }
		/// <summary>
		/// 手术名称ID
		/// </summary>
		[StringLength(10)]
		public string SHOUSHUMCID { get; set; }
		/// <summary>
		/// 审核状态 
		/// </summary>
		public int? SHENHETZ { get; set; }
		/// <summary>
		/// 审核人
		/// </summary>
		[StringLength(10)]
		public string SHENHEREN { get; set; }
		/// <summary>
		/// 审核时间
		/// </summary>
		public DateTime? SHENHESJ { get; set; }
		private int? _ZUOFEIBZ;
		/// <summary>
		/// 作废标志
		/// </summary>
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 麻醉权限级别
		/// </summary>
		[StringLength(10)]
		public string MAZUIQXJB { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{
			if (ZUOFEIBZ.IsNullOrDBNull())
			ZUOFEIBZ = 0;
		}
	}
}
