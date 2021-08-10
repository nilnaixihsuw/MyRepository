using System;
using System.Data.Entity.ModelConfiguration;
using Mediinfo.Enterprise;
using System.Linq.Expressions;
using System.Linq;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Domain.JCJG.GY;
namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_CHUANGKOUZY_NEWEntityConfiguration : EntityTypeConfigurationBase<GY_CHUANGKOUZY_NEW>, IEntityTypeConfiguration
	{
		public GY_CHUANGKOUZY_NEWEntityConfiguration()
		{
			var properties = typeof(GY_CHUANGKOUZY_NEW).GetProperties().ToList().Where(p => p.PropertyType == typeof(string)).ToList();
			properties.ForEach(o =>
			{
				ParameterExpression param = Expression.Parameter(typeof(GY_CHUANGKOUZY_NEW), "t1");
				var body = Expression.Property(param, o.Name);
				Expression<Func<GY_CHUANGKOUZY_NEW, string>> expr = Expression.Lambda<Func<GY_CHUANGKOUZY_NEW, string>>(body, param);
				this.Property(expr)
				.HasColumnType("VARCHAR2");
			});
		}
		    
	}
}
