using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Mediinfo.Infrastructure.Oracle.Core
{
    /// <summary>
    /// 对DbContextDatabase的批量操作的扩展
    /// </summary>
    public static class DbContextDatabaseExtension
    {
        /// <summary>
        /// 批量执行Cmd
        /// </summary>
        /// <param name="database">DataBase扩展对象</param>
        /// <param name="sql">需要执行的批量sql</param>
        /// <param name="oracleParameterLists">批量sql的参数列表</param>
        /// <returns></returns>
        public static int BulkExecuteOracleCommand(this Database database, string sql, List<List<OracleParameter>> oracleParameterLists)
        {
            int affectRows = 0;
            using (var cmd = (OracleCommand)database.Connection.CreateCommand())
            {
                // 取出每个sql的参数list
                var updCmdParmList = oracleParameterLists;

                OracleParameter param = null;
                object[] values = null;
                int index = 0;

                // 遍历参数list的所有列
                foreach (var col in updCmdParmList[0].Select(m => m.ParameterName))
                {
                    values = new object[oracleParameterLists.Count()];
                    index = 0;

                    // 遍历所有sql
                    foreach (var upd in oracleParameterLists)
                    {
                        values[index++] = upd.Where(m => m.ParameterName == col).FirstOrDefault().Value;
                    }

                    param = new OracleParameter(col, oracleParameterLists[0].Where(m => m.ParameterName == col).FirstOrDefault().OracleDbType);
                    param.Value = values;
                    cmd.Parameters.Add(param);
                }

                // 批量执行的数量
                cmd.ArrayBindCount = oracleParameterLists.Count();
                cmd.CommandType = CommandType.Text;
                cmd.BindByName = true;
                cmd.CommandTimeout = database.CommandTimeout.HasValue ? database.CommandTimeout.Value : 0;
                cmd.CommandText = sql;

                // 开始执行
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
            return affectRows;
        }
    }
}
