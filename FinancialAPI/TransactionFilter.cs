using Financial;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Transactions;

namespace FinancialAPI.Controllers
{
    internal class TransactionFilter : IActionFilter
    {
        private DbContext _FinancialDBContext;

        private static TransactionScope _transactionScope;
        public TransactionFilter(DbContext financialDBContext)
        {
            _FinancialDBContext = financialDBContext;


        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
           
            _transactionScope.Dispose();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            object token;
            if (context.ActionArguments.TryGetValue("token", out token))
            {
                  _transactionScope = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromTicks(1), TransactionScopeAsyncFlowOption.Enabled);
                //   _transactionScope = new TransactionScope();
                
                BindSessionToken(token.ToString(), (FinancialDBContext)this._FinancialDBContext);
            }
        }
        private static void BindSessionToken(string token, FinancialDBContext dbContextNet)
        {
            //   dbContextNet.Database.ExecuteSqlCommand($"EXEC sp_bindsession '{token}'");
            var command = dbContextNet.Database.GetDbConnection().CreateCommand();
            command.CommandText = $"EXEC sp_bindsession '{token}'";
            dbContextNet.Database.OpenConnection();
            var result = command.ExecuteNonQuery();
        }
    }
}