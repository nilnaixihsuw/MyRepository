using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JIANCHAXMDYSM")]
	public partial class GY_JIANCHAXMDYSM : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 检查项目ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JIANCHAXMID { get; set; }
		/// <summary>
		/// 应用ID(意义扩展,适用范围为2时,科室ID)
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 保存名称应用名称或科室名称
		/// </summary>
		[StringLength(50)]
		public string YINGYONGIDMC { get; set; }
		private string _SHIYONGFW;
		/// <summary>
		/// 适用范围 目前1.应用ID 2.科室ID
		/// </summary>
		[StringLength(4)]
		public string SHIYONGFW { get; set; }
		/// <summary>
		/// 检查说明
		/// </summary>
		[StringLength(500)]
		public string JIANCHASM { get; set; }
		/// <summary>
		/// 导医说明
		/// </summary>
		[StringLength(500)]
		public string DAOYISM { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{
			if (SHIYONGFW.IsNullOrDBNull())
			SHIYONGFW = "1";
		}
	}
}
