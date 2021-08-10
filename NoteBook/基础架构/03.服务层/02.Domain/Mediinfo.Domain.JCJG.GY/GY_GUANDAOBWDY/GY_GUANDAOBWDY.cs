using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_GUANDAOBWDY")]
	public partial class GY_GUANDAOBWDY : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 管道ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string GUANDAOID { get; set; }
		/// <summary>
		/// 管道部位id
		/// </summary>
		[Key]
		[Column(Order=2)]
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
