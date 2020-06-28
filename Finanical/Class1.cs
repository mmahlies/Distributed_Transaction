using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Financial
{
    class Class1
    {
        public void c() {
            TimeSpan transactionTimeOut = TimeSpan.MaxValue;//long.Parse(Core.Utilities.Support.Configuration.GetConfig("transactionTimeOut"));
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
            options.Timeout = transactionTimeOut;

            TransactionScope _transactionScope = new TransactionScope(TransactionScopeOption.Required, options, TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
