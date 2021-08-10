using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JIANCHAXMBWDY")]
	public partial class GY_JIANCHAXMBWDY : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 检查项目ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string JIANCHAXMID { get; set; }
		/// <summary>
		/// 检查部位ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string JIANCHABWID { get; set; }
		/// <summary>
		/// 检查部位对应代码
		/// </summary>
		[StringLength(50)]
		public string JIANCHABWDYDM { get; set; }
		/// <summary>
		/// 检查方法
		/// </summary>
		[StringLength(50)]
		public string JIANCHAFF { get; set; }
		/// <summary>
		/// 检查项目部位对应ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JIANCHAXMBWDYID { get; set; }
		/// <summary>
		/// 顺序号HR3-45345(392026)
		/// </summary>
		public int? SHUNXUHAO { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{}
		}
	}
