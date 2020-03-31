using BackOffice;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
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
            Logic(null);
        }


        public static bool Logic(byte[] token)
        {

            DbContextNet1 dbContextNet1 = new DbContextNet1();
            bool result = false;
            TransactionManager.DistributedTransactionStarted += TransactionManager_DistributedTransactionStarted;

            using (TransactionScope gScope = new TransactionScope())
            {
               
                    List<School> list = new List<School>();
                dbContextNet1.School.Add(new School() { Name = "school2" });
                dbContextNet1.School.Add(new School() { Name = "school3" });


                DisplayStates(dbContextNet1.ChangeTracker.Entries());
                //dbContextNet1.BulkSaveChanges();
                var sb = new StringBuilder();
                dbContextNet1.BulkSaveChanges( options =>
                {
                    options.Log = s => sb.AppendLine(s);
                });


                DisplayStates(dbContextNet1.ChangeTracker.Entries());

                //var s1 = dbContextNet1.School.First();
                //s1.Name = "mohamed";
            
               // dbContextNet1.School.AddRange(list);
              
              //  dbContextNet1.SaveChanges();
            
                //DbContextNet1 dbContextNet2 = new DbContextNet1();
                ////dbContextNet2.Database.UseTransaction(DbContextNet1_1.Database. );
                //dbContextNet2.Add(new School() { Name = "school" });                

                if (token == null)
                {
                    gScope.Complete();
                }

            }
            //    from medical
            if (token != null)
            {
                //DTC.Dtc dtc = new DTC.Dtc();
                //result = dtc.SyncDTCTransaction(token, SqlAggregate.SqlText.ToString());

                result = CallingDtcAPI(token, SqlAggregate.SqlText.ToString());

            }
            return result;
        }

        private static void DisplayStates(IEnumerable<EntityEntry> entries)
        {
            entries = entries.ToList();
            Console.WriteLine("_________________________________");
            foreach (var entry in entries)
            {
               
                Console.WriteLine($"Entity:{entry.Entity.GetType().Name},   State: { entry.State.ToString()}             ");
             
            }
            Console.WriteLine("_________________________________");
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
