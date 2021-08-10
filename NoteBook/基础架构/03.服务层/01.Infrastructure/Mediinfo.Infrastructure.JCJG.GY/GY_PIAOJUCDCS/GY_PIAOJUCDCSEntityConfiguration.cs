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
	public class GY_PIAOJUCDCSEntityConfiguration : EntityTypeConfiguration<GY_PIAOJUCDCS>, IEntityTypeConfiguration
	{
		public GY_PIAOJUCDCSEntityConfiguration()
		{
			var properties = typeof(GY_PIAOJUCDCS).GetProperties().ToList().Where(p => p.PropertyType == typeof(string)).ToList();
			properties.ForEach(o =>
			{
				ParameterExpression param = Expression.Parameter(typeof(GY_PIAOJUCDCS), "t1");
				var body = Expression.Property(param, o.Name);
				Expression<Func<GY_PIAOJUCDCS, string>> expr = Expression.Lambda<Func<GY_PIAOJUCDCS, string>>(body, param);
				this.Property(expr)
				.HasColumnType("VARCHAR2");
			});
		}
		    
	}
}
