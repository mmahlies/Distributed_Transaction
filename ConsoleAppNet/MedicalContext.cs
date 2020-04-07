using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ConsoleAppNet.MedicalContext
{

    public class MedicalContext : DbContext
    {

        public DbSet<Student> Student { get; set; }
        public DbSet<SessionToken> SessionToken { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // options.UseSqlServer("Data Source=.;Integrated Security=True;Initial Catalog=DBNet1");
            options.UseSqlServer("Server=.;Database=Medical;User Id=sa;Password=sasa;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SessionToken>(eb => {
                eb.HasNoKey();
            });
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


    public class SessionToken
    {
        public String Token { get; set; }
    }

}
