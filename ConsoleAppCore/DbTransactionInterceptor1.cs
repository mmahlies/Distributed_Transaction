using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace BackOffice
{
    class DbTransactionInterceptor1 : IDbTransactionInterceptor
    {
       

        public InterceptionResult<DbTransaction> TransactionStarting(DbConnection connection, TransactionStartingEventData eventData, InterceptionResult<DbTransaction> result)
        {
            var x = "";
            var xx = Transaction.Current;
            return result;
        }

        public DbTransaction TransactionStarted(DbConnection connection, TransactionEndEventData eventData, DbTransaction result)
        {
            var x = "";
            var xx = Transaction.Current;
            return result;
        }

        public Task<InterceptionResult<DbTransaction>> TransactionStartingAsync(DbConnection connection, TransactionStartingEventData eventData, InterceptionResult<DbTransaction> result, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<DbTransaction> TransactionStartedAsync(DbConnection connection, TransactionEndEventData eventData, DbTransaction result, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public DbTransaction TransactionUsed(DbConnection connection, TransactionEventData eventData, DbTransaction result)
        {
            throw new NotImplementedException();
        }

        public Task<DbTransaction> TransactionUsedAsync(DbConnection connection, TransactionEventData eventData, DbTransaction result, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void TransactionCommitted(DbTransaction transaction, TransactionEndEventData eventData)
        {
            var x = "";
           
        }

        public Task<InterceptionResult> TransactionCommittingAsync(DbTransaction transaction, TransactionEventData eventData, InterceptionResult result, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task TransactionCommittedAsync(DbTransaction transaction, TransactionEndEventData eventData, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public InterceptionResult TransactionRollingBack(DbTransaction transaction, TransactionEventData eventData, InterceptionResult result)
        {
            var x = "";
            return result;
        }

        public void TransactionRolledBack(DbTransaction transaction, TransactionEndEventData eventData)
        {
            throw new NotImplementedException();
        }

        public Task<InterceptionResult> TransactionRollingBackAsync(DbTransaction transaction, TransactionEventData eventData, InterceptionResult result, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task TransactionRolledBackAsync(DbTransaction transaction, TransactionEndEventData eventData, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void TransactionFailed(DbTransaction transaction, TransactionErrorEventData eventData)
        {
            throw new NotImplementedException();
        }

        public Task TransactionFailedAsync(DbTransaction transaction, TransactionErrorEventData eventData, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public InterceptionResult TransactionCommitting(DbTransaction transaction, TransactionEventData eventData, InterceptionResult result)
        {
            var x = "";
            return result;
        }

         
    }
}
