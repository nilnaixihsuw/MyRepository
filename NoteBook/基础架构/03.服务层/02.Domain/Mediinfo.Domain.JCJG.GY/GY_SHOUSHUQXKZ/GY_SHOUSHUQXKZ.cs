using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_SHOUSHUQXKZ")]
	public partial class GY_SHOUSHUQXKZ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 手术权限控制ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string SHOUSHUQXKZID { get; set; }
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
		/// 手术专业HR3-35901(337588)
		/// </summary>
		[StringLength(10)]
		public string SHOUSHUZY { get; set; }
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
