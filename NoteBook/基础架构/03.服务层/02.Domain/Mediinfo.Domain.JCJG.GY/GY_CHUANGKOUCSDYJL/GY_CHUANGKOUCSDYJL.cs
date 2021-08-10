using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mediinfo.Infrastructure.Core.Entity;
namespace Mediinfo.Domain.JCJG.GY
{
	[Table("GY_CHUANGKOUCSDYJL")]
	public partial class GY_CHUANGKOUCSDYJL : EntityBase, IEntityMapper
	{
		/// <summary>
		/// 记录ID
		/// </summary>
		[Key]
		[Column(Order=1)]
		[StringLength(50)]
		public string JILUID { get; set; }
		/// <summary>
		/// 窗口名称
		/// </summary>
		[StringLength(100)]
		public string CHUANGKOUMC { get; set; }
		/// <summary>
		/// 参数ID
		/// </summary>
		[StringLength(100)]
		public string CANSHUID { get; set; }
		/// <summary>
		/// 参数值
		/// </summary>
		[StringLength(100)]
		public string CANSHUZHI { get; set; }
		/// <summary>
		/// 应用ID
		/// </summary>
		[StringLength(100)]
		public string YINGYONGID { get; set; }
		/// <summary>
		/// 默认值
		/// </summary>
		[StringLength(100)]
		public string DEFAULTVALUE { get; set; }
		/// <summary>
		///默认值方法 
		/// </summary>
		public void SetDefaultValue () 
		{}
		}
	}
