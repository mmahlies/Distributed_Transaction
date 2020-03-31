using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

using Newtonsoft.Json;

namespace ConsoleAppNet
{
   public class Program
    {
       public static void Main(string[] args)
        {
            var targetFrameworkAttribute = System.Reflection.Assembly.GetExecutingAssembly()
    .GetCustomAttributes(typeof(System.Runtime.Versioning.TargetFrameworkAttribute), false)
    .SingleOrDefault();
            TransactionManager.DistributedTransactionStarted += TransactionManager_DistributedTransactionStarted;
            using (TransactionScope scope = new TransactionScope())
            {
                //TransactionInformation info = Transaction.Current.TransactionInformation;
                //IsolationLevel isolationLevel = Transaction.Current.IsolationLevel;
                TimeSpan defaultTimeout = TransactionManager.DefaultTimeout;
                TimeSpan maximumTimeout = TransactionManager.MaximumTimeout;

                //var r1 = info.LocalIdentifier;
                //var r2 = info.DistributedIdentifier;

                DbContextNet1 dbContextNet1 = new DbContextNet1();
                dbContextNet1.Add(new Student() { Name = "student2" });

                dbContextNet1.SaveChanges();


               var token = TransactionInterop.GetTransmitterPropagationToken(Transaction.Current);

                CallingBackOffice(token);

             //   DirectCallDTC(token);

               scope.Complete();
            }
                var x = "";
          
        }

        private static void DirectCallDTC(byte[] token)
        {
            DTC.Dtc dtc = new DTC.Dtc();
         dtc.SyncDTCTransaction(token, "");
        }

        private static void CallingBackOffice(byte[] token)
        {
             HttpClient client = new HttpClient();

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost:54645/api/values");


                               // back office api
HttpResponseMessage response = client.PostAsJsonAsync<byte[]>("http://localhost:54645/api/values", token).Result;
            var x = "";
        }

        private static void TransactionManager_DistributedTransactionStarted(object sender, TransactionEventArgs e)
        {
            var x = "";
        }
    }
}
