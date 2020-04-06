using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DTC
{
    public class Dtc
    {
        public static void Main(string[] args)
        {

            Console.Read();
        }

        public Transaction SyncDTCTransaction(byte[] token)
        {
            var x = Transaction.Current;
            
            using (NamedPipeServerStream s = new NamedPipeServerStream("n1", PipeDirection.InOut, 1, PipeTransmissionMode.Message))
            {
                s.WaitForConnection();
                byte[] buffer = new byte[int.MaxValue];

                do
                {
                    s.Read(buffer, 0, buffer.Length);
                 
                } while (true);
            }
            var tran = TransactionInterop.GetTransactionFromTransmitterPropagationToken(token);
            return tran;


            //using (TransactionScope transactionScope = new TransactionScope(tran))
            //{
            //    using (SqlConnection connection = new SqlConnection("Server=.;Database=BackOffice;User Id=sa;Password=sasa;"))
            //    {
            //        SqlCommand command = new SqlCommand();
            //        command.Connection = connection;
            //        command.CommandText = sql;
            //        connection.Open();
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {                                               
            //            reader.Close();
            //        }
            //    }
            //    transactionScope.Complete();
            //}



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


            //    return true;



            // exexute logic with in the distributed trnsaction


        }

   
    }
}
