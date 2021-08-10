using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.ZJ
{
	[Table("ZJ_JIUZHENXX")]
	public partial class ZJ_JIUZHENXX : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 就诊ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string JIUZHENID { get; set; }
		/// <summary>
		/// 挂号ID
		/// </summary>
		[StringLength(50)]
		public string GUAHAOID { get; set; }
		/// <summary>
		/// 挂号科室
		/// </summary>
		[StringLength(10)]
		public string GUAHAOKS { get; set; }
		/// <summary>
		/// 院区ID
		/// </summary>
		[Required]
		[StringLength(1)]
		public string YUANQUID { get; set; }
		/// <summary>
		/// 病人ID
		/// </summary>
		[Required]
		[StringLength(100)]
		public string BINGRENID { get; set; }
		/// <summary>
		/// 费用类别
		/// </summary>
		[Required]
		[StringLength(10)]
		public string FEIYONGLB { get; set; }
		/// <summary>
		/// 费用性质
		/// </summary>
		[Required]
		[StringLength(10)]
		public string FEIYONGXZ { get; set; }
		/// <summary>
		/// 优惠类别
		/// </summary>
		[StringLength(10)]
		public string YOUHUILB { get; set; }
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
		/// 年龄
		/// </summary>
		public int? NIANLING { get; set; }
		/// <summary>
		/// 年龄单位
		/// </summary>
		[StringLength(4)]
		public string NIANLINGDW { get; set; }
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
		/// 规定病种标志
		/// </summary>
		[Required]
		public int? GUIDINGBZBZ { get; set; }
        /// <summary>
		/// 调阅标志
		/// </summary>
        public int? DIAOYUEBZ { get; set; }
        /// <summary>
        /// 病人去向
        /// </summary>
        [StringLength(10)]
		public string BINGRENQX { get; set; }
		/// <summary>
		/// 就诊日期(最近一次完成接诊日期)
		/// </summary>
		[Required]
		public DateTime? JIUZHENRQ { get; set; }
		/// <summary>
		/// 初诊标志
		/// </summary>
		[Required]
		public int? CHUZHENBZ { get; set; }
		/// <summary>
		/// 召回标志
		/// </summary>
		[Required]
		public int? ZHAOHUIBZ { get; set; }
		/// <summary>
		/// 系统时间(挂号或代挂号时间)
		/// </summary>
		[Required]
		public DateTime? XITONGSJ { get; set; }
		/// <summary>
		/// 就诊医生
		/// </summary>
		[StringLength(200)]
		public string JIUZHENYS { get; set; }
		/// <summary>
		/// 就诊科室
		/// </summary>
		[StringLength(10)]
		public string JIUZHENKS { get; set; }
		/// <summary>
		/// 就诊状态
		/// </summary>
		[Required]
		public int? JIUZHENZT { get; set; }
		/// <summary>
		/// 记录来源
		/// </summary>
		[Required]
		[StringLength(4)]
		public string JILULY { get; set; }
		/// <summary>
		/// 召回应用ID
		/// </summary>
		[StringLength(4)]
		public string ZHAOHUIYYID { get; set; }
		/// <summary>
		/// 挂号医生
		/// </summary>
		[StringLength(10)]
		public string GUAHAOYS { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[StringLength(4)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 主诉
		/// </summary>
		[StringLength(1000)]
		public string ZHUSU { get; set; }
		/// <summary>
		/// 病史(现病史)
		/// </summary>
		[StringLength(1000)]
		public string JIANYAOBS { get; set; }
		/// <summary>
		/// 体格检查
		/// </summary>
		[StringLength(1000)]
		public string TIGEJC { get; set; }
		/// <summary>
		/// 临床诊断
		/// </summary>
		[StringLength(100)]
		public string LINCHUANGZD { get; set; }
		/// <summary>
		/// 发热标志
		/// </summary>
		public int? FAREBZ { get; set; }
		/// <summary>
		/// 疾病分类
		/// </summary>
		[StringLength(50)]
		public string JIBINGFL { get; set; }
		/// <summary>
		/// 挂号类别管理
		/// </summary>
		[StringLength(4)]
		public string GUAHAOLBGL { get; set; }
		/// <summary>
		/// 舒张压
		/// </summary>
		public decimal? SHUZHANGYA { get; set; }
		/// <summary>
		/// 收缩压
		/// </summary>
		public decimal? SHOUSUOYA { get; set; }
		/// <summary>
		/// 收取病历本费标志
		/// </summary>
		public int? SHOUQUBLBFBZ { get; set; }
		/// <summary>
		/// 处置
		/// </summary>
		[StringLength(1000)]
		public string CHUZHI { get; set; }
		/// <summary>
		/// 门诊挂号大类
		/// </summary>
		[StringLength(10)]
		public string MENZHENGHDL { get; set; }
		/// <summary>
		/// 预约标志
		/// </summary>
		public int? YUYUEBZ { get; set; }
		/// <summary>
		/// 挂号套餐ID
		/// </summary>
		[StringLength(10)]
		public string GUAHAOTCID { get; set; }
		/// <summary>
		/// 接诊时间(第一次接诊日期)
		/// </summary>
		public DateTime? JIEZHENSJ { get; set; }
		/// <summary>
		/// 挂号单元ID
		/// </summary>
		[StringLength(10)]
		public string GUAHAODYID { get; set; }
		/// <summary>
		/// 作废标志
		/// </summary>
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 转诊收费信息(GH^0^shoufeixmid|SF^0^shoufeixmid)
		/// </summary>
		[StringLength(100)]
		public string ZHUANZHENSFXX { get; set; }
		/// <summary>
		/// 挂号类别
		/// </summary>
		[StringLength(10)]
		public string GUAHAOLB { get; set; }
		private int? _YIBAOZZFBZ;
		/// <summary>
		/// 医保转自费标志，1：转自费
		/// </summary>
		public int? YIBAOZZFBZ { get{ return _YIBAOZZFBZ; } set{ _YIBAOZZFBZ = value; } }
		/// <summary>
		/// 挂号序号
		/// </summary>
		[StringLength(10)]
		public string GUAHAOXH { get; set; }
		/// <summary>
		/// 上下午标志
		/// </summary>
		public int? SHANGXIAWBZ { get; set; }
		/// <summary>
		/// 排班ID
		/// </summary>
		[StringLength(10)]
		public string PAIBANID { get; set; }
		/// <summary>
		/// 留观状态0正常 1开始留观 2结束留观
		/// </summary>
		public int? LIUGUANZT { get; set; }
		/// <summary>
		/// 留观科室
		/// </summary>
		[StringLength(10)]
		public string LIUGUANKS { get; set; }
		/// <summary>
		/// 留观科室名称
		/// </summary>
		[StringLength(100)]
		public string LIUGUANKSMC { get; set; }
		/// <summary>
		/// 留观开始时间
		/// </summary>
		public DateTime? LIUGUANKSSJ { get; set; }
		/// <summary>
		/// 留观结束时间
		/// </summary>
		public DateTime? LIUGUANJSSJ { get; set; }
		/// <summary>
		/// 制单日期:XITONGSJ=MZ_GUAHAO1.GUAHAORQ,这个字段=sysdate
		/// </summary>
		public DateTime? ZHIDANRQ { get; set; }
		/// <summary>
		/// 病史(既往史)
		/// </summary>
		[StringLength(1000)]
		public string BINGSHIJWS { get; set; }
		/// <summary>
		/// 病史(其他-个人月经婚姻家庭)
		/// </summary>
		[StringLength(1000)]
		public string BINGSHIQT { get; set; }
		/// <summary>
		/// 绿色通道标志
		/// </summary>
		public int? LVSETDBZ { get; set; }
		/// <summary>
		/// 床位ID
		/// </summary>
		[StringLength(6)]
		public string CHUANGWEIID { get; set; }
		/// <summary>
		/// 体温HR3-13375(172345)
		/// </summary>
		public decimal? TIWEN { get; set; }
		/// <summary>
		/// 脉搏HR3-13375(172345)
		/// </summary>
		public decimal? MAIBO { get; set; }
		/// <summary>
		/// 呼吸HR3-13375(172345)
		/// </summary>
		public decimal? HUXI { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(10)]
		public string DABINGZHONG { get; set; }
		/// <summary>
		/// 小病种HR3-13054
		/// </summary>
		[StringLength(10)]
		public string XIAOBINGZHONG { get; set; }
		/// <summary>
		/// 病历打印标志
		/// </summary>
		public int? BINGLIDYBZ { get; set; }
		/// <summary>
		/// 中医四诊HR3-15795
		/// </summary>
		[StringLength(1000)]
		public string ZHONGYISZ { get; set; }
		/// <summary>
		/// 身高HR3-15419
		/// </summary>
		public decimal? SHENGAO { get; set; }
		/// <summary>
		/// 体重HR3-15419
		/// </summary>
		public decimal? TIZHONG { get; set; }
		/// <summary>
		/// 辅助检查HR3-16486
		/// </summary>
		[StringLength(1000)]
		public string FUZHUJC { get; set; }
		/// <summary>
		/// 家族史HR3-16486
		/// </summary>
		[StringLength(1000)]
		public string BINGSHI_JZ { get; set; }
		/// <summary>
		/// 手术史HR3-16486
		/// </summary>
		[StringLength(1000)]
		public string BINGSHI_SS { get; set; }
		/// <summary>
		/// 婚育史HR3-16486
		/// </summary>
		[StringLength(1000)]
		public string BINGSHI_HY { get; set; }
		/// <summary>
		/// 外伤史HR3-16486
		/// </summary>
		[StringLength(1000)]
		public string BINGSHI_WS { get; set; }
		/// <summary>
		/// 注意事项HR3-16486
		/// </summary>
		[StringLength(1000)]
		public string ZHUYISX { get; set; }
		/// <summary>
		/// 月经史HR3-14695
		/// </summary>
		[StringLength(1000)]
		public string BINGSHI_YJ { get; set; }
		/// <summary>
		/// 暂存标志HR3-19387
		/// </summary>
		public int? ZANCUNBZ { get; set; }
		/// <summary>
		/// 关联就诊IDHR3-19387
		/// </summary>
		[StringLength(10)]
		public string GUANLIANJZID { get; set; }
		/// <summary>
		/// 修改人
		/// </summary>
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 修改标志
		/// </summary>
		[StringLength(4)]
		public string XIUGAIBZ { get; set; }
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? XIUGAISJ { get; set; }
		/// <summary>
		/// 修改应用ID
		/// </summary>
		[StringLength(4)]
		public string XIUGAIYYID { get; set; }
		/// <summary>
		/// 有效天数HR3-20654
		/// </summary>
		public int? YOUXIAOTS { get; set; }
		/// <summary>
		/// 慢性病标志
		/// </summary>
		public int? MANXINGBBZ { get; set; }
		/// <summary>
		/// 传染病标志
		/// </summary>
		public int? CHUANRANBBZ { get; set; }
		/// <summary>
		/// 孕周HR3-25595(273910)
		/// </summary>
		[StringLength(10)]
		public string YUNZHOU { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? GAOWEIBZ { get; set; }
		/// <summary>
		/// 末次月经时间HR3-25595(273910)
		/// </summary>
		public DateTime? MOCIYUEJINGSJ { get; set; }
		/// <summary>
		/// 胎产次HR3-25595(273910)
		/// </summary>
		[StringLength(50)]
		public string TAICHANCI { get; set; }
		/// <summary>
		/// 短信提醒 HR3-24173(264165) 
		/// </summary>
		public int? DUANXINTX { get; set; }
		/// <summary>
		/// 肺结核阶段 HR3-25647(274230)
		/// </summary>
		[StringLength(10)]
		public string FEIJIEHEJD { get; set; }
		/// <summary>
		/// 首诊时间HR3-25624
		/// </summary>
		public DateTime? SHOUZHENSJ { get; set; }
		/// <summary>
		/// 首诊医生HR3-25624
		/// </summary>
		[StringLength(10)]
		public string SHOUZHENYS { get; set; }
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
		/// IP地址
		/// </summary>
		[StringLength(20)]
		public string IP { get; set; }
		/// <summary>
		/// 支付宝二维码标志HR3-42628()含微信
		/// </summary>
		public int? ZFBERWEIMABZ { get; set; }
		/// <summary>
		/// 支付宝商户单号HR3-42628()含微信
		/// </summary>
		[StringLength(500)]
		public string ZFBSHANGHUDH { get; set; }
		/// <summary>
		/// 就诊位置ID HR3-28134
		/// </summary>
		[StringLength(10)]
		public string JIUZHENWZID { get; set; }
		/// <summary>
		/// 首诊科室HR3-28770
		/// </summary>
		[StringLength(10)]
		public string SHOUZHENKS { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(10)]
		public string JIUZHENYYID { get; set; }
		/// <summary>
		/// 分诊人HR3-25595(273910)
		/// </summary>
		[StringLength(10)]
		public string FENZHENREN { get; set; }
		/// <summary>
		/// 计算用末次月经时间HR3-25595(273910)
		/// </summary>
		public DateTime? JSMOCIYJSJ { get; set; }
		/// <summary>
		/// 孕天HR3-25595(273910)
		/// </summary>
		public long? YUNTIAN { get; set; }
		/// <summary>
		/// 过敏史HR3-25595(273910)
		/// </summary>
		[StringLength(100)]
		public string GUOMINSHI { get; set; }
		/// <summary>
		/// 来院方式 gy_daimalb=0248 HR3-29137
		/// </summary>
		[StringLength(10)]
		public string LAIYUANFS { get; set; }
		/// <summary>
		/// 病情分级gy_daimalb=0249
		/// </summary>
		[StringLength(10)]
		public string BINGQINGFJ { get; set; }
		/// <summary>
		/// 病人类别(1孕产妇，2妇女，3儿童，0其他）嘉兴悦程
		/// </summary>
		[StringLength(10)]
		public string BINGRENLB { get; set; }
		/// <summary>
		/// 溶栓标志HR3-31459(311770)
		/// </summary>
		public int? RONGSHUANBZ { get; set; }
		/// <summary>
		/// 就诊卡号HR3-31807
		/// </summary>
		[StringLength(100)]
		public string JIUZHENKH { get; set; }
		/// <summary>
		/// 肺结核路径记录IDHR3-34750(331888)
		/// </summary>
		[StringLength(10)]
		public string FEIJIEHLJJLID { get; set; }
		/// <summary>
		/// 肺结核阶段就诊日期HR3-34750(331888)
		/// </summary>
		public DateTime? FEIJIEHJDJZRQ { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(1)]
		public string ZIFEIBZ { get; set; }
		/// <summary>
		/// 心率(HR3-37412(345398))
		/// </summary>
		public decimal? XINLV { get; set; }
		/// <summary>
		/// 宫高HR3-37798(347743)
		/// </summary>
		[StringLength(3)]
		public string GONGGAO { get; set; }
		/// <summary>
		/// 腹围HR3-37798(347743)
		/// </summary>
		[StringLength(3)]
		public string FUWEI { get; set; }
		/// <summary>
		/// 胎心HR3-37798(347743)
		/// </summary>
		[StringLength(3)]
		public string TAIXIN { get; set; }
		/// <summary>
		/// 胎位HR3-37798(347743)
		/// </summary>
		[StringLength(10)]
		public string TAIWEI { get; set; }
		/// <summary>
		/// 浮肿HR3-37798(347743)
		/// </summary>
		[StringLength(10)]
		public string FUZHONG { get; set; }
		/// <summary>
		/// 签约标志HR3-37484(345781)
		/// </summary>
		public int? QIANYUEBZ { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		[StringLength(1000)]
		public string BEIZHU { get; set; }
		/// <summary>
		/// 抢救室标志
		/// </summary>
		public int? QIANGJIUSBZ { get; set; }
		/// <summary>
		/// 胸痛中心标志HR3-42671(377923)
		/// </summary>
		public int? XIONGTONGZXBZ { get; set; }
		/// <summary>
		/// 病人来源HR3-45166 1：120转送
		/// </summary>
		[StringLength(10)]
		public string BINGRENLY { get; set; }
		/// <summary>
		/// 质控标志HR3-50718(422706)
		/// </summary>
		public int? ZHIKONGBZ { get; set; }
		/// <summary>
		/// 初评HR3-49921(417883)
		/// </summary>
		[StringLength(2000)]
		public string CHUPINGXX { get; set; }

        /// <summary>
        /// 登记索引HR6-860(490430)
        /// </summary>
        [StringLength(50)]
        public string CSDN_DENJISY { get; set; }

        /// <summary>
        /// 就医支付方式HR6-860(490430)
        /// </summary>
        [StringLength(10)]
        public string JIUYIZFFS { get; set; }

        /// <summary>
        /// 规定病种审批编号
        /// </summary>
        [StringLength(20)]
        public string TESHUSPBH { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [StringLength(20)]
        public string SHOUJI { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        [StringLength(20)]
        public string LIANXIRDH { get; set; }

        /// <summary>
		/// 有无口服抗凝药物史HR6-1049(503984)
		/// </summary>
		public int? KOUFUKNYWSBZ { get; set; }

        /// <summary>
        ///默认值方法 
        /// </summary>
        public override void SetDefaultValue () 
		{
			YIBAOZZFBZ = 0;
		}

        public decimal? XUEYANGBHD { get; set; }
    }
}
