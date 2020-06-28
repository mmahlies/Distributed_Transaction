using BackOffice;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Transactions;

namespace BackOfficeAPI.Controllers
{
    internal class TransactionFilter : ActionFilterAttribute
    {
        private DbContext _BackOfficeDBContext;
    

        private static TransactionScope _transactionScope;
        public TransactionFilter(DbContext BackOfficeDBContext)
        {
            _BackOfficeDBContext = BackOfficeDBContext;


        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //_transactionScope.Complete();
            if (context.Exception != null)
            {
                // rollback
                var sqlText = "rollback tran";
                this._BackOfficeDBContext.Database.ExecuteSqlCommand(sqlText);
            }
            _transactionScope.Dispose();
            //context.Exception
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            object token;
            if (context.ActionArguments.TryGetValue("token", out token))
            {
                //    _transactionScope = new TransactionScope();
                TimeSpan transactionTimeOut = TimeSpan.MaxValue;//long.Parse(Core.Utilities.Support.Configuration.GetConfig("transactionTimeOut"));
                TransactionOptions options = new TransactionOptions();
                options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                options.Timeout = transactionTimeOut;
                _transactionScope = new TransactionScope(TransactionScopeOption.Required, options, TransactionScopeAsyncFlowOption.Enabled);
                BindSessionToken(token.ToString(), (BackOfficeDBContext)this._BackOfficeDBContext);
            }
        }
        private static void BindSessionToken(string token, BackOfficeDBContext dbContextNet)
        {
          
            //   dbContextNet.Database.ExecuteSqlCommand($"EXEC sp_bindsession '{token}'");
            var command = dbContextNet.Database.ExecuteSqlCommand($"EXEC sp_bindsession '{token}'");
          
        }
    }
}