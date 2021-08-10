using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_GUANDAOYZXMDY")]
	public partial class GY_GUANDAOYZXMDY : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 管道id
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string GUANDAOID { get; set; }
		/// <summary>
		/// 医嘱项目ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string YIZHUXMID { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{}
		}
	}
