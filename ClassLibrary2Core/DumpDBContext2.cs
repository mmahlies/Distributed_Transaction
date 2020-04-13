using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Dump
{

    public class DumpDBContext2 : DbContext
    {


        public DbSet<School> School { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<SessionToken> TokenSession { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // options.UseSqlServer("Data Source=.;Integrated Security=True;Initial Catalog=DBNet2");            
            options.UseSqlServer("Server=.;Database=BackOffice;User Id=sa;Password=sasa;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SessionToken>(eb =>
            {
                eb.HasNoKey();
            });
        }


    }

}
