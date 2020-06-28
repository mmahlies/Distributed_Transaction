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
            // dispose the tranacion scope
            if (_transactionScope != null)
            {
                _transactionScope.Dispose();
            }


            if (context.Exception != null)
            {
                // rollback                
                _FinancialDBContext.Database.ExecuteSqlCommand($"rollback tran");
            }





        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            object token;
            if (context.ActionArguments.TryGetValue("token", out token))
            {

                _transactionScope = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(5), TransactionScopeAsyncFlowOption.Enabled);
                //   _transactionScope = new TransactionScope();                

                BindSessionToken(token.ToString(), _FinancialDBContext);
            }
        }
        private static void BindSessionToken(string token, DbContext dbContextNet)
        {
            var sqlText = $"EXEC sp_bindsession '{token}'";
            dbContextNet.Database.ExecuteSqlCommand(sqlText);
        }


        private static void ExecuteNonQuery(DbContext dbContextNet, string sql)
        {
            //   dbContextNet.Database.ExecuteSqlCommand($"EXEC sp_bindsession '{token}'");
            var command = dbContextNet.Database.GetDbConnection().CreateCommand();
            command.CommandText = sql;
            dbContextNet.Database.OpenConnection();
            var result = command.ExecuteNonQuery();
            dbContextNet.Database.CloseConnection();
        }

    }
}