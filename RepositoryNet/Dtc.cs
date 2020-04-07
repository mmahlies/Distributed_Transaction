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

         
        }

        public void SyncDTCTransaction(byte[] token)
        {


            Transaction tran = TransactionInterop.GetTransactionFromTransmitterPropagationToken(token);

            using (TransactionScope transactionScope = new TransactionScope(tran))
            {
                DbContextNet2 dbContextNet2 = new DbContextNet2();
                dbContextNet2.School.Add(new School() { Name = "dtc" });
                dbContextNet2.SaveChanges();
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


            //    return true;



            // exexute logic with in the distributed trnsaction


        }

   
    }
}
