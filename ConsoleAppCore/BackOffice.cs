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
            //  BindTransSession(token);

            //SqlConnection connection = new SqlConnection("Server =.; Database = BackOffice; User Id = sa; Password = sasa; Initial Catalog=BackOffice");

            //SqlCommand command = new SqlCommand($"      EXEC sp_bindsession '{token}'; --@out value$                  ", connection);
            //// enlist embient transcion 
            //command.Connection.Open();
            //command.ExecuteNonQuery();

            // act as global service Transaction
            DbContextNet1 dbContextNet1 = new DbContextNet1();
            //     using (TransactionScope gScope = new TransactionScope())
            TransactionScope gScope = new TransactionScope();
            {
                dbContextNet1.Database.ExecuteSqlRaw($"      EXEC sp_bindsession '{token}'     ");
                //  using (TransactionScope innerScope = new TransactionScope())               
                //   DbContextNet1 dbContextNet2 = new DbContextNet1();
                dbContextNet1.School.Add(new School() { Name = "school2" });
                dbContextNet1.School.Add(new School() { Name = "school3" });
                dbContextNet1.SaveChanges();
                gScope.Complete();
            }
            return true;
        }

        private static void BindTransSession(string token)
        {

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
