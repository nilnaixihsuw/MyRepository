using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_ZHIGONGXX")]
	public partial class GY_ZHIGONGXX : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 职工ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(10)]
		public string ZHIGONGID { get; set; }
		/// <summary>
		/// 职工工号
		/// </summary>
		[StringLength(10)]
		public string ZHIGONGGH { get; set; }
		/// <summary>
		/// 职工姓名
		/// </summary>
		[StringLength(20)]
		public string ZHIGONGXM { get; set; }
		/// <summary>
		/// 性别
		/// </summary>
		[StringLength(4)]
		public string XINGBIE { get; set; }
		/// <summary>
		/// 出生日期
		/// </summary>
		public DateTime? CHUSHENGRQ { get; set; }
		/// <summary>
		/// 身份证号
		/// </summary>
		[StringLength(20)]
		public string SHENFENZH { get; set; }
		/// <summary>
		/// 家庭地址
		/// </summary>
		[StringLength(200)]
		public string JIATINGDZ { get; set; }
		/// <summary>
		/// 家庭邮编
		/// </summary>
		[StringLength(10)]
		public string JIATINGYB { get; set; }
		/// <summary>
		/// 电子邮件
		/// </summary>
		[StringLength(50)]
		public string DIANZIYJ { get; set; }
		/// <summary>
		/// 电话
		/// </summary>
		[StringLength(20)]
		public string DIANHUA { get; set; }
		/// <summary>
		/// 职务
		/// </summary>
		[StringLength(10)]
		public string ZHIWU { get; set; }
		/// <summary>
		/// 职称
		/// </summary>
		[StringLength(10)]
		public string ZHICHENG { get; set; }
		/// <summary>
		/// 参加工作时间
		/// </summary>
		public DateTime? CANJIAGZSJ { get; set; }
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
		/// 当前状态
		/// </summary>
		[Required]
		[StringLength(4)]
		public string DANGQIANZT { get; set; }
		private string _QUANXIAN;
		/// <summary>
		/// 权限
		/// </summary>
		[Required]
		[StringLength(50)]
		public string QUANXIAN { get{ return _QUANXIAN; } set{ _QUANXIAN = value ?? "00000000000000000000"; } }
		/// <summary>
		/// 门诊类别
		/// </summary>
		[StringLength(10)]
		public string MENZHENLB { get; set; }
		/// <summary>
		/// 职工类别
		/// </summary>
		[Required]
		[StringLength(4)]
		public string ZHIGONGLB { get; set; }
		/// <summary>
		/// 密码
		/// </summary>
		[StringLength(20)]
		public string MIMA { get; set; }
		/// <summary>
		/// 人事科室
		/// </summary>
		[StringLength(10)]
		public string RENSHIKS { get; set; }
		/// <summary>
		/// 挂号费项目
		/// </summary>
		[StringLength(10)]
		public string GUAHAOFXM { get; set; }
		/// <summary>
		/// 诊疗费项目
		/// </summary>
		[StringLength(10)]
		public string ZHENLIAOFXM { get; set; }
		private string _KESHIID;
		/// <summary>
		/// 科室ID
		/// </summary>
		[Required]
		[StringLength(10)]
		public string KESHIID { get{ return _KESHIID; } set{ _KESHIID = value ?? "*"; } }
		/// <summary>
		/// 医生等级
		/// </summary>
		[StringLength(4)]
		public string YISHENGDJ { get; set; }
		/// <summary>
		/// 英文名
		/// </summary>
		[StringLength(50)]
		public string YINGWENMING { get; set; }
		private int? _QIANMINGBZ;
		/// <summary>
		/// 是否签名标志
		/// </summary>
		public int? QIANMINGBZ { get{ return _QIANMINGBZ; } set{ _QIANMINGBZ = value ?? 0; } }
		/// <summary>
		/// 图章号
		/// </summary>
		[StringLength(50)]
		public string TUZHANGHAO { get; set; }
		/// <summary>
		/// 职工资料
		/// </summary>
		[StringLength(2000)]
		public string ZHIGONGZL { get; set; }
		/// <summary>
		/// 核算科室
		/// </summary>
		[StringLength(10)]
		public string HESUANKS { get; set; }
		/// <summary>
		/// 医师资格证书编码HR3-16977
		/// </summary>
		[StringLength(50)]
		public string YISHIZGZS { get; set; }
		/// <summary>
		/// 职业开始日期HR3-16977
		/// </summary>
		public DateTime? ZHIYEKSRQ { get; set; }
		/// <summary>
		/// 职业结束日期HR3-16977
		/// </summary>
		public DateTime? ZHIYEJSRQ { get; set; }
		/// <summary>
		/// 医师执业证书编码HR3-17394
		/// </summary>
		[StringLength(50)]
		public string YISHIZYZS { get; set; }
		/// <summary>
		/// 在职状态HR3-16975
		/// </summary>
		[StringLength(10)]
		public string ZAIZHIZT { get; set; }
		/// <summary>
		/// 职业资格HR3-16975
		/// </summary>
		[StringLength(10)]
		public string ZHIYEZG { get; set; }
		/// <summary>
		/// 人员编制类别HR3-16975
		/// </summary>
		[StringLength(10)]
		public string RENYUANBZLB { get; set; }
		/// <summary>
		/// 职业顺序号HR3-18523
		/// </summary>
		[StringLength(1)]
		public string ZHIYESXH { get; set; }
		/// <summary>
		/// 执业范围HR3-18523
		/// </summary>
		[StringLength(2)]
		public string ZHIYEFW { get; set; }
		/// <summary>
		/// 执业科别HR3-18523
		/// </summary>
		[StringLength(10)]
		public string ZHIYEKB { get; set; }
		/// <summary>
		/// 康复资质HR3-18523
		/// </summary>
		[StringLength(1)]
		public string KANGFUZZ { get; set; }
		/// <summary>
		/// 是否技术交流HR3-18523
		/// </summary>
		[StringLength(1)]
		public string SHIFOUJSJL { get; set; }
		/// <summary>
		/// 医保医师服务编码HR3-18523
		/// </summary>
		[StringLength(50)]
		public string YIBAOYSFWBM { get; set; }
		/// <summary>
		/// 医师级别HR3-18523
		/// </summary>
		[StringLength(1)]
		public string YISHIJB { get; set; }
		/// <summary>
		/// 执业地点HR3-19704
		/// </summary>
		[StringLength(50)]
		public string ZHIYEDD { get; set; }
		/// <summary>
		/// 执业类别HR3-19710
		/// </summary>
		[StringLength(1)]
		public string ZHIYELB { get; set; }
		/// <summary>
		/// 电话1(虚拟网号码)HR3-22399
		/// </summary>
		[StringLength(20)]
		public string DIANHUA1 { get; set; }
		/// <summary>
		/// 电话2HR3-22399
		/// </summary>
		[StringLength(20)]
		public string DIANHUA2 { get; set; }
		/// <summary>
		/// 人员序号FOR HR3-23877(262362)
		/// </summary>
		[StringLength(20)]
		public string RENYUANXH { get; set; }
		/// <summary>
		/// 挂号网IDHR3-22355(252587)
		/// </summary>
		[StringLength(20)]
		public string GUAHAOWID { get; set; }
		/// <summary>
		/// 学历HR3-24900(268712)
		/// </summary>
		[StringLength(30)]
		public string XUELI { get; set; }
		/// <summary>
		/// 门诊电子病历启用标志
		/// </summary>
		public int? MENZHENDZBLQYBZ { get; set; }
		/// <summary>
		/// 社区工号HR3-26659(280001)
		/// </summary>
		[StringLength(20)]
		public string SHEQUGH { get; set; }
		/// <summary>
		/// 社区密码HR3-26659(280001)
		/// </summary>
		[StringLength(20)]
		public string SHEQUMM { get; set; }
		/// <summary>
		/// 社区角色HR3-26659(280001)
		/// </summary>
		[StringLength(20)]
		public string SHEQUJS { get; set; }
		/// <summary>
		/// 技术职称HR3-30427(305519)
		/// </summary>
		[StringLength(10)]
		public string JISHUZC { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(1)]
		public string CABZ { get; set; }
		/// <summary>
		/// 推荐属性HR3-33183(322051)
		/// </summary>
		[StringLength(10)]
		public string TUIJIANSX { get; set; }
		/// <summary>
		/// 诊断界面右侧工具栏启用标志
		/// </summary>
		public int? ZHENDUANJMYCGJLQYBZ { get; set; }
	}
}
