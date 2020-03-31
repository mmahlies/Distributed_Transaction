
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Transactions;

namespace Lib1Core
{
    public class Core1
    {
        public string Method1() {

            var y = "";
#if NET472
      string x ="net472";
#elif NETCOREAPP2_2
            string x = "NETCOREAPP2_2";
#endif
            ///TransactionInformation info = Transaction.Current.TransactionInformation;
            BloggingContext dbContext = new BloggingContext();
            var tran = dbContext.Database.CurrentTransaction;
           // dbContext.Database.UseTransaction();
            dbContext.Add(new Blog() { Url = "t2" });
            dbContext.SaveChanges();

            return dbContext.Blogs.First().Url;
         //   return "welcome from core";
        }
    }
}
