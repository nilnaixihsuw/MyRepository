using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_TUPIANXX")]
	public partial class GY_TUPIANXX : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 图片资源ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(100)]
		public string TUPIANID { get; set; }
		/// <summary>
		/// 图片的行序号
		/// </summary>
		[Key]
		[Column(Order=2)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public long? XUHAO { get; set; }
		/// <summary>
		/// 图片内容
		/// </summary>
		[StringLength(4000)]
		public string NEIRONG { get; set; }
		/// <summary>
		/// 图片行像素
		/// </summary>
		public int? HANGXIANGSHU { get; set; }
		/// <summary>
		/// 图片列像素
		/// </summary>
		public int? LIEXIANGSHU { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{}
		}
	}
