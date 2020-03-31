using BackOffice;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Transactions;

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

            bool result  =false;
            TransactionManager.DistributedTransactionStarted += TransactionManager_DistributedTransactionStarted;

            using (TransactionScope gScope = new TransactionScope())
            {               
                {
                    DbContextNet1 dbContextNet1 = new DbContextNet1();



                    List<School> list = new List<School>();
                    list.Add(new School() { Name = "school2" });
                    list.Add(new School() { Name = "school3" });
                    list.Add(new School() { Name = "school4" });
                    list.Add(new School() { Name = "school5" });


                    dbContextNet1.BulkInsert(list);


                    dbContextNet1.School.AddRange(list);
                    dbContextNet1.SaveChanges();


                    //DbContextNet1 dbContextNet2 = new DbContextNet1();
                    ////dbContextNet2.Database.UseTransaction(DbContextNet1_1.Database. );
                    //dbContextNet2.Add(new School() { Name = "school" });

                }
        
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
