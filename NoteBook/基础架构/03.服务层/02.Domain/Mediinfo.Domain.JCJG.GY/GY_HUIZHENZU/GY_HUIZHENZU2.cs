using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_HUIZHENZU2")]
	public partial class GY_HUIZHENZU2 : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 会诊组ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string HUIZHENZID { get; set; }
		/// <summary>
		/// 职工ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string ZHIGONGID { get; set; }
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
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{}
		}
	}
