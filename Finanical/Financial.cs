
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
            Thread.Sleep(5);
            FinancialDBContext dbContextNet = (FinancialDBContext)generalDbContextNet;
            try
            {
                using (TransactionScope innerScope1 = new TransactionScope())
                {
                    dbContextNet.tb_Financial.Add(new tb_Financial() { Name = "Finanical Row1" });
                    dbContextNet.BulkSaveChanges();

                    using (TransactionScope inner2Scope = new TransactionScope())
                    {
                        dbContextNet.tb_Financial.Add(new tb_Financial() { Name = "Finanical Row2" });
                        //  dbContextNet.SaveChanges();                      
                        //inner2Scope.RollBack(dbContextNet);
                        dbContextNet.BulkSaveChanges();
                        inner2Scope.Complete();
                    }
                    innerScope1.Complete();
                   //  dbContextNet.RollBack();
                }
                return true;
            }
            catch (Exception ex)
            {
                // ex handling 
                throw ex;
                var x = "";

                //  dbContextNet.Database.ExecuteSqlRaw($"EXEC sp_bindsession NULL");

                return false;
                //    throw;
            }

        }
    }
}
