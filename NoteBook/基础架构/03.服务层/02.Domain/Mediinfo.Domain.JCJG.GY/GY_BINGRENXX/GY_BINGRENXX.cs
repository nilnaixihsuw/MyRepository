using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_BINGRENXX")]
	public partial class GY_BINGRENXX : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 病人ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(100)]
		public string BINGRENID { get; set; }
		/// <summary>
		/// 社保编号
		/// </summary>
		[StringLength(50)]
		public string SHEBAOBH { get; set; }
		/// <summary>
		/// 医保卡号
		/// </summary>
		[StringLength(50)]
		public string YIBAOKH { get; set; }
		/// <summary>
		/// 就诊卡号
		/// </summary>
		[StringLength(100)]
		public string JIUZHENKH { get; set; }
		/// <summary>
		/// 社区编号
		/// </summary>
		[StringLength(50)]
		public string SHEQUBH { get; set; }
		/// <summary>
		/// 费用类别
		/// </summary>
		[StringLength(10)]
		public string FEIYONGLB { get; set; }
		/// <summary>
		/// 费用性质
		/// </summary>
		[StringLength(10)]
		public string FEIYONGXZ { get; set; }
		/// <summary>
		/// 优惠类别
		/// </summary>
		[StringLength(4)]
		public string YOUHUILB { get; set; }
		/// <summary>
		/// 公费证号
		/// </summary>
		[StringLength(20)]
		public string GONGFEIZH { get; set; }
		/// <summary>
		/// 公费单位
		/// </summary>
		[StringLength(20)]
		public string GONGFEIDW { get; set; }
		/// <summary>
		/// 公费单位名称
		/// </summary>
		[StringLength(100)]
		public string GONGFEIDWMC { get; set; }
		/// <summary>
		/// 姓名
		/// </summary>
		[Required]
		[StringLength(50)]
		public string XINGMING { get; set; }
		/// <summary>
		/// 输入码1
		/// </summary>
		[StringLength(10)]
		public string SHURUMA1 { get; set; }
		/// <summary>
		/// 输入码3
		/// </summary>
		[StringLength(10)]
		public string SHURUMA3 { get; set; }
		/// <summary>
		/// 输入码2
		/// </summary>
		[StringLength(10)]
		public string SHURUMA2 { get; set; }
		/// <summary>
		/// 性别
		/// </summary>
		[StringLength(4)]
		public string XINGBIE { get; set; }
		/// <summary>
		/// 年龄单位
		/// </summary>
		[StringLength(4)]
		public string NIANLINGDW { get; set; }
		/// <summary>
		/// 身份证号
		/// </summary>
		[StringLength(20)]
		public string SHENFENZH { get; set; }
		/// <summary>
		/// 出生日期
		/// </summary>
		public DateTime? CHUSHENGRQ { get; set; }
		/// <summary>
		/// 工作单位
		/// </summary>
		[StringLength(100)]
		public string GONGZUODW { get; set; }
		/// <summary>
		/// 单位电话
		/// </summary>
		[StringLength(50)]
		public string DANWEIDH { get; set; }
		/// <summary>
		/// 单位邮编
		/// </summary>
		[StringLength(10)]
		public string DANWEIYB { get; set; }
		/// <summary>
		/// 家庭地址
		/// </summary>
		[StringLength(200)]
		public string JIATINGDZ { get; set; }
		/// <summary>
		/// 家庭电话
		/// </summary>
		[StringLength(50)]
		public string JIATINGDH { get; set; }
		/// <summary>
		/// 家庭邮编
		/// </summary>
		[StringLength(10)]
		public string JIATINGYB { get; set; }
		/// <summary>
		/// 血型
		/// </summary>
		[StringLength(10)]
		public string XUEXING { get; set; }
		/// <summary>
		/// 婚姻
		/// </summary>
		[StringLength(10)]
		public string HUNYIN { get; set; }
		/// <summary>
		/// 职业
		/// </summary>
		[StringLength(100)]
		public string ZHIYE { get; set; }
		/// <summary>
		/// 国籍
		/// </summary>
		[StringLength(100)]
		public string GUOJI { get; set; }
		/// <summary>
		/// 民族
		/// </summary>
		[StringLength(100)]
		public string MINZU { get; set; }
		/// <summary>
		/// 省份
		/// </summary>
		[StringLength(100)]
		public string SHENGFEN { get; set; }
		/// <summary>
		/// 乡镇街道
		/// </summary>
		[StringLength(100)]
		public string XIANGZHENJD { get; set; }
		/// <summary>
		/// 市地区
		/// </summary>
		[StringLength(100)]
		public string SHIDIQU { get; set; }
		/// <summary>
		/// 籍贯
		/// </summary>
		[StringLength(100)]
		public string JIGUAN { get; set; }
		/// <summary>
		/// 出生地
		/// </summary>
		[StringLength(100)]
		public string CHUSHENGDI { get; set; }
		/// <summary>
		/// 邮编
		/// </summary>
		[StringLength(10)]
		public string YOUBIAN { get; set; }
		/// <summary>
		/// 联系人
		/// </summary>
		[StringLength(20)]
		public string LIANXIREN { get; set; }
		/// <summary>
		/// 关系
		/// </summary>
		[StringLength(10)]
		public string GUANXI { get; set; }
		/// <summary>
		/// 联系人地址
		/// </summary>
		[StringLength(100)]
		public string LIANXIRDZ { get; set; }
		/// <summary>
		/// 联系人电话
		/// </summary>
		[StringLength(50)]
		public string LIANXIRDH { get; set; }
		/// <summary>
		/// 联系人邮编
		/// </summary>
		[StringLength(10)]
		public string LIANXIRYB { get; set; }
		/// <summary>
		/// 既往史
		/// </summary>
		[StringLength(500)]
		public string JIWANGSHI { get; set; }
		/// <summary>
		/// 过敏史
		/// </summary>
		[StringLength(500)]
		public string GUOMINSHI { get; set; }
		/// <summary>
		/// 皮试结果
		/// </summary>
		[StringLength(500)]
		public string PISHIJG { get; set; }
		private string _JIANDANGREN;
		/// <summary>
		/// 建档人
		/// </summary>
		[Required]
		[StringLength(10)]
		public string JIANDANGREN { get{ return _JIANDANGREN; } set{ _JIANDANGREN = value ?? "1"; } }
		/// <summary>
		/// 建档日期
		/// </summary>
		public DateTime? JIANDANGRQ { get; set; }
		private string _XIUGAIREN;
		/// <summary>
		/// 修改人
		/// </summary>
		[Required]
		[StringLength(10)]
		public string XIUGAIREN { get{ return _XIUGAIREN; } set{ _XIUGAIREN = value ?? "1"; } }
		/// <summary>
		/// 修改时间
		/// </summary>
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime? XIUGAISJ { get; set; }
		/// <summary>
		/// 门诊次数
		/// </summary>
		public int? MENZHENCS { get; set; }
		/// <summary>
		/// 住院次数
		/// </summary>
		public int? ZHUYUANCS { get; set; }
		/// <summary>
		/// 病人住院ID
		/// </summary>
		[StringLength(10)]
		public string BINGRENZYID { get; set; }
		/// <summary>
		/// 记录来源
		/// </summary>
		[StringLength(4)]
		public string JILULY { get; set; }
		/// <summary>
		/// 区域
		/// </summary>
		[StringLength(4)]
		public string QUYU { get; set; }
		/// <summary>
		/// 个人编号
		/// </summary>
		[StringLength(50)]
		public string GERENBH { get; set; }
		/// <summary>
		/// 医保卡ID
		/// </summary>
		[StringLength(50)]
		public string YIBAOKID { get; set; }
		/// <summary>
		/// ICXX
		/// </summary>
		[StringLength(2000)]
		public string ICXX { get; set; }
		/// <summary>
		/// 器官移植标志
		/// </summary>
		public int? QIGUANYZBZ { get; set; }
		/// <summary>
		/// 资金账户启用标志
		/// </summary>
		public int? ZIJINZHQYBZ { get; set; }
		/// <summary>
		/// 关联病人ID
		/// </summary>
		[StringLength(10)]
		public string GUANLIANBRID { get; set; }
		/// <summary>
		/// 停用标志
		/// </summary>
		public int? TINGYONGBZ { get; set; }
		/// <summary>
		/// 绿色通道标志
		/// </summary>
		public int? LVSETDBZ { get; set; }
		/// <summary>
		/// 担保类型
		/// </summary>
		[StringLength(10)]
		public string DANBAOLX { get; set; }
		private string _XINGMINGQP;
		/// <summary>
		/// 姓名全拼
		/// </summary>
		[StringLength(100)]
		public string XINGMINGQP { get{ return _XINGMINGQP; } set{ _XINGMINGQP = value ?? "NULL"; } }
		/// <summary>
		/// 母亲姓名
		/// </summary>
		[StringLength(20)]
		public string MUQINXM { get; set; }
		/// <summary>
		/// 母亲姓名全拼
		/// </summary>
		[StringLength(100)]
		public string MUQINXMQP { get; set; }
		/// <summary>
		/// 家庭地址所在地区
		/// </summary>
		[StringLength(50)]
		public string JIATINGDZSZDQ { get; set; }
		/// <summary>
		/// 单位地址所在地区
		/// </summary>
		[StringLength(50)]
		public string DANWEIDZSZDQ { get; set; }
		/// <summary>
		/// 证件类型
		/// </summary>
		[StringLength(10)]
		public string ZHENGJIANLX { get; set; }
		/// <summary>
		/// 病案号
		/// </summary>
		[StringLength(10)]
		public string BINGANHAO { get; set; }
		/// <summary>
		/// 病理生理状态
		/// </summary>
		[StringLength(10)]
		public string BINGLISLZT { get; set; }
		/// <summary>
		/// 身份类别
		/// </summary>
		[StringLength(20)]
		public string SHENFENLB { get; set; }
		/// <summary>
		/// 在职标志
		/// </summary>
		[StringLength(10)]
		public string ZAIZHIBZ { get; set; }
		/// <summary>
		/// 本人电话
		/// </summary>
		[StringLength(20)]
		public string DIANHUA { get; set; }
		/// <summary>
		/// 手机
		/// </summary>
		[StringLength(20)]
		public string SHOUJI { get; set; }
		/// <summary>
		/// 电子邮件
		/// </summary>
		[StringLength(50)]
		public string EMAIL { get; set; }
		/// <summary>
		/// 单位地址
		/// </summary>
		[StringLength(100)]
		public string DANWEIDZ { get; set; }
		/// <summary>
		/// 户口地址
		/// </summary>
		[StringLength(100)]
		public string HUKOUDZ { get; set; }
		/// <summary>
		/// 初诊科室
		/// </summary>
		[StringLength(10)]
		public string CHUZHENKS { get; set; }
		/// <summary>
		/// 保密级别
		/// </summary>
		[StringLength(10)]
		public string BAOMIJB { get; set; }
		/// <summary>
		/// 欠费标志
		/// </summary>
		[StringLength(10)]
		public string QIANFEIBZ { get; set; }
		/// <summary>
		/// 欠费日期
		/// </summary>
		public DateTime? QIANFEIRQ { get; set; }
		/// <summary>
		/// 性别代码
		/// </summary>
		[StringLength(10)]
		public string XINGBIEDM { get; set; }
		/// <summary>
		/// 婚姻代码
		/// </summary>
		[StringLength(10)]
		public string HUNYINDM { get; set; }
		/// <summary>
		/// 职业代码
		/// </summary>
		[StringLength(10)]
		public string ZHIYEDM { get; set; }
		/// <summary>
		/// 国籍代码
		/// </summary>
		[StringLength(10)]
		public string GUOJIDM { get; set; }
		/// <summary>
		/// 民族代码
		/// </summary>
		[StringLength(10)]
		public string MINZUDM { get; set; }
		/// <summary>
		/// 省份代码
		/// </summary>
		[StringLength(10)]
		public string SHENGFENDM { get; set; }
		/// <summary>
		/// 籍贯代码
		/// </summary>
		[StringLength(10)]
		public string JIGUANGDM { get; set; }
		/// <summary>
		/// 出生地代码
		/// </summary>
		[StringLength(10)]
		public string CHUSHENGDDM { get; set; }
		/// <summary>
		/// 家庭地址所在地区代码
		/// </summary>
		[StringLength(10)]
		public string JIATINGDZSZDQDM { get; set; }
		/// <summary>
		/// 单位地址所在地区代码
		/// </summary>
		[StringLength(10)]
		public string DANWEIDZSZDQDM { get; set; }
		/// <summary>
		/// 关系名称
		/// </summary>
		[StringLength(50)]
		public string GUANXIMC { get; set; }
		/// <summary>
		/// 医保病人信息
		/// </summary>
		[StringLength(2000)]
		public string YIBAOBRXX { get; set; }
		/// <summary>
		/// 应急收费标志
		/// </summary>
		public long? YINGJISFBZ { get; set; }
		/// <summary>
		/// 照片文件
		/// </summary>
		[StringLength(50)]
		public string ZHAOPIANWJ { get; set; }
		/// <summary>
		/// 医联码
		/// </summary>
		[StringLength(20)]
		public string YILIANMA { get; set; }
		/// <summary>
		/// 1-天长用的标志,其它正常
		/// </summary>
		public int? TESHUBZ { get; set; }
		/// <summary>
		/// 健康卡号
		/// </summary>
		[StringLength(100)]
		public string JIANKANGKH { get; set; }
		/// <summary>
		/// HR3-36758)最后就诊日期
		/// </summary>
		public DateTime? ZUIHOUJZRQ { get; set; }
		/// <summary>
		/// 身高CM
		/// </summary>
		public decimal? SHENGAO { get; set; }
		/// <summary>
		/// 体重KG
		/// </summary>
		public decimal? TIZHONG { get; set; }
		/// <summary>
		/// 腰围CM
		/// </summary>
		public decimal? YAOWEI { get; set; }
		/// <summary>
		/// 高危因素
		/// </summary>
		[StringLength(200)]
		public string GAOWEIYS { get; set; }
		/// <summary>
		/// 年收入
		/// </summary>
		[StringLength(200)]
		public string NIANSHOURU { get; set; }
		/// <summary>
		/// 家庭人口
		/// </summary>
		public decimal? JIATINGRK { get; set; }
		/// <summary>
		/// 优惠类别_挂号
		/// </summary>
		[StringLength(4)]
		public string YOUHUILBGH { get; set; }
		/// <summary>
		/// 疾病分类HB3-15902
		/// </summary>
		[StringLength(50)]
		public string JIBINGFL { get; set; }
		/// <summary>
		/// 账户状态HR3-12735，一百位，依次对应GY_ZHUANGHULB的ZHANGHUID,取值同GY_ZHUANGHUXXMX里ZHUANGTAI
		/// </summary>
		[StringLength(100)]
		public string ZHANGHUZT { get; set; }
		/// <summary>
		/// 控制属性位置HR3-13146(170328)恶心需求
		/// </summary>
		public int? KONGZHISXWZ { get; set; }
		/// <summary>
		/// 预授权标志HR3-13604(174381),
		/// </summary>
		public int? YUSHOUQBZ { get; set; }
		/// <summary>
		/// 预授权金额HR3-13604(174381),
		/// </summary>
		public decimal? YUSHOUQJE { get; set; }
		/// <summary>
		/// 消息来源
		/// </summary>
		[StringLength(10)]
		public string XIAOXILY { get; set; }
		/// <summary>
		/// 绿色通道日期HR3-14958
		/// </summary>
		public DateTime? LVSETDRQ { get; set; }
		/// <summary>
		/// 绿色通道结束日期HR3-14958
		/// </summary>
		public DateTime? LVSETDJSRQ { get; set; }
		/// <summary>
		/// 联系人身份证号
		/// </summary>
		[StringLength(50)]
		public string LIANXIRENSFZH { get; set; }
		private string _CISHANJZBZ;
		/// <summary>
		/// 慈善救助标志
		/// </summary>
		[StringLength(1)]
		public string CISHANJZBZ { get{ return _CISHANJZBZ; } set{ _CISHANJZBZ = value ?? "0"; } }
		/// <summary>
		/// 残疾证号
		/// </summary>
		[StringLength(20)]
		public string CANJIZH { get; set; }
		/// <summary>
		/// 虚拟账户ID HR3-16951
		/// </summary>
		[StringLength(50)]
		public string ZHANGHUID { get; set; }
		/// <summary>
		/// 银行卡绑定标志 HR3-16951
		/// </summary>
		public int? YINHANGKBDBZ { get; set; }
		/// <summary>
		/// 县区HR3-17804
		/// </summary>
		[StringLength(100)]
		public string XIANQU { get; set; }
		/// <summary>
		/// 规定病种审批编号HR3-18375(213399)
		/// </summary>
		[StringLength(50)]
		public string GUIDINGBZSPBH { get; set; }
		/// <summary>
		/// 生育保险审批编号HR3-18375(213399)
		/// </summary>
		[StringLength(50)]
		public string SHENGYUBXSPBH { get; set; }
		/// <summary>
		/// 产检申请单号HR3-18819
		/// </summary>
		[StringLength(50)]
		public string CHANJIANSQDH { get; set; }
		/// <summary>
		/// 证件归属HR3-20319
		/// </summary>
		[StringLength(20)]
		public string ZHENGJIANGS { get; set; }
		/// <summary>
		/// 健康卡信息HR3-22071
		/// </summary>
		[StringLength(1000)]
		public string JIANKANGKXX { get; set; }
		/// <summary>
		/// 户口省份HR3-22037(250447)
		/// </summary>
		[StringLength(100)]
		public string HUKOUSF { get; set; }
		/// <summary>
		/// 户口市地区HR3-22037(250447)
		/// </summary>
		[StringLength(100)]
		public string HUKOUSDQ { get; set; }
		/// <summary>
		/// 户口县区HR3-22037(250447)
		/// </summary>
		[StringLength(100)]
		public string HUKOUXQ { get; set; }
		/// <summary>
		/// 户口乡镇街道HR3-22037(250447)
		/// </summary>
		[StringLength(100)]
		public string HUKOUXZJD { get; set; }
		/// <summary>
		/// 户口地址类别HR3-22037(250447)
		/// </summary>
		[StringLength(10)]
		public string HUKOUDZLB { get; set; }
		/// <summary>
		/// 家庭地址类别HR3-22037(250447)
		/// </summary>
		[StringLength(10)]
		public string JIATINGDZLB { get; set; }
		/// <summary>
		/// 强制自费标志HR3-23173(257763)
		/// </summary>
		public int? QIANGZHIZFBZ { get; set; }
		/// <summary>
		/// 婴儿标志HR3-23581(260432)
		/// </summary>
		public int? YINGERBZ { get; set; }
		/// <summary>
		/// 母亲身份证号HR3-23581(260432)
		/// </summary>
		[StringLength(20)]
		public string MUQINSFZH { get; set; }
		/// <summary>
		/// 产次HR3-23581(260432)
		/// </summary>
		[StringLength(10)]
		public string CHANCI { get; set; }
		/// <summary>
		/// 胎次HR3-23581(260432)
		/// </summary>
		[StringLength(10)]
		public string TAICI { get; set; }
		/// <summary>
		/// 职工工号HR3-24534(266257)
		/// </summary>
		[StringLength(20)]
		public string ZHIGONGGH { get; set; }
		/// <summary>
		/// 身份确认标志HR3-24922(268820)
		/// </summary>
		public int? SHENFENQRBZ { get; set; }
		/// <summary>
		/// 取现限制标志
		/// </summary>
		public int? QUXIANXZBZ { get; set; }
		/// <summary>
		/// 隐私控制医生等级HR3-26337
		/// </summary>
		[StringLength(10)]
		public string YINSIKZYSDJ { get; set; }
		/// <summary>
		/// 肺结核标志 HR3-26644(279924)
		/// </summary>
		public int? FEIJIEHEBZ { get; set; }
		/// <summary>
		/// 肺结核就诊日期 HR3-26644(279924)
		/// </summary>
		public DateTime? FEIJIEHEJZRQ { get; set; }
		/// <summary>
		/// 肺结核病人类型 HR3-26644(279924)
		/// </summary>
		[StringLength(10)]
		public string BINGRENLX { get; set; }
		/// <summary>
		/// 肺结核病种类型 HR3-26644(279924)
		/// </summary>
		[StringLength(10)]
		public string BINGZHONGLX { get; set; }
		/// <summary>
		/// 肺结核阶段 HR3-26644(279924)
		/// </summary>
		[StringLength(10)]
		public string JIEDUAN { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? QIANYUERQ { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(2000)]
		public string YIBAOTSBZXX { get; set; }
		/// <summary>
		/// 平安养老保险标志
		/// </summary>
		public int? PINGANYLBXBZ { get; set; }
		/// <summary>
		/// 嘉兴实名制注册返回EMPIID HR3-29464(298220)
		/// </summary>
		[StringLength(50)]
		public string EMPIID { get; set; }
		/// <summary>
		/// 黑名单标志HR3-26523(279233)
		/// </summary>
		public int? HEIMINGDANBZ { get; set; }
		/// <summary>
		/// 家庭ID 悦程
		/// </summary>
		[StringLength(10)]
		public string JIATINGID { get; set; }
		/// <summary>
		/// 末次月经时间HR3-31248(310416)
		/// </summary>
		public DateTime? MOCIYUEJINGSJ { get; set; }
		/// <summary>
		/// 病人类别HR3-31248(310416)
		/// </summary>
		[StringLength(10)]
		public string BINGRENLB { get; set; }
		/// <summary>
		/// 提示信息HR3-30957(308588)
		/// </summary>
		[StringLength(1000)]
		public string TISHIXX { get; set; }
		/// <summary>
		/// 肺结核路径记录IDHR3-34750(331888)
		/// </summary>
		[StringLength(10)]
		public string FEIJIEHLJJLID { get; set; }
		/// <summary>
		/// 银行卡号HB3-22815(349562)
		/// </summary>
		[StringLength(20)]
		public string YINHANGKH { get; set; }
		/// <summary>
		/// 生育标志HR3-37163(344121)
		/// </summary>
		public int? SHENGYUBZ { get; set; }
		/// <summary>
		/// 医保经办机构编码HR3-41206(370174)
		/// </summary>
		[StringLength(10)]
		public string YIBAOJBJGBM { get; set; }
	}
}
