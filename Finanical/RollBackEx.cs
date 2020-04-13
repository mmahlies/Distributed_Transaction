using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Financial
{

        public static class MyExtensions
        {
            public static void RollBack(this DbContext  dbContext)
            {
              dbContext.Database.ExecuteSqlCommand($"rollback tran");
            }
        }
    
}
