using System;
using System.Data.Entity.ModelConfiguration;
using Mediinfo.Enterprise;
using System.Linq.Expressions;
using System.Linq;
using Mediinfo.Domain.JCJG.GY;
using Mediinfo.Infrastructure.Core.Entity;

namespace Mediinfo.Infrastructure.JCJG.GY
{
	public class GY_CANSHUEntityConfiguration : EntityTypeConfigurationBase<GY_CANSHU>, IEntityTypeConfiguration
	{
		public GY_CANSHUEntityConfiguration()
		{
			var properties = typeof(GY_CANSHU).GetProperties().ToList().Where(p => p.PropertyType == typeof(string)).ToList();
			properties.ForEach(o =>
			{
				ParameterExpression param = Expression.Parameter(typeof(GY_CANSHU), "t1");
				var body = Expression.Property(param, o.Name);
				Expression<Func<GY_CANSHU, string>> expr = Expression.Lambda<Func<GY_CANSHU, string>>(body, param);
				this.Property(expr)
				.HasColumnType("VARCHAR2");
			});
		}
		    
	}
}
