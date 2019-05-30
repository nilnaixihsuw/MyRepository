using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace ClassLibrary
{
    /// <summary>
    /// SQlite操作类
    /// </summary>
    public class SQLite
    {
        private string _connectionString;
        private SQLiteConnection _sqliteConnection;
        private SQLiteCommand _sqliteCommand;
        private SQLiteTransaction _sqliteTransaction;
        public SQLite(string dbPath)
        {
            _connectionString = "Data Source=" + dbPath;
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sqlString">sql语句</param>
        /// <param name="commandTimeout">执行超时设置，默认30s</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sqlString, int commandTimeout = 30)
        {
            using (_sqliteConnection = new SQLiteConnection(_connectionString))
            {
                using (_sqliteCommand = new SQLiteCommand(sqlString, _sqliteConnection))
                {
                    try
                    {
                        _sqliteConnection.Open();
                        _sqliteCommand.CommandTimeout = commandTimeout;
                        int rows = _sqliteCommand.ExecuteNonQuery();
                        return rows;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        /// <summary>  
        /// 执行多条SQL语句，用事物批量操作  
        /// </summary>  
        /// <param name="sqlStringList">多条SQL语句</param>          
        public void ExecuteNonQueryBatch(List<string> sqlStringList)
        {
            using (_sqliteConnection = new SQLiteConnection(_connectionString))
            {
                using (_sqliteCommand = new SQLiteCommand())
                {
                    _sqliteConnection.Open();
                    _sqliteCommand.Connection = _sqliteConnection;
                    _sqliteTransaction = _sqliteConnection.BeginTransaction();
                    try
                    {
                        sqlStringList.ForEach(sqlString =>
                        {
                            _sqliteCommand.CommandText = sqlString;
                            _sqliteCommand.ExecuteNonQuery();
                        });
                        _sqliteTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        _sqliteTransaction.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        /// <summary>  
        /// 返回查询的第一行第一列 
        /// </summary>  
        /// <param name="sqlString">查询语句</param>  
        /// <returns>查询结果</returns>  
        public object ExecuteScalar(string sqlString)
        {
            using (_sqliteConnection = new SQLiteConnection(_connectionString))
            {
                using (_sqliteCommand = new SQLiteCommand(sqlString, _sqliteConnection))
                {
                    try
                    {
                        _sqliteConnection.Open();
                        object result = _sqliteCommand.ExecuteScalar();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        /// <summary>  
        /// 返回SQLiteDataReader,使用完毕手动关闭SQLiteDataReader
        ///</summary>  
        /// <param name = "sqlString" > 查询语句 </param >
        /// <returns > SQLiteDataReader </returns >
        public SQLiteDataReader ExecuteReader(string sqlString)
        {
            try
            {
                _sqliteConnection = new SQLiteConnection(_connectionString);
                _sqliteCommand = new SQLiteCommand(sqlString, _sqliteConnection);
                _sqliteConnection.Open();
                SQLiteDataReader dataReader = _sqliteCommand.ExecuteReader();
                return dataReader;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 返回DataSet的第一张表,数据类型DataTable
        /// </summary>
        /// <param name="sqlString">查询语句</param>
        /// <returns>DataTable</returns>
        public DataTable ExecuteDataTable(string sqlString)
        {
            using (_sqliteConnection = new SQLiteConnection(_connectionString))
            {
                try
                {
                    _sqliteConnection.Open();
                    DataSet dataSet = new DataSet();
                    SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sqlString, _sqliteConnection);
                    dataAdapter.Fill(dataSet);
                    return dataSet.Tables[0];
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
