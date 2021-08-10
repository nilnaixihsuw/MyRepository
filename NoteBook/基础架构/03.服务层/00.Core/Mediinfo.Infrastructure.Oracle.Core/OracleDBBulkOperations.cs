using Mediinfo.Enterprise.Config;
using Mediinfo.Infrastructure.Core.DBContext;
using Mediinfo.Utility.Reflection;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace Mediinfo.Infrastructure.Oracle.Core
{
    /// <summary>
    /// oracle数据库批量操作类
    /// </summary>
    public class OracleDBBulkOperations : IDBBulkOperations
    {
        private DBContextBase dBContextBase = null;
        public OracleDBBulkOperations(DBContextBase dBContextBase)
        {
            this.dBContextBase = dBContextBase;
        }

        public int BulkSaveChanges(bool validateOnSaveEnabled = true, int bulkSize = 64)
        {
            // 如果为1的话直接调用默认的方法
            if (bulkSize == 1)
                return dBContextBase.SaveChanges();

            bool ownConnection = false;
            bool ownTransaction = false;
            DbConnection DbConnect = null;
            DbTransaction DbTrans = null;
            ObjectContext objectContext = ((IObjectContextAdapter)this.dBContextBase).ObjectContext;
            int totalCount = 0;
            //move By  zhukunpin 现在对接his1的时候gy_bingrenxx是视图，systemconfig里设置忽略了但是还是会先校验，导致报错
            //李宗申说直接去掉这块验证功能，验证消耗性能，又没什么作用，如果数据库插入有问题已经直接会报错的.
            //// 验证
            //if (dBContextBase.Configuration.ValidateOnSaveEnabled && validateOnSaveEnabled)
            //{
            //    var validationResults = dBContextBase.GetValidationErrors();
            //    if (validationResults.Any())
            //    {
            //        StringBuilder vErrors = new StringBuilder();
            //        foreach (var item in validationResults)
            //        {
            //            foreach (var e in item.ValidationErrors)
            //            {
            //                vErrors.Append(item.Entry.Entity.GetType() + ":" + e.PropertyName + " 验证失败，原因：" + e.ErrorMessage);
            //            }

            //        }
            //        throw new Enterprise.Exceptions.DBException("在BulkSaveChanges时，验证实体不通过:" + vErrors.ToString());
            //    }
            //}

            // 获取变化
            if (dBContextBase.Configuration.AutoDetectChangesEnabled && !dBContextBase.Configuration.ValidateOnSaveEnabled)
                objectContext.DetectChanges();

            try
            {
                #region 处理数据库事务及连接信息

                var store = this.GetStore(objectContext);
                DbConnect = store.Item1;
                DbTrans = store.Item2;

                if (DbConnect.State != ConnectionState.Open)
                {
                    DbConnect.Open();
                    ownConnection = true;
                }

                if (DbTrans == null)
                {
                    DbTrans = DbConnect.BeginTransaction();
                    ownTransaction = true;
                }

                #endregion

                // 获取变化的实体信息
                var changeList = objectContext.ObjectStateManager
                                          .GetObjectStateEntries(EntityState.Added | EntityState.Modified | System.Data.Entity.EntityState.Deleted)
                                          .Select(c => new EntityInfo
                                          {
                                              TableName = this.GetTableName(c.Entity),
                                              StateEntity = c,
                                              ColumnList = GetEntityInfo(c)
                                          })
                                          .Where(c => !string.IsNullOrWhiteSpace(c.TableName))
                                          .ToList();

                var tables = changeList.Select(c => c.TableName).Distinct().ToList();

                foreach (var tableName in tables)
                {
                    var list = changeList.Where(c => c.TableName == tableName).ToList().ToList();

                    // 删除
                    if (list.Where(c => c.StateEntity.State == EntityState.Deleted).Any())
                        totalCount += Delete(DbTrans, list, bulkSize);

                    // 修改
                    if (list.Where(c => c.StateEntity.State == EntityState.Modified).Any())
                        totalCount += Update(DbTrans, list, bulkSize);

                    // 新增
                    if (list.Where(c => c.StateEntity.State == EntityState.Added).Any())
                        totalCount += Insert(DbTrans, list, bulkSize);
                }

                if (ownTransaction)
                    DbTrans.Commit();

                return totalCount;
            }
            finally
            {
                if (DbTrans != null && ownTransaction)
                    DbTrans.Dispose();

                if (DbConnect != null && ownConnection)
                    DbConnect.Close();
            }
        }

        private int Insert(DbTransaction trans, List<EntityInfo> dataList, int bulkSize)
        {
            var cmd1 = dBContextBase.Database.Connection.CreateCommand();

            using (var cmd = (OracleCommand)dBContextBase.Database.Connection.CreateCommand())
            {
                StringBuilder insertSql = new StringBuilder(256);
                StringBuilder valuesSql = new StringBuilder(256);

                cmd.Transaction = (OracleTransaction)trans;

                int affectRows = 0;

                while (true)
                {
                    var insertList = dataList.Where(c => c.StateEntity.State == EntityState.Added).Take(bulkSize).ToList();
                    if (!insertList.Any())
                        break;

                    // 判断该表是否忽略保存
                    if (!IgnoreEntityEntityList.Contains(dataList.FirstOrDefault().TableName))
                    {
                        OracleParameter param = null;
                        object[] values = null;
                        int index = 0;

                        cmd.Parameters.Clear();
                        insertSql.Clear();
                        valuesSql.Clear();

                        foreach (var col in insertList[0].ColumnList)
                        {
                            insertSql.AppendFormat("{0},", col.DbName);
                            valuesSql.AppendFormat(":{0},", col.DbName);

                            values = new object[insertList.Count];
                            index = 0;

                            foreach (var entry in insertList)
                            {
                                values[index++] = entry.ColumnList.Where(c => c.ColumnName == col.ColumnName)
                                    .FirstOrDefault().CurrentValue;
                            }

                            param = new OracleParameter(col.DbName, col.DbType);
                            param.Value = values;
                            cmd.Parameters.Add(param);
                        }

                        insertSql = insertSql.Remove(insertSql.Length - 1, 1);
                        valuesSql = valuesSql.Remove(valuesSql.Length - 1, 1);

                        cmd.ArrayBindCount = insertList.Count();
                        cmd.BindByName = true;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = dBContextBase.Database.CommandTimeout.HasValue ? dBContextBase.Database.CommandTimeout.Value : 0;
                        cmd.CommandText = string.Format("insert into {0} ({1}) values ({2})", insertList[0].TableName, insertSql, valuesSql);

                        this.WriteSqlLog(dBContextBase, cmd);

                        var ret = cmd.ExecuteNonQuery();
                        if (ret <= 0)
                        {

                        }
                        else
                        {
                            affectRows += ret;
                        }
                    }
                    // 接受变化
                    foreach (var entry in insertList)
                    {
                        entry.StateEntity.AcceptChanges();
                    }
                }

                return affectRows;
            }
        }

        // 需要跟踪的表列表
        protected List<string> IgnoreEntityEntityList = MediinfoConfig.GetValue("SystemConfig.xml", "IgnoreEntityToSave").Split(',').ToList();

        private int Delete(DbTransaction trans, List<EntityInfo> dataList, int bulkSize)
        {
            using (var cmd = (OracleCommand)dBContextBase.Database.Connection.CreateCommand())
            {
                StringBuilder deleteSql = new StringBuilder(256);
                StringBuilder whereSql = new StringBuilder(256);

                cmd.Transaction = (OracleTransaction)trans;

                int affectRows = 0;

                while (true)
                {
                    var list = dataList.Where(c => c.StateEntity.State == EntityState.Deleted).Take(bulkSize).ToList();
                    if (list.Count <= 0)
                        break;

                    // 判断该表是否忽略保存
                    if (!IgnoreEntityEntityList.Contains(list.FirstOrDefault().TableName))
                    {
                        OracleParameter param = null;
                        object[] values = null;
                        int index = 0;

                        cmd.Parameters.Clear();
                        deleteSql.Clear();
                        whereSql.Clear();

                        if (list.FirstOrDefault().ColumnList.Where(c => c.IsKey == true).Count() <= 0)
                            throw new DbUpdateException(string.Format("未发现实体{0}的主键信息，不能完成删除操作", list.FirstOrDefault().TableName));

                        var colList = list.FirstOrDefault().ColumnList.Where(c => c.IsKey || c.IsChanged || c.ConcurrencyCheck).ToList();
                        var count = 0;

                        foreach (var col in colList)
                        {
                            //whereSql.AppendFormat("{0}{1}{2}", col.DbName, col.OriginalValue == null ? " is " : "=", ":p" + count.ToString());

                            // 删除数据基本上都是只有历史值
                            if (col.OriginalValue == null || col.OriginalValue == System.DBNull.Value)
                                whereSql.AppendFormat(" {0} is null And", col.DbName);
                            else
                                whereSql.AppendFormat(" {0}={1} And", col.DbName, ":p" + count.ToString());

                            // 批量参数化
                            values = new object[list.Count];
                            index = 0;

                            foreach (var entry in list)
                            {
                                values[index++] = entry.ColumnList.Where(c => c.ColumnName == col.ColumnName)
                                    .FirstOrDefault().OriginalValue;
                            }

                            param = new OracleParameter("p" + count.ToString(), col.DbType);
                            param.Value = values;
                            cmd.Parameters.Add(param);
                            count++;
                        }

                        whereSql = whereSql.Remove(whereSql.Length - 3, 3); //去掉最后一个And

                        cmd.ArrayBindCount = list.Count();
                        cmd.CommandType = CommandType.Text;
                        cmd.BindByName = true;
                        cmd.CommandTimeout = dBContextBase.Database.CommandTimeout.HasValue ? dBContextBase.Database.CommandTimeout.Value : 0;
                        cmd.CommandText = string.Format("Delete from {0} where {1}", list.FirstOrDefault().TableName, whereSql);

                        this.WriteSqlLog(dBContextBase, cmd);

                        var ret = cmd.ExecuteNonQuery();

                        if (ret < 0)
                        {
                            throw new DbUpdateConcurrencyException("SQL执行返回0行：" + cmd.CommandText);
                        }
                        else
                        {
                            affectRows = affectRows + ret;
                        }
                    }

                    //处理状态变化
                    foreach (var entry in list)
                    {
                        entry.StateEntity.AcceptChanges();
                    }
                }

                return affectRows;
            }
        }

        private int Update(DbTransaction trans, List<EntityInfo> dataList, int bulkSize)
        {
            using (var cmd = (OracleCommand)dBContextBase.Database.Connection.CreateCommand())
            {
                StringBuilder updateSql = new StringBuilder(256);
                StringBuilder whereSql = new StringBuilder(256);

                cmd.Transaction = (OracleTransaction)trans;

                int affectRows = 0;

                while (true)
                {
                    // 获取所有update实体
                    var updateList = dataList.Where(c => c.StateEntity.State == EntityState.Modified).Take(bulkSize).ToList();

                    if (updateList.Count <= 0)
                        break;

                    int count = 0;

                    // 初始化待执行的更新命令
                    Dictionary<string, List<List<OracleParameter>>> updCmdDict = new Dictionary<string, List<List<OracleParameter>>>();

                    foreach (var stateEntity in updateList)
                    {
                        // 判断该表是否忽略保存
                        if (!IgnoreEntityEntityList.Contains(stateEntity.TableName))
                        {
                            count = 0;
                            updateSql.Clear();
                            whereSql.Clear();

                            if (stateEntity.ColumnList.Where(c => c.IsKey == true).Count() <= 0)
                                throw new DbUpdateException(string.Format("未发现实体{0}的主键信息，不能完成更新操作", stateEntity.TableName));

                            var colList = stateEntity.ColumnList.Where(c => c.IsKey || c.IsChanged || c.ConcurrencyCheck).ToList();
                            OracleParameter param = null;

                            // 生成sql和参数命令
                            List<OracleParameter> paraList = new List<OracleParameter>();
                            foreach (var col in colList)
                            {
                                if (col.IsChanged)
                                {
                                    updateSql.AppendFormat("{0}={1},", col.DbName, ":p" + count.ToString());

                                    param = new OracleParameter("p" + count.ToString(), col.DbType);
                                    param.Value = col.CurrentValue;
                                    paraList.Add(param);

                                    count++;
                                }

                                if (col.OriginalValue == null || col.OriginalValue == System.DBNull.Value || string.IsNullOrEmpty(col.OriginalValue.ToString()))
                                {
                                    whereSql.AppendFormat(" {0} is null And", col.DbName);
                                }
                                else
                                {
                                    whereSql.AppendFormat(" {0}={1} And", col.DbName, ":p" + count.ToString());
                                }

                                param = new OracleParameter("p" + count.ToString(), col.DbType);
                                param.Value = col.OriginalValue;
                                paraList.Add(param);

                                count++;
                            }

                            updateSql = updateSql.Remove(updateSql.Length - 1, 1);
                            whereSql = whereSql.Remove(whereSql.Length - 3, 3);

                            // 以sql分组添加到待执行命令
                            string sql = string.Format("Update {0} Set {1} Where {2}", stateEntity.TableName, updateSql.ToString(), whereSql.ToString());
                            if (updCmdDict.ContainsKey(sql))
                            {
                                updCmdDict[sql].Add(paraList);
                            }
                            else
                            {
                                updCmdDict.Add(sql, new List<List<OracleParameter>>() { paraList });
                            }
                        }
                    }

                    // 遍历sql类型
                    foreach (var updCmd in updCmdDict)
                    {
                        cmd.Parameters.Clear();

                        // 取出每个sql的参数list
                        var updCmdParmList = updCmd.Value;

                        OracleParameter param = null;
                        object[] values = null;
                        int index = 0;

                        // 遍历参数list的所有列
                        foreach (var col in updCmdParmList[0].Select(m => m.ParameterName))
                        {
                            values = new object[updCmd.Value.Count()];
                            index = 0;

                            // 遍历所有sql
                            foreach (var upd in updCmd.Value)
                            {
                                values[index++] = upd.Where(m => m.ParameterName == col).FirstOrDefault().Value;
                            }

                            param = new OracleParameter(col, updCmd.Value[0].Where(m => m.ParameterName == col).FirstOrDefault().OracleDbType);
                            param.Value = values;
                            cmd.Parameters.Add(param);
                        }

                        cmd.ArrayBindCount = updCmd.Value.Count();
                        cmd.CommandType = CommandType.Text;
                        cmd.BindByName = true;
                        cmd.CommandTimeout = dBContextBase.Database.CommandTimeout.HasValue ? dBContextBase.Database.CommandTimeout.Value : 0;
                        cmd.CommandText = updCmd.Key;

                        this.WriteSqlLog(dBContextBase, cmd);

                        var ret = cmd.ExecuteNonQuery();
                        if (ret <= 0)
                        {
                            throw new DbUpdateConcurrencyException("更新失败，请刷新数据后重试：" + cmd.CommandText);
                        }
                        else
                        {
                            affectRows += ret;
                        }
                    }

                    //接受变化
                    foreach (var entry in updateList)
                    {
                        entry.StateEntity.AcceptChanges();
                    }
                }

                return affectRows;
            }
        }

        // 缓存模型中的字段排序
        private Dictionary<string, Dictionary<string, int>> EntityOrdinal = new Dictionary<string, Dictionary<string, int>>();

        private List<ColumnInfo> GetEntityInfo(ObjectStateEntry objectEntry)
        {
            var type = objectEntry.Entity.GetType();

            ColumnInfo columnInfo = null;
            List<ColumnInfo> columnList = new List<ColumnInfo>();

            // 获取变化的列
            List<string> changeColumns = objectEntry.GetModifiedProperties().ToList();

            // 历史值列表
            object[] originalValues = null;
            // 当前值列表
            object[] currentValues = null;
            foreach (var item in type.GetProperties())
            {
                if (item.GetCustomAttributes(typeof(NotMappedAttribute), false).Any())
                    continue;

                columnInfo = new ColumnInfo();

                // 物理列名
                var colatts = (ColumnAttribute[])item.GetCustomAttributes(typeof(ColumnAttribute), false);
                if (colatts.Count() > 0 && !string.IsNullOrWhiteSpace(colatts[0].Name))
                    columnInfo.DbName = colatts[0].Name;
                else
                    columnInfo.DbName = item.Name;

                // 主键
                var keyatts = (KeyAttribute[])item.GetCustomAttributes(typeof(KeyAttribute), false);
                if (keyatts.Count() > 0)
                    columnInfo.IsKey = true;
                else
                    columnInfo.IsKey = false;

                // 列名
                columnInfo.ColumnName = item.Name;

                // 数据类型
                columnInfo.DbType = GetOracleDbType(item.PropertyType);

                // 当前值
                if (objectEntry.State != EntityState.Deleted)
                {
                    var i = 0;
                    var name = objectEntry.EntitySet.ElementType.FullName;
                    if (EntityOrdinal.ContainsKey(name))
                    {
                        if (!EntityOrdinal[name].ContainsKey(item.Name))
                        {
                            i = objectEntry.CurrentValues.GetOrdinal(item.Name);

                            EntityOrdinal[name].Add(item.Name, i);
                        }
                        else
                        {
                            i = EntityOrdinal[name][item.Name];
                        }
                    }
                    else
                    {
                        i = objectEntry.CurrentValues.GetOrdinal(item.Name);

                        var ko = new Dictionary<string, int>();
                        ko.Add(item.Name, i);

                        EntityOrdinal.Add(name, ko);
                    }

                    if (currentValues == null)
                    {
                        currentValues = new object[objectEntry.CurrentValues.FieldCount];
                        objectEntry.CurrentValues.GetValues(currentValues);
                    }

                    columnInfo.CurrentValue = currentValues[i];
                }

                // 原始值
                if (objectEntry.State != EntityState.Added)
                {
                    var i = 0;
                    var name = objectEntry.EntitySet.ElementType.FullName;
                    if (EntityOrdinal.ContainsKey(name))
                    {
                        if (!EntityOrdinal[name].ContainsKey(item.Name))
                        {
                            i = objectEntry.OriginalValues.GetOrdinal(item.Name);
                            EntityOrdinal[name].Add(item.Name, i);
                        }
                        else
                        {
                            i = EntityOrdinal[name][item.Name];
                        }
                    }
                    else
                    {
                        i = objectEntry.OriginalValues.GetOrdinal(item.Name);

                        var ko = new Dictionary<string, int>();
                        ko.Add(item.Name, i);

                        EntityOrdinal.Add(name, ko);
                    }

                    if (originalValues == null)
                    {
                        originalValues = new object[objectEntry.OriginalValues.FieldCount];
                        objectEntry.OriginalValues.GetValues(originalValues);
                    }
                    columnInfo.OriginalValue = originalValues[i];
                }

                // 值是否被修改
                if (objectEntry.State == EntityState.Modified)
                {
                    if (changeColumns.Contains(columnInfo.ColumnName))
                        columnInfo.IsChanged = true;
                    else
                        columnInfo.IsChanged = false;
                }

                columnList.Add(columnInfo);
            }
            return columnList;
        }

        private OracleDbType GetOracleDbType(Type type)
        {
            OracleDbType dataType = OracleDbType.Varchar2;

            if (type == typeof(string))
            {
                dataType = OracleDbType.Varchar2;
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                dataType = OracleDbType.Date;
            }
            else if (type == typeof(int) || type == typeof(int?))
            {
                dataType = OracleDbType.Int32;
            }
            else if (type == typeof(short) || type == typeof(short?))
            {
                dataType = OracleDbType.Int32;
            }
            else if (type == typeof(long) || type == typeof(long?) || type == typeof(Int64) || type == typeof(Int64?))
            {
                dataType = OracleDbType.Int64;
            }
            else if (type == typeof(decimal) || type == typeof(decimal?) || type == typeof(double) || type == typeof(double?))
            {
                dataType = OracleDbType.Decimal;
            }
            else if (type == typeof(Guid))
            {
                dataType = OracleDbType.Varchar2;
            }
            else if (type == typeof(bool) || type == typeof(bool?) || type == typeof(Boolean) || type == typeof(Boolean?))
            {
                dataType = OracleDbType.Byte;
            }
            else if (type == typeof(byte[]))
            {
                dataType = OracleDbType.Blob;
            }
            else if (type == typeof(char))
            {
                dataType = OracleDbType.Char;
            }
            return dataType;
        }

        #region 批量操作
        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="cmd"></param>
        private void WriteSqlLog(DBContextBase dBContextBase, DbCommand cmd)
        {
            if (dBContextBase.Database.Log == null) return;

            StringBuilder builder = new StringBuilder(256);
            builder.AppendLine(cmd.CommandText);

            for (int i = 0; i < cmd.Parameters.Count; i++)
            {
                var parameter = cmd.Parameters[i];

                builder.Append("-- ")
                    .Append(parameter.ParameterName);

                if (null != parameter.Value && parameter.Value.GetType() == typeof(object[]))
                {
                    object[] values = (object[])parameter.Value;

                    builder.Append(": ");
                    foreach (var item in values)
                    {
                        builder.Append("'")
                            .Append((item == null || item == DBNull.Value) ? "null" : item)
                            .Append(",");
                    }

                    if (values.Length > 0)
                        builder.Remove(builder.Length - 1, 1);

                    builder.Append("' (Type = ")
                            .Append(parameter.DbType);
                }
                else
                {
                    builder.Append(": '")
                            .Append((parameter.Value == null || parameter.Value == DBNull.Value) ? "null" : parameter.Value)
                            .Append("' (Type = ")
                            .Append(parameter.DbType);
                }

                if (parameter.Direction != ParameterDirection.Input)
                {
                    builder.Append(", Direction = ").Append(parameter.Direction);
                }

                if (!parameter.IsNullable)
                {
                    builder.Append(", IsNullable = false");
                }

                if (parameter.Size != 0)
                {
                    builder.Append(", Size = ").Append(parameter.Size);
                }

                if (((IDbDataParameter)parameter).Precision != 0)
                {
                    builder.Append(", Precision = ").Append(((IDbDataParameter)parameter).Precision);
                }

                if (((IDbDataParameter)parameter).Scale != 0)
                {
                    builder.Append(", Scale = ").Append(((IDbDataParameter)parameter).Scale);
                }

                builder.Append(")").Append(Environment.NewLine);
            }

            dBContextBase.Database.Log(builder.ToString());
        }

        /// <summary>
        /// 获取实体的物理表表名
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private string GetTableName(object entity)
        {
            var type = entity.GetType();

            TableAttribute[] table = (TableAttribute[])type.GetCustomAttributes(typeof(TableAttribute), false);

            if (table.Length > 0)
                return table[0].Name;
            else
                return string.Empty;
        }

        /// <summary>
        /// 获取数据库的事务信息
        /// </summary>
        /// <param name="objectContext"></param>
        /// <returns></returns>
        private Tuple<DbConnection, DbTransaction> GetStore(ObjectContext objectContext)
        {
            DbConnection dbConnection = objectContext.Connection;
            var entityConnection = dbConnection as EntityConnection;

            if (entityConnection == null)
                return new Tuple<DbConnection, DbTransaction>(dbConnection, null);

            DbConnection connection = entityConnection.StoreConnection;

            //动态获取当前的数据库事务
            dynamic connectionProxy = new DynamicProxy(entityConnection);
            dynamic entityTransaction = connectionProxy.CurrentTransaction;
            if (entityTransaction == null)
                return new Tuple<DbConnection, DbTransaction>(connection, null);

            DbTransaction transaction = entityTransaction.StoreTransaction;
            return new Tuple<DbConnection, DbTransaction>(connection, transaction);
        }


        #endregion
    }

    #region 数据库批量操作辅助类

    /// <summary>
    /// 列信息
    /// </summary>
    internal class ColumnInfo
    {
        public ColumnInfo()
        {
            IsChanged = false;
            IsKey = false;
        }

        public string ColumnName { get; set; }

        public string DbName { get; set; }

        public OracleDbType DbType { get; set; }

        public object CurrentValue { get; set; }

        public object OriginalValue { get; set; }

        public bool IsKey { get; set; }

        public bool IsChanged { get; set; }

        public bool ConcurrencyCheck { get; set; }

        //public string WhereSql(bool ori)
        //{
        //    return string.Format("{0}{1}{2}",DbName,()
        //}
    }

    /// <summary>
    /// 实体信息
    /// </summary>
    internal class EntityInfo
    {
        public string TableName { get; set; }

        public ObjectStateEntry StateEntity { get; set; }

        public List<ColumnInfo> ColumnList { get; set; }
    }

    #endregion
}
