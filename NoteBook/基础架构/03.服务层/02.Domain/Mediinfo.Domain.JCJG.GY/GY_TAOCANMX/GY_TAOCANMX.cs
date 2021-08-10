using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_TAOCANMX")]
	public partial class GY_TAOCANMX : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 套餐ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string TAOCANID { get; set; }
		/// <summary>
		/// 项目ID
		/// </summary>
		[Key]
		[Column(Order=2)]
		[StringLength(10)]
		public string XIANGMUID { get; set; }
		/// <summary>
		/// 费用类别
		/// </summary>
		[Key]
		[Column(Order=3)]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int? FEIYONGLB { get; set; }
		/// <summary>
		/// 项目名称
		/// </summary>
		[StringLength(100)]
		public string XIANGMUMC { get; set; }
		/// <summary>
		/// 单位
		/// </summary>
		[StringLength(10)]
		public string DANWEI { get; set; }
		/// <summary>
		/// 单价
		/// </summary>
		public decimal? DANJIA { get; set; }
		/// <summary>
		/// 数量
		/// </summary>
		public decimal? SHULIANG { get; set; }
		/// <summary>
		/// 修改人
		/// </summary>
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? XIUGAISJ { get; set; }
		/// <summary>
		/// 执行科室
		/// </summary>
		[StringLength(10)]
		public string ZHIXINGKS { get; set; }
		/// <summary>
		/// 产地名称
		/// </summary>
		[StringLength(100)]
		public string CHANDIMC { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 适用诊疗医保代码
		/// </summary>
		[StringLength(20)]
		public string SHIYONGZLYBDM { get; set; }
		/// <summary>
		/// 适用诊疗项目名称
		/// </summary>
		[StringLength(100)]
		public string SHIYONGZLXMMC { get; set; }
		/// <summary>
		/// 顺序号HR3-20796(240764)
		/// </summary>
		public int? SHUNXUHAO { get; set; }
		/// <summary>
		/// 药房应用IDHR3-21532(245830)
		/// </summary>
		[StringLength(20)]
		public string YAOFANGYYID { get; set; }
		private int? _YEJIANBZ;
		/// <summary>
		/// 夜间标志HR3-21532(245830)
		/// </summary>
		public int? YEJIANBZ { get{ return _YEJIANBZ; } set{ _YEJIANBZ = value; } }
		private int? _ZIFEIBZ;
		/// <summary>
		/// 自费标志 HR3-22625(254348) 
		/// </summary>
		public int? ZIFEIBZ { get{ return _ZIFEIBZ; } set{ _ZIFEIBZ = value; } }
		/// <summary>
		///默认值方法 
		/// </summary>
		public void SetDefaultValue () 
		{
			int? YEJIANBZ = 0;
			int? ZIFEIBZ = 0;
		}
	}
}
