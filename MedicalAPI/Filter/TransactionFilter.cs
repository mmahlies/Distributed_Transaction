

using MedicalEF6;
using System;
using System.Data.Entity;
using System.Transactions;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MedicalAPI.Filter
{
    internal class TransactionFilter :  ActionFilterAttribute
    {
        private DbContext _medicalDBContext;

        private static TransactionScope _transactionScope;

        public TransactionFilter()
        {
           _medicalDBContext = new MedicalContext();
        }
        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            _transactionScope.Dispose();
        }

        public override void OnActionExecuting(HttpActionContext context)
        {
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

    }
}