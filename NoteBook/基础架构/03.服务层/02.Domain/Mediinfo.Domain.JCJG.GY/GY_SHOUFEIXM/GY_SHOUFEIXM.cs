using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_SHOUFEIXM")]
	public partial class GY_SHOUFEIXM : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 收费项目ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string SHOUFEIXMID { get; set; }
		/// <summary>
		/// 收费项目名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string SHOUFEIXMMC { get; set; }
		/// <summary>
		/// 收费项目内涵
		/// </summary>
		[StringLength(500)]
		public string SHOUFEIXMNH { get; set; }
		/// <summary>
		/// 父类项目
		/// </summary>
		[StringLength(10)]
		public string FULEIXM { get; set; }
		/// <summary>
		/// 核算项目
		/// </summary>
		[Required]
		[StringLength(10)]
		public string HESUANXM { get; set; }
		/// <summary>
		/// 项目类型
		/// </summary>
		[Required]
		[StringLength(4)]
		public string XIANGMULX { get; set; }
		/// <summary>
		/// 病案收费项目
		/// </summary>
		[StringLength(10)]
		public string BINGANSFXM { get; set; }
		/// <summary>
		/// 会计科目
		/// </summary>
		[StringLength(10)]
		public string KUAIJIKM { get; set; }
		/// <summary>
		/// 套餐标志
		/// </summary>
		[Required]
		public int? TAOCANBZ { get; set; }
		private int? _MOJIBZ;
		/// <summary>
		/// 末级标志
		/// </summary>
		public int? MOJIBZ { get{ return _MOJIBZ; } set{ _MOJIBZ = value ?? 0; } }
		/// <summary>
		/// 计价单位
		/// </summary>
		[Required]
		[StringLength(20)]
		public string JIJIADW { get; set; }
		/// <summary>
		/// 单价1
		/// </summary>
		[Required]
		public decimal? DANJIA1 { get; set; }
		/// <summary>
		/// 单价2
		/// </summary>
		public decimal? DANJIA2 { get; set; }
		private decimal? _DANJIA3;
		/// <summary>
		/// 单价3
		/// </summary>
		[Required]
		public decimal? DANJIA3 { get{ return _DANJIA3; } set{ _DANJIA3 = value ?? 0; } }
		private decimal? _DANJIA4;
		/// <summary>
		/// 单价4
		/// </summary>
		[Required]
		public decimal? DANJIA4 { get{ return _DANJIA4; } set{ _DANJIA4 = value ?? 0; } }
		private decimal? _DANJIA5;
		/// <summary>
		/// 单价5
		/// </summary>
		[Required]
		public decimal? DANJIA5 { get{ return _DANJIA5; } set{ _DANJIA5 = value ?? 0; } }
		/// <summary>
		/// 标准编码
		/// </summary>
		[StringLength(20)]
		public string BIAOZHUNBM { get; set; }
		/// <summary>
		/// 输入码1
		/// </summary>
		[StringLength(10)]
		public string SHURUMA1 { get; set; }
		/// <summary>
		/// 输入码2
		/// </summary>
		[StringLength(10)]
		public string SHURUMA2 { get; set; }
		/// <summary>
		/// 输入码3
		/// </summary>
		[StringLength(10)]
		public string SHURUMA3 { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		[StringLength(500)]
		public string BEIZHU { get; set; }
		/// <summary>
		/// 院区使用
		/// </summary>
		[Required]
		[StringLength(10)]
		public string YUANQUSY { get; set; }
		/// <summary>
		/// 住院使用
		/// </summary>
		[Required]
		public int? ZHUYUANSY { get; set; }
		/// <summary>
		/// 门诊使用
		/// </summary>
		[Required]
		public int? MENZHENSY { get; set; }
		/// <summary>
		/// 急诊使用
		/// </summary>
		public int? JIZHENSY { get; set; }
		/// <summary>
		/// 提示说明
		/// </summary>
		[StringLength(500)]
		public string TISHISM { get; set; }
		/// <summary>
		/// 启用日期
		/// </summary>
		[Required]
		public DateTime? QIYONGRQ { get; set; }
		/// <summary>
		/// 停用日期
		/// </summary>
		public DateTime? TINGYONGRQ { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		[Required]
		public int? ZUOFEIBZ { get; set; }
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
		/// 样本类型
		/// </summary>
		[StringLength(10)]
		public string YANGBENLX { get; set; }
		/// <summary>
		/// 采集部位
		/// </summary>
		[StringLength(10)]
		public string CAIJIBW { get; set; }
		/// <summary>
		/// 取报告地点
		/// </summary>
		[StringLength(50)]
		public string QUBAOGDD { get; set; }
		/// <summary>
		/// 取报告时间
		/// </summary>
		[StringLength(50)]
		public string QUBAOGSJ { get; set; }
		/// <summary>
		/// 性质属性
		/// </summary>
		[StringLength(50)]
		public string XINGZHISX { get; set; }
		/// <summary>
		/// 医保等级
		/// </summary>
		[StringLength(10)]
		public string YIBAODJ { get; set; }
		/// <summary>
		/// 项目编码
		/// </summary>
		[StringLength(20)]
		public string XIANGMUBM { get; set; }
		/// <summary>
		/// 门诊发票项目
		/// </summary>
		[StringLength(10)]
		public string MENZHENFPXM { get; set; }
		/// <summary>
		/// 住院发票项目
		/// </summary>
		[StringLength(10)]
		public string ZHUYUANFPXM { get; set; }
		/// <summary>
		/// 字符01
		/// </summary>
		[StringLength(100)]
		public string ZIFU01 { get; set; }
		/// <summary>
		/// 字符02
		/// </summary>
		[StringLength(100)]
		public string ZIFU02 { get; set; }
		/// <summary>
		/// 字符03
		/// </summary>
		[StringLength(100)]
		public string ZIFU03 { get; set; }
		/// <summary>
		/// 字符04
		/// </summary>
		[StringLength(100)]
		public string ZIFU04 { get; set; }
		/// <summary>
		/// 字符05
		/// </summary>
		[StringLength(100)]
		public string ZIFU05 { get; set; }
		/// <summary>
		/// 字符06
		/// </summary>
		[StringLength(100)]
		public string ZIFU06 { get; set; }
		/// <summary>
		/// 字符07
		/// </summary>
		[StringLength(100)]
		public string ZIFU07 { get; set; }
		/// <summary>
		/// 字符08
		/// </summary>
		[StringLength(100)]
		public string ZIFU08 { get; set; }
		/// <summary>
		/// 字符09
		/// </summary>
		[StringLength(100)]
		public string ZIFU09 { get; set; }
		/// <summary>
		/// 字符10
		/// </summary>
		[StringLength(100)]
		public string ZIFU10 { get; set; }
		/// <summary>
		/// 标志位1
		/// </summary>
		public int? BIAOZHI1 { get; set; }
		/// <summary>
		/// 标志位2
		/// </summary>
		public int? BIAOZHI2 { get; set; }
		/// <summary>
		/// 数字1
		/// </summary>
		public decimal? SHUZI1 { get; set; }
		/// <summary>
		/// 数字2
		/// </summary>
		public decimal? SHUZI2 { get; set; }
		/// <summary>
		/// 数字3
		/// </summary>
		public decimal? SHUZI3 { get; set; }
		/// <summary>
		/// 数字4
		/// </summary>
		public decimal? SHUZI4 { get; set; }
		/// <summary>
		/// 数字5
		/// </summary>
		public decimal? SHUZI5 { get; set; }
		private string _WAIBUXMID;
		/// <summary>
		/// 外部项目ID
		/// </summary>
		[StringLength(20)]
		public string WAIBUXMID { get{ return _WAIBUXMID; } set{ _WAIBUXMID = value ?? "0"; } }
		/// <summary>
		/// 物资序号
		/// </summary>
		[StringLength(10)]
		public string WUZIXH { get; set; }
		/// <summary>
		/// 外部类别
		/// </summary>
		[StringLength(4)]
		public string WAIBULB { get; set; }
		/// <summary>
		/// 病案费用大类
		/// </summary>
		[StringLength(20)]
		public string BINGANFYDL { get; set; }
		/// <summary>
		/// 病案费用小类
		/// </summary>
		[StringLength(20)]
		public string BINGANFYXL { get; set; }
		private string _XIANZHISYBZ;
		/// <summary>
		/// 
		/// </summary>
		[StringLength(100)]
		public string XIANZHISYBZ { get{ return _XIANZHISYBZ; } set{ _XIANZHISYBZ = value ?? "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"; } }
		/// <summary>
		/// 医保不上传属性HR3-11061(150310)
		/// </summary>
		[StringLength(100)]
		public string YIBAOBCSSX { get; set; }
		private int? _ZHUYUANTFQXKZBZ;
		/// <summary>
		/// 住院退费权限控制标志HR3-13047(169375)
		/// </summary>
		public int? ZHUYUANTFQXKZBZ { get{ return _ZHUYUANTFQXKZBZ; } set{ _ZHUYUANTFQXKZBZ = value ?? 0; } }
		private int? _DUIZHAOBZ;
		/// <summary>
		/// 对照标志HR3-14865(187699)
		/// </summary>
		public int? DUIZHAOBZ { get{ return _DUIZHAOBZ; } set{ _DUIZHAOBZ = value ?? 0; } }
		/// <summary>
		/// 默认频次HR3-18747(216004)
		/// </summary>
		[StringLength(20)]
		public string MORENPC { get; set; }
		/// <summary>
		/// 医生指示HR3-18747(216004)
		/// </summary>
		[StringLength(500)]
		public string YISHENGZS { get; set; }
		/// <summary>
		/// 字符11
		/// </summary>
		[StringLength(100)]
		public string ZIFU11 { get; set; }
		/// <summary>
		/// 字符12
		/// </summary>
		[StringLength(100)]
		public string ZIFU12 { get; set; }
		/// <summary>
		/// 字符13
		/// </summary>
		[StringLength(100)]
		public string ZIFU13 { get; set; }
		/// <summary>
		/// 字符14
		/// </summary>
		[StringLength(200)]
		public string ZIFU14 { get; set; }
		/// <summary>
		/// 字符15
		/// </summary>
		[StringLength(200)]
		public string ZIFU15 { get; set; }
		/// <summary>
		/// 字符16
		/// </summary>
		[StringLength(200)]
		public string ZIFU16 { get; set; }
		/// <summary>
		/// 字符17
		/// </summary>
		[StringLength(200)]
		public string ZIFU17 { get; set; }
		/// <summary>
		/// 字符18
		/// </summary>
		[StringLength(500)]
		public string ZIFU18 { get; set; }
		/// <summary>
		/// 字符19
		/// </summary>
		[StringLength(500)]
		public string ZIFU19 { get; set; }
		/// <summary>
		/// 字符20
		/// </summary>
		[StringLength(500)]
		public string ZIFU20 { get; set; }
		/// <summary>
		/// 物价代码FOR HR3-23877(262362)
		/// </summary>
		[StringLength(20)]
		public string WUJIADM { get; set; }
		private int? _XIANGMUHSBZ;
		/// <summary>
		/// HR3-24975(269103)项目核算参与标志
		/// </summary>
		public int? XIANGMUHSBZ { get{ return _XIANGMUHSBZ; } set{ _XIANGMUHSBZ = value ?? 0; } }
		private string _CHANDILB;
		/// <summary>
		/// 产地类别
		/// </summary>
		[StringLength(200)]
		public string CHANDILB { get{ return _CHANDILB; } set{ _CHANDILB = value ?? "1"; } }
		private decimal? _ERTONGJG1;
		/// <summary>
		/// 儿童价格1
		/// </summary>
		public decimal? ERTONGJG1 { get{ return _ERTONGJG1; } set{ _ERTONGJG1 = value ?? 0; } }
		private decimal? _ERTONGJG2;
		/// <summary>
		/// 儿童价格2
		/// </summary>
		public decimal? ERTONGJG2 { get{ return _ERTONGJG2; } set{ _ERTONGJG2 = value ?? 0; } }
		private decimal? _ERTONGJG3;
		/// <summary>
		/// 儿童价格3
		/// </summary>
		public decimal? ERTONGJG3 { get{ return _ERTONGJG3; } set{ _ERTONGJG3 = value ?? 0; } }
		private decimal? _ERTONGJG4;
		/// <summary>
		/// 儿童价格4
		/// </summary>
		public decimal? ERTONGJG4 { get{ return _ERTONGJG4; } set{ _ERTONGJG4 = value ?? 0; } }
		private decimal? _ERTONGJG5;
		/// <summary>
		/// 儿童价格5
		/// </summary>
		public decimal? ERTONGJG5 { get{ return _ERTONGJG5; } set{ _ERTONGJG5 = value ?? 0; } }
		/// <summary>
		/// 单价HR3-37692
		/// </summary>
		public decimal? DANJIA6 { get; set; }
		/// <summary>
		/// 单价HR3-37692
		/// </summary>
		public decimal? DANJIA7 { get; set; }
		/// <summary>
		/// 单价HR3-37692
		/// </summary>
		public decimal? DANJIA8 { get; set; }
		/// <summary>
		/// 单价HR3-37692
		/// </summary>
		public decimal? DANJIA9 { get; set; }
		/// <summary>
		/// 单价HR3-37692
		/// </summary>
		public decimal? DANJIA10 { get; set; }
		/// <summary>
		/// 单价HR3-37692
		/// </summary>
		public decimal? DANJIA11 { get; set; }
		/// <summary>
		/// 单价HR3-37692
		/// </summary>
		public decimal? DANJIA12 { get; set; }
		/// <summary>
		/// 单价HR3-37692
		/// </summary>
		public decimal? DANJIA13 { get; set; }
		/// <summary>
		/// 单价HR3-37692
		/// </summary>
		public decimal? DANJIA14 { get; set; }
		/// <summary>
		/// 单价HR3-37692
		/// </summary>
		public decimal? DANJIA15 { get; set; }
		/// <summary>
		/// 单价HR3-37692
		/// </summary>
		public decimal? DANJIA16 { get; set; }
		/// <summary>
		/// 单价HR3-37692
		/// </summary>
		public decimal? DANJIA17 { get; set; }
		/// <summary>
		/// 单价HR3-37692
		/// </summary>
		public decimal? DANJIA18 { get; set; }
		/// <summary>
		/// 单价HR3-37692
		/// </summary>
		public decimal? DANJIA19 { get; set; }
		/// <summary>
		/// 单价HR3-37692
		/// </summary>
		public decimal? DANJIA20 { get; set; }
	}
}
