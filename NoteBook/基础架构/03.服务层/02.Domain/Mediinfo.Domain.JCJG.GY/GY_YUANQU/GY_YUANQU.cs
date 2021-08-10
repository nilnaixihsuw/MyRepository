using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YUANQU")]
	public partial class GY_YUANQU : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 院区ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(1)]
		public string YUANQUID { get; set; }
		/// <summary>
		/// 院区名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string YUANQUMC { get; set; }
		/// <summary>
		/// 院区编号
		/// </summary>
		[StringLength(20)]
		public string YUANQUBH { get; set; }
		/// <summary>
		/// 修改人
		/// </summary>
		[Required]
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		[Required]
		public DateTime? XIUGAISJ { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		public long? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 医院全称
		/// </summary>
		[StringLength(100)]
		public string YIYUANQC { get; set; }
		/// <summary>
		/// 医院简称
		/// </summary>
		[StringLength(100)]
		public string YIYUANJC { get; set; }
		/// <summary>
		/// 银行账户
		/// </summary>
		[StringLength(20)]
		public string YINHANGZH { get; set; }
		/// <summary>
		/// 开户银行
		/// </summary>
		[StringLength(100)]
		public string KAIHUYH { get; set; }
		/// <summary>
		/// 检验机构简码HR3-13089(169754)
		/// </summary>
		[StringLength(20)]
		public string JIANYANJGJM { get; set; }
		/// <summary>
		/// 医保机构编号
		/// </summary>
		[StringLength(20)]
		public string YIBAOJGBH { get; set; }
		/// <summary>
		/// 农保机构编号
		/// </summary>
		[StringLength(20)]
		public string NONGBAOJGBH { get; set; }
		/// <summary>
		/// 英文名称HR3-23852
		/// </summary>
		[StringLength(100)]
		public string YINGWENMC { get; set; }
		/// <summary>
		/// 联系电话 HR3-23852
		/// </summary>
		[StringLength(50)]
		public string LIANXIDH { get; set; }
		/// <summary>
		/// 邮编 HR3-23852
		/// </summary>
		[StringLength(10)]
		public string YOUBIAN { get; set; }
		/// <summary>
		/// 地址 HR3-23852
		/// </summary>
		[StringLength(100)]
		public string DIZHI { get; set; }
		/// <summary>
		/// 电子邮箱 HR3-23852
		/// </summary>
		[StringLength(100)]
		public string DIANZIYX { get; set; }
		/// <summary>
		/// 网站 HR3-23852
		/// </summary>
		[StringLength(100)]
		public string WANGZHAN { get; set; }
		/// <summary>
		/// 组织结构代码 HR3-23852
		/// </summary>
		[StringLength(50)]
		public string ZUZHIJGDM { get; set; }
		/// <summary>
		/// 机构负责人姓名 HR3-23852
		/// </summary>
		[StringLength(50)]
		public string JIGOUFZRXM { get; set; }
		/// <summary>
		/// 机构负责人联系电话 HR3-23852
		/// </summary>
		[StringLength(50)]
		public string JIGOUFZRLXDH { get; set; }
		/// <summary>
		/// 市异地医保机构编号
		/// </summary>
		[StringLength(20)]
		public string YIBAOSYDJGBH { get; set; }
		/// <summary>
		/// 民政机构编号
		/// </summary>
		[StringLength(20)]
		public string MINZHENGJGBH { get; set; }
		/// <summary>
		/// 区域机构编号HR3-27350(284694)
		/// </summary>
		[StringLength(20)]
		public string QUYUJGBH { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		public void SetDefaultValue () 
		{}
		}
	}
