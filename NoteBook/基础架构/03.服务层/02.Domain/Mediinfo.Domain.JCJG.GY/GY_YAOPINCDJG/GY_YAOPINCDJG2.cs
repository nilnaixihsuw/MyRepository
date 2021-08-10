using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_YAOPINCDJG2")]
	public partial class GY_YAOPINCDJG2 : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 价格ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JIAGEID { get; set; }
		/// <summary>
		/// 规格ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string GUIGEID { get; set; }
		/// <summary>
		/// 大规格ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string DAGUIGID { get; set; }
		/// <summary>
		/// 小规格ID
		/// </summary>
		[StringLength(10)]
		public string XIAOGUIGID { get; set; }
		/// <summary>
		/// 帐簿类别
		/// </summary>
		[Required]
		[StringLength(6)]
		public string ZHANGBULB { get; set; }
		/// <summary>
		/// 收费项目
		/// </summary>
		[StringLength(10)]
		public string SHOUFEIXM { get; set; }
		/// <summary>
		/// 药品名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string YAOPINMC { get; set; }
		/// <summary>
		/// 产地
		/// </summary>
		[Required]
		[StringLength(10)]
		public string CHANDI { get; set; }
		/// <summary>
		/// 产地名称
		/// </summary>
		[Required]
		[StringLength(100)]
		public string CHANDIMC { get; set; }
		/// <summary>
		/// 剂型
		/// </summary>
		[StringLength(10)]
		public string JIXING { get; set; }
		/// <summary>
		/// 药品类型
		/// </summary>
		[Required]
		[StringLength(4)]
		public string YAOPINLX { get; set; }
		/// <summary>
		/// 招标进价
		/// </summary>
		public decimal? ZHAOBIAOJJ { get; set; }
		/// <summary>
		/// 批发价
		/// </summary>
		public decimal? PIFAJIA { get; set; }
		/// <summary>
		/// 单价1
		/// </summary>
		[Required]
		public decimal? DANJIA1 { get; set; }
		/// <summary>
		/// 单价2
		/// </summary>
		public decimal? DANJIA2 { get; set; }
		/// <summary>
		/// 单价3
		/// </summary>
		public decimal? DANJIA3 { get; set; }
		/// <summary>
		/// 单价4
		/// </summary>
		public decimal? DANJIA4 { get; set; }
		/// <summary>
		/// 单价5
		/// </summary>
		public decimal? DANJIA5 { get; set; }
		/// <summary>
		/// 最高批发价
		/// </summary>
		public decimal? ZUIGAOPFJ { get; set; }
		/// <summary>
		/// 最高零售价
		/// </summary>
		public decimal? ZUIGAOLSJ { get; set; }
		/// <summary>
		/// 购货标志
		/// </summary>
		[Required]
		public int? GOUHUOBZ { get; set; }
		/// <summary>
		/// 查询标志
		/// </summary>
		[Required]
		public int? CHAXUNBZ { get; set; }
		/// <summary>
		/// 停用标志
		/// </summary>
		[Required]
		public int? TINGYONGBZ { get; set; }
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
		/// 注册商标
		/// </summary>
		[StringLength(50)]
		public string ZHUCESB { get; set; }
		/// <summary>
		/// 批准文号
		/// </summary>
		[StringLength(50)]
		public string PIZHUNWH { get; set; }
		/// <summary>
		/// 进口药品证号
		/// </summary>
		[StringLength(50)]
		public string JINKOUYPZH { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		[Required]
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 招标药品标志
		/// </summary>
		[Required]
		public int? ZHAOBIAOYPBZ { get; set; }
		/// <summary>
		/// GMP标志
		/// </summary>
		[Required]
		public int? GMPBZ { get; set; }
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
		/// 毒理分类
		/// </summary>
		[Required]
		[StringLength(4)]
		public string DULIFL { get; set; }
		/// <summary>
		/// 价值分类
		/// </summary>
		[Required]
		[StringLength(4)]
		public string JIAZHIFL { get; set; }
		/// <summary>
		/// 管理模式
		/// </summary>
		[Required]
		public int? GUANLIMS { get; set; }
		/// <summary>
		/// 药品规格
		/// </summary>
		[Required]
		[StringLength(50)]
		public string YAOPINGG { get; set; }
		/// <summary>
		/// 包装单位
		/// </summary>
		[Required]
		[StringLength(20)]
		public string BAOZHUANGDW { get; set; }
		/// <summary>
		/// 给药方式
		/// </summary>
		[StringLength(10)]
		public string GEIYAOFS { get; set; }
		/// <summary>
		/// 用药梯次
		/// </summary>
		[StringLength(10)]
		public string YONGYAOTC { get; set; }
		/// <summary>
		/// 最小单位
		/// </summary>
		[Required]
		[StringLength(20)]
		public string ZUIXIAODW { get; set; }
		/// <summary>
		/// 包装量
		/// </summary>
		[Required]
		public decimal? BAOZHUANGLIANG { get; set; }
		/// <summary>
		/// 皮试标志
		/// </summary>
		[Required]
		[StringLength(4)]
		public string PISHIBZ { get; set; }
		/// <summary>
		/// 抗生素限级
		/// </summary>
		[Required]
		public int? KANGSHENGSXJ { get; set; }
		/// <summary>
		/// 抗菌药限级
		/// </summary>
		[Required]
		public int? KANGJUNYXJ { get; set; }
		/// <summary>
		/// 大输液标志
		/// </summary>
		[Required]
		public int? DASHUYBZ { get; set; }
		/// <summary>
		/// 复方标志
		/// </summary>
		[Required]
		public int? FUFANGBZ { get; set; }
		/// <summary>
		/// 药库使用
		/// </summary>
		[StringLength(50)]
		public string YAOKUSY { get; set; }
		/// <summary>
		/// 门药使用
		/// </summary>
		[StringLength(50)]
		public string MENYAOSY { get; set; }
		/// <summary>
		/// 病药使用
		/// </summary>
		[StringLength(50)]
		public string BINGYAOSY { get; set; }
		/// <summary>
		/// 临床使用标志
		/// </summary>
		[Required]
		public int? LINCHUANGSYBZ { get; set; }
		/// <summary>
		/// 对应皮试药品
		/// </summary>
		[StringLength(10)]
		public string DUIYINGPSYP { get; set; }
		/// <summary>
		/// 附加收费标志
		/// </summary>
		public int? FUJIASFBZ { get; set; }
		/// <summary>
		/// 急诊用药标志
		/// </summary>
		[Required]
		public int? JIZHENYYBZ { get; set; }
		/// <summary>
		/// 平均进价
		/// </summary>
		public decimal? PINGJUNJJ { get; set; }
		/// <summary>
		/// 限量十五
		/// </summary>
		public decimal? XIANLIANG15 { get; set; }
		/// <summary>
		/// 限量七
		/// </summary>
		public decimal? XIANLIANG7 { get; set; }
		/// <summary>
		/// 限量三
		/// </summary>
		public decimal? XIANLIANG3 { get; set; }
		/// <summary>
		/// 限量三十
		/// </summary>
		public decimal? XIANLIANG30 { get; set; }
		/// <summary>
		/// 剂量
		/// </summary>
		[StringLength(20)]
		public string JILIANG { get; set; }
		/// <summary>
		/// 剂量单位
		/// </summary>
		[StringLength(20)]
		public string JILIANGDW { get; set; }
		/// <summary>
		/// 浓度
		/// </summary>
		public decimal? NONGDU { get; set; }
		/// <summary>
		/// 体积
		/// </summary>
		[StringLength(10)]
		public string TIJI { get; set; }
		/// <summary>
		/// 体积单位
		/// </summary>
		[StringLength(20)]
		public string TIJIDW { get; set; }
		/// <summary>
		/// 医保等级
		/// </summary>
		[StringLength(10)]
		public string YIBAODJ { get; set; }
		/// <summary>
		/// 药品ID
		/// </summary>
		[StringLength(10)]
		public string YAOPINID { get; set; }
		/// <summary>
		/// 服药顺序
		/// </summary>
		[StringLength(10)]
		public string FUYAOSX { get; set; }
		/// <summary>
		/// 用药简要说明
		/// </summary>
		[StringLength(50)]
		public string YONGYAOJYSM { get; set; }
		private int? _WEIJINYPBZ;
		/// <summary>
		/// 违禁药品标志
		/// </summary>
		public int? WEIJINYPBZ { get{ return _WEIJINYPBZ; } set{ _WEIJINYPBZ = value ?? 0; } }
		private int? _HUALIAOYPBZ;
		/// <summary>
		/// 化疗药品标志
		/// </summary>
		public int? HUALIAOYPBZ { get{ return _HUALIAOYPBZ; } set{ _HUALIAOYPBZ = value ?? 0; } }
		/// <summary>
		/// 打印名称
		/// </summary>
		[StringLength(100)]
		public string DAYINMC { get; set; }
		/// <summary>
		/// 门诊最大限量
		/// </summary>
		public decimal? MENZHENZDXL { get; set; }
		/// <summary>
		/// 住院最大限量
		/// </summary>
		public decimal? ZHUYUANZDXL { get; set; }
		private string _MENYAOSY_ZY;
		/// <summary>
		/// 门药使用_住院
		/// </summary>
		[StringLength(50)]
		public string MENYAOSY_ZY { get{ return _MENYAOSY_ZY; } set{ _MENYAOSY_ZY = value ?? "00000000000000000000000000000000000000000000000000"; } }
		private string _BINGYAOSY_MZ;
		/// <summary>
		/// 病药使用_门诊
		/// </summary>
		[StringLength(50)]
		public string BINGYAOSY_MZ { get{ return _BINGYAOSY_MZ; } set{ _BINGYAOSY_MZ = value ?? "00000000000000000000000000000000000000000000000000"; } }
		private string _JINGMAIPSY;
		/// <summary>
		/// 静脉配使用
		/// </summary>
		[StringLength(50)]
		public string JINGMAIPSY { get{ return _JINGMAIPSY; } set{ _JINGMAIPSY = value ?? "11111111111111111111111111111111111111111111111111"; } }
		private string _ZHIJISY;
		/// <summary>
		/// 制剂使用
		/// </summary>
		[StringLength(50)]
		public string ZHIJISY { get{ return _ZHIJISY; } set{ _ZHIJISY = value ?? "11111111111111111111111111111111111111111111111111"; } }
		/// <summary>
		/// 制剂剂型
		/// </summary>
		[StringLength(10)]
		public string ZHIJIJX { get; set; }
		private string _QITASX;
		/// <summary>
		/// 其他属性
		/// </summary>
		[StringLength(100)]
		public string QITASX { get{ return _QITASX; } set{ _QITASX = value ?? "00000000000000000000000000000000000000000000000000"; } }
		/// <summary>
		/// 皮试有效天数
		/// </summary>
		public int? PISHIYXTS { get; set; }
		/// <summary>
		/// 中药配方颗粒ID
		/// </summary>
		[StringLength(10)]
		public string ZHONGYAOPFKLID { get; set; }
		/// <summary>
		/// 对应皮试药品数量
		/// </summary>
		public int? DUIYINGPSYPSL { get; set; }
		/// <summary>
		/// 草药单复方标志
		/// </summary>
		public int? CAOYAODFFBZ { get; set; }
		/// <summary>
		/// 外部编码1
		/// </summary>
		[StringLength(50)]
		public string WAIBUBM1 { get; set; }
		/// <summary>
		/// 外部编码2
		/// </summary>
		[StringLength(50)]
		public string WAIBUBM2 { get; set; }
		/// <summary>
		/// 外部编码3
		/// </summary>
		[StringLength(50)]
		public string WAIBUBM3 { get; set; }
		private int? _MENZHENQYBZ;
		/// <summary>
		/// 门诊启用标志
		/// </summary>
		public int? MENZHENQYBZ { get{ return _MENZHENQYBZ; } set{ _MENZHENQYBZ = value ?? 1; } }
		private int? _ZHUYUANQYBZ;
		/// <summary>
		/// 住院启用标志
		/// </summary>
		public int? ZHUYUANQYBZ { get{ return _ZHUYUANQYBZ; } set{ _ZHUYUANQYBZ = value ?? 1; } }
		/// <summary>
		/// 中标号
		/// </summary>
		[StringLength(10)]
		public string ZHONGBIAOHAO { get; set; }
		/// <summary>
		/// 日常用剂量
		/// </summary>
		[StringLength(20)]
		public string RICHANGYJL { get; set; }
		/// <summary>
		/// 加价率
		/// </summary>
		public decimal? JIAJIALV { get; set; }
		/// <summary>
		/// OTC标志
		/// </summary>
		public int? OTCBZ { get; set; }
		private int? _LINGCHAJBZ;
		/// <summary>
		/// 零差价标志
		/// </summary>
		public int? LINGCHAJBZ { get{ return _LINGCHAJBZ; } set{ _LINGCHAJBZ = value ?? 0; } }
		private string _XIANZHISYBZ;
		/// <summary>
		/// 
		/// </summary>
		[StringLength(100)]
		public string XIANZHISYBZ { get{ return _XIANZHISYBZ; } set{ _XIANZHISYBZ = value ?? "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"; } }
		/// <summary>
		/// 特殊剂量
		/// </summary>
		[StringLength(20)]
		public string TESHUJL { get; set; }
		/// <summary>
		/// 单剂量
		/// </summary>
		[StringLength(20)]
		public string DANJILIANG { get; set; }
		/// <summary>
		/// 一次剂量
		/// </summary>
		[StringLength(20)]
		public string YICIJL { get; set; }
		/// <summary>
		/// 一次剂量单位
		/// </summary>
		[StringLength(20)]
		public string YICIJLDW { get; set; }
		/// <summary>
		/// 频次
		/// </summary>
		[StringLength(10)]
		public string PINCI { get; set; }
		/// <summary>
		/// 担保类型名称
		/// </summary>
		[StringLength(100)]
		public string DANBAOLXMC { get; set; }
		/// <summary>
		/// 担保类型
		/// </summary>
		[StringLength(100)]
		public string DANBAOLX { get; set; }
		/// <summary>
		/// 皮试时间
		/// </summary>
		public int? PISHISJ { get; set; }
		private string _YUANQUSY;
		/// <summary>
		/// 院区使用
		/// </summary>
		[Required]
		[StringLength(10)]
		public string YUANQUSY { get{ return _YUANQUSY; } set{ _YUANQUSY = value ?? "1000000000"; } }
		/// <summary>
		/// 基药组数HR3-17722(209413)
		/// </summary>
		[StringLength(10)]
		public string JIYAOZS { get; set; }
		/// <summary>
		/// 药理分类代码(AHFS)
		/// </summary>
		[StringLength(50)]
		public string AHFS { get; set; }
		/// <summary>
		/// 药理分类代码(ATC7)
		/// </summary>
		[StringLength(50)]
		public string ATC7 { get; set; }
		/// <summary>
		/// 配制说明
		/// </summary>
		[StringLength(4000)]
		public string PEIZHISM { get; set; }
		/// <summary>
		/// 怀孕分级
		/// </summary>
		[StringLength(4)]
		public string HUAIYUNFJ { get; set; }
		/// <summary>
		/// 替代药品
		/// </summary>
		[StringLength(200)]
		public string TIDAIYP { get; set; }
		/// <summary>
		/// 预设日数
		/// </summary>
		public int? YUSHERS { get; set; }
		/// <summary>
		/// 依年纪调整剂量
		/// </summary>
		public int? YINIANJTZJL { get; set; }
		/// <summary>
		/// 依器官调整剂量
		/// </summary>
		public int? YIQIGTZJL { get; set; }
		/// <summary>
		/// 开封后有效时数
		/// </summary>
		public int? KAIFENGHYXSS { get; set; }
		/// <summary>
		/// 输注速率(针剂专用)
		/// </summary>
		[StringLength(200)]
		public string SHUZHUSL { get; set; }
		/// <summary>
		/// 剂型分类
		/// </summary>
		[StringLength(10)]
		public string JIXINGFL { get; set; }
		/// <summary>
		/// 储存温度
		/// </summary>
		public int? CUNCHUWD { get; set; }
		/// <summary>
		/// 单位治疗剂量 (化疗专用 mg/m2)
		/// </summary>
		public decimal? DANWEIZLJL { get; set; }
		/// <summary>
		/// 药品形状
		/// </summary>
		[StringLength(10)]
		public string YAOPINXZ { get; set; }
		/// <summary>
		/// 药品颜色
		/// </summary>
		[StringLength(10)]
		public string YAOPINYS { get; set; }
		/// <summary>
		/// 药品相似度
		/// </summary>
		public int? YAOPINXSD { get; set; }
		/// <summary>
		/// 字体图形
		/// </summary>
		public int? ZITITX { get; set; }
		/// <summary>
		/// 字体图形批注
		/// </summary>
		[StringLength(200)]
		public string ZITITXPZ { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(20)]
		public string DANRIZDJL { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(20)]
		public string DANCIZDJL { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(20)]
		public string DANCIZXJL { get; set; }
		/// <summary>
		/// 免费药品标志
		/// </summary>
		public int? MIANFEIYPBZ { get; set; }
		/// <summary>
		/// 赠品标志
		/// </summary>
		public int? ZENGPINBZ { get; set; }
		/// <summary>
		/// 条形码
		/// </summary>
		[StringLength(20)]
		public string TIAOXINGMA { get; set; }
		/// <summary>
		/// 双签名限定给药方式HR3-21156(243735)
		/// </summary>
		[StringLength(100)]
		public string XIANDINGGYFS { get; set; }
		private int? _WAIPEIBZ;
		/// <summary>
		/// 外配标志(0院内 1外配 2院外)
		/// </summary>
		public int? WAIPEIBZ { get{ return _WAIPEIBZ; } set{ _WAIPEIBZ = value ?? 0; } }
		/// <summary>
		/// 医院药库包装与局平台装包转换比FOR HR3-23877(262362)
		/// </summary>
		[StringLength(10)]
		public string ZHUANHUANBL { get; set; }
		/// <summary>
		/// 药品ID FOR HR3-23877(262362)
		/// </summary>
		[StringLength(20)]
		public string YAOPINIDBM { get; set; }
		/// <summary>
		/// 滴速
		/// </summary>
		[StringLength(20)]
		public string DISU { get; set; }
		/// <summary>
		/// 上传时间HR3-29612(299133)
		/// </summary>
		public DateTime? SHANGCHUANSJ { get; set; }
		/// <summary>
		/// 化药后有效期,单位为小时HR3-32270(316744)
		/// </summary>
		public decimal? YOUXIAOQI { get; set; }
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
        /// <summary>
		/// 单价HR3-37692
		/// </summary>
		public decimal? FZFL { get; set; }
    }
}
