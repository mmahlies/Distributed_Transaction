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
using System.Configuration;
using Microsoft.IdentityModel.Protocols;

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
            //    dbContextNet.BulkSaveChanges();



            //    inner2Scope.Complete();
            //}
        }


        public bool Logic(DbContext generalDbContextNet)
        {
            BackOfficeDBContext dbContextNet = (BackOfficeDBContext)generalDbContextNet;


            //BackOfficeDBContext dbContextNet = (BackOfficeDBContext)generalDbContextNet;       
            try
            {



                using (TransactionScope innerScope1 = new TransactionScope())
                {

                    dbContextNet.Teacher.Add(new Teacher() { Name = "Teacher root1" });
                    dbContextNet.BulkSaveChanges();

                    using (TransactionScope inner2Scope = new TransactionScope())
                    {
                        dbContextNet.Teacher.Add(new Teacher() { Name = "teacher nested" });
                        //  dbContextNet.SaveChanges();
                        //  var teas3 = dbContextNet.Teacher.ToList();


                        //inner2Scope.RollBack(dbContextNet);
                        dbContextNet.BulkSaveChanges();
                        inner2Scope.Complete();
                    }
                    //   innerScope1.Complete();
                    dbContextNet.RollBack();
                }



                //using (TransactionScope innerScope1 = new TransactionScope())
                //{
                //    var x5 = Transaction.Current?.TransactionInformation?.LocalIdentifier;
                //    BackOfficeDBContext dbContextNet1 = new BackOfficeDBContext();
                //    dbContextNet1.Teacher.Add(new Teacher() { Name = "Teacher root bulk save changes" });
                //    dbContextNet1.BulkSaveChanges();
                //    innerScope1.Complete();
                //}


                return true;

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
