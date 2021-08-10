using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JIANYANXMSF")]
	public partial class GY_JIANYANXMSF : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 检验项目ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JIANYANXMID { get; set; }
		/// <summary>
		/// 项目类型
		/// </summary>
		[StringLength(4)]
		public string XIANGMULX { get; set; }
		/// <summary>
		/// 项目ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string XIANGMUID { get; set; }
		/// <summary>
		/// 数量
		/// </summary>
		[Required]
		public decimal? SHULIANG { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{}
		}
	}
