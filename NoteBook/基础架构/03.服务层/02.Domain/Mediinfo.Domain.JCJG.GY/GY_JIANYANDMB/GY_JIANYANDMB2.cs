using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JIANYANDMB2")]
	public partial class GY_JIANYANDMB2 : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 检验单格式ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JIANYANDGSID { get; set; }
		/// <summary>
		/// 行号
		/// </summary>
		[Key]
		[Column(Order=2)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int? HANGHAO { get; set; }
		/// <summary>
		/// 内容
		/// </summary>
		[Required]
		[StringLength(1000)]
		public string NEIRONG { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{}
		}
	}
