using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_JIARIPAIBAN")]
	public partial class GY_JIARIPAIBAN : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 排班ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(20)]
		public string JIEJIARPBID { get; set; }
		/// <summary>
		/// 挂号日期
		/// </summary>
		public DateTime? GUAHAORQ { get; set; }
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
		/// 上午限号HR3-36838(342196)
		/// </summary>
		public int? SHANGWUXH { get; set; }
		/// <summary>
		/// 上午最高限号HR3-36838(342196)
		/// </summary>
		public int? SHANGWUZGXH { get; set; }
		/// <summary>
		/// 下午限号HR3-36838(342196)
		/// </summary>
		public int? XIAWUXH { get; set; }
		/// <summary>
		/// 下午最高限号HR3-36838(342196)
		/// </summary>
		public int? XIAWUZGXH { get; set; }
		/// <summary>
		/// 晚上限号HR3-36838(342196)
		/// </summary>
		public int? WANSHANGXH { get; set; }
		/// <summary>
		/// 晚上最高限号HR3-36838(342196)
		/// </summary>
		public int? WANSHANGZGXH { get; set; }
		/// <summary>
		/// 晚上挂号项目HR3-35832(337275)
		/// </summary>
		[StringLength(30)]
		public string WANSHANGGHXM { get; set; }
		/// <summary>
		/// 晚上诊疗项目HR3-35832(337275)
		/// </summary>
		[StringLength(30)]
		public string WANSHANGZLXM { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{}
		}
	}
