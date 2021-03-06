﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MedicalEF6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //   while (true)
            {
                // Console.Read();
                  MedicalDTC();
                //  Trans();
               // MedicalLogic.Logic();
            }

        }

        private static void MedicalDTC()
        {


            //   using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(5), TransactionScopeAsyncFlowOption.Enabled))
            using (TransactionScope scope = new TransactionScope())
            {
                //   var token = TransactionInterop.GetTransmitterPropagationToken(Transaction.Current);
                MedicalContext medicalContext = new MedicalContext();
                string sessionToken = GetSessionTokenFromAdo();
                string sessionToken2 = GetSessionTokenFromDbContext(medicalContext);
                medicalContext.Student.Add(new Student() { Name = "student from medical " });


                var result = medicalContext.SaveChanges();

              //  var backOfficeResult = CallingBackOffice(sessionToken);
                var finanicalResult = CallingFinancial(sessionToken);

                Task.WaitAll(new Task[] {  finanicalResult });

                scope.Complete();
            }
            var x = "";
        }



        private static string GetSessionTokenFromDbContext(MedicalContext dbContextNet1)
        {
            return dbContextNet1.Database.SqlQuery<string>(@"DECLARE @bind_token varchar(255);
                                                                        EXECUTE sp_getbindtoken @bind_token OUTPUT;
                                                                        SELECT @bind_token AS Token;  --GetSessionTokenFromDbContext ").FirstOrDefault();
        }

        private static string GetSessionTokenFromAdo()
        {
            string result = "";
            using (SqlConnection connection = new SqlConnection("Data Source=.;User Id=sa;Password=sasa;Initial Catalog=BackOffice"))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"DECLARE @bind_token varchar(255);  
                                EXECUTE sp_getbindtoken @bind_token OUTPUT;  
                                SELECT @bind_token AS Token; -- GetSessionTokenFromAdo";
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
                connection.Close();
            }
            return result;
        }
      

        private static Task<HttpResponseMessage> CallingBackOffice(string token)
        {
            HttpClient client = new HttpClient();

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost:54645/api/values");
            //; charset=utf-8
            var serializeToken = JsonConvert.SerializeObject(token);

            HttpContent tokenContent = new StringContent(serializeToken, Encoding.UTF8, "application/json");
            httpRequestMessage.Content = tokenContent;

            // back office api
            return client.SendAsync(httpRequestMessage);

        }
        private static Task<HttpResponseMessage> CallingFinancial(string token)
        {
            HttpClient client = new HttpClient();

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost:54902/api/values");
            //; charset=utf-8
            var serializeToken = JsonConvert.SerializeObject(token);

            HttpContent tokenContent = new StringContent(serializeToken, Encoding.UTF8, "application/json");
            httpRequestMessage.Content = tokenContent;

            return client.SendAsync(httpRequestMessage);
        }
        //

        private static void TransactionManager_DistributedTransactionStarted(object sender, TransactionEventArgs e)
        {
            var x = "";
        }
    }
}
