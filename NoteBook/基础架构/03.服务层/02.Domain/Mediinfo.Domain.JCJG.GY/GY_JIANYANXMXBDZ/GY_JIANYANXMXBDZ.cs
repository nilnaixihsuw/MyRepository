using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JIANYANXMXBDZ")]
	public partial class GY_JIANYANXMXBDZ : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 检验项目ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JIANYANXMID { get; set; }
		/// <summary>
		/// 样本类型
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string YANGBENLX { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{}
		}
	}
