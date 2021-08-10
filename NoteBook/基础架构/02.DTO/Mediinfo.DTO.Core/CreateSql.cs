using Mediinfo.Enterprise.Exceptions;

using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Mediinfo.DTO.Core
{
    /// <summary>
    /// 构造sql语句
    /// </summary>
    public class CreateSql
    {
        #region 老代码
        //public static string Build<T>(T Entity) where T : QueryDTO
        //{
        //    StringBuilder sql = new StringBuilder(512);
        //    var Properties = Entity.GetType().GetProperties().ToList();
        //    var lastsql = Create(Entity, Properties);
        //    if (Entity.UnionAlls != null && Entity.UnionAlls.Count > 0)
        //    {
        //        Entity.UnionAlls.ForEach(o =>
        //        {
        //            Entity.QueryWhere = o.QueryWhere;
        //            Entity.QueryHaving = o.QueryHaving;
        //            Entity.Selects = o.Selects;
        //            Entity.GroupBys = o.GroupBys;
        //            Entity.OrderBys = o.OrderBys;
        //            Entity.Joins = o.Joins;
        //            Entity.DTOtype = o.DTOtype;
        //            sql.Append(Create(Entity, Properties));
        //            sql.Append(" Union All ");
        //        });
        //    }
        //    if (Entity.Unions != null && Entity.Unions.Count > 0)
        //    {
        //        Entity.Unions.ForEach(o =>
        //        {
        //            Entity.QueryWhere = o.QueryWhere;
        //            Entity.QueryHaving = o.QueryHaving;
        //            Entity.Selects = o.Selects;
        //            Entity.GroupBys = o.GroupBys;
        //            Entity.OrderBys = o.OrderBys;
        //            Entity.Joins = o.Joins;
        //            Entity.DTOtype = o.DTOtype;
        //            sql.Append(Create(Entity, Properties));
        //            sql.Append(" Union ");
        //        });
        //    }
        //    if (Entity.Minuss != null && Entity.Minuss.Count > 0)
        //    {
        //        Entity.Minuss.ForEach(o =>
        //        {
        //            Entity.QueryWhere = o.QueryWhere;
        //            Entity.QueryHaving = o.QueryHaving;
        //            Entity.Selects = o.Selects;
        //            Entity.GroupBys = o.GroupBys;
        //            Entity.OrderBys = o.OrderBys;
        //            Entity.Joins = o.Joins;
        //            Entity.DTOtype = o.DTOtype;
        //            sql.Append(Create(Entity, Properties));
        //            sql.Append(" Minus ");
        //        });
        //    }
        //    sql.Append(lastsql);
        //    return sql.ToString();
        //}

        //private static StringBuilder Create<T>(T Entity, List<System.Reflection.PropertyInfo> Properties) where T : QueryDTO
        //{
        //    List<string> TableNames = new List<string>();
        //    StringBuilder select = new StringBuilder("select ", 128);
        //    StringBuilder from = new StringBuilder(" from ", 32);
        //    StringBuilder where = new StringBuilder(" where ", 128);
        //    StringBuilder group = new StringBuilder(" group by ", 32);
        //    StringBuilder having = new StringBuilder(" having ", 64);
        //    StringBuilder orderby = new StringBuilder(" order by ", 32);
        //    //拼select
        //    if (Entity.Selects != null && Entity.Selects.Count > 0)
        //    {
        //        Dictionary<string, string> columnNames = new Dictionary<string, string>();
        //        Properties.ForEach(o =>
        //        {
        //            if (o.PropertyType == typeof(object))
        //            {
        //                var attr = o.GetCustomAttributes(typeof(TableColumnAttribute), false);
        //                if (attr.Count() == 1)
        //                {
        //                    if (!TableNames.Contains((attr[0] as TableColumnAttribute).TableName) && Entity.Selects.Where(p => p.Key.Split('.')[0].ToUpper() == (attr[0] as TableColumnAttribute).TableName.ToUpper()).Count() > 0)
        //                    {
        //                        TableNames.Add((attr[0] as TableColumnAttribute).TableName);
        //                    }
        //                    columnNames.Add((attr[0] as TableColumnAttribute).TableName + "." + (attr[0] as TableColumnAttribute).ColumnName, o.Name);
        //                }
        //                else
        //                {
        //                    var fttr = o.GetCustomAttributes(typeof(FictitiousAttribute), false);
        //                    if (fttr.Count() == 0)
        //                    {
        //                        throw new DTOException("属性" + o.Name + "未标记为TableColumnAttribute或者FictitiousAttribute");
        //                    }
        //                    else if (fttr.Count() > 1)
        //                    {
        //                        throw new DTOException("属性" + o.Name + "标记了多个FictitiousAttribute");
        //                    }
        //                    else
        //                    {
        //                        columnNames.Add((fttr[0] as FictitiousAttribute).DescribColmun, o.Name);
        //                    }
        //                }
        //            }
        //        });
        //        Entity.Selects.ForEach(o =>
        //        {
        //            if (string.IsNullOrEmpty(o.Value))
        //            {
        //                select.Append(o.Key);
        //            }
        //            else
        //            {
        //                select.Append(string.Format(o.Value, o.Key));
        //            }
        //            if (columnNames.ContainsKey(o.Key))
        //            {
        //                select.Append(" as ");
        //                select.Append(columnNames[o.Key]);
        //            }
        //            else
        //            {
        //                throw new DTOException("未找到" + o.Key + "对应的类属性");
        //            }
        //            select.Append(',');
        //        });
        //    }
        //    else
        //    {
        //        Properties.ForEach(o =>
        //        {
        //            if (o.PropertyType == typeof(object))
        //            {
        //                var attr = o.GetCustomAttributes(typeof(TableColumnAttribute), false);
        //                if (attr != null && attr.Count() == 1)
        //                {
        //                    if (!TableNames.Contains((attr[0] as TableColumnAttribute).TableName))
        //                    {
        //                        TableNames.Add((attr[0] as TableColumnAttribute).TableName);
        //                    }
        //                    select.Append((attr[0] as TableColumnAttribute).TableName);
        //                    select.Append('.');
        //                    select.Append((attr[0] as TableColumnAttribute).ColumnName);
        //                    select.Append(" as ");
        //                    select.Append(o.Name);
        //                    select.Append(',');
        //                }
        //                else
        //                {
        //                    var fttr = o.GetCustomAttributes(typeof(FictitiousAttribute), false);
        //                    if (fttr.Count() == 0)
        //                    {
        //                        throw new DTOException("属性" + o.Name + "未标记为TableColumnAttribute或者FictitiousAttribute");
        //                    }
        //                    else if (fttr.Count() > 1)
        //                    {
        //                        throw new DTOException("属性" + o.Name + "标记了多个FictitiousAttribute");
        //                    }
        //                    else
        //                    {
        //                        //select.Append(string.IsNullOrWhiteSpace((fttr[0] as FictitiousAttribute).DescribColmun) ? "''" : (fttr[0] as FictitiousAttribute).DescribColmun);
        //                        //select.Append(" as ");
        //                        //select.Append(o.Name);
        //                        //select.Append(',');
        //                    }
        //                }
        //            }
        //        });
        //    }
        //    select = select.Remove(select.Length - 1, 1);
        //    //拼from 和 关联
        //    if (Entity.Joins == null)
        //    {
        //        if (TableNames.Count == 1)
        //        {
        //            from.Append(TableNames[0]);
        //        }
        //        else if (TableNames.Count > 1)
        //        {
        //            List<KeyValuePair<string, string>> relations = new List<KeyValuePair<string, string>>();
        //            Properties.ForEach(o =>
        //            {
        //                var attrs = o.GetCustomAttributes(typeof(RelationAttribute), false);
        //                if (attrs.Count() > 0)
        //                {
        //                    var cattr = o.GetCustomAttributes(typeof(TableColumnAttribute), false);
        //                    if (cattr.Count() == 0 || cattr.Count() > 1)
        //                    {
        //                        throw new DTOException("属性" + o.Name + "未标记为TableColumnAttribute");
        //                    }
        //                    else if (cattr.Count() > 1)
        //                    {
        //                        throw new DTOException("属性" + o.Name + "标记了多个TableColumnAttribute");
        //                    }
        //                    attrs.ToList().ForEach(p =>
        //                    {
        //                        string leftColumnName = string.Empty;
        //                        string rightColumnName = string.Empty;
        //                        leftColumnName = (cattr[0] as TableColumnAttribute).TableName + "." + (cattr[0] as TableColumnAttribute).ColumnName + (p as RelationAttribute).Join;
        //                        var property = Entity.GetType().GetProperty(((RelationAttribute)p).PropertyName);
        //                        if (property != null)
        //                        {
        //                            var rattrs = property.GetCustomAttributes(typeof(RelationAttribute), false);
        //                            var relationAttr = rattrs.ToList().Where(q => ((RelationAttribute)q).PropertyName == o.Name).FirstOrDefault();
        //                            if (relationAttr != null)
        //                            {
        //                                if (!string.IsNullOrWhiteSpace((relationAttr as RelationAttribute).Join) && !string.IsNullOrWhiteSpace((p as RelationAttribute).Join))
        //                                {
        //                                    throw new DTOException("属性" + o.Name + "和属性" + property.Name + "不能同时标记外连接");
        //                                }
        //                                var tableAttrs = property.GetCustomAttributes(typeof(TableColumnAttribute), false);
        //                                if (tableAttrs.Count() == 1)
        //                                {
        //                                    rightColumnName = (tableAttrs[0] as TableColumnAttribute).TableName + "." + (tableAttrs[0] as TableColumnAttribute).ColumnName + (relationAttr as RelationAttribute).Join;
        //                                }
        //                                else
        //                                {
        //                                    throw new DTOException("属性" + o.Name + "标记为TableColumnAttribute不正确");
        //                                }
        //                            }
        //                            else
        //                            {
        //                                throw new DTOException("属性" + property.Name + "上没有标记对应属性" + o.Name + "的RelationAttribute");
        //                            }
        //                        }
        //                        else
        //                        {
        //                            throw new DTOException("属性" + o.Name + "标记的Relation不存在");
        //                        }
        //                        bool same = false;
        //                        relations.ForEach(r =>
        //                        {
        //                            if ((r.Key == leftColumnName && r.Value == rightColumnName) || (r.Key == rightColumnName && r.Value == leftColumnName))
        //                            {
        //                                same = true;
        //                            }
        //                        });
        //                        if (!same)
        //                        {
        //                            relations.Add(new KeyValuePair<string, string>(leftColumnName, rightColumnName));
        //                        }
        //                    });
        //                }
        //            });
        //            if (TableNames.Count == 1)
        //            {
        //                from.Append(TableNames[0]);
        //            }
        //            else if (relations.Count >= TableNames.Count - 1)
        //            {
        //                TableNames.ForEach(o =>
        //                {
        //                    from.Append(o);
        //                    from.Append(',');
        //                });
        //                from = from.Remove(from.Length - 1, 1);
        //                relations.ForEach(o =>
        //                {
        //                    where.Append(o.Key);
        //                    where.Append(" = ");
        //                    where.Append(o.Value);
        //                    where.Append(" and ");
        //                });
        //            }
        //            else
        //            {
        //                throw new DTOException("RelationAttribute设置不正确");
        //            }
        //            from.Append(" ");
        //        }
        //        else
        //        {
        //            throw new DTOException("无法获取表名");
        //        }
        //    }
        //    else
        //    {
        //        Entity.Joins.joins.ForEach(o =>
        //        {
        //            var property = Properties.Where(p => p.Name.ToUpper() == o.Key.ColumnName.ToUpper()).FirstOrDefault();
        //            var cattr = property.GetCustomAttributes(typeof(TableColumnAttribute), false);
        //            if (cattr.Count() == 0)
        //            {
        //                throw new DTOException("属性" + property.Name + "未标记为TableColumnAttribute");
        //            }
        //            else if (cattr.Count() > 1)
        //            {
        //                throw new DTOException("属性" + property.Name + "标记了多个TableColumnAttribute");
        //            }
        //            if (!TableNames.Contains((cattr[0] as TableColumnAttribute).TableName))
        //            {
        //                TableNames.Add((cattr[0] as TableColumnAttribute).TableName);
        //            }
        //            where.Append((cattr[0] as TableColumnAttribute).TableName + "." + (cattr[0] as TableColumnAttribute).ColumnName + o.Key.Out);
        //            where.Append(" = ");
        //            property = Properties.Where(p => p.Name.ToUpper() == o.Value.ColumnName.ToUpper()).FirstOrDefault();
        //            cattr = property.GetCustomAttributes(typeof(TableColumnAttribute), false);
        //            if (cattr.Count() == 0)
        //            {
        //                throw new DTOException("属性" + property.Name + "未标记为TableColumnAttribute");
        //            }
        //            else if (cattr.Count() > 1)
        //            {
        //                throw new DTOException("属性" + property.Name + "标记了多个TableColumnAttribute");
        //            }
        //            if (!TableNames.Contains((cattr[0] as TableColumnAttribute).TableName))
        //            {
        //                TableNames.Add((cattr[0] as TableColumnAttribute).TableName);
        //            }
        //            where.Append((cattr[0] as TableColumnAttribute).TableName + "." + (cattr[0] as TableColumnAttribute).ColumnName + o.Value.Out);
        //            where.Append(" and ");
        //        });
        //        if (TableNames.Count == 1)
        //        {
        //            from.Append(TableNames[0]);
        //        }
        //        else if (TableNames.Count > 1)
        //        {
        //            TableNames.ForEach(o =>
        //            {
        //                from.Append(o);
        //                from.Append(',');
        //            });
        //            from = from.Remove(from.Length - 1, 1);
        //        }
        //    }
        //    //拼where条件
        //    if (Entity.QueryWhere != null)
        //    {
        //        if (Entity.QueryWhere.where != null && (Entity.QueryWhere.wheres == null || Entity.QueryWhere.wheres.Count == 0))
        //        {
        //            var queryWhere = Entity.QueryWhere.where;
        //            where.Append('(');
        //            NewWhere(where, Properties, queryWhere);
        //            where.Append(')');
        //        }
        //        else if (Entity.QueryWhere.wheres != null && Entity.QueryWhere.wheres.Count > 0)
        //        {
        //            where.Append('(');
        //            Entity.QueryWhere.wheres.ForEach(o =>
        //            {
        //                if (string.IsNullOrEmpty(o.Key))
        //                {
        //                    NewWhere(where, Properties, o.Value);
        //                }
        //                else
        //                {
        //                    where.Append(o.Key);
        //                }
        //            });
        //            where.Append(')');
        //        }
        //        else
        //        {
        //            where = where.Clear();
        //        }
        //    }
        //    else
        //    {
        //        if (TableNames.Count > 1)
        //        {
        //            where = where.Remove(where.Length - 5, 5);
        //        }
        //        else
        //        {
        //            where = where.Clear();
        //        }
        //    }

        //    //拼group
        //    if (Entity.GroupBys != null && Entity.GroupBys.Count > 0)
        //    {
        //        Entity.GroupBys.ForEach(o =>
        //        {
        //            var property = Properties.Where(p => p.Name.ToUpper() == o.ColumnName.ToUpper()).FirstOrDefault();
        //            var cattr = property.GetCustomAttributes(typeof(TableColumnAttribute), false);
        //            if (cattr.Count() == 0)
        //            {
        //                throw new DTOException("属性" + property.Name + "未标记为TableColumnAttribute");
        //            }
        //            else if (cattr.Count() > 1)
        //            {
        //                throw new DTOException("属性" + property.Name + "标记了多个TableColumnAttribute");
        //            }
        //            group.Append((cattr[0] as TableColumnAttribute).TableName + "." + (cattr[0] as TableColumnAttribute).ColumnName);
        //            group.Append(',');
        //        });
        //        group = group.Remove(group.Length - 1, 1);
        //    }
        //    else
        //    {
        //        group = group.Clear();
        //    }

        //    //拼having
        //    if (Entity.QueryHaving != null)
        //    {
        //        if (Entity.QueryHaving.where != null && (Entity.QueryHaving.wheres == null || Entity.QueryHaving.wheres.Count == 0))
        //        {
        //            var queryHaving = Entity.QueryHaving.where;
        //            NewWhere(having, Properties, queryHaving);
        //        }
        //        else if (Entity.QueryHaving.wheres != null && Entity.QueryHaving.wheres.Count > 0)
        //        {
        //            Entity.QueryHaving.wheres.ForEach(o =>
        //            {
        //                if (string.IsNullOrEmpty(o.Key))
        //                {
        //                    NewWhere(having, Properties, o.Value);
        //                }
        //                else
        //                {
        //                    having.Append(o.Key);
        //                }
        //            });
        //        }
        //        else
        //        {
        //            having = having.Clear();
        //        }
        //    }
        //    else
        //    {
        //        having = having.Clear();
        //    }

        //    //拼order by
        //    if (Entity.OrderBys != null && Entity.OrderBys.Count > 0)
        //    {
        //        Entity.OrderBys.ForEach(o =>
        //        {
        //            var property = Properties.Where(p => p.Name.ToUpper() == o.ColumnName.ToUpper()).FirstOrDefault();
        //            var cattr = property.GetCustomAttributes(typeof(TableColumnAttribute), false);
        //            if (cattr.Count() == 0)
        //            {
        //                throw new DTOException("属性" + property.Name + "未标记为TableColumnAttribute");
        //            }
        //            else if (cattr.Count() > 1)
        //            {
        //                throw new DTOException("属性" + property.Name + "标记了多个TableColumnAttribute");
        //            }
        //            if (string.IsNullOrEmpty(o.Descing))
        //            {
        //                orderby.Append((cattr[0] as TableColumnAttribute).TableName + "." + (cattr[0] as TableColumnAttribute).ColumnName);
        //            }
        //            else
        //            {
        //                orderby.Append((cattr[0] as TableColumnAttribute).TableName + "." + (cattr[0] as TableColumnAttribute).ColumnName);
        //                orderby.Append(" ");
        //                orderby.Append(o.Descing);
        //            }
        //            orderby.Append(',');
        //        });
        //        orderby = orderby.Remove(orderby.Length - 1, 1);
        //    }
        //    else
        //    {
        //        orderby = orderby.Clear();
        //    }
        //    StringBuilder sql = new StringBuilder(256);
        //    sql.Append(select);
        //    sql.Append(from);
        //    sql.Append(where);
        //    sql.Append(group);
        //    sql.Append(having);
        //    sql.Append(orderby);
        //    return sql;
        //}

        //private static void NewWhere(StringBuilder where, List<System.Reflection.PropertyInfo> Properties, Tuple<Property, string, string> queryWhere)
        //{
        //    if (queryWhere.Item1.ColumnName == "1")
        //    {
        //        where.Append(queryWhere.Item1.ColumnName);
        //        where.Append(queryWhere.Item2);
        //        where.Append(queryWhere.Item3);
        //    }
        //    else
        //    {
        //        var property = Properties.Where(p => p.Name.ToUpper() == queryWhere.Item1.ColumnName.ToUpper()).FirstOrDefault();
        //        var cattr = property.GetCustomAttributes(typeof(TableColumnAttribute), false);
        //        if (cattr.Count() == 0)
        //        {
        //            throw new DTOException("属性" + property.Name + "未标记为TableColumnAttribute");
        //        }
        //        else if (cattr.Count() > 1)
        //        {
        //            throw new DTOException("属性" + property.Name + "标记了多个TableColumnAttribute");
        //        }
        //        if (queryWhere.Item1.Aggregates != null && queryWhere.Item1.Aggregates.Count > 0)
        //        {
        //            where.Append(string.Format(queryWhere.Item1.Aggregates[0], (cattr[0] as TableColumnAttribute).TableName + "." + (cattr[0] as TableColumnAttribute).ColumnName));
        //        }
        //        else
        //        {
        //            where.Append((cattr[0] as TableColumnAttribute).TableName + "." + (cattr[0] as TableColumnAttribute).ColumnName);
        //        }
        //        where.Append(queryWhere.Item2);
        //        if (queryWhere.Item3 == null)
        //        {
        //            if (queryWhere.Item1.Value == null)
        //            {
        //                where.Append("''");
        //            }
        //            else if (queryWhere.Item1.Value.GetType().IsValueType)
        //            {
        //                where.Append(queryWhere.Item1.Value);
        //            }
        //            else if (queryWhere.Item1.Value is string)
        //            {
        //                where.Append("'");
        //                where.Append(queryWhere.Item1.Value);
        //                where.Append("'");
        //            }
        //            else if (queryWhere.Item1.Value is DateTime)
        //            {
        //                where.Append("TO_DATE('");
        //                where.Append(((DateTime)queryWhere.Item1.Value).ToString("yyyy-MM-dd HH:mm:ss"));
        //                where.Append("', 'yyyy-mm-dd hh24:mi:ss')");
        //            }
        //            else if (queryWhere.Item1.Value is Value)
        //            {
        //                if (((Value)queryWhere.Item1.Value).PropertyValue == null)
        //                {
        //                    where.Append("''");
        //                }
        //                else if (((Value)queryWhere.Item1.Value).PropertyValue.GetType().IsValueType)
        //                {
        //                    where.Append(((Value)queryWhere.Item1.Value).PropertyValue);
        //                }
        //                else if (((Value)queryWhere.Item1.Value).PropertyValue is string)
        //                {
        //                    where.Append("'");
        //                    where.Append(((Value)queryWhere.Item1.Value).PropertyValue);
        //                    where.Append("'");
        //                }
        //                else if (((Value)queryWhere.Item1.Value).PropertyValue is DateTime)
        //                {
        //                    where.Append("TO_DATE('");
        //                    where.Append(((DateTime)((Value)queryWhere.Item1.Value).PropertyValue).ToString("yyyy-MM-dd HH:mm:ss"));
        //                    where.Append("', 'yyyy-mm-dd hh24:mi:ss')");
        //                }
        //            }
        //            else
        //            {
        //                throw new DTOException(queryWhere.Item1.ColumnName + "的数据类型不被支持");
        //            }
        //        }
        //        else
        //        {
        //            where.Append(queryWhere.Item3);
        //        }
        //    }
        //}

        #endregion
        
        /// <summary>
        /// 构造查询语句
        /// </summary>
        /// <param name="Entity">DTO</param>
        /// <returns></returns>
        public static string Build(DTOBase Entity)
        {
           StringBuilder sql = new StringBuilder(256);

            var type = Entity.GetType();

            sql.Append("select ");

            if (Entity.SelectedColumns.Count == 0)
            {
                type.GetProperties().ToList().ForEach(o =>
                {
                    //if (o.GetCustomAttributes(typeof(NotMappedAttribute), false).Count() <= 0)
                    //{ 
                    var notfieldAttr = o.GetCustomAttributes(typeof(NotFieldAttribute), false);
                    if (notfieldAttr.Count() == 0)
                    {
                        var dattr = o.GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (dattr.Count() == 0)
                        {
                            var fattr = o.GetCustomAttributes(typeof(FictitiousAttribute), false);
                            if (fattr.Count() == 0)
                            {
                                throw new DTOException("属性" + o.Name + "未标记为DescriptionAttribute或者FictitiousAttribute");
                            }
                            else
                            {
                                if (string.IsNullOrWhiteSpace((fattr[0] as FictitiousAttribute).DescribColmun))
                                {
                                    sql.Append(o);
                                    sql.Append(',');
                                }
                                else
                                {
                                    sql.Append((fattr[0] as FictitiousAttribute).DescribColmun);
                                    sql.Append(' ');
                                    sql.Append(o);
                                    sql.Append(',');
                                }
                            }
                        }
                        else
                        {
                            //var names = (dattr[0] as DescriptionAttribute).Description.Split('~');
                            //sql.Append(names[0] +"."+ names[1]);
                            //sql.Append(' ');
                            sql.Append(o.Name);
                            sql.Append(',');
                        }
                        //}
                    }
                });
            }
            else
            {
                var propertys = type.GetProperties().ToList();
                Entity.SelectedColumns.ForEach(o =>
                {
                    var property = propertys.Where(p => o == p.Name).FirstOrDefault();
                    if (property == null)
                    {
                        throw new DTOException("在实体" + type.Name + "中,未找到属性" + o);
                    }
                    else
                    {
                        var dattr = property.GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (dattr.Count() == 0)
                        {
                            var fattr = property.GetCustomAttributes(typeof(FictitiousAttribute), false);
                            if (fattr.Count() == 0)
                            {
                                throw new DTOException("属性" + o + "未标记为DescriptionAttribute或者FictitiousAttribute");
                            }
                            else
                            {
                                if (string.IsNullOrWhiteSpace((fattr[0] as FictitiousAttribute).DescribColmun))
                                {
                                    sql.Append(o);
                                    sql.Append(',');
                                }
                                else
                                {
                                    sql.Append((fattr[0] as FictitiousAttribute).DescribColmun);
                                    sql.Append(' ');
                                    sql.Append(o);
                                    sql.Append(',');
                                }
                            }
                        }
                        else
                        {
                            //var names = (dattr[0] as DescriptionAttribute).Description.Split('~');
                            //sql.Append(names[0] + "." + names[1]);
                            //sql.Append(' ');
                            sql.Append(o);
                            sql.Append(',');
                        }
                    }
                });
            }
            sql = sql.Remove(sql.Length - 1, 1);
            sql.Append(" from (");
            if (string.IsNullOrWhiteSpace(Entity.GetDefaultSQL()))
            {
                if (string.IsNullOrWhiteSpace(Entity.QuerySql) || Entity.QuerySql.ToUpper().IndexOf("Select") < 0)
                {
                    throw new DTOException("Sql语句无法执行,请检查传入的WHERE条件和" + type.Name + "类是否标记了DefaultSql属性");
                }
                else
                {
                    //sql.Append(Entity.QuerySql);
                    sql.AppendFormat(" {0}", Entity.QuerySql);
                }
                sql.Append(")");
            }
            else
            {
                sql.Append(Entity.GetDefaultSQL());
                sql.Append(")");
                // sql.Append(Entity.QuerySql);
                sql.AppendFormat(" {0}", Entity.QuerySql);
            }
            return sql.ToString();
        }
    }
}
