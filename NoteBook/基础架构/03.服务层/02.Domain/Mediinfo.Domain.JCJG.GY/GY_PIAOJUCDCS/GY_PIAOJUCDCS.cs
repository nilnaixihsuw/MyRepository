using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_PIAOJUCDCS")]
	public partial class GY_PIAOJUCDCS : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 票据重打次数ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string PIAOJUCDCSID { get; set; }
		/// <summary>
		/// 票据业务ID
		/// </summary>
		[StringLength(10)]
		public string PIAOJUYWID { get; set; }
		/// <summary>
		/// 票据重打次数
		/// </summary>
		[StringLength(10)]
		public string PIAOJUCDCS { get; set; }
		/// <summary>
		/// 票据类型ID
		/// </summary>
		[StringLength(30)]
		public string PIAOJULXID { get; set; }
		/// <summary>
		/// 票据重打人
		/// </summary>
		[StringLength(10)]
		public string DAYINREN { get; set; }
		/// <summary>
		/// 票据重打日期
		/// </summary>
		public DateTime? DAYINRQ { get; set; }

        [StringLength(10)]
        /// <summary>
		/// 票据业务系统
		/// </summary>
		public string PIAOJUYWXT { get; set; }
    }
}
