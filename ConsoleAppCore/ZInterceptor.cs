using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Z.EntityFramework.Extensions;
using Z.EntityFramework.Extensions.EFCore;

namespace BackOffice
{
    public class ZInterceptor : Z.EntityFramework.Extensions.DbCommandInterceptor
    {
        
        public override void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            base.NonQueryExecuted(command, interceptionContext);
            LogInfo("EFCommandInterceptor.NonQueryExecuted", interceptionContext.Result.ToString(), command.CommandText);
        }

        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            base.NonQueryExecuting(command, interceptionContext);
            LogInfo("EFCommandInterceptor.NonQueryExecuting", interceptionContext.EventData.ToString(), command.CommandText);
        }

        public override void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            base.ReaderExecuted(command, interceptionContext);
            LogInfo("EFCommandInterceptor.ReaderExecuted", interceptionContext.Result.ToString(), command.CommandText);
        }

        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            base.ReaderExecuting(command, interceptionContext);
            LogInfo("EFCommandInterceptor.ReaderExecuting", interceptionContext.EventData.ToString(), command.CommandText);
        }

        public override void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            base.ScalarExecuted(command, interceptionContext);
            LogInfo("EFCommandInterceptor.ScalarExecuted", interceptionContext.Result.ToString(), command.CommandText);
        }

        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            base.ScalarExecuting(command, interceptionContext);
            LogInfo("EFCommandInterceptor.ScalarExecuting", interceptionContext.EventData.ToString(), command.CommandText);
        }

        public override void NonQueryError(DbCommand command, DbCommandInterceptionContext<int> interceptionContext, Exception exception)
        {
            base.NonQueryError(command, interceptionContext, exception);
            LogInfo("EFCommandInterceptor.NonQueryError", interceptionContext.EventData.ToString(), command.CommandText, exception.Message);
        }

        public override void ReaderError(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext, Exception exception)
        {
            base.ReaderError(command, interceptionContext, exception);
            LogInfo("EFCommandInterceptor.NonQueryError", interceptionContext.EventData.ToString(), command.CommandText, exception.Message);
        }

        public override void ScalarError(DbCommand command, DbCommandInterceptionContext<object> interceptionContext, Exception exception)
        {
            base.ScalarError(command, interceptionContext, exception);
            LogInfo("EFCommandInterceptor.NonQueryError", interceptionContext.EventData.ToString(), command.CommandText, exception.Message);
        }

        private void LogInfo(string method, string command, string commandText)
        {
            Console.WriteLine("Intercepted on: {0} \n {1} \n {2}", method, command, commandText);
        }

        private void LogInfo(string method, string command, string commandText, string exception)
        {
            Console.WriteLine("Intercepted on: {0} \n {1} \n {2} \n {3}", method, command, commandText, exception);
        }
    }
}
