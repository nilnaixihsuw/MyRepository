using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_FEIYONGKZDY")]
	public partial class GY_FEIYONGKZDY : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 费用控制ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string FEIYONGKZID { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 费用性质
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string FEIYONGXZ { get; set; }
		/// <summary>
		/// 修改人
		/// </summary>
		[Required]
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		[Required]
		public DateTime? XIUGAISJ { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		public int? ZUOFEIBZ { get; set; }
	}
}
