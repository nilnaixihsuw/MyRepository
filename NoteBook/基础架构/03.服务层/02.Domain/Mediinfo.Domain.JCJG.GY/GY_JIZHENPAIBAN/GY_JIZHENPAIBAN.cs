using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JIZHENPAIBAN")]
	public partial class GY_JIZHENPAIBAN : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 急诊排班ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(20)]
		public string JIZHENPBID { get; set; }
		/// <summary>
		/// 科室ID
		/// </summary>
		[StringLength(20)]
		public string KESHIID { get; set; }
		/// <summary>
		/// 医生ID
		/// </summary>
		[StringLength(20)]
		public string YISHENGID { get; set; }
		/// <summary>
		/// 挂号类别
		/// </summary>
		[StringLength(20)]
		public string GUAHAOLB { get; set; }
		/// <summary>
		/// 挂号项目
		/// </summary>
		[StringLength(30)]
		public string GUAOHAOXM { get; set; }
		/// <summary>
		/// 诊断项目
		/// </summary>
		[StringLength(30)]
		public string ZHENLIAOXM { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[Required]
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 开始时间
		/// </summary>
		[Required]
		public DateTime? KAISHISJ { get; set; }
		/// <summary>
		/// 结束时间
		/// </summary>
		[Required]
		public DateTime? JIESHUSJ { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? XIUGAISJ { get; set; }
		/// <summary>
		/// 修改人
		/// </summary>
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{}
		}
	}
