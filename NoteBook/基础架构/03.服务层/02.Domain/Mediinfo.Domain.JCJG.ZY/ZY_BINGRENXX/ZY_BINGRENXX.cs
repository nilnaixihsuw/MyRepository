using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.ZY
{
    [Table("ZY_BINGRENXX")]
    public partial class ZY_BINGRENXX : EntityBase, IEntityMapper
    {
        /// <summary>
        /// 病人住院ID
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string BINGRENZYID { get; set; }
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
        [StringLength(20)]
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
        [StringLength(100)]
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
        /// 区域
        /// </summary>
        [StringLength(10)]
        public string QUYU { get; set; }
        /// <summary>
        /// 外地病人标志
        /// </summary>
        [Required]
        public int? WAIDIBRBZ { get; set; }
        /// <summary>
        /// 建档人
        /// </summary>
        [StringLength(10)]
        public string JIANDANGREN { get; set; }
        /// <summary>
        /// 建档日期
        /// </summary>
        public DateTime? JIANDANGRQ { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [StringLength(10)]
        public string XIUGAIREN { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? XIUGAISJ { get; set; }
        private int? _YINGERBZ;
        /// <summary>
        /// 婴儿标志
        /// </summary>
        public int? YINGERBZ { get; set; }
        /// <summary>
        /// 住院次数
        /// </summary>
        public decimal? ZHUYUANCS { get; set; }
        /// <summary>
        /// 入院日期
        /// </summary>
        [Required]
        public DateTime? RUYUANRQ { get; set; }
        /// <summary>
        /// 预出院日期
        /// </summary>
        public DateTime? YUCHUYRQ { get; set; }
        /// <summary>
        /// 出院日期
        /// </summary>
        public DateTime? CHUYUANRQ { get; set; }
        /// <summary>
        /// 在院状态
        /// </summary>
        [StringLength(4)]
        public string ZAIYUANZT { get; set; }
        /// <summary>
        /// 入院途径
        /// </summary>
        [StringLength(4)]
        public string RUYUANTJ { get; set; }
        /// <summary>
        /// 入院科室
        /// </summary>
        [StringLength(10)]
        public string RUYUANKS { get; set; }
        /// <summary>
        /// 入院病区
        /// </summary>
        [StringLength(10)]
        public string RUYUANBQ { get; set; }
        /// <summary>
        /// 入院床位HR3-32668(319040)
        /// </summary>
        [StringLength(10)]
        public string RUYUANCW { get; set; }
        /// <summary>
        /// 当前科室
        /// </summary>
        [StringLength(10)]
        public string DANGQIANKS { get; set; }
        /// <summary>
        /// 当前病区
        /// </summary>
        [StringLength(10)]
        public string DANGQIANBQ { get; set; }
        /// <summary>
        /// 当前床位
        /// </summary>
        [StringLength(200)]
        public string DANGQIANCW { get; set; }
        /// <summary>
        /// 管理科室
        /// </summary>
        [Required]
        [StringLength(10)]
        public string GUANLIKS { get; set; }
        /// <summary>
        /// 借床标志
        /// </summary>
        public int? JIECHUANGBZ { get; set; }
        /// <summary>
        /// 离院去向
        /// </summary>
        [StringLength(4)]
        public string LIYUANQX { get; set; }
        /// <summary>
        /// 门诊诊断代码
        /// </summary>
        [StringLength(10)]
        public string MENZHENZDDM { get; set; }
        /// <summary>
        /// 门诊诊断名称
        /// </summary>
        [StringLength(100)]
        public string MENZHENZDMC { get; set; }
        /// <summary>
        /// 入院诊断代码
        /// </summary>
        [StringLength(10)]
        public string RUYUANZDDM { get; set; }
        /// <summary>
        /// 入院诊断名称
        /// </summary>
        [StringLength(100)]
        public string RUYUANZDMC { get; set; }
        /// <summary>
        /// 出院诊断代码
        /// </summary>
        [StringLength(10)]
        public string CHUYUANZDDM { get; set; }
        /// <summary>
        /// 出院诊断名称
        /// </summary>
        [StringLength(100)]
        public string CHUYUANZDMC { get; set; }
        /// <summary>
        /// 出院诊断代码2
        /// </summary>
        [StringLength(10)]
        public string CHUYUANZDDM2 { get; set; }
        /// <summary>
        /// 出院诊断名称2
        /// </summary>
        [StringLength(100)]
        public string CHUYUANZDMC2 { get; set; }
        /// <summary>
        /// 出院诊断名称3
        /// </summary>
        [StringLength(100)]
        public string CHUYUANZDMC3 { get; set; }
        /// <summary>
        /// 出院诊断代码3
        /// </summary>
        [StringLength(10)]
        public string CHUYUANZDDM3 { get; set; }
        /// <summary>
        /// 家庭病床标志
        /// </summary>
        [Required]
        public int? JIATINGBCBZ { get; set; }
        /// <summary>
        /// 病情
        /// </summary>
        [StringLength(10)]
        public string BINGQING { get; set; }
        /// <summary>
        /// 分娩
        /// </summary>
        [StringLength(10)]
        public string FENMIAN { get; set; }
        /// <summary>
        /// 上传标志
        /// </summary>
        public int? SHANGCHUANBZ { get; set; }
        /// <summary>
        /// 上传日期
        /// </summary>
        public DateTime? SHANGCHUANRQ { get; set; }
        /// <summary>
        /// 担保人
        /// </summary>
        [StringLength(500)]
        public string DANBAOREN { get; set; }
        /// <summary>
        /// 担保金额
        /// </summary>
        public decimal? DANBAOJE { get; set; }
        /// <summary>
        /// 家长姓名
        /// </summary>
        [StringLength(50)]
        public string JIAZHANGXM { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        [StringLength(10)]
        public string ZHUYUANHAO { get; set; }
        /// <summary>
        /// 病案号
        /// </summary>
        [StringLength(10)]
        public string BINGANHAO { get; set; }
        /// <summary>
        /// 病人ID
        /// </summary>
        [StringLength(100)]
        public string BINGRENID { get; set; }
        /// <summary>
        /// 结算序号
        /// </summary>
        public int? JIESUANXH { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int? NIANLING { get; set; }
        /// <summary>
        /// 门诊医师
        /// </summary>
        [StringLength(10)]
        public string MENZHENYS { get; set; }
        /// <summary>
        /// 收治医师
        /// </summary>
        [StringLength(10)]
        public string SHOUZHIYS { get; set; }
        /// <summary>
        /// 住院医师
        /// </summary>
        [StringLength(10)]
        public string ZHUYUANYS { get; set; }
        /// <summary>
        /// 主治医师
        /// </summary>
        [StringLength(200)]
        public string ZHUZHIYS { get; set; }
        /// <summary>
        /// 预约ID
        /// </summary>
        [StringLength(10)]
        public string YUYUEID { get; set; }
        private int? _SHENHEBZ;
        /// <summary>
        /// 审核标志
        /// </summary>
        public int? SHENHEBZ { get; set; }
        /// <summary>
        /// 器官移植标志
        /// </summary>
        public int? QIGUANYZBZ { get; set; }
        /// <summary>
        /// 交易流水号
        /// </summary>
        [StringLength(50)]
        public string JIAOYILSH { get; set; }
        /// <summary>
        /// 个人编号
        /// </summary>
        [StringLength(50)]
        public string GERENBH { get; set; }
        /// <summary>
        /// IC
        /// </summary>
        [StringLength(2000)]
        public string IC { get; set; }
        /// <summary>
        /// 市民卡外卡号
        /// </summary>
        [StringLength(50)]
        public string SHIMINKWKH { get; set; }
        /// <summary>
        /// 医疗组ID
        /// </summary>
        [StringLength(200)]
        public string YILIAOZID { get; set; }
        /// <summary>
        /// 医疗组名
        /// </summary>
        [StringLength(200)]
        public string YILIAOZM { get; set; }
        /// <summary>
        /// 入科日期
        /// </summary>
        public DateTime? RUKERQ { get; set; }
        private int? _RUKEBZ;
        /// <summary>
        /// 入科标志
        /// </summary>
        public int? RUKEBZ { get; set; }
        private int? _ZHANCHUANGBZ;
        /// <summary>
        /// 占床标志
        /// </summary>
        public int? ZHANCHUANGBZ { get; set; }
        /// <summary>
        /// 入院科室名称
        /// </summary>
        [StringLength(100)]
        public string RUYUANKSMC { get; set; }
        /// <summary>
        /// 入院病区名称
        /// </summary>
        [StringLength(100)]
        public string RUYUANBQMC { get; set; }
        /// <summary>
        /// 当前科室名称
        /// </summary>
        [StringLength(100)]
        public string DANGQIANKSMC { get; set; }
        /// <summary>
        /// 当前病区名称
        /// </summary>
        [StringLength(100)]
        public string DANGQIANBQMC { get; set; }
        /// <summary>
        /// 管理科室名称
        /// </summary>
        [StringLength(100)]
        public string GUANLIKSMC { get; set; }
        /// <summary>
        /// 门诊医师名称
        /// </summary>
        [StringLength(20)]
        public string MENZHENYSXM { get; set; }
        /// <summary>
        /// 收治医师名称
        /// </summary>
        [StringLength(20)]
        public string SHOUZHIYSXM { get; set; }
        /// <summary>
        /// 住院医师名称
        /// </summary>
        [StringLength(20)]
        public string ZHUYUANYSXM { get; set; }
        /// <summary>
        /// 主治医师名称
        /// </summary>
        [StringLength(200)]
        public string ZHUZHIYSXM { get; set; }
        /// <summary>
        /// 病理生理状态
        /// </summary>
        [StringLength(10)]
        public string BINGLISLZT { get; set; }
        private int? _FEIYONGXZZHBZ;
        /// <summary>
        /// 费用性质转换标志
        /// </summary>
        public int? FEIYONGXZZHBZ { get; set; }
        /// <summary>
        /// 费用性质转换人
        /// </summary>
        [StringLength(10)]
        public string FEIYONGXZZHR { get; set; }
        /// <summary>
        /// 费用性质转换日期
        /// </summary>
        public DateTime? FEIYONGXZZHRQ { get; set; }
        /// <summary>
        /// 医保医疗类别HR3-19389(221569)
        /// </summary>
        [StringLength(4)]
        public string YIBAOYLLB { get; set; }
        private int? _TESHUBZBZ;
        /// <summary>
        /// 特殊病种标志
        /// </summary>
        public int? TESHUBZBZ { get; set; }
        /// <summary>
        /// 特殊病种名称
        /// </summary>
        [StringLength(100)]
        public string TESHUBZMC { get; set; }
        /// <summary>
        /// 特殊病种编码
        /// </summary>
        [StringLength(10)]
        public string TESHUBZBM { get; set; }
        /// <summary>
        /// 门诊中医诊断代码
        /// </summary>
        [StringLength(10)]
        public string MENZHENZYZDDM { get; set; }
        /// <summary>
        /// 门诊中医诊断名称
        /// </summary>
        [StringLength(100)]
        public string MENZHENZYZDMC { get; set; }
        /// <summary>
        /// 入院中医诊断代码
        /// </summary>
        [StringLength(10)]
        public string RUYUANZYZDDM { get; set; }
        /// <summary>
        /// 入院中医诊断名称
        /// </summary>
        [StringLength(100)]
        public string RUYUANZYZDMC { get; set; }
        private int? _WAIYUANZDBZ;
        /// <summary>
        /// 入院前经外院诊断标志
        /// </summary>
        public int? WAIYUANZDBZ { get; set; }
        /// <summary>
        /// 入院中医症候代码
        /// </summary>
        [StringLength(10)]
        public string RUYUANZYZHDM { get; set; }
        /// <summary>
        /// 入院中医症候名称
        /// </summary>
        [StringLength(100)]
        public string RUYUANZYZHMC { get; set; }
        /// <summary>
        /// 药品不转移标志
        /// </summary>
        public int? YAOPINBZYBZ { get; set; }
        /// <summary>
        /// 转病区标志
        /// </summary>
        public int? ZHUANBINGQBZ { get; set; }
        /// <summary>
        /// 母亲住院ID
        /// </summary>
        [StringLength(10)]
        public string MUQINZYID { get; set; }
        private int? _CHANFUBZ;
        /// <summary>
        /// 产妇标志
        /// </summary>
        public int? CHANFUBZ { get; set; }
        private int? _LINCHUANGLJBZ;
        /// <summary>
        /// 临床路径标志
        /// </summary>
        public int? LINCHUANGLJBZ { get; set; }
        private int? _QUXIAORYBZ;
        /// <summary>
        /// 取消入院标志
        /// </summary>
        public int? QUXIAORYBZ { get; set; }
        private int? _QINGJIABZ;
        /// <summary>
        /// 请假标志
        /// </summary>
        public int? QINGJIABZ { get; set; }
        /// <summary>
        /// 原病人住院ID
        /// </summary>
        [StringLength(10)]
        public string YUANBINGRZYID { get; set; }
        /// <summary>
        /// 绿色通道标志
        /// </summary>
        public int? LVSETDBZ { get; set; }
        /// <summary>
        /// 绿色通道开启日期
        /// </summary>
        public DateTime? LVSETDKQRQ { get; set; }
        /// <summary>
        /// 医保明细反交易记录数
        /// </summary>
        public long? YIBAOMXFJYJLS { get; set; }
        /// <summary>
        /// 特殊标志
        /// </summary>
        public int? TESHUBZ { get; set; }
        /// <summary>
        /// 医疗卡号内部医疗账户号
        /// </summary>
        [StringLength(10)]
        public string YILIAOKH { get; set; }
        /// <summary>
        /// 介绍人
        /// </summary>
        [StringLength(10)]
        public string JIESHAOREN { get; set; }
        private int? _YINGERSL;
        /// <summary>
        /// 婴儿数量
        /// </summary>
        public int? YINGERSL { get; set; }
        /// <summary>
        /// 优惠类别列表
        /// </summary>
        [StringLength(500)]
        public string YOUHUILBLB { get; set; }
        /// <summary>
        /// 取消预出院日期
        /// </summary>
        public DateTime? QUXIAOYCYRQ { get; set; }
        /// <summary>
        /// 入院诊断代码2
        /// </summary>
        [StringLength(10)]
        public string RUYUANZDDM2 { get; set; }
        /// <summary>
        /// 入院诊断名称2
        /// </summary>
        [StringLength(100)]
        public string RUYUANZDMC2 { get; set; }
        /// <summary>
        /// 入院诊断代码3
        /// </summary>
        [StringLength(10)]
        public string RUYUANZDDM3 { get; set; }
        /// <summary>
        /// 入院诊断名称3
        /// </summary>
        [StringLength(100)]
        public string RUYUANZDMC3 { get; set; }
        /// <summary>
        /// 医保病人信息
        /// </summary>
        [StringLength(4000)]
        public string YIBAOBRXX { get; set; }
        /// <summary>
        /// 费用类别2
        /// </summary>
        [StringLength(10)]
        public string FEIYONGLB2 { get; set; }
        /// <summary>
        /// 费用性质2
        /// </summary>
        [StringLength(10)]
        public string FEIYONGXZ2 { get; set; }
        private int? _YILIAOJZDJBZ;
        /// <summary>
        /// 医疗救助登记标注
        /// </summary>
        public int? YILIAOJZDJBZ { get; set; }
        /// <summary>
        /// 字符1医保
        /// </summary>
        [StringLength(50)]
        public string ZHIFU1YB { get; set; }
        /// <summary>
        /// 字符2医保
        /// </summary>
        [StringLength(50)]
        public string ZHIFU2YB { get; set; }
        /// <summary>
        /// 字符3医保
        /// </summary>
        [StringLength(500)]
        public string ZHIFU3YB { get; set; }
        /// <summary>
        /// 字符4医保
        /// </summary>
        [StringLength(500)]
        public string ZHIFU4YB { get; set; }
        /// <summary>
        /// 字符5医保
        /// </summary>
        [StringLength(50)]
        public string ZHIFU5YB { get; set; }
        /// <summary>
        /// 字符6医保
        /// </summary>
        [StringLength(50)]
        public string ZHIFU6YB { get; set; }
        /// <summary>
        /// 字符7医保
        /// </summary>
        [StringLength(50)]
        public string ZHIFU7YB { get; set; }
        /// <summary>
        /// 字符8医保
        /// </summary>
        [StringLength(50)]
        public string ZHIFU8YB { get; set; }
        /// <summary>
        /// 字符9医保
        /// </summary>
        [StringLength(500)]
        public string ZHIFU9YB { get; set; }
        /// <summary>
        /// 字符10医保
        /// </summary>
        [StringLength(500)]
        public string ZHIFU10YB { get; set; }
        /// <summary>
        /// 字符11医保
        /// </summary>
        [StringLength(500)]
        public string ZHIFU11YB { get; set; }
        /// <summary>
        /// 字符12医保
        /// </summary>
        [StringLength(500)]
        public string ZHIFU12YB { get; set; }
        /// <summary>
        /// 数值1医保
        /// </summary>
        public decimal? SHUZHI1YB { get; set; }
        /// <summary>
        /// 数值2医保
        /// </summary>
        public decimal? SHUZHI2YB { get; set; }
        /// <summary>
        /// 数值3医保
        /// </summary>
        public decimal? SHUZHI3YB { get; set; }
        /// <summary>
        /// 数值4医保
        /// </summary>
        public decimal? SHUZHI4YB { get; set; }
        /// <summary>
        /// 数值5医保
        /// </summary>
        public decimal? SHUZHI5YB { get; set; }
        /// <summary>
        /// 数值6医保
        /// </summary>
        public decimal? SHUZHI6YB { get; set; }
        /// <summary>
        /// 数值7医保
        /// </summary>
        public decimal? SHUZHI7YB { get; set; }
        /// <summary>
        /// 数值8医保
        /// </summary>
        public decimal? SHUZHI8YB { get; set; }
        /// <summary>
        /// 数值9医保
        /// </summary>
        public decimal? SHUZHI9YB { get; set; }
        /// <summary>
        /// 数值10医保
        /// </summary>
        public decimal? SHUZHI10YB { get; set; }
        /// <summary>
        /// 医保出院登记标志145122
        /// </summary>
        public int? YIBAOCYDJBZ { get; set; }
        private int? _YIZHONGYLBZ;
        /// <summary>
        /// 医中医疗标志HR3-10998(149753)
        /// </summary>
        public int? YIZHONGYLBZ { get; set; }
        /// <summary>
        /// 医中医疗类别HR3-10998(149753)
        /// </summary>
        [StringLength(10)]
        public string YIZHONGYLLB { get; set; }
        private decimal? _YIZHONGYLXYJE;
        /// <summary>
        /// 医中医疗协议金额HR3-10998(149753)
        /// </summary>
        public decimal? YIZHONGYLXYJE { get; set; }
        /// <summary>
        /// 医中医疗备注说明HR3-10998(149753)
        /// </summary>
        [StringLength(500)]
        public string YIZHONGYLBZSM { get; set; }
        /// <summary>
        /// 分娩日期HR3-11436(153413)
        /// </summary>
        public DateTime? FENMIANRQ { get; set; }
        /// <summary>
        /// 并发症HR3-11436(153413)
        /// </summary>
        [StringLength(100)]
        public string BINGFAZHENG { get; set; }
        /// <summary>
        /// 补助卡编号HR3-11436(153413)
        /// </summary>
        [StringLength(100)]
        public string BUZHUKBH { get; set; }
        /// <summary>
        /// 医保卡号2HR3-11582
        /// </summary>
        [StringLength(50)]
        public string YIBAOKH2 { get; set; }
        /// <summary>
        /// IC2HR3-11582
        /// </summary>
        [StringLength(2000)]
        public string IC2 { get; set; }
        /// <summary>
        /// 取消预出院原因1.费用处理2.持续治疗HR3-12279(160965)
        /// </summary>
        [StringLength(4)]
        public string QUXIAOYCYYY { get; set; }
        /// <summary>
        /// 备注HR3-12759(165878)
        /// </summary>
        [StringLength(500)]
        public string BEIZHU { get; set; }
        /// <summary>
        /// 责任护士HR3-13246(171002)
        /// </summary>
        [StringLength(10)]
        public string ZERENHS { get; set; }
        /// <summary>
        /// 责任护士姓名HR3-13246(171002)
        /// </summary>
        [StringLength(100)]
        public string ZERENHSXM { get; set; }
        /// <summary>
        /// 纠纷病人标志HR3-13272(171228)
        /// </summary>
        public int? JIUFENBRBZ { get; set; }
        /// <summary>
        /// 证件类型HR3-13459(173157)
        /// </summary>
        [StringLength(50)]
        public string ZHENGJIANLX { get; set; }
        /// <summary>
        /// 监护人身份证号HR3-14834(187391)
        /// </summary>
        [StringLength(20)]
        public string JIANHURENSFZH { get; set; }
        /// <summary>
        /// 隔离类型
        /// </summary>
        [StringLength(10)]
        public string GELILX { get; set; }
        /// <summary>
        /// 上次预出院日期HR3-16239(199856)
        /// </summary>
        public DateTime? LASTYUCHUYRQ { get; set; }
        /// <summary>
        /// 上次出院日期HR3-16239(199856)
        /// </summary>
        public DateTime? LASTCHUYUANRQ { get; set; }
        /// <summary>
        /// 预入院标志HR3-17203(206253)
        /// </summary>
        public int? YURUYUANBZ { get; set; }
        /// <summary>
        /// 入院登记类型HR3-17801(209933)
        /// </summary>
        [StringLength(4)]
        public string RUYUANDJLX { get; set; }
        /// <summary>
        /// 县区HR3-17804
        /// </summary>
        [StringLength(100)]
        public string XIANQU { get; set; }
        /// <summary>
        /// 年龄1HR3-17941(210691)
        /// </summary>
        public int? NIANLING1 { get; set; }
        /// <summary>
        /// 年龄单位1HR3-17941(210691)
        /// </summary>
        [StringLength(4)]
        public string NIANLINGDW1 { get; set; }
        /// <summary>
        /// 工伤康复登记号HR3-17763(209699)
        /// </summary>
        [StringLength(100)]
        public string GONGSHANGKFDJH { get; set; }
        /// <summary>
        /// 工伤康复日期HR3-17763(209699)
        /// </summary>
        public DateTime? GONGSHANGKFRQ { get; set; }
        private int? _LINCHUANGLJDBZBZ;
        /// <summary>
        /// 临床路径单病种标志HR3-18946(217335)
        /// </summary>
        public int? LINCHUANGLJDBZBZ { get; set; }
        /// <summary>
        /// 身高CM--HR3-18912(217153)
        /// </summary>
        public decimal? SHENGAO { get; set; }
        /// <summary>
        /// 体重KG--HR3-18912(217153)
        /// </summary>
        public decimal? TIZHONG { get; set; }
        /// <summary>
        /// ADL评分--HR3-18912(217153)
        /// </summary>
        [StringLength(4)]
        public string ADLSCORE { get; set; }
        /// <summary>
        /// 审核人HR3-19101(218437)
        /// </summary>
        [StringLength(10)]
        public string SHENHEREN { get; set; }
        /// <summary>
        /// 审核日期HR3-19101(218437)
        /// </summary>
        public DateTime? SHENHERQ { get; set; }
        private int? _GUAZHANGBZ;
        /// <summary>
        /// 挂账标志HR3-19485(222557)
        /// </summary>
        public int? GUAZHANGBZ { get; set; }
        /// <summary>
        /// 证件扫描文件HR3-19769(226192)
        /// </summary>
        [StringLength(500)]
        public string ZHENGJIANSMWJ { get; set; }
        /// <summary>
        /// 保密级别[1、保密，0、否]HR3-21300(244424)
        /// </summary>
        [StringLength(10)]
        public string BAOMIJB { get; set; }
        /// <summary>
        /// 户口地址HR3-20470(237669)
        /// </summary>
        [StringLength(100)]
        public string HUKOUDZ { get; set; }
        /// <summary>
        /// 户口省份HR3-20470(237669)
        /// </summary>
        [StringLength(100)]
        public string HUKOUSF { get; set; }
        /// <summary>
        /// 户口市地区HR3-20470(237669)
        /// </summary>
        [StringLength(100)]
        public string HUKOUSDQ { get; set; }
        /// <summary>
        /// 户口县区HR3-20470(237669)
        /// </summary>
        [StringLength(100)]
        public string HUKOUXQ { get; set; }
        /// <summary>
        /// 户口乡镇街道HR3-20470(237669)
        /// </summary>
        [StringLength(100)]
        public string HUKOUXZJD { get; set; }
        /// <summary>
        /// 家庭地址类别 HR3-20955(241982) 
        /// </summary>
        [StringLength(10)]
        public string JIATINGDZLB { get; set; }
        /// <summary>
        /// 户口地址类别 HR3-20955(241982) 
        /// </summary>
        [StringLength(10)]
        public string HUKOUDZLB { get; set; }
        /// <summary>
        /// 健康卡号HR3-20873(241354)
        /// </summary>
        [StringLength(100)]
        public string JIANKANGKH { get; set; }
        /// <summary>
        /// 肇事肇祸标志HR3BY-14365(248467)
        /// </summary>
        public int? ZHAOSHIZHBZ { get; set; }
        /// <summary>
        /// 健康卡信息HR3-22272(251934)
        /// </summary>
        [StringLength(1000)]
        public string JIANKANGKXX { get; set; }
        /// <summary>
        /// 日间住院标志HR3-22292(252104)
        /// </summary>
        public int? RIJIANZYBZ { get; set; }
        /// <summary>
        /// 床位占用标志HR3-20439(237322)
        /// </summary>
        public int? CHUANGWEIZYBZ { get; set; }
        /// <summary>
        /// 护士长HR3-22460(253357)
        /// </summary>
        [StringLength(10)]
        public string HUSHIZHANG { get; set; }
        /// <summary>
        /// 护士长姓名HR3-22460(253357)
        /// </summary>
        [StringLength(100)]
        public string HUSHIZXM { get; set; }
        private int? _JIZHENYXBZ;
        /// <summary>
        /// 急诊优先标志HR3-21783(248182)
        /// </summary>
        public int? JIZHENYXBZ { get; set; }
        /// <summary>
        /// 预出院系统时间(257488)
        /// </summary>
        public DateTime? YUCHUYXTSJ { get; set; }
        /// <summary>
        /// 体表面积HR3-23298(258669)
        /// </summary>
        public decimal? TIBIAOMJ { get; set; }
        private string _YIWAISHBZ;
        /// <summary>
        /// 意外伤害标志0-否,1-是HR3-24502(265973)
        /// </summary>
        [StringLength(3)]
        public string YIWAISHBZ { get; set; }
        /// <summary>
        /// 出生地省份HR3-24641(267014)
        /// </summary>
        [StringLength(100)]
        public string CHUSHENGDSF { get; set; }
        /// <summary>
        /// 出生地市地区HR3-24641(267014)
        /// </summary>
        [StringLength(100)]
        public string CHUSHENGDSDQ { get; set; }
        /// <summary>
        /// 出生地县区HR3-24641(267014)
        /// </summary>
        [StringLength(100)]
        public string CHUSHENGDXQ { get; set; }
        /// <summary>
        /// 籍贯省份HR3-24641(267014)
        /// </summary>
        [StringLength(100)]
        public string JIGUANSF { get; set; }
        /// <summary>
        /// 籍贯市地区HR3-24641(267014)
        /// </summary>
        [StringLength(100)]
        public string JIGUANSDQ { get; set; }
        /// <summary>
        /// 现住址省份HR3-24641(267014)
        /// </summary>
        [StringLength(100)]
        public string XIANZHUZSF { get; set; }
        /// <summary>
        /// 现住址市地区HR3-24641(267014)
        /// </summary>
        [StringLength(100)]
        public string XIANZHUZSDQ { get; set; }
        /// <summary>
        /// 现住址县区HR3-24641(267014)
        /// </summary>
        [StringLength(100)]
        public string XIANZHUZXQ { get; set; }
        /// <summary>
        /// 现住址其他HR3-24641(267014)
        /// </summary>
        [StringLength(100)]
        public string XIANZHUZQT { get; set; }
        /// <summary>
        /// 户口地址其他HR3-24641(267014)
        /// </summary>
        [StringLength(100)]
        public string HUKOUDZQT { get; set; }
        /// <summary>
        /// 消息来源HR3-24809(268182)
        /// </summary>
        [StringLength(10)]
        public string XIAOXILY { get; set; }
        /// <summary>
        /// 网络服务标志HR3-25562(273650)
        /// </summary>
        public int? WANGLUOFWBZ { get; set; }
        /// <summary>
        /// 大病标志HR3-23419(259519)
        /// </summary>
        public int? DABINGBZ { get; set; }
        private int? _SHIFOUCSSFZ;
        /// <summary>
        /// 是否出示身份证
        /// </summary>
        public int? SHIFOUCSSFZ { get; set; }
        /// <summary>
        /// 孕次HR3-26532(279271)
        /// </summary>
        [StringLength(20)]
        public string YUNCI { get; set; }
        /// <summary>
        /// 产次HR3-26532(279271)
        /// </summary>
        [StringLength(10)]
        public string CHANCI { get; set; }
        /// <summary>
        /// 胎次HR3-26532(279271)
        /// </summary>
        [StringLength(10)]
        public string TAICI { get; set; }
        /// <summary>
        /// 预产期HR3-26532(279271)
        /// </summary>
        [StringLength(20)]
        public string YUCHANQI { get; set; }
        /// <summary>
        /// 区域编号HR3-26532(279271)
        /// </summary>
        [StringLength(20)]
        public string QUYUBH { get; set; }
        /// <summary>
        /// 末次月经HR3-26532(279271)
        /// </summary>
        [StringLength(20)]
        public string MOCIYJ { get; set; }
        /// <summary>
        /// 免费券类型HR3-26532(279271)
        /// </summary>
        [StringLength(20)]
        public string MIANFEIQLX { get; set; }
        /// <summary>
        /// 户口邮编HR3-27179(283756)
        /// </summary>
        [StringLength(20)]
        public string HUKOUYB { get; set; }
        /// <summary>
        /// 十八中大病代码
        /// </summary>
        [StringLength(10)]
        public string SHIBAZDBDM { get; set; }
        /// <summary>
        /// 末次月经时间
        /// </summary>
        public DateTime? MOCIYUEJINGSJ { get; set; }
        /// <summary>
        /// 计算用末次月经时间
        /// </summary>
        public DateTime? JSMOCIYJSJ { get; set; }
        /// <summary>
        /// 孕周
        /// </summary>
        public long? YUNZHOU { get; set; }
        /// <summary>
        /// 孕天
        /// </summary>
        public long? YUNTIAN { get; set; }
        /// <summary>
        /// 胎产次
        /// </summary>
        [StringLength(50)]
        public string TAICHANCI { get; set; }
        /// <summary>
        /// 不随访标志HR3-30460(305798)
        /// </summary>
        public int? BUSUIFANGBZ { get; set; }
        /// <summary>
        /// 院区IDHR3-32029(315245)
        /// </summary>
        [Required]
        [StringLength(1)]
        public string YUANQUID { get; set; }
        /// <summary>
        /// 是否收取起伏线HR3-33756(325871)
        /// </summary>
        [StringLength(1)]
        public string SHIFOUSQQFX { get; set; }
        /// <summary>
        /// 营养膳食费用标志HR3-33378(323469)
        /// </summary>
        public int? SHANSHIFYBZ { get; set; }
        /// <summary>
        /// 首发精神分裂症标志
        /// </summary>
        public int? SFJSFLZBZ { get; set; }
        /// <summary>
        /// 不入临床路径理由HR3-31405(311428)
        /// </summary>
        [StringLength(500)]
        public string BURUJINGLY { get; set; }
        /// <summary>
        /// 医联体标志HR3-34466(330330)
        /// </summary>
        [StringLength(1)]
        public string YILIANTBZ { get; set; }
        /// <summary>
        /// 医联体编号HR3-34466(330330)
        /// </summary>
        [StringLength(20)]
        public string YILIANTBH { get; set; }
        /// <summary>
        /// 医联体申请单IDHR3-34466(330330)
        /// </summary>
        [StringLength(50)]
        public string YILIANTSQDID { get; set; }
        /// <summary>
        /// 主任医生HR3-35491(335395)
        /// </summary>
        [StringLength(10)]
        public string ZHURENYS { get; set; }
        /// <summary>
        /// 主任医生名称HR3-35491(335395)
        /// </summary>
        [StringLength(20)]
        public string ZHURENYSXM { get; set; }
        /// <summary>
        /// 生育标志HR3-37163(344121)
        /// </summary>
        public int? SHENGYUBZ { get; set; }
        /// <summary>
        /// 120标志HR3-39758(361431)
        /// </summary>
        public int? BIAOZHI120 { get; set; }
        private int? _YINANBRBZ;
        /// <summary>
        /// 疑难病人标志HR3-41664
        /// </summary>
        public int? YINANBRBZ { get; set; }
        /// <summary>
        /// 入院原因HR3-42447(376875)
        /// </summary>
        [StringLength(100)]
        public string RUYUANYY { get; set; }
        /// <summary>
        /// 再次入院类型1:7天;2:31天HR3-42447(376875)
        /// </summary>
        public int? ZAICIRYLX { get; set; }
        /// <summary>
        /// 转诊单号HR3-42411(376652)
        /// </summary>
        [StringLength(50)]
        public string ZHUANZHENDH { get; set; }
        /// <summary>
        /// 原入科日期384476
        /// </summary>
        public DateTime? YUANRUKERQ { get; set; }
        /// <summary>
        /// 婴儿床位费记账状态HR3-44295(386318)
        /// </summary>
        public int? YINGERCWFJZZT { get; set; }
        /// <summary>
        /// 婴儿床位费记账调整日期HR3-44295(386318)
        /// </summary>
        public DateTime? YINGERCWFJZTZRQ { get; set; }
        /// <summary>
        /// 婴儿床位费记账调整人HR3-44295(386318)
        /// </summary>
        [StringLength(10)]
        public string YINGERCWFJZTZR { get; set; }
        /// <summary>
        /// 快速入院标志HR3-45077(390486)
        /// </summary>
        public int? KUAISURYBZ { get; set; }
        /// <summary>
        /// 身份证不详原因HR3-46327(397284)
        /// </summary>
        [StringLength(20)]
        public string SHENFENZBXYY { get; set; }
        /// <summary>
        /// 异地人员标志
        /// </summary>
        [StringLength(10)]
        public string YDRYBZ { get; set; }
        /// <summary>
        /// 统筹区标志
        /// </summary>
        [StringLength(20)]
        public string TCQBZ { get; set; }
        /// <summary>
        /// 森林防火病人标志HR3-54823(446985)
        /// </summary>
        public int? SENLINFHBRBZ { get; set; }
        /// <summary>
        /// 森林防火审批日期HR3-54823(446985)
        /// </summary>
        public DateTime? SENLINFHSPRQ { get; set; }
        /// <summary>
        /// 森林防火备注HR3-54823(446985)
        /// </summary>
        [StringLength(100)]
        public string SENLINFHBZ { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        public string MPI { get; set; }
        /// <summary>
        /// 首次入科人HR6-355(468160)
        /// </summary>
        [StringLength(10)]
        public string SHOUCIRKR { get; set; }
        /// <summary>
        /// 首次入科人姓名HR6-355(468160)
        /// </summary>
        [StringLength(50)]
        public string SHOUCIRKRXM { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? JIUZHENQRRQ { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? JIUZHENQRBZ { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? QUERENSFRQ_HIS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? QUERENSFBZ_HIS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        public string DENGJISY { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(10)]
        public string YUNYIXYED { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? YUNYIQYBZ { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(10)]
        public string ZHUANDRYELB { get; set; }
        /// <summary>
        /// 留观标志（1：留观   0：非留观）HR6-1864(530725)
        /// </summary>
        public int? LIUGUANBZ { get; set; }
        /// <summary>
        ///  留观申请单ID HR6-1864(530725)（关联JZ_LIUGUANSQD表）
        /// </summary>
        [StringLength(10)]
        public string LIUGUANSQDID { get; set; }
        /// <summary>
        /// 就诊ID  HR6-1864(530725)
        /// </summary>
        [StringLength(10)]
        public string JIUZHENID { get; set; }
        /// <summary>
        /// 分诊时间 HR6-1864(530725)
        /// </summary>
        public DateTime? FENZHENSJ { get; set; }
        /// <summary>
        /// 入院途径HR6-1955(535061)
        /// </summary>
        [StringLength(10)]
        public string LAIYUAN { get; set; }
        /// <summary>
        /// 分诊员HR6-1864(530725)
        /// </summary>
        [StringLength(100)]
        public string FENZHENYUAN { get; set; }
        /// <summary>
        /// 分诊员ID HR6-1864(530725)
        /// </summary>
        [StringLength(100)]
        public string FENZHENYUANID { get; set; }
        /// <summary>
        /// 分诊级别HR6-1864(530725)
        /// </summary>
        [StringLength(10)]
        public string FENZHENLB { get; set; }
        /// <summary>
        /// 是否调整日期
        /// </summary>
        public int? SHIFUTZRQ { get; set; }
        /// <summary>
        /// 个人电话
        /// </summary>
        [StringLength(20)]
        public string DIANHUA { get; set; }
        /// <summary>
        ///默认值方法 
        /// </summary>
        [Ignore]
        public override void SetDefaultValue()
        {
            if (YINGERBZ.IsNullOrDBNull())
                YINGERBZ = 0;
            if (SHENHEBZ.IsNullOrDBNull())
                SHENHEBZ = 0;
            if (RUKEBZ.IsNullOrDBNull())
                RUKEBZ = 0;
            if (ZHANCHUANGBZ.IsNullOrDBNull())
                ZHANCHUANGBZ = 0;
            if (FEIYONGXZZHBZ.IsNullOrDBNull())
                FEIYONGXZZHBZ = 0;
            if (TESHUBZBZ.IsNullOrDBNull())
                TESHUBZBZ = 0;
            if (WAIYUANZDBZ.IsNullOrDBNull())
                WAIYUANZDBZ = 0;
            if (CHANFUBZ.IsNullOrDBNull())
                CHANFUBZ = 0;
            if (LINCHUANGLJBZ.IsNullOrDBNull())
                LINCHUANGLJBZ = 0;
            if (QUXIAORYBZ.IsNullOrDBNull())
                QUXIAORYBZ = 0;
            if (QINGJIABZ.IsNullOrDBNull())
                QINGJIABZ = 0;
            if (YINGERSL.IsNullOrDBNull())
                YINGERSL = 0;
            if (YILIAOJZDJBZ.IsNullOrDBNull())
                YILIAOJZDJBZ = 0;
            if (YIZHONGYLBZ.IsNullOrDBNull())
                YIZHONGYLBZ = 0;
            if (YIZHONGYLXYJE.IsNullOrDBNull())
                YIZHONGYLXYJE = 0;
            if (LINCHUANGLJDBZBZ.IsNullOrDBNull())
                LINCHUANGLJDBZBZ = 0;
            if (GUAZHANGBZ.IsNullOrDBNull())
                GUAZHANGBZ = 0;
            if (JIZHENYXBZ.IsNullOrDBNull())
                JIZHENYXBZ = 0;
            if (YIWAISHBZ.IsNullOrDBNull())
                YIWAISHBZ = "0";
            if (SHIFOUCSSFZ.IsNullOrDBNull())
                SHIFOUCSSFZ = 0;
            if (YINANBRBZ.IsNullOrDBNull())
                YINANBRBZ = 0;
        }
    }
}
