using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_BUJUQX")]
	public partial class GY_BUJUQX : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 权限ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(50)]
		public string QUANXIANID { get; set; }
		/// <summary>
		/// 权限名称
		/// </summary>
		[Required]
		[StringLength(200)]
		public string QUANXIANMC { get; set; }
		/// <summary>
		/// 角色ID
		/// </summary>
		[Required]
		[StringLength(50)]
		public string JUESEID { get; set; }
		/// <summary>
		/// 权限级别
		/// </summary>
		public int? QUANXIANJB { get; set; }
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
