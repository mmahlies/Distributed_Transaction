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
            using (TransactionScope inner2Scope = new TransactionScope())
            {
                BackOfficeDBContext dbContextNet = new BackOfficeDBContext();
                dbContextNet.Teacher.Add(new Teacher() { Name = "teacher nested" });
                dbContextNet.SaveChanges();
              
                BackOfficeDBContext2.BackOfficeDBContext2 backOfficeDBContext2 = new BackOfficeDBContext2.BackOfficeDBContext2();
                var teachers = backOfficeDBContext2.Teacher.ToList();
                backOfficeDBContext2.Teacher.Add(new BackOfficeDBContext2.Teacher() { Name = "Teacher from backOfficeDBContext2" });
                backOfficeDBContext2.SaveChanges();

                inner2Scope.Complete();
            }
        }


        public static bool Logic(string token)
        {
            DependentTransaction dependentTransaction;
            BackOfficeDBContext dbContextNet = new BackOfficeDBContext();
            Transaction transaction = null;
            try
            {
                using (TransactionScope globalScope = new TransactionScope())
                {
                    dbContextNet.Database.ExecuteSqlRaw($"EXEC sp_bindsession '{token}'");
                    var x1 = Transaction.Current?.TransactionInformation?.LocalIdentifier;      
                    using (TransactionScope innerScope1 = new TransactionScope())
                    {                     
                        transaction = dbContextNet.Database.GetEnlistedTransaction();
                        var x3 = Transaction.Current?.TransactionInformation?.LocalIdentifier;

                        dbContextNet.Teacher.Add(new Teacher() { Name = "Teacher root1" });
                        dbContextNet.SaveChanges();
                    
                        using (TransactionScope inner2Scope = new TransactionScope())
                        {
                         
                            dbContextNet.Teacher.Add(new Teacher() { Name = "teacher nested" });
                            dbContextNet.SaveChanges();

                            //   dbContextNet.Database.ExecuteSqlRaw($"rollback tran");
                            //    Transaction.Current.Rollback();
                            //     dbContextNet.Database.ExecuteSqlRaw($"EXEC sp_bindsession NULL");
                            //     throw new Exception();
                            // inner2Scope.RollBack(dbContextNet);

                            BackOfficeDBContext2.BackOfficeDBContext2 backOfficeDBContext2 = new BackOfficeDBContext2.BackOfficeDBContext2();
                            var teachers = backOfficeDBContext2.Teacher.ToList();
                            backOfficeDBContext2.Teacher.Add(new BackOfficeDBContext2.Teacher() { Name = "Teacher from backOfficeDBContext2" });
                            backOfficeDBContext2.SaveChanges();

                            inner2Scope.Complete();
                        }
                        innerScope1.Complete();
                      
                    }



                    //using (TransactionScope innerScope1 = new TransactionScope())
                    //{
                    //    var x5 = Transaction.Current?.TransactionInformation?.LocalIdentifier;
                    //    BackOfficeDBContext dbContextNet1 = new BackOfficeDBContext();
                    //    dbContextNet1.Teacher.Add(new Teacher() { Name = "Teacher root bulk save changes" });
                    //    dbContextNet1.BulkSaveChanges();
                    //    innerScope1.Complete();
                    //}

                    //   globalScope.Dispose();

                    return true;
                }
            }
            catch (Exception ex)
            {
                // ex handling 
                var x = "";

                //  dbContextNet.Database.ExecuteSqlRaw($"EXEC sp_bindsession NULL");

                return false;
                //    throw;
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
