using Mediinfo.Enterprise;
using Mediinfo.Infrastructure.Core;
using Mediinfo.Infrastructure.Core.DBContext;
using Mediinfo.Infrastructure.Core.Entity;
using Mediinfo.Infrastructure.Oracle.Core;
using NLog;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mediinfo.Infrastructure.JCJG
{
    /// <summary>
    /// HIS EF上下文
    /// </summary>
    public class HISDBContext : DBContextBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        StringBuilder SQL = new StringBuilder(256);
        /// <summary>
        /// 保存日志
        /// </summary>
        /// <param name="sql"></param>
        public void Log(string sql)
        {
            SQL.Append(sql);
            SQL.Append(Environment.NewLine);
            logger.Debug(sql);
        }
        /// <summary>
        /// 默认构造函数
        /// </summary>
        static HISDBContext()
        {
            Database.SetInitializer<HISDBContext>(null);
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="contextOwnsConnection"></param>
        /// <param name="defaultSchema"></param>
        public HISDBContext(OracleConnection conn, bool contextOwnsConnection, string defaultSchema) : base(conn, contextOwnsConnection, defaultSchema)
        {
            // this.Database.Log = Log;
            dBBulkOperations = new OracleDBBulkOperations(this);
            pagedQuery = new PagedQuery(this);
            pagedTableQuery = new PagedTableQuery(this);
        }

        /// <summary>
        /// 重写实体加载
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void loadEntity(DbModelBuilder modelBuilder)
        {
            //List<Type> types = new List<Type>();
            string dir = System.AppDomain.CurrentDomain.BaseDirectory;

            if (HttpContext.Current != null)
            {
                dir += "bin";
            }
            foreach (var ef in System.IO.Directory.EnumerateFiles(dir, "Mediinfo.Infrastructure.JCJG*.dll"))
            {
                Assembly asm = Assembly.LoadFrom(ef);
                var reg = modelBuilder.Configurations.AddFromAssembly(asm);
                if (null != asm)
                {
                    asm.GetTypes().Where(c => !c.IsAbstract && c.GetInterfaces().Where(o => o == typeof(IEntityMapper)).Count() > 0).ToList().ForEach(o =>
                    {
                        //IEntityMapper mapper = (IEntityMapper)o.Assembly.CreateInstance(o.FullName);
                        //if (null != mapper)
                        //    mapper.RegistTo(modelBuilder.Configurations);

                        //foreach (var propAttr in o.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.GetCustomAttribute<DecimalPrecisionAttribute>() != null).Select(
                        //       p => new { prop = p, attr = p.GetCustomAttribute<DecimalPrecisionAttribute>(true) }))
                        //{
                        //    var entityConfig = modelBuilder.GetType().GetMethod("Entity").MakeGenericMethod(o).Invoke(modelBuilder, null);
                        //    ParameterExpression param = ParameterExpression.Parameter(o, "c");
                        //    Expression property = Expression.Property(param, propAttr.prop.Name);
                        //    LambdaExpression lambdaExpression = Expression.Lambda(property, true,
                        //                                                             new ParameterExpression[]
                        //                                                                 {param});
                        //    DecimalPropertyConfiguration decimalConfig;
                        //    if (propAttr.prop.PropertyType.IsGenericType && propAttr.prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        //    {
                        //        MethodInfo methodInfo = entityConfig.GetType().GetMethods().Where(p => p.Name == "Property").ToList()[7];
                        //        decimalConfig = methodInfo.Invoke(entityConfig, new[] { lambdaExpression }) as DecimalPropertyConfiguration;
                        //    }
                        //    else
                        //    {
                        //        MethodInfo methodInfo = entityConfig.GetType().GetMethods().Where(p => p.Name == "Property").ToList()[6];
                        //        decimalConfig = methodInfo.Invoke(entityConfig, new[] { lambdaExpression }) as DecimalPropertyConfiguration;
                        //    }

                        //    decimalConfig.HasPrecision(propAttr.attr.Precision, propAttr.attr.Scale);
                        //}
                    });

                }
            }

            foreach (var ef in System.IO.Directory.EnumerateFiles(dir, "Mediinfo.Infrastructure.HRP*.dll"))
            {
                Assembly asm = Assembly.LoadFrom(ef);
                var reg = modelBuilder.Configurations.AddFromAssembly(asm);
                if (null != asm)
                {
                    asm.GetTypes().Where(c => !c.IsAbstract && c.GetInterfaces().Where(o => o == typeof(IEntityMapper)).Count() > 0).ToList().ForEach(o =>
                    {
                        //IEntityMapper mapper = (IEntityMapper)o.Assembly.CreateInstance(o.FullName);
                        //if (null != mapper)
                        //    mapper.RegistTo(modelBuilder.Configurations);

                        //foreach (var propAttr in o.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.GetCustomAttribute<DecimalPrecisionAttribute>() != null).Select(
                        //       p => new { prop = p, attr = p.GetCustomAttribute<DecimalPrecisionAttribute>(true) }))
                        //{
                        //    var entityConfig = modelBuilder.GetType().GetMethod("Entity").MakeGenericMethod(o).Invoke(modelBuilder, null);
                        //    ParameterExpression param = ParameterExpression.Parameter(o, "c");
                        //    Expression property = Expression.Property(param, propAttr.prop.Name);
                        //    LambdaExpression lambdaExpression = Expression.Lambda(property, true,
                        //                                                             new ParameterExpression[]
                        //                                                                 {param});
                        //    DecimalPropertyConfiguration decimalConfig;
                        //    if (propAttr.prop.PropertyType.IsGenericType && propAttr.prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        //    {
                        //        MethodInfo methodInfo = entityConfig.GetType().GetMethods().Where(p => p.Name == "Property").ToList()[7];
                        //        decimalConfig = methodInfo.Invoke(entityConfig, new[] { lambdaExpression }) as DecimalPropertyConfiguration;
                        //    }
                        //    else
                        //    {
                        //        MethodInfo methodInfo = entityConfig.GetType().GetMethods().Where(p => p.Name == "Property").ToList()[6];
                        //        decimalConfig = methodInfo.Invoke(entityConfig, new[] { lambdaExpression }) as DecimalPropertyConfiguration;
                        //    }

                        //    decimalConfig.HasPrecision(propAttr.attr.Precision, propAttr.attr.Scale);
                        //}
                    });

                }
            }

            

            base.loadEntity(modelBuilder);
        }

    }
}
