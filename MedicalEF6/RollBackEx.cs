
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Transactions;

namespace MedicalEF6
{

        public static class MyExtensions
        {
            public static void RollBack(this DbContext  dbContext)
            {
              dbContext.Database.ExecuteSqlCommand($"rollback tran");
            }
        }
    
}
