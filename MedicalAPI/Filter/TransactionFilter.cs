

using Autofac.Integration.WebApi;
using MedicalEF6;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MedicalAPI.Filter
{
    public class TransactionFilter :  IAutofacActionFilter
    {
        private DbContext _medicalDBContext;

        private static TransactionScope _transactionScope;

        public TransactionFilter()
        {

        }

        public TransactionFilter(DbContext dbContext)
        {
            _medicalDBContext = dbContext;
        }
        public  void OnActionExecuted(HttpActionExecutedContext context)
        {
            try
            {
                _transactionScope.Dispose();
            }
            catch (Exception)
            {
                // cant dispose opened transactionScope
            }

        }

        public  void OnActionExecuting(HttpActionContext context)
        {
            IEnumerable<string> values;
            context.Request.Headers.TryGetValues("SqlTransactionToken", out values);
            var xx = values.FirstOrDefault();
            object token;
            if (context.ActionArguments.TryGetValue("token", out token))
            {
                _transactionScope = new TransactionScope();
                //   _transactionScope = new TransactionScope();
                BindSessionToken(token.ToString(), (MedicalContext)this._medicalDBContext);
            }
        }
        private static void BindSessionToken(string token, MedicalContext dbContextNet)
        {
            //   dbContextNet.Database.ExecuteSqlCommand($"EXEC sp_bindsession '{token}'");
            var command = dbContextNet.Database.Connection.CreateCommand();
            command.CommandText = $"EXEC sp_bindsession '{token}'";
            dbContextNet.Database.Connection.Open();
            var result = command.ExecuteNonQuery();
            dbContextNet.Database.Connection.Close();
        }

        public Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}