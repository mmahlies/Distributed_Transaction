using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BackOffice
{

    public class DbContextNet1 : DbContext
    {
        public DbContextNet1()
        {
            this.BindInterceptor(new ZInterceptor());
            this.ChangeTracker.StateChanged += ChangeTracker_StateChanged;
            this.ChangeTracker.Tracked += ChangeTracker_Tracked; ;
        }

        private void ChangeTracker_Tracked(object sender, Microsoft.EntityFrameworkCore.ChangeTracking.EntityTrackedEventArgs e)
        {
            var x = "";
        }

        private void ChangeTracker_StateChanged(object sender, Microsoft.EntityFrameworkCore.ChangeTracking.EntityStateChangedEventArgs e)
        {
            var x = "";
        }

        public DbSet<School> School { get; set; }

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter((c, l) =>
            c == DbLoggerCategory.Database.Command.Name && l == LogLevel.Information
            );

        });
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLoggerFactory(loggerFactory);
            // options.UseSqlServer("Data Source=.;Integrated Security=True;Initial Catalog=DBNet2");            
            options.UseSqlServer("Server=.;Database=BackOffice;User Id=sa;Password=sasa;");
            options.AddInterceptors(new Interceptor());
            


        }


    }

    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
