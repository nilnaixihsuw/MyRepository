using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.XT
{
	[Table("XT_HANZIKU")]
	public partial class XT_HANZIKU : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 汉字
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string HANZI { get; set; }
		/// <summary>
		/// 全拼
		/// </summary>
		[StringLength(10)]
		public string QUANPIN { get; set; }
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
		private long? _DUOYINBZ;
		/// <summary>
		/// 是否多音字
		/// </summary>
		[Required]
		public long? DUOYINBZ { get{ return _DUOYINBZ; } set{ _DUOYINBZ = value ?? 0; } }
	}
}
