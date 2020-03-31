using BackOffice;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;

namespace BackOffice
{
    public class Interceptor : IDbCommandInterceptor
    {

        public DbCommand CommandCreated(CommandEndEventData eventData, DbCommand result)
        {
            var xx = Transaction.Current;
            var x = "";
            return result;
        }

        public InterceptionResult<DbCommand> CommandCreating(CommandCorrelatedEventData eventData, InterceptionResult<DbCommand> result)
        {
            var x = "";
            return result;
        }

        public void CommandFailed(DbCommand command, CommandErrorEventData eventData)
        {
            var x = "";
        }

        public Task CommandFailedAsync(DbCommand command, CommandErrorEventData eventData, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public InterceptionResult DataReaderDisposing(DbCommand command, DataReaderDisposingEventData eventData, InterceptionResult result)
        {
            var x = "";
            return result;
        }

        public int NonQueryExecuted(DbCommand command, CommandExecutedEventData eventData, int result)
        {
            var x = "";
            return result;
        }

        public Task<int> NonQueryExecutedAsync(DbCommand command, CommandExecutedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            var x = "";
            return null;
        }

        public InterceptionResult<int> NonQueryExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<int> result)
        {
            var x = "";
            return result;
        }

        public Task<InterceptionResult<int>> NonQueryExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            SqlCommand sqlCommand = AutoMap(eventData.Command);
           SqlAggregate.SqlText.Append( CommandAsSql.ExtensionMethods.CommandAsSql(sqlCommand));                      
            return result;
        }



        /// <summary>
        /// can use adapter design pattern
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private SqlCommand AutoMap(DbCommand command)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = command.CommandText;
            foreach (DbParameter item in command.Parameters)
            {

                sqlCommand.Parameters.AddWithValue(item.ParameterName, item.Value);
            }
            sqlCommand.Connection = new SqlConnection(command.Connection.ConnectionString);
            return sqlCommand;
        }

        public Task<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData eventData, DbDataReader result, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            var x = "";
            return result;
        }

        public Task<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public object ScalarExecuted(DbCommand command, CommandExecutedEventData eventData, object result)
        {
            throw new NotImplementedException();
        }

        public Task<object> ScalarExecutedAsync(DbCommand command, CommandExecutedEventData eventData, object result, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public InterceptionResult<object> ScalarExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<object> result)
        {
            throw new NotImplementedException();
        }

        public Task<InterceptionResult<object>> ScalarExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<object> result, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}

