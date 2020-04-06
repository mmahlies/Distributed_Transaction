using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BackOffice
{

    public class DbContextNet2 : DbContext
    {
        public DbContextNet2()
        {
            
        }
        public DbSet<Teacher> Teacher  { get; set; }

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter((c, l) =>
            c == DbLoggerCategory.Database.Command.Name && l == LogLevel.Information
            );

        });
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLoggerFactory(loggerFactory);
            // options.UseSqlServer("Data Source=.;Integrated Security=True;Initial Catalog=DBNet1");
            options.UseSqlServer("Server =.; Database = BackOffice; User Id = sa; Password = sasa; Initial Catalog=BackOffice");
            options.AddInterceptors(new Interceptor());
            options.AddInterceptors(new DbTransactionInterceptor1()) ;
        }


    }

    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
