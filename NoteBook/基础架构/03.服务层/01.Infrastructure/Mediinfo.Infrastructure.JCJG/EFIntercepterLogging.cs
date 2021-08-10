using Mediinfo.Enterprise.Log;
using Mediinfo.Utility.Extensions;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;

namespace Mediinfo.Infrastructure.JCJG
{

    /// <summary>
    /// 监听EF执行的SQL语句
    /// add by songxl on 2019-10-8
    /// </summary>
    public class EFIntercepterLogging : DbCommandInterceptor
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        public override void ScalarExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            base.ScalarExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }
        public override void ScalarExecuted(System.Data.Common.DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            _stopwatch.Stop();
            if (interceptionContext.Exception != null)
            {
                Trace.TraceError("Exception:{1} rn --> Error executing command: {0}", command.CommandText, interceptionContext.Exception.ToString());
            }
            else
            {
                Trace.TraceInformation("rn执行时间:{0} 毫秒rn-->ScalarExecuted.Command:{1}rn", _stopwatch.ElapsedMilliseconds, command.CommandText);
            }
            base.ScalarExecuted(command, interceptionContext);
        }
        public override void NonQueryExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            base.NonQueryExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }
        public override void NonQueryExecuted(System.Data.Common.DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            _stopwatch.Stop();

             if (!command.CommandText.Contains("HIS6"))
            {
                SysLogEntity entity = new SysLogEntity();
                entity.RiZhiID = Guid.NewGuid().ToString();
                entity.ChuangJianSj = DateTime.Now.ToInvariantString("yyyy/MM/dd HH:mm:ss");
                entity.RiZhiBt = "HIS插件程序产生的sql记录";

                var param = Environment.NewLine;
                int i = 0;
                foreach (var p in command.Parameters)
                {
                    param += "p" + i + ":" + ((DbParameter)p).Value + Environment.NewLine;
                    i++;
                }
                entity.RiZhiNr = "HIS插件程序产生的sql记录" + Environment.NewLine + command.CommandText + Environment.NewLine + "参数：" + param;
                entity.QingQiuLy = "HIS插件";
                entity.RiZhiLx = 5;

                if (interceptionContext.Exception != null)
                {
                    entity.RiZhiNr = entity.RiZhiNr + Environment.NewLine + interceptionContext.Exception;
                }

                //ESLog eSLog = new ESLog();
                //eSLog.PutLog(entity);
                LogHelper.Intance.PutSysInfoLog(entity);
            }


            base.NonQueryExecuted(command, interceptionContext);
        }
        public override void ReaderExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext)
        {
            base.ReaderExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }
        public override void ReaderExecuted(System.Data.Common.DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext)
        {
            _stopwatch.Stop();
            if (interceptionContext.Exception != null)
            {
                Trace.TraceError("Exception:{1} rn --> Error executing command:rn {0}", command.CommandText, interceptionContext.Exception.ToString());
            }
            else
            {
                Trace.TraceInformation("rn执行时间:{0} 毫秒 rn -->ReaderExecuted.Command:rn{1}", _stopwatch.ElapsedMilliseconds, command.CommandText);
            }
            base.ReaderExecuted(command, interceptionContext);
        }
    }

}

