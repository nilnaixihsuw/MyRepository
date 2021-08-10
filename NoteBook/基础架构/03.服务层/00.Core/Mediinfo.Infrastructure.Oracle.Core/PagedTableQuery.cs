using Mediinfo.Enterprise.PagedResult;
using Mediinfo.Infrastructure.Core.DBContext;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Mediinfo.Infrastructure.Oracle.Core
{
    public class PagedTableQuery : IPagedTableQuery
    {
        private DBContextBase dBContextBase = null;
        public PagedTableQuery(DBContextBase dBContextBase)
        {
            this.dBContextBase = dBContextBase;
        }

        public IPagedTableResult Query(string strSql, Dictionary<string, object> parameters, int pageIndex = 1, int pageSize = 20, string orderBy = "")
        {
            string sql = "SELECT * FROM (SELECT A.*, ROWNUM R FROM (" + strSql;
            if (!string.IsNullOrEmpty(orderBy))
            {
                sql += " order by " + orderBy;
            }
            sql += " )A where ROWNUM <=" + pageIndex * pageSize + ") WHERE R> " + (pageIndex - 1) * pageSize;

            List<DbParameter> paraList = new List<DbParameter>();

            var cmd = dBContextBase.Database.Connection.CreateCommand();

            parameters.ToList().ForEach(o =>
            {
                var parm = cmd.CreateParameter();
                parm.ParameterName = o.Key;
                parm.Value = o.Value;
                paraList.Add(parm);
            });

            int totalRecords = dBContextBase.Database.SqlQuery<int>("Select Count(1) From (" + strSql + ")", paraList.ToArray()).FirstOrDefault();

            var entityResult = GetDataTable(sql, parameters);

            // 计算余页
            int pages = 0;
            if (totalRecords % pageSize != 0)
                pages = 1;
            else
                pages = 0;
            // 获取页数（pages用于考虑有余数的情况）
            long totalPages = totalRecords / pageSize + pages;

            return new PagedTableResult(totalRecords, totalPages, pageSize, pageIndex, entityResult);
        }

        private DataTable GetDataTable(string sql, Dictionary<string, object> parameters)
        {
            if (this.dBContextBase.Database.Connection.State != ConnectionState.Open)
                this.dBContextBase.Database.Connection.Open();
            var cmd = this.dBContextBase.Database.Connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.Clear();

            List<DbParameter> paraList = new List<DbParameter>();

            parameters.ToList().ForEach(o =>
            {
                var parm = cmd.CreateParameter();
                parm.ParameterName = o.Key;
                parm.Value = o.Value;
                paraList.Add(parm);
            });

            DataTable table = new DataTable();
            var read = cmd.ExecuteReader();
            for (int i = 0; i < read.FieldCount; i++)
            {
                // 创建table的第一列
                DataColumn Column = new DataColumn();
                // 该列的数据类型
                Column.DataType = read.GetFieldType(i);
                // 该列得名称
                Column.ColumnName = read.GetName(i);
                table.Columns.Add(Column);
            }

            while (read.Read())
            {
                var row = table.NewRow();
                for (int i = 0; i < read.FieldCount; i++)
                {
                    if (read[i] != DBNull.Value)
                    {
                        row[i] = read[i];
                    }
                }
                table.Rows.Add(row);
            }

            return table;
        }
    }
}
