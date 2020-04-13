using BackOffice;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

using System.Transactions;

namespace BackOfficeAPI.Controllers
{
    internal class TransactionFilter : ActionFilterAttribute
    {
        private DbContext _BackOfficeDBContext;
        Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext

        private static TransactionScope _transactionScope;
        public TransactionFilter(DbContext BackOfficeDBContext)
        {
            _BackOfficeDBContext = BackOfficeDBContext;


        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //_transactionScope.Complete();
            _transactionScope.Dispose();
            //context.Exception
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            object token;
            if (context.ActionArguments.TryGetValue("token", out token))
            {
                _transactionScope = new TransactionScope();
                BindSessionToken(token.ToString(), (BackOfficeDBContext)this._BackOfficeDBContext);
            }
        }
        private static void BindSessionToken(string token, BackOfficeDBContext dbContextNet)
        {
            Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext
            //   dbContextNet.Database.ExecuteSqlCommand($"EXEC sp_bindsession '{token}'");
            var command = dbContextNet.Database.ExecuteSqlCommand($"EXEC sp_bindsession '{token}'");
          
        }
    }
}