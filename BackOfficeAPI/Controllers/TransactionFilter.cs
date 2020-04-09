using BackOffice;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace BackOfficeAPI.Controllers
{
    internal class TransactionFilter : IActionFilter
    {
        private DbContext _BackOfficeDBContext;
        private Itest _test;
        private static TransactionScope _transactionScope;
        public TransactionFilter(DbContext BackOfficeDBContext, Itest test)
        {
           _BackOfficeDBContext = BackOfficeDBContext;
            
            
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _transactionScope.Dispose();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            object token;
            if(context.ActionArguments.TryGetValue("token", out token))
            {
                _transactionScope  = new TransactionScope();
                
                    BindSessionToken(token.ToString(), (BackOfficeDBContext)this._BackOfficeDBContext);
                
            }



        }
        private static void BindSessionToken(string token, BackOfficeDBContext dbContextNet)
        {
            //   dbContextNet.Database.ExecuteSqlCommand($"EXEC sp_bindsession '{token}'");
            var command = dbContextNet.Database.GetDbConnection().CreateCommand();
            command.CommandText = $"EXEC sp_bindsession '{token}'";
            dbContextNet.Database.OpenConnection();
            var result = command.ExecuteNonQuery();
        }
    }
}