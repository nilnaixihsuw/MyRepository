using Mediinfo.Enterprise;
using Mediinfo.Enterprise.Exceptions;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediinfo.Infrastructure.JCJG
{
    /// <summary>
    /// HIS ef上下文工厂
    /// </summary>
    public class HISDBContextFactory
    {
        /// <summary>
        /// 创建EF上下文
        /// </summary>
        /// <returns></returns>
        public static HISDBContext Create()
        {
            HISConnDBContext connDB = HISConnDBContext.GetInstance();
            if (string.IsNullOrWhiteSpace(connDB.HISConnectionString))
            {
                throw new DBException("主数据库为空", Enterprise.ReturnCode.MAINDBCONNECTERROR);
            }

            OracleConnectionStringBuilder oraConn = new OracleConnectionStringBuilder(connDB.HISConnectionString);

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = oraConn.ConnectionString;

            return new HISDBContext(conn, true, oraConn.UserID.ToUpper());
        }
    }
}

