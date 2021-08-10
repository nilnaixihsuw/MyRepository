using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_DANWEI")]
	public partial class GY_DANWEI : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 单位ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string DANWEIID { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[Required]
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 单位名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string DANWEIMC { get; set; }
		/// <summary>
		/// 省份
		/// </summary>
		[StringLength(10)]
		public string SHENGFEN { get; set; }
		/// <summary>
		/// 开户银行
		/// </summary>
		[StringLength(100)]
		public string KAIHUYH { get; set; }
		/// <summary>
		/// 单位帐号
		/// </summary>
		[StringLength(50)]
		public string DANWEIZH { get; set; }
		/// <summary>
		/// 应付款管理标志
		/// </summary>
		[Required]
		public int? YINGFUKGLBZ { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		[Required]
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 标准编码
		/// </summary>
		[StringLength(20)]
		public string BIAOZHUNBM { get; set; }
		/// <summary>
		/// 单位类型
		/// </summary>
		[Required]
		[StringLength(4)]
		public string DANWEILX { get; set; }
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
		/// 修改人
		/// </summary>
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? XIUGAISJ { get; set; }
		/// <summary>
		/// 显示标志
		/// </summary>
		[Required]
		public int? XIANSHIBZ { get; set; }
	}
}
