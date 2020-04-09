using Newtonsoft.Json;
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

            }

        }

        private static void MedicalDTC()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                //   var token = TransactionInterop.GetTransmitterPropagationToken(Transaction.Current);
                MedicalContext medicalContext = new MedicalContext();
                medicalContext.Student.Add(new Student() { Name = "student from medical " });

                string sessionToken = GetSessionTokenFromAdo();
                string sessionToken2 = GetSessionTokenFromDbContext(medicalContext);

                var result = medicalContext.SaveChanges();


                CallingBackOffice(sessionToken2);
                scope.Complete();

            }

            var x = "";
        }



        private static string GetSessionTokenFromDbContext(MedicalContext dbContextNet1)
        {
            return dbContextNet1.Database.SqlQuery<string>(@"DECLARE @bind_token varchar(255);
                                                                        EXECUTE sp_getbindtoken @bind_token OUTPUT;
                                                                        SELECT @bind_token AS Token; ").FirstOrDefault();
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
            //; charset=utf-8
            var serializeToken = JsonConvert.SerializeObject(token);

            HttpContent tokenContent = new StringContent(serializeToken, Encoding.UTF8, "application/json");
            httpRequestMessage.Content = tokenContent;

            // back office api
            HttpResponseMessage response = client.SendAsync(httpRequestMessage).Result;
            var x = "";
        }

        private static void TransactionManager_DistributedTransactionStarted(object sender, TransactionEventArgs e)
        {
            var x = "";
        }
    }
}
