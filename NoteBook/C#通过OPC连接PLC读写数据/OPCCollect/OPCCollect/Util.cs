using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPCCollect
{
    public class Util
    {
        public static void InitDatabase()
        {
            string dbpath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"DB\db.db";
            if (!File.Exists(dbpath))
            {
                SQLiteConnection.CreateFile(dbpath);
                string sql = @"CREATE TABLE caipian( [ID] INTEGER PRIMARY KEY ASC AUTOINCREMENT, [timeRecord] DATETIME DEFAULT(CURRENT_TIMESTAMP),[type] VARCHAR(200), [result] VARCHAR(4096));
                               CREATE TABLE diepian( [ID] INTEGER PRIMARY KEY ASC AUTOINCREMENT, [timeRecord] DATETIME DEFAULT(CURRENT_TIMESTAMP),[type] VARCHAR(200), [result] VARCHAR(4096));
                               CREATE TABLE hejiang( [ID] INTEGER PRIMARY KEY ASC AUTOINCREMENT, [timeRecord] DATETIME DEFAULT(CURRENT_TIMESTAMP),[type] VARCHAR(200), [result] VARCHAR(4096));
                               CREATE TABLE tubu( [ID] INTEGER PRIMARY KEY ASC AUTOINCREMENT, [timeRecord] DATETIME DEFAULT(CURRENT_TIMESTAMP),[type] VARCHAR(200), [result] VARCHAR(4096));";
                new SQLite(dbpath).ExecuteNonQuery(sql);
            }
        }
    }
}
