using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_HULIRWPZMX")]
	public partial class GY_HULIRWPZMX : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 护理任务ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string HULIRWID { get; set; }
		/// <summary>
		/// 数据来源1.医嘱项目2.给药方式
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(4)]
		public string SHUJULY { get; set; }
		/// <summary>
		/// 项目ID
		/// </summary>
		[Key]
		[Column(Order=3)]
		[StringLength(10)]
		public string XIANGMUID { get; set; }
		/// <summary>
		/// 项目名称
		/// </summary>
		[StringLength(100)]
		public string XIANGMUMC { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{}
		}
	}
