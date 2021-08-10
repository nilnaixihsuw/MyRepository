using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JUESEBB")]
	public partial class GY_JUESEBB : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 角色ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JUESEID { get; set; }
		/// <summary>
		/// 报表ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string BAOBIAOID { get; set; }
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
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{}
		}
	}
