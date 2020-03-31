using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Transactions;
using EFCoreTran.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFCoreTran
{
    class Program
    {
        static void Main(string[] args)
        {

            DoMultiple();
            var options = new DbContextOptionsBuilder<DBContext1>()
    .UseSqlServer(new Microsoft.Data.SqlClient.SqlConnection("Data Source=.;Initial Catalog=db1;Integrated Security=True; "))
    .Options;

            var options2 = new DbContextOptionsBuilder<DBContext2>()
  .UseSqlServer(new Microsoft.Data.SqlClient.SqlConnection("Data Source=.;Initial Catalog=db2;Integrated Security=True; "))
  .Options;

            using (var context1 = new DBContext1(options))
            {
                using (var transaction = context1.Database.BeginTransaction())
                {
                    try
                    {
                        context1.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/dotnet" });
                        context1.SaveChanges();

                        using (var context2 = new DBContext2(options2))
                        {
                            context2.Database.UseTransaction(transaction.GetDbTransaction());

                            context2.Add(new Persons() { Name = "m1" });


                        }
                        // Commit transaction if all commands succeed, transaction will auto-rollback
                        // when disposed if either commands fails
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        // TODO: Handle failure
                    }
                }
                ////     ///////////////////////////////////////////////
            }
                var optionsBuilder = new DbContextOptionsBuilder<DBContext1>();

            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=db1;Integrated Security=True; ");
            using (DBContext1 context1 = new DBContext1(optionsBuilder.Options))
            {
                using (var transaction = context1.Database.BeginTransaction())
                {
                    context1.Add(new Blog { Url = "url tran" });
                    context1.SaveChanges();
                    using (DBContext1 context2 = new DBContext1(optionsBuilder.Options))
                    {
                        DbTransaction tran = transaction.GetDbTransaction();
                        context2.Database.UseTransaction(tran);
                        

                        context1.Add(new Blog { Url = "url tran" });
                        //context2.Add(new Persons() { Name = "m1" });
                    }


                        transaction.Commit();
                    
                }
            }
        }

        private static void DoMultiple()
        {
            TransactionManager.DistributedTransactionStarted += TransactionManager_DistributedTransactionStarted;
            using (var connection = new SqlConnection("Data Source=.;Initial Catalog=db1;Integrated Security=True; "))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Run raw ADO.NET command in the transaction
                        var command = connection.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandText = "DELETE FROM dbo.Blogs";
                        command.ExecuteNonQuery();

                        // Run an EF Core command in the transaction
                        var options = new DbContextOptionsBuilder<DBContext2>()
 .UseSqlServer(new Microsoft.Data.SqlClient.SqlConnection("Data Source=.;Initial Catalog=db2;Integrated Security=True; "))
 .Options;
                       // IEnlistmentNotification

                        using (var context = new DBContext2(options))
                        {
                            context.Database.UseTransaction(transaction);
                            context.Add(new Persons() { Name = "m1" });
                            context.SaveChanges();
                        }

                        // Commit transaction if all commands succeed, transaction will auto-rollback
                        // when disposed if either commands fails
                        transaction.Commit();
                    }
                    catch (System.Exception ex)
                    {
                        // TODO: Handle failure
                    }
                }
            }
        }

        private static void TransactionManager_DistributedTransactionStarted(object sender, TransactionEventArgs e)
        {
            string x = "";
        }
    }
}
