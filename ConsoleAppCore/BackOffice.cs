using BackOffice;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Transactions;
using Z.EntityFramework.Extensions;

namespace BackOffice
{
    public class BackOffice
    {
        public static void Main(string[] args)
        {

        }


        public static bool Logic(string token)
        {
            try
            {
                DependentTransaction dependentTransaction;
                using (TransactionScope globalScope = new TransactionScope())
                {
                    BackOfficeDBContext dbContextNet = new BackOfficeDBContext();
                    dbContextNet.Database.ExecuteSqlRaw($"EXEC sp_bindsession '{token}'");
                    dependentTransaction = Transaction.Current.DependentClone(DependentCloneOption.BlockCommitUntilComplete);

                    using (TransactionScope innerScope1 = new TransactionScope(dependentTransaction))
                    {
                        // DbContextNet1 dbContextNet2 = new DbContextNet1();
                        BackOfficeDBContext dbContextNet1 = new BackOfficeDBContext();
                        //   dbContextNet1.Database.ExecuteSqlRaw($"EXEC sp_bindsession '{token}'");                    
                        dbContextNet1.Teacher.Add(new Teacher() { Name = "Teacher root1" });
                        dbContextNet1.SaveChanges();
                        innerScope1.Complete();
                        using (TransactionScope inner2Scope = new TransactionScope(dependentTransaction))
                        {
                            BackOfficeDBContext dbContextNet2 = new BackOfficeDBContext();
                            dbContextNet2.Teacher.Add(new Teacher() { Name = "teacher nested" });
                            dbContextNet2.SaveChanges();
                            inner2Scope.Complete();
                        }
                    }

                    using (TransactionScope innerScope1 = new TransactionScope(dependentTransaction))
                    {
                        BackOfficeDBContext dbContextNet1 = new BackOfficeDBContext();
                        dbContextNet1.Teacher.Add(new Teacher() { Name = "Teacher root bulk save changes" });
                        dbContextNet1.BulkSaveChanges();
                        innerScope1.Complete();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                // ex handling 
                var x = "";
                return false;
                //  throw;
            }

        }

      

        private static void TransactionManager_DistributedTransactionStarted(object sender, TransactionEventArgs e)
        {
            var x = "";
        }
        private static bool CallingDtcAPI(byte[] token, string sql)
        {
            HttpClient client = new HttpClient();

            DtcValues dtcValues = new DtcValues() { Sql = sql, Token = token };


            HttpResponseMessage response = client.PostAsJsonAsync<DtcValues>("http://localhost:56578/api/values", dtcValues).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            return bool.Parse(result);
        }

    }

    class DtcValues
    {
        public byte[] Token { get; set; }
        public string Sql { get; set; }
    }
}
