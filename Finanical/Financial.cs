
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Financial
{
    public class Financial
    {
        static void Main(string[] args)
        {
     
        }


        public bool Logic(DbContext generalDbContextNet)
        {
            // simulate havy oparions
           // Thread.Sleep(5);
          
            FinancialDBContext dbContextNet = (FinancialDBContext)generalDbContextNet;
            try
            {
                using (TransactionScope innerScope1 = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(5), TransactionScopeAsyncFlowOption.Enabled))
                {
                  //  var state = Transaction.Current.TransactionInformation.Status;
                    dbContextNet.tb_Financial.Add(new tb_Financial() { Name = "Finanical Row1" });
                    dbContextNet.SaveChanges();

                    using (TransactionScope inner2Scope = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(5), TransactionScopeAsyncFlowOption.Enabled))
                    {
                        dbContextNet.tb_Financial.Add(new tb_Financial() { Name = "Finanical Row2" });
                        //  dbContextNet.SaveChanges();                      
                        //inner2Scope.RollBack(dbContextNet);
                        dbContextNet.BulkSaveChanges();
                        inner2Scope.Complete();
                        throw new Exception();
                    }
                    innerScope1.Complete();
                   //  dbContextNet.RollBack();
                }
                return true;
            }
            catch (Exception ex)
            {
                // ex handling 
                var sqlText = "rollback tran";

                var state = Transaction.Current.TransactionInformation.Status;


                //  dbContextNet.Database.ExecuteSqlCommand(sqlText);
                throw ex;
                var x = "";

                //  dbContextNet.Database.ExecuteSqlRaw($"EXEC sp_bindsession NULL");

                return false;
                //    throw;
            }

        }
    }
}
