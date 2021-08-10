using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_RIBAOMBXM")]
	public partial class GY_RIBAOMBXM : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 模板项目ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string MOBANXMID { get; set; }
		/// <summary>
		/// 模板ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string MOBANID { get; set; }
		/// <summary>
		/// 模板项目名称
		/// </summary>
		[StringLength(100)]
		public string MOBANXMMC { get; set; }
		/// <summary>
		/// 输入码1
		/// </summary>
		[StringLength(10)]
		public string SHURUMA1 { get; set; }
		/// <summary>
		/// 输入码2
		/// </summary>
		[StringLength(10)]
		public string SHURUMA2 { get; set; }
		/// <summary>
		/// 输入码3
		/// </summary>
		[StringLength(10)]
		public string SHURUMA3 { get; set; }
		/// <summary>
		/// 模板属性
		/// </summary>
		[StringLength(10)]
		public string MOBANXMSX { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		public void SetDefaultValue () 
		{}
		}
	}
