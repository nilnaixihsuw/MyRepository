using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_QINGLINGDAN2")]
	public partial class GY_QINGLINGDAN2 : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 请领单明细ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string QINGLINGDMXID { get; set; }
		/// <summary>
		/// 请领单ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string QINGLINGDID { get; set; }
		/// <summary>
		/// 类别ID
		/// </summary>
		[Required]
		[StringLength(6)]
		public string LEIBIEID { get; set; }
		/// <summary>
		/// 物品ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string WUPINID { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string MINGCHENG { get; set; }
		/// <summary>
		/// 请领数量
		/// </summary>
		[Required]
		public decimal? QINGLINGSL { get; set; }
		/// <summary>
		/// 急用标志
		/// </summary>
		[Required]
		public int? JIYONGBZ { get; set; }
		/// <summary>
		/// 请领状态
		/// </summary>
		[Required]
		[StringLength(4)]
		public string QINGLINGZT { get; set; }
		/// <summary>
		/// 申请日期
		/// </summary>
		[Required]
		public DateTime? SHENQINGRQ { get; set; }
		/// <summary>
		/// 受理日期
		/// </summary>
		public DateTime? SHOULIRQ { get; set; }
		/// <summary>
		/// 顺序号
		/// </summary>
		[Required]
		public int? SHUNXUHAO { get; set; }
		/// <summary>
		/// 库存数量
		/// </summary>
		[Required]
		public decimal? KUCUNSL { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		[StringLength(500)]
		public string BEIZHU { get; set; }
		/// <summary>
		/// 产地ID
		/// </summary>
		[StringLength(10)]
		public string CHANDIID { get; set; }
		/// <summary>
		/// 产地名称
		/// </summary>
		[StringLength(100)]
		public string CHANDIMC { get; set; }
		/// <summary>
		/// 规格ID
		/// </summary>
		[StringLength(10)]
		public string GUIGEID { get; set; }
		/// <summary>
		/// 规格名称
		/// </summary>
		[StringLength(50)]
		public string GUIGEMC { get; set; }
		/// <summary>
		/// 包装单位
		/// </summary>
		[StringLength(20)]
		public string BAOZHUANGDW { get; set; }
		/// <summary>
		/// 单价
		/// </summary>
		public decimal? DANJIA { get; set; }
		/// <summary>
		/// 药品类型
		/// </summary>
		[StringLength(4)]
		public string YAOPINLX { get; set; }
		/// <summary>
		/// 生产批号
		/// </summary>
		[StringLength(50)]
		public string SHENGCHANPH { get; set; }
		/// <summary>
		/// 生产厂家
		/// </summary>
		[StringLength(50)]
		public string SHENGCHANCJ { get; set; }
		/// <summary>
		/// 灭菌批号
		/// </summary>
		[StringLength(50)]
		public string MIEJUNPH { get; set; }
		/// <summary>
		/// 库存序号(物资请领时用)
		/// </summary>
		public long? KUCUNXH { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(10)]
		public string DUIYINGCKDMXID { get; set; }
		/// <summary>
		/// 物品属性
		/// </summary>
		[StringLength(4)]
		public string WUPINSX { get; set; }
		private int? _SANJIKFBZ;
		/// <summary>
		/// 三级库房标志
		/// </summary>
		public int? SANJIKFBZ { get{ return _SANJIKFBZ; } set{ _SANJIKFBZ = value ?? 0; } }
		private string _SHENQINGFS;
		/// <summary>
		/// 申请方式
		/// </summary>
		[StringLength(4)]
		public string SHENQINGFS { get{ return _SHENQINGFS; } set{ _SHENQINGFS = value ?? "1"; } }
		/// <summary>
		/// 对应单据号
		/// </summary>
		[StringLength(20)]
		public string DUIYINGDJH { get; set; }
		/// <summary>
		/// 出入库方式
		/// </summary>
		[StringLength(4)]
		public string CHURUKFS { get; set; }
		/// <summary>
		/// 效期
		/// </summary>
		public DateTime? XIAOQI { get; set; }
		/// <summary>
		/// 实发数量
		/// </summary>
		public decimal? SHIFASL { get; set; }
		/// <summary>
		/// 被请领应用的库存数量
		/// </summary>
		public decimal? BEIQINGLYYKCSL { get; set; }
		/// <summary>
		/// 进价
		/// </summary>
		public decimal? JINJIA { get; set; }
		/// <summary>
		/// 拒绝原因
		/// </summary>
		[StringLength(4)]
		public string JUJUEYY { get; set; }
		private decimal? _YUANQINGLSL;
		/// <summary>
		/// 原请领数量
		/// </summary>
		public decimal? YUANQINGLSL { get{ return _YUANQINGLSL; } set{ _YUANQINGLSL = value ?? 0; } }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? YAOPINXQ { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(6)]
		public string ZHANGBULB { get; set; }
	}
}
