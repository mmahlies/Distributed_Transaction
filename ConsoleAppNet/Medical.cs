using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ConsoleAppNet
{
    public class Program
    {
        public static void Main(string[] args)
        {           
         //   TransactionManager.DistributedTransactionStarted += TransactionManager_DistributedTransactionStarted;
            using (TransactionScope scope = new TransactionScope())
            {               
                DbContextNet1 dbContextNet1 = new DbContextNet1();

                dbContextNet1.Add(new Teacher() { Name = "t1" });

               var token = TransactionInterop.GetTransmitterPropagationToken(Transaction.Current);
                dbContextNet1.SaveChanges();

                 var token2 = GetToken();
             //   var token2 = dbContextNet1.Token.FromSqlRaw(@"DECLARE @bind_token varchar(255);  
               //                 EXECUTE sp_getbindtoken @bind_token OUTPUT;  
                 //               SELECT @bind_token AS Token;").AsEnumerable().FirstOrDefault().Token;
    


                //using (TransactionScope innerScope = new TransactionScope())
                //{
                //    DbContextNet1 dbContextNet2 = new DbContextNet1();
                //    dbContextNet1.Add(new Teacher() { Name = "t2" });
                //    dbContextNet1.SaveChanges();
                //    innerScope.Complete();
                //}

                CallingBackOffice(token2);
                scope.Complete();
            }
            var y = "";

        }

        private static string GetToken()
        {
            string result = "";
            using (SqlConnection connection = new SqlConnection("Data Source=.;User Id=sa;Password=sasa;Initial Catalog=BackOffice"))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"DECLARE @bind_token varchar(255);  
                                EXECUTE sp_getbindtoken @bind_token OUTPUT;  
                                SELECT @bind_token AS Token;";
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    {
                        if (reader.Read())
                        {

                            result = reader.GetString(0);
                            reader.Close();
                        }
                    }
                }
            }
            return result;
        }

        private static void DirectCallDTC(byte[] token)
        {
            //  DTC.Dtc dtc = new DTC.Dtc();
            //dtc.SyncDTCTransaction(token, "");
        }

        private static void CallingBackOffice(string token)
        {
            HttpClient client = new HttpClient();

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost:54645/api/values");


            // back office api
            HttpResponseMessage response = client.PostAsJsonAsync<string>("http://localhost:54645/api/values", token).Result;
            var x = "";
        }

        private static void TransactionManager_DistributedTransactionStarted(object sender, TransactionEventArgs e)
        {
            var x = "";
        }
    }
}
