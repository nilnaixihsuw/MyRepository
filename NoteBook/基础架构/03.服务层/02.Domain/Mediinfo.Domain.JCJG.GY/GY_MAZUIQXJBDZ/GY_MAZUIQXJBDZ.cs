using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_MAZUIQXJBDZ")]
	public partial class GY_MAZUIQXJBDZ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 权限对照ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string QUANXIANDZID { get; set; }
		/// <summary>
		/// 麻醉职称ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string MAZUIZCID { get; set; }
		/// <summary>
		/// 科室ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string KESHIID { get; set; }
		/// <summary>
		/// 手术级别列表
		/// </summary>
		[Required]
		[StringLength(50)]
		public string SHOUSHUJBLB { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		public int? ZUOFEIBZ { get; set; }
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
