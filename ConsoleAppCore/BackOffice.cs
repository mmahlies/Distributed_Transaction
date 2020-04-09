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


namespace BackOffice
{
    public class BackOffice
    {
        public static void Main(string[] args)
        {
            //using (TransactionScope inner2Scope = new TransactionScope())
            //{
            //    BackOfficeDBContext dbContextNet = new BackOfficeDBContext();
            //    dbContextNet.Teacher.Add(new Teacher() { Name = "teacher nested" });
            //    dbContextNet.SaveChanges();

            //    BackOfficeDBContext2.BackOfficeDBContext2 backOfficeDBContext2 = new BackOfficeDBContext2.BackOfficeDBContext2();
            //    var teachers = backOfficeDBContext2.Teacher.ToList();
            //    backOfficeDBContext2.Teacher.Add(new BackOfficeDBContext2.Teacher() { Name = "Teacher from backOfficeDBContext2" });
            //    backOfficeDBContext2.SaveChanges();

            //    inner2Scope.Complete();
            //}
        }


        public  bool Logic(DbContext generalDbContextNet, string token)
        {
            DependentTransaction dependentTransaction;
            BackOfficeDBContext dbContextNet = (BackOfficeDBContext)generalDbContextNet;
              Transaction transaction = null;
            try
            {
              // BackOfficeDBContext dbContextNet = new BackOfficeDBContext();         
                using (TransactionScope globalScope = new TransactionScope())
                {
                BindSessionToken(token, dbContextNet);
                    using (TransactionScope innerScope1 = new TransactionScope())
                    {

                        dbContextNet.Teacher.Add(new Teacher() { Name = "Teacher root1" });
                        dbContextNet.SaveChanges();

                        using (TransactionScope inner2Scope = new TransactionScope())
                        {
                            dbContextNet.Teacher.Add(new Teacher() { Name = "teacher nested" });
                            dbContextNet.SaveChanges();
                            var teas3 = dbContextNet.Teacher.ToList();


                            //inner2Scope.RollBack(dbContextNet);
                            dbContextNet.SaveChanges();
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

        private static void BindSessionToken(string token, BackOfficeDBContext dbContextNet)
        {
            //   dbContextNet.Database.ExecuteSqlCommand($"EXEC sp_bindsession '{token}'");
            var command = dbContextNet.Database.GetDbConnection().CreateCommand();
            command.CommandText = $"EXEC sp_bindsession '{token}'";
            dbContextNet.Database.OpenConnection();
            var result = command.ExecuteNonQuery();
        }

        private static void TransactionManager_DistributedTransactionStarted(object sender, TransactionEventArgs e)
        {
            var x = "";
        }


    }

    class DtcValues
    {
        public byte[] Token { get; set; }
        public string Sql { get; set; }
    }
}
