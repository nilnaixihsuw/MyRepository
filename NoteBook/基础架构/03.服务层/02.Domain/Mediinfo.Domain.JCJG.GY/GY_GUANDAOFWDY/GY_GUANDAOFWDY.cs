using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_GUANDAOFWDY")]
	public partial class GY_GUANDAOFWDY : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 管道ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string GUANDAOID { get; set; }
		/// <summary>
		/// 管道方位ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string GUANDAOFWID { get; set; }
		/// <summary>
		/// 管道部位ID
		/// </summary>
		[Key]
		[Column(Order=3)]
		[StringLength(10)]
		public string GUANDAOBWID { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{}
		}
	}
