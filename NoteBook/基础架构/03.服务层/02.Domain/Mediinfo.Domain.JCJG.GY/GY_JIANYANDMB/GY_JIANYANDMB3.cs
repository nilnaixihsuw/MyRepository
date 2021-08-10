using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JIANYANDMB3")]
	public partial class GY_JIANYANDMB3 : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 检验单模版ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JIANYANDMBID { get; set; }
		/// <summary>
		/// 检验单格式ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string JIANYANDGSID { get; set; }
		/// <summary>
		/// 列代码
		/// </summary>
		[Required]
		[StringLength(20)]
		public string LIEDAIMA { get; set; }
		/// <summary>
		/// 列描述
		/// </summary>
		[StringLength(100)]
		public string LIEMIAOSHU { get; set; }
		/// <summary>
		/// 对应类型
		/// </summary>
		[StringLength(4)]
		public string DUIYINGLX { get; set; }
		/// <summary>
		/// 检验项目
		/// </summary>
		[StringLength(10)]
		public string JIANYANXM { get; set; }
		/// <summary>
		/// 顺序号
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
