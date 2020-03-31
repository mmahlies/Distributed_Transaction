using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DTC
{
    public class Dtc
    {
        public bool SyncDTCTransaction(byte[] token, string sql)
        {

            var targetFrameworkAttribute = System.Reflection.Assembly.GetExecutingAssembly()
    .GetCustomAttributes(typeof(System.Runtime.Versioning.TargetFrameworkAttribute), false)
    .SingleOrDefault();

          
            var tran = TransactionInterop.GetTransactionFromTransmitterPropagationToken(token);

          
            using (TransactionScope transactionScope = new TransactionScope(tran))
            {
                using (SqlConnection connection = new SqlConnection("Server=.;Database=BackOffice;User Id=sa;Password=sasa;"))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = sql;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {                                               
                        reader.Close();
                    }
                }
                transactionScope.Complete();
            }



            //    using (var dbContextNet2 = new DbContextNet2())
            //using (var command = dbContextNet2.Database.GetDbConnection().CreateCommand())
            //{               
            //    command.CommandText = sql;
            //    dbContextNet2.Database.OpenConnection();
            //    using (var result = command.ExecuteReader())
            //    {
            //        var x = "";
            //    }
            //}


            return true;

            

            // exexute logic with in the distributed trnsaction


        }
    }
}
