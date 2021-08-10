using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_TAOCAN")]
	public partial class GY_TAOCAN : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 套餐ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string TAOCANID { get; set; }
		/// <summary>
		/// 套餐名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string TAOCANMC { get; set; }
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
		/// 作废标志
		/// </summary>
		[Required]
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		[StringLength(200)]
		public string BEIZHU { get; set; }
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
		/// 科室代码
		/// </summary>
		[StringLength(10)]
		public string KESHIDM { get; set; }
		/// <summary>
		/// 共享标志
		/// </summary>
		public int? GONGXIANGBZ { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		public void SetDefaultValue () 
		{}
		}
	}
