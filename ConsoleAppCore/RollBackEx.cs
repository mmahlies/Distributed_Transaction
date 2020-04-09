using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace BackOffice
{

        public static class MyExtensions
        {
            public static void RollBack(this TransactionScope transactionScope, DbContext dbContext)
            {
              dbContext.Database.ExecuteSqlCommand($"rollback tran");
            }
        }
    
}
