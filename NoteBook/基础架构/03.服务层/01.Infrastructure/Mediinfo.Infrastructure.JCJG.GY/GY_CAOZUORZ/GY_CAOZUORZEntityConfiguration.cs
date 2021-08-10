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
	public class GY_CAOZUORZEntityConfiguration : EntityTypeConfigurationBase<GY_CAOZUORZ>, IEntityTypeConfiguration
	{
		public GY_CAOZUORZEntityConfiguration()
		{
			var properties = typeof(GY_CAOZUORZ).GetProperties().ToList().Where(p => p.PropertyType == typeof(string)).ToList();
			properties.ForEach(o =>
			{
				ParameterExpression param = Expression.Parameter(typeof(GY_CAOZUORZ), "t1");
				var body = Expression.Property(param, o.Name);
				Expression<Func<GY_CAOZUORZ, string>> expr = Expression.Lambda<Func<GY_CAOZUORZ, string>>(body, param);
				this.Property(expr)
				.HasColumnType("VARCHAR2");
			});
		}
		    
	}
}
