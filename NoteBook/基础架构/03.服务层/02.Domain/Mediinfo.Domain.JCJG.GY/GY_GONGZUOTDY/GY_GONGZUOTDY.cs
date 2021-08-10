using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Utility.Extensions;
using NLite;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_GONGZUOTDY")]
	public partial class GY_GONGZUOTDY : EntityBase, IEntityMapper
	{
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
		public string GONGZUOTDYID { get; set; }
        [StringLength(10)]
        public string GONGZUOTDYFID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(1)]
		public string GONGZUOTDYLX { get; set; }

        [StringLength(2000)]
        public string SQL1 { get; set; }

        [StringLength(2000)]
        public string SQL2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
		public string GONGZUODYMC { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(50)]
		public string DUIXIANGMC { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(50)]
		public string XIAOXIMC1 { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(10)]
		public string XIAOXIBM1 { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(50)]
		public string XIAOXIMC2 { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(10)]
		public string XIAOXIBM2 { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(50)]
		public string XIAOXIMC3 { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(10)]
		public string XIAOXIBM3 { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(50)]
		public string XIAOXIMC4 { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(10)]
		public string XIAOXIBM4 { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(50)]
		public string XIAOXIMC5 { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(10)]
		public string XIAOXIBM5 { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(4)]
		public string SHIYONGFW { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(10)]
		public string YISHENGKS { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(100)]
		public string YISHENGKSMC { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? ZUOFEIBZ { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[Required]
		[StringLength(10)]
		public string XIUGAIREN { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[Required]
		public DateTime? XIUGAISJ { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? SHUNXUHAO { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? XIANSHISHU { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[StringLength(10)]
		public string XITONGID { get; set; }
		/// <summary>
		/// 0:靠左，1靠左
		/// </summary>
		public int? ZUOYOUBZ { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		[Ignore]
		public override void SetDefaultValue () 
		{}
		}
	}
